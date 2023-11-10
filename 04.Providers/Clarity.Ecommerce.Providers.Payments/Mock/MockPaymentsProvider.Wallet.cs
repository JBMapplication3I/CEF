// <copyright file="MockPaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the MockPaymentProvider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Mock
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>A mock payments provider.</summary>
    /// <seealso cref="IWalletProviderBase"/>
    public partial class MockPaymentsProvider : IWalletProviderBase
    {
        /// <summary>Attempts to parse token from the given data.</summary>
        /// <param name="token">                   The token.</param>
        /// <param name="customerRefNum">          The customer reference number.</param>
        /// <param name="mitReceivedTransactionID">Identifier for the mit received transaction.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool TryParseToken(
            string token,
            out string? customerRefNum,
            out string? mitReceivedTransactionID)
        {
            customerRefNum = null;
            mitReceivedTransactionID = null;
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            var delimiterIndex = token.IndexOf('_');
            if (delimiterIndex <= 0 || delimiterIndex >= token.Length - 1)
            {
                return false;
            }
            customerRefNum = token[..delimiterIndex];
            mitReceivedTransactionID = token[(delimiterIndex + 1)..];
            return Contract.CheckAllValidKeys(customerRefNum, mitReceivedTransactionID);
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            return Task.FromResult<IPaymentWalletResponse>(
                new PaymentWalletResponse
                {
                    Approved = true,
                    Token = GenCode(payment),
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
                    Approved = true,
                    Token = GenCode(payment),
                });
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            return Task.FromResult<IPaymentWalletResponse>(new PaymentWalletResponse { Approved = true });
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            return Task.FromResult<IPaymentWalletResponse>(new PaymentWalletResponse { Approved = true });
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
