// <copyright file="CartValidator.Shared.FreeShip.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator class</summary>
// ReSharper disable BadParensLineBreaks, CognitiveComplexity, CyclomaticComplexity, InvertIf, MergeSequentialChecks, MultipleSpaces
namespace Clarity.Ecommerce.Providers.CartValidation
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A cart validator.</summary>
    public partial class CartValidator
    {
        /// <summary>Process the summary with minimum dollar free shipping.</summary>
        /// <typeparam name="TMaster">     Type of the master.</typeparam>
        /// <typeparam name="TSlave">      Type of the slave.</typeparam>
        /// <typeparam name="TProductLink">Type of the product link.</typeparam>
        /// <typeparam name="TStoreLink">  Type of the store link.</typeparam>
        /// <param name="cart">                 The cart.</param>
        /// <param name="summary">              The summary.</param>
        /// <param name="kind">                 The kind.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="response">             The response.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it changes the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> ProcessSummaryWithMinimumDollarFreeShippingAsync<TMaster, TSlave, TProductLink, TStoreLink>(
                ICartModel cart,
                Summary summary,
                string kind,
                IPricingFactoryContextModel pricingFactoryContext,
                CEFActionResponse response,
                string? contextProfileName)
            where TMaster : IBase
            where TSlave : IBase
            where TProductLink : class, IAmAProductRelationshipTableWhereProductIsTheSlave<TMaster>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheMaster<TSlave>
        {
            if (!Contract.CheckValidID(summary.FreeShippingDollarAmount))
            {
                // There was no min purchase amount value, move on
                return false;
            }
            var attrKey = $"FreeShipBy:Dol:IgnAcc:{kind}:{summary.ID}";
            var cacheKey = $"HardSoftStops:{attrKey}";
            var cacheKeyStores = $"{cacheKey}-Stores";
            var dollarInCart = 0m;
            var needsToBeMet = false;
            var previouslyMet = false;
            bool cartMeetsDollar;
            var minPurchaseDollar = summary.FreeShippingDollarAmount!.Value;
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            var linkHashesStores = client is null ? null : await client.GetAsync<HashSet<(int?, string)>>(cacheKeyStores).ConfigureAwait(false);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var linkHashes = client is null ? null : await client.GetAsync<HashSet<int>>(cacheKey).ConfigureAwait(false);
                if (linkHashes == null)
                {
                    linkHashes = new(
                        context.Set<TProductLink>()
                            .AsNoTracking()
                            .Where(x => x.Active && x.MasterID == summary.ID)
                            .OrderBy(x => x.SlaveID)
                            .Select(x => x.SlaveID));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKey, linkHashes, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                if (linkHashesStores == null)
                {
                    linkHashesStores = new(
                        (await context.Set<TStoreLink>()
                                .AsNoTracking()
                                .Where(x => x.Active && x.SlaveID == summary.ID)
                                .OrderBy(x => x.MasterID)
                                .Select(x => new { x.MasterID, x.Master!.Name })
                                .ToListAsync()
                                .ConfigureAwait(false))
                            .Select(x => ((int?)x.MasterID, x.Name!)));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKeyStores, linkHashesStores, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                foreach (var cartItem in cart.SalesItems!.Where(c => c.ProductID.HasValue && linkHashes.Contains(c.ProductID.Value)))
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
                // ReSharper disable HeuristicUnreachableCode, ConditionIsAlwaysTrueOrFalse
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
                        .SumAsync()
                        .ConfigureAwait(false);
                    previouslyMet = previousAmount >= minPurchaseDollarAfter;
                }
                cartMeetsDollar = previouslyMet
                    ? dollarInCart >= minPurchaseDollarAfter
                    : dollarInCart >= minPurchaseDollar;
                // ReSharper restore HeuristicUnreachableCode, ConditionIsAlwaysTrueOrFalse
            }
            // Send messaging if not meeting
            if (cartMeetsDollar)
            {
                return false;
            }
            var (relatedStoreID, relatedStoreName) = linkHashesStores.Count > 0
                ? linkHashesStores.First()
                : (null, null);
            if (cart.SerializableAttributes.ContainsKey(attrKey)
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
                            relatedBrandID: relatedStoreID,
                            relatedBrandName: relatedStoreName ?? string.Empty,
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
                    $"WARNING! The dollar amount of products in the cart for \"{summary.Name}\" doesn't meet the"
                    + $" requirements set by the store administrators for {kind}s to get free shipping, however "
                    + " the warning has been ignored.");
                return false;
            }
            if (!Contract.CheckValidKey(summary.FreeShippingDollarAmountWarningMessage))
            {
                response.Messages.Add(
                    $"ERROR! The minimum purchase requirements for {kind} '{summary.Name}' to get free shipping have not been met!");
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
                    relatedBrandID: relatedStoreID,
                    relatedBrandName: relatedStoreName ?? string.Empty,
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
            // SUCCESS! The dollar amount of products in the cart for "{summary.Name}" meets the requirements set by the store administrators for {kind}s.
            return false;
        }

        /// <summary>Process the summary with minimum quantity free shipping.</summary>
        /// <typeparam name="TMaster">     Type of the master.</typeparam>
        /// <typeparam name="TSlave">      Type of the slave.</typeparam>
        /// <typeparam name="TProductLink">Type of the product link.</typeparam>
        /// <typeparam name="TStoreLink">  Type of the store link.</typeparam>
        /// <param name="cart">                 The cart.</param>
        /// <param name="summary">              The summary.</param>
        /// <param name="kind">                 The kind.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="response">             The response.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual async Task<bool> ProcessSummaryWithMinimumQuantityFreeShippingAsync<TMaster, TSlave, TProductLink, TStoreLink>(
                ICartModel cart,
                Summary summary,
                string kind,
                IPricingFactoryContextModel pricingFactoryContext,
                CEFActionResponse response,
                string? contextProfileName)
            where TMaster : IBase
            where TSlave : IBase
            where TProductLink : class, IAmAProductRelationshipTableWhereProductIsTheSlave<TMaster>
            where TStoreLink : class, IAmAStoreRelationshipTableWhereStoreIsTheMaster<TSlave>
        {
            if (!Contract.CheckValidID(summary.FreeShippingQuantityAmount))
            {
                // There was no min purchase amount value, move on
                return false;
            }
            var attrKey = $"FreeShipBy:Qty:IgnAcc:{kind}:{summary.ID}";
            var cacheKey = $"HardSoftStops:{attrKey}";
            var cacheKeyStores = $"{cacheKey}-Stores";
            var quantityInCart = 0m;
            var needsToBeMet = false;
            var previouslyMet = false;
            bool cartMeetsQuantity;
            var minPurchaseQuantity = summary.FreeShippingQuantityAmount!.Value;
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            var linkHashesStores = client is null ? null : await client.GetAsync<HashSet<(int?, string)>>(cacheKeyStores).ConfigureAwait(false);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var linkHashesProducts = client is null ? null : await client.GetAsync<HashSet<int>>(cacheKey).ConfigureAwait(false);
                if (linkHashesProducts == null)
                {
                    linkHashesProducts = new(
                        context.Set<TProductLink>()
                            .AsNoTracking()
                            .Where(x => x.Active && x.MasterID == summary.ID)
                            .OrderBy(x => x.SlaveID)
                            .Select(x => x.SlaveID));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKey, linkHashesProducts, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                if (linkHashesStores == null)
                {
                    linkHashesStores = new(
                        (await context.Set<TStoreLink>()
                                .AsNoTracking()
                                .Where(x => x.Active && x.SlaveID == summary.ID)
                                .OrderBy(x => x.MasterID)
                                .Select(x => new { x.MasterID, x.Master!.Name })
                                .ToListAsync()
                                .ConfigureAwait(false))
                            .Select(x => ((int?)x.MasterID, x.Name!)));
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKeyStores, linkHashesStores, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
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
                            summary.ID,
                            kind,
                            pricingFactoryContext.AccountID,
                            pricingFactoryContext.AccountKey,
                            pricingFactoryContext.UserID,
                            pricingFactoryContext.UserKey,
                            context)
                        .Select(x => x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m))
                        .DefaultIfEmpty(0m)
                        .SumAsync()
                        .ConfigureAwait(false);
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
            var (relatedStoreID, relatedStoreName) = linkHashesStores.Count > 0
                ? linkHashesStores.First()
                : (null, null);
            if (cart.SerializableAttributes.ContainsKey(attrKey)
                && cart.SerializableAttributes.TryGetValue(attrKey, out var ignoredValue)
                && bool.TryParse(ignoredValue.Value, out var ignoredValueParsed)
                && ignoredValueParsed)
            {
                if (Contract.CheckValidKey(summary.FreeShippingQuantityAmountIgnoredAcceptedMessage))
                {
                    response.Messages.Add(
                        "WARNING! "
                        + DoReplacementsFreeShipping(
                            warningMessage: summary.FreeShippingQuantityAmountIgnoredAcceptedMessage ?? string.Empty,
                            ownerID: summary.ID,
                            ownerName: summary.Name ?? string.Empty,
                            attrKey: attrKey,
                            requiredAmount: $"{minPurchaseQuantity:n0}",
                            missingAmount: $"{minPurchaseQuantity - quantityInCart:n0}",
                            ignoredMessage: summary.FreeShippingQuantityAmountWarningMessage ?? string.Empty,
                            relatedBrandID: relatedStoreID,
                            relatedBrandName: relatedStoreName ?? string.Empty,
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
                    $"WARNING! The quantity amount of products in the cart for {kind} \"{summary.Name}\" doesn't"
                    + " meet the requirements set by the store administrators to get free shipping, however the"
                    + " warning has been ignored.");
                return false;
            }
            if (!Contract.CheckValidKey(summary.FreeShippingQuantityAmountWarningMessage))
            {
                response.Messages.Add(
                    $"ERROR! The minimum purchase requirements for {kind} \"{summary.Name}\" have not been met for free shipping!");
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
                    relatedBrandID: relatedStoreID,
                    relatedBrandName: relatedStoreName ?? string.Empty,
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
            // SUCCESS! The quantity amount of products in the cart for "{summary.Name}" meets the requirements set by the store administrators for {kind}s
            return false;
        }

        /// <summary>Executes the replacements free shipping operation.</summary>
        /// <param name="warningMessage">      Message describing the warning.</param>
        /// <param name="ownerID">             The identifier that owns this item.</param>
        /// <param name="ownerName">           The name that owns this item.</param>
        /// <param name="attrKey">             The attribute key.</param>
        /// <param name="requiredAmount">      The required amount.</param>
        /// <param name="missingAmount">       The missing amount.</param>
        /// <param name="ignoredMessage">      Message describing the ignored.</param>
        /// <param name="relatedBrandID">      Identifier for the related Brand.</param>
        /// <param name="relatedBrandName">    Name of the related Brand.</param>
        /// <param name="bufferCategoryName">  Name of the buffer category.</param>
        /// <param name="bufferCategorySeoUrl">SEO URL of the buffer category.</param>
        /// <param name="bufferItemName">      Name of the buffer item.</param>
        /// <param name="bufferItemSeoUrl">    SEO URL of the buffer item.</param>
        /// <returns>A string.</returns>
        protected virtual string DoReplacementsFreeShipping(
            string warningMessage,
            int ownerID,
            string ownerName,
            string attrKey,
            string requiredAmount,
            string missingAmount,
            string ignoredMessage,
            int? relatedBrandID,
            string relatedBrandName,
            string bufferCategoryName,
            string bufferCategorySeoUrl,
            string bufferItemName,
            string bufferItemSeoUrl)
        {
            return warningMessage
                .Replace(
                    "{{checkbox}}",
                    @"type=""checkbox"" ng-change=""cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)"" ng-model=""cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['{{attrKey}}'].Value"" ng-true-value=""'true'"" ng-false-value=""'false'""")
                .Replace("{{attrKey}}", attrKey)
                .Replace("{{ignoredMessage}}", ignoredMessage)
                .Replace("{{ownerID}}", ownerID.ToString())
                .Replace("{{ownerName}}", ownerName)
                .Replace("{{requiredAmount}}", requiredAmount)
                .Replace("{{missingAmount}}", missingAmount)
                .Replace("{{relatedBrandID}}", relatedBrandID > 0 ? relatedBrandID.ToString() : string.Empty)
                .Replace("{{relatedBrandName}}", relatedBrandName)
                .Replace("{{bufferCategoryName}}", bufferCategoryName)
                .Replace("{{bufferCategorySeoUrl}}", bufferCategorySeoUrl)
                .Replace("{{bufferItemName}}", bufferItemName)
                .Replace("{{bufferItemSeoUrl}}", bufferItemSeoUrl);
        }

        /// <summary>Process the global with minimum dollar free shipping.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="response">             The response.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it changes the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> ProcessGlobalWithMinimumDollarFreeShippingAsync(
            ICartModel cart,
            IPricingFactoryContextModel pricingFactoryContext,
            CEFActionResponse response,
            string? contextProfileName)
        {
            if (!CEFConfigDictionary.ShippingRatesFreeThresholdGlobalEnabled
                || CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmount <= 0m)
            {
                // There was no min purchase amount value, move on
                return false;
            }
            var attrKey = "FreeShipBy:Dol:IgnAcc:Global";
            var dollarInCart = 0m;
            var needsToBeMet = false;
            var previouslyMet = false;
            bool cartMeetsDollar;
            var minPurchaseDollar = CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmount!.Value;
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                foreach (var cartItem in cart.SalesItems!)
                {
                    needsToBeMet = true;
                    dollarInCart += cartItem.ExtendedPrice;
                }
                if (!needsToBeMet)
                {
                    // Move on to next Summary
                    return false;
                }
                var minPurchaseDollarAfter = CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountAfter is >= 0
                    ? CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountAfter.Value
                    : minPurchaseDollar;
                // ReSharper disable HeuristicUnreachableCode, ConditionIsAlwaysTrueOrFalse
                if (Contract.CheckValidIDOrKey(pricingFactoryContext.AccountID, pricingFactoryContext.AccountKey)
                    || Contract.CheckValidIDOrKey(pricingFactoryContext.UserID, pricingFactoryContext.UserKey))
                {
                    var previousAmount = await PrepHistoryQuery(
                            summaryID: 0,
                            kind: "Global",
                            accountID: pricingFactoryContext.AccountID,
                            accountKey: pricingFactoryContext.AccountKey,
                            userID: pricingFactoryContext.UserID,
                            userKey: pricingFactoryContext.UserKey,
                            context: context)
                        .Select(x => (x.UnitSoldPrice ?? x.UnitCorePrice)
                            * (x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m)))
                        .DefaultIfEmpty(0m)
                        .SumAsync()
                        .ConfigureAwait(false);
                    previouslyMet = previousAmount >= minPurchaseDollarAfter;
                }
                cartMeetsDollar = previouslyMet
                    ? dollarInCart >= minPurchaseDollarAfter
                    : dollarInCart >= minPurchaseDollar;
                // ReSharper restore HeuristicUnreachableCode, ConditionIsAlwaysTrueOrFalse
            }
            // Send messaging if not meeting
            if (cartMeetsDollar)
            {
                return false;
            }
            if (cart.SerializableAttributes.ContainsKey(attrKey)
                && cart.SerializableAttributes.TryGetValue(attrKey, out var ignoreValue)
                && bool.TryParse(ignoreValue.Value, out var ignoreValueParsed)
                && ignoreValueParsed)
            {
                if (Contract.CheckValidKey(CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountIgnoredAcceptedMessage))
                {
                    response.Messages.Add(
                        "WARNING! "
                        + DoReplacementsFreeShipping(
                            warningMessage: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountIgnoredAcceptedMessage!,
                            ownerID: 0,
                            ownerName: CEFConfigDictionary.CompanyName,
                            attrKey: attrKey,
                            requiredAmount: $"{minPurchaseDollar:c}",
                            missingAmount: $"{minPurchaseDollar - dollarInCart:c}",
                            ignoredMessage: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountWarningMessage ?? string.Empty,
                            relatedBrandID: null,
                            relatedBrandName: string.Empty,
                            bufferCategoryName: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferCategoryDisplayName ?? CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferCategoryName ?? string.Empty,
                            bufferCategorySeoUrl: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferCategorySeoUrl ?? string.Empty,
                            bufferItemName: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferProductName ?? string.Empty,
                            bufferItemSeoUrl: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferProductSeoUrl ?? string.Empty));
                    if (Contract.CheckValidKey(CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferCategorySeoUrl))
                    {
                        response.Messages.Add(CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferCategorySeoUrl!);
                    }
                    if (Contract.CheckValidKey(CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferProductSeoUrl))
                    {
                        response.Messages.Add(CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferProductSeoUrl!);
                    }
                    return false;
                }
                response.Messages.Add(
                    "WARNING! The dollar amount of products in the cart doesn't meet the"
                    + " requirements set by the store administrators to get free shipping, however "
                    + " the warning has been ignored.");
                return false;
            }
            if (!Contract.CheckValidKey(CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountWarningMessage))
            {
                response.Messages.Add(
                    "ERROR! The minimum purchase requirements to get free shipping have not been met!");
                return false;
            }
            response.Messages.Add(
                "ERROR! "
                + DoReplacementsFreeShipping(
                    warningMessage: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountWarningMessage!,
                    ownerID: 0,
                    ownerName: CEFConfigDictionary.CompanyName,
                    attrKey: attrKey,
                    requiredAmount: $"{minPurchaseDollar:c}",
                    missingAmount: $"{minPurchaseDollar - dollarInCart:c}",
                    ignoredMessage: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountWarningMessage ?? string.Empty,
                    relatedBrandID: null,
                    relatedBrandName: string.Empty,
                    bufferCategoryName: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferCategoryDisplayName ?? CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferCategoryName ?? string.Empty,
                    bufferCategorySeoUrl: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferCategorySeoUrl ?? string.Empty,
                    bufferItemName: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferProductName ?? string.Empty,
                    bufferItemSeoUrl: CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferProductSeoUrl ?? string.Empty));
            if (Contract.CheckValidKey(CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferCategorySeoUrl))
            {
                response.Messages.Add(CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferCategorySeoUrl!);
            }
            if (Contract.CheckValidKey(CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferProductSeoUrl))
            {
                response.Messages.Add(CEFConfigDictionary.ShippingRatesFreeThresholdGlobalAmountBufferProductSeoUrl!);
            }
            // Do nothing
            // SUCCESS! The dollar amount of products in the cart for "{summary.Name}" meets the requirements set by the store administrators for {kind}s.
            return false;
        }
    }
}
