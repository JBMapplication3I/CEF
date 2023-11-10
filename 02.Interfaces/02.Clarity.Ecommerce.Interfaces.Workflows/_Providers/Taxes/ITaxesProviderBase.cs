// <copyright file="ITaxesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITaxesProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Taxes
{
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for taxes provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface ITaxesProviderBase : IProviderBase
    {
        /// <summary>Gets a value indicating whether this ITaxesProviderBase is initialized.</summary>
        /// <value>True if this ITaxesProviderBase is initialized, false if not.</value>
        bool IsInitialized { get; }

        /// <summary>Initialize Functions.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if succeeded or false error-ed out.</returns>
        Task<bool> InitAsync(string? contextProfileName);

        /// <summary>Calculates Taxes.</summary>
        /// <param name="taxEntityType">     Type of the tax entity.</param>
        /// <param name="cart">              Cart to calculate taxes on.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A decimal.</returns>
        Task<decimal> CalculateAsync(
            Enums.TaxEntityType taxEntityType,
            ICartModel cart,
            string? contextProfileName);

        /// <summary>Calculates the with line items.</summary>
        /// <param name="taxEntityType">     Type of the tax entity.</param>
        /// <param name="cart">              Cart to calculate taxes on.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The calculated with line items.</returns>
        Task<TaxesResult> CalculateWithLineItemsAsync(
            Enums.TaxEntityType taxEntityType,
            ICartModel cart,
            string? contextProfileName);

        /// <summary>Commits tax transaction (Integrations).</summary>
        /// <param name="taxEntityType">      Type of the tax entity.</param>
        /// <param name="cartModel">          Cart to calculate taxes on.</param>
        /// <param name="purchaseOrderNumber">PO for tax integrations.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        /// <returns>CEFActionResponse true if succeeded (w/ transactionID) or false if issue (with Errors).</returns>
        Task<CEFActionResponse<string>> CommitAsync(
            Enums.TaxEntityType taxEntityType,
            ICartModel cartModel,
            string purchaseOrderNumber,
            string? contextProfileName);

        /// <summary>Void tax transaction (Integrations).</summary>
        /// <param name="taxTransactionID">  The transaction to void (integrations only).</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>CEFActionResponse true if succeeded (w/ transactionID) or false if issue (with Errors).</returns>
        Task<CEFActionResponse<string>> VoidAsync(
            string taxTransactionID,
            string? contextProfileName);

        /// <summary>Test Tax Service (Integrations).</summary>
        /// <returns>CEFActionResponse true if succeeded false if not.</returns>
        Task<CEFActionResponse> TestServiceAsync();

        /// <summary>Calculates taxes.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="userID">            The user identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="key">               Target key (Optional).</param>
        /// <param name="vatId">             The VAT ID number (Optional).</param>
        /// <returns>The calculated cart.</returns>
        Task<TaxesResult> CalculateCartAsync(
            ICartModel cart,
            int? userID,
            string? contextProfileName,
            TargetGroupingKey? key = null,
            string? vatId = null);

        /// <summary>Calculates taxes.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="userID">            The user identifier.</param>
        /// <param name="currentAccountId">  The current account Id.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="key">               Target key (Optional).</param>
        /// <param name="vatId">             The VAT ID number (Optional).</param>
        /// <returns>The calculated cart.</returns>
        Task<TaxesResult> CalculateCartAsync(
            ICartModel cart,
            int? userID,
            int? currentAccountId,
            string? contextProfileName,
            TargetGroupingKey? key = null,
            string? vatId = null);

        /// <summary>Commits tax transaction.</summary>
        /// <param name="cart">               The cart.</param>
        /// <param name="userID">             The user Id.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <param name="purchaseOrderNumber">PO for tax integrations.</param>
        /// <param name="vatId">              The VAT ID number (Optional).</param>
        /// <returns>A Task{TaxesResult}.</returns>
        Task<TaxesResult> CommitCartAsync(
            ICartModel cart,
            int? userID,
            string? contextProfileName,
            string? purchaseOrderNumber = null,
            string? vatId = null);

        /// <summary>Commits tax transaction.</summary>
        /// <param name="salesReturn">       The return.</param>
        /// <param name="description">       A description of the return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task CommitReturnAsync(ISalesReturnModel salesReturn, string description, string? contextProfileName);

        /// <summary>Void order tax.</summary>
        /// <param name="salesOrder">        The order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task VoidOrderAsync(ISalesOrderModel salesOrder, string? contextProfileName);

        /// <summary>Void return tax.</summary>
        /// <param name="salesReturn">       The return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task VoidReturnAsync(ISalesReturnModel salesReturn, string? contextProfileName);
    }
}
