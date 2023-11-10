// <copyright file="PayPalPayflowProPaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayPal Payflow Pro payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayPalPayflowPro
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    public partial class PayPalPayflowProPaymentsProvider
    {
        /// <summary>Gets the extensions.</summary>
        /// <value>The extensions.</value>
        protected Lazy<IPayPalPayflowProPaymentsProviderExtensions> Extensions { get; }
            = new(() => RegistryLoaderWrapper.GetInstance<IPayPalPayflowProPaymentsProviderExtensions>());

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            if (Extensions.Value.IsACH(payment))
            {
                throw new InvalidOperationException(
                    "The eCheck/ACH process cannot perform authorize with later capture actions");
            }
            Contract.Requires<ArgumentException>(payment.Amount > 0);
            Contract.RequiresValidID(payment.ExpirationMonth);
            Contract.RequiresValidID(payment.ExpirationYear);
            var creditCard = Extensions.Value.InfoToCardParameters(
                payment,
                billing,
                shipping,
                payment.ExpirationMonth > 0 && payment.ExpirationYear > 0
                    ? CleanExpirationDate(payment.ExpirationMonth.Value, payment.ExpirationYear.Value)
                    : null);
            creditCard.TransactionType = "A";
            creditCard.OrderID = $"Web Order {DateExtensions.GenDateTime.ToFileTimeUtc()}";
            var requestBody = Extensions.Value.CreditCardOrACHToRequestBody(creditCard);
            return Task.FromResult(
                Extensions.Value.PaymentBodyToRequestAndGetResult(
                    requestBody,
                    contextProfileName,
                    Logger));
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            if (Extensions.Value.IsACH(payment))
            {
                throw new InvalidOperationException(
                    "The eCheck/ACH process cannot perform authorize with later capture actions");
            }
            Contract.Requires<ArgumentException>(payment.Amount > 0);
            Contract.RequiresValidID(payment.ExpirationMonth);
            Contract.RequiresValidID(payment.ExpirationYear);
            var creditCardOrACH = Extensions.Value.InfoToCardParameters(
                payment,
                null,
                null,
                CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value));
            creditCardOrACH.TransactionType = "D";
            creditCardOrACH.OriginalID = paymentAuthorizationToken;
            var requestBody = Extensions.Value.CreditCardToDelayedCaptureRequestBody(creditCardOrACH);
            var result = Extensions.Value.PaymentBodyToRequestAndGetResult(
                requestBody,
                contextProfileName,
                Logger);
            result.Amount = payment.Amount!.Value;
            return Task.FromResult(result);
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel? cart = null,
            bool useWalletToken = false)
        {
            Contract.Requires<ArgumentException>(payment.Amount > 0);
            if (ProviderMode != Enums.PaymentProviderMode.Production && payment.Amount >= 1000)
            {
                // PayFlow Pro Test servers will automatically decline any amount over 1000, production doesn't do that
                payment.Amount = 999m;
            }
            PayflowCreditCardOrACHParameters creditCardOrACH;
            if (Extensions.Value.IsACH(payment))
            {
                creditCardOrACH = Extensions.Value.InfoToCardParameters(
                    payment,
                    billing,
                    shipping,
                    null);
            }
            else
            {
                Contract.RequiresValidID(payment.ExpirationMonth);
                Contract.RequiresValidID(payment.ExpirationYear);
                var exp = payment.ExpirationMonth > 0 && payment.ExpirationYear > 0
                    ? CleanExpirationDate(payment.ExpirationMonth.Value, payment.ExpirationYear.Value)
                    : null;
                creditCardOrACH = Extensions.Value.InfoToCardParameters(
                    payment,
                    billing,
                    shipping,
                    exp);
            }
            creditCardOrACH.TransactionType = "S";
            creditCardOrACH.OrderID = $"Web Order {DateExtensions.GenDateTime.ToFileTimeUtc()}";
            var requestBody = Extensions.Value.CreditCardOrACHToRequestBody(creditCardOrACH);
            return Task.FromResult(
                Extensions.Value.PaymentBodyToRequestAndGetResult(
                    requestBody,
                    contextProfileName,
                    Logger));
        }
    }
}
