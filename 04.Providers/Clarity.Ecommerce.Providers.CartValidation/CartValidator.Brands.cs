// <copyright file="CartValidator.Brands.cs" company="clarity-ventures.com">
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
        /// <summary>Process the brand with minimums.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="summary">              The brand.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="response">             The response.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it changes the cart, false if it doesn't.</returns>
        protected virtual async Task<bool> ProcessBrandWithMinimumsAsync(
            ICartModel cart,
            Summary summary,
            IPricingFactoryContextModel pricingFactoryContext,
            CEFActionResponse response,
            string? contextProfileName)
        {
            var temp1 = await ProcessSummaryWithMinimumDollarAsync<Brand, Product, BrandProduct, StoreProduct>(cart, summary, nameof(Brand), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp2 = await ProcessSummaryWithMinimumQuantityAsync<Brand, Product, BrandProduct, StoreProduct>(cart, summary, nameof(Brand), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp3 = await ProcessSummaryWithMinimumDollarFreeShippingAsync<Brand, Product, BrandProduct, StoreProduct>(cart, summary, nameof(Brand), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            var temp4 = await ProcessSummaryWithMinimumQuantityFreeShippingAsync<Brand, Product, BrandProduct, StoreProduct>(cart, summary, nameof(Brand), pricingFactoryContext, response, contextProfileName).ConfigureAwait(false);
            return temp1 || temp2 || temp3 || temp4;
        }

        /// <summary>Process the brands with minimums.</summary>
        /// <param name="response">             The response.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>True if it changes the cart, false if it doesn't.</returns>
        protected virtual Task<bool> ProcessBrandsWithMinimumsAsync(
            CEFActionResponse response,
            ICartModel cart,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            return ProcessRecordsWithMinimumsAsync<Brand, BrandProduct>(
                response,
                cart,
                pricingFactoryContext,
                Config!.DoBrandRestrictionsByMinMax,
                ProcessBrandWithMinimumsAsync,
                contextProfileName);
        }
    }
}
