// <copyright file="BuyXGetYDiscountValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the "Buy X/Get Y" discount validator class</summary>
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
    using Mapper;
    using Models;

    /// <summary>A "Buy X/Get Y" discount validator.</summary>
    /// <seealso cref="BaseDiscountValidator"/>
    public class BuyXGetYDiscountValidator : BaseDiscountValidator
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
            var response = new CEFActionResponse<List<IAppliedDiscountBaseModel>>();
            var buyXValue = (int)(discount.BuyXValue ?? 0);
            var getYValue = (int)(discount.GetYValue ?? 0);
            if (buyXValue == 0 || getYValue == 0)
            {
                response.Messages.Add("Invalid BuyXGetY values");
            }
            else
            {
                response = await base.ValidateDiscountAsync(
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
            }
            if (response.ActionSucceeded)
            {
                response = ValidateAmountThreshold(
                    cartItems.Sum(x => (x.UnitSoldPrice ?? x.UnitCorePrice) * x.TotalQuantity),
                    discount.DiscountCompareOperator,
                    discount.ThresholdAmount,
                    discount.DiscountTypeID);
            }
            if (!response.ActionSucceeded)
            {
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
            var buyXValue = (int)(discount.BuyXValue ?? 0);
            var getYValue = (int)(discount.GetYValue ?? 0);
            var skip = buyXValue;
            var updatedDiscounts = currentDiscounts;
            var spreadCartItems = GetSpreadCartItems(cartItems);
            foreach (var items in spreadCartItems)
            {
                var (amount, skipped) = GetAmount(
                    items.ToList(),
                    skip,
                    buyXValue,
                    getYValue,
                    discount.ID,
                    discount.Value,
                    (Enums.DiscountValueType)discount.ValueType);
                skip = Math.Abs(skip - skipped);
                var cartItem = cartItems.Single(x => x.ID.Value == items.Key);
                if (amount == 0m)
                {
                    await DeactivateAsync<AppliedCartItemDiscount, CartItem>(
                            masterID: items.Key,
                            discountID: discount.ID,
                            context: context)
                        .ConfigureAwait(false);
                    cartItem.Discounts = new List<IAppliedCartItemDiscountModel>(
                        cartItem.Discounts.Where(x => x.DiscountID != discount.ID));
                    deactivated = true;
                    continue;
                }
                var entity = await CreateOrUpdateAsync<AppliedCartItemDiscount, CartItem>(
                        amount: amount,
                        masterID: items.Key,
                        discountID: discount.ID,
                        context: context)
                    .ConfigureAwait(false);
                if (isAdd)
                {
                    cartItem.Discounts = new List<IAppliedCartItemDiscountModel>(cartItem.Discounts)
                    {
                        ModelMapperForAppliedCartItemDiscount.MapAppliedCartItemDiscountModelFromEntityFull(entity),
                    };
                    applied = true;
                    continue;
                }
                if (cartItem.Discounts.All(x => x.DiscountID != discount.ID))
                {
                    continue;
                }
                cartItem.Discounts.Single(x => x.DiscountID == discount.ID).DiscountTotal = amount * -1;
                applied = true;
            }
            if (applied || deactivated)
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            return (applied, updatedDiscounts);
        }

        /// <summary>Gets an amount.</summary>
        /// <param name="items">            The cart items.</param>
        /// <param name="skip">             The skip.</param>
        /// <param name="buyXValue">        The buy x coordinate value.</param>
        /// <param name="getYValue">        The get y coordinate value.</param>
        /// <param name="discountID">       Identifier for the discount.</param>
        /// <param name="value">            The value.</param>
        /// <param name="discountValueType">Type of the discount value.</param>
        /// <returns>The amount.</returns>
        private (decimal amount, int skipped) GetAmount(
            IReadOnlyCollection<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> items,
            int skip,
            int buyXValue,
            int getYValue,
            int discountID,
            decimal value,
            Enums.DiscountValueType discountValueType)
        {
            var amount = 0m;
            while (skip + 1 <= items.Count)
            {
                switch (discountValueType)
                {
                    case Enums.DiscountValueType.Amount:
                    {
                        amount += items
                            .Skip(skip)
                            .Take(getYValue)
                            .Sum(x => Math.Min(
                                        (x.UnitSoldPrice ?? x.UnitCorePrice)
                                        + x.Discounts
                                           .Where(y => y.DiscountID != discountID)
                                           .Sum(y => y.DiscountTotal / items.Count),
                                        value));
                        break;
                    }
                    case Enums.DiscountValueType.Percent:
                    {
                        amount += value
                            / 100
                            * items
                                .Skip(skip)
                                .Take(getYValue)
                                .Sum(x => x.UnitSoldPrice ?? x.UnitCorePrice);
                        break;
                    }
                }
                skip += buyXValue + getYValue;
            }
            return (Math.Round(amount, 2, MidpointRounding.AwayFromZero), items.Count % (buyXValue + getYValue));
        }

        /// <summary>Gets spread cart items.</summary>
        /// <param name="cartItems">The cart items.</param>
        /// <returns>The spread cart items.</returns>
        private List<IGrouping<int, ISalesItemBaseModel<IAppliedCartItemDiscountModel>>> GetSpreadCartItems(
            IEnumerable<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> cartItems)
        {
            var items = new List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
            foreach (var cartItem in cartItems.OrderByDescending(x => x.UnitSoldPrice ?? x.UnitCorePrice))
            {
                for (var i = 0; i < cartItem.TotalQuantity; i++)
                {
                    items.Add(cartItem);
                }
            }
            return items.GroupBy(x => x.ID.Value).ToList();
        }
    }
}
