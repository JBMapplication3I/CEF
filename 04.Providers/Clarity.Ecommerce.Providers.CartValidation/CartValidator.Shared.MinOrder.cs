// <copyright file="CartValidator.Shared.MinOrder.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator class</summary>
// ReSharper disable BadParensLineBreaks, CognitiveComplexity, CyclomaticComplexity, InvertIf, MergeSequentialChecks, MultipleSpaces
namespace Clarity.Ecommerce.Providers.CartValidation
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A cart validator.</summary>
    public partial class CartValidator
    {
        /// <summary>Process the summary with minimum dollar.</summary>
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
        protected virtual async Task<bool> ProcessSummaryWithMinimumDollarAsync<TMaster, TSlave, TProductLink, TStoreLink>(
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
            if (!Contract.CheckValidID(summary.DollarAmount))
            {
                // There was no min purchase amount value, move on
                return false;
            }
            var attrKey = $"MinOrderBy:Dol:IgnAcc:{kind}:{summary.ID}";
            var cacheKey = $"HardSoftStops:{attrKey}";
            var cacheKeyStores = $"{cacheKey}-Stores";
            var dollarInCart = 0m;
            var needsToBeMet = false;
            var previouslyMet = false;
            bool cartMeetsDollar;
            var minPurchaseDollar = summary.DollarAmount!.Value;
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
                if (kind == nameof(Store))
                {
                    linkHashesStores = new();
                    if (client is not null)
                    {
                        await client.AddAsync(cacheKeyStores, linkHashesStores, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
                    }
                }
                else if (linkHashesStores == null)
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
                        await client.AddAsync(cacheKeyStores, linkHashesStores, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false); // Save the cache
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
                var minPurchaseDollarAfter = summary.DollarAmountAfter is >= 0
                    ? summary.DollarAmountAfter.Value
                    : minPurchaseDollar;
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
                        .Select(x => (x.UnitSoldPrice ?? x.UnitCorePrice)
                                   * (x.Quantity + (x.QuantityBackOrdered ?? 0m) + (x.QuantityPreSold ?? 0m)))
                        .DefaultIfEmpty(0m)
                        .SumAsync()
                        .ConfigureAwait(false);
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
            var (relatedStoreID, relatedStoreName) = linkHashesStores.Count > 0
                ? linkHashesStores.First()
                : (null, null);
            if (summary.DollarAmountOverrideFee >= 0m
                && cart.SerializableAttributes.ContainsKey(attrKey)
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
                            relatedBrandID: relatedStoreID,
                            relatedBrandName: relatedStoreName,
                            bufferCategoryName: summary.DollarAmountBufferCategoryDisplayName ?? summary.DollarAmountBufferCategoryName ?? string.Empty,
                            bufferCategorySeoUrl: summary.DollarAmountBufferCategorySeoUrl ?? string.Empty,
                            bufferItemName: summary.DollarAmountBufferProductName ?? string.Empty,
                            bufferItemSeoUrl: summary.DollarAmountBufferProductSeoUrl ?? string.Empty));
                    return false;
                }
                response.Messages.Add(
                    $"WARNING! The dollar amount of products in the cart for \"{summary.Name}\" doesn't meet the"
                    + $" requirements set by the store administrators for {kind}s, however the Override Fee of"
                    + $" {overrideFee} has been accepted.");
                return false;
            }
            if (!Contract.CheckValidKey(summary.DollarAmountWarningMessage))
            {
                if (summary.DollarAmountOverrideFee >= 0m
                    && !Contract.CheckValidKey(summary.DollarAmountOverrideFeeWarningMessage))
                {
                    response.Messages.Add(
                        $"ERROR! The minimum purchase requirements for {kind} \"{summary.Name}\" have not been"
                        + $" met! However, an override option is available for a fee of {overrideFee}.");
                    return false;
                }
                response.Messages.Add(
                    $"ERROR! The minimum purchase requirements for {kind} '{summary.Name}' have not been met!");
                return false;
            }
            response.Messages.Add(
                "ERROR! "
                + DoReplacementsMinOrders(
                    warningMessage: summary.DollarAmountWarningMessage!,
                    ownerID: summary.ID,
                    ownerName: summary.Name,
                    attrKey: attrKey,
                    requiredAmount: $"{minPurchaseDollar:c}",
                    missingAmount: $"{minPurchaseDollar - dollarInCart:c}",
                    overrideFeeWarningMessage: summary.DollarAmountOverrideFeeWarningMessage,
                    overrideFee: overrideFee,
                    relatedBrandID: relatedStoreID,
                    relatedBrandName: relatedStoreName,
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
            // SUCCESS! The dollar amount of products in the cart for "{summary.Name}" meets the requirements set by the store administrators for {kind}s
            return false;
        }

        /// <summary>Process the summary with minimum quantity.</summary>
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
        protected virtual async Task<bool> ProcessSummaryWithMinimumQuantityAsync<TMaster, TSlave, TProductLink, TStoreLink>(
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
            if (!Contract.CheckValidID(summary.QuantityAmount))
            {
                // There was no min purchase amount value, move on
                return false;
            }
            var attrKey = $"MinOrderBy:Qty:IgnAcc:{kind}:{summary.ID}";
            var cacheKey = $"HardSoftStops:{attrKey}";
            var cacheKeyStores = $"{cacheKey}-Stores";
            var quantityInCart = 0m;
            var needsToBeMet = false;
            var previouslyMet = false;
            bool cartMeetsQuantity;
            var minPurchaseQuantity = summary.QuantityAmount!.Value;
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
                                .OrderBy(x => x.SlaveID)
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
                    quantityInCart += cartItem.TotalQuantity;
                }
                if (!needsToBeMet)
                {
                    return false;
                } // Move on to next Summary
                var minPurchaseQuantityAfter = summary.QuantityAmountAfter is >= 0
                    ? summary.QuantityAmountAfter.Value
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
            var overrideFee = summary.QuantityAmountOverrideFeeIsPercent
                ? ((summary.QuantityAmountOverrideFee ?? 0m) / 100).ToString("p0")
                : (summary.QuantityAmountOverrideFee ?? 0m).ToString("c2");
            var (relatedStoreID, relatedStoreName) = linkHashesStores.Count > 0
                ? linkHashesStores.First()
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
                            requiredAmount: $"{minPurchaseQuantity:n0}",
                            missingAmount: $"{minPurchaseQuantity - quantityInCart:n0}",
                            overrideFeeWarningMessage: summary.QuantityAmountOverrideFeeWarningMessage,
                            overrideFee: overrideFee,
                            relatedBrandID: relatedStoreID,
                            relatedBrandName: relatedStoreName,
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
                    $"WARNING! The quantity amount of products in the cart for {kind} \"{summary.Name}\" doesn't"
                    + " meet the requirements set by the store administrators, however the Override Fee of"
                    + $" {overrideFee} has been accepted.");
                return false;
            }
            if (!Contract.CheckValidKey(summary.QuantityAmountWarningMessage))
            {
                if (summary.QuantityAmountOverrideFee >= 0m
                    && !Contract.CheckValidKey(summary.QuantityAmountOverrideFeeWarningMessage))
                {
                    response.Messages.Add(
                        $"ERROR! The minimum purchase requirements for {kind} \"{summary.Name}\" have not been"
                        + $" met! However, an override option is available for a fee of {overrideFee}.");
                    return false;
                }
                response.Messages.Add(
                    $"ERROR! The minimum purchase requirements for {kind} \"{summary.Name}\" have not been met!");
                return false;
            }
            response.Messages.Add(
                "ERROR! "
                + DoReplacementsMinOrders(
                    warningMessage: summary.QuantityAmountWarningMessage!,
                    ownerID: summary.ID,
                    ownerName: summary.Name,
                    attrKey: attrKey,
                    requiredAmount: $"{minPurchaseQuantity:n0}",
                    missingAmount: $"{minPurchaseQuantity - quantityInCart:n0}",
                    overrideFeeWarningMessage: summary.QuantityAmountOverrideFeeWarningMessage,
                    overrideFee: overrideFee,
                    relatedBrandID: relatedStoreID,
                    relatedBrandName: relatedStoreName,
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
            // SUCCESS! The quantity amount of products in the cart for "{summary.Name}" meets the requirements set by the store administrators for {kind}s
            return false;
        }

        /// <summary>Process the records with minimums.</summary>
        /// <typeparam name="TEntity">     Type of the entity.</typeparam>
        /// <typeparam name="TProductLink">Type of the product link.</typeparam>
        /// <param name="response">                The response.</param>
        /// <param name="cart">                    The cart.</param>
        /// <param name="pricingFactoryContext">   Context for the pricing factory.</param>
        /// <param name="do">                      True to do.</param>
        /// <param name="processWithMinimumsAsync">The process with minimums (async).</param>
        /// <param name="contextProfileName">      Name of the context profile.</param>
        /// <returns>True if it changes the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> ProcessRecordsWithMinimumsAsync<TEntity, TProductLink>(
                CEFActionResponse response,
                ICartModel cart,
                IPricingFactoryContextModel pricingFactoryContext,
                bool @do,
                Func<ICartModel, Summary, IPricingFactoryContextModel, CEFActionResponse, string?, Task<bool>> processWithMinimumsAsync,
                string? contextProfileName)
            where TEntity : class, INameableBase, IHaveOrderMinimumsBase, IHaveFreeShippingMinimumsBase, IAmFilterableByProduct<TProductLink>
            where TProductLink : class, IBase, IAmAProductRelationshipTableWhereProductIsTheSlave<TEntity>
        {
            if (!@do)
            {
                // SUCCESS! There are no {kind}s with restrictions
                return false;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var withMinimums = (await context.Set<TEntity>()
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
                .Select(x => new Summary
                {
                    ID = x.ID,
                    Name = x.Name,
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
                // SUCCESS! There are no {kind}s with restrictions
                return false;
            }
            var changedCart = false;
            foreach (var summary in withMinimums.Where(x => !checkedIDs.Contains(x.ID)))
            {
                checkedIDs.Add(summary.ID);
                changedCart |= await processWithMinimumsAsync(
                        cart,
                        summary,
                        pricingFactoryContext,
                        response,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            return changedCart;
        }

        /// <summary>Executes the replacements operation.</summary>
        /// <param name="warningMessage">           Message describing the warning.</param>
        /// <param name="ownerID">                  The identifier that owns this item.</param>
        /// <param name="ownerName">                The name that owns this item.</param>
        /// <param name="attrKey">                  The attribute key.</param>
        /// <param name="requiredAmount">           The required amount.</param>
        /// <param name="missingAmount">            The missing amount.</param>
        /// <param name="overrideFeeWarningMessage">Message describing the override fee warning.</param>
        /// <param name="overrideFee">              The override fee.</param>
        /// <param name="relatedBrandID">           Identifier for the related brand.</param>
        /// <param name="relatedBrandName">         Name of the related brand.</param>
        /// <param name="bufferCategoryName">       Name of the buffer category.</param>
        /// <param name="bufferCategorySeoUrl">     SEO URL of the buffer category.</param>
        /// <param name="bufferItemName">           Name of the buffer item.</param>
        /// <param name="bufferItemSeoUrl">         SEO URL of the buffer item.</param>
        /// <returns>A string.</returns>
        protected virtual string DoReplacementsMinOrders(
            string warningMessage,
            int ownerID,
            string? ownerName,
            string? attrKey,
            string? requiredAmount,
            string? missingAmount,
            string? overrideFeeWarningMessage,
            string? overrideFee,
            int? relatedBrandID,
            string? relatedBrandName,
            string? bufferCategoryName,
            string? bufferCategorySeoUrl,
            string? bufferItemName,
            string? bufferItemSeoUrl)
        {
            return warningMessage
                .Replace("{{overrideFeeWarningMessage}}", overrideFeeWarningMessage)
                .Replace(
                    "{{checkbox}}",
                    @"type=""checkbox"" ng-change=""cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)"" ng-model=""cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['{{attrKey}}'].Value"" ng-true-value=""'true'"" ng-false-value=""'false'""")
                .Replace("{{attrKey}}", attrKey)
                .Replace("{{ownerID}}", ownerID.ToString())
                .Replace("{{ownerName}}", ownerName)
                .Replace("{{requiredAmount}}", requiredAmount)
                .Replace("{{missingAmount}}", missingAmount)
                .Replace("{{overrideFee}}", overrideFee)
                .Replace("{{relatedBrandID}}", relatedBrandID.HasValue ? relatedBrandID.ToString() : string.Empty)
                .Replace("{{relatedBrandName}}", relatedBrandName)
                .Replace("{{bufferCategoryName}}", bufferCategoryName)
                .Replace("{{bufferCategorySeoUrl}}", bufferCategorySeoUrl)
                .Replace("{{bufferItemName}}", bufferItemName)
                .Replace("{{bufferItemSeoUrl}}", bufferItemSeoUrl);
        }

        /// <summary>Prep history query.</summary>
        /// <param name="summaryID"> Identifier for the summary.</param>
        /// <param name="kind">      The kind.</param>
        /// <param name="accountID"> Identifier for the account.</param>
        /// <param name="accountKey">The account key.</param>
        /// <param name="userID">    Identifier for the user.</param>
        /// <param name="userKey">   The user key.</param>
        /// <param name="context">   The context.</param>
        /// <returns>An IQueryable{SalesOrderItem}.</returns>
        protected virtual IQueryable<SalesOrderItem> PrepHistoryQuery(
            int summaryID,
            string kind,
            int? accountID,
            string? accountKey,
            int? userID,
            string? userKey,
            IClarityEcommerceEntities context)
        {
            var query = context.SalesOrderItems
                .AsNoTracking()
                .Where(x => x.Active && x.Master!.Active && x.ProductID.HasValue && x.Product!.Active);
            switch (kind)
            {
                case nameof(Category):
                {
                    query = query.Where(x => x.Product!.Categories!.Any(y => y.Active && y.SlaveID == summaryID));
                    break;
                }
                case nameof(Manufacturer):
                {
                    query = query.Where(x => x.Product!.Manufacturers!.Any(y => y.Active && y.MasterID == summaryID));
                    break;
                }
                case nameof(Vendor):
                {
                    query = query.Where(x => x.Product!.Vendors!.Any(y => y.Active && y.MasterID == summaryID));
                    break;
                }
                case nameof(Store):
                {
                    query = query.Where(x => x.Product!.Stores!.Any(y => y.Active && y.MasterID == summaryID));
                    break;
                }
                case "Global":
                {
                    // No additional filter
                    break;
                }
                default:
                {
                    throw new InvalidOperationException("Unknown query kind");
                }
            }
            return Contract.CheckValidID(accountID)
                ? query.Where(x => x.Master!.AccountID == accountID!.Value
                                || x.Master.User != null && x.Master.User.AccountID == accountID.Value)
                : Contract.CheckValidKey(accountKey)
                    ? query.Where(x => x.Master!.Account != null && x.Master.Account.CustomKey == accountKey
                                    || x.Master.User != null && x.Master.User.Account != null && x.Master.User.Account.CustomKey == accountKey)
                    : Contract.CheckValidID(userID)
                        ? query.Where(x => x.Master!.UserID == userID!.Value)
                        : Contract.CheckValidKey(userKey)
                            ? query.Where(x => x.Master!.User != null && x.Master.User.CustomKey == userKey)
                            : query;
        }
    }
}
