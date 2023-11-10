// <copyright file="StoreCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store workflow class</summary>
// ReSharper disable StyleCop.SA1202
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Workflow;
    using JSConfigs;
    using Mapper;
    using Models;
    using ServiceStack;
    using Utilities;

    /// <summary>A store workflow.</summary>
    /// <seealso cref="NameableWorkflowBase{IStoreModel, IStoreSearchModel, IStore, Store}"/>
    /// <seealso cref="IStoreWorkflow"/>
    public partial class StoreWorkflow
    {
        /// <summary>Gets or sets the identifier of the internal type.</summary>
        /// <value>The identifier of the internal type.</value>
        private static int? InternalTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the distro center type.</summary>
        /// <value>The identifier of the distro center type.</value>
        private static int? DistroCenterTypeID { get; set; }

        /// <inheritdoc/>
        public Task<IStoreModel?> GetFullAsync(int id, string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(GetAsEntityFull(id, context).CreateStoreModelFromEntityFull(contextProfileName));
        }

        /// <inheritdoc/>
        public Task<IStoreModel?> GetFullAsync(string key, string? contextProfileName)
        {
            Contract.RequiresValidKey(key);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(GetAsEntityFull(key, context).CreateStoreModelFromEntityFull(contextProfileName));
        }

        /// <inheritdoc/>
        public override async Task<(List<IStoreModel> results, int totalPages, int totalCount)> SearchAsync(
            IStoreSearchModel search,
            bool asListing,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Stores/*.AsNoTracking()*/.AsQueryable();
            query = await FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false);
            // Sort by membership level using the SortOrder on the SubscriptionType
            if (search.SortByMembershipLevel ?? false)
            {
                query = query.OrderByDescending(
                    c => c.Accounts!.Where(sta => sta.Slave!.Active)
                        .Max(sta => sta.Slave!.Subscriptions!.Where(sub => sub.Active)
                            .Max(sub => sub.Type!.SortOrder)));
            }
            var (models, totalPages, totalCount) = asListing
                ? query.SelectListStoreAndMapToStoreModel(search.Paging, search.Sorts, search.Groupings, contextProfileName)
                : query.SelectLiteStoreAndMapToStoreModel(search.Paging, search.Sorts, search.Groupings, contextProfileName);
            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var model in models)
            {
                if (model.Contact?.Address == null)
                {
                    continue;
                }
                if (search.Latitude.HasValue
                    && search.Longitude.HasValue
                    && model.Contact.Address.Latitude.HasValue
                    && model.Contact.Address.Longitude.HasValue)
                {
                    // ReSharper disable PossibleInvalidOperationException
                    model.Distance = (decimal)GeographicDistance.BetweenLocations(
                        (double)model.Contact.Address.Latitude,
                        (double)model.Contact.Address.Longitude,
                        search.Latitude.Value,
                        search.Longitude.Value,
                        search.Units ?? Enums.LocatorUnits.Miles);
                    // ReSharper restore PossibleInvalidOperationException
                }
                else if (!string.IsNullOrWhiteSpace(search.ZipCode) && search.Radius.HasValue)
                {
                    var zipCode = await Workflows.ZipCodes.GetByZipCodeValueAsync(search.ZipCode, contextProfileName).ConfigureAwait(false);
                    if (zipCode?.Latitude == null || zipCode.Longitude == null || search.Radius.Value == 0)
                    {
                        continue;
                    }
                    // ReSharper disable PossibleInvalidOperationException
                    model.Distance = (decimal)GeographicDistance.BetweenLocations(
                        (double)(model.Contact.Address.Latitude ?? 0m),
                        (double)(model.Contact.Address.Longitude ?? 0m),
                        (double)zipCode.Latitude.Value,
                        (double)zipCode.Longitude.Value,
                        search.Units);
                    // ReSharper restore PossibleInvalidOperationException
                }
            }
            // ReSharper disable once PossibleMultipleEnumeration
            return (models.ToList(), totalPages, totalCount);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<decimal>> GetRevenueAsync(
            int storeID,
            int days,
            string? contextProfileName)
        {
            var then = DateTime.Today.AddDays(Math.Max(0, days - 1) * -1);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.SalesOrders
                .Where(x => x.StoreID == storeID && x.CreatedDate >= then)
                .Select(x => x.Total)
                .DefaultIfEmpty(0)
                .SumAsync()
                .ConfigureAwait(false))
                .WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetStoreInventoryLocationsMatrixLastModifiedAsync(string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // ReSharper disable once PossibleInvalidOperationException
            var internalTypeID = InternalTypeID ??= await Workflows.StoreInventoryLocationTypes.ResolveWithAutoGenerateToIDAsync(
                    null,
                    "Internal-Warehouse",
                    "Internal Warehouse",
                    null,
                    contextProfileName)
                .ConfigureAwait(false);
            // ReSharper disable once PossibleInvalidOperationException
            var distroCenterTypeID = DistroCenterTypeID ??= await Workflows.StoreInventoryLocationTypes.ResolveWithAutoGenerateToIDAsync(
                    null,
                    "Distribution-Center",
                    "Distribution Center",
                    null,
                    contextProfileName)
                .ConfigureAwait(false);
            return await context.Stores
                .AsNoTracking()
                .FilterByActive(true)
                .Select(x => new
                {
                    StoreDate = x.UpdatedDate ?? x.CreatedDate,
                    InternalInventoryLocation = x.StoreInventoryLocations!
                        .Where(y => y.Active && y.TypeID == internalTypeID)
                        .Select(y => new
                        {
                            Date = y.UpdatedDate ?? y.CreatedDate,
                            SlaveDate = y.Slave!.UpdatedDate ?? y.Slave.CreatedDate,
                        })
                        .FirstOrDefault(),
                    DistributionCenterInventoryLocation = x.StoreInventoryLocations!
                        .Where(y => y.Active && y.TypeID == distroCenterTypeID)
                        .Select(y => new
                        {
                            Date = y.UpdatedDate ?? y.CreatedDate,
                            SlaveDate = y.Slave!.UpdatedDate ?? y.Slave.CreatedDate,
                        })
                        .FirstOrDefault(),
                })
                .Select(x => new
                {
                    x.StoreDate,
                    IILDate = x.InternalInventoryLocation == null
                        ? (DateTime?)null
                        : x.InternalInventoryLocation.Date > x.InternalInventoryLocation.SlaveDate
                            ? x.InternalInventoryLocation.Date
                            : x.InternalInventoryLocation.SlaveDate,
                    DCDate = x.DistributionCenterInventoryLocation == null
                        ? (DateTime?)null
                        : x.DistributionCenterInventoryLocation.Date > x.DistributionCenterInventoryLocation.SlaveDate
                            ? x.DistributionCenterInventoryLocation.Date
                            : x.DistributionCenterInventoryLocation.SlaveDate,
                })
                .Select(x => x.StoreDate > x.IILDate && x.StoreDate > x.DCDate
                    ? x.StoreDate
                    : x.IILDate > x.StoreDate && x.IILDate > x.DCDate
                        ? x.IILDate
                        : x.DCDate)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<IStoreInventoryLocationsMatrixModel>> GetStoreInventoryLocationsMatrixAsync(
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // ReSharper disable once PossibleInvalidOperationException
            var internalTypeID = InternalTypeID ??= await Workflows.StoreInventoryLocationTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Internal-Warehouse",
                    byName: "Internal Warehouse",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            // ReSharper disable once PossibleInvalidOperationException
            var distroCenterTypeID = DistroCenterTypeID ??= await Workflows.StoreInventoryLocationTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Distribution-Center",
                    byName: "Distribution Center",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return await context.Stores
                .AsNoTracking()
                .FilterByActive(true)
                .Select(x => new
                {
                    StoreID = x.ID,
                    StoreKey = x.CustomKey,
                    StoreName = x.Name,
                    InternalInventoryLocation = x.StoreInventoryLocations!
                        .Where(y => y.Active && y.TypeID == internalTypeID)
                        .Select(y => new
                        {
                            y.Slave!.ID,
                            y.Slave.CustomKey,
                            y.Slave.Name,
                        })
                        .FirstOrDefault(),
                    DistributionCenterInventoryLocation = x.StoreInventoryLocations!
                        .Where(y => y.Active && y.TypeID == distroCenterTypeID)
                        .Select(y => new
                        {
                            y.Slave!.ID,
                            y.Slave.CustomKey,
                            y.Slave.Name,
                        })
                        .FirstOrDefault(),
                })
                .Select(x => new StoreInventoryLocationsMatrixModel
                {
                    StoreID = x.StoreID,
                    StoreKey = x.StoreKey,
                    StoreName = x.StoreName,
                    InternalInventoryLocationID = x.InternalInventoryLocation != null ? x.InternalInventoryLocation.ID : 0,
                    InternalInventoryLocationKey = x.InternalInventoryLocation != null ? x.InternalInventoryLocation.CustomKey : null,
                    InternalInventoryLocationName = x.InternalInventoryLocation != null ? x.InternalInventoryLocation.Name : null,
                    DistributionCenterInventoryLocationID = x.DistributionCenterInventoryLocation != null ? x.DistributionCenterInventoryLocation.ID : 0,
                    DistributionCenterInventoryLocationKey = x.DistributionCenterInventoryLocation != null ? x.DistributionCenterInventoryLocation.CustomKey : null,
                    DistributionCenterInventoryLocationName = x.DistributionCenterInventoryLocation != null ? x.DistributionCenterInventoryLocation.Name : null,
                })
                .ToListAsync<IStoreInventoryLocationsMatrixModel>()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<IUserModel>> GetStoreAdministratorUserAsync(
            int storeID,
            string? contextProfileName)
        {
            Contract.RequiresValidID(storeID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var userID = await context.Stores
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(storeID)
                .SelectMany(x => x.Users!)
                .FilterByActive(true)
                .Where(x => x.Slave!.Active && x.Slave.Roles.Any(y => y.Role!.Name == "CEF Store Administrator"))
                .Select(x => (int?)x.SlaveID)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(userID))
            {
                return CEFAR.FailingCEFAR<IUserModel>("No Store Administrator Found");
            }
            var user = context.Users
                .AsNoTracking()
                .FilterByID(userID)
                .SelectSingleFullUserAndMapToUserModel(contextProfileName);
            if (user == null)
            {
                return CEFAR.FailingCEFAR<IUserModel>("Unable to load User");
            }
            // Clear anything to do with the password from the model before returning
            user.OverridePassword = user.PasswordHash = null;
            // This should never be set, it's actually deprecated in favor of identity roles
            user.IsSuperAdmin = false;
            // Return a cleaned model
            return user.WrapInPassingCEFARIfNotNull();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<decimal>> GetSalesCountAsync(
            int storeID,
            string type,
            string status,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            switch (type.ToLower())
            {
                case "order":
                {
                    return ((decimal)await context.SalesOrders
                            .FilterByActive(true)
                            .FilterByNullableStoreID(storeID)
                            .FilterByStatusName<SalesOrder, SalesOrderStatus>(status)
                            .CountAsync()
                            .ConfigureAwait(false))
                        .WrapInPassingCEFAR();
                }
                case "invoice":
                {
                    return ((decimal)await context.SalesInvoices
                            .FilterByActive(true)
                            .FilterByNullableStoreID(storeID)
                            .FilterByStatusName<SalesInvoice, SalesInvoiceStatus>(status)
                            .CountAsync()
                            .ConfigureAwait(false))
                        .WrapInPassingCEFAR();
                }
                case "quote":
                {
                    return ((decimal)await context.SalesQuotes
                            .FilterByActive(true)
                            .FilterByNullableStoreID(storeID)
                            .FilterByStatusName<SalesQuote, SalesQuoteStatus>(status)
                            .CountAsync()
                            .ConfigureAwait(false))
                        .WrapInPassingCEFAR();
                }
                default:
                {
                    throw new ArgumentException("Invalid type");
                }
            }
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<decimal>> GetSalesCountAsync(
            int storeID,
            string type,
            string[] statuses,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            switch (type.ToLower())
            {
                case "order":
                {
                    return ((decimal)await context.SalesOrders
                            .FilterByActive(true)
                            .FilterByNullableStoreID(storeID)
                            .FilterByStatusNames<SalesOrder, SalesOrderStatus>(statuses)
                            .CountAsync()
                            .ConfigureAwait(false))
                        .WrapInPassingCEFAR();
                }
                case "invoice":
                {
                    return ((decimal)await context.SalesInvoices
                            .FilterByActive(true)
                            .FilterByNullableStoreID(storeID)
                            .FilterByStatusNames<SalesInvoice, SalesInvoiceStatus>(statuses)
                            .CountAsync()
                            .ConfigureAwait(false))
                        .WrapInPassingCEFAR();
                }
                case "quote":
                {
                    return ((decimal)await context.SalesQuotes
                            .FilterByActive(true)
                            .FilterByNullableStoreID(storeID)
                            .FilterByStatusNames<SalesQuote, SalesQuoteStatus>(statuses)
                            .CountAsync()
                            .ConfigureAwait(false))
                        .WrapInPassingCEFAR();
                }
                default:
                {
                    throw new ArgumentException("Invalid type");
                }
            }
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<SerializableAttributesDictionary>> UpdateAttributesAsync(
            int storeID,
            SerializableAttributesDictionary serializableAttributes,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.Stores.FilterByID(storeID).SingleOrDefaultAsync();
            if (entity == null)
            {
                return CEFAR.FailingCEFAR<SerializableAttributesDictionary>("Could not locate the Store to update.");
            }
            var dummy = RegistryLoaderWrapper.GetInstance<IStoreModel>(contextProfileName);
            dummy.SerializableAttributes = serializableAttributes;
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, dummy, contextProfileName).ConfigureAwait(false);
            entity.UpdatedDate = DateExtensions.GenDateTime;
            var result = await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            return result
                ? entity.SerializableAttributes.WrapInPassingCEFAR()!
                : CEFAR.FailingCEFAR<SerializableAttributesDictionary>("Something about this data failed to save.");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<IStoreModel>> CloneStoreAsync(
            int storeID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var store = await context.Stores.FilterByID(storeID).AsNoTracking()
                .Include(x => x.Accounts)
                .Include(x => x.Brands)
                .Include(x => x.Images)
                .Include(x => x.Manufacturers)
                .Include(x => x.Products)
                .Include(x => x.StoreBadges)
                .Include(x => x.StoreContacts)
                .Include(x => x.StoreInventoryLocations)
                .Include(x => x.StoreSubscriptions)
                .Include(x => x.Users)
                .Include(x => x.Vendors)
                .SingleOrDefaultAsync().ConfigureAwait(false);
            if (store == null)
            {
                return CEFAR.FailingCEFAR<IStoreModel>("Could not locate the Store to clone.");
            }
            var createdDate = DateExtensions.GenDateTime;
            store.CreatedDate = createdDate;
            store.UpdatedDate = null;
            store.Active = false;
            if (store.Contact != null)
            {
                store.Contact.ID = 0;
                if (store.Contact.Address != null)
                {
                    store.Contact.Address.ID = 0;
                }
            }
            for (var i = 0; store.Accounts?.Any() == true && i < store.Accounts.Count; i++)
            {
                var storeAccount = store.Accounts.ElementAt(i);
                storeAccount.CreatedDate = createdDate;
                storeAccount.UpdatedDate = null;
                storeAccount.PricePointID = null;
                if (storeAccount.PricePoint != null)
                {
                    storeAccount.PricePoint.ID = 0;
                }
            }
            for (var i = 0; store.Users?.Any() == true && i < store.Users.Count; i++)
            {
                var storeUser = store.Users.ElementAt(i);
                storeUser.CreatedDate = createdDate;
                storeUser.UpdatedDate = null;
            }
            for (var i = 0; store.StoreContacts?.Any() == true && i < store.StoreContacts.Count; i++)
            {
                var storeContact = store.StoreContacts.ElementAt(i);
                storeContact.CreatedDate = createdDate;
                storeContact.UpdatedDate = null;
                storeContact.SlaveID = 0;
                if (storeContact.Slave == null)
                {
                    continue;
                }
                storeContact.Slave.ID = 0;
                storeContact.Slave.CreatedDate = createdDate;
                storeContact.Slave.UpdatedDate = null;
                if (storeContact.Slave.Address == null)
                {
                    continue;
                }
                storeContact.Slave.Address.ID = 0;
                storeContact.Slave.Address.CreatedDate = createdDate;
                storeContact.Slave.Address.UpdatedDate = null;
            }
            for (var i = 0; store.Brands?.Any() == true && i < store.Brands.Count; i++)
            {
                var storeBrand = store.Brands.ElementAt(i);
                storeBrand.CreatedDate = createdDate;
                storeBrand.UpdatedDate = null;
            }
            for (var i = 0; store.Images?.Any() == true && i < store.Images.Count; i++)
            {
                var storeImage = store.Images.ElementAt(i);
                storeImage.CreatedDate = createdDate;
                storeImage.UpdatedDate = null;
            }
            for (var i = 0; store.Manufacturers?.Any() == true && i < store.Manufacturers.Count; i++)
            {
                var storeManufacturer = store.Manufacturers.ElementAt(i);
                storeManufacturer.CreatedDate = createdDate;
                storeManufacturer.UpdatedDate = null;
            }
            for (var i = 0; store.Products?.Any() == true && i < store.Products.Count; i++)
            {
                var storeProduct = store.Products.ElementAt(i);
                storeProduct.CreatedDate = createdDate;
                storeProduct.UpdatedDate = null;
            }
            // Don't copy Store reviews
            store.Reviews = null;
            for (var i = 0; store.StoreBadges?.Any() == true && i < store.StoreBadges.Count; i++)
            {
                var storeBadge = store.StoreBadges.ElementAt(i);
                storeBadge.CreatedDate = createdDate;
                storeBadge.UpdatedDate = null;
            }
            for (var i = 0; store.StoreInventoryLocations?.Any() == true && i < store.StoreInventoryLocations.Count; i++)
            {
                var storeInventoryLocation = store.StoreInventoryLocations.ElementAt(i);
                storeInventoryLocation.CreatedDate = createdDate;
                storeInventoryLocation.UpdatedDate = null;
            }
            for (var i = 0; store.StoreSubscriptions?.Any() == true && i < store.StoreSubscriptions.Count; i++)
            {
                var storeSubscription = store.StoreSubscriptions.ElementAt(i);
                storeSubscription.CreatedDate = createdDate;
                storeSubscription.UpdatedDate = null;
            }
            for (var i = 0; store.Vendors?.Any() == true && i < store.Vendors.Count; i++)
            {
                var storeUser = store.Vendors.ElementAt(i);
                storeUser.CreatedDate = createdDate;
                storeUser.UpdatedDate = null;
            }
            context.Stores.Add(store);
            var result = await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            return result
                ? CEFAR.PassingCEFAR(store.CreateStoreModelFromEntityFull(contextProfileName))!
                : CEFAR.FailingCEFAR<IStoreModel>("Something about cloning the store failed.");
        }

        /// <inheritdoc/>
        public async Task<int?> GetStoreIDForAdminPortalAsync(
            string hostUrl,
            string sessionUserAuthId,
            string? contextProfileName)
        {
            Contract.Requires<ArgumentException>(int.TryParse(sessionUserAuthId, out var userID));
            Contract.RequiresValidID(userID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var storeID = await context.StoreUsers
                .AsNoTracking()
                .FilterByActive(true)
                .FilterIAmARelationshipTableBySlaveID<StoreUser, Store, User>(userID)
                .Select(x => x.MasterID)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(storeID))
            {
                throw HttpError.Unauthorized("This user is not assigned to a store");
            }
            return storeID;
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<Store>> FilterQueryByModelCustomAsync(
            IQueryable<Store> query,
            IStoreSearchModel search,
            IClarityEcommerceEntities context)
        {
            var zipCode = await Workflows.ZipCodes.GetByZipCodeValueAsync(
                    search.ZipCode,
                    context.ContextProfileName)
                .ConfigureAwait(false);
            return query
                .FilterByHaveATypeSearchModel<Store, StoreType>(search)
                .OrderBy(v => v.Name)
                // Filter within ZipCode Radius
                .FilterStoresByZipCodeRadius(zipCode?.Latitude, zipCode?.Longitude, search.Radius, search.Units)
                // Filter within Lat/Long Radius
                .FilterStoresByLatitudeLongitudeRadius(search.Latitude, search.Longitude, search.Radius, search.Units)
                .FilterStoresByRegionID(search.RegionID)
                .FilterStoresByAnyContactAddressMatchingRegionID(search.StoreContactRegionID)
                .FilterStoresByAnyContactAddressMatchingCountryID(search.CountryID)
                .FilterStoresByAnyContactAddressMatchingRegionID(search.StoreContactCountryID)
                .FilterStoresByAnyContactAddressMatchingCity(search.City)
                .FilterStoresByAnyContactAddressMatchingCity(search.StoreContactCity)
                .FilterStoresByDistrictID(search.DistrictID);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeactivateAsync(
            Store? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot Deactivate a null record");
            }
            var timestamp = DateExtensions.GenDateTime;
            if (context.StoreProducts != null)
            {
                // Must wrap in null check for unit tests
                if (ProviderConfig.GetBooleanSetting("Clarity.Stores.Deactivate.AlsoDeactivatesAssociatedProducts"))
                {
                    var changed = false;
                    foreach (var id in await context.StoreProducts.AsNoTracking().Where(x => x.Active && x.MasterID == entity.ID).Select(x => x.SlaveID).ToListAsync().ConfigureAwait(false))
                    {
                        await Workflows.Products.DeactivateAsync(id, context).ConfigureAwait(false);
                        changed = true;
                    }
                    if (changed)
                    {
                        await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                    }
                }
                await DeactivateAssociatedAsMasterObjectsAsync<StoreProduct>(entity.ID, timestamp, context).ConfigureAwait(false);
            }
            if (context.StoreUsers != null)
            {
                // Must wrap in null check for unit tests
                if (ProviderConfig.GetBooleanSetting("Clarity.Stores.Deactivate.AlsoDeactivatesAssociatedUsers"))
                {
                    var changed = false;
                    foreach (var id in await context.StoreUsers.AsNoTracking().Where(x => x.Active && x.MasterID == entity.ID).Select(x => x.SlaveID).ToListAsync().ConfigureAwait(false))
                    {
                        await Workflows.Users.DeactivateAsync(id, context).ConfigureAwait(false);
                        changed = true;
                    }
                    if (changed)
                    {
                        await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                    }
                }
                await DeactivateAssociatedAsMasterObjectsAsync<StoreUser>(entity.ID, timestamp, context).ConfigureAwait(false);
            }
            await DeactivateAssociatedImagesAsync<StoreImage>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<StoreAccount>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<StoreBadge>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<StoreContact>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<StoreInventoryLocation>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<StoreManufacturer>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<StoreSubscription>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<StoreVendor>(entity.ID, timestamp, context).ConfigureAwait(false);
            return await base.DeactivateAsync(entity, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IStore entity,
            IStoreModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Store Properties
            entity.UpdateStoreFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            // Related Objects
            await model.Contact.AssignPrePropertiesToContactAndAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            entity.Contact.AssignPostPropertiesToContactAndAddress(model.Contact, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null, context.ContextProfileName);
            await RunLimitedAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            if (!CEFConfigDictionary.DoAutoUpdateLatitudeLongitude)
            {
                return;
            }
            // Only overwrite Lat/Long if it is null. This allows for setting a store's actual Lat/Long
            if (entity.Contact?.Address != null
                && model.Contact?.Address != null
                && (entity.Contact.Address.Latitude == null || entity.Contact.Address.Longitude == null))
            {
                await Workflows.ZipCodes.UpdateLatitudeLongitudeBasedOnZipCodeAsync(
                        entity.Contact.Address,
                        model.Contact.Address,
                        context.ContextProfileName)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Store? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            // ReSharper disable once InvertIf
            if (context.StoreImages != null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.StoreImages.Count(x => x.MasterID == entity.ID);)
                {
                    context.StoreImages.Remove(context.StoreImages.First(x => x.MasterID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }

        /// <summary>Gets as entity full base.</summary>
        /// <param name="context">The context.</param>
        /// <returns>as entity full base.</returns>
        private static IQueryable<IStore> GetAsEntityFullBase(IClarityEcommerceEntities context)
        {
            return context.Stores
                .Include(x => x.Brands)
                .Include(x => x.Contact)
                .Include(x => x.Contact!.Address)
                .Include(x => x.Reviews)
                .Include(x => x.Notes)
                .Include(x => x.Accounts)
                .Include(x => x.StoreContacts)
                .Include(x => x.StoreInventoryLocations)
                .Include(x => x.StoreInventoryLocations!.Select(y => y.Slave))
                .Include(x => x.Manufacturers)
                .Include(x => x.Products)
                .Include(x => x.Products!.Select(y => y.Slave))
                .Include(x => x.StoreSubscriptions)
                .Include(x => x.Users)
                .Include(x => x.Vendors);
        }

        /// <summary>Gets as entity full.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="context">The context.</param>
        /// <returns>as entity full.</returns>
        private static IStore? GetAsEntityFull(int id, IClarityEcommerceEntities context)
        {
            return GetAsEntityFullBase(context).FilterByID(id).FirstOrDefault();
        }

        /// <summary>Gets as entity full.</summary>
        /// <param name="key">    The key to get.</param>
        /// <param name="context">The context.</param>
        /// <returns>as entity full.</returns>
        private static IStore? GetAsEntityFull(string key, IClarityEcommerceEntities context)
        {
            return GetAsEntityFullBase(context).FilterByActive(true).FilterByCustomKey(key, true).FirstOrDefault();
        }

        /// <summary>Executes all associate workflows operations with the default information.</summary>
        /// <remarks>Perform any extra resolvers before running this function.<br/>
        /// The calls are nullable so you could null out specific workflow if you don't want to it to run.</remarks>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task RunLimitedAssociateWorkflowsAsync(
            IStore entity,
            IStoreModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, contextProfileName).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            if (model.Notes != null)
            {
                if (Contract.CheckValidID(entity.ID))
                {
                    foreach (var note in model.Notes)
                    {
                        note.StoreID = entity.ID;
                    }
                }
                await Workflows.StoreWithNotesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
#pragma warning disable SA1501,format // Statement should not be on a single line
            if (model.Reviews != null) { await Workflows.StoreWithReviewsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.Images != null) { await Workflows.StoreWithImagesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.Brands != null) { await Workflows.StoreWithBrandsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.Accounts != null) { await Workflows.StoreWithAccountsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.StoreBadges != null) { await Workflows.StoreWithStoreBadgesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.StoreContacts != null) { await Workflows.StoreWithStoreContactsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.StoreInventoryLocations != null) { await Workflows.StoreWithStoreInventoryLocationsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (CEFConfigDictionary.ImportProductsAllowSaveStoreProductsWithStore)
            {
                if (model.Products != null) { await Workflows.StoreWithProductsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            }
            if (model.Manufacturers != null) { await Workflows.StoreWithManufacturersAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.StoreSubscriptions != null) { await Workflows.StoreWithStoreSubscriptionsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.Vendors != null) { await Workflows.StoreWithVendorsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.Users != null) { await Workflows.StoreWithUsersAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.StoreSubscriptions != null) { await Workflows.StoreWithStoreSubscriptionsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
            if (model.StoreRegions != null) { await Workflows.StoreWithStoreRegionsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false); }
#pragma warning restore SA1501,format // Statement should not be on a single line
        }
    }
}
