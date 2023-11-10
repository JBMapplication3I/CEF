// <copyright file="ProductDumpReader.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product dump reader class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Domain.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Searching;
    using MoreLinq.Extensions;
    using Nest;
    using Utilities;

    /// <summary>A product dump reader.</summary>
    public class ProductDumpReader
    {
        private const decimal CoreWeightMultiplier = 100m;

        private static ILogger Logger { get; } = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Gets the products in this collection.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process the products in this collection.</returns>
        public IEnumerable<IndexableProductModel> GetProducts(string contextProfileName)
        {
            Logger.LogInformation($"{nameof(ProductDumpReader)}.${nameof(GetProducts)}", "Entered", contextProfileName);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var attributes = context.GeneralAttributes
                .FilterByActive(true)
                .Where(x => x.IsFilter || x.CustomKey == "SKU-Restrictions")
                .Where(x => !x.IsMarkup && !x.HideFromStorefront)
                .Select(x => new AttrModel
                {
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    Active = true,
                    Name = x.Name,
                    DisplayName = x.DisplayName,
                    SortOrder = x.SortOrder,
                    IsFilter = true,
                    TypeID = x.TypeID,
                })
                .ToDictionary(x => x.CustomKey, x => x);
            var categories = context.Categories
                .Where(x => x.Active && x.IsVisible && x.IncludeInMenu)
                .Select(x => new
                {
                    x.ID,
                    x.CustomKey,
                    x.Name,
                    x.DisplayName,
                    x.ParentID,
                })
                .ToDictionary(x => x.ID, x => x);
            List<IndexableProductCategory> AssignCategoryData(
                IEnumerable<IndexableProductCategoryRead> categoriesRead)
            {
                List<IndexableProductCategory> indexes = new();
                foreach (var read in categoriesRead)
                {
                    var toAdd = new IndexableProductCategory
                    {
                        CategoryName = read.Name,
                    };
                    if (!Contract.CheckValidID(read.ParentID))
                    {
                        indexes.Add(toAdd);
                        continue;
                    }
                    if (!categories.ContainsKey(read.ParentID!.Value))
                    {
                        // Category isn't visible
                        indexes.Add(toAdd);
                        continue;
                    }
                    var parents = new[]
                    {
                        categories[read.ParentID.Value],
                        null,
                        null,
                        null,
                        null,
                        null,
                    };
                    // ReSharper disable PossibleInvalidOperationException
                    var i = 0;
                    toAdd.CategoryParent1Name = parents[i]!.Name + "|" + parents[i]!.CustomKey;
                    if (!Contract.CheckValidID(parents[i]!.ParentID))
                    {
                        indexes.Add(toAdd);
                        continue;
                    }
                    parents[++i] = categories[parents[i - 1]!.ParentID!.Value];
                    toAdd.CategoryParent2Name = parents[i]!.Name + "|" + parents[i]!.CustomKey;
                    if (!Contract.CheckValidID(parents[i]!.ParentID))
                    {
                        indexes.Add(toAdd);
                        continue;
                    }
                    parents[++i] = categories[parents[i - 1]!.ParentID!.Value];
                    toAdd.CategoryParent3Name = parents[i]!.Name + "|" + parents[i]!.CustomKey;
                    if (!Contract.CheckValidID(parents[i]!.ParentID))
                    {
                        indexes.Add(toAdd);
                        continue;
                    }
                    parents[++i] = categories[parents[i - 1]!.ParentID!.Value];
                    toAdd.CategoryParent4Name = parents[i]!.Name + "|" + parents[i]!.CustomKey;
                    if (!Contract.CheckValidID(parents[i]!.ParentID))
                    {
                        indexes.Add(toAdd);
                        continue;
                    }
                    parents[++i] = categories[parents[i - 1]!.ParentID!.Value];
                    toAdd.CategoryParent5Name = parents[i]!.Name + "|" + parents[i]!.CustomKey;
                    if (!Contract.CheckValidID(parents[i]!.ParentID))
                    {
                        indexes.Add(toAdd);
                        continue;
                    }
                    parents[++i] = categories[parents[i - 1]!.ParentID!.Value];
                    toAdd.CategoryParent6Name = parents[i]!.Name + "|" + parents[i]!.CustomKey;
                    if (!Contract.CheckValidID(parents[i]!.ParentID))
                    {
                        indexes.Add(toAdd);
                        continue;
                    }
                    indexes.Add(toAdd);
                    // ReSharper restore PossibleInvalidOperationException
                }
                return indexes;
            }
            // Get List of type keys we want to filter by
            var keys = ElasticSearchingProviderConfig.SearchingProductIndexFilterByTypeKeys;
            var keyList = Array.Empty<string>();
            if (Contract.CheckNotEmpty(keys))
            {
                keyList = keys!.Select(x => x.Trim()).ToArray();
            }
            // TODO: Figure out how to apply this with the multiple variants
            foreach (var productVersion in context.Products
                .FilterByActive(true)
                .FilterProductsByIsVisible(true)
                .FilterProductsByIsDiscontinued(false)
                .FilterByTypeKeys<Product, ProductType>(keyList)
                .OrderBy(x => x.ID)
                ////.Take(200_000)
                .Select(x => new
                {
                    // Base Properties
                    x.ID,
                    x.CustomKey,
                    x.CreatedDate,
                    x.UpdatedDate,
                    x.JsonAttributes,
                    // NameableBase Properties
                    x.Name,
                    x.Description,
                    // IHaveSeoBase Properties
                    x.SeoUrl,
                    x.SeoKeywords,
                    x.SeoDescription,
                    ////x.SeoPageTitle,
                    // Product Properties
                    x.TotalPurchasedQuantity,
                    x.PriceBase,
                    x.PriceSale,
                    x.ShortDescription,
                    x.BrandName,
                    x.ManufacturerPartNumber,
                    x.SortOrder,
                    x.RequiresRoles,
                    x.RequiresRolesAlt,
                    // Associated Objects
                    HasBrands = x.Brands.Any(y => y.Active && y.IsVisibleInBrand && y.Master.Active),
                    ProductBrands = x.Brands
                        .Where(y => y.Active
                                 && y.IsVisibleInBrand
                                 && y.Master.Active)
                        .Select(y => new
                        {
                            BrandID = y.MasterID,
                            BrandKey = y.Master.CustomKey,
                            BrandName = y.Master.Name,
                        }),
                    HasStores = x.Stores.Any(y => y.Active && y.IsVisibleInStore && y.Master.Active),
                    ProductStores = x.Stores
                        .Where(y => y.Active
                                 && y.IsVisibleInStore
                                 && y.Master.Active)
                        .Select(y => new
                        {
                            StoreID = y.MasterID,
                            StoreKey = y.Master.CustomKey,
                            StoreName = y.Master.Name,
                        }),
                    ProductCategoriesRead = x.ProductCategories
                        .Where(y => y.Active
                                 && y.Slave.Active
                                 && y.Slave.IsVisible
                                 && y.Slave.IncludeInMenu)
                        .Select(y => new
                        {
                            y.Slave.ID,
                            Name = y.Slave.Name + "|" + y.Slave.CustomKey,
                            y.Slave.DisplayName,
                            y.Slave.ParentID,
                        }),
                })
                .ToList()
                .Select(x => new // IndexableProductModel
                {
                    // Base Properties
                    x.ID,
                    x.CustomKey,
                    Active = true,
                    UpdatedDate = x.UpdatedDate ?? x.CreatedDate,
                    x.JsonAttributes,
                    // NameableBase Properties
                    x.Name,
                    x.Description,
                    // IHaveSeoBase Properties
                    x.SeoUrl,
                    x.SeoKeywords,
                    x.SeoDescription,
                    ////x.SeoPageTitle,
                    // Product Properties
                    TotalPurchasedQuantity = x.TotalPurchasedQuantity ?? 0m,
                    FinalPrice = x.PriceSale ?? x.PriceBase ?? 0m,
                    ShortDescription = x.ShortDescription?.Replace("  ", " ").Replace("  ", " "),
                    x.BrandName,
                    BrandNameAgg = x.BrandName,
                    x.ManufacturerPartNumber,
                    SortOrder = x.SortOrder ?? 0,
                    x.RequiresRoles,
                    x.RequiresRolesAlt,
                    // Associated Objects
                    x.HasBrands,
                    ProductBrands = x.ProductBrands
                        .Select(y => new IndexableProductBrand
                        {
                            BrandID = y.BrandID,
                            BrandKey = y.BrandKey,
                            BrandName = y.BrandName,
                        }),
                    x.HasStores,
                    ProductStores = x.ProductStores
                        .Select(y => new IndexableProductStore
                        {
                            StoreID = y.StoreID,
                            StoreKey = y.StoreKey,
                            StoreName = y.StoreName,
                        }),
                    ProductCategoriesRead = x.ProductCategoriesRead
                        .Select(y => new IndexableProductCategoryRead
                        {
                            ID = y.ID,
                            Name = y.Name,
                            DisplayName = y.DisplayName,
                            ParentID = y.ParentID,
                        }),
                }))
            {
                if (ElasticSearchingProviderConfig.SearchingProductIndexRequiresACategory && !productVersion.ProductCategoriesRead.Any())
                {
                    continue;
                }
                if (ElasticSearchingProviderConfig.SearchingProductIndexRequiresAStore && !productVersion.HasStores)
                {
                    continue;
                }
                if (ElasticSearchingProviderConfig.SearchingProductIndexRequiresABrand && !productVersion.HasBrands)
                {
                    continue;
                }
                IndexableProductModel indexableModel = new()
                {
                    // Base Properties
                    ID = productVersion.ID,
                    CustomKey = productVersion.CustomKey,
                    Active = true,
                    UpdatedDate = productVersion.UpdatedDate,
                    // NameableBase Properties
                    Name = productVersion.Name,
                    Description = productVersion.Description,
                    // IHaveSeoBase Properties
                    SeoUrl = productVersion.SeoUrl,
                    SeoKeywords = productVersion.SeoKeywords,
                    SeoDescription = productVersion.SeoDescription,
                    ////SeoPageTitle = productVersion.SeoPageTitle,
                    // Product Properties
                    TotalPurchasedQuantity = productVersion.TotalPurchasedQuantity,
                    FinalPrice = productVersion.FinalPrice,
                    ShortDescription = productVersion.ShortDescription,
                    BrandName = productVersion.BrandName,
                    ManufacturerPartNumber = productVersion.ManufacturerPartNumber,
                    SortOrder = productVersion.SortOrder,
                    RequiresRoles = productVersion.RequiresRoles,
                    RequiresRolesAlt = productVersion.RequiresRolesAlt,
                    // Associated Objects
                    HasBrands = productVersion.HasBrands,
                    ProductBrands = productVersion.ProductBrands,
                    HasStores = productVersion.HasStores,
                    ProductStores = productVersion.ProductStores,
                    Attributes = !string.IsNullOrEmpty(productVersion.JsonAttributes)
                        ? productVersion.JsonAttributes.DeserializeAttributesDictionary()
                            .Where(x => !string.IsNullOrWhiteSpace(x.Key)
                                     && x.Key != "undefined"
                                     && x.Value != null
                                     && (attributes.ContainsKey(x.Key) || x.Key == "SKU-Restrictions"))
                            .Select(x => new IndexableSerializableAttributeObject
                            {
                                ID = x.Value.ID > 0 ? x.Value.ID : attributes[x.Key].ID,
                                Key = x.Value.Key ?? x.Key,
                                SortOrder = x.Value.SortOrder ?? attributes[x.Key].SortOrder,
                                Value = x.Value.Value?.ToString(),
                                UofM = x.Value.UofM,
                            })
                            .Where(x => Contract.CheckValidKey(x.Value))
                        : null,
                };
                // QuickLogger.Logger(contextProfileName, "Entered ProductDumpReader.GetProducts(...)", $"adding product ID {productVersion.ID} to the 'to index' list");
                if (indexableModel.Attributes?.Any(x => x.Key == "SKU-Restrictions") == true)
                {
                    var skuRestrictionString = indexableModel.Attributes.First(x => x.Key == "SKU-Restrictions");
                    if (skuRestrictionString.Value.Contains("RestrictShipFlag\":\"Y\""))
                    {
                        continue;
                    }
                }
                indexableModel.ProductCategories = AssignCategoryData(
                    productVersion.ProductCategoriesRead
                        .Select(y => new IndexableProductCategoryRead
                        {
                            ID = y.ID,
                            Name = y.Name,
                            DisplayName = y.DisplayName,
                            ParentID = y.ParentID,
                        })
                        .DistinctBy(x => x.ID));
                if (ElasticSearchingProviderConfig.SearchingProductIndexFiltersIncludeRoles)
                {
                    indexableModel.RequiresRolesList = productVersion.RequiresRoles
                        ?.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(str => new IndexableProductRole { RoleName = str })
                        .ToList()
                        ?? new List<IndexableProductRole> { new() { RoleName = "Anonymous" } };
                }
                indexableModel.SuggestedByName = new CompletionField
                {
                    Input = new List<string>(indexableModel.Name.Split(' ')) { indexableModel.Name },
                    Weight = Math.Max(1, (int)((long)((indexableModel.TotalPurchasedQuantity ?? 1m) * 2m * CoreWeightMultiplier) / 10000)),
                };
                indexableModel.SuggestedByCustomKey = new CompletionField
                {
                    Input = new List<string>(indexableModel.CustomKey.Split(' ')) { indexableModel.CustomKey },
                    Weight = Math.Max(1, (int)((long)((indexableModel.TotalPurchasedQuantity ?? 1m) * 2m * CoreWeightMultiplier) / 10000)),
                };
                if (!string.IsNullOrWhiteSpace(indexableModel.BrandName))
                {
                    indexableModel.SuggestedByBrandName = new CompletionField
                    {
                        Input = new List<string>(indexableModel.BrandName.Split(' ')) { indexableModel.BrandName },
                        Weight = Math.Max(1, (int)((long)((indexableModel.TotalPurchasedQuantity ?? 1m) * 1m * CoreWeightMultiplier) / 10000)),
                    };
                }
                if (!string.IsNullOrWhiteSpace(indexableModel.ManufacturerPartNumber))
                {
                    indexableModel.SuggestedByManufacturerPartNumber = new CompletionField
                    {
                        Input = new List<string>(indexableModel.ManufacturerPartNumber.Split(' ')) { indexableModel.ManufacturerPartNumber },
                        Weight = Math.Max(1, (int)((long)((indexableModel.TotalPurchasedQuantity ?? 1m) * 1m * CoreWeightMultiplier) / 10000)),
                    };
                }
                ////if (!string.IsNullOrWhiteSpace(indexableModel.ShortDescription))
                ////{
                ////    indexableModel.SuggestedByShortDescription = new CompletionField
                ////    {
                ////        Input = new List<string>(indexableModel.ShortDescription.Split(' ')) { indexableModel.ShortDescription },
                ////        Weight = (int)((long)((indexableModel.TotalPurchasedQuantity) * 0.25m * CoreWeightMultiplier) / 10000))
                ////    };
                ////}
                yield return indexableModel;
            }
            // QuickLogger.Logger(contextProfileName, "Exited ProductDumpReader.GetProducts(...)");
        }
    }
}
