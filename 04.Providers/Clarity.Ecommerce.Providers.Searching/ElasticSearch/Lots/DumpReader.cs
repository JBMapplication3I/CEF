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
    using Utilities;

    /// <summary>A product dump reader.</summary>
    internal class LotDumpReader : DumpReaderBase<LotIndexableModel>
    {
        /// <inheritdoc/>
        public override IEnumerable<LotIndexableModel> GetRecords(string? contextProfileName, CancellationToken ct)
        {
            Log("Entered", contextProfileName).Wait(10_000, ct);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var filterableAttributes = LoadFilterableAttributes(context, true);
            // var queryableAttributes = LoadQueryableAttributes(context);
            var categories = LoadInitialCategoryData(context);
            // Get List of type keys we want to filter by
            var keys = ElasticSearchingProviderConfig.SearchingLotIndexFilterByTypeKeys;
            var keyList = Array.Empty<string>();
            if (Contract.CheckNotEmpty(keys))
            {
                keyList = keys!.Select(x => x.Trim()).ToArray();
            }
            // Get list of attrubutes that can be queried using search term
            var queryableAttributeKeyList = Array.Empty<string>();
            if (Contract.CheckValidKey(ElasticSearchingProviderConfig.SearchingLotIndexQueryByAttributeKeys))
            {
                queryableAttributeKeyList = ElasticSearchingProviderConfig.SearchingLotIndexQueryByAttributeKeys
                    !.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
            }
            foreach (var version in context.Lots
                .FilterByActive(true)
                .FilterByTypeKeys<Lot, LotType>(keyList)
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
                    // x.SeoUrl,
                    // x.SeoKeywords,
                    // x.SeoDescription,
                    // Lot Properties
                    // Associated Objects
                    // Brands
                    HasBrands = x.Auction!.Brands!.Any(y => y.Active && y.IsVisibleIn && y.Master!.Active),
                    Brands = x.Auction!.Brands!
                        .Where(y => y.Active && y.IsVisibleIn && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    // Categories
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
                    // Franchises
                    HasFranchises = x.Auction.Franchises!.Any(y => y.Active && y.IsVisibleIn && y.Master!.Active),
                    Franchises = x.Auction.Franchises!
                        .Where(y => y.Active && y.IsVisibleIn && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    // Manufacturers
                    HasManufacturers = x.Product!.Manufacturers!.Any(y => y.Active && y.Master!.Active),
                    Manufacturers = x.Product!.Manufacturers!
                        .Where(y => y.Active && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    // Products
                    HasProducts = true, // x.Products!.Any(y => y.Active && y.Master!.Active),
                    Product = new
                    {
                        x.Product.ID,
                        Key = x.Product.CustomKey,
                        x.Product.Name,
                    },
                    // Stores
                    HasStores = x.Auction!.Stores!.Any(y => y.Active && y.IsVisibleIn && y.Master!.Active),
                    Stores = x.Auction!.Stores!
                        .Where(y => y.Active
                            && y.IsVisibleIn
                            && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    // Vendors
                    HasVendors = x.Product!.Vendors!.Any(y => y.Active && y.Master!.Active),
                    Vendors = x.Product!.Vendors!
                        .Where(y => y.Active && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                })
                .ToList()
                .Select(x => new // LotIndexableModel
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
                    // x.SeoUrl,
                    // x.SeoKeywords,
                    // x.SeoDescription,
                    // Lot Properties
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
                    x.HasProducts,
                    Products = new[]
                    {
                        new IndexableProductFilter
                        {
                            ID = x.Product.ID,
                            Key = x.Product.Key,
                            Name = x.Product.Name,
                        },
                    },
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
                }))
            {
                if (ElasticSearchingProviderConfig.SearchingLotIndexRequiresABrand && !version.HasBrands
                    || ElasticSearchingProviderConfig.SearchingLotIndexRequiresACategory && !version.HasCategories
                    || ElasticSearchingProviderConfig.SearchingLotIndexRequiresAFranchise && !version.HasFranchises
                    || ElasticSearchingProviderConfig.SearchingLotIndexRequiresAManufacturer && !version.HasManufacturers
                    // || ElasticSearchingProviderConfig.SearchingLotIndexRequiresALot && !version.HasLots
                    || ElasticSearchingProviderConfig.SearchingLotIndexRequiresAStore && !version.HasStores
                    || ElasticSearchingProviderConfig.SearchingLotIndexRequiresAVendor && !version.HasVendors)
                {
                    continue;
                }
                LotIndexableModel indexableModel = new()
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
                    // SeoUrl = version.SeoUrl,
                    // SeoKeywords = version.SeoKeywords,
                    // SeoDescription = version.SeoDescription,
                    // Lot Properties
                    // Associated Objects
                    HasBrands = version.HasBrands,
                    Brands = version.Brands,
                    HasCategories = version.HasCategories, // CategoriesRead handled below
                    HasFranchises = version.HasFranchises,
                    Franchises = version.Franchises,
                    HasManufacturers = version.HasManufacturers,
                    Manufacturers = version.Manufacturers,
                    HasProducts = version.HasProducts,
                    Products = version.Products,
                    HasStores = version.HasStores,
                    Stores = version.Stores,
                    HasVendors = version.HasVendors,
                    Vendors = version.Vendors,
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
                HandleCategoryData(categories, version.CategoriesRead.ToList(), indexableModel);
                /*
                if (ElasticSearchingProviderConfig.SearchingLotIndexFiltersIncludeRoles)
                {
                    HandleRoles(version.RequiresRoles, indexableModel);
                }
                */
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
            LotIndexableModel indexableModel)
        {
        }
    }
}
