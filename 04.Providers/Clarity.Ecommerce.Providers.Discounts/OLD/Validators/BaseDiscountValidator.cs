// <copyright file="BaseDiscountValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base discount validator class</summary>
namespace Clarity.Ecommerce.Providers.Discounts
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;

    /// <summary>A base discount validator.</summary>
    /// <seealso cref="IDiscountValidator"/>
    public abstract class BaseDiscountValidator : IDiscountValidator
    {
        /// <inheritdoc/>
        [Pure]
        public virtual async Task<CEFActionResponse<List<IAppliedDiscountBaseModel>>> ValidateDiscountAsync(
            bool isAdd,
            int userID,
            int cartID,
            decimal? subtotal,
            IDiscount discount,
            List<IAppliedDiscountBaseModel> currentDiscounts,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> cartItems,
            List<IRateQuoteModel> rateQuotes,
            IClarityEcommerceEntities context)
        {
            if (!discount.Active)
            {
                return CEFAR.FailingCEFAR<List<IAppliedDiscountBaseModel>>("Invalid Discount code");
            }
            if (isAdd && currentDiscounts.Any(x => x.SlaveID == discount.ID))
            {
                return CEFAR.FailingCEFAR<List<IAppliedDiscountBaseModel>>("Discount has already been added");
            }
            if (discount.StartDate.HasValue && discount.StartDate.Value > DateExtensions.GenDateTime)
            {
                return CEFAR.FailingCEFAR<List<IAppliedDiscountBaseModel>>("Discount hasn't started yet");
            }
            if (discount.EndDate.HasValue && discount.EndDate.Value < DateExtensions.GenDateTime)
            {
                return CEFAR.FailingCEFAR<List<IAppliedDiscountBaseModel>>("Discount ended");
            }
            if (!discount.CanCombine && currentDiscounts.Any(x => x.DiscountID != discount.ID)
                || discount.CanCombine && currentDiscounts.Any(x => !x.DiscountCanCombine))
            {
                return CEFAR.FailingCEFAR<List<IAppliedDiscountBaseModel>>(
                    "The current discounts can not be combined with other discounts");
            }
            if (discount.UsageLimit <= 0)
            {
                return CEFAR.PassingCEFAR<List<IAppliedDiscountBaseModel>>(null);
            }
            return (await CountDiscountUsagesAsync(
                        isAdd,
                        userID,
                        cartID,
                        discount.UsageLimitByUser,
                        discount.UsageLimitByCart,
                        discount.ID,
                        discount.DiscountTypeID,
                        context)
                    .ConfigureAwait(false) < discount.UsageLimit)
                .BoolToCEFAR(
                    currentDiscounts,
                    "This discount has been applied the maximum number of times");
        }

        /// <summary>Validates the amount threshold.</summary>
        /// <param name="compareAmount">  The compare amount.</param>
        /// <param name="compareOperator">The compare operator.</param>
        /// <param name="thresholdAmount">The threshold amount.</param>
        /// <param name="discountType">   Type of the discount.</param>
        /// <returns>A list of.</returns>
        [Pure]
        public CEFActionResponse<List<IAppliedDiscountBaseModel>> ValidateAmountThreshold(
            decimal compareAmount,
            int? compareOperator,
            decimal thresholdAmount,
            int discountType)
        {
            return ValidateAmountThreshold(
                compareAmount,
                compareOperator,
                thresholdAmount,
                (Enums.DiscountType)discountType);
        }

        /// <summary>Validates the amount threshold described by compareAmount.</summary>
        /// <param name="compareAmount">  The compare amount.</param>
        /// <param name="compareOperator">The compare operator.</param>
        /// <param name="thresholdAmount">The threshold amount.</param>
        /// <param name="discountType">   Type of the discount.</param>
        /// <returns>A CEFActionResponse.</returns>
        [Pure]
        public CEFActionResponse<List<IAppliedDiscountBaseModel>> ValidateAmountThreshold(
            decimal compareAmount,
            int? compareOperator,
            decimal thresholdAmount,
            Enums.DiscountType discountType)
        {
            if (!compareOperator.HasValue)
            {
                return CEFAR.PassingCEFAR<List<IAppliedDiscountBaseModel>>(null);
            }
            switch ((Enums.CompareOperator)compareOperator.Value)
            {
                case Enums.CompareOperator.LessThan:
                {
                    return (compareAmount < thresholdAmount).BoolToCEFAR<List<IAppliedDiscountBaseModel>>(
                        null,
                        $"{discountType} amount should be less than {thresholdAmount:c} (actual: {compareAmount:c})");
                }
                case Enums.CompareOperator.LessThanOrEqualTo:
                {
                    return (compareAmount <= thresholdAmount).BoolToCEFAR<List<IAppliedDiscountBaseModel>>(
                        null,
                        $"{discountType} amount should be less or equal to {thresholdAmount:c} (actual: {compareAmount:c})");
                }
                case Enums.CompareOperator.GreaterThan:
                {
                    return (compareAmount > thresholdAmount).BoolToCEFAR<List<IAppliedDiscountBaseModel>>(
                        null,
                        $"{discountType} amount should be greater than {thresholdAmount:c} (actual: {compareAmount:c})");
                }
                case Enums.CompareOperator.GreaterThanOrEqualTo:
                {
                    return (compareAmount >= thresholdAmount).BoolToCEFAR<List<IAppliedDiscountBaseModel>>(
                        null,
                        $"{discountType} amount should be greater or equal to {thresholdAmount:c} (actual: {compareAmount:c})");
                }
                case Enums.CompareOperator.Undefined:
                {
                    return CEFAR.PassingCEFAR<List<IAppliedDiscountBaseModel>>(null);
                }
                default:
                {
                    return CEFAR.FailingCEFAR<List<IAppliedDiscountBaseModel>>("Invalid comparison operator for discount");
                }
            }
        }

        /// <summary>Sets discount total.</summary>
        /// <param name="isAdd">           True if this is add.</param>
        /// <param name="subtotal">        The subtotal.</param>
        /// <param name="discount">        The discount.</param>
        /// <param name="cartID">          Identifier for the cart.</param>
        /// <param name="currentDiscounts">The current discounts.</param>
        /// <param name="cartItems">       The cart items.</param>
        /// <param name="rateQuotes">      The rate quotes.</param>
        /// <param name="context">         The context.</param>
        /// <returns>A Task{(bool added, List{IAppliedDiscountBaseModel} currentDiscounts)}.</returns>
        protected abstract Task<(bool added, List<IAppliedDiscountBaseModel> currentDiscounts)> SetDiscountTotalAsync(
            bool isAdd,
            decimal subtotal,
            IDiscount discount,
            int cartID,
            List<IAppliedDiscountBaseModel> currentDiscounts,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> cartItems,
            List<IRateQuoteModel> rateQuotes,
            IClarityEcommerceEntities context);

        /// <summary>Deactivate the discount.</summary>
        /// <typeparam name="TAppliedDiscount">Type of the applied discount.</typeparam>
        /// <typeparam name="T">               Generic type parameter.</typeparam>
        /// <param name="masterID">  Identifier for the master.</param>
        /// <param name="discountID">Identifier for the discount.</param>
        /// <param name="context">   The context.</param>
        /// <returns>A Task.</returns>
        protected async Task DeactivateAsync<TAppliedDiscount, T>(
                int masterID,
                int discountID,
                IClarityEcommerceEntities context)
            where TAppliedDiscount : class, IAppliedDiscountBase<T>
            where T : IBase
        {
            var entity = await context.Set<TAppliedDiscount>()
                .SingleOrDefaultAsync(x => x.MasterID == masterID && x.SlaveID == discountID)
                .ConfigureAwait(false);
            if (entity == null)
            {
                return;
            }
            entity.Active = false;
            entity.UpdatedDate = DateExtensions.GenDateTime;
        }

        /// <summary>Creates or update.</summary>
        /// <typeparam name="TAppliedDiscount">Type of the applied discount.</typeparam>
        /// <typeparam name="T">               Generic type parameter.</typeparam>
        /// <param name="amount">    The amount.</param>
        /// <param name="masterID">  Identifier for the master.</param>
        /// <param name="discountID">Identifier for the discount.</param>
        /// <param name="context">   The context.</param>
        /// <returns>The new or update.</returns>
        protected async Task<TAppliedDiscount> CreateOrUpdateAsync<TAppliedDiscount, T>(
                decimal amount,
                int masterID,
                int discountID,
                IClarityEcommerceEntities context)
            where TAppliedDiscount : class, IAppliedDiscountBase<T>, new()
            where T : IBase
        {
            var entity = await context.Set<TAppliedDiscount>()
                .SingleOrDefaultAsync(x => x.MasterID == masterID && x.SlaveID == discountID)
                .ConfigureAwait(false);
            if (entity != null)
            {
                entity.Active = true;
                entity.UpdatedDate = DateExtensions.GenDateTime;
                entity.DiscountTotal = amount * -1;
            }
            else
            {
                entity = new TAppliedDiscount
                {
                    Active = true,
                    CreatedDate = DateExtensions.GenDateTime,
                    MasterID = masterID,
                    SlaveID = discountID,
                    DiscountTotal = amount * -1,
                };
                context.Set<TAppliedDiscount>().Add(entity);
            }
            return entity;
        }

        /// <summary>Count discount usages.</summary>
        /// <param name="isAdd">           True if this BaseDiscountValidator is trying to add.</param>
        /// <param name="userID">          Identifier for the user.</param>
        /// <param name="cartID">          Identifier for the cart.</param>
        /// <param name="usageLimitByUser">True to usage limit by user.</param>
        /// <param name="usageLimitByCart">True to usage limit by cart.</param>
        /// <param name="discountID">      Identifier for the discount.</param>
        /// <param name="discountType">    Type of the discount.</param>
        /// <param name="context">         The context.</param>
        /// <returns>The total number of discount usages.</returns>
        [Pure]
        private Task<int> CountDiscountUsagesAsync(
            bool isAdd,
            int userID,
            int cartID,
            bool usageLimitByUser,
            bool usageLimitByCart,
            int discountID,
            int discountType,
            IClarityEcommerceEntities context)
        {
            return isAdd
                ? CountUsagesOfThisDiscountInnerAsync<Cart, AppliedCartDiscount, CartItem, AppliedCartItemDiscount, CartItemTarget>(
                    usageLimitByUser ? userID : (int?)null,
                    usageLimitByCart ? cartID : (int?)null,
                    DateExtensions.GenDateTime.AddHours(-4),
                    discountID,
                    discountType,
                    context)
                : CountUsagesOfThisDiscountInnerAsync<SalesOrder, AppliedSalesOrderDiscount, SalesOrderItem, AppliedSalesOrderItemDiscount, SalesOrderItemTarget>(
                    usageLimitByUser ? userID : (int?)null,
                    null,
                    null,
                    discountID,
                    discountType,
                    context);
        }

        /// <summary>Count usages of this discount.</summary>
        /// <typeparam name="TCollection">        Type of the sales collection.</typeparam>
        /// <typeparam name="TCollectionDiscount">Type of the applied sales collection discount.</typeparam>
        /// <typeparam name="TItem">              Type of the sales item.</typeparam>
        /// <typeparam name="TItemDiscount">      Type of the applied sales item discount.</typeparam>
        /// <typeparam name="TItemTarget">        Type of the sales item target.</typeparam>
        /// <param name="userID">      Identifier for the user.</param>
        /// <param name="cartID">      Identifier for the cart.</param>
        /// <param name="minimum">     The minimum.</param>
        /// <param name="discountID">  Identifier for the discount.</param>
        /// <param name="discountType">Type of the discount.</param>
        /// <param name="context">     The context.</param>
        /// <returns>The total number of usages of this discount.</returns>
        [Pure]
        private Task<int> CountUsagesOfThisDiscountInnerAsync<TCollection, TCollectionDiscount, TItem, TItemDiscount, TItemTarget>(
                int? userID,
                int? cartID,
                DateTime? minimum,
                int discountID,
                int discountType,
                IClarityEcommerceEntities context)
            where TCollection : class, ISalesCollectionBase
            where TCollectionDiscount : class, IAppliedDiscountBase<TCollection>
            where TItem : class, ISalesItemBase<TItemDiscount, TItemTarget>
            where TItemDiscount : class, IAppliedDiscountBase<TItem>
            where TItemTarget : class, ISalesItemTargetBase
        {
            switch (discountType)
            {
                case (int)Enums.DiscountType.Product:
                case (int)Enums.DiscountType.BuyXGetY:
                {
                    return context.Set<TItemDiscount>()
                        .CountAsync(x => x.Active
                            && x.SlaveID == discountID
                            && (!minimum.HasValue || x.UpdatedDate >= minimum.Value || x.CreatedDate >= minimum.Value)
                            && (!userID.HasValue || userID == 0 || x.Master.Master.UserID == userID.Value)
                            && (!cartID.HasValue || cartID == 0 || x.Master.MasterID == cartID.Value));
                }
                case (int)Enums.DiscountType.Order:
                case (int)Enums.DiscountType.Shipping:
                {
                    return context.Set<TCollectionDiscount>()
                        .CountAsync(x => x.SlaveID == discountID
                            && x.Active
                            && (!minimum.HasValue || x.UpdatedDate >= minimum.Value || x.CreatedDate >= minimum.Value)
                            && (!userID.HasValue || userID == 0 || x.Master.UserID == userID.Value)
                            && (!cartID.HasValue || cartID == 0 || x.MasterID == cartID.Value));
                }
                default: // NOTE: This should never happen
                {
                    throw new ArgumentException("Unknown Discount Type");
                }
            }
        }
    }
}
