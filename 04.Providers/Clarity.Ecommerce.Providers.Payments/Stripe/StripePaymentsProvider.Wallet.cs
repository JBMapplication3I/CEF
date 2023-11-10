// <copyright file="StripePaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the stripe payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.StripeInt
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Stripe;

    /// <summary>A stripe payments provider.</summary>
    /// <seealso cref="IWalletProviderBase"/>
    public partial class StripePaymentsProvider : IWalletProviderBase
    {
        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            var tokenOptions = new SourceCreateOptions
            {
                Card = new SourceCardOptions
                {
                    Number = payment.CardNumber,
                    ExpYear = payment.ExpirationYear ?? 0,
                    ExpMonth = payment.ExpirationMonth ?? 0,
                    Cvc = payment.CVV,
                },
                Type = SourceType.Card,
            };
            var sourceService = new SourceService();
            _ = sourceService.Create(tokenOptions);
            var customerOptions = new CustomerCreateOptions
            {
                Description = $"Customer for {billing.Email1}",
                // SourceToken = stripeToken.Id,
            };
            var customerService = new CustomerService();
            var customer = customerService.Create(customerOptions);
            return Task.FromResult<IPaymentWalletResponse>(
                new PaymentWalletResponse
                {
                    Token = customer.Id,
                    Approved = true,
                });
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            return Task.FromResult<IPaymentWalletResponse>(
                new PaymentWalletResponse
                {
                    Token = payment.Token,
                    Approved = true,
                });
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            return Task.FromResult<IPaymentWalletResponse>(
                new PaymentWalletResponse
                {
                    Token = payment.Token,
                    Approved = true,
                });
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            return Task.FromResult<IPaymentWalletResponse>(
                new PaymentWalletResponse
                {
                    Token = payment.Token,
                    Approved = true,
                });
        }

        /// <inheritdoc/>
        public Task<List<IPaymentWalletResponse>> GetCustomerProfilesAsync(
            string walletAccountNumber,
            string? contextProfileName)
        {
            throw new System.NotImplementedException();
        }
    }
}
