// <copyright file="ISampleRequestCheckoutProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISampleRequestCheckoutProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.SampleRequestHandlers.Checkouts
{
    using System.Threading.Tasks;
    using Models;
    using Taxes;

    /// <summary>Interface for sample request checkout provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface ISampleRequestCheckoutProviderBase : IProviderBase
    {
        /// <summary>Performs a Checkout for the Sample Request.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="cartType">             Type of the cart.</param>
        /// <param name="taxesProvider">        The taxes provider.</param>
        /// <param name="isTaxable">            true if this ISampleRequestCheckoutWorkflow is taxable.</param>
        /// <param name="currentUserID">        Identifier for the current user.</param>
        /// <param name="currentAccountID">     Identifier for the current account.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICheckoutResult.</returns>
        Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            string cartType,
            ITaxesProviderBase? taxesProvider,
            bool isTaxable,
            int? currentUserID,
            int? currentAccountID,
            string? contextProfileName);
    }
}
