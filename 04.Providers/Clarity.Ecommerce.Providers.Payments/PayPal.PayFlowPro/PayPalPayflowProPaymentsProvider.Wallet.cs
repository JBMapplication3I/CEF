// <copyright file="PayPalPayflowProPaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayPal Payflow Pro payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayPalPayflowPro
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>A PayPal Payflow Pro payments provider.</summary>
    /// <seealso cref="IWalletProviderBase"/>
    public partial class PayPalPayflowProPaymentsProvider : IWalletProviderBase
    {
        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            PayflowCreditCardOrACHParameters creditCardOrACH;
            if (Extensions.Value.IsACH(payment))
            {
                creditCardOrACH = Extensions.Value.InfoToWalletParameters(
                    payment,
                    billing,
                    null);
            }
            else
            {
                Contract.RequiresValidID(payment.ExpirationMonth);
                Contract.RequiresValidID(payment.ExpirationYear);
                creditCardOrACH = Extensions.Value.InfoToWalletParameters(
                    payment,
                    billing,
                    CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value));
            }
            creditCardOrACH.TransactionType = "L";
            var requestBody = Extensions.Value.CreditCardOrACHToRequestBody(creditCardOrACH);
            return Task.FromResult(
                Extensions.Value.PaymentBodyToWalletRequestAndGetResult(
                    requestBody,
                    contextProfileName,
                    Logger));
        }

        /// <inheritdoc/>
        /// <remarks>This functions the same way that Create does, essentially it overwrites if already there.</remarks>
        public Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment, IContactModel billing, string? contextProfileName)
        {
            return CreateCustomerProfileAsync(payment, billing, contextProfileName);
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException("Not Supported by Gateway");
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException("Not Supported by Gateway");
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
