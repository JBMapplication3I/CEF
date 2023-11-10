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

    /// <summary>A store dump reader.</summary>
    internal class StoreDumpReader : DumpReaderBase<StoreIndexableModel>
    {
        /// <inheritdoc/>
        public override IEnumerable<StoreIndexableModel> GetRecords(string? contextProfileName, CancellationToken ct)
        {
            Log("Entered", contextProfileName).Wait(10_000, ct);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var filterableAttributes = LoadFilterableAttributes(context);
            // var queryableAttributes = LoadQueryableAttributes(context);
            var categories = LoadInitialCategoryData(context);
            var types = LoadInitialTypeData<ProductType>(context);
            var regions = LoadInitialRegionData(context);
            // Get List of type keys we want to filter by
            var keys = ElasticSearchingProviderConfig.SearchingStoreIndexFilterByTypeKeys;
            var keyList = Array.Empty<string>();
            if (Contract.CheckNotEmpty(keys))
            {
                keyList = keys!.Select(x => x.Trim()).ToArray();
            }
            // Get list of attrubutes that can be queried using search term
            var queryableAttributeKeyList = Array.Empty<string>();
            if (Contract.CheckValidKey(ElasticSearchingProviderConfig.SearchingStoreIndexQueryByAttributeKeys))
            {
                queryableAttributeKeyList = ElasticSearchingProviderConfig.SearchingStoreIndexQueryByAttributeKeys!
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
            }
            var now = DateExtensions.GenDateTime;
            foreach (var version in context.Stores
                .FilterByActive(true)
                .FilterByTypeKeys<Store, StoreType>(keyList)
                .Where(x => (!x.StartDate.HasValue || now >= x.StartDate.Value)
                    && (!x.EndDate.HasValue || now < x.EndDate.Value))
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
                    // Store Properties
                    x.DisplayInStorefront,
                    x.Slogan,
                    x.MissionStatement,
                    x.ExternalUrl,
                    x.About,
                    x.Overview,
                    ContactCity = x.Contact != null && x.Contact.Address != null ? x.Contact.Address.City : null,
                    HasDistricts = x.StoreDistricts!.Any(y => y.Active && y.Master!.Active),
                    Districts = x.StoreDistricts!
                        .Where(y => y.Active && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.SlaveID,
                            Key = y.Slave!.CustomKey,
                            y.Slave.Name,
                        }),
                    ContactRegionID = x.Contact != null && x.Contact.Address != null ? x.Contact.Address.RegionID : null,
                    // ContactDistrictID = x.Contact != null && x.Contact.Address != null ? x.Contact.Address.DistrictID : null,
                    x.OperatingHoursTimeZoneId,
                    x.OperatingHoursMondayStart,
                    x.OperatingHoursMondayEnd,
                    x.OperatingHoursTuesdayStart,
                    x.OperatingHoursTuesdayEnd,
                    x.OperatingHoursWednesdayStart,
                    x.OperatingHoursWednesdayEnd,
                    x.OperatingHoursThursdayStart,
                    x.OperatingHoursThursdayEnd,
                    x.OperatingHoursFridayStart,
                    x.OperatingHoursFridayEnd,
                    x.OperatingHoursSaturdayStart,
                    x.OperatingHoursSaturdayEnd,
                    x.OperatingHoursSundayStart,
                    x.OperatingHoursSundayEnd,
                    x.OperatingHoursClosedStatement,
                    x.SortOrder,
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
                    /*
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
                    */
                    HasVendors = x.Vendors!.Any(y => y.Active && y.Master!.Active),
                    Vendors = x.Vendors!
                        .Where(y => y.Active && y.Master!.Active)
                        .Select(y => new
                        {
                            ID = y.MasterID,
                            Key = y.Master!.CustomKey,
                            y.Master.Name,
                        }),
                })
                .ToList()
                .Select(x => new // StoreIndexableModel
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
                    // Store Properties
                    DisplayInStorefront = x.DisplayInStorefront ?? true,
                    x.Slogan,
                    x.MissionStatement,
                    x.ExternalUrl,
                    x.About,
                    x.Overview,
                    x.ContactCity,
                    x.ContactRegionID,
                    // TODO: x.ContactDistrictID,
                    x.OperatingHoursTimeZoneId,
                    x.OperatingHoursMondayStart,
                    x.OperatingHoursMondayEnd,
                    x.OperatingHoursTuesdayStart,
                    x.OperatingHoursTuesdayEnd,
                    x.OperatingHoursWednesdayStart,
                    x.OperatingHoursWednesdayEnd,
                    x.OperatingHoursThursdayStart,
                    x.OperatingHoursThursdayEnd,
                    x.OperatingHoursFridayStart,
                    x.OperatingHoursFridayEnd,
                    x.OperatingHoursSaturdayStart,
                    x.OperatingHoursSaturdayEnd,
                    x.OperatingHoursSundayStart,
                    x.OperatingHoursSundayEnd,
                    x.OperatingHoursClosedStatement,
                    SortOrder = x.SortOrder ?? 0,
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
                    x.HasDistricts,
                    Districts = x.Districts
                        .Select(y => new IndexableDistrictFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
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
                    Products = x.Products
                        .Select(y => new IndexableProductFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    x.RatingToIndexAs,
                    /*
                    x.HasStores,
                    Stores = x.Stores
                        .Select(y => new IndexableStoreFilter
                        {
                            ID = y.ID,
                            Key = y.Key,
                            Name = y.Name,
                        }),
                    */
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
                if (ElasticSearchingProviderConfig.SearchingStoreIndexRequiresABrand && !version.HasBrands
                    || ElasticSearchingProviderConfig.SearchingStoreIndexRequiresACategory && !version.HasCategories
                    || ElasticSearchingProviderConfig.SearchingStoreIndexRequiresAFranchise && !version.HasFranchises
                    || ElasticSearchingProviderConfig.SearchingStoreIndexRequiresAManufacturer && !version.HasManufacturers
                    || ElasticSearchingProviderConfig.SearchingStoreIndexRequiresAProduct && !version.HasProducts
                    /*|| ElasticSearchingProviderConfig.SearchingStoreIndexRequiresAStore && !version.HasStores */
                    || ElasticSearchingProviderConfig.SearchingStoreIndexPrivateStoresEnabled && !(bool)version.DisplayInStorefront
                    || ElasticSearchingProviderConfig.SearchingStoreIndexRequiresAVendor && !version.HasVendors)
                {
                    continue;
                }
                StoreIndexableModel indexableModel = new()
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
                    // Store Properties
                    Slogan = version.Slogan,
                    MissionStatement = version.MissionStatement,
                    ExternalUrl = version.ExternalUrl,
                    About = version.About,
                    Overview = version.Overview,
                    ContactCity = version.ContactCity,
                    ContactRegionID = version.ContactRegionID,
                    ContactRegionName = version.ContactRegionID.HasValue && regions.ContainsKey(version.ContactRegionID.Value)
                        ? regions[version.ContactRegionID.Value].Name
                        : "N/A",
                    // TODO: ContactDistrictID = version.ContactDistrictID,
                    OperatingHoursTimeZoneId = version.OperatingHoursTimeZoneId,
                    OperatingHoursMondayStart = version.OperatingHoursMondayStart,
                    OperatingHoursMondayEnd = version.OperatingHoursMondayEnd,
                    OperatingHoursTuesdayStart = version.OperatingHoursTuesdayStart,
                    OperatingHoursTuesdayEnd = version.OperatingHoursTuesdayEnd,
                    OperatingHoursWednesdayStart = version.OperatingHoursWednesdayStart,
                    OperatingHoursWednesdayEnd = version.OperatingHoursWednesdayEnd,
                    OperatingHoursThursdayStart = version.OperatingHoursThursdayStart,
                    OperatingHoursThursdayEnd = version.OperatingHoursThursdayEnd,
                    OperatingHoursFridayStart = version.OperatingHoursFridayStart,
                    OperatingHoursFridayEnd = version.OperatingHoursFridayEnd,
                    OperatingHoursSaturdayStart = version.OperatingHoursSaturdayStart,
                    OperatingHoursSaturdayEnd = version.OperatingHoursSaturdayEnd,
                    OperatingHoursSundayStart = version.OperatingHoursSundayStart,
                    OperatingHoursSundayEnd = version.OperatingHoursSundayEnd,
                    OperatingHoursClosedStatement = version.OperatingHoursClosedStatement,
                    SortOrder = version.SortOrder,
                    /*
                    RequiresRoles = version.RequiresRoles,
                    RequiresRolesAlt = version.RequiresRolesAlt,
                    */
                    // Associated Objects
                    HasBrands = version.HasBrands,
                    Brands = version.Brands,
                    HasCategories = version.HasCategories, // CategoriesRead handled below
                    HasDistricts = version.HasDistricts,
                    Districts = version.Districts,
                    HasFranchises = version.HasFranchises,
                    Franchises = version.Franchises,
                    HasManufacturers = version.HasManufacturers,
                    Manufacturers = version.Manufacturers,
                    HasProducts = version.HasProducts,
                    Products = version.Products,
                    RatingToIndexAs = version.RatingToIndexAs,
                    /*
                    HasStores = version.HasStores,
                    Stores = version.Stores,
                    */
                    HasVendors = version.HasVendors,
                    Vendors = version.Vendors,
                };
                HandleFilterableAttributes(
                    version.FilterableJsonAttributes,
                    filterableAttributes,
                    indexableModel,
                    true);
                HandleCategoryData(categories, version.CategoriesRead.ToList(), indexableModel);
                /*
                if (ElasticSearchingProviderConfig.SearchingStoreIndexFiltersIncludeRoles)
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
            StoreIndexableModel indexableModel)
        {
        }
    }
}
