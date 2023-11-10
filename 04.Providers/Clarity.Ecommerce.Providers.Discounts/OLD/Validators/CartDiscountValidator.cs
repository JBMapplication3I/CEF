// <copyright file="CartDiscountValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart discount validator class</summary>
namespace Clarity.Ecommerce.Providers.Discounts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;

    /// <summary>A cart discount validator.</summary>
    /// <seealso cref="BaseDiscountValidator"/>
    public class CartDiscountValidator : BaseDiscountValidator
    {
        /// <inheritdoc/>
        public override async Task<CEFActionResponse<List<IAppliedDiscountBaseModel>>> ValidateDiscountAsync(
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
            if (cartItems.Count <= 0)
            {
                await DeactivateAsync<AppliedCartDiscount, Cart>(
                        cartID,
                        discount.ID,
                        context)
                    .ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return CEFAR.FailingCEFAR<List<IAppliedDiscountBaseModel>>(
                    "No Items in Cart",
                    "Discount Removed");
            }
            var response = await base.ValidateDiscountAsync(
                    isAdd: isAdd,
                    userID: userID,
                    cartID: cartID,
                    subtotal: subtotal,
                    discount: discount,
                    currentDiscounts: currentDiscounts,
                    cartItems: cartItems,
                    rateQuotes: rateQuotes,
                    context: context)
                .ConfigureAwait(false);
            if (response.ActionSucceeded)
            {
                response = ValidateAmountThreshold(
                    compareAmount: subtotal ?? 0m,
                    compareOperator: discount.DiscountCompareOperator,
                    thresholdAmount: discount.ThresholdAmount,
                    discountType: discount.DiscountTypeID);
            }
            if (response.ActionSucceeded)
            {
                var (_, appliedDiscountBaseModels) = await SetDiscountTotalAsync(
                        isAdd: isAdd,
                        subtotal: subtotal ?? 0m,
                        discount: discount,
                        cartID: cartID,
                        currentDiscounts: currentDiscounts,
                        cartItems: cartItems,
                        rateQuotes: rateQuotes,
                        context: context)
                    .ConfigureAwait(false);
                response.Result = appliedDiscountBaseModels;
                ////response.Messages.Add(added ? "Discount added" : "Not Applied");
                return response;
            }
            if (isAdd)
            {
                return response;
            }
            await DeactivateAsync<AppliedCartDiscount, Cart>(cartID, discount.ID, context).ConfigureAwait(false);
            await context.SaveChangesAsync().ConfigureAwait(false);
            response.Messages.Add("Discount Removed");
            return response;
        }

        /// <inheritdoc/>
        protected override async Task<(bool added, List<IAppliedDiscountBaseModel> currentDiscounts)> SetDiscountTotalAsync(
            bool isAdd,
            decimal subtotal,
            IDiscount discount,
            int cartID,
            List<IAppliedDiscountBaseModel> currentDiscounts,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> cartItems,
            List<IRateQuoteModel> rateQuotes,
            IClarityEcommerceEntities context)
        {
            var amount = GetAmount(discount.Value, subtotal, discount.ValueType);
            if (amount == 0m)
            {
                await DeactivateAsync<AppliedCartDiscount, Cart>(
                        cartID,
                        discount.ID,
                        context)
                    .ConfigureAwait(false);
                var updatedDiscounts = new List<IAppliedDiscountBaseModel>(
                    currentDiscounts.Where(x => x.DiscountID != discount.ID));
                await context.SaveChangesAsync().ConfigureAwait(false);
                return (false, updatedDiscounts);
            }
            var entity = await CreateOrUpdateAsync<AppliedCartDiscount, Cart>(
                    amount: amount,
                    masterID: cartID,
                    discountID: discount.ID,
                    context: context)
                .ConfigureAwait(false);
            var existing = currentDiscounts.SingleOrDefault(x => x.DiscountID == discount.ID);
            if (isAdd || existing == null)
            {
                var updatedDiscounts = new List<IAppliedDiscountBaseModel>(currentDiscounts)
                {
                    ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityFull(entity),
                };
                await context.SaveChangesAsync().ConfigureAwait(false);
                return (true, updatedDiscounts);
            }
            existing.DiscountTotal = amount * -1;
            await context.SaveChangesAsync().ConfigureAwait(false);
            return (true, currentDiscounts);
        }

        /// <summary>Gets an adjusted amount from the original.</summary>
        /// <param name="original"> The original amount.</param>
        /// <param name="value">    The value.</param>
        /// <param name="valueType">Type of the value.</param>
        /// <returns>The adjusted amount.</returns>
        private decimal GetAmount(
            decimal? original,
            decimal value,
            int valueType)
        {
            original ??= 0m;
            var amount = 0m;
            amount += valueType switch
            {
                (int)Enums.DiscountValueType.Amount => Math.Min(original.Value, value),
                (int)Enums.DiscountValueType.Percent => value / 100 * original.Value,
                _ => throw new ArgumentException("Invalid Discount Value Type"),
            };
            return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        }
    }
}
