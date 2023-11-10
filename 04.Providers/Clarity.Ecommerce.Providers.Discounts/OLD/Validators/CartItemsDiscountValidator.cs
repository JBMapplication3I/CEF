// <copyright file="CartItemsDiscountValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart items discount validator class</summary>
// ReSharper disable PossibleInvalidOperationException
namespace Clarity.Ecommerce.Providers.Discounts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Mapper;
    using Models;

    /// <summary>A cart items discount validator.</summary>
    /// <seealso cref="BaseDiscountValidator"/>
    public class CartItemsDiscountValidator : BaseDiscountValidator
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
            var response = await base.ValidateDiscountAsync(
                    isAdd,
                    userID,
                    cartID,
                    subtotal,
                    discount,
                    currentDiscounts,
                    cartItems,
                    rateQuotes,
                    context)
                .ConfigureAwait(false);
            if (response.ActionSucceeded)
            {
                response = ValidateAmountThreshold(
                    cartItems.Sum(x => (x.UnitSoldPrice ?? x.UnitCorePrice) * x.TotalQuantity),
                    discount.DiscountCompareOperator,
                    discount.ThresholdAmount,
                    discount.DiscountTypeID);
            }
            if (response.ActionSucceeded)
            {
                var (_, appliedDiscountBaseModels) = await SetDiscountTotalAsync(
                        isAdd,
                        subtotal ?? 0m,
                        discount,
                        cartID,
                        currentDiscounts,
                        cartItems,
                        rateQuotes,
                        context)
                    .ConfigureAwait(false);
                response.Result = appliedDiscountBaseModels;
                ////response.Messages.Add(added ? "Discount added" : "Not Applied");
                return response;
            }
            if (isAdd)
            {
                return response;
            }
            foreach (var cartItem in cartItems)
            {
                await DeactivateAsync<AppliedCartItemDiscount, CartItem>(
                        masterID: cartItem.ID.Value,
                        discountID: discount.ID,
                        context: context)
                    .ConfigureAwait(false);
            }
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
            var applied = false;
            var deactivated = false;
            foreach (var cartItem in cartItems)
            {
                var amount = GetAmount(
                    discount.Value,
                    discount.ID,
                    (Enums.DiscountValueType)discount.ValueType,
                    cartItem);
                if (amount == 0m)
                {
                    await DeactivateAsync<AppliedCartItemDiscount, CartItem>(
                            masterID: cartItem.ID.Value,
                            discountID: discount.ID,
                            context: context)
                        .ConfigureAwait(false);
                    cartItem.Discounts = new List<IAppliedCartItemDiscountModel>(
                        cartItem.Discounts.Where(x => x.DiscountID != discount.ID));
                    deactivated = true;
                    continue;
                }
                var discount2 = await CreateOrUpdateAsync<AppliedCartItemDiscount, CartItem>(
                        amount: amount,
                        masterID: cartItem.ID.Value,
                        discountID: discount.ID,
                        context)
                    .ConfigureAwait(false);
                if (isAdd)
                {
                    cartItem.Discounts = new List<IAppliedCartItemDiscountModel>(cartItem.Discounts)
                    {
                        ModelMapperForAppliedCartItemDiscount.MapAppliedCartItemDiscountModelFromEntityFull(discount2),
                    };
                    applied = true;
                    continue;
                }
                var existing = cartItem.Discounts.SingleOrDefault(x => x.DiscountID == discount.ID);
                if (existing == null)
                {
                    // Invalid state, do nothing
                }
                else
                {
                    existing.DiscountTotal = amount * -1;
                    applied = true;
                }
            }
            if (applied || deactivated)
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            return (applied, currentDiscounts);
        }

        /// <summary>Gets an amount.</summary>
        /// <param name="value">            The value.</param>
        /// <param name="discountID">       Identifier for the discount.</param>
        /// <param name="discountValueType">Type of the discount value.</param>
        /// <param name="cartItem">         The cart item.</param>
        /// <returns>The amount.</returns>
        [Pure]
        private decimal GetAmount(
            decimal value,
            int discountID,
            Enums.DiscountValueType discountValueType,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> cartItem)
        {
            var amount = 0m;
            var extendedPrice = (cartItem.UnitSoldPrice ?? cartItem.UnitCorePrice) * cartItem.TotalQuantity;
            switch (discountValueType)
            {
                case Enums.DiscountValueType.Amount:
                {
                    amount += Math.Min(
                        extendedPrice
                        + cartItem.Discounts
                            .Where(x => x.DiscountID != discountID)
                            .Sum(x => x.DiscountTotal),
                        value * cartItem.TotalQuantity);
                    break;
                }
                case Enums.DiscountValueType.Percent:
                {
                    amount += value / 100 * extendedPrice;
                    break;
                }
            }
            return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        }
    }
}
