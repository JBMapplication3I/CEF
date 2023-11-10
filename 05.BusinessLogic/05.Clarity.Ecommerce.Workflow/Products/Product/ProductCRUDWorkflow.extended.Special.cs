// <copyright file="ProductCRUDWorkflow.extended.Special.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Interfaces.Models;
    using Interfaces.Providers.Personalization;
    using JetBrains.Annotations;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    public partial class ProductWorkflow
    {
        /// <summary>Gets the personalization providers pad lock.</summary>
        /// <value>The personalization providers pad lock.</value>
        private object PersonalizationProvidersPadLock { get; } = new();

        /// <summary>Gets or sets the personalization providers.</summary>
        /// <value>The personalization providers.</value>
        private List<IPersonalizationProviderBase>? PersonalizationProviders { get; set; }

        /// <inheritdoc/>
        public async Task<DataSet> ExportToExcelAsync(string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var products =
                from x in context.Products.AsNoTracking()
                let type = x.Type
                let pc = x.Categories!.FirstOrDefault(y => y.Active && y.Slave!.Active)
                let category = pc == null ? null : pc.Slave
                let parentCategory = category == null ? null : category.Parent
                orderby x.CustomKey, x.ID
                select
                    new ExportDTO
                    {
                        // Base Properties
                        ID = x.ID,
                        CustomKey = x.CustomKey,
                        Active = x.Active,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        // NameableBase Properties
                        Name = x.Name,
                        Description = x.Description,
                        // Other Properties
                        ////Image = imageName,
                        SeoUrl = x.SeoUrl,
                        ShortDescription = x.ShortDescription,
                        PriceBase = x.PriceBase,
                        PriceSale = x.PriceSale,
                        IsUnlimitedStock = x.IsUnlimitedStock,
                        IsVisible = x.IsVisible,
                        IsDiscontinued = x.IsDiscontinued,
                        HandlingCharge = x.HandlingCharge,
                        UnitOfMeasure = x.UnitOfMeasure,
                        SKU = x.CustomKey,
                        Category = parentCategory == null && category == null
                            ? null
                            : parentCategory != null
                                ? parentCategory.Name
                                : category.Name,
                        SubCategory = parentCategory == null && category == null
                            ? null
                            : parentCategory != null
                                ? category.Name
                                : null,
                        TypeKey = type.CustomKey,
                        TypeName = type.Name,
                        AssociatedProducts = x.ProductAssociations!
                            .Where(y => y.Active && y.Slave!.Active)
                            .Select(y => new KeyValueObject { Key = y.Slave!.CustomKey, Value = y.Slave.Name }),
                        InventoryLocations = x.ProductInventoryLocationSections!
                            .Where(y => y.Active && y.Slave!.Active && y.Slave!.InventoryLocation!.Active)
                            .Select(y => y.Slave!.InventoryLocation!.Name + ": " + y.Slave.Name),
                        JsonAttributes = x.JsonAttributes,
                    };
            return await ExportToExcelDataSetAsync(products).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<string> GenerateProductSiteMapContentAsync(string? contextProfileName)
        {
            // TODO: Read CORS path for catalog route instead
            var root = CEFConfigDictionary.SiteRouteHostUrl;
            while (root.EndsWith("/"))
            {
                root = root.TrimEnd('/');
            }
            var relativePath = CEFConfigDictionary.ProductDetailRouteRelativePath;
            while (relativePath.StartsWith("/"))
            {
                relativePath = relativePath.TrimStart('/');
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var urlSet = new ProductSiteMapUrlSet(
                (await context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterProductsByIsVisible(true)
                    .FilterProductsByIsDiscontinued(false)
                    .Where(x => x.SeoUrl != null && x.SeoUrl != string.Empty)
                    .Select(x => new
                    {
                        x.SeoUrl,
                        UpdatedDate = x.UpdatedDate ?? x.CreatedDate,
                    })
                    .OrderBy(x => x.SeoUrl)
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new ProductSiteMapUrl
                {
                    ChangeFrequency = "daily",
                    UpdatedDate = x.UpdatedDate.ToString("yyyy-MM-dd"),
                    Location = $"{root}/{relativePath}/{x.SeoUrl}",
                    Priority = 0.5,
                })
                .ToList());
            return urlSet
                .ToXmlString()
                .Replace("utf-16", "utf-8")
                .Replace(
                    "<urlset xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">",
                    "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xhtml=\"http://www.w3.org/1999/xhtml\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">")
                .Replace(
                    "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">",
                    "<urlset xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xhtml=\"http://www.w3.org/1999/xhtml\" xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\" xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");
        }

        /// <inheritdoc/>
        public async Task<bool> SaveProductSiteMapAsync(string content, string dropPath)
        {
            var fullPath = Path.Combine(dropPath, "ProductSiteMap.xml");
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            using var file = new StreamWriter(fullPath);
            await file.WriteAsync(content).ConfigureAwait(false);
            return true;
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetPersonalizationProductsForUserLastModifiedAsync(
            int? userID,
            string? contextProfileName)
        {
            if (!await EnsurePersonalizationProvidersAsync(contextProfileName).ConfigureAwait(false))
            {
                return null;
            }
            var resultIDs = new List<int>();
            foreach (var resultIDsInner in (await Task.WhenAll(PersonalizationProviders!
                        .Select(x => x.GetResultingProductIDsForUserIDAsync(
                            userID,
                            9,
                            contextProfileName)))
                        .ConfigureAwait(false))
                    .Where(resultIDsInner => resultIDsInner?.Any() == true))
            {
                resultIDs.AddRange(resultIDsInner);
            }
            if (resultIDs.Count == 0)
            {
                return null;
            }
            // Mappings we are familiar with
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByIDs(resultIDs.Distinct().ToArray())
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .OrderByDescending(x => x)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<IProductModel?>> GetPersonalizationProductsForUserAsync(
            int? userID,
            string? contextProfileName)
        {
            if (!await EnsurePersonalizationProvidersAsync(contextProfileName).ConfigureAwait(false))
            {
                return new();
            }
            var resultIDs = new List<int>();
            foreach (var resultIDsInner in (await Task.WhenAll(PersonalizationProviders!
                        .Select(x => x.GetResultingProductIDsForUserIDAsync(
                            userID,
                            9,
                            contextProfileName)))
                        .ConfigureAwait(false))
                    .Where(resultIDsInner => resultIDsInner?.Any() == true))
            {
                resultIDs.AddRange(resultIDsInner);
            }
            if (resultIDs.Count == 0)
            {
                return new();
            }
            // Mappings we are familiar with
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await Task.WhenAll(
                (await context.Products
                    .AsNoTracking()
                    .FilterByIDs(resultIDs.Distinct().ToArray()).ToListAsync().ConfigureAwait(false))
                .Select(p => p.ToProductCatalogItemAltAsync(
                    this,
                    true, ////CEFConfigDictionary.SearchingProductIndexResultsIncludeAssociatedProducts,
                    contextProfileName)))
                .ConfigureAwait(false))
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetPersonalizedCategoriesForUserLastModifiedAsync(
            int? userID,
            string? contextProfileName)
        {
            if (!await EnsurePersonalizationProvidersAsync(contextProfileName).ConfigureAwait(false))
            {
                return null;
            }
            var resultIDs = new List<int>();
            foreach (var provider in PersonalizationProviders!)
            {
                // ReSharper disable once PossibleInvalidOperationException
                var resultIDsInner = await provider.GetResultingCategoryIDsForUserIDAsync(
                        userID,
                        24,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (resultIDsInner?.Any() != true)
                {
                    continue;
                }
                resultIDs.AddRange(resultIDsInner);
            }
            if (resultIDs.Count == 0)
            {
                return null;
            }
            // Mappings we are familiar with
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Categories
                .AsNoTracking()
                .FilterByIDs(resultIDs.Distinct().ToArray())
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .OrderByDescending(x => x)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<ICategoryModel>> GetPersonalizedCategoriesForUserAsync(
            int? userID,
            string? contextProfileName)
        {
            if (!await EnsurePersonalizationProvidersAsync(contextProfileName).ConfigureAwait(false))
            {
                return new();
            }
            var resultIDs = new List<int>();
            foreach (var provider in PersonalizationProviders!)
            {
                // ReSharper disable once PossibleInvalidOperationException
                var resultIDsInner = await provider.GetResultingCategoryIDsForUserIDAsync(
                        userID,
                        24,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (resultIDsInner?.Any() != true)
                {
                    continue;
                }
                resultIDs.AddRange(resultIDsInner);
            }
            if (resultIDs.Count == 0)
            {
                return new();
            }
            // Mappings we are familiar with
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.Categories
                .AsNoTracking()
                .FilterByIDs(resultIDs.Distinct().ToArray())
                .SelectListCategoryAndMapToCategoryModel(contextProfileName)
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetPersonalizedCategoryAndProductFeedForUserIDLastModifiedAsync(
            int? userID,
            string? contextProfileName)
        {
            if (!await EnsurePersonalizationProvidersAsync(contextProfileName).ConfigureAwait(false))
            {
                return null;
            }
            var resultIDs = new Dictionary<int /*categoryID*/, List<int> /*productIDs*/>();
            foreach (var provider in PersonalizationProviders!)
            {
                // ReSharper disable once PossibleInvalidOperationException
                var resultIDsInner = await provider.GetResultingFeedForUserIDAsync(
                        userID,
                        24,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (resultIDsInner?.Any() != true)
                {
                    continue;
                }
                foreach (var categoryID in resultIDsInner.Keys)
                {
                    if (!resultIDs.ContainsKey(categoryID))
                    {
                        resultIDs[categoryID] = new();
                    }
                    resultIDs[categoryID].AddRange(resultIDsInner[categoryID]);
                }
            }
            if (resultIDs.Count == 0)
            {
                return null;
            }
            // Mappings we are familiar with
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var result = DateTime.MinValue;
            foreach (var resultCategory in context.Categories
                                            .AsNoTracking()
                                            .FilterByIDs(resultIDs.Keys.Distinct().ToArray())
                                            .SelectListCategoryAndMapToCategoryModel(contextProfileName))
            {
                // Mappings we are familiar with
                var max = await context.Products
                    .FilterByActive(true)
                    // ReSharper disable once PossibleInvalidOperationException
                    .FilterByIDs(resultIDs[resultCategory.ID].Distinct().ToArray())
                    .Select(x => x.UpdatedDate ?? x.CreatedDate)
                    .OrderByDescending(x => x)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                result = max > result ? max : result;
            }
            return result;
        }

        /// <inheritdoc/>
        public async Task<Dictionary<ICategoryModel, List<IProductModel?>>> GetPersonalizedCategoryAndProductFeedForUserIDAsync(
            int? userID,
            string? contextProfileName)
        {
            if (!await EnsurePersonalizationProvidersAsync(contextProfileName).ConfigureAwait(false))
            {
                return new();
            }
            var resultIDs = new Dictionary<int /*categoryID*/, List<int> /*productIDs*/>();
            foreach (var provider in PersonalizationProviders!)
            {
                // ReSharper disable once PossibleInvalidOperationException
                var resultIDsInner = await provider.GetResultingFeedForUserIDAsync(
                        userID,
                        24,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (resultIDsInner?.Any() != true)
                {
                    continue;
                }
                foreach (var categoryID in resultIDsInner.Keys)
                {
                    if (!resultIDs.ContainsKey(categoryID))
                    {
                        resultIDs[categoryID] = new();
                    }
                    resultIDs[categoryID].AddRange(resultIDsInner[categoryID]);
                }
            }
            if (resultIDs.Count == 0)
            {
                return new();
            }
            // Mappings we are familiar with
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var results = new Dictionary<ICategoryModel, List<IProductModel?>>();
            foreach (var resultCategory in context.Categories
                                            .AsNoTracking()
                                            .FilterByIDs(resultIDs.Keys.Distinct().ToArray())
                                            .SelectListCategoryAndMapToCategoryModel(contextProfileName))
            {
                results[resultCategory] = new();
                // Mappings we are familiar with
                results[resultCategory] = (await Task.WhenAll(
                    (await context.Products
                        // ReSharper disable once PossibleInvalidOperationException
                        .FilterByIDs(resultIDs[resultCategory.ID].Distinct().ToArray())
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Select(p => p.ToProductCatalogItemAltAsync(
                            this,
                            true, ////CEFConfigDictionary.SearchingProductIndexResultsIncludeAssociatedProducts,
                            contextProfileName)))
                        .ConfigureAwait(false))
                    .ToList();
            }
            return results;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ProductUpdateNotificationsAsync(
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName)
        {
            try
            {
                return await CEFAR.AggregateAsync(
                    await GetUpdatedProductsAsync(days, pricingFactoryContext, categorySeoUrl, contextProfileName).ConfigureAwait(false),
                    async x => await Workflows.ProductNotifications.CreateAsync(
                            new ProductNotificationModel
                            {
                                Active = true,
                                ProductID = x!.ID,
                                Name = "Product Changed",
                                Description = $"Updated: {x.UpdatedDate:medium}",
                            },
                            contextProfileName)
                        .ConfigureAwait(false))
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        $"{nameof(ProductWorkflow)}.{nameof(ProductUpdateNotificationsAsync)}.{ex.GetType().Name}",
                        ex.Message,
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR(ex.Message);
            }
        }

        /// <summary>Export to excel.</summary>
        /// <param name="products">The products.</param>
        /// <returns>A DataSet.</returns>
        private static Task<DataSet> ExportToExcelDataSetAsync(IEnumerable<ExportDTO> products)
        {
            var dataSet = new DataSet();
            var productTable = dataSet.Tables.Add("Products");
            // Base Properties
            productTable.Columns.Add("ID");
            productTable.Columns.Add("CustomKey");
            productTable.Columns.Add("Active");
            productTable.Columns.Add("CreatedDate");
            productTable.Columns.Add("UpdatedDate");
            // NameableBase Properties
            productTable.Columns.Add("Name");
            productTable.Columns.Add("Description");
            // Other Properties
            productTable.Columns.Add("SeoUrl");
            productTable.Columns.Add("ShortDescription");
            productTable.Columns.Add("PriceBase");
            productTable.Columns.Add("PriceSale");
            productTable.Columns.Add("HandlingCharge");
            productTable.Columns.Add("SKU");
            productTable.Columns.Add("TypeKey");
            productTable.Columns.Add("TypeName");
            productTable.Columns.Add("UnitOfMeasure");
            productTable.Columns.Add("IsUnlimitedStock");
            productTable.Columns.Add("IsVisible");
            productTable.Columns.Add("IsDiscontinued");
            productTable.Columns.Add("InventoryLocations");
            productTable.Columns.Add("Category");
            productTable.Columns.Add("SubCategory");
            productTable.Columns.Add("ShippingSKU");
            productTable.Columns.Add("AssociatedProductName");
            productTable.Columns.Add("AssociatedProductSKU");
            productTable.Columns.Add("PaymentGateway");
            foreach (var product in products)
            {
                var row = productTable.NewRow();
                // Base Properties
                row["ID"] = product.ID;
                row["CustomKey"] = product.CustomKey;
                row["Active"] = product.Active;
                row["CreatedDate"] = product.CreatedDate;
                row["UpdatedDate"] = product.UpdatedDate;
                // NameableBase Properties
                row["Name"] = product.Name;
                row["Description"] = product.Description;
                // Other Properties
                row["InventoryLocations"] = product.InventoryLocations!.DefaultIfEmpty(string.Empty).Aggregate((c, n) => c + ", " + n);
                row["HandlingCharge"] = product.HandlingCharge;
                row["Category"] = product.Category;
                row["SubCategory"] = product.SubCategory;
                row["AssociatedProductName"] = product.AssociatedProducts!.Select(x => x.Value).DefaultIfEmpty(string.Empty).Aggregate((c, n) => c + ", " + n);
                row["AssociatedProductSKU"] = product.AssociatedProducts!.Select(x => x.Key).DefaultIfEmpty(string.Empty).Aggregate((c, n) => c + ", " + n);
                row["SeoUrl"] = product.SeoUrl;
                row["ShortDescription"] = product.ShortDescription;
                row["PriceBase"] = product.PriceBase;
                row["PriceSale"] = product.PriceSale;
                row["HandlingCharge"] = product.HandlingCharge;
                row["IsUnlimitedStock"] = product.IsUnlimitedStock;
                row["IsVisible"] = product.IsVisible;
                row["IsDiscontinued"] = product.IsDiscontinued;
                row["UnitOfMeasure"] = product.UnitOfMeasure;
                row["SKU"] = product.SKU;
                row["TypeKey"] = product.TypeKey;
                row["TypeName"] = product.TypeName;
                // Attributes
                product.SerializableAttributes = product.JsonAttributes.DeserializeAttributesDictionary();
                foreach (var key in product.SerializableAttributes.Keys)
                {
                    if (!productTable.Columns.Contains(key))
                    {
                        productTable.Columns.Add(key);
                    }
                    row[key] = product.SerializableAttributes[key].Value;
                    if (!Contract.CheckValidKey(product.SerializableAttributes[key].UofM))
                    {
                        continue;
                    }
                    if (!productTable.Columns.Contains(key + " UofM"))
                    {
                        productTable.Columns.Add(key + "UofM");
                    }
                    row[key + "UofM"] = product.SerializableAttributes[key].UofM;
                }
                // Add the product row
                productTable.Rows.Add(row);
            }
            return Task.FromResult(dataSet);
        }

        /// <summary>Ensures that personalization providers.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private Task<bool> EnsurePersonalizationProvidersAsync(string? contextProfileName)
        {
            lock (PersonalizationProvidersPadLock)
            {
                if (PersonalizationProviders?.Any() == true)
                {
                    return Task.FromResult(true);
                }
                PersonalizationProviders = RegistryLoaderWrapper.GetPersonalizationProviders(contextProfileName);
                return Task.FromResult(PersonalizationProviders?.Any() == true);
            }
        }

        /// <summary>A product site map url.</summary>
        [Serializable, XmlType("url"), PublicAPI]
        public class ProductSiteMapUrl
        {
            /// <summary>Gets or sets the location.</summary>
            /// <value>The location.</value>
            [XmlElement("loc")]
            public string? Location { get; set; }

            /// <summary>Gets or sets the updated date.</summary>
            /// <value>The updated date.</value>
            [XmlElement("lastmod")]
            public string? UpdatedDate { get; set; }

            /// <summary>Gets or sets the change frequency.</summary>
            /// <value>The change frequency.</value>
            [XmlElement("changefreq")]
            public string? ChangeFrequency { get; set; }

            /// <summary>Gets or sets the priority.</summary>
            /// <value>The priority.</value>
            [XmlElement("priority")]
            public double Priority { get; set; }
        }

        /// <summary>A product site map URL set.</summary>
        /// <seealso cref="List{ProductSiteMapUrl}"/>
        [Serializable, XmlType("urlset"), PublicAPI]
        public class ProductSiteMapUrlSet : List<ProductSiteMapUrl>
        {
            /// <summary>Initializes a new instance of the <see cref="ProductSiteMapUrlSet"/> class.</summary>
            public ProductSiteMapUrlSet()
            {
            }

            /// <summary>Initializes a new instance of the <see cref="ProductSiteMapUrlSet"/> class.</summary>
            /// <param name="source">Source for the.</param>
            public ProductSiteMapUrlSet(IEnumerable<ProductSiteMapUrl> source)
                : base(source)
            {
            }
        }

        private class ExportDTO : NameableBaseModel
        {
            public string? SeoUrl { get; set; }

            public string? ShortDescription { get; set; }

            public decimal? PriceBase { get; set; }

            public decimal? PriceSale { get; set; }

            public decimal? HandlingCharge { get; set; }

            public bool? IsUnlimitedStock { get; set; }

            public bool? IsVisible { get; set; }

            public bool? IsDiscontinued { get; set; }

            public string? UnitOfMeasure { get; set; }

            public string? SKU { get; set; }

            public string? Category { get; set; }

            public string? SubCategory { get; set; }

            public string? TypeKey { get; set; }

            public string? TypeName { get; set; }

            public IEnumerable<KeyValueObject>? AssociatedProducts { get; set; }

            public IEnumerable<string>? InventoryLocations { get; set; }

            public string? JsonAttributes { get; set; }
        }

        private class KeyValueObject
        {
            public string? Key { get; set; }

            public string? Value { get; set; }
        }
    }
}
