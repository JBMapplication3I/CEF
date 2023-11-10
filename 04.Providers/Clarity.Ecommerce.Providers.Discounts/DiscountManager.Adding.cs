// <copyright file="DiscountManager.Adding.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount manager class</summary>
namespace Clarity.Ecommerce.Providers.Discounts
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>Manager for discounts.</summary>
    public partial class DiscountManager
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> AddDiscountByCodeAsync(
            string code,
            int cartID,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            if (!CEFConfigDictionary.DiscountsEnabled)
            {
                // Feature Disabled
                return CEFAR.PassingCEFAR();
            }
            var discountID = await Workflows.Discounts.GetDiscountIDByCodeAsync(
                    code,
                    contextProfileName)
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(discountID))
            {
                return CEFAR.FailingCEFAR("ERROR! No discount found for this code.");
            }
            // Load the cart
            var cartResponse = await LoadCartAndClearDiscountsIfEmptyAsync(
                    cartID,
                    pricingFactoryContext,
                    taxesProvider,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!cartResponse.ActionSucceeded)
            {
                return cartResponse;
            }
            var cart = cartResponse.Result!;
            // Read the discount definition
            var verification = await VerifyDiscountAgainstCartAsync(
                    discountID: discountID!.Value,
                    cart: cart,
                    pricingFactoryContext: pricingFactoryContext,
                    skipAutoApplied: ProcessAutoApplied.Skip,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var reasonsToBeInvalid = verification.Result;
            if (reasonsToBeInvalid!.TryConvertToCEFAR(ProcessAutoApplied.Skip, out var retVal))
            {
                // Don't send over the wire
                retVal!.Result!.Discount = null;
                return retVal;
            }
            // Try to add it
            var added = await ApplyDiscountToCartAsync(
                    id: Contract.RequiresValidID(reasonsToBeInvalid.Discount!.ID),
                    typeID: reasonsToBeInvalid.Discount.DiscountTypeID,
                    value: reasonsToBeInvalid.Discount.Value,
                    valueType: reasonsToBeInvalid.Discount.ValueType,
                    buyXValue: reasonsToBeInvalid.Discount.BuyXValue ?? 0m,
                    getYValue: reasonsToBeInvalid.Discount.GetYValue ?? 0m,
                    productTypeIDs: reasonsToBeInvalid.Discount.ProductTypes?.Select(x => x.SlaveID).Cast<int?>().ToArray(),
                    categoryIDs: reasonsToBeInvalid.Discount.Categories?.Select(x => x.SlaveID).Cast<int?>().ToArray(),
                    currentApplicationLimit: reasonsToBeInvalid.UsageLimitRemainingApplications,
                    //// applicationsPreviouslyConsumedBy: reasonsToBeInvalid.ApplicationsPreviouslyConsumedBy,
                    validByBrandProducts: reasonsToBeInvalid.ValidByBrandProducts,
                    validByCategoryProducts: reasonsToBeInvalid.ValidByCategoryProducts,
                    validByManufacturerProducts: reasonsToBeInvalid.ValidByManufacturerProducts,
                    validByStoreProducts: reasonsToBeInvalid.ValidByStoreProducts,
                    validByFranchiseProducts: reasonsToBeInvalid.ValidByFranchiseProducts,
                    validByVendorProducts: reasonsToBeInvalid.ValidByVendorProducts,
                    validByProductTypes: reasonsToBeInvalid.ValidByProductTypes,
                    validByThresholdProducts: reasonsToBeInvalid.ValidByThresholdAmountProducts,
                    productIDs: reasonsToBeInvalid.Discount.Products!.Select(x => x.SlaveID).ToList(),
                    cart: cart,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return added.ActionSucceeded ? CEFAR.PassingCEFAR() : added;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> AddDiscountsByIDsAsync(
            List<int> discountIDs,
            int cartID,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            if (!CEFConfigDictionary.DiscountsEnabled)
            {
                // Feature Disabled
                return CEFAR.PassingCEFAR();
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            foreach (var discountID in discountIDs)
            {
                var code = await context.DiscountCodes
                    .AsNoTracking()
                    .Where(x => x.Active && x.DiscountID == discountID)
                    .Select(x => x.Code)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                if (!Contract.CheckValidKey(code))
                {
                    continue;
                }
                var result = await AddDiscountByCodeAsync(
                        code: code!,
                        cartID: cartID,
                        pricingFactoryContext: pricingFactoryContext,
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                await Logger.LogInformationAsync(
                        name: $"{nameof(DiscountManager)}.{nameof(AddDiscountsByIDsAsync)}.Re-adding Original Discount result.{cartID}|{discountID}",
                        message: JsonConvert.SerializeObject(result),
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Applies the verified automatic applied discount to cart.</summary>
        /// <param name="id">                         The identifier.</param>
        /// <param name="typeID">                     Identifier for the type.</param>
        /// <param name="value">                      The value.</param>
        /// <param name="valueType">                  Type of the value.</param>
        /// <param name="buyXValue">                  The buy x value.</param>
        /// <param name="getYValue">                  The get y value.</param>
        /// <param name="productTypeIDs">             The product type IDs.</param>
        /// <param name="categoryIDs">                The category IDs.</param>
        /// <param name="currentApplicationLimit">    The current application limit.</param>
        /// <param name="validByBrandProducts">       The valid by brand products.</param>
        /// <param name="validByCategoryProducts">    The valid by category products.</param>
        /// <param name="validByManufacturerProducts">The valid by manufacturer products.</param>
        /// <param name="validByStoreProducts">       The valid by store products.</param>
        /// <param name="validByFranchiseProducts">   The valid by franchise products.</param>
        /// <param name="validByVendorProducts">      The valid by vendor products.</param>
        /// <param name="validByProductTypes">        The valid by product types.</param>
        /// <param name="validByThresholdProducts">   The valid by threshold products.</param>
        /// <param name="productIDs">                 The product IDs.</param>
        /// <param name="cart">                       The cart.</param>
        /// <param name="contextProfileName">         Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        //// /// <param name="applicationsPreviouslyConsumedBy">Amount to applications previously consumed by.</param>
        internal static async Task<CEFActionResponse> ApplyVerifiedAutoAppliedDiscountToCartAsync(
            int id,
            int typeID,
            decimal value,
            int valueType,
            decimal buyXValue,
            decimal getYValue,
            int?[]? productTypeIDs,
            int?[]? categoryIDs,
            int? currentApplicationLimit,
            ////Dictionary<int, Dictionary<int, decimal>> applicationsPreviouslyConsumedBy,
            Dictionary<int, bool>? validByBrandProducts,
            Dictionary<int, bool>? validByCategoryProducts,
            Dictionary<int, bool>? validByManufacturerProducts,
            Dictionary<int, bool>? validByStoreProducts,
            Dictionary<int, bool>? validByFranchiseProducts,
            Dictionary<int, bool>? validByVendorProducts,
            Dictionary<int, bool>? validByProductTypes,
            Dictionary<int, bool>? validByThresholdProducts,
            ICollection<int> productIDs,
            ICartModel cart,
            string? contextProfileName)
        {
            // Try to add it
            var added = await ApplyDiscountToCartAsync(
                    id: id,
                    typeID: typeID,
                    value: value,
                    valueType: valueType,
                    buyXValue: buyXValue,
                    getYValue: getYValue,
                    productTypeIDs: productTypeIDs,
                    categoryIDs: categoryIDs,
                    currentApplicationLimit: currentApplicationLimit,
                    ////applicationsPreviouslyConsumedBy: applicationsPreviouslyConsumedBy,
                    validByBrandProducts: validByBrandProducts,
                    validByCategoryProducts: validByCategoryProducts,
                    validByManufacturerProducts: validByManufacturerProducts,
                    validByStoreProducts: validByStoreProducts,
                    validByFranchiseProducts: validByFranchiseProducts,
                    validByVendorProducts: validByVendorProducts,
                    validByProductTypes: validByProductTypes,
                    validByThresholdProducts: validByThresholdProducts,
                    productIDs: productIDs,
                    cart: cart,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return added.ActionSucceeded ? CEFAR.PassingCEFAR() : added;
        }

        /// <summary>Gets an adjusted amount from the original.</summary>
        /// <param name="original">               The original amount.</param>
        /// <param name="value">                  The value.</param>
        /// <param name="valueType">              Type of the value.</param>
        /// <param name="previouslyConsumed">     The previously consumed.</param>
        /// <param name="currentApplicationLimit">The current applications limit.</param>
        /// <returns>The total amount of the discount value to apply (money amount discounted) and the number of
        /// applications consumed.</returns>
#pragma warning disable SA1204 // Static elements should appear before instance elements
        private static (decimal amount, int consumed) GetAmountCartOrShip(
#pragma warning restore SA1204 // Static elements should appear before instance elements
            decimal? original,
            decimal value,
            int valueType,
            int previouslyConsumed,
            int? currentApplicationLimit)
        {
            if (currentApplicationLimit.HasValue
                && currentApplicationLimit - previouslyConsumed <= 0)
            {
                // We cannot consume any more applications
                return (0m, previouslyConsumed);
            }
            original ??= 0m;
            var amount = 0m;
            amount += valueType switch
            {
                (int)Enums.DiscountValueType.Amount => Math.Min(original.Value, value),
                (int)Enums.DiscountValueType.Percent => value / 100 * original.Value,
                _ => throw new ArgumentException("Invalid Discount Value Type"),
            };
            var roundedAmount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
            var roundedCurrentConsumed = roundedAmount > 0m ? 1 : 0;
            return (roundedAmount, roundedCurrentConsumed + previouslyConsumed);
        }

        /// <summary>Gets an amount.</summary>
        /// <param name="value">                      The value.</param>
        /// <param name="discountID">                 Identifier for the discount.</param>
        /// <param name="discountValueType">          Type of the discount value.</param>
        /// <param name="discountProductTypeIDs">     The discount product type IDs, if set.</param>
        /// <param name="discountCategoryIDs">        The discount category IDs, if set.</param>
        /// <param name="cartItem">                   The cart item.</param>
        /// <param name="previouslyConsumed">         The applications consumed.</param>
        /// <param name="currentApplicationLimit">    The current applications limit.</param>
        /// <param name="validByBrandProducts">       The valid by brand products.</param>
        /// <param name="validByCategoryProducts">    The valid by category products.</param>
        /// <param name="validByManufacturerProducts">The valid by manufacturer products.</param>
        /// <param name="validByStoreProducts">       The valid by store products.</param>
        /// <param name="validByFranchiseProducts">   The valid by franchise products.</param>
        /// <param name="validByVendorProducts">      The valid by vendor products.</param>
        /// <param name="validByProductTypes">        The valid by product types.</param>
        /// <param name="validByThresholdProducts">   The valid by threshold products.</param>
        /// <param name="contextProfileName">         Name of the context profile.</param>
        /// <returns>The total amount of the discount value to apply (money amount discounted) and the number of
        /// applications consumed.</returns>
        //// /// <param name="cartID">                          Identifier for the cart.</param>
        //// /// <param name="applicationsPreviouslyConsumedBy">Amount to applications previously consumed by.</param>
        private static (decimal amount, int consumed) GetAmountCartItems(
            decimal value,
            int discountID,
            int discountValueType,
            int?[]? discountProductTypeIDs,
            int?[]? discountCategoryIDs,
            //// int cartID,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> cartItem,
            int previouslyConsumed,
            int? currentApplicationLimit,
            //// Dictionary<int, Dictionary<int, decimal>> applicationsPreviouslyConsumedBy,
            IReadOnlyDictionary<int, bool>? validByBrandProducts,
            IReadOnlyDictionary<int, bool>? validByCategoryProducts,
            IReadOnlyDictionary<int, bool>? validByManufacturerProducts,
            IReadOnlyDictionary<int, bool>? validByStoreProducts,
            IReadOnlyDictionary<int, bool>? validByFranchiseProducts,
            IReadOnlyDictionary<int, bool>? validByVendorProducts,
            IReadOnlyDictionary<int, bool>? validByProductTypes,
            IReadOnlyDictionary<int, bool>? validByThresholdProducts,
            string? contextProfileName)
        {
            if (currentApplicationLimit.HasValue
                && currentApplicationLimit - previouslyConsumed <= 0)
            {
                // We cannot consume any more applications
                return (0m, previouslyConsumed);
            }
            if (Contract.CheckAnyValidID(discountProductTypeIDs)
                && (!Contract.CheckValidID(cartItem.ProductTypeID)
                    || !discountProductTypeIDs!.Contains(cartItem.ProductTypeID!.Value)))
            {
                // This isn't the right product type, skip
                return (0m, previouslyConsumed);
            }
            var validBys = new[]
            {
                validByBrandProducts,
                validByCategoryProducts,
                validByManufacturerProducts,
                validByStoreProducts,
                validByFranchiseProducts,
                validByVendorProducts,
                validByProductTypes,
                validByThresholdProducts,
            };
            if (validBys.Any(validBy => validBy != null
                && Contract.CheckValidID(cartItem.ProductID)
                && (!validBy.ContainsKey(cartItem.ProductID!.Value)
                    || !validBy[cartItem.ProductID.Value])))
            {
                return (0m, previouslyConsumed);
            }
            if (Contract.CheckAnyValidID(discountCategoryIDs))
            {
                if (!Contract.CheckValidID(cartItem.ProductID))
                {
                    // This isn't the right category, skip
                    return (0m, previouslyConsumed);
                }
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var categoryIDs = context.ProductCategories
                    .FilterByActive(true)
                    .FilterProductCategoriesByProductID(cartItem.ProductID!.Value)
                    .Select(x => x.SlaveID)
                    .Cast<int?>()
                    .ToList();
                if (!categoryIDs.Any())
                {
                    // This isn't the right category, skip
                    return (0m, previouslyConsumed);
                }
                if (!discountCategoryIDs!.Any(x => categoryIDs.Contains(x)))
                {
                    // This isn't the right category, skip
                    return (0m, previouslyConsumed);
                }
            }
            if (!currentApplicationLimit.HasValue
                || currentApplicationLimit - previouslyConsumed >= cartItem.TotalQuantity)
            {
                // There is either no limit or the limit is more than what this cart item would consume.
                // We can consume the full amount of this cart item in applications, no special logic required
                var extendedPrice = (cartItem.UnitSoldPrice ?? cartItem.UnitCorePrice) * cartItem.TotalQuantity;
                var amount = 0m;
                amount += discountValueType switch
                {
                    (int)Enums.DiscountValueType.Amount => Math.Min(
                        extendedPrice + cartItem.Discounts!.Where(x => x.DiscountID != discountID).Sum(x => x.DiscountTotal),
                        value * cartItem.TotalQuantity),
                    (int)Enums.DiscountValueType.Percent => value / 100 * extendedPrice,
                    _ => throw new ArgumentException("Invalid Discount Value Type"),
                };
                var roundedAmount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
                var roundedCurrentConsumed = (int)Math.Round(cartItem.TotalQuantity, 0, MidpointRounding.AwayFromZero);
                return (
                    roundedAmount,
                    Math.Max(
                        0,
                        roundedCurrentConsumed
                    ////- (applicationsPreviouslyConsumedBy.ContainsKey(cartID)
                    ////  && applicationsPreviouslyConsumedBy[cartID].ContainsKey(cartItem.ID.Value)
                    ////  ? (int)(applicationsPreviouslyConsumedBy[cartID][cartItem.ID.Value] / cartItem.TotalQuantity)
                    ////  : 0)
                    ) + previouslyConsumed
                );
            }
            else
            {
                // We can only consume up to the amount of applications we can apply
                var toConsume = Math.Max(0m, Math.Min((decimal)currentApplicationLimit - previouslyConsumed, cartItem.TotalQuantity));
                var extendedPrice = (cartItem.UnitSoldPrice ?? cartItem.UnitCorePrice) * toConsume;
                var amount = 0m;
                amount += discountValueType switch
                {
                    (int)Enums.DiscountValueType.Amount => Math.Min(
                        extendedPrice
                        + cartItem.Discounts!.Where(x => x.DiscountID != discountID).Sum(x => x.DiscountTotal),
                        value * toConsume),
                    (int)Enums.DiscountValueType.Percent => value / 100 * extendedPrice,
                    _ => throw new ArgumentException("Invalid Discount Value Type"),
                };
                var roundedAmount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
                var roundedCurrentConsumed = (int)Math.Round(toConsume, 0, MidpointRounding.AwayFromZero);
                return (roundedAmount, roundedCurrentConsumed + previouslyConsumed);
            }
        }

        /// <summary>Gets an amount.</summary>
        /// <param name="items">                  The cart items.</param>
        /// <param name="skip">                   The skip.</param>
        /// <param name="buyXValue">              The buy x value.</param>
        /// <param name="getYValue">              The get y value.</param>
        /// <param name="discountID">             Identifier for the discount.</param>
        /// <param name="value">                  The value.</param>
        /// <param name="valueType">              Type of the discount value.</param>
        /// <param name="previouslyConsumed">     The previously consumed.</param>
        /// <param name="currentApplicationLimit">The current application limit.</param>
        /// <returns>The amount.</returns>
        private static (decimal amount, int skipped, int consumed) GetAmountBuyXGetY(
            IReadOnlyCollection<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> items,
            int skip,
            decimal buyXValue,
            decimal getYValue,
            int discountID,
            decimal value,
            int valueType,
            int previouslyConsumed,
            int? currentApplicationLimit)
        {
            if (currentApplicationLimit.HasValue
                && currentApplicationLimit - previouslyConsumed <= 0)
            {
                // We cannot consume any more applications
                return (0m, 0, previouslyConsumed);
            }
            var amount = 0m;
            var toConsume = 0;
            while (skip + 1 <= items.Count)
            {
                amount += valueType switch
                {
                    (int)Enums.DiscountValueType.Amount => items
                        .Skip(skip)
                        .Take((int)getYValue)
                        .Sum(x => Math.Min(
                            (x.UnitSoldPrice ?? x.UnitCorePrice)
                                + x.Discounts!
                                    .Where(y => y.DiscountID != discountID)
                                    .Sum(y => y.DiscountTotal / items.Count),
                            value)),
                    (int)Enums.DiscountValueType.Percent => value / 100 * items
                        .Skip(skip)
                        .Take((int)getYValue)
                        .Sum(x => x.UnitSoldPrice ?? x.UnitCorePrice),
                    _ => throw new ArgumentException("Invalid Discount Value Type"),
                };
                skip += (int)(buyXValue + getYValue);
                toConsume++;
            }
            var roundedAmount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
            var skipped = items.Count % (int)(buyXValue + getYValue);
            return (roundedAmount, skipped, toConsume + previouslyConsumed);
        }

        /// <summary>Gets spread cart items.</summary>
        /// <param name="cartItems">The cart items.</param>
        /// <returns>The spread cart items.</returns>
        private static IEnumerable<IGrouping<int, ISalesItemBaseModel<IAppliedCartItemDiscountModel>>> GetSpreadCartItemsBuyXGetY(
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
            return items.GroupBy(x => x.ID);
        }

        /// <summary>Applies the discount to cart.</summary>
        /// <param name="id">                         The identifier.</param>
        /// <param name="typeID">                     Identifier for the type.</param>
        /// <param name="value">                      The value.</param>
        /// <param name="valueType">                  Type of the value.</param>
        /// <param name="buyXValue">                  The buy x value.</param>
        /// <param name="getYValue">                  The get y value.</param>
        /// <param name="productTypeIDs">             The product type IDs.</param>
        /// <param name="categoryIDs">                The category IDs.</param>
        /// <param name="currentApplicationLimit">    The current application limit.</param>
        /// <param name="validByBrandProducts">       The valid by brand products.</param>
        /// <param name="validByCategoryProducts">    The valid by category products.</param>
        /// <param name="validByManufacturerProducts">The valid by manufacturer products.</param>
        /// <param name="validByStoreProducts">       The valid by store products.</param>
        /// <param name="validByFranchiseProducts">   The valid by franchise products.</param>
        /// <param name="validByVendorProducts">      The valid by vendor products.</param>
        /// <param name="validByProductTypes">        The valid by product types.</param>
        /// <param name="validByThresholdProducts">   The valid by threshold products.</param>
        /// <param name="productIDs">                 The product IDs.</param>
        /// <param name="cart">                       The cart.</param>
        /// <param name="contextProfileName">         Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        //// /// <param name="applicationsPreviouslyConsumedBy">Amount to applications previously consumed by.</param>
        private static Task<CEFActionResponse> ApplyDiscountToCartAsync(
            int id,
            int typeID,
            decimal value,
            int valueType,
            decimal buyXValue,
            decimal getYValue,
            int?[]? productTypeIDs,
            int?[]? categoryIDs,
            int? currentApplicationLimit,
            //// Dictionary<int, Dictionary<int, decimal>> applicationsPreviouslyConsumedBy,
            IReadOnlyDictionary<int, bool>? validByBrandProducts,
            IReadOnlyDictionary<int, bool>? validByCategoryProducts,
            IReadOnlyDictionary<int, bool>? validByManufacturerProducts,
            IReadOnlyDictionary<int, bool>? validByStoreProducts,
            IReadOnlyDictionary<int, bool>? validByFranchiseProducts,
            IReadOnlyDictionary<int, bool>? validByVendorProducts,
            IReadOnlyDictionary<int, bool>? validByProductTypes,
            IReadOnlyDictionary<int, bool>? validByThresholdProducts,
            ICollection<int> productIDs,
            ICartModel cart,
            string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            return typeID switch
            {
                (int)Enums.DiscountType.Order => ApplyDiscountToCartOrderAsync(
                    id: id,
                    value: value,
                    valueType: valueType,
                    currentApplicationLimit: currentApplicationLimit,
                    cart: cart,
                    contextProfileName: contextProfileName),
                (int)Enums.DiscountType.Shipping => ApplyDiscountToCartShippingAsync(
                    id: id,
                    value: value,
                    valueType: valueType,
                    currentApplicationLimit: currentApplicationLimit,
                    cart: cart,
                    contextProfileName: contextProfileName),
                (int)Enums.DiscountType.BuyXGetY => ApplyDiscountToCartBuyXGetYAsync(
                    id: id,
                    value: value,
                    valueType: valueType,
                    buyXValue: buyXValue,
                    getYValue: getYValue,
                    currentApplicationLimit: currentApplicationLimit,
                    productIDs: productIDs,
                    cart: cart,
                    contextProfileName: contextProfileName),
                (int)Enums.DiscountType.Product => ApplyDiscountToCartProductAsync(
                    id: id,
                    value: value,
                    valueType: valueType,
                    productTypeIDs: productTypeIDs,
                    categoryIDs: categoryIDs,
                    currentApplicationLimit: currentApplicationLimit,
                    //// applicationsPreviouslyConsumedBy: applicationsPreviouslyConsumedBy,
                    validByBrandProducts: validByBrandProducts,
                    validByCategoryProducts: validByCategoryProducts,
                    validByManufacturerProducts: validByManufacturerProducts,
                    validByStoreProducts: validByStoreProducts,
                    validByFranchiseProducts: validByFranchiseProducts,
                    validByVendorProducts: validByVendorProducts,
                    validByProductTypes: validByProductTypes,
                    validByThresholdProducts: validByThresholdProducts,
                    productIDs: productIDs,
                    cart: cart,
                    contextProfileName: contextProfileName),
                _ => throw new ArgumentException("Unknown discount type"),
            };
        }

        /// <summary>Applies the discount to cart product.</summary>
        /// <param name="id">                         The identifier.</param>
        /// <param name="value">                      The value.</param>
        /// <param name="valueType">                  Type of the value.</param>
        /// <param name="productTypeIDs">             The product type IDs.</param>
        /// <param name="categoryIDs">                The category IDs.</param>
        /// <param name="currentApplicationLimit">    The current application limit.</param>
        /// <param name="validByBrandProducts">       The valid by brand products.</param>
        /// <param name="validByCategoryProducts">    The valid by category products.</param>
        /// <param name="validByManufacturerProducts">The valid by manufacturer products.</param>
        /// <param name="validByStoreProducts">       The valid by store products.</param>
        /// <param name="validByFranchiseProducts">   The valid by franchise products.</param>
        /// <param name="validByVendorProducts">      The valid by vendor products.</param>
        /// <param name="validByProductTypes">        The valid by product types.</param>
        /// <param name="validByThresholdProducts">   The valid by threshold products.</param>
        /// <param name="productIDs">                 The product IDs.</param>
        /// <param name="cart">                       The cart.</param>
        /// <param name="contextProfileName">         Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        //// /// <param name="applicationsPreviouslyConsumedBy">Amount to applications previously consumed by.</param>
        private static async Task<CEFActionResponse> ApplyDiscountToCartProductAsync(
            int id,
            decimal value,
            int valueType,
            int?[]? productTypeIDs,
            int?[]? categoryIDs,
            int? currentApplicationLimit,
            //// Dictionary<int, Dictionary<int, decimal>> applicationsPreviouslyConsumedBy,
            IReadOnlyDictionary<int, bool>? validByBrandProducts,
            IReadOnlyDictionary<int, bool>? validByCategoryProducts,
            IReadOnlyDictionary<int, bool>? validByManufacturerProducts,
            IReadOnlyDictionary<int, bool>? validByStoreProducts,
            IReadOnlyDictionary<int, bool>? validByFranchiseProducts,
            IReadOnlyDictionary<int, bool>? validByVendorProducts,
            IReadOnlyDictionary<int, bool>? validByProductTypes,
            IReadOnlyDictionary<int, bool>? validByThresholdProducts,
            ICollection<int> productIDs,
            ICartModel cart,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var applied = false;
            var applicationsConsumed = 0;
            foreach (var cartItem in cart.SalesItems!
                .Where(x => !productIDs.Any()
                         || Contract.CheckValidID(x.ProductID) && productIDs.Contains(x.ProductID!.Value)))
            {
                decimal amount;
                (amount, applicationsConsumed) = GetAmountCartItems(
                    value: value,
                    discountID: id,
                    discountValueType: valueType,
                    discountProductTypeIDs: productTypeIDs,
                    discountCategoryIDs: categoryIDs,
                    //// cartID: cart.ID,
                    cartItem: cartItem,
                    previouslyConsumed: applicationsConsumed,
                    currentApplicationLimit: currentApplicationLimit,
                    ////applicationsPreviouslyConsumedBy: applicationsPreviouslyConsumedBy,
                    validByBrandProducts: validByBrandProducts,
                    validByCategoryProducts: validByCategoryProducts,
                    validByManufacturerProducts: validByManufacturerProducts,
                    validByStoreProducts: validByStoreProducts,
                    validByFranchiseProducts: validByFranchiseProducts,
                    validByVendorProducts: validByVendorProducts,
                    validByProductTypes: validByProductTypes,
                    validByThresholdProducts: validByThresholdProducts,
                    contextProfileName: contextProfileName);
                if (amount <= 0m || applicationsConsumed <= 0)
                {
                    await DeactivateSaveAndRemoveFromModelListAsync<ISalesItemBaseModel<IAppliedCartItemDiscountModel>,
                            IAppliedCartItemDiscountModel,
                            CartItem,
                            AppliedCartItemDiscount>(
                            cartItem,
                            id,
                            context)
                        .ConfigureAwait(false);
                    continue;
                }
                await UpsertSaveAndAppendEntityToModelListAsync<ISalesItemBaseModel<IAppliedCartItemDiscountModel>,
                            IAppliedCartItemDiscountModel,
                            AppliedCartItemDiscountModel,
                            CartItem,
                            AppliedCartItemDiscount>(
                        amount,
                        applicationsConsumed,
                        cartItem,
                        id,
                        context)
                    .ConfigureAwait(false);
                applied = true;
            }
            return applied.BoolToCEFAR("ERROR! Result would be no effect on final price.");
        }

        /// <summary>Applies the discount to cart buy x get y.</summary>
        /// <param name="id">                     The identifier.</param>
        /// <param name="value">                  The value.</param>
        /// <param name="valueType">              Type of the value.</param>
        /// <param name="buyXValue">              The buy x value.</param>
        /// <param name="getYValue">              The get y value.</param>
        /// <param name="currentApplicationLimit">The current application limit.</param>
        /// <param name="productIDs">             The product IDs.</param>
        /// <param name="cart">                   The cart.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        private static async Task<CEFActionResponse> ApplyDiscountToCartBuyXGetYAsync(
            int id,
            decimal value,
            int valueType,
            decimal buyXValue,
            decimal getYValue,
            int? currentApplicationLimit,
            ICollection<int> productIDs,
            ICartModel cart,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var applied = false;
            var skip = (int)buyXValue;
            var spreadCartItems = GetSpreadCartItemsBuyXGetY(cart.SalesItems!);
            var applicationsConsumed = 0;
            foreach (var items in spreadCartItems)
            {
                decimal amount;
                int skipped;
                (amount, skipped, applicationsConsumed) = GetAmountBuyXGetY(
                    items: items.ToList(),
                    skip: skip,
                    buyXValue: buyXValue,
                    getYValue: getYValue,
                    discountID: id,
                    value: value,
                    valueType: valueType,
                    previouslyConsumed: applicationsConsumed,
                    currentApplicationLimit: currentApplicationLimit);
                skip = Math.Abs(skip - skipped);
                var cartItem = cart.SalesItems!.Single(x => x.ID == items.Key);
                if (productIDs.Any()
                    && (!Contract.CheckValidID(cartItem.ProductID)
                        || !productIDs.Contains(cartItem.ProductID!.Value)))
                {
                    continue;
                }
                if (amount <= 0m || applicationsConsumed <= 0)
                {
                    await DeactivateSaveAndRemoveFromModelListAsync<ISalesItemBaseModel<IAppliedCartItemDiscountModel>,
                                IAppliedCartItemDiscountModel,
                                CartItem,
                                AppliedCartItemDiscount>(
                            master: cartItem,
                            discountID: id,
                            context: context)
                        .ConfigureAwait(false);
                    continue;
                }
                await UpsertSaveAndAppendEntityToModelListAsync<ISalesItemBaseModel<IAppliedCartItemDiscountModel>,
                            IAppliedCartItemDiscountModel,
                            AppliedCartItemDiscountModel,
                            CartItem,
                            AppliedCartItemDiscount>(
                        amount: amount,
                        consumed: applicationsConsumed,
                        master: cartItem,
                        discountID: id,
                        context: context,
                        overrideItemID: items.Key)
                    .ConfigureAwait(false);
                applied = true;
            }
            return applied.BoolToCEFAR("ERROR! Result would be no effect on final price.");
        }

        /// <summary>Applies the discount to cart shipping.</summary>
        /// <param name="id">                     The identifier.</param>
        /// <param name="value">                  The value.</param>
        /// <param name="valueType">              Type of the value.</param>
        /// <param name="currentApplicationLimit">The current application limit.</param>
        /// <param name="cart">                   The cart.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        private static async Task<CEFActionResponse> ApplyDiscountToCartShippingAsync(
            int id,
            decimal value,
            int valueType,
            int? currentApplicationLimit,
            ICartModel cart,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var applied = false;
            foreach (var rateQuote in cart.RateQuotes!)
            {
                // Rate quotes are individualized and only one would be active/selected, so each one can only consume
                // one application and the final which goes into an order would only consume one as well, but we need
                // to show discounted values to all rates for the user to pick the one they want
                var applicationsConsumed = 0;
                decimal amount;
                (amount, applicationsConsumed) = GetAmountCartOrShip(
                    original: rateQuote.Rate,
                    value: value,
                    valueType: valueType,
                    previouslyConsumed: applicationsConsumed,
                    currentApplicationLimit: currentApplicationLimit);
                if (amount <= 0m || applicationsConsumed <= 0)
                {
                    await DeactivateSaveAndRemoveFromModelListAsync<ICartModel,
                                IAppliedCartDiscountModel,
                                Cart,
                                AppliedCartDiscount>(
                            master: cart,
                            discountID: id,
                            context: context)
                        .ConfigureAwait(false);
                    continue;
                }
                await UpsertSaveAndAppendEntityToModelListAsync<ICartModel,
                            IAppliedCartDiscountModel,
                            AppliedCartDiscountModel,
                            Cart,
                            AppliedCartDiscount>(
                        amount: amount,
                        consumed: applicationsConsumed,
                        master: cart,
                        discountID: id,
                        context: context)
                    .ConfigureAwait(false);
                applied = true;
            }
            return applied.BoolToCEFAR("ERROR! Result would be no effect on final price.");
        }

        /// <summary>Applies the discount to cart order.</summary>
        /// <param name="id">                     The identifier.</param>
        /// <param name="value">                  The value.</param>
        /// <param name="valueType">              Type of the value.</param>
        /// <param name="currentApplicationLimit">The current application limit.</param>
        /// <param name="cart">                   The cart.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        private static async Task<CEFActionResponse> ApplyDiscountToCartOrderAsync(
            int id,
            decimal value,
            int valueType,
            int? currentApplicationLimit,
            ICartModel cart,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var applicationsConsumed = 0;
            decimal amount;
            (amount, applicationsConsumed) = GetAmountCartOrShip(
                original: cart.Totals.SubTotal,
                value: value,
                valueType: valueType,
                previouslyConsumed: applicationsConsumed,
                currentApplicationLimit: currentApplicationLimit);
            if (amount <= 0m || applicationsConsumed <= 0)
            {
                await DeactivateSaveAndRemoveFromModelListAsync<ICartModel,
                            IAppliedCartDiscountModel,
                            Cart,
                            AppliedCartDiscount>(
                        master: cart,
                        discountID: id,
                        context: context)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR("ERROR! Result would be no effect on final price.");
            }
            await UpsertSaveAndAppendEntityToModelListAsync<ICartModel,
                        IAppliedCartDiscountModel,
                        AppliedCartDiscountModel,
                        Cart,
                        AppliedCartDiscount>(
                    amount: amount,
                    consumed: applicationsConsumed,
                    master: cart,
                    discountID: id,
                    context: context)
                .ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }
    }
}
