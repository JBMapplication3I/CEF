// <copyright file="ICheckoutProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICheckoutProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Checkouts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;
    using Payments;
    using Taxes;

    /// <summary>Interface for checkout provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface ICheckoutProviderBase : IProviderBase
    {
        /// <summary>Gets a value indicating whether this ICheckoutProviderBase is initialized.</summary>
        /// <value>True if this ICheckoutProviderBase is initialized, false if not.</value>
        bool IsInitialized { get; }

        /// <summary>Initializes the configuration of the provider, including passing in workflows and other required
        /// variables.</summary>
        /// <param name="orderStatusPendingID">Identifier for the order status pending.</param>
        /// <param name="orderStatusPaidID">   Identifier for the order status paid.</param>
        /// <param name="orderStatusOnHoldID"> Identifier for the order status on hold.</param>
        /// <param name="orderTypeID">         Identifier for the order type.</param>
        /// <param name="orderStateID">        Identifier for the order state.</param>
        /// <param name="billingTypeID">       Identifier for the billing type.</param>
        /// <param name="shippingTypeID">      Identifier for the shipping type.</param>
        /// <param name="customerNoteTypeID">  Identifier for the customer note type.</param>
        /// <param name="defaultCurrencyID">   The default currency identifier.</param>
        /// <param name="contextProfileName">  Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task InitAsync(
            int orderStatusPendingID,
            int orderStatusPaidID,
            int orderStatusOnHoldID,
            int orderTypeID,
            int orderStateID,
            int billingTypeID,
            int shippingTypeID,
            int customerNoteTypeID,
            int defaultCurrencyID,
            string? contextProfileName);

        /// <summary>Analyzes to determine what kind of orders would be created.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{List_ICartModel}"/>.</returns>
        Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Analyzes to determine what kind of orders would be created.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="lookupKey">            Identifier for the cart.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{List_ICartModel}"/>.</returns>
        Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Run the Checkout procedure defined by the provider.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="taxesProvider">        The tax provider.</param>
        /// <param name="gateway">              The gateway.</param>
        /// <param name="selectedAccountID">     The current account identifier.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICheckoutResult.</returns>
        Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            int? selectedAccountID,
            string? contextProfileName);

        /// <summary>Run the Checkout procedure defined by the provider.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="gateway">              The gateway.</param>
        /// <param name="selectedAccountID">     The current account identifier.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICheckoutResult.</returns>
        Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            int? selectedAccountID,
            string? contextProfileName);

        /// <summary>Clears the analysis.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Clears the analysis.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            CartByIDLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);
    }
}
