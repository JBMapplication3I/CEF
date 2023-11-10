// <copyright file="DumpReader.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
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
    using Utilities;

    /// <summary>A category dump reader.</summary>
    internal class CategoryDumpReader : DumpReaderBase<CategoryIndexableModel>
    {
        /// <inheritdoc/>
        public override IEnumerable<CategoryIndexableModel> GetRecords(string? contextProfileName, CancellationToken ct)
        {
            Log("Entered", contextProfileName).Wait(10_000, ct);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var filterableAttributes = LoadFilterableAttributes(context);
            // var queryableAttributes = LoadQueryableAttributes(context);
            var categories = LoadInitialCategoryData(context);
            // Get List of type keys we want to filter by
            var keys = ElasticSearchingProviderConfig.SearchingCategoryIndexFilterByTypeKeys;
            var keyList = Array.Empty<string>();
            if (Contract.CheckNotEmpty(keys))
            {
                keyList = keys!.Select(x => x.Trim()).ToArray();
            }
            // Get list of attrubutes that can be queried using search term
            var queryableAttributeKeyList = Array.Empty<string>();
            if (Contract.CheckValidKey(ElasticSearchingProviderConfig.SearchingCategoryIndexQueryByAttributeKeys))
            {
                queryableAttributeKeyList = ElasticSearchingProviderConfig.SearchingCategoryIndexQueryByAttributeKeys
                    !.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
            }
            foreach (var version in context.Categories
                .FilterByActive(true)
                .FilterCategoriesByIsVisible(true)
                .FilterCategoriesByIncludeInMenu(true)
                .FilterByTypeKeys<Category, CategoryType>(keyList)
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
                    // Category Properties
                    x.SortOrder,
                    x.RequiresRoles,
                    x.RequiresRolesAlt,
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
                    HasCategories = x.ParentID != null,
                    CategoriesRead = x.ParentID != null
                        ? new
                        {
                            x.Parent!.ID,
                            Name = x.Parent.Name + Pipe + x.Parent.CustomKey,
                            x.Parent.DisplayName,
                            x.Parent.ParentID,
                        }
                        : null,
                    HasFranchises = x.Franchises!.Any(y => y.Active && y.IsVisibleIn && y.Master!.Active),
                    Franchises = x.Franchises!
                        .Where(y => y.Active && y.IsVisibleIn && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    /*
                    HasManufacturers = x.Manufacturers!.Any(y => y.Active && y.Master!.Active),
                    Manufacturers = x.Manufacturers!
                        .Where(y => y.Active && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    */
                    HasProducts = x.Products!.Any(y => y.Active && y.Master!.Active),
                    Products = x.Products!
                        .Where(y => y.Active && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
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
                    /*
                    HasVendors = x.Vendors!.Any(y => y.Active && y.Master!.Active),
                    Vendors = x.Vendors!
                        .Where(y => y.Active && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    */
                })
                .ToList()
                .Select(x => new // CategoryIndexableModel
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
                    // Category Properties
                    SortOrder = x.SortOrder ?? 0,
                    x.RequiresRoles,
                    x.RequiresRolesAlt,
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
                    CategoriesRead = x.HasCategories ?
                        new IndexableCategoryFilterRead
                        {
                            ID = x.CategoriesRead!.ID,
                            Name = x.CategoriesRead!.Name,
                            DisplayName = x.CategoriesRead!.DisplayName,
                            ParentID = x.CategoriesRead!.ParentID,
                        }
                        : null,
                    x.HasFranchises,
                    Franchises = x.Franchises
                        .Select(y => new IndexableFranchiseFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    /*
                    x.HasManufacturers,
                    Manufacturers = x.Manufacturers
                        .Select(y => new IndexableManufacturerFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    */
                    x.HasProducts,
                    Products = x.Products
                        .Select(y => new IndexableProductFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    x.RatingToIndexAs,
                    x.HasStores,
                    Stores = x.Stores
                        .Select(y => new IndexableStoreFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    /*
                    x.HasVendors,
                    Vendors = x.Vendors
                        .Select(y => new IndexableVendorFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    */
                }))
            {
                if (ElasticSearchingProviderConfig.SearchingCategoryIndexRequiresABrand && !version.HasBrands
                    /*|| ElasticSearchingProviderConfig.SearchingCategoryIndexRequiresACategory && !version.HasCategories */
                    || ElasticSearchingProviderConfig.SearchingCategoryIndexRequiresAFranchise && !version.HasFranchises
                    /* || ElasticSearchingProviderConfig.SearchingCategoryIndexRequiresAManufacturer && !version.HasManufacturers */
                    || ElasticSearchingProviderConfig.SearchingCategoryIndexRequiresAProduct && !version.HasProducts
                    || ElasticSearchingProviderConfig.SearchingCategoryIndexRequiresAStore && !version.HasStores
                    /*|| ElasticSearchingProviderConfig.SearchingCategoryIndexRequiresAVendor && !version.HasVendors*/)
                {
                    continue;
                }
                CategoryIndexableModel indexableModel = new()
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
                    // Category Properties
                    SortOrder = version.SortOrder,
                    RequiresRoles = version.RequiresRoles,
                    RequiresRolesAlt = version.RequiresRolesAlt,
                    // Associated Objects
                    HasBrands = version.HasBrands,
                    Brands = version.Brands,
                    HasCategories = version.HasCategories, // CategoriesRead handled below
                    HasFranchises = version.HasFranchises,
                    Franchises = version.Franchises,
                    /*
                    HasManufacturers = version.HasManufacturers,
                    Manufacturers = version.Manufacturers,
                    */
                    HasProducts = version.HasProducts,
                    Products = version.Products,
                    RatingToIndexAs = version.RatingToIndexAs,
                    HasStores = version.HasStores,
                    Stores = version.Stores,
                    /*
                    HasVendors = version.HasVendors,
                    Vendors = version.Vendors,
                    */
                };
                HandleFilterableAttributes(
                    version.FilterableJsonAttributes,
                    filterableAttributes,
                    indexableModel,
                    true);
                if (version.HasCategories)
                {
                    HandleCategoryData(categories, new List<IndexableCategoryFilterRead>() { version.CategoriesRead! }, indexableModel);
                }
                if (ElasticSearchingProviderConfig.SearchingCategoryIndexFiltersIncludeRoles)
                {
                    HandleRoles(version.RequiresRoles, indexableModel);
                }
                HandleStandardSuggests(indexableModel);
                HandleCustomSuggests(indexableModel);
                HandleQueryableAttributes(version.QueryableJsonAttributes, indexableModel, queryableAttributeKeyList);
                yield return indexableModel;
            }
            Log("Exited", contextProfileName).Wait(10_000, ct);
        }

        /// <summary>Handles the custom suggests described by indexableModel.</summary>
        /// <param name="indexableModel">The indexable model.</param>
        protected virtual void HandleCustomSuggests(
            CategoryIndexableModel indexableModel)
        {
        }
    }
}
