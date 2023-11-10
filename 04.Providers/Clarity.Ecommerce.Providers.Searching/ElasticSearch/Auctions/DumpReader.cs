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
    using MoreLinq;
    using Utilities;

    /// <summary>A product dump reader.</summary>
    internal class AuctionDumpReader : DumpReaderBase<AuctionIndexableModel>
    {
        /// <inheritdoc/>
        public override IEnumerable<AuctionIndexableModel> GetRecords(string? contextProfileName, CancellationToken ct)
        {
            Log("Entered", contextProfileName).Wait(10_000, ct);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var filterableAttributes = LoadFilterableAttributes(context, true);
            // var queryableAttributes = LoadQueryableAttributes(context);
            var categories = LoadInitialCategoryData(context);
            // Get List of type keys we want to filter by
            var keys = ElasticSearchingProviderConfig.SearchingAuctionIndexFilterByTypeKeys;
            var keyList = Array.Empty<string>();
            if (Contract.CheckNotEmpty(keys))
            {
                keyList = keys!.Select(x => x.Trim()).ToArray();
            }
            // Get list of attrubutes that can be queried using search term
            var queryableAttributeKeyList = Array.Empty<string>();
            if (Contract.CheckValidKey(ElasticSearchingProviderConfig.SearchingAuctionIndexQueryByAttributeKeys))
            {
                queryableAttributeKeyList = ElasticSearchingProviderConfig.SearchingAuctionIndexQueryByAttributeKeys
                    !.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
            }
            foreach (var version in context.Auctions
                .FilterByActive(true)
                .FilterByTypeKeys<Auction, AuctionType>(keyList)
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
                    // Auction Properties
                    x.ClosesAt,
                    ContactCity = x.Contact != null && x.Contact.Address != null ? x.Contact.Address.City : null,
                    ContactRegionID = x.Contact != null && x.Contact.Address != null ? x.Contact.Address.RegionID : null,
                    // ContactDistrictID = x.Contact != null && x.Contact.Address != null ? x.Contact.Address.DistrictID : null,
                    // Associated Objects
                    // Brands
                    HasBrands = x.Brands!.Any(y => y.Active && y.IsVisibleIn && y.Master!.Active),
                    Brands = x.Brands!
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
                    HasFranchises = x.Franchises!.Any(y => y.Active && y.IsVisibleIn && y.Master!.Active),
                    Franchises = x.Franchises!
                        .Where(y => y.Active && y.IsVisibleIn && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                    // Manufacturers
                    HasManufacturersFromLots = x.Lots!.Any(y => y.Product!.Manufacturers!.Any(z => z.Active && z.Master!.Active)),
                    ManufacturersFromLots = x.Lots!.SelectMany(y => y.Product!.Manufacturers!
                        .Where(z => z.Active && z.Master!.Active)
                        .Select(z => new
                        {
                            ID = z.MasterID,
                            Key = z.Master!.CustomKey,
                            z.Master.Name,
                        })),
                    // Products
                    HasProductsFromLots = x.Lots!.Any(y => y.Active && y.Product!.Active),
                    ProductsFromLots = x.Lots!.Where(y => y.Active && y.Product!.Active)
                        .Select(y => new
                        {
                            ID = y.ProductID,
                            Key = y.Product!.CustomKey,
                            y.Product.Name,
                        }),
                    // Stores
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
                    // Vendors
                    HasVendorsFromLots = x.Lots!.Any(y => y.Product!.Vendors!.Any(z => z.Active && z.Master!.Active)),
                    VendorsFromLots = x.Lots!.SelectMany(y => y.Product!.Vendors!
                        .Where(z => z.Active && z.Master!.Active)
                        .Select(z => new
                        {
                            ID = z.MasterID,
                            Key = z.Master!.CustomKey,
                            z.Master.Name,
                        })),
                })
                .ToList()
                .Select(x => new // AuctionIndexableModel
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
                    // Auction Properties
                    x.ClosesAt,
                    x.ContactCity,
                    x.ContactRegionID,
                    // TODO: x.ContactDistrictID,
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
                    HasManufacturers = x.HasManufacturersFromLots,
                    Manufacturers = x.ManufacturersFromLots
                        .Select(y => new IndexableManufacturerFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        })
                        .DistinctBy(y => y.ID),
                    HasProducts = x.HasProductsFromLots,
                    Products = x.ProductsFromLots
                        .Select(y => new IndexableProductFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        })
                        .DistinctBy(y => y.ID),
                    x.HasStores,
                    Stores = x.Stores
                        .Select(y => new IndexableStoreFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    HasVendors = x.HasVendorsFromLots,
                    Vendors = x.VendorsFromLots
                        .Select(y => new IndexableVendorFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        })
                        .DistinctBy(y => y),
                }))
            {
                if (ElasticSearchingProviderConfig.SearchingAuctionIndexRequiresABrand && !version.HasBrands
                    || ElasticSearchingProviderConfig.SearchingAuctionIndexRequiresACategory && !version.HasCategories
                    || ElasticSearchingProviderConfig.SearchingAuctionIndexRequiresAFranchise && !version.HasFranchises
                    // || ElasticSearchingProviderConfig.SearchingAuctionIndexRequiresAManufacturer && !version.HasManufacturers
                    // || ElasticSearchingProviderConfig.SearchingAuctionIndexRequiresAAuction && !version.HasAuctions
                    || ElasticSearchingProviderConfig.SearchingAuctionIndexRequiresAStore && !version.HasStores
                    /*|| ElasticSearchingProviderConfig.SearchingAuctionIndexRequiresAVendor && !version.HasVendors*/)
                {
                    continue;
                }
                AuctionIndexableModel indexableModel = new()
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
                    // Auction Properties
                    ClosesAt = version.ClosesAt,
                    ContactCity = version.ContactCity,
                    ContactRegionID = version.ContactRegionID,
                    // TODO: ContactDistrictID = version.ContactDistrictID,
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
                if (ElasticSearchingProviderConfig.SearchingAuctionIndexFiltersIncludeRoles)
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
            AuctionIndexableModel indexableModel)
        {
        }
    }
}
