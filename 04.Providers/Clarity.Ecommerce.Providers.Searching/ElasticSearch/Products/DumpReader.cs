// <copyright file="DumpReader.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the dump reader class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Searching;
    using Nest;
    using Utilities;

    /// <summary>A product dump reader.</summary>
    internal class ProductDumpReader : DumpReaderBase<ProductIndexableModel>
    {
        /// <inheritdoc/>
        public override IEnumerable<ProductIndexableModel> GetRecords(string? contextProfileName, CancellationToken ct)
        {
            Log("Entered", contextProfileName).Wait(10_000, ct);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var filterableAttributes = LoadFilterableAttributes(context, true);
            // var queryableAttributes = LoadQueryableAttributes(context);
            var categories = LoadInitialCategoryData(context);
            var types = LoadInitialTypeData<ProductType>(context);
            // Get List of type keys we want to filter by
            var keys = ElasticSearchingProviderConfig.SearchingProductIndexFilterByTypeKeys;
            var keyList = Array.Empty<string>();
            if (Contract.CheckNotEmpty(keys))
            {
                keyList = keys!.Select(x => x.Trim()).ToArray();
            }
            // Get list of attrubutes that can be queried using search term
            var queryableAttributeKeyList = Array.Empty<string>();
            if (Contract.CheckValidKey(ElasticSearchingProviderConfig.SearchingProductIndexQueryByAttributeKeys))
            {
                queryableAttributeKeyList = ElasticSearchingProviderConfig.SearchingProductIndexQueryByAttributeKeys
                    !.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
            }
            foreach (var version in context.Products
                .FilterByActive(true)
                .FilterProductsByIsVisible(true)
                .FilterProductsByIsDiscontinued(false)
                .FilterByTypeKeys<Product, ProductType>(keyList)
                .OrderBy(x => x.ID)
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
                    // IHaveATypeBase Properties
                    x.TypeID,
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
                    x.StockQuantity,
                    // Associated Objects
                    HasBrands = x.Brands!.Any(y => y.Active && y.IsVisibleIn && y.Master!.Active),
                    Brands = x.Brands!
                        .Where(y => y.Active && y.IsVisibleIn && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    HasCategories = x.Categories!
                        .Any(y => y.Active && y.Slave!.Active && y.Slave.IsVisible && y.Slave.IncludeInMenu),
                    CategoriesRead = x.Categories!
                        .Where(y => y.Active && y.Slave!.Active && y.Slave.IsVisible && y.Slave.IncludeInMenu)
                        .Select(y => new
                        {
                            y.Slave!.ID,
                            Name = y.Slave.Name + Pipe + y.Slave.CustomKey,
                            y.Slave.DisplayName,
                            y.Slave.ParentID,
                        }),
                    HasFranchises = x.Franchises!.Any(y => y.Active && y.IsVisibleIn && y.Master!.Active),
                    Franchises = x.Franchises!
                        .Where(y => y.Active && y.IsVisibleIn && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    HasManufacturers = x.Manufacturers!.Any(y => y.Active && y.Master!.Active),
                    Manufacturers = x.Manufacturers!
                        .Where(y => y.Active && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    /*
                    HasProducts = x.Products!.Any(y => y.Active && y.Master!.Active),
                    Products = x.Products!
                        .Where(y => y.Active && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    */
                    RatingToIndexAs = x.Reviews!.Where(y => y.Active && y.Approved).Select(y => y.Value).DefaultIfEmpty(0).Average(),
                    HasStores = x.Stores!.Any(y => y.Active && y.IsVisibleIn && y.Master!.Active),
                    Stores = x.Stores!
                        .Where(y => y.Active
                            && y.IsVisibleIn
                            && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    HasVendors = x.Vendors!.Any(y => y.Active && y.Master!.Active),
                    Vendors = x.Vendors!
                        .Where(y => y.Active && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    Variants = x.ProductAssociations!
                        .Where(y => y.Active
                            && y.Slave!.Active
                            && y.Type!.CustomKey == "VARIANT-OF-MASTER"
                            && y.MasterID == x.ID)
                        .Select(y => new
                        {
                            ID = y.SlaveID,
                            Attributes = y.Slave!.JsonAttributes,
                        }),
                    PriceLists = x.ProductPricePoints
                        .Where(y => y.Active && y.Slave!.Active)
                        .Select(y => y.Slave!.Name!),
                })
                .ToList()
                .Select(x => new // ProductIndexableModel
                {
                    // Base Properties
                    x.ID,
                    CustomKey = CollapseWhitespace(x.CustomKey, '\"'),
                    Active = true,
                    UpdatedDate = x.UpdatedDate ?? x.CreatedDate,
                    FilterableJsonAttributes = x.JsonAttributes,
                    QueryableJsonAttributes = x.JsonAttributes,
                    // NameableBase Properties
                    Name = CollapseWhitespace(x.Name, '\"'),
                    x.Description,
                    // IHaveSeoBase Properties
                    x.SeoUrl,
                    x.SeoKeywords,
                    x.SeoDescription,
                    // IHaveATypeBase Properties
                    x.TypeID,
                    // Product Properties
                    TotalPurchasedQuantity = x.TotalPurchasedQuantity ?? 0m,
                    FinalPrice = x.PriceSale ?? x.PriceBase ?? 0m,
                    ShortDescription = CollapseWhitespace(x.ShortDescription, '/', '^', '\"'),
                    x.BrandName,
                    x.ManufacturerPartNumber,
                    SortOrder = x.SortOrder ?? 0,
                    x.RequiresRoles,
                    x.RequiresRolesAlt,
                    x.StockQuantity,
                    // Associated Objects
                    x.HasBrands,
                    Brands = x.Brands
                        .Select(y => new IndexableBrandFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    x.HasCategories,
                    CategoriesRead = x.CategoriesRead
                        .Select(y => new IndexableCategoryFilterRead
                        {
                            ID = y.ID,
                            Name = y.Name,
                            DisplayName = y.DisplayName,
                            ParentID = y.ParentID,
                        }),
                    x.HasFranchises,
                    Franchises = x.Franchises
                        .Select(y => new IndexableFranchiseFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    x.HasManufacturers,
                    Manufacturers = x.Manufacturers
                        .Select(y => new IndexableManufacturerFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    /*
                    x.HasProducts,
                    Products = x.Products
                        .Select(y => new IndexableProductFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    */
                    x.RatingToIndexAs,
                    x.HasStores,
                    Stores = x.Stores
                        .Select(y => new IndexableStoreFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    x.HasVendors,
                    Vendors = x.Vendors
                        .Select(y => new IndexableVendorFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    VariantAttributes = x.Variants
                        .Select(y => new IndexableVariantAttributesFilter
                        {
                            ID = y.ID,
                            JsonAttributes = y.Attributes,
                        }),
                    PriceLists = x.PriceLists.Distinct(),
                }))
            {
                if (ElasticSearchingProviderConfig.SearchingProductIndexRequiresABrand && !version.HasBrands
                    || ElasticSearchingProviderConfig.SearchingProductIndexRequiresACategory && !version.HasCategories
                    || ElasticSearchingProviderConfig.SearchingProductIndexRequiresAFranchise && !version.HasFranchises
                    || ElasticSearchingProviderConfig.SearchingProductIndexRequiresAManufacturer && !version.HasManufacturers
                    // || ElasticSearchingProviderConfig.SearchingProductIndexRequiresAProduct && !version.HasProducts
                    || ElasticSearchingProviderConfig.SearchingProductIndexRequiresAStore && !version.HasStores
                    || ElasticSearchingProviderConfig.SearchingProductIndexRequiresAVendor && !version.HasVendors)
                {
                    continue;
                }
                ProductIndexableModel indexableModel = new()
                {
                    // Base Properties
                    ID = version.ID,
                    CustomKey = version.CustomKey,
                    Active = true,
                    UpdatedDate = version.UpdatedDate,
                    // NameableBase Properties
                    Name = version.Name,
                    Description = version.Description,
                    // IHaveSeoBase Properties
                    SeoUrl = version.SeoUrl,
                    SeoKeywords = version.SeoKeywords,
                    SeoDescription = version.SeoDescription,
                    // IHaveATypeBase Properties
                    TypeID = version.TypeID,
                    TypeName = types.ContainsKey(version.TypeID)
                        ? types[version.TypeID].Name
                        : "N/A",
                    TypeSortOrder = types.ContainsKey(version.TypeID)
                        ? types[version.TypeID].SortOrder
                        : 0,
                    // Product Properties
                    TotalPurchasedQuantity = version.TotalPurchasedQuantity,
                    PricingToIndexAs = version.FinalPrice,
                    ShortDescription = version.ShortDescription,
                    BrandName = version.BrandName,
                    BrandNameAgg = version.BrandName,
                    ManufacturerPartNumber = version.ManufacturerPartNumber,
                    SortOrder = version.SortOrder,
                    RequiresRoles = version.RequiresRoles,
                    RequiresRolesAlt = version.RequiresRolesAlt,
                    // Associated Objects
                    HasBrands = version.HasBrands,
                    Brands = version.Brands,
                    HasCategories = version.HasCategories, // CategoriesRead handled below
                    HasFranchises = version.HasFranchises,
                    Franchises = version.Franchises,
                    HasManufacturers = version.HasManufacturers,
                    Manufacturers = version.Manufacturers,
                    /*
                    HasProducts = version.HasProducts,
                    Products = version.Products,
                    */
                    RatingToIndexAs = version.RatingToIndexAs,
                    HasStores = version.HasStores,
                    Stores = version.Stores,
                    HasVendors = version.HasVendors,
                    Vendors = version.Vendors,
                    StockQuantity = version.StockQuantity,
                };
                HandleFilterableAttributes(
                    version.FilterableJsonAttributes,
                    filterableAttributes,
                    indexableModel,
                    true);
                if (indexableModel.FilterableAttributes?.Any(x => x.Key == AttributeNameForSKURestrictions) == true)
                {
                    var skuRestrictionString = indexableModel.FilterableAttributes.First(x => x.Key == AttributeNameForSKURestrictions);
                    if (skuRestrictionString.Value?.Contains(AttributeValueForSKURestrictions) == true)
                    {
                        continue;
                    }
                }
                HandleVariantAttributes(version.VariantAttributes.ToList(), filterableAttributes, indexableModel);
                HandleCategoryData(categories, version.CategoriesRead.ToList(), indexableModel);
                if (ElasticSearchingProviderConfig.SearchingProductIndexFiltersIncludeRoles)
                {
                    HandleRoles(version.RequiresRoles, indexableModel);
                }
                HandleStandardSuggests(indexableModel, indexableModel.TotalPurchasedQuantity ?? 1m);
                HandleCustomSuggests(indexableModel);
                HandleQueryableAttributes(version.QueryableJsonAttributes, indexableModel, queryableAttributeKeyList);
                yield return indexableModel;
            }
            Log("Exited", contextProfileName).Wait(10_000, ct);
        }

        /// <summary>Handles the roles.</summary>
        /// <param name="requiresRoles"> The requires roles.</param>
        /// <param name="indexableModel">The indexable model.</param>
        protected override void HandleRoles(string? requiresRoles, ProductIndexableModel indexableModel)
        {
            List<IndexableRoleFilter> indexableRoles = new();
            if (!string.IsNullOrWhiteSpace(requiresRoles))
            {
                indexableRoles.AddRange(requiresRoles
                    ?.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(str => new IndexableRoleFilter { RoleName = str })
                    .ToList());
            }
            indexableRoles.Add(new IndexableRoleFilter { RoleName = RoleForAnonymous });
            indexableModel.RequiresRolesList = indexableRoles;
        }

        /// <summary>Handles the variant attributes.</summary>
        /// <param name="variantAttributes">   The variant attributes.</param>
        /// <param name="filterableAttributes">The filterable attributes.</param>
        /// <param name="indexableModel">      The indexable model.</param>
        protected virtual void HandleVariantAttributes(
            List<IndexableVariantAttributesFilter> variantAttributes,
            Dictionary<string, AttrModel> filterableAttributes,
            ProductIndexableModel indexableModel)
        {
            if (Contract.CheckEmpty(variantAttributes))
            {
                return;
            }
            foreach (var variant in variantAttributes)
            {
                var variantAttributes2 = variant.JsonAttributes.DeserializeAttributesDictionary()
                    .Where(x => Contract.CheckValidKey(x.Key)
                        && x.Key != Undefined
                        && Contract.CheckValidKey(x.Value.Value)
                        && (filterableAttributes.ContainsKey(x.Key) || x.Key == AttributeNameForSKURestrictions))
                    .Select(x => new IndexableAttributeObjectFilter
                    {
                        ID = x.Value.ID > 0 ? x.Value.ID : filterableAttributes[x.Key].ID,
                        Key = x.Value.Key ?? x.Key,
                        SortOrder = x.Value.SortOrder ?? filterableAttributes[x.Key].SortOrder,
                        Value = x.Value.Value?.ToString(),
                        UofM = x.Value.UofM,
                    })
                    .ToList();
                if (!Contract.CheckNotEmpty(variantAttributes2))
                {
                    continue;
                }
                indexableModel.FilterableAttributes = variantAttributes2
                    .Union(indexableModel.FilterableAttributes ?? new List<IndexableAttributeObjectFilter>());
            }
        }

        /// <summary>Handles the custom suggests described by indexableModel.</summary>
        /// <param name="indexableModel">The indexable model.</param>
        protected virtual void HandleCustomSuggests(
            ProductIndexableModel indexableModel)
        {
            if (Contract.CheckValidKey(indexableModel.BrandName))
            {
                indexableModel.SuggestedByBrandName = new CompletionField
                {
                    Input = new List<string>(indexableModel.BrandName!.Trim().Split(' ')) { indexableModel.BrandName.Trim() },
                    Weight = Math.Max(1, (int)((long)((indexableModel.TotalPurchasedQuantity ?? 1m) * 1m * CoreWeightMultiplier) / 10000)),
                };
            }
            if (Contract.CheckValidKey(indexableModel.ManufacturerPartNumber))
            {
                indexableModel.SuggestedByManufacturerPartNumber = new CompletionField
                {
                    Input = new List<string>(indexableModel.ManufacturerPartNumber!.Trim().Split(' ')) { indexableModel.ManufacturerPartNumber.Trim() },
                    Weight = Math.Max(1, (int)((long)((indexableModel.TotalPurchasedQuantity ?? 1m) * 1m * CoreWeightMultiplier) / 10000)),
                };
            }
            if (Contract.CheckValidKey(indexableModel.ShortDescription))
            {
                indexableModel.SuggestedByShortDescription = new CompletionField
                {
                    Input = new List<string>(indexableModel.ShortDescription!.Trim().Split(' ')) { indexableModel.ShortDescription.Trim() },
                    Weight = Math.Max(1, (int)((long)((indexableModel.TotalPurchasedQuantity ?? 1m) * 0.25m * CoreWeightMultiplier) / 10000)),
                };
            }
        }
    }
}
