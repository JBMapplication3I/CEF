// <copyright file="DiscountManager.Removals.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount manager class</summary>
namespace Clarity.Ecommerce.Providers.Discounts
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using MoreLinq;
    using Utilities;

    /// <summary>Manager for discounts.</summary>
    public partial class DiscountManager
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> RemoveDiscountByAppliedCartDiscountIDAsync(ICartModel cart, int appliedID, string? contextProfileName)
        {
            Contract.RequiresNotNull(cart);
            Contract.RequiresValidID(appliedID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var discountID = await context.AppliedCartDiscounts
                .AsNoTracking()
                .FilterByID(appliedID)
                .Select(x => x.SlaveID)
                .SingleAsync()
                .ConfigureAwait(false);
            foreach (var discount in cart.Discounts!.Where(x => x.SlaveID == discountID))
            {
                await DeactivateDiscountForCartAndSaveAsync(discount, contextProfileName).ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> RemoveDiscountByAppliedCartItemDiscountIDAsync(ICartModel cart, int appliedID, string? contextProfileName)
        {
            Contract.RequiresNotNull(cart);
            Contract.RequiresValidID(appliedID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var discountID = await context.AppliedCartItemDiscounts
                .AsNoTracking()
                .FilterByID(appliedID)
                .Select(x => x.SlaveID)
                .SingleAsync()
                .ConfigureAwait(false);
            foreach (var discount in cart.SalesItems!.Where(x => x.Active).SelectMany(x => x.Discounts!).Where(x => x.SlaveID == discountID))
            {
                await DeactivateDiscountForCartAndSaveAsync(discount, contextProfileName).ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Deactivate all discounts for cart.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task DeactivateAllDiscountsForCartAndSaveAsync(
            ICartModel cart,
            string? contextProfileName)
        {
            foreach (var discount in cart.Discounts!)
            {
                await DeactivateDiscountForCartAndSaveAsync(discount, contextProfileName).ConfigureAwait(false);
            }
            foreach (var discount in cart.SalesItems!.Where(x => x.Active).SelectMany(x => x.Discounts!))
            {
                await DeactivateDiscountForCartAndSaveAsync(discount, contextProfileName).ConfigureAwait(false);
            }
        }

        /// <summary>Deactivate discount for cart.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="discountID">        Identifier for the discount.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task DeactivateDiscountForCartAndSaveAsync(
            ICartModel cart,
            int discountID,
            string? contextProfileName)
        {
            foreach (var discount in cart.Discounts!.Where(x => x.SlaveID == discountID))
            {
                await DeactivateDiscountForCartAndSaveAsync(discount, contextProfileName).ConfigureAwait(false);
            }
            foreach (var discount in cart.SalesItems!.Where(x => x.Active).SelectMany(x => x.Discounts!).Where(x => x.SlaveID == discountID))
            {
                await DeactivateDiscountForCartAndSaveAsync(discount, contextProfileName).ConfigureAwait(false);
            }
        }

        /// <summary>Deactivate discount by kind for cart.</summary>
        /// <param name="discount">          The discount.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task DeactivateDiscountForCartAndSaveAsync(
            IAppliedDiscountBaseModel discount,
            string? contextProfileName)
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Contract.RequiresValidID(discount.ID);
            switch (discount.DiscountTypeID)
            {
                case (int)Enums.DiscountType.Shipping:
                case (int)Enums.DiscountType.Order:
                {
                    await RemoveDiscountAndSaveAsync<AppliedCartDiscount>(discount.ID, contextProfileName).ConfigureAwait(false);
                    break;
                }
                case (int)Enums.DiscountType.BuyXGetY:
                case (int)Enums.DiscountType.Product:
                {
                    await RemoveDiscountAndSaveAsync<AppliedCartItemDiscount>(discount.ID, contextProfileName).ConfigureAwait(false);
                    break;
                }
                default:
                {
                    throw new ArgumentException("Unknown discount type");
                }
            }
        }

        /// <summary>Removes the discount.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="appliedDiscountID"> Identifier for the applied discount.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        private static async Task RemoveDiscountAndSaveAsync<T>(
                int appliedDiscountID,
                string? contextProfileName)
            where T : class, IBase
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.Set<T>()
                .FilterByID(appliedDiscountID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (entity is not { Active: true })
            {
                return;
            }
            entity.Active = false;
            entity.UpdatedDate = DateExtensions.GenDateTime;
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
        }

        /// <summary>Gets applied discount entity.</summary>
        /// <typeparam name="TMaster">         Type of the master entity for the relationship (the slave is always the
        ///                                    discount).</typeparam>
        /// <typeparam name="TAppliedDiscount">Type of the applied discount entity.</typeparam>
        /// <param name="masterID">  Identifier for the master.</param>
        /// <param name="discountID">Identifier for the discount.</param>
        /// <param name="context">   The context.</param>
        /// <returns>The applied discount entity.</returns>
        [Pure]
        private static Task<TAppliedDiscount> GetAppliedDiscountEntityAsync<TMaster, TAppliedDiscount>(
                int masterID,
                int discountID,
                IDbContext context)
            where TMaster : class, IHaveAppliedDiscountsBase<TMaster, TAppliedDiscount>
            where TAppliedDiscount : class, IAppliedDiscountBase<TMaster, TAppliedDiscount>
        {
            return context.Set<TAppliedDiscount>()
                .FilterByActive(true)
                .FilterIAmARelationshipTableByMasterID<TAppliedDiscount, TMaster, Discount>(Contract.RequiresValidID(masterID))
                .FilterIAmARelationshipTableBySlaveID<TAppliedDiscount, TMaster, Discount>(Contract.RequiresValidID(discountID))
                .SingleOrDefaultAsync(x => x.MasterID == masterID && x.SlaveID == discountID);
        }

        /// <summary>Deactivate save and remove from model list.</summary>
        /// <typeparam name="TMasterModel"> Type of the master model.</typeparam>
        /// <typeparam name="TAppliedModel">Type of the applied model.</typeparam>
        /// <typeparam name="TMaster">      Type of the master.</typeparam>
        /// <typeparam name="TApplied">     Type of the applied.</typeparam>
        /// <param name="master">    The master.</param>
        /// <param name="discountID">Identifier for the discount.</param>
        /// <param name="context">   The context.</param>
        /// <returns>A Task.</returns>
        private static async Task DeactivateSaveAndRemoveFromModelListAsync<TMasterModel, TAppliedModel, TMaster, TApplied>(
                TMasterModel master,
                int discountID,
                IClarityEcommerceEntities context)
            where TMasterModel : IHaveAppliedDiscountsBaseModel<TAppliedModel>
            where TAppliedModel : IAppliedDiscountBaseModel
            where TApplied : class, IAppliedDiscountBase<TMaster, TApplied>
            where TMaster : class, IHaveAppliedDiscountsBase<TMaster, TApplied>
        {
            var entity = await GetAppliedDiscountEntityAsync<TMaster, TApplied>(
                    Contract.RequiresValidID(master.ID),
                    discountID,
                    context)
                .ConfigureAwait(false);
            if (entity == null || entity.Active == false)
            {
                // It either doesn't exist or is already deactivated
                master.Discounts = master.Discounts!.Where(x => x.DiscountID != discountID).ToList();
                return;
            }
            entity.Active = false;
            entity.UpdatedDate = DateExtensions.GenDateTime;
            master.Discounts = master.Discounts!.Where(x => x.DiscountID != discountID).ToList();
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
        }

        /// <summary>Upsert save and append entity to model list.</summary>
        /// <typeparam name="TMasterModel">  Type of the master model.</typeparam>
        /// <typeparam name="TIAppliedModel">Type of the applied model's interface.</typeparam>
        /// <typeparam name="TAppliedModel"> Type of the applied model.</typeparam>
        /// <typeparam name="TMaster">       Type of the master.</typeparam>
        /// <typeparam name="TApplied">      Type of the applied.</typeparam>
        /// <param name="amount">        The amount.</param>
        /// <param name="consumed">      The consumed.</param>
        /// <param name="master">        The master.</param>
        /// <param name="discountID">    Identifier for the discount.</param>
        /// <param name="context">       The context.</param>
        /// <param name="overrideItemID">Identifier for the override item.</param>
        /// <returns>A Task.</returns>
        private static async Task UpsertSaveAndAppendEntityToModelListAsync<TMasterModel, TIAppliedModel, TAppliedModel, TMaster, TApplied>(
                decimal amount,
                int consumed,
                TMasterModel master,
                int discountID,
                IClarityEcommerceEntities context,
                int? overrideItemID = null)
            where TMasterModel : IHaveAppliedDiscountsBaseModel<TIAppliedModel>
            where TIAppliedModel : IAppliedDiscountBaseModel
            where TAppliedModel : TIAppliedModel, new()
            where TMaster : class, IHaveAppliedDiscountsBase<TMaster, TApplied>
            where TApplied : class, IAppliedDiscountBase<TMaster, TApplied>, new()
        {
            var entity = await GetAppliedDiscountEntityAsync<TMaster, TApplied>(
                    overrideItemID ?? Contract.RequiresValidID(master.ID),
                    discountID,
                    context)
                .ConfigureAwait(false);
            if (entity != null && entity.DiscountTotal == amount * -1m && entity.ApplicationsUsed == consumed)
            {
                // Do Nothing
            }
            else if (entity != null)
            {
                entity.UpdatedDate = DateExtensions.GenDateTime;
                entity.DiscountTotal = amount * -1m;
                entity.ApplicationsUsed = Math.Max(1, consumed);
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            else
            {
                entity = RegistryLoaderWrapper.GetInstance<TApplied>(context.ContextProfileName);
                // Base Properties
                entity.Active = true;
                entity.CreatedDate = DateExtensions.GenDateTime;
                // Applied Discount Properties
                entity.DiscountTotal = amount * -1m;
                entity.ApplicationsUsed = Math.Max(1, consumed);
                // Related Objects
                entity.MasterID = overrideItemID ?? Contract.RequiresValidID(master.ID);
                entity.SlaveID = discountID;
                context.Set<TApplied>().Add(entity);
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            master.Discounts = master.Discounts!
                .Where(x => x.Active)
                .OrderBy(x => x.ID)
                .DistinctBy(x => x.DiscountID)
                .ToList();
            var existingModel = master.Discounts.SingleOrDefault(x => x.DiscountID == entity.SlaveID);
            if (existingModel != null)
            {
                // Just updating the total
                if (existingModel.DiscountTotal == entity.DiscountTotal
                    && existingModel.ApplicationsUsed == entity.ApplicationsUsed)
                {
                    return;
                }
                existingModel.DiscountTotal = entity.DiscountTotal;
                existingModel.ApplicationsUsed = entity.ApplicationsUsed;
                existingModel.CreatedDate = entity.CreatedDate;
                existingModel.UpdatedDate = entity.UpdatedDate;
                return;
            }
            var updatedDiscounts = master.Discounts;
            var newAppliedModel = (await context.Set<TApplied>()
                    .AsNoTracking()
                    .FilterByID(entity.ID)
                    .Select(x => new
                    {
                        // Base Properties
                        x.ID,
                        x.CustomKey,
                        x.CreatedDate,
                        x.Active,
                        x.JsonAttributes,
                        // Applied Discount Properties
                        x.DiscountTotal,
                        x.ApplicationsUsed,
                        // Related Objects
                        x.MasterID,
                        x.SlaveID,
                        SlaveKey = x.Slave!.CustomKey,
                        SlaveName = x.Slave.Name,
                        // Used by UI
                        x.Slave.DiscountTypeID,
                        DiscountCanCombine = x.Slave.CanCombine,
                        DiscountValue = x.Slave.Value,
                        DiscountValueType = x.Slave.ValueType,
                        DiscountIsAutoApplied = x.Slave.IsAutoApplied,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new TAppliedModel
                {
                    // Base Properties
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    CreatedDate = x.CreatedDate,
                    Active = x.Active,
                    SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                    // Applied Discount Properties
                    DiscountTotal = x.DiscountTotal,
                    ApplicationsUsed = x.ApplicationsUsed ?? 1,
                    // Related Objects
                    MasterID = x.MasterID,
                    SlaveID = x.SlaveID,
                    SlaveKey = x.SlaveKey,
                    SlaveName = x.SlaveName,
                    // Used by UI
                    DiscountTypeID = x.DiscountTypeID,
                    DiscountCanCombine = x.DiscountCanCombine,
                    DiscountValue = x.DiscountValue,
                    DiscountValueType = x.DiscountValueType,
                    DiscountIsAutoApplied = x.DiscountIsAutoApplied,
                })
                .Single();
            updatedDiscounts.Add(newAppliedModel);
            master.Discounts = updatedDiscounts;
        }
    }
}
