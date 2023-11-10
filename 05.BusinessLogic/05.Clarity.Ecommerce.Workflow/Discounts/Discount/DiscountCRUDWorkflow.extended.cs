// <copyright file="DiscountCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales discount workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>A discount workflow.</summary>
    /// <seealso cref="NameableWorkflowBase{IDiscountModel, IDiscountSearchModel, IDiscount, Discount}"/>
    /// <seealso cref="IDiscountWorkflow"/>
    public partial class DiscountWorkflow
    {
        /// <summary>Initializes static members of the <see cref="DiscountWorkflow" /> class.</summary>
        static DiscountWorkflow()
        {
            try
            {
                EnsureMapOutHooks();
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <inheritdoc/>
        public async Task<int?> GetDiscountIDByCodeAsync(string code, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.DiscountCodes
                .FilterByActive(true)
                .FilterDiscountCodesByCode(code)
                .Select(x => (int?)x.DiscountID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task<List<IDiscountModel>> SearchAsync(
            string? searchTerm,
            bool? active,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Discounts
                .AsNoTracking()
                .FilterByActive(active);
            if (Contract.CheckValidKey(searchTerm))
            {
                var search = searchTerm!.Trim().ToLower();
                query = query
                    .Where(d => d.ID.ToString().Contains(search)
                        || d.CustomKey != null && d.CustomKey.Contains(search)
                        || d.Name != null && d.Name.Contains(search)
                        || d.Description != null && d.Description.Contains(search));
            }
            return Task.FromResult(
                query
                    .ApplySorting(new[] { new Sort { Field = "ID", Dir = "asc" } }, null, contextProfileName)
                    .SelectListDiscountAndMapToDiscountModel(contextProfileName)
                    .ToList());
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<bool>> EndDiscountByIDAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var discount = await context.Discounts
                .FilterByActive(true)
                .FilterByID(id)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            discount.EndDate = DateExtensions.GenDateTime.AddHours(-6);
            discount.UpdatedDate = DateExtensions.GenDateTime;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<Discount>> FilterQueryByModelCustomAsync(
            IQueryable<Discount> query,
            IDiscountSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterDiscountsByCode(search.Code);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeactivateAsync(
            Discount? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot Deactivate a null record");
            }
            if (!entity.Active)
            {
                return CEFAR.PassingCEFAR();
            }
            var timestamp = DateExtensions.GenDateTime;
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedPurchaseOrderDiscount>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedPurchaseOrderItemDiscount>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesOrderDiscount>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesOrderItemDiscount>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesInvoiceDiscount>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesInvoiceItemDiscount>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesQuoteDiscount>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesQuoteItemDiscount>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesReturnDiscount>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesReturnItemDiscount>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSampleRequestDiscount>(entity.ID, context).ConfigureAwait(false);
            await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSampleRequestItemDiscount>(entity.ID, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountAccount>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountAccountType>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountBrand>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountCategory>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountCountry>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountManufacturer>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountProduct>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountProductType>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountShipCarrierMethod>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountStore>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountVendor>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsMasterObjectsAsync<DiscountUser>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<AppliedCartDiscount>(entity.ID, timestamp, context).ConfigureAwait(false);
            await DeactivateAssociatedAsSlaveObjectsAsync<AppliedCartItemDiscount>(entity.ID, timestamp, context).ConfigureAwait(false);
            var e = await context.Discounts.FilterByID(entity.ID).SingleAsync().ConfigureAwait(false);
            foreach (var code in e.Codes!)
            {
                if (!code.Active)
                {
                    continue;
                }
                code.UpdatedDate = timestamp;
                code.Active = false;
            }
            foreach (var role in e.UserRoles!)
            {
                if (!role.Active)
                {
                    continue;
                }
                role.UpdatedDate = timestamp;
                role.Active = false;
            }
            e.UpdatedDate = timestamp;
            e.Active = false;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Could not save Deactivate record");
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> ReactivateAsync(
            Discount? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot Reactivate a null record");
            }
            if (entity.Active)
            {
                return CEFAR.PassingCEFAR();
            }
            var timestamp = DateExtensions.GenDateTime;
            var e = await context.Discounts.FilterByID(entity.ID).SingleAsync();
            e.UpdatedDate = timestamp;
            e.Active = true;
            foreach (var code in e.Codes!)
            {
                if (code.Active)
                {
                    continue;
                }
                code.UpdatedDate = timestamp;
                code.Active = true;
            }
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Could not save Reactivate record");
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Discount? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            try
            {
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedPurchaseOrderDiscount>(entity.ID, context).ConfigureAwait(false);
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedPurchaseOrderItemDiscount>(entity.ID, context).ConfigureAwait(false);
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesOrderDiscount>(entity.ID, context).ConfigureAwait(false);
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesOrderItemDiscount>(entity.ID, context).ConfigureAwait(false);
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesInvoiceDiscount>(entity.ID, context).ConfigureAwait(false);
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesInvoiceItemDiscount>(entity.ID, context).ConfigureAwait(false);
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesQuoteDiscount>(entity.ID, context).ConfigureAwait(false);
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesQuoteItemDiscount>(entity.ID, context).ConfigureAwait(false);
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesReturnDiscount>(entity.ID, context).ConfigureAwait(false);
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSalesReturnItemDiscount>(entity.ID, context).ConfigureAwait(false);
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSampleRequestDiscount>(entity.ID, context).ConfigureAwait(false);
                await ThrowIfAssociatedAsSlaveObjectAsync<AppliedSampleRequestItemDiscount>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountAccount>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountAccountType>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountBrand>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountCategory>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountCountry>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountManufacturer>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountProduct>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountProductType>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountShipCarrierMethod>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountStore>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountVendor>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsMasterObjectsAsync<DiscountUser>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsSlaveObjectsAsync<AppliedCartDiscount>(entity.ID, context).ConfigureAwait(false);
                await DeleteAssociatedAsSlaveObjectsAsync<AppliedCartItemDiscount>(entity.ID, context).ConfigureAwait(false);
                context.Set<Discount>().Remove(entity);
                return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                    .BoolToCEFAR("ERROR! Failed to save Delete");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
            {
                // Updated/Deleted elsewhere
                return CEFAR.PassingCEFAR();
            }
            catch (OptimisticConcurrencyException)
            {
                // Updated/Deleted elsewhere
                return CEFAR.PassingCEFAR();
            }
        }

        /// <summary>Ensures that map out hooks.</summary>
        private static void EnsureMapOutHooks()
        {
            if (ModelMapperForDiscount.CreateDiscountModelFromEntityHooksList != null)
            {
                return;
            }
            ModelMapperForDiscount.CreateDiscountModelFromEntityHooksList = (entity, model, contextProfileName) =>
            {
                model.DiscountTypeID = entity.DiscountTypeID;
                if (Contract.CheckEmpty(model.Codes))
                {
                    model.Codes = (entity is ModelMapperForDiscount.AnonDiscount discount ? discount.Codes : entity.Codes)
                        ?.Where(y => y.Active)
                        .Select(y => ModelMapperForDiscountCode.CreateDiscountCodeModelFromEntityList(y, contextProfileName))
                        .ToList()!;
                }
                return model;
            };
        }
    }
}
