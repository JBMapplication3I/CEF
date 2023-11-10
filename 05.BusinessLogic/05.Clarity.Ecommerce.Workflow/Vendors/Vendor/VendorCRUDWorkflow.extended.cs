// <copyright file="VendorCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class VendorWorkflow
    {
        /// <inheritdoc/>
        public Task<List<IVendorProductModel>> GetVendorsByProductAsync(int productID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.VendorProducts
                    .FilterByActive(true)
                    .FilterVendorProductsByActiveVendors()
                    .FilterVendorProductsByActiveProducts()
                    .FilterVendorProductsByProductID(productID)
                    .SelectListVendorProductAndMapToVendorProductModel(contextProfileName)
                    .ToList());
        }

        /// <inheritdoc/>
        public Task<(IEnumerable<IVendorProductModel> results, int totalPages, int totalCount)> GetProductsByVendorAsync(
            IVendorProductSearchModel search,
            bool asListing,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.VendorProducts
                .AsNoTracking()
                .FilterByActive(search.Active)
                .FilterVendorProductsByActiveVendors()
                .FilterVendorProductsByActiveProducts()
                .FilterVendorProductsByVendorID(search.VendorID)
                .FilterVendorProductsByProductID(search.ProductID)
                .FilterVendorProductsByProductKey(search.ProductKey)
                .FilterVendorProductsByProductName(search.ProductName)
                .FilterVendorProductsByMinInventoryCount(search.MinInventoryCount)
                .FilterVendorProductsByMaxInventoryCount(search.MaxInventoryCount)
                .FilterByModifiedSince(search.ModifiedSince)
                .OrderBy(x => x.Slave!.Name);
            return Task.FromResult(asListing
                ? query.SelectListVendorProductAndMapToVendorProductModel(search.Paging, search.Sorts, search.Groupings, contextProfileName)
                : query.SelectLiteVendorProductAndMapToVendorProductModel(search.Paging, search.Sorts, search.Groupings, contextProfileName));
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int?>> GetIDByAssignedUserIDAsync(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var accountID = await context.Users
                .AsNoTracking()
                .FilterByID(Contract.RequiresValidID(userID))
                .Select(x => x.AccountID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(accountID))
            {
                return CEFAR.FailingCEFAR<int?>("ERROR: User or User's Account not found.");
            }
            var vendorID = await context.Vendors
                .AsNoTracking()
                .Where(x => x.Accounts!.Any(y => y.Active && y.SlaveID == accountID!.Value))
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            return vendorID.WrapInPassingCEFARIfNotNull("ERROR: No Vendor associated to this Account.");
        }

        /// <inheritdoc/>
        public Task<IVendorModel?> GetByUserNameAsync(string userName, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(context.Vendors
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => x.UserName == userName)
                .SelectSingleFullVendorAndMapToVendorModel(contextProfileName));
        }

        /// <inheritdoc/>
        public async Task<bool> AssignAccountToUserNameAsync(
            string userName,
            string accountToken,
            string? contextProfileName)
        {
            // TODO: Add logging
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var vendorID = await context.Vendors
                .AsNoTracking()
                .Where(x => x.UserName == userName)
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(vendorID))
            {
                return false;
            }
            var account = await context.Accounts
                .FilterByActive(true)
                .FilterAccountsByToken(accountToken, true, false)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (account == null)
            {
                return false;
            }
            if (account.Vendors!.Any(x => x.Active && x.MasterID == vendorID))
            {
                // Already assigned
                return true;
            }
            account.Vendors!.Add(new()
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
                MasterID = vendorID!.Value,
            });
            return await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<bool> ResetTokenAsync(string userName, string? contextProfileName)
        {
            // TODO: Add logging
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var vendor = await context.Vendors.SingleOrDefaultAsync(x => x.UserName == userName).ConfigureAwait(false);
            if (vendor == null)
            {
                return false;
            }
            vendor.SecurityToken = Guid.NewGuid().ToString();
            var execute = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            return execute;
        }

        /// <inheritdoc/>
        public async Task<string> UpdatePasswordAsync(string userName, string password, string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var vendor = await context.Vendors
                    .SingleOrDefaultAsync(x => x.UserName == userName)
                    .ConfigureAwait(false);
                if (vendor == null)
                {
                    return "could not locate vendor";
                }
                vendor.PasswordHash = HashString(password);
                vendor.MustResetPassword = false;
                return await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false)
                    ? "success"
                    : "no updates have been made";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <inheritdoc/>
        public async Task<bool> UserMustResetPasswordAsync(string userName, string? contextProfileName)
        {
            // TODO: Add logging
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Vendors
                .AsNoTracking()
                .Where(x => x.UserName == userName)
                .Select(x => (bool?)x.MustResetPassword)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false)
                ?? false;
        }

        /// <inheritdoc/>
        public async Task<string> LoginAsync(string userName, string password, string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var hashedPassword = HashString(password);
                return await context.Vendors
                    .AsNoTracking()
                    .Where(x => x.UserName == userName && x.PasswordHash == hashedPassword)
                    .Select(x => (int?)x.ID)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false)
                    == null
                        ? "vendor not found"
                        : "success";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IVendor entity,
            IVendorModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateVendorFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await model.Contact.AssignPrePropertiesToContactAndAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            entity.Contact.AssignPostPropertiesToContactAndAddress(model.Contact, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null, context.ContextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<Vendor>> FilterQueryByModelCustomAsync(
            IQueryable<Vendor> query,
            IVendorSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterVendorsByNotes(search.Notes)
                .OrderBy(v => v.Name);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeactivateAsync(
            Vendor? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot Deactivate a null record");
            }
            if (!entity.Active)
            {
                // Already inactive
                return CEFAR.PassingCEFAR();
            }
            var timestamp = DateExtensions.GenDateTime;
            await DeactivateAssociatedImagesAsync<VendorImage>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<VendorAccount>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<VendorManufacturer>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<VendorProduct>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<DiscountVendor>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<FavoriteVendor>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<StoreVendor>(entity.ID, timestamp, context).ConfigureAwait(false);
            var e = await context.Set<Vendor>().FilterByID(entity.ID).SingleAsync();
            e.UpdatedDate = timestamp;
            e.Active = false;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Failed to save Deactivate");
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Vendor? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            await ThrowIfAssociatedAsSalesItemTargetObjectAsync<PurchaseOrderItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemTargetObjectAsync<SalesOrderItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemTargetObjectAsync<SalesQuoteItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemTargetObjectAsync<SalesInvoiceItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemTargetObjectAsync<SalesReturnItemTarget>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSalesItemTargetObjectAsync<SampleRequestItemTarget>(entity.ID, context).ConfigureAwait(false);
            await DeleteAssociatedAsMasterObjectsAsync<VendorAccount>(entity.ID, context).ConfigureAwait(false);
            await DeleteAssociatedAsMasterObjectsAsync<VendorManufacturer>(entity.ID, context).ConfigureAwait(false);
            await DeleteAssociatedAsMasterObjectsAsync<VendorProduct>(entity.ID, context).ConfigureAwait(false);
            await DeleteAssociatedImagesAsync<VendorImage>(entity.ID, context).ConfigureAwait(false);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse, InvertIf
            if (context.Notes is not null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Notes.Count(x => x.SalesOrderID == entity.ID);)
                {
                    context.Notes.Remove(context.Notes.First(x => x.SalesOrderID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Notes.Count(x => x.VendorID == entity.ID);)
                {
                    context.Notes.Remove(context.Notes.First(x => x.VendorID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }

        /// <summary>Hash string.</summary>
        /// <param name="input">The input.</param>
        /// <returns>A string.</returns>
        private static string HashString(string input)
        {
            return string.IsNullOrEmpty(input)
                ? string.Empty
                : SHA1.Create()
                    .ComputeHash(Encoding.UTF8.GetBytes(input))
                    .Aggregate(string.Empty, (current, t) => current + t.ToString("x2").ToUpperInvariant());
        }

        /// <summary>Throw if associated as sales item target object asynchronous.</summary>
        /// <typeparam name="TTarget">Type of the set target.</typeparam>
        /// <param name="relatedID">Identifier for the related.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        private static async Task ThrowIfAssociatedAsSalesItemTargetObjectAsync<TTarget>(
                int relatedID,
                IDbContext context)
            where TTarget : class, ISalesItemTargetBase
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Set<TTarget>() is null)
            {
                return;
            }
            if (context.Set<TTarget>().Any(x => x.Active && x.OriginVendorProductID != null && x.OriginVendorProduct!.MasterID == relatedID))
            {
                throw new InvalidOperationException(
                    "Cannot delete or deactivate this record as it is tied by association to at least one record of"
                    + $" type {typeof(TTarget).Name} which should not be deleted or deactivated. If you still wish to"
                    + " delete or deactivate this record, you must delete or deactivate the association first, which"
                    + " may require performing the action against the database directly.");
            }
        }
    }
}
