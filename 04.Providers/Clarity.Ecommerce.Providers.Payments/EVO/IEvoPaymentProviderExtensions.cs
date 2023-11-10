// <copyright file="IEvoPaymentProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IEvoPaymentProviderExtensions interface</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    using System.Collections.Generic;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;

    /// <summary>Interface for EVO payments provider extensions.</summary>
    public interface IEvoPaymentProviderExtensions
    {
        /// <summary>Query if 'payment' is ACH.</summary>
        /// <param name="payment">The payment.</param>
        /// <returns>True if ACH, false if not.</returns>
        bool IsACH(IProviderPayment payment);

        /// <summary>Information to card parameters.</summary>
        /// <param name="payment"> The payment.</param>
        /// <param name="billing"> The billing.</param>
        /// <param name="shipping">The shipping.</param>
        /// <param name="expDate"> The exponent date.</param>
        /// <returns>The EvoPaymentProviderParameters.</returns>
        EvoPaymentProviderParameters InfoToCardParameters(
            IProviderPayment payment,
            IContactModel billing,
            IContactModel shipping,
            string expDate);

        /// <summary>Information to refund parameters.</summary>
        /// <param name="payment">The payment.</param>
        /// <param name="amount"> The amount.</param>
        /// <param name="expDate">The exponent date.</param>
        /// <returns>The EvoPaymentProviderParameters.</returns>
        EvoPaymentProviderParameters InfoToRefundParameters(
            IProviderPayment payment,
            decimal? amount,
            string expDate);

        /// <summary>Information to wallet parameters.</summary>
        /// <param name="payment">The payment.</param>
        /// <param name="billing">The billing.</param>
        /// <param name="expDate">The exponent date.</param>
        /// <returns>The EvoPaymentProviderParameters.</returns>
        EvoPaymentProviderParameters InfoToWalletParameters(
            IProviderPayment payment,
            IContactModel billing,
            string expDate);

        /// <summary>Gets authorization token.</summary>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The authorization token.</returns>
        IPaymentResponse GetAuthorizationToken(
            string authKey,
            string? contextProfileName,
            ILogger logger);

        /// <summary>Gets account transaction.</summary>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="transactionID">     Identifier for the transaction.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The account transaction.</returns>
        string GetAccountTransaction(
            string authKey,
            string transactionID,
            string? contextProfileName,
            ILogger logger);

        /// <summary>Gets account addresses.</summary>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="account">           The account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The account addresses.</returns>
        string GetAccountAddresses(
            string authKey,
            string account,
            string? contextProfileName,
            ILogger logger);

        /// <summary>Gets account wallet.</summary>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="account">           The account.</param>
        /// <param name="getCreditCard">     True to get credit card.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The account wallet.</returns>
        IPaymentWalletResponse GetAccountWallet(
            string authKey,
            string account,
            bool getCreditCard,
            string? contextProfileName,
            ILogger logger);

        /// <summary>Gets account wallets.</summary>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="account">           The account.</param>
        /// <param name="getCreditCard">     True to get credit card.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The account wallets.</returns>
        List<IPaymentWalletResponse> GetAccountWallets(
            string authKey,
            string account,
            bool getCreditCard,
            string? contextProfileName,
            ILogger logger);

        /// <summary>Gets account profile.</summary>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="profileType">       Type of the profile.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The account profile.</returns>
        PayFabricGatewayAccountProfile GetAccountProfile(
            string authKey,
            string profileType,
            string? contextProfileName,
            ILogger logger);

        /// <summary>Gets account profile gateway.</summary>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="profileType">       Type of the profile.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The account profile gateway.</returns>
        string GetAccountProfileGateway(
            string authKey,
            string profileType,
            string? contextProfileName,
            ILogger logger);

        /// <summary>Creates a wallet.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="createCreditCard">  True to create credit card.</param>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The new wallet.</returns>
        IPaymentWalletResponse CreateWallet(
            PayFabricWalletRequest model,
            bool createCreditCard,
            string authKey,
            string? contextProfileName,
            ILogger logger);

        /// <summary>Creates payment transaction.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The new payment transaction.</returns>
        IPaymentResponse CreatePaymentTransaction(
            PayFabricTransactionRequest model,
            string authKey,
            string? contextProfileName,
            ILogger logger);

        /// <summary>Creates and process payment transaction.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>The new and process payment transaction.</returns>
        IPaymentResponse CreateAndProcessPaymentTransaction(
            PayFabricTransactionRequest model,
            string authKey,
            string? contextProfileName,
            ILogger logger);

        /// <summary>Refund customer.</summary>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>An IPaymentResponse.</returns>
        IPaymentResponse RefundCustomer(
            string authKey,
            PayFabricTransactionRequest model,
            string? contextProfileName,
            ILogger logger);

        /// <summary>
        /// Generates a response.
        /// </summary>
        /// <param name="approved">         True if approved.</param>
        /// <param name="customerProfileId">Identifier for the customer profile.</param>
        /// <param name="responseCode">     The response code.</param>
        /// <returns>The response.</returns>
        IPaymentWalletResponse ToPaymentWalletResponse(
            bool approved,
            string customerProfileId,
            string responseCode);

        /// <summary>Removes the wallet.</summary>
        /// <param name="token">             The token.</param>
        /// <param name="authKey">           The authentication key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>An IPaymentWalletResponse.</returns>
        IPaymentWalletResponse RemoveWallet(
            string token,
            string authKey,
            string? contextProfileName,
            ILogger logger);
    }
}
