// <copyright file="CartValidator.Categories.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator class</summary>
// ReSharper disable MultipleSpaces
namespace Clarity.Ecommerce.Providers.CartValidation
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A cart validator.</summary>
    public partial class CartValidator
    {
        /// <summary>Process the category with minimum dollar.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="summary">              The category.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="response">             The response.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it needs to modify the item, false if it does not.</returns>
        protected virtual async Task<bool> ProcessCategoryWithMinimumDollarAsync(
            ICartModel cart,
            CategorySummary summary,
            IPricingFactoryContextModel pricingFactoryContext,
            CEFActionResponse response,
            string? contextProfileName)
        {
            // ReSharper disable once InconsistentNaming, StyleCop.SA1303
            const string kind = nameof(Category);
            if (!Contract.CheckValidID(summary.DollarAmount))
            {
                // There was no min purchase amount value, move on
                return false;
            }
            var attrKey = $"MinOrderBy:Dol:IgnAcc:{kind}:{summary.ID}";
            var cacheKey = $"HardSoftStops:{attrKey}";
            var cacheKeyBrands = $"{cacheKey}-Brands";
            var dollarInCart = 0m;
            var catNeedsToBeMet = false;
            var previouslyMet = false;
            bool cartMeetsDollar;
            var minPurchaseDollar = summary.DollarAmount!.Value;
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            var linkHashesBrands = client is null ? null : await client.GetAsync<HashSet<(int?, string)>>(cacheKeyBrands).ConfigureAwait(false);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var linkHashesProducts = client is null ? null : await client.GetAsync<HashSet<int>>(cacheKey).ConfigureAwait(false);
                if (linkHashesProducts == null)
                {
                    linkHashesProducts = new(
                        context.ProductCategories
                            .AsNoTracking()
                            .Where(x => x.Active && x.SlaveID == summary.ID)
                            .OrderBy(x => x.MasterID)
                            .Select(x => x.MasterID));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKey, linkHashesProducts, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                if (linkHashesBrands == null)
                {
                    linkHashesBrands = new(
                        (await context.Set<BrandCategory>()
                                .AsNoTracking()
                                .Where(x => x.Active && x.SlaveID == summary.ID)
                                .OrderBy(x => x.MasterID)
                                .Select(x => new { x.MasterID, x.Master!.Name })
                                .ToListAsync()
                                .ConfigureAwait(false))
                            .Select(x => ((int?)x.MasterID, x.Name!)));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKeyBrands, linkHashesBrands, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false); // Save the cache
                    }
                }
                foreach (var cartItem in cart.SalesItems!.Where(c => c.ProductID.HasValue && linkHashesProducts.Contains(c.ProductID.Value)))
                {
                    catNeedsToBeMet = true;
                    dollarInCart += cartItem.ExtendedPrice;
                }
                if (!catNeedsToBeMet)
                {
                    return false;
                } // Move on to next Category
                var minPurchaseDollarAfter = summary.DollarAmountAfter is >= 0
                    ? summary.DollarAmountAfter.Value
                    : minPurchaseDollar;
                // ReSharper disable ConditionIsAlwaysTrueOrFalse, HeuristicUnreachableCode
                if (Contract.CheckValidIDOrKey(pricingFactoryContext.AccountID, pricingFactoryContext.AccountKey)
                    || Contract.CheckValidIDOrKey(pricingFactoryContext.UserID, pricingFactoryContext.UserKey))
                {
                    var previousAmount = await PrepHistoryQuery(
                            summaryID: summary.ID,
                            kind: kind,
                            accountID: pricingFactoryContext.AccountID,
                            accountKey: pricingFactoryContext.AccountKey,
                            userID: pricingFactoryContext.UserID,
                            userKey: pricingFactoryContext.UserKey,
                            context: context)
                        .Select(x => (x.UnitSoldPrice ?? x.UnitCorePrice)
                            * (x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m)))
                        .DefaultIfEmpty(0m)
                        .SumAsync();
                    previouslyMet = previousAmount >= minPurchaseDollar;
                }
                cartMeetsDollar = previouslyMet
                    ? dollarInCart >= minPurchaseDollarAfter
                    : dollarInCart >= minPurchaseDollar;
                // ReSharper restore ConditionIsAlwaysTrueOrFalse, HeuristicUnreachableCode
            }
            // Send messaging if not meeting
            if (cartMeetsDollar)
            {
                return false;
            }
            var overrideFee = summary.DollarAmountOverrideFeeIsPercent
                ? ((summary.DollarAmountOverrideFee ?? 0m) / 100).ToString("p0")
                : (summary.DollarAmountOverrideFee ?? 0m).ToString("c2");
            var (relatedBrandID, relatedBrandName) = linkHashesBrands.Count > 0
                ? linkHashesBrands.First()
                : (null, null);
            if (summary.DollarAmountOverrideFee >= 0m
                && cart.SerializableAttributes!.ContainsKey(attrKey)
                && cart.SerializableAttributes.TryGetValue(attrKey, out var overrideValue)
                && bool.TryParse(overrideValue.Value, out var overrideValueParsed)
                && overrideValueParsed)
            {
                if (Contract.CheckValidKey(summary.DollarAmountOverrideFeeAcceptedMessage))
                {
                    response.Messages.Add(
                        "WARNING! "
                        + DoReplacementsMinOrders(
                            warningMessage: summary.DollarAmountOverrideFeeAcceptedMessage!,
                            ownerID: summary.ID,
                            ownerName: summary.Name,
                            attrKey: attrKey,
                            requiredAmount: $"{minPurchaseDollar:c}",
                            missingAmount: $"{minPurchaseDollar - dollarInCart:c}",
                            overrideFeeWarningMessage: summary.DollarAmountOverrideFeeWarningMessage,
                            overrideFee: overrideFee,
                            relatedBrandID: relatedBrandID,
                            relatedBrandName: relatedBrandName,
                            bufferCategoryName: summary.DollarAmountBufferCategoryDisplayName ?? summary.DollarAmountBufferCategoryName ?? string.Empty,
                            bufferCategorySeoUrl: summary.DollarAmountBufferCategorySeoUrl ?? string.Empty,
                            bufferItemName: summary.DollarAmountBufferProductName ?? string.Empty,
                            bufferItemSeoUrl: summary.DollarAmountBufferProductSeoUrl ?? string.Empty));
                    return false;
                }
                response.Messages.Add(
                    $"WARNING! The dollar amount of products in the cart for \"{summary.DisplayName ?? summary.Name}\""
                    + " doesn't meet the requirements set by the store administrators for categories, however the"
                    + $" Override Fee of {overrideFee} has been accepted.");
                return false;
            }
            if (!Contract.CheckValidKey(summary.DollarAmountWarningMessage))
            {
                if (summary.DollarAmountOverrideFee >= 0m
                    && !Contract.CheckValidKey(summary.DollarAmountOverrideFeeWarningMessage))
                {
                    response.Messages.Add(
                        $"ERROR! The minimum purchase requirements for category \"{summary.DisplayName ?? summary.Name}\""
                        + $" have not been met! However, an override option is available for a fee of {overrideFee}.");
                    return false;
                }
                response.Messages.Add(
                    $"ERROR! The minimum purchase requirements for category \"{summary.DisplayName ?? summary.Name}\" have not been met!");
                return false;
            }
            response.Messages.Add(
                "ERROR! "
                + DoReplacementsMinOrders(
                        warningMessage: summary.DollarAmountWarningMessage!,
                        ownerID: summary.ID,
                        ownerName: summary.DisplayName ?? summary.Name,
                        attrKey: attrKey,
                        requiredAmount: $"{minPurchaseDollar:c}",
                        missingAmount: $"{minPurchaseDollar - dollarInCart:c}",
                        overrideFeeWarningMessage: summary.DollarAmountOverrideFeeWarningMessage,
                        overrideFee: overrideFee,
                        relatedBrandID: relatedBrandID,
                        relatedBrandName: relatedBrandName,
                        bufferCategoryName: summary.DollarAmountBufferCategoryDisplayName ?? summary.DollarAmountBufferCategoryName ?? string.Empty,
                        bufferCategorySeoUrl: summary.DollarAmountBufferCategorySeoUrl ?? string.Empty,
                        bufferItemName: summary.DollarAmountBufferProductName ?? string.Empty,
                        bufferItemSeoUrl: summary.DollarAmountBufferProductSeoUrl ?? string.Empty));
            if (Contract.CheckValidKey(summary.DollarAmountBufferCategorySeoUrl))
            {
                response.Messages.Add(summary.DollarAmountBufferCategorySeoUrl!);
            }
            if (Contract.CheckValidKey(summary.DollarAmountBufferProductSeoUrl))
            {
                response.Messages.Add(summary.DollarAmountBufferProductSeoUrl!);
            }
            // Do nothing
            // SUCCESS! The dollar amount of products in the cart for category meets the requirements set by the store administrators
            return false;
        }

        /// <summary>Process the category with minimum quantity.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="summary">              The category.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="response">             The response.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual async Task<bool> ProcessCategoryWithMinimumQuantityAsync(
            ICartModel cart,
            CategorySummary summary,
            IPricingFactoryContextModel pricingFactoryContext,
            CEFActionResponse response,
            string? contextProfileName)
        {
            // ReSharper disable once InconsistentNaming, StyleCop.SA1303
            const string kind = nameof(Category);
            if (!Contract.CheckValidID(summary.QuantityAmount))
            {
                // There was no min purchase amount value, move on
                return false;
            }
            var attrKey = $"MinOrderBy:Qty:IgnAcc:{kind}:{summary.ID}";
            var cacheKey = $"HardSoftStops:{attrKey}";
            var cacheKeyBrands = $"{cacheKey}-Brands";
            var quantityInCart = 0m;
            var catNeedsToBeMet = false;
            var previouslyMet = false;
            bool cartMeetsQuantity;
            var minOrderQuantity = summary.QuantityAmount!.Value;
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            var linkHashesBrands = client is null ? null : await client.GetAsync<HashSet<(int?, string)>>(cacheKeyBrands).ConfigureAwait(false);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var linkHashesProducts = client is null ? null : await client.GetAsync<HashSet<int>>(cacheKey).ConfigureAwait(false);
                if (linkHashesProducts == null)
                {
                    linkHashesProducts = new(
                        context.ProductCategories
                            .AsNoTracking()
                            .Where(x => x.Active && x.SlaveID == summary.ID)
                            .OrderBy(x => x.MasterID)
                            .Select(x => x.MasterID));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKey, linkHashesProducts, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                if (linkHashesBrands == null)
                {
                    linkHashesBrands = new(
                        (await context.Set<BrandCategory>()
                                .AsNoTracking()
                                .Where(x => x.Active && x.SlaveID == summary.ID)
                                .OrderBy(x => x.MasterID)
                                .Select(x => new { x.MasterID, x.Master!.Name })
                                .ToListAsync()
                                .ConfigureAwait(false))
                            .Select(x => ((int?)x.MasterID, x.Name!)));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKeyBrands, linkHashesBrands, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                foreach (var cartItem in cart.SalesItems!.Where(x => x.ProductID.HasValue && linkHashesProducts.Contains(x.ProductID.Value)))
                {
                    catNeedsToBeMet = true;
                    quantityInCart += cartItem.TotalQuantity;
                }
                if (!catNeedsToBeMet)
                {
                    // Move on to next Category
                    return false;
                }
                var minOrderQuantityAfter = summary.QuantityAmountAfter is >= 0m
                    ? summary.QuantityAmountAfter.Value
                    : minOrderQuantity;
                // ReSharper disable ConditionIsAlwaysTrueOrFalse, HeuristicUnreachableCode
                if (Contract.CheckValidIDOrKey(pricingFactoryContext.AccountID, pricingFactoryContext.AccountKey)
                    || Contract.CheckValidIDOrKey(pricingFactoryContext.UserID, pricingFactoryContext.UserKey))
                {
                    var previousAmount = await PrepHistoryQuery(
                            summary.ID,
                            kind,
                            pricingFactoryContext.AccountID,
                            pricingFactoryContext.AccountKey,
                            pricingFactoryContext.UserID,
                            pricingFactoryContext.UserKey,
                            context)
                        .Select(x => x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m))
                        .DefaultIfEmpty(0m)
                        .SumAsync();
                    previouslyMet = previousAmount >= minOrderQuantity;
                }
                cartMeetsQuantity = previouslyMet
                    ? quantityInCart >= minOrderQuantityAfter
                    : quantityInCart >= minOrderQuantity;
                // ReSharper restore ConditionIsAlwaysTrueOrFalse, HeuristicUnreachableCode
            }
            // Send messaging if not meeting
            if (cartMeetsQuantity)
            {
                return false;
            }
            var overrideFee = summary.QuantityAmountOverrideFeeIsPercent
                ? ((summary.QuantityAmountOverrideFee ?? 0m) / 100).ToString("p0")
                : (summary.QuantityAmountOverrideFee ?? 0m).ToString("c2");
            var (relatedBrandID, relatedBrandName) = linkHashesBrands.Count > 0
                ? linkHashesBrands.First()
                : (null, null);
            if (summary.QuantityAmountOverrideFee >= 0m
                && cart.SerializableAttributes!.ContainsKey(attrKey)
                && cart.SerializableAttributes.TryGetValue(attrKey, out var overrideValue)
                && bool.TryParse(overrideValue.Value, out var overrideValueParsed)
                && overrideValueParsed)
            {
                if (Contract.CheckValidKey(summary.QuantityAmountOverrideFeeAcceptedMessage))
                {
                    response.Messages.Add(
                        "WARNING! "
                        + DoReplacementsMinOrders(
                            warningMessage: summary.QuantityAmountOverrideFeeAcceptedMessage!,
                            ownerID: summary.ID,
                            ownerName: summary.Name,
                            attrKey: attrKey,
                            requiredAmount: $"{minOrderQuantity:n0}",
                            missingAmount: $"{minOrderQuantity - quantityInCart:n0}",
                            overrideFeeWarningMessage: summary.QuantityAmountOverrideFeeWarningMessage,
                            overrideFee: overrideFee,
                            relatedBrandID: relatedBrandID,
                            relatedBrandName: relatedBrandName,
                            bufferCategoryName: summary.QuantityAmountBufferCategoryDisplayName ?? summary.QuantityAmountBufferCategoryName ?? string.Empty,
                            bufferCategorySeoUrl: summary.QuantityAmountBufferCategorySeoUrl ?? string.Empty,
                            bufferItemName: summary.QuantityAmountBufferProductName ?? string.Empty,
                            bufferItemSeoUrl: summary.QuantityAmountBufferProductSeoUrl ?? string.Empty));
                    if (Contract.CheckValidKey(summary.QuantityAmountBufferCategorySeoUrl))
                    {
                        response.Messages.Add(summary.QuantityAmountBufferCategorySeoUrl!);
                    }
                    if (Contract.CheckValidKey(summary.QuantityAmountBufferProductSeoUrl))
                    {
                        response.Messages.Add(summary.QuantityAmountBufferProductSeoUrl!);
                    }
                    return false;
                }
                response.Messages.Add(
                    $"WARNING! The quantity amount of products in the cart for \"{summary.DisplayName ?? summary.Name}\""
                    + " doesn't meet the requirements set by the store administrators for categories, however the"
                    + $" Override Fee of {overrideFee} has been accepted.");
                return false;
            }
            if (!Contract.CheckValidKey(summary.QuantityAmountWarningMessage))
            {
                if (summary.QuantityAmountOverrideFee >= 0m
                    && !Contract.CheckValidKey(summary.QuantityAmountOverrideFeeWarningMessage))
                {
                    response.Messages.Add(
                        $"ERROR! The minimum purchase requirements for category \"{summary.DisplayName ?? summary.Name}\""
                        + $" have not been met! However, an override option is available for a fee of {overrideFee}.");
                    return false;
                }
                response.Messages.Add(
                    $"ERROR! The minimum purchase requirements for category \"{summary.DisplayName ?? summary.Name}\" have not been met!");
                return false;
            }
            response.Messages.Add(
                "ERROR! "
                + DoReplacementsMinOrders(
                        warningMessage: summary.QuantityAmountWarningMessage!,
                        ownerID: summary.ID,
                        ownerName: summary.DisplayName ?? summary.Name,
                        attrKey: attrKey,
                        requiredAmount: $"{minOrderQuantity:n0}",
                        missingAmount: $"{minOrderQuantity - quantityInCart:n0}",
                        overrideFeeWarningMessage: summary.QuantityAmountOverrideFeeWarningMessage,
                        overrideFee: overrideFee,
                        relatedBrandID: relatedBrandID,
                        relatedBrandName: relatedBrandName,
                        bufferCategoryName: summary.QuantityAmountBufferCategoryDisplayName ?? summary.QuantityAmountBufferCategoryName ?? string.Empty,
                        bufferCategorySeoUrl: summary.QuantityAmountBufferCategorySeoUrl ?? string.Empty,
                        bufferItemName: summary.QuantityAmountBufferProductName ?? string.Empty,
                        bufferItemSeoUrl: summary.QuantityAmountBufferProductSeoUrl ?? string.Empty));
            if (Contract.CheckValidKey(summary.QuantityAmountBufferCategorySeoUrl))
            {
                response.Messages.Add(summary.QuantityAmountBufferCategorySeoUrl!);
            }
            if (Contract.CheckValidKey(summary.QuantityAmountBufferProductSeoUrl))
            {
                response.Messages.Add(summary.QuantityAmountBufferProductSeoUrl!);
            }
            // Do nothing
            // SUCCESS! The quantity amount of products in the cart for this category meets the requirements set by the store administrators
            return false;
        }

        /// <summary>Process the summary with minimum dollar free shipping.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="summary">              The summary.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="response">             The response.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual async Task<bool> ProcessCategoryWithMinimumDollarFreeShippingAsync(
            ICartModel cart,
            CategorySummary summary,
            IPricingFactoryContextModel pricingFactoryContext,
            CEFActionResponse response,
            string? contextProfileName)
        {
            // ReSharper disable once InconsistentNaming, StyleCop.SA1303
            const string kind = nameof(Category);
            if (!Contract.CheckValidID(summary.FreeShippingDollarAmount))
            {
                // There was no min purchase amount value, move on
                return false;
            }
            var attrKey = $"FreeShipBy:Dol:IgnAcc:{kind}:{summary.ID}";
            var cacheKey = $"HardSoftStops:{attrKey}";
            var cacheKeyBrands = $"{cacheKey}-Brands";
            var dollarInCart = 0m;
            var needsToBeMet = false;
            var previouslyMet = false;
            bool cartMeetsDollar;
            var minPurchaseDollar = summary.FreeShippingDollarAmount!.Value;
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            var linkHashesBrands = client is null ? null : await client.GetAsync<HashSet<(int?, string)>>(cacheKeyBrands).ConfigureAwait(false);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var linkHashesProducts = client is null ? null : await client.GetAsync<HashSet<int>>(cacheKey).ConfigureAwait(false);
                if (linkHashesProducts == null)
                {
                    linkHashesProducts = new(
                        context.ProductCategories
                            .AsNoTracking()
                            .Where(x => x.Active && x.SlaveID == summary.ID)
                            .OrderBy(x => x.MasterID)
                            .Select(x => x.MasterID));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKey, linkHashesProducts, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                if (linkHashesBrands == null)
                {
                    linkHashesBrands = new(
                        (await context.Set<BrandCategory>()
                                .AsNoTracking()
                                .Where(x => x.Active && x.SlaveID == summary.ID)
                                .OrderBy(x => x.MasterID)
                                .Select(x => new { x.MasterID, x.Master!.Name })
                                .ToListAsync()
                                .ConfigureAwait(false))
                            .Select(x => ((int?)x.MasterID, x.Name!)));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKeyBrands, linkHashesBrands, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                foreach (var cartItem in cart.SalesItems!.Where(c => c.ProductID.HasValue && linkHashesProducts.Contains(c.ProductID.Value)))
                {
                    needsToBeMet = true;
                    dollarInCart += cartItem.ExtendedPrice;
                }
                if (!needsToBeMet)
                {
                    return false;
                } // Move on to next Summary
                var minPurchaseDollarAfter = summary.FreeShippingDollarAmountAfter is >= 0
                    ? summary.FreeShippingDollarAmountAfter.Value
                    : minPurchaseDollar;
                // ReSharper disable ConditionIsAlwaysTrueOrFalse, HeuristicUnreachableCode
                if (Contract.CheckValidIDOrKey(pricingFactoryContext.AccountID, pricingFactoryContext.AccountKey)
                    || Contract.CheckValidIDOrKey(pricingFactoryContext.UserID, pricingFactoryContext.UserKey))
                {
                    var previousAmount = await PrepHistoryQuery(
                            summaryID: summary.ID,
                            kind: kind,
                            accountID: pricingFactoryContext.AccountID,
                            accountKey: pricingFactoryContext.AccountKey,
                            userID: pricingFactoryContext.UserID,
                            userKey: pricingFactoryContext.UserKey,
                            context: context)
                        .Select(x => (x.UnitSoldPrice ?? x.UnitCorePrice)
                            * (x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m)))
                        .DefaultIfEmpty(0m)
                        .SumAsync();
                    previouslyMet = previousAmount >= minPurchaseDollar;
                }
                cartMeetsDollar = previouslyMet
                    ? dollarInCart >= minPurchaseDollarAfter
                    : dollarInCart >= minPurchaseDollar;
                // ReSharper restore ConditionIsAlwaysTrueOrFalse, HeuristicUnreachableCode
            }
            // Send messaging if not meeting
            if (cartMeetsDollar)
            {
                return false;
            }
            var (relatedBrandID, relatedBrandName) = linkHashesBrands.Count > 0
                ? linkHashesBrands.First()
                : (null, null);
            if (cart.SerializableAttributes!.ContainsKey(attrKey)
                && cart.SerializableAttributes.TryGetValue(attrKey, out var ignoreValue)
                && bool.TryParse(ignoreValue.Value, out var ignoreValueParsed)
                && ignoreValueParsed)
            {
                if (Contract.CheckValidKey(summary.FreeShippingDollarAmountIgnoredAcceptedMessage))
                {
                    response.Messages.Add(
                        "WARNING! "
                        + DoReplacementsFreeShipping(
                            warningMessage: summary.FreeShippingDollarAmountIgnoredAcceptedMessage!,
                            ownerID: summary.ID,
                            ownerName: summary.Name ?? string.Empty,
                            attrKey: attrKey,
                            requiredAmount: $"{minPurchaseDollar:c}",
                            missingAmount: $"{minPurchaseDollar - dollarInCart:c}",
                            ignoredMessage: summary.FreeShippingDollarAmountWarningMessage ?? string.Empty,
                            relatedBrandID: relatedBrandID,
                            relatedBrandName: relatedBrandName ?? string.Empty,
                            bufferCategoryName: summary.FreeShippingDollarAmountBufferCategoryDisplayName ?? summary.FreeShippingDollarAmountBufferCategoryName ?? string.Empty,
                            bufferCategorySeoUrl: summary.FreeShippingDollarAmountBufferCategorySeoUrl ?? string.Empty,
                            bufferItemName: summary.FreeShippingDollarAmountBufferProductName ?? string.Empty,
                            bufferItemSeoUrl: summary.FreeShippingDollarAmountBufferProductSeoUrl ?? string.Empty));
                    if (Contract.CheckValidKey(summary.FreeShippingDollarAmountBufferCategorySeoUrl))
                    {
                        response.Messages.Add(summary.FreeShippingDollarAmountBufferCategorySeoUrl!);
                    }
                    if (Contract.CheckValidKey(summary.FreeShippingDollarAmountBufferProductSeoUrl))
                    {
                        response.Messages.Add(summary.FreeShippingDollarAmountBufferProductSeoUrl!);
                    }
                    return false;
                }
                response.Messages.Add(
                    $"WARNING! The dollar amount of products in the cart for \"{summary.DisplayName ?? summary.Name}\" doesn't meet the"
                    + " requirements set by the store administrators for Categories to get free shipping, however "
                    + " the warning has been ignored.");
                return false;
            }
            if (!Contract.CheckValidKey(summary.FreeShippingDollarAmountWarningMessage))
            {
                response.Messages.Add(
                    $"ERROR! The minimum purchase requirements for Category '{summary.DisplayName ?? summary.Name}' to get free shipping have not been met!");
                return false;
            }
            response.Messages.Add(
                "ERROR! "
                + DoReplacementsFreeShipping(
                    warningMessage: summary.FreeShippingDollarAmountWarningMessage!,
                    ownerID: summary.ID,
                    ownerName: summary.Name ?? string.Empty,
                    attrKey: attrKey,
                    requiredAmount: $"{minPurchaseDollar:c}",
                    missingAmount: $"{minPurchaseDollar - dollarInCart:c}",
                    ignoredMessage: summary.FreeShippingDollarAmountWarningMessage ?? string.Empty,
                    relatedBrandID: relatedBrandID,
                    relatedBrandName: relatedBrandName ?? string.Empty,
                    bufferCategoryName: summary.FreeShippingDollarAmountBufferCategoryDisplayName ?? summary.FreeShippingDollarAmountBufferCategoryName ?? string.Empty,
                    bufferCategorySeoUrl: summary.FreeShippingDollarAmountBufferCategorySeoUrl ?? string.Empty,
                    bufferItemName: summary.FreeShippingDollarAmountBufferProductName ?? string.Empty,
                    bufferItemSeoUrl: summary.FreeShippingDollarAmountBufferProductSeoUrl ?? string.Empty));
            if (Contract.CheckValidKey(summary.FreeShippingDollarAmountBufferCategorySeoUrl))
            {
                response.Messages.Add(summary.FreeShippingDollarAmountBufferCategorySeoUrl!);
            }
            if (Contract.CheckValidKey(summary.FreeShippingDollarAmountBufferProductSeoUrl))
            {
                response.Messages.Add(summary.FreeShippingDollarAmountBufferProductSeoUrl!);
            }
            // Do nothing
            // SUCCESS! The dollar amount of products in the cart for "{summary.DisplayName ?? summary.Name}" meets the requirements set by the store administrators for Categories
            return false;
        }

        /// <summary>Process the summary with minimum quantity free shipping.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="summary">              The summary.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="response">             The response.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual async Task<bool> ProcessCategoryWithMinimumQuantityFreeShippingAsync(
            ICartModel cart,
            CategorySummary summary,
            IPricingFactoryContextModel pricingFactoryContext,
            CEFActionResponse response,
            string? contextProfileName)
        {
            // ReSharper disable once InconsistentNaming, StyleCop.SA1303
            const string kind = nameof(Category);
            if (!Contract.CheckValidID(summary.FreeShippingQuantityAmount))
            {
                // There was no min purchase amount value, move on
                return false;
            }
            var attrKey = $"FreeShipBy:Qty:IgnAcc:{kind}:{summary.ID}";
            var cacheKey = $"HardSoftStops:{attrKey}";
            var cacheKeyBrands = $"{cacheKey}-Brands";
            var quantityInCart = 0m;
            var needsToBeMet = false;
            var previouslyMet = false;
            bool cartMeetsQuantity;
            var minPurchaseQuantity = summary.FreeShippingQuantityAmount!.Value;
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            var linkHashesBrands = client is null ? null : await client.GetAsync<HashSet<(int?, string)>>(cacheKeyBrands).ConfigureAwait(false);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var linkHashesProducts = client is null ? null : await client.GetAsync<HashSet<int>>(cacheKey).ConfigureAwait(false);
                if (linkHashesProducts == null)
                {
                    linkHashesProducts = new(
                        context.ProductCategories
                            .AsNoTracking()
                            .Where(x => x.Active && x.SlaveID == summary.ID)
                            .OrderBy(x => x.MasterID)
                            .Select(x => x.MasterID));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKey, linkHashesProducts, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                if (linkHashesBrands == null)
                {
                    linkHashesBrands = new(
                        (await context.Set<BrandCategory>()
                                .AsNoTracking()
                                .Where(x => x.Active && x.SlaveID == summary.ID)
                                .OrderBy(x => x.MasterID)
                                .Select(x => new { x.MasterID, x.Master!.Name })
                                .ToListAsync()
                                .ConfigureAwait(false))
                            .Select(x => ((int?)x.MasterID, x.Name!)));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKeyBrands, linkHashesBrands, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                foreach (var cartItem in cart.SalesItems!.Where(c => c.ProductID.HasValue && linkHashesProducts.Contains(c.ProductID.Value)))
                {
                    needsToBeMet = true;
                    quantityInCart += cartItem.TotalQuantity;
                }
                if (!needsToBeMet)
                {
                    return false;
                } // Move on to next Summary
                var minPurchaseQuantityAfter = summary.FreeShippingQuantityAmountAfter is >= 0
                    ? summary.FreeShippingQuantityAmountAfter.Value
                    : minPurchaseQuantity;
                // ReSharper disable ConditionIsAlwaysTrueOrFalse, HeuristicUnreachableCode
                if (Contract.CheckValidIDOrKey(pricingFactoryContext.AccountID, pricingFactoryContext.AccountKey)
                    || Contract.CheckValidIDOrKey(pricingFactoryContext.UserID, pricingFactoryContext.UserKey))
                {
                    var previousAmount = await PrepHistoryQuery(
                            summaryID: summary.ID,
                            kind: kind,
                            accountID: pricingFactoryContext.AccountID,
                            accountKey: pricingFactoryContext.AccountKey,
                            userID: pricingFactoryContext.UserID,
                            userKey: pricingFactoryContext.UserKey,
                            context: context)
                        .Select(x => x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m))
                        .DefaultIfEmpty(0m)
                        .SumAsync();
                    previouslyMet = previousAmount >= minPurchaseQuantity;
                }
                cartMeetsQuantity = previouslyMet
                    ? quantityInCart >= minPurchaseQuantityAfter
                    : quantityInCart >= minPurchaseQuantity;
                // ReSharper restore ConditionIsAlwaysTrueOrFalse, HeuristicUnreachableCode
            }
            // Send messaging if not meeting
            if (cartMeetsQuantity)
            {
                return false;
            }
            var (relatedBrandID, relatedBrandName) = linkHashesBrands.Count > 0
                ? linkHashesBrands.First()
                : (null, null);
            if (cart.SerializableAttributes!.ContainsKey(attrKey)
                && cart.SerializableAttributes.TryGetValue(attrKey, out var ignoredValue)
                && bool.TryParse(ignoredValue.Value, out var ignoredValueParsed)
                && ignoredValueParsed)
            {
                if (Contract.CheckValidKey(summary.FreeShippingQuantityAmountIgnoredAcceptedMessage))
                {
                    response.Messages.Add(
                        "WARNING! "
                        + DoReplacementsFreeShipping(
                            warningMessage: summary.FreeShippingQuantityAmountIgnoredAcceptedMessage!,
                            ownerID: summary.ID,
                            ownerName: summary.Name ?? string.Empty,
                            attrKey: attrKey,
                            requiredAmount: $"{minPurchaseQuantity:n0}",
                            missingAmount: $"{minPurchaseQuantity - quantityInCart:n0}",
                            ignoredMessage: summary.FreeShippingQuantityAmountWarningMessage ?? string.Empty,
                            relatedBrandID: relatedBrandID,
                            relatedBrandName: relatedBrandName ?? string.Empty,
                            bufferCategoryName: summary.FreeShippingQuantityAmountBufferCategoryDisplayName ?? summary.FreeShippingQuantityAmountBufferCategoryName ?? string.Empty,
                            bufferCategorySeoUrl: summary.FreeShippingQuantityAmountBufferCategorySeoUrl ?? string.Empty,
                            bufferItemName: summary.FreeShippingQuantityAmountBufferProductName ?? string.Empty,
                            bufferItemSeoUrl: summary.FreeShippingQuantityAmountBufferProductSeoUrl ?? string.Empty));
                    if (Contract.CheckValidKey(summary.FreeShippingQuantityAmountBufferCategorySeoUrl))
                    {
                        response.Messages.Add(summary.FreeShippingQuantityAmountBufferCategorySeoUrl!);
                    }
                    if (Contract.CheckValidKey(summary.FreeShippingQuantityAmountBufferProductSeoUrl))
                    {
                        response.Messages.Add(summary.FreeShippingQuantityAmountBufferProductSeoUrl!);
                    }
                    return false;
                }
                response.Messages.Add(
                    $"WARNING! The quantity amount of products in the cart for Category \"{summary.DisplayName ?? summary.Name}\" doesn't"
                    + " meet the requirements set by the store administrators to get free shipping, however the"
                    + " warning has been ignored.");
                return false;
            }
            if (!Contract.CheckValidKey(summary.FreeShippingQuantityAmountWarningMessage))
            {
                response.Messages.Add(
                    $"ERROR! The minimum purchase requirements for Category \"{summary.DisplayName ?? summary.Name}\" have not been met for free shipping!");
                return false;
            }
            response.Messages.Add(
                "ERROR! "
                + DoReplacementsFreeShipping(
                    warningMessage: summary.FreeShippingQuantityAmountWarningMessage!,
                    ownerID: summary.ID,
                    ownerName: summary.Name ?? string.Empty,
                    attrKey: attrKey,
                    requiredAmount: $"{minPurchaseQuantity:n0}",
                    missingAmount: $"{minPurchaseQuantity - quantityInCart:n0}",
                    ignoredMessage: summary.FreeShippingQuantityAmountWarningMessage ?? string.Empty,
                    relatedBrandID: relatedBrandID,
                    relatedBrandName: relatedBrandName ?? string.Empty,
                    bufferCategoryName: summary.FreeShippingQuantityAmountBufferCategoryDisplayName ?? summary.FreeShippingQuantityAmountBufferCategoryName ?? string.Empty,
                    bufferCategorySeoUrl: summary.FreeShippingQuantityAmountBufferCategorySeoUrl ?? string.Empty,
                    bufferItemName: summary.FreeShippingQuantityAmountBufferProductName ?? string.Empty,
                    bufferItemSeoUrl: summary.FreeShippingQuantityAmountBufferProductSeoUrl ?? string.Empty));
            if (Contract.CheckValidKey(summary.FreeShippingQuantityAmountBufferCategorySeoUrl))
            {
                response.Messages.Add(summary.FreeShippingQuantityAmountBufferCategorySeoUrl!);
            }
            if (Contract.CheckValidKey(summary.FreeShippingQuantityAmountBufferProductSeoUrl))
            {
                response.Messages.Add(summary.FreeShippingQuantityAmountBufferProductSeoUrl!);
            }
            // Do nothing
            // SUCCESS! The quantity amount of products in the cart for "{summary.DisplayName ?? summary.Name}" meets the requirements set by the store administrators for Categories
            return false;
        }

        /// <summary>Process the category with minimums.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="category">             The category.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="response">             The response.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual async Task<bool> ProcessCategoryWithMinimumsAsync(
            ICartModel cart,
            CategorySummary category,
            IPricingFactoryContextModel pricingFactoryContext,
            CEFActionResponse response,
            string? contextProfileName)
        {
            var temp1 = await ProcessCategoryWithMinimumDollarAsync(cart, category, pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp2 = await ProcessCategoryWithMinimumQuantityAsync(cart, category, pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp3 = await ProcessCategoryWithMinimumDollarFreeShippingAsync(cart, category, pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp4 = await ProcessCategoryWithMinimumQuantityFreeShippingAsync(cart, category, pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            return temp1 || temp2 || temp3 || temp4;
        }

        /// <summary>Process the categories with restrictions.</summary>
        /// <param name="response">             The response.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it changes the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> ProcessCategoriesWithMinimumsAsync(
            CEFActionResponse response,
            ICartModel cart,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            if (!Config!.DoCategoryRestrictionsByMinMax)
            {
                // SUCCESS! There are no categories with restrictions
                return false;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var withMinimums = (await context.Categories
                    .AsNoTracking()
                    .FilterByActive(true)
                    .Where(x => x.MinimumOrderDollarAmount > 0
                             || x.MinimumOrderQuantityAmount > 0
                             || x.MinimumForFreeShippingDollarAmount > 0
                             || x.MinimumForFreeShippingQuantityAmount > 0)
                    .Select(x => new
                    {
                        x.ID,
                        x.Name,
                        x.DisplayName,
                        x.JsonAttributes,
                        DollarAmount = x.MinimumOrderDollarAmount,
                        DollarAmountAfter = x.MinimumOrderDollarAmountAfter,
                        DollarAmountWarningMessage = x.MinimumOrderDollarAmountWarningMessage,
                        DollarAmountOverrideFee = x.MinimumOrderDollarAmountOverrideFee,
                        DollarAmountOverrideFeeIsPercent = x.MinimumOrderDollarAmountOverrideFeeIsPercent,
                        DollarAmountOverrideFeeWarningMessage = x.MinimumOrderDollarAmountOverrideFeeWarningMessage,
                        DollarAmountOverrideFeeAcceptedMessage = x.MinimumOrderDollarAmountOverrideFeeAcceptedMessage,
                        DollarAmountBufferProductSeoUrl = x.MinimumOrderDollarAmountBufferProduct != null ? x.MinimumOrderDollarAmountBufferProduct.SeoUrl : null,
                        DollarAmountBufferProductName = x.MinimumOrderDollarAmountBufferProduct != null ? x.MinimumOrderDollarAmountBufferProduct.Name : null,
                        DollarAmountBufferCategorySeoUrl = x.MinimumOrderDollarAmountBufferCategory != null ? x.MinimumOrderDollarAmountBufferCategory.SeoUrl : null,
                        DollarAmountBufferCategoryName = x.MinimumOrderDollarAmountBufferCategory != null ? x.MinimumOrderDollarAmountBufferCategory.Name : null,
                        DollarAmountBufferCategoryDisplayName = x.MinimumOrderDollarAmountBufferCategory != null ? x.MinimumOrderDollarAmountBufferCategory.DisplayName : null,
                        QuantityAmount = x.MinimumOrderQuantityAmount,
                        QuantityAmountAfter = x.MinimumOrderQuantityAmountAfter,
                        QuantityAmountWarningMessage = x.MinimumOrderQuantityAmountWarningMessage,
                        QuantityAmountOverrideFee = x.MinimumOrderQuantityAmountOverrideFee,
                        QuantityAmountOverrideFeeIsPercent = x.MinimumOrderQuantityAmountOverrideFeeIsPercent,
                        QuantityAmountOverrideFeeWarningMessage = x.MinimumOrderQuantityAmountOverrideFeeWarningMessage,
                        QuantityAmountOverrideFeeAcceptedMessage = x.MinimumOrderQuantityAmountOverrideFeeAcceptedMessage,
                        QuantityAmountBufferProductSeoUrl = x.MinimumOrderQuantityAmountBufferProduct != null ? x.MinimumOrderQuantityAmountBufferProduct.SeoUrl : null,
                        QuantityAmountBufferProductName = x.MinimumOrderQuantityAmountBufferProduct != null ? x.MinimumOrderQuantityAmountBufferProduct.Name : null,
                        QuantityAmountBufferCategorySeoUrl = x.MinimumOrderQuantityAmountBufferCategory != null ? x.MinimumOrderQuantityAmountBufferCategory.SeoUrl : null,
                        QuantityAmountBufferCategoryName = x.MinimumOrderQuantityAmountBufferCategory != null ? x.MinimumOrderQuantityAmountBufferCategory.Name : null,
                        QuantityAmountBufferCategoryDisplayName = x.MinimumOrderQuantityAmountBufferCategory != null ? x.MinimumOrderQuantityAmountBufferCategory.DisplayName : null,
                        FreeShippingDollarAmount = x.MinimumForFreeShippingDollarAmount,
                        FreeShippingDollarAmountAfter = x.MinimumForFreeShippingDollarAmountAfter,
                        FreeShippingDollarAmountWarningMessage = x.MinimumForFreeShippingDollarAmountWarningMessage,
                        FreeShippingDollarAmountIgnoredAcceptedMessage = x.MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage,
                        FreeShippingDollarAmountBufferProductSeoUrl = x.MinimumForFreeShippingDollarAmountBufferProduct != null ? x.MinimumForFreeShippingDollarAmountBufferProduct.SeoUrl : null,
                        FreeShippingDollarAmountBufferProductName = x.MinimumForFreeShippingDollarAmountBufferProduct != null ? x.MinimumForFreeShippingDollarAmountBufferProduct.Name : null,
                        FreeShippingDollarAmountBufferCategorySeoUrl = x.MinimumForFreeShippingDollarAmountBufferCategory != null ? x.MinimumForFreeShippingDollarAmountBufferCategory.SeoUrl : null,
                        FreeShippingDollarAmountBufferCategoryName = x.MinimumForFreeShippingDollarAmountBufferCategory != null ? x.MinimumForFreeShippingDollarAmountBufferCategory.Name : null,
                        FreeShippingDollarAmountBufferCategoryDisplayName = x.MinimumForFreeShippingDollarAmountBufferCategory != null ? x.MinimumForFreeShippingDollarAmountBufferCategory.DisplayName : null,
                        FreeShippingQuantityAmount = x.MinimumForFreeShippingQuantityAmount,
                        FreeShippingQuantityAmountAfter = x.MinimumForFreeShippingQuantityAmountAfter,
                        FreeShippingQuantityAmountWarningMessage = x.MinimumForFreeShippingQuantityAmountWarningMessage,
                        FreeShippingQuantityAmountIgnoredAcceptedMessage = x.MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage,
                        FreeShippingQuantityAmountBufferProductSeoUrl = x.MinimumForFreeShippingQuantityAmountBufferProduct != null ? x.MinimumForFreeShippingQuantityAmountBufferProduct.SeoUrl : null,
                        FreeShippingQuantityAmountBufferProductName = x.MinimumForFreeShippingQuantityAmountBufferProduct != null ? x.MinimumForFreeShippingQuantityAmountBufferProduct.Name : null,
                        FreeShippingQuantityAmountBufferCategorySeoUrl = x.MinimumForFreeShippingQuantityAmountBufferCategory != null ? x.MinimumForFreeShippingQuantityAmountBufferCategory.SeoUrl : null,
                        FreeShippingQuantityAmountBufferCategoryName = x.MinimumForFreeShippingQuantityAmountBufferCategory != null ? x.MinimumForFreeShippingQuantityAmountBufferCategory.Name : null,
                        FreeShippingQuantityAmountBufferCategoryDisplayName = x.MinimumForFreeShippingQuantityAmountBufferCategory != null ? x.MinimumForFreeShippingQuantityAmountBufferCategory.DisplayName : null,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new CategorySummary
                {
                    ID = x.ID,
                    Name = x.Name,
                    DisplayName = x.DisplayName,
                    JsonAttributes = x.JsonAttributes,
                    DollarAmount = x.DollarAmount,
                    DollarAmountAfter = x.DollarAmountAfter,
                    DollarAmountWarningMessage = x.DollarAmountWarningMessage,
                    DollarAmountOverrideFee = x.DollarAmountOverrideFee,
                    DollarAmountOverrideFeeIsPercent = x.DollarAmountOverrideFeeIsPercent,
                    DollarAmountOverrideFeeWarningMessage = x.DollarAmountOverrideFeeWarningMessage,
                    DollarAmountOverrideFeeAcceptedMessage = x.DollarAmountOverrideFeeAcceptedMessage,
                    DollarAmountBufferProductSeoUrl = x.DollarAmountBufferProductSeoUrl,
                    DollarAmountBufferProductName = x.DollarAmountBufferProductName,
                    DollarAmountBufferCategorySeoUrl = x.DollarAmountBufferCategorySeoUrl,
                    DollarAmountBufferCategoryName = x.DollarAmountBufferCategoryName,
                    DollarAmountBufferCategoryDisplayName = x.DollarAmountBufferCategoryDisplayName,
                    QuantityAmount = x.QuantityAmount,
                    QuantityAmountAfter = x.QuantityAmountAfter,
                    QuantityAmountWarningMessage = x.QuantityAmountWarningMessage,
                    QuantityAmountOverrideFee = x.QuantityAmountOverrideFee,
                    QuantityAmountOverrideFeeIsPercent = x.QuantityAmountOverrideFeeIsPercent,
                    QuantityAmountOverrideFeeWarningMessage = x.QuantityAmountOverrideFeeWarningMessage,
                    QuantityAmountOverrideFeeAcceptedMessage = x.QuantityAmountOverrideFeeAcceptedMessage,
                    QuantityAmountBufferProductSeoUrl = x.QuantityAmountBufferProductSeoUrl,
                    QuantityAmountBufferProductName = x.QuantityAmountBufferProductName,
                    QuantityAmountBufferCategorySeoUrl = x.QuantityAmountBufferCategorySeoUrl,
                    QuantityAmountBufferCategoryName = x.QuantityAmountBufferCategoryName,
                    QuantityAmountBufferCategoryDisplayName = x.QuantityAmountBufferCategoryDisplayName,
                    FreeShippingDollarAmount = x.FreeShippingDollarAmount,
                    FreeShippingDollarAmountAfter = x.FreeShippingDollarAmountAfter,
                    FreeShippingDollarAmountWarningMessage = x.FreeShippingDollarAmountWarningMessage,
                    FreeShippingDollarAmountIgnoredAcceptedMessage = x.FreeShippingDollarAmountIgnoredAcceptedMessage,
                    FreeShippingDollarAmountBufferProductSeoUrl = x.FreeShippingDollarAmountBufferProductSeoUrl,
                    FreeShippingDollarAmountBufferProductName = x.FreeShippingDollarAmountBufferProductName,
                    FreeShippingDollarAmountBufferCategorySeoUrl = x.FreeShippingDollarAmountBufferCategorySeoUrl,
                    FreeShippingDollarAmountBufferCategoryName = x.FreeShippingDollarAmountBufferCategoryName,
                    FreeShippingDollarAmountBufferCategoryDisplayName = x.FreeShippingDollarAmountBufferCategoryDisplayName,
                    FreeShippingQuantityAmount = x.FreeShippingQuantityAmount,
                    FreeShippingQuantityAmountAfter = x.FreeShippingQuantityAmountAfter,
                    FreeShippingQuantityAmountWarningMessage = x.FreeShippingQuantityAmountWarningMessage,
                    FreeShippingQuantityAmountIgnoredAcceptedMessage = x.FreeShippingQuantityAmountIgnoredAcceptedMessage,
                    FreeShippingQuantityAmountBufferProductSeoUrl = x.FreeShippingQuantityAmountBufferProductSeoUrl,
                    FreeShippingQuantityAmountBufferProductName = x.FreeShippingQuantityAmountBufferProductName,
                    FreeShippingQuantityAmountBufferCategorySeoUrl = x.FreeShippingQuantityAmountBufferCategorySeoUrl,
                    FreeShippingQuantityAmountBufferCategoryName = x.FreeShippingQuantityAmountBufferCategoryName,
                    FreeShippingQuantityAmountBufferCategoryDisplayName = x.FreeShippingQuantityAmountBufferCategoryDisplayName,
                })
                .ToList();
            var checkedIDs = new List<int>();
            if (withMinimums.Count == 0)
            {
                // SUCCESS! There are no categories with restrictions
                return false;
            }
            var changedCart = false;
            foreach (var summary in withMinimums.Where(x => !checkedIDs.Contains(x.ID)))
            {
                checkedIDs.Add(summary.ID);
                changedCart |= await ProcessCategoryWithMinimumsAsync(
                        cart,
                        summary,
                        pricingFactoryContext,
                        response,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            return changedCart;
        }
    }
}
