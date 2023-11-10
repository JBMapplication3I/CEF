// <copyright file="ISalesQuoteSubmitProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesQuoteSubmitProviderBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Providers.SalesQuoteHandlers.Checkouts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;
    using Taxes;

    /// <summary>Interface for sales quote checkout provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface ISalesQuoteSubmitProviderBase : IProviderBase
    {
        /// <summary>Gets a value indicating whether this ISalesQuoteSubmitProviderBase is initialized.</summary>
        /// <value>True if this ISalesQuoteSubmitProviderBase is initialized, false if not.</value>
        bool IsInitialized { get; }

        /// <summary>Initializes the configuration of the provider, including passing in workflows and other required
        /// variables.</summary>
        /// <param name="quoteStatusPendingID">Identifier for the quote status pending.</param>
        /// <param name="quoteStatusOnHoldID"> Identifier for the quote status on hold.</param>
        /// <param name="quoteTypeID">         Identifier for the quote type.</param>
        /// <param name="quoteStateID">        Identifier for the quote state.</param>
        /// <param name="shippingTypeID">      Identifier for the shipping type.</param>
        /// <param name="customerNoteTypeID">  Identifier for the customer note type.</param>
        /// <param name="defaultCurrencyID">   The default currency identifier.</param>
        /// <param name="contextProfileName">  Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task InitAsync(
            int quoteStatusPendingID,
            int quoteStatusOnHoldID,
            int quoteTypeID,
            int quoteStateID,
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
        /// <returns>A <see cref="CEFActionResponse{TListICartModel}"/>.</returns>
        Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Analyzes to determine what kind of orders would be created.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A <see cref="CEFActionResponse{TListICartModel}"/>.</returns>
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
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICheckoutResult.</returns>
        Task<ICheckoutResult> SubmitAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Run the Checkout procedure defined by the provider.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICheckoutResult.</returns>
        Task<ICheckoutResult> SubmitAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Creates single via checkout process.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The new single via checkout process.</returns>
        Task<CEFActionResponse<int?>> CreateSingleViaCheckoutProcessAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
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
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);
    }
}
