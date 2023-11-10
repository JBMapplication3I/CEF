// <copyright file="BraintreePaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Ecommerce.Providers.Payments.BraintreePayments
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>Implements the BraintreePaymentsProvider class.</summary>
    public partial class BraintreePaymentsProvider
    {
        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel? cart = null,
            bool useAutoPay = false)
        {
            Contract.RequiresNotNull(billing);
            // Pay by PayPal
            if (Contract.CheckValidKey(payment.Token))
            {
                return BraintreePaymentsProviderExtensions.ToPaymentResponse(
                    gateway.Transaction.Sale(new()
                    {
                        Amount = payment.Amount ?? 0m,
                        BillingAddress = BillingToAddressRequest(billing),
                        MerchantAccountId = BraintreePaymentsProviderConfig.MerchantAccountID,
                        PaymentMethodNonce = payment.Token,
                        Options = new()
                        {
                            SubmitForSettlement = true,
                        },
                    }));
            }
            //check for existing payment method in wallet.
            var customer = await GetCustomerProfileAsync(billing).ConfigureAwait(false);
            if (customer == null)
            {
                await CreateCustomerProfileAsync(payment, billing, null).ConfigureAwait(false);
                customer = await GetCustomerProfileAsync(billing).ConfigureAwait(false);
            }
            var card = await GetCreditCardAsync(payment, billing!).ConfigureAwait(false);
            if (card == null)
            {
                await CreateCustomerCreditCardAsync(payment, billing!).ConfigureAwait(false);
                card = await GetCreditCardAsync(payment, billing!).ConfigureAwait(false);
            }
            if (!Contract.CheckValidID(payment.Amount))
            {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                throw new ArgumentNullException(nameof(payment.Amount));
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
            }
            if (Contract.CheckValidKey(card!.Token))
            {
                return await CustomerProfilePaymentAsync(payment, billing!, contextProfileName).ConfigureAwait(false);
            }
            // Create transaction request
            return BraintreePaymentsProviderExtensions.ToPaymentResponse(
                gateway.Transaction.Sale(new()
                {
                    CustomerId = customer!.Id,
                    Amount = payment.Amount ?? 0m,
                    BillingAddress = BillingToAddressRequest(billing),
                    CreditCard = new()
                    {
                        CardholderName = payment.CardHolderName,
                        Number = payment.CardNumber,
                        ExpirationMonth = payment.ExpirationMonth!.Value.ToString(),
                        ExpirationYear = payment.ExpirationYear!.Value.ToString(),
                        CVV = payment.CVV,
                    },
                    MerchantAccountId = BraintreePaymentsProviderConfig.MerchantAccountID,
                    Options = new()
                    {
                        SubmitForSettlement = true,
                    },
                }));
        }
    }
}
