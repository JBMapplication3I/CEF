// <copyright file="IPricingFactory.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPricingFactory interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Pricing
{
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for pricing factory.</summary>
    public interface IPricingFactory
    {
        /// <summary>Gets the default pricing factory context.</summary>
        /// <value>The default pricing factory context.</value>
        IPricingFactoryContextModel DefaultPricingFactoryContext { get; }

        /// <summary>Gets options for controlling the operation.</summary>
        /// <value>The settings.</value>
        IPricingFactorySettings Settings { get; }

        /// <summary>Calculates the price using the product id, will pull the product context internally.</summary>
        /// <param name="productID">          Identifier for the product.</param>
        /// <param name="salesItemAttributes">The sales item attributes.</param>
        /// <param name="pricingFactoryContext">            The context.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <param name="pricingProvider">    The pricing provider.</param>
        /// <param name="forceNoCache">       True to force no cache.</param>
        /// <param name="forCart">            Flag for cart item price calculation.</param>
        /// <returns>The calculated price.</returns>
        Task<ICalculatedPrice> CalculatePriceAsync(
            int productID,
            SerializableAttributesDictionary? salesItemAttributes,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName,
            IPricingProviderBase? pricingProvider = null,
            bool forceNoCache = false,
            bool? forCart = false);

        /// <summary>Removes all cached prices by product identifier described by product identifier.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task RemoveAllCachedPricesByProductIDAsync(int productID, string? contextProfileName);

        /// <summary>Removes all cached prices.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task RemoveAllCachedPricesAsync(string? contextProfileName);
    }
}
