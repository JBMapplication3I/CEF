// <copyright file="ShipmentDiscountValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment discount validator class</summary>
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

    /// <summary>A shipment discount validator.</summary>
    /// <seealso cref="BaseDiscountValidator"/>
    public class ShipmentDiscountValidator : BaseDiscountValidator
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
            rateQuotes = GetSelectedRateQuotes(rateQuotes, discount);
            var response = new CEFActionResponse<List<IAppliedDiscountBaseModel>>();
            if (rateQuotes.Any(x => x.Selected))
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
            else
            {
                response.Messages.Add(
                    rateQuotes.Any(x => x.Active && x.Selected)
                        ? "Discount does not apply to selected rate quotes"
                        : "No shipment methods selected");
            }
            if (response.ActionSucceeded)
            {
                response = ValidateAmountThreshold(
                    rateQuotes.Sum(x => x.Rate ?? 0),
                    discount.DiscountCompareOperator,
                    discount.ThresholdAmount,
                    discount.DiscountTypeID);
            }
            if (response.ActionSucceeded)
            {
                var (added, appliedDiscountBaseModels) = await SetDiscountTotalAsync(
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
            foreach (var rateQuote in rateQuotes)
            {
                // ReSharper disable once PossibleInvalidOperationException
                await DeactivateAsync<AppliedCartDiscount, Cart>(
                        masterID: rateQuote.ID.Value,
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
            var updatedDiscounts = currentDiscounts;
            foreach (var amount in rateQuotes.Select(x => GetAmount(x.Rate, discount.Value, discount.ValueType)))
            {
                if (amount == 0m)
                {
                    // ReSharper disable once PossibleInvalidOperationException
                    await DeactivateAsync<AppliedCartDiscount, Cart>(
                            masterID: cartID,
                            discountID: discount.ID,
                            context: context)
                        .ConfigureAwait(false);
                    updatedDiscounts = new List<IAppliedDiscountBaseModel>(
                        currentDiscounts.Where(x => x.DiscountID != discount.ID));
                    deactivated = true;
                    continue;
                }
                var entity = await CreateOrUpdateAsync<AppliedCartDiscount, Cart>(
                        amount: amount,
                        masterID: cartID,
                        discountID: discount.ID,
                        context: context)
                    .ConfigureAwait(false);
                if (isAdd || discount.IsAutoApplied && currentDiscounts.All(x => x.DiscountID != discount.ID))
                {
                    updatedDiscounts = new List<IAppliedDiscountBaseModel>(currentDiscounts)
                    {
                        ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityFull(entity),
                    };
                    applied = true;
                    continue;
                }
                currentDiscounts.Single(x => x.DiscountID == discount.ID).DiscountTotal = amount * -1;
                applied = true;
            }
            if (applied || deactivated)
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            return (applied, updatedDiscounts);
        }

        /// <summary>Gets an adjusted amount from the original.</summary>
        /// <param name="original"> The original amount.</param>
        /// <param name="value">    The value.</param>
        /// <param name="valueType">Type of the value.</param>
        /// <returns>The adjusted amount.</returns>
        private decimal GetAmount(decimal? original, decimal value, int valueType)
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

        /// <summary>Gets selected rate quotes.</summary>
        /// <param name="theRateQuotes">The rate quotes.</param>
        /// <param name="discount">     The discount.</param>
        /// <returns>The selected rate quotes.</returns>
        private List<IRateQuoteModel> GetSelectedRateQuotes(
            IEnumerable<IRateQuoteModel> theRateQuotes,
            IDiscount discount)
        {
            // Business requirement:
            // If the discount has associated shipment methods, only apply it to those
            // Else apply it to any selected
            if (discount.ShipCarrierMethods.Count > 0)
            {
                return theRateQuotes
                    .Where(x => x.Selected
                             && discount.ShipCarrierMethods.Any(y => y.SlaveID == x.ShipCarrierMethodID))
                    .ToList();
            }
            return theRateQuotes.Where(x => x.Selected).ToList();
        }
    }
}
