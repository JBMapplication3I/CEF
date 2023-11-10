// <copyright file="IPricingProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPricingProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Pricing
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for pricing provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface IPricingProviderBase : IProviderBase
    {
        /// <summary>Calculates the price.</summary>
        /// <param name="factoryProduct">    The pricing factory product.</param>
        /// <param name="factoryContext">    Context for the pricing factory.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="forCart">           Flag for prices for cart caclulation.</param>
        /// <returns>The calculated price.</returns>
        Task<ICalculatedPrice> CalculatePriceAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext,
            string? contextProfileName,
            bool? forCart = false);

        /// <summary>Gets price key.</summary>
        /// <param name="factoryProduct"> The pricing factory product.</param>
        /// <param name="factoryContext"> Context for the pricing factory.</param>
        /// <returns>The price key.</returns>
        Task<string> GetPriceKeyAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext);

        /// <summary>Gets price key (alternate mode, as a stringified json object).</summary>
        /// <param name="factoryProduct"> The pricing factory product.</param>
        /// <param name="factoryContext"> Context for the pricing factory.</param>
        /// <returns>The price key.</returns>
        Task<string> GetPriceKeyAltAsync(
            IPricingFactoryProductModel factoryProduct,
            IPricingFactoryContextModel factoryContext);
    }
}
