// <copyright file="ImporterProviderBase.V1.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the importer provider base class</summary>
namespace Clarity.Ecommerce.Providers.Importer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Importer;
    using Models;
    using Utilities;

    public abstract partial class ImporterProviderBase
    {
        /// <summary>The general attribute cache.</summary>
        private static readonly Dictionary<string, IGeneralAttributeModel> GeneralAttributeCache = new();

        /// <summary>The attribute type identifier for product.</summary>
        private static int attributeTypeIDForProduct;

        /// <inheritdoc/>
        public Dictionary<string /*fileName*/, Dictionary<string /*columnName*/, uint /*columnNumber*/>> ColumnByName { get; } = new();

        /// <inheritdoc/>
        public Dictionary<string /*fileName*/, Dictionary<string /*columnName*/, uint /*columnNumber*/>> ColumnByNameUofMs { get; } = new();

        /// <inheritdoc/>
        public Dictionary<string /*fileName*/, Dictionary<string /*columnName*/, Enums.ProductImportFieldEnum>> ImportMappings { get; set; } = new();

        /// <inheritdoc/>
        public virtual string? GoogleAccessToken { get; set; }

        /// <inheritdoc/>
        public virtual string? GoogleClientId { get; set; }

        /// <inheritdoc/>
        public virtual Dictionary<string /*fileName*/, int?> StoreID { get; } = new();

        /// <inheritdoc/>
        public virtual Dictionary<string /*fileName*/, int?> BrandID { get; } = new();

        /// <inheritdoc/>
        public virtual Dictionary<string /*fileName*/, int?> VendorID { get; } = new();

        /// <inheritdoc/>
        public virtual bool AllowCreateCategories { get; set; } = true;

        /// <inheritdoc/>
        public Dictionary<string /*fileName*/, ImportResponse> Responses { get; set; } = new();

        /// <summary>Gets the rows.</summary>
        /// <value>The rows.</value>
        protected Dictionary<string /*fileName*/, Row[]> Rows { get; } = new();

        /// <inheritdoc/>
        public abstract Task<List<string>> ReadWorkbookHeaderInfoAsync(string fileName, string? contextProfileName);

        /// <inheritdoc/>
        public async Task ImportAsync(string fileName, string? contextProfileName)
        {
            // SetupImageCdn();
            await ReadCellDataAsync(fileName, contextProfileName).ConfigureAwait(false);
            var parentCategoryHash = await ImportProductCategoriesAsync(fileName, contextProfileName).ConfigureAwait(false);
            await ImportProductsAsync(fileName, parentCategoryHash, contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Enumerates entry by column name UofM in this collection.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <param name="name">    The name.</param>
        /// <returns>An enumerator that allows foreach to be used to process entry by column name UofM in this collection.</returns>
        protected IEnumerable<Cell> EntryByColumnNameUofM(string fileName, string name)
        {
            var colIndex = ColumnByNameUofMs[fileName][name];
            foreach (var row in Rows[fileName].Where(row => row?.EntryByColumn.ContainsKey(colIndex) == true))
            {
                yield return row.EntryByColumn[colIndex];
            }
        }

        /// <summary>Reads cell data.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected abstract Task ReadCellDataAsync(string fileName, string? contextProfileName);

        /// <summary>Try get cell data by field enum.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <param name="field">   The field.</param>
        /// <param name="row">     The row.</param>
        /// <returns>A Task{(bool success,string value,string unit)}.</returns>
        private static async Task<(bool success, string? value, string? unit)> TryGetCellDataByFieldEnumAsync(
            string fileName,
            Enums.ProductImportFieldEnum field,
            IRow row)
        {
            var entry = await row.EntryByMappingFieldAsync(fileName, field).ConfigureAwait(false);
            return (entry == null, entry?.Value, entry?.UofM);
        }

        /// <summary>Determine import action.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <param name="row">     The row.</param>
        /// <returns>A Task{ImportAction}.</returns>
        private static async Task<Enums.ImportAction> DetermineImportActionAsync(string fileName, IRow row)
        {
            var (hadValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, Enums.ProductImportFieldEnum.ImportAction, row).ConfigureAwait(false);
            if (!hadValue)
            {
                return Enums.ImportAction.CreateOrUpdate;
            }
            return Enum.TryParse(value, out Enums.ImportAction parsed) ? parsed : Enums.ImportAction.DoNothing;
        }

        /// <summary>Loop manufacturers.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="field">             The field.</param>
        /// <param name="row">               The row.</param>
        /// <param name="product">           The product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{bool}.</returns>
        private static async Task<bool> LoopManufacturersAsync(
            string fileName,
            Enums.ProductImportFieldEnum field,
            IRow row,
            IProductModel product,
            string? contextProfileName)
        {
            var productIsDirty = false;
            var (hadValue, value, _) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
            if (!hadValue)
            {
                return false;
            }
            var manufacturers = value?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (!manufacturers?.Any() != true)
            {
                return false;
            }
            product.Manufacturers ??= new();
            var existingList = product.Manufacturers;
            foreach (var manufacturer in manufacturers!)
            {
                if (existingList.Any(x => x.Active && x.MasterName == manufacturer))
                {
                    continue;
                }
                var existingManufacturer = await Workflows.Manufacturers.GetByNameAsync(manufacturer, contextProfileName).ConfigureAwait(false);
                ManufacturerProductModel newEntity;
                if (existingManufacturer?.ID == null)
                {
                    newEntity = new()
                    {
                        Active = true,
                        CreatedDate = DateExtensions.GenDateTime,
                        Master = new()
                        {
                            Active = true,
                            CreatedDate = DateExtensions.GenDateTime,
                            Name = manufacturer,
                        },
                    };
                }
                else
                {
                    newEntity = new()
                    {
                        Active = true,
                        CreatedDate = DateExtensions.GenDateTime,
                        MasterID = existingManufacturer.ID,
                    };
                }
                existingList.Add(newEntity);
                productIsDirty = true;
            }
            product.Manufacturers = existingList;
            return productIsDirty;
        }

        /// <summary>Loop vendors .</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="field">             The field.</param>
        /// <param name="row">               The row.</param>
        /// <param name="product">           The product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{bool}.</returns>
        private static async Task<bool> LoopVendorsAsync(
            string fileName,
            Enums.ProductImportFieldEnum field,
            IRow row,
            IProductModel product,
            string? contextProfileName)
        {
            var productIsDirty = false;
            // ReSharper disable once UnusedVariable
            var (hadValue, value, unit) = await TryGetCellDataByFieldEnumAsync(fileName, field, row).ConfigureAwait(false);
            if (!hadValue)
            {
                return false;
            }
            var vendors = value?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (!vendors?.Any() != true)
            {
                return false;
            }
            product.Vendors ??= new();
            var existingList = product.Vendors;
            foreach (var vendor in vendors!)
            {
                if (existingList.Any(x => x.Active && x.MasterName == vendor))
                {
                    continue;
                }
                var existingVendor = await Workflows.Vendors.GetByNameAsync(vendor, contextProfileName).ConfigureAwait(false);
                IVendorProductModel newEntity;
                if (existingVendor?.ID == null)
                {
                    newEntity = new VendorProductModel
                    {
                        Active = true,
                        CreatedDate = DateExtensions.GenDateTime,
                        Master = new()
                        {
                            Active = true,
                            CreatedDate = DateExtensions.GenDateTime,
                            Name = vendor,
                        },
                    };
                }
                else
                {
                    newEntity = new VendorProductModel
                    {
                        Active = true,
                        CreatedDate = DateExtensions.GenDateTime,
                        MasterID = existingVendor.ID,
                    };
                }
                existingList.Add(newEntity);
                productIsDirty = true;
            }
            product.Vendors = existingList;
            return productIsDirty;
        }

        /// <summary>Enumerates entry by column name in this collection.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <param name="name">    The name.</param>
        /// <returns>An enumerator that allows foreach to be used to process entry by column name in this collection.</returns>
        private IEnumerable<Cell> EntryByColumnName(string fileName, string name)
        {
            var colIndex = ColumnByName[fileName][name];
            foreach (var row in Rows[fileName].Where(row => row?.EntryByColumn.ContainsKey(colIndex) == true))
            {
                yield return row.EntryByColumn[colIndex];
            }
        }

        /// <summary>Import products.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="parentCategoryHash">The parent category hash.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task ImportProductsAsync(
            string fileName,
            IReadOnlyDictionary<int, Dictionary<string, int>> parentCategoryHash,
            string? contextProfileName)
        {
            // Note: We have to store the relationships and do them at the end after all the products have been loaded.
            var relationships = new Dictionary<IProductModel, Dictionary<string /* relationship type name */, List<string /* specific skus/product names that it relates too */>>>();
            // Import Products
            for (var rowIndex = 1; rowIndex < Rows[fileName].Length; rowIndex++)
            {
                try
                {
                    await ImportProductAsync(fileName, parentCategoryHash, rowIndex, relationships, contextProfileName).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    await Logger.LogErrorAsync("ProductImport.Error", e.Message, e, contextProfileName).ConfigureAwait(false);
                    Responses[fileName].ErrorMessages.Add(e.Message);
                }
            }
            // Products added. Now let's connect those related products
            var now = DateExtensions.GenDateTime;
            foreach (var pair2 in relationships)
            {
                var productModel = pair2.Key;
                var associatedProductsList = new List<IProductAssociationModel>();
                foreach (var pair in pair2.Value)
                {
                    var productAssociationTypeName = pair.Key;
                    var typeModel = await Workflows.ProductAssociationTypes.GetByNameAsync(productAssociationTypeName, contextProfileName).ConfigureAwait(false);
                    if (typeModel == null)
                    {
                        Responses[fileName].InfoMessages.Add($"Warning: Missing Product Association Type named '{productAssociationTypeName}'. Continuing with import without this association assignment");
                        Debug.WriteLine(Responses[fileName].InfoMessages.Last());
                        continue;
                    }
                    if (!Contract.CheckValidID(productModel.ID))
                    {
                        continue;
                    }
                    // ReSharper disable once UseObjectOrCollectionInitializer
                    var relatedProductsAlreadyAdded = new HashSet<int>
                    {
                        productModel.ID, // So we don't add a link to itself
                    }; // So we don't add one more than once
                    foreach (var sku2 in
                        from relationshipsString in pair.Value
                        select relationshipsString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray()
                        into split
                        from sku in split
                        select sku.Trim())
                    {
                        // Note: searching both CustomKey/SKU and Name
                        foreach (var result2 in (await Workflows.Products.SearchAsync(new ProductSearchModel { CustomKey = sku2 }, true, contextProfileName).ConfigureAwait(false))
                            .results
                            .Concat((await Workflows.Products.SearchAsync(new ProductSearchModel { Name = sku2 }, true, contextProfileName).ConfigureAwait(false)).results)
                            .Where(result2 => Contract.CheckValidID(result2.ID) && !relatedProductsAlreadyAdded.Contains(result2.ID)))
                        {
                            associatedProductsList.Add(new ProductAssociationModel
                            {
                                Active = true,
                                SlaveID = result2.ID,
                                CreatedDate = now,
                                MasterID = productModel.ID,
                                TypeID = typeModel.ID,
                            });
                            relatedProductsAlreadyAdded.Add(result2.ID);
                        }
                    }
                }
                if (!associatedProductsList.Any())
                {
                    continue;
                }
                productModel.ProductAssociations = associatedProductsList;
                await Workflows.Products.UpdateAsync(productModel, contextProfileName).ConfigureAwait(false);
                Responses[fileName].InfoMessages.Add($"Updated Product Associations for <i>'{productModel.Name}'</i>");
                Debug.WriteLine(Responses[fileName].InfoMessages.Last());
            }
        }

        /// <summary>Import product.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="parentCategoryHash">The parent category hash.</param>
        /// <param name="rowIndex">          Zero-based index of the row.</param>
        /// <param name="relationships">     The relationships.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task ImportProductAsync(
            string fileName,
            IReadOnlyDictionary<int, Dictionary<string, int>> parentCategoryHash,
            int rowIndex,
            IDictionary<IProductModel, Dictionary<string /* relationship type name */, List<string /* specific skus/product names that it relates too */>>> relationships,
            string? contextProfileName)
        {
            Debug.WriteLine($"Importing product #{rowIndex - 1}...");
            var row = Rows[fileName][rowIndex];
            if (row == null)
            {
                return;
            }
            var productName = await row.EntryByMappingFieldAsync(fileName, Enums.ProductImportFieldEnum.Name).ConfigureAwait(false);
            var productNameTemp = productName?.Value
                ?? throw new ArgumentNullException(nameof(productName), "Product Name cannot be null or unmapped.");
            Debug.WriteLine($"'{productNameTemp}'...");
            var productCategoryColumns = ImportMappings[fileName]
                .Keys
                .Where(k => ImportMappings[fileName][k] == Enums.ProductImportFieldEnum.Categories);
            // Lookup our product category
            var parentCategoryIDs = GetParentCategoryIDs(fileName, parentCategoryHash, productCategoryColumns, row);
            // Does the product already exist?
            var product = await Workflows.Products.GetByNameAsync(productNameTemp, contextProfileName).ConfigureAwait(false);
            var tryCount = 1;
            while (product?.Stores!.All(x => x.StoreID != StoreID[fileName]) == true)
            {
                var msg = $"Could not utilize Product name '<i>{productNameTemp}</i>' as it already exists and is not assigned"
                    + $" to your store. Appending ' ({++tryCount})' to the name and retrying. This may occur multiple times.";
                Responses[fileName].InfoMessages.Add(msg);
                Debug.WriteLine(msg);
                productNameTemp += $" ({tryCount})";
                product = await Workflows.Products.GetByNameAsync(productNameTemp, contextProfileName).ConfigureAwait(false);
            }
            while (product?.Brands!.All(x => x.BrandID != BrandID[fileName]) == true)
            {
                var msg = $"Could not utilize Product name '<i>{productNameTemp}</i>' as it already exists and is not assigned"
                    + $" to your brand. Appending ' ({++tryCount})' to the name and retrying. This may occur multiple times.";
                Responses[fileName].InfoMessages.Add(msg);
                Debug.WriteLine(msg);
                productNameTemp += $" ({tryCount})";
                product = await Workflows.Products.GetByNameAsync(productNameTemp, contextProfileName).ConfigureAwait(false);
            }
            if (product?.Stores!.All(x => x.StoreID != StoreID[fileName]) == true)
            {
                var msg = $"Skipping '{productNameTemp}' as already exists and not assigned to the store after multiple attempts to resolve.";
                Responses[fileName].InfoMessages.Add(msg);
                Debug.WriteLine(msg);
                return;
            }
            if (product?.Brands!.All(x => x.BrandID != BrandID[fileName]) == true)
            {
                var msg = $"Skipping '{productNameTemp}' as already exists and not assigned to the brand after multiple attempts to resolve.";
                Responses[fileName].InfoMessages.Add(msg);
                Debug.WriteLine(msg);
                return;
            }
            if (product == null)
            {
                // We found a name we can use that isn't in the system
                var description = (await row.EntryByMappingFieldAsync(fileName, Enums.ProductImportFieldEnum.Description).ConfigureAwait(false))?.Value;
                var createResponse = await Workflows.Products.CreateAsync(
                        new ProductModel
                        {
                            // Base Properties
                            Active = true,
                            // NameableBase Properties
                            Name = productNameTemp,
                            Description = description,
                            // IHaveATypeBase Properties
                            TypeID = 1,
                            // IHaveSeoBase Properties
                            SeoUrl = productNameTemp.ToSeoUrl(),
                            // Product Properties
                            IsVisible = true,
                            // Associated Objects
                            ProductCategories = parentCategoryIDs.Select(x => new ProductCategoryModel { Active = true, SlaveID = x }).ToList(),
                            // ReSharper disable PossibleInvalidOperationException
                            Stores = Contract.CheckValidID(StoreID[fileName]) ? new[] { new StoreProductModel { Active = true, MasterID = StoreID[fileName]!.Value, IsVisibleIn = true } }.ToList() : null,
                            Brands = Contract.CheckValidID(StoreID[fileName]) ? new[] { new BrandProductModel { Active = true, MasterID = BrandID[fileName]!.Value, IsVisibleIn = true } }.ToList() : null,
                            // ReSharper restore PossibleInvalidOperationException
                        },
                        contextProfileName)
                    .ConfigureAwait(false);
                product = await Workflows.Products.GetAsync(createResponse.Result, contextProfileName).ConfigureAwait(false);
                Responses[fileName].InfoMessages.Add($"Created new Product <i>'{product!.Name}'</i> (ID: {product.ID}) and will now apply values");
            }
            Responses[fileName].InfoMessages.Add($"Will Update existing Product <i>'{product.Name}'</i> (ID: {product.ID}) with new values");
            Debug.WriteLine(Responses[fileName].InfoMessages.Last());
            await ProcessDirtyProductAsync(fileName, row, product, relationships, contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Gets the parent category IDs in this collection.</summary>
        /// <param name="fileName">              Filename of the file.</param>
        /// <param name="parentCategoryHash">    The parent category hash.</param>
        /// <param name="productCategoryColumns">The product category columns.</param>
        /// <param name="row">                   The row.</param>
        /// <returns>An enumerator that allows foreach to be used to process the parent category IDs in this collection.</returns>
        private IEnumerable<int> GetParentCategoryIDs(string fileName, IReadOnlyDictionary<int, Dictionary<string, int>> parentCategoryHash, IEnumerable<string> productCategoryColumns, IRow row)
        {
            var parentCategoryIDs = new List<int>();
            foreach (var catSet in productCategoryColumns
                .Select(cn => row.EntryByColumn.Union(row.EntryByColumnUofM)
                    .First(x => x.Key == ColumnByName[fileName][cn] || x.Value.Column == ColumnByName[fileName][cn])
                    .Value.Value!.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(cat => cat.Trim())
                    .ToArray())
                .SelectMany(treeSplit => treeSplit))
            {
                string? catName;
                int id;
                if (!catSet.Contains(","))
                {
                    catName = catSet;
                    id = parentCategoryHash[-1][catName];
                    if (id > 0)
                    {
                        parentCategoryIDs.Add(id);
                    }
                    continue;
                }
                var split = catSet.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(cat => cat.Trim()).ToArray();
                catName = split.LastOrDefault();
                var catParentName = split.Length == 3 ? split[^2] : split.First();
                var parentID = -1;
                if (!string.IsNullOrWhiteSpace(catParentName))
                {
                    foreach (var kvp in parentCategoryHash)
                    {
                        foreach (var kvp2 in kvp.Value.Where(kvp2 => kvp2.Key == catParentName))
                        {
                            parentID = kvp2.Value;
                        }
                        if (parentID != -1)
                        {
                            break;
                        }
                    }
                    //// parentID = parentCategoryHash[-1][catParentName];
                }
                if (string.IsNullOrWhiteSpace(catName))
                {
                    continue;
                }
                id = parentCategoryHash[parentID > 0 ? parentID : -1][catName];
                if (id > 0)
                {
                    parentCategoryIDs.Add(id);
                }
            }
            return parentCategoryIDs;
        }

        /// <summary>Process the dirty product.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="row">               The row.</param>
        /// <param name="product">           The product.</param>
        /// <param name="relationships">     The relationships.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task ProcessDirtyProductAsync(
            string fileName,
            IRow row,
            IProductModel product,
            IDictionary<IProductModel, Dictionary<string /* relationship type name */, List<string /* specific skus/product names that it relates too */>>> relationships,
            string? contextProfileName)
        {
            var importAction = await DetermineImportActionAsync(fileName, row).ConfigureAwait(false);
            switch (importAction)
            {
                case Enums.ImportAction.DoNothing:
                {
                    break;
                }
                case Enums.ImportAction.Deactivate:
                {
                    await Workflows.Products.DeactivateAsync(product.ID, contextProfileName).ConfigureAwait(false);
                    Responses[fileName].InfoMessages.Add($"Deactivated product '{product.Name}'");
                    Debug.WriteLine(Responses[fileName].InfoMessages.Last());
                    return;
                }
                case Enums.ImportAction.Delete:
                {
                    await Workflows.Products.DeleteAsync(product.ID, contextProfileName).ConfigureAwait(false);
                    Responses[fileName].InfoMessages.Add($"Deleted product '{product.Name}'");
                    Debug.WriteLine(Responses[fileName].InfoMessages.Last());
                    return;
                }
                // ReSharper disable once RedundantCaseLabel
                case Enums.ImportAction.CreateOrUpdate:
                default:
                {
                    var productIsDirty = false;
                    // Product Attribute Values
                    productIsDirty |= await AssociateProductAttributeValuesAsync(fileName, product, row, contextProfileName).ConfigureAwait(false);
                    var doGetRelatedInfoAction = new Func<Enums.ProductImportFieldEnum, Enums.ProductImportFieldEnum, Task>(
                        async (relatedProductsEnum, relatedProductsTypeEnum) =>
                    {
                        // Related Products. Need to search a list of products and associate them. Note: We'll have to do the search after all products are imported first
                        var relatedCell = await row.EntryByMappingFieldAsync(fileName, relatedProductsEnum).ConfigureAwait(false);
                        if (relatedCell == null)
                        {
                            return;
                        }
                        var relatedTypeName = "Related Product";
                        var relatedTypeNameCell = await row.EntryByMappingFieldAsync(fileName, relatedProductsTypeEnum).ConfigureAwait(false);
                        if (relatedTypeNameCell != null)
                        {
                            relatedTypeName = relatedTypeNameCell.Value;
                        }
                        if (!relationships.ContainsKey(product))
                        {
                            relationships.Add(product, new());
                        }
                        if (!relationships[product].ContainsKey(relatedTypeName!))
                        {
                            relationships[product].Add(relatedTypeName!, new());
                        }
                        relationships[product][relatedTypeName!].Add(relatedCell.Value!);
                    });
                    await doGetRelatedInfoAction(Enums.ProductImportFieldEnum.RelatedProducts, Enums.ProductImportFieldEnum.RelatedProductsType).ConfigureAwait(false);
                    await doGetRelatedInfoAction(Enums.ProductImportFieldEnum.RelatedProducts2, Enums.ProductImportFieldEnum.RelatedProductsType2).ConfigureAwait(false);
                    await doGetRelatedInfoAction(Enums.ProductImportFieldEnum.RelatedProducts3, Enums.ProductImportFieldEnum.RelatedProductsType3).ConfigureAwait(false);
                    await doGetRelatedInfoAction(Enums.ProductImportFieldEnum.RelatedProducts4, Enums.ProductImportFieldEnum.RelatedProductsType4).ConfigureAwait(false);
                    // All other fields
                    foreach (var field in EnumExtensions.AsValues<Enums.ProductImportFieldEnum>())
                    {
                        productIsDirty |= await AssignCellDataByFieldEnumAsync(fileName, field, row, product, contextProfileName).ConfigureAwait(false);
                    }
                    // Dirty Check
                    if (!productIsDirty)
                    {
                        return;
                    }
                    // Update it (Create would have happened initially with minimal data if the product didn't exist before)
                    await Workflows.Products.UpdateAsync(product, contextProfileName).ConfigureAwait(false);
                    if (!Responses[fileName].InfoMessages.Contains($"Created Product <i>'{product.Name}'</i> (ID: {product.ID})'"))
                    {
                        Responses[fileName].InfoMessages.Add($"Updated Product <i>'{product.Name}'</i> (ID: {product.ID})");
                    }
                    Debug.WriteLine(Responses[fileName].InfoMessages.Last());
                    break;
                }
            }
        }

        /// <summary>Associate product attribute values.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="product">           The product.</param>
        /// <param name="row">               The row.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{bool}.</returns>
        private async Task<bool> AssociateProductAttributeValuesAsync(
            string fileName,
            IHaveJsonAttributesBaseModel product,
            IRow row,
            string? contextProfileName)
        {
            var productIsDirty = false;
            if (attributeTypeIDForProduct <= 0)
            {
                attributeTypeIDForProduct = await EnsureProductAttributeTypeAsync(fileName, contextProfileName).ConfigureAwait(false);
            }
            if (GeneralAttributeCache.Count == 0)
            {
                var existingAttributes = (await Workflows.GeneralAttributes.SearchAsync(new GeneralAttributeSearchModel(), true, contextProfileName).ConfigureAwait(false)).results;
                foreach (var attr in existingAttributes)
                {
                    GeneralAttributeCache[attr.Name!] = attr;
                }
            }
            ////var existingList = product.Attributes;
            var existingProductAttributes = product.SerializableAttributes ?? new SerializableAttributesDictionary();
            foreach (var columnName in ImportMappings[fileName].Keys.Where(k => ImportMappings[fileName][k] == Enums.ProductImportFieldEnum.Attribute))
            {
                if (!GeneralAttributeCache.ContainsKey(columnName))
                {
                    var createResponse = await Workflows.GeneralAttributes.CreateAsync(
                            new GeneralAttributeModel
                            {
                                Active = true,
                                CreatedDate = DateExtensions.GenDateTime,
                                Name = columnName,
                                TypeName = "Product",
                            },
                            contextProfileName)
                        .ConfigureAwait(false);
                    GeneralAttributeCache[columnName] = (await Workflows.GeneralAttributes.GetAsync(
                            createResponse.Result,
                            contextProfileName)
                        .ConfigureAwait(false))!;
                }
                var attribute = GeneralAttributeCache[columnName];
                if (!Contract.CheckValidID(attribute.ID))
                {
                    Responses[fileName].ErrorMessages.Add($"Attribute {columnName} exists in the database but has no attributeID");
                    Debug.WriteLine(Responses[fileName].ErrorMessages.Last());
                    continue;
                }
                var entry = await row.EntryByColumnNameAsync(fileName, columnName).ConfigureAwait(false);
                if (string.IsNullOrWhiteSpace(entry?.Value))
                {
                    continue;
                }
                // These are the old style Attributes
                ////var productAttribute = existingList.FirstOrDefault(a => a.Active && a.Name == columnName);
                ////if (productAttribute == null)
                ////{
                ////    productAttribute = new AttributeModel
                ////    {
                ////        Active = true,
                ////        Value = entry.Value.Trim(),
                ////        AttributeID = attribute.ID.Value,
                ////        TypeID = attributeTypeIDForProduct
                ////    };
                ////    existingList.Add(productAttribute);
                ////    productIsDirty = true;
                ////}
                ////else if (!productAttribute.Value.Equals(entry.Value))
                ////{
                ////    productIsDirty = true;
                ////    productAttribute.Value = entry.Value;
                ////}
                // These are the JsonSerializable Attributes
                bool applyNewData;
                if (!existingProductAttributes.ContainsKey(attribute.Name!))
                {
                    applyNewData = true;
                }
                else
                {
                    var existing = existingProductAttributes[attribute.Name!];
                    applyNewData = !(existing.ID == attribute.ID
                                     && existing.Key == attribute.Name
                                     && existing.Value == entry!.Value
                                     && existing.UofM == entry.UofM);
                }
                // ReSharper disable once InvertIf
                if (applyNewData)
                {
                    existingProductAttributes[attribute.Name!] = new()
                    {
                        ID = attribute.ID,
                        Key = attribute.Name!,
                        Value = entry!.Value!,
                        UofM = entry.UofM!,
                    };
                    productIsDirty = true;
                }
            }
            ////product.Attributes = existingList;
            product.SerializableAttributes = existingProductAttributes;
            return productIsDirty;
        }

        /// <summary>Loop images.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="row">               The row.</param>
        /// <param name="product">           The product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{bool}.</returns>
        // ReSharper disable once FunctionComplexityOverflow
        private async Task<bool> LoopImagesAsync(
            string fileName,
            IRow row,
            IProductModel product,
            string? contextProfileName)
        {
            var productIsDirty = false;
            using (var webClient = new WebClient())
            {
                // Adding a User Agent value tends to fix 403 unauthorized errors
                webClient.Headers[HttpRequestHeader.UserAgent] = "User-Agent: Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";
                var imageIndex = 1;
                var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName);
                if (provider is null)
                {
                    return false;
                }
                var path = await provider.GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType.ImageProduct).ConfigureAwait(false);
                foreach (var imageUrl in (await row.EntriesByMappingFieldAsync(fileName, Enums.ProductImportFieldEnum.Images).ConfigureAwait(false))
                    .Where(imageUrls => imageUrls?.Value != null)
                    .SelectMany(
                        imageUrls => imageUrls!.Value!
                            .Split(new[] { '\n', ',', '|', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => x.Trim()),
                        (imageUrls, imageUrl) => new { imageUrls, imageUrl })
                    .Where(t => !string.Equals(t.imageUrl, "NONE", StringComparison.OrdinalIgnoreCase))
                    .Select(t => t.imageUrl))
                {
                    if (!imageUrl.StartsWith("http://") && !imageUrl.StartsWith("https://") && !imageUrl.StartsWith("ftp://"))
                    {
                        // It isn't actually a URL to download the image, instead it's just the path to the image.
                        try
                        {
                            var filename2 = imageUrl;
                            filename2 = filename2.TrimStart('.');
                            var existingListNew = product.Images!;
                            var productImageNew = existingListNew.FirstOrDefault(f => f.Active && f.OriginalFileName == filename2);
                            if (productImageNew == null)
                            {
                                existingListNew!.Add(new ProductImageModel
                                {
                                    Active = true,
                                    CreatedDate = DateExtensions.GenDateTime,
                                    Name = filename2,
                                    OriginalFileName = filename2,
                                    IsPrimary = !product.Images!.Any(x => x.Active && x.IsPrimary),
                                    TypeKey = "General",
                                });
                                product.Images = existingListNew;
                                productIsDirty = true;
                            }
                        }
                        catch (Exception e)
                        {
                            var msg = $"Unable to set '{imageUrl}' to product - '{e.Message}'";
                            await Logger.LogErrorAsync("ProductImport.Error", msg, e, contextProfileName).ConfigureAwait(false);
                            Responses[fileName].ErrorMessages.Add(msg);
                        }
                        imageIndex++;
                        continue;
                    } // end of non-url check
                    var filename = $"Product_{product.ID:00000}{(!string.IsNullOrWhiteSpace(product.CustomKey) ? "_" + product.CustomKey : string.Empty)}_{imageIndex:00}{Path.GetExtension(imageUrl)}".Trim();
                    var destination = (path.TrimEnd('\\') + @"\" + filename).Trim();
                    try
                    {
                        var file = new FileInfo(destination);
                        if (!file.Exists)
                        {
                            await webClient.DownloadFileTaskAsync(new Uri(imageUrl), destination).ConfigureAwait(false);
                            Responses[fileName].InfoMessages.Add($"Downloading '{imageUrl}' to '{destination}'");
                            Debug.WriteLine(Responses[fileName].InfoMessages.Last());
                        }
                        var existingListNew = product.Images!;
                        var productImageNew = existingListNew.FirstOrDefault(f => f.Active && f.OriginalFileName == filename);
                        if (productImageNew == null)
                        {
                            existingListNew!.Add(new ProductImageModel
                            {
                                Active = true,
                                CreatedDate = DateExtensions.GenDateTime,
                                Name = filename,
                                OriginalFileName = filename,
                                IsPrimary = !product.Images!.Any(x => x.Active && x.IsPrimary),
                                TypeKey = "General",
                            });
                            product.Images = existingListNew;
                            productIsDirty = true;
                        }
                        imageIndex++;
                        continue;
                    }
                    catch (Exception e)
                    {
                        var msg = $"Unable to copy '{imageUrl}' to '{destination}' - '{e.Message}'";
                        await Logger.LogErrorAsync("ProductImport.Error", msg, e, contextProfileName).ConfigureAwait(false);
                        Responses[fileName].ErrorMessages.Add(msg);
                    }
                    imageIndex++;
                }
            }
            return productIsDirty;
        }

        /// <summary>Ensures that general category type.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{int}.</returns>
        private async Task<int> EnsureGeneralCategoryTypeAsync(string fileName, string? contextProfileName)
        {
            var productCategoryType = await Workflows.CategoryTypes.GetByNameAsync("General", contextProfileName).ConfigureAwait(false);
            int productCategoryTypeId;
            if (productCategoryType?.ID == null)
            {
                productCategoryTypeId = (await Workflows.CategoryTypes.CreateAsync(
                            new TypeModel { Name = "General" },
                            contextProfileName)
                        .ConfigureAwait(false))
                    .Result;
                Responses[fileName].InfoMessages.Add($"Created CategoryType '<i>General<i>' (ID: {productCategoryTypeId})");
            }
            else
            {
                productCategoryTypeId = productCategoryType.ID;
            }
            return productCategoryTypeId;
        }

        /// <summary>Import product categories.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{Dictionary{int,Dictionary{string,int}}}.</returns>
        private async Task<Dictionary<int, Dictionary<string, int>>> ImportProductCategoriesAsync(
            string fileName,
            string? contextProfileName)
        {
            Debug.WriteLine("Importing product categories");
            var parentCategoryHash = new Dictionary<int, Dictionary<string, int>>();
            var productCategoryTypeId = await EnsureGeneralCategoryTypeAsync(fileName, contextProfileName).ConfigureAwait(false);
            var productCategoryColumns = ImportMappings[fileName].Keys.Where(k => ImportMappings[fileName][k] == Enums.ProductImportFieldEnum.Categories);
            // Import the category hierarchy from the sheet
            foreach (var columnName in productCategoryColumns)
            {
                foreach (var cell in EntryByColumnName(fileName, columnName))
                {
                    // A value in this cell is assumed to be a comma delimited list
                    // Example:  Clothing, Shoes, Men
                    foreach (var catSet in cell.Value!.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(cat => cat.Trim()))
                    {
                        int? parentCategoryID = null;
                        foreach (var categoryName in catSet.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(cat => cat.Trim()))
                        {
                            var parentCategoryKey = parentCategoryID ?? -1;
                            if (!parentCategoryHash.ContainsKey(parentCategoryKey))
                            {
                                parentCategoryHash.Add(parentCategoryKey, new());
                            }
                            var categoryHash = parentCategoryHash[parentCategoryKey];
                            // Hash miss?
                            if (!categoryHash.ContainsKey(categoryName))
                            {
                                // Lookup the category in the database
                                var existingCategory = await Workflows.Categories.GetByNameAsync(categoryName, contextProfileName).ConfigureAwait(false);
                                // Found it!
                                if (existingCategory?.ID != null)
                                {
                                    categoryHash[categoryName] = existingCategory.ID;
                                }
                                else
                                {
                                    if (AllowCreateCategories)
                                    {
                                        // Create the product category
                                        var createResponse = await Workflows.Categories.CreateAsync(
                                                new CategoryModel
                                                {
                                                    TypeID = productCategoryTypeId,
                                                    ParentID = parentCategoryID,
                                                    Name = categoryName,
                                                    SeoUrl = categoryName.ToSeoUrl(),
                                                    IsVisible = true,
                                                    IncludeInMenu = true,
                                                },
                                                contextProfileName)
                                            .ConfigureAwait(false);
                                        categoryHash[categoryName] = createResponse.Result;
                                        Responses[fileName].InfoMessages.Add($"Created Category '<i>{categoryName}</i>' (ID: {categoryHash[categoryName]})");
                                        Debug.WriteLine(Responses[fileName].InfoMessages.Last());
                                    }
                                    else
                                    {
                                        Responses[fileName].ErrorMessages.Add($"Unable to use category '<i>{categoryName}</i>'. Store imports must utilize existing categories only");
                                        continue;
                                    }
                                }
                            }
                            parentCategoryID = categoryHash[categoryName];
                        }
                    }
                }
            }
            return parentCategoryHash;
        }

        /// <summary>Ensures that product attribute type.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{int}.</returns>
        private async Task<int> EnsureProductAttributeTypeAsync(string fileName, string? contextProfileName)
        {
            var type = await Workflows.AttributeTypes.GetByNameAsync("Product", contextProfileName).ConfigureAwait(false);
            int productAttributeTypeId;
            if (type == null)
            {
                productAttributeTypeId = (await Workflows.AttributeTypes.CreateAsync(
                            new AttributeTypeModel { Name = "Product" },
                            contextProfileName)
                        .ConfigureAwait(false))
                    .Result;
                Responses[fileName].InfoMessages.Add($"Created Attribute Type '<i>Product</i>' (ID: {productAttributeTypeId})");
            }
            else
            {
                productAttributeTypeId = type.ID;
            }
            return productAttributeTypeId;
        }
    }
}
