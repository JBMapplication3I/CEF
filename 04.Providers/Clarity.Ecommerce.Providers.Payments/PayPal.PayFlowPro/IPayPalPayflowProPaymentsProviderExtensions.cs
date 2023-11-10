// <copyright file="IPayPalPayflowProPaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPayPalPayflowProPaymentsProviderExtensions interface</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayPalPayflowPro
{
    using Clarity.Ecommerce.Interfaces.Providers;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;

    /// <summary>Interface for PayPal Payflow pro payments provider extensions.</summary>
    public interface IPayPalPayflowProPaymentsProviderExtensions
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
        /// <returns>The PayflowCreditCardOrACHParameters.</returns>
        PayflowCreditCardOrACHParameters InfoToCardParameters(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            string? expDate);

        /// <summary>Information to refund parameters.</summary>
        /// <param name="payment">The payment.</param>
        /// <param name="amount"> The amount.</param>
        /// <param name="expDate">The exponent date.</param>
        /// <returns>The PayflowCreditCardOrACHParameters.</returns>
        PayflowCreditCardOrACHParameters InfoToRefundParameters(
            IProviderPayment payment,
            decimal? amount,
            string? expDate);

        /// <summary>Information to wallet parameters.</summary>
        /// <param name="payment">The payment.</param>
        /// <param name="billing">The billing.</param>
        /// <param name="expDate">The exponent date.</param>
        /// <returns>The PayflowCreditCardParameters.</returns>
        PayflowCreditCardOrACHParameters InfoToWalletParameters(
            IProviderPayment payment,
            IContactModel billing,
            string? expDate);

        /// <summary>Credit card to request body.</summary>
        /// <param name="creditCardOrACH">The credit card or a ch.</param>
        /// <returns>A string.</returns>
        string CreditCardOrACHToRequestBody(PayflowCreditCardOrACHParameters creditCardOrACH);

        /// <summary>Credit card to delayed capture request body.</summary>
        /// <param name="creditCardOrACH">The credit card or a ch.</param>
        /// <returns>A string.</returns>
        string CreditCardToDelayedCaptureRequestBody(PayflowCreditCardOrACHParameters creditCardOrACH);

        /// <summary>Credit card to refund request body.</summary>
        /// <param name="creditCardOrACH">The credit card or a ch.</param>
        /// <returns>A string.</returns>
        string CreditCardOrACHToRefundRequestBody(PayflowCreditCardOrACHParameters creditCardOrACH);

        /// <summary>Payment body to request and get result.</summary>
        /// <param name="requestBody">       The request body.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>An IPaymentResponse.</returns>
        IPaymentResponse PaymentBodyToRequestAndGetResult(
            string requestBody,
            string? contextProfileName,
            ILogger logger);

        /// <summary>Payment body to wallet request and get result.</summary>
        /// <param name="requestBody">       The request body.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="logger">            The logger.</param>
        /// <returns>An IPaymentWalletResponse.</returns>
        IPaymentWalletResponse PaymentBodyToWalletRequestAndGetResult(
            string requestBody,
            string? contextProfileName,
            ILogger logger);
    }
}
