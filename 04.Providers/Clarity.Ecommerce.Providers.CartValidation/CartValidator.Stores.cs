// <copyright file="CartValidator.Stores.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator class</summary>
namespace Clarity.Ecommerce.Providers.CartValidation
{
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Models;

    /// <summary>A cart validator.</summary>
    public partial class CartValidator
    {
        /// <summary>Process the store with minimums.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="summary">              The store.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="response">             The response.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it changes the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> ProcessStoreWithMinimumsAsync(
            ICartModel cart,
            Summary summary,
            IPricingFactoryContextModel pricingFactoryContext,
            CEFActionResponse response,
            string? contextProfileName)
        {
            var temp1 = await ProcessSummaryWithMinimumDollarAsync<Store, Product, StoreProduct, StoreProduct>(cart, summary, nameof(Store), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp2 = await ProcessSummaryWithMinimumQuantityAsync<Store, Product, StoreProduct, StoreProduct>(cart, summary, nameof(Store), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp3 = await ProcessSummaryWithMinimumDollarFreeShippingAsync<Store, Product, StoreProduct, StoreProduct>(cart, summary, nameof(Store), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp4 = await ProcessSummaryWithMinimumQuantityFreeShippingAsync<Store, Product, StoreProduct, StoreProduct>(cart, summary, nameof(Store), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            return temp1 || temp2 || temp3 || temp4;
        }

        /// <summary>Process the stores with minimums.</summary>
        /// <param name="response">             The response.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it changes the cart, false if it doesn't.</returns>
        protected virtual Task<bool> ProcessStoresWithMinimumsAsync(
            CEFActionResponse response,
            ICartModel cart,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            return ProcessRecordsWithMinimumsAsync<Store, StoreProduct>(
                response,
                cart,
                pricingFactoryContext,
                Config!.DoStoreRestrictionsByMinMax,
                ProcessStoreWithMinimumsAsync,
                contextProfileName);
        }
    }
}
