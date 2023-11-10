// <copyright file="HeartlandPaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the heartland payments provider class</summary>
// https://developer.heartlandpaymentsystems.com/Documentation/credit-card-payments
namespace Clarity.Ecommerce.Providers.Payments.Heartland
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;

    /// <summary>A heartland payments provider.</summary>
    /// <seealso cref="IWalletProviderBase"/>
    public partial class HeartlandPaymentsProvider : IWalletProviderBase
    {
        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            var card = HeartlandPaymentsProviderExtensions.PaymentToCard(payment);
            var address = HeartlandPaymentsProviderExtensions.StreetAndZipToAddress(
                billing?.Address?.Street1,
                billing?.Address?.PostalCode);
            var response = card.Verify()
                .WithCurrency("USD")
                .WithAddress(address)
                .WithRequestMultiUseToken(true)
                .Execute();
            return Task.FromResult<IPaymentWalletResponse>(
                new PaymentWalletResponse
                {
                    Approved = response.ResponseCode == "00", // "00" == Success
                    ResponseCode = response.ResponseCode,
                    Token = response.Token,
                });
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            throw new NotSupportedException("Not supported by Gateway");
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotSupportedException("Not supported by Gateway");
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotSupportedException("Not supported by Gateway");
        }

        /// <inheritdoc/>
        public Task<List<IPaymentWalletResponse>> GetCustomerProfilesAsync(
            string walletAccountNumber,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }
    }
}
