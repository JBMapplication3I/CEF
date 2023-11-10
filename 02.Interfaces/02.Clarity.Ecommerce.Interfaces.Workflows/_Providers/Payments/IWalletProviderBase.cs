// <copyright file="IWalletProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IWalletProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for wallet provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface IWalletProviderBase : IProviderBase
    {
        /// <summary>Creates customer profile.</summary>
        /// <param name="payment">           The payment.</param>
        /// <param name="billing">           The billing.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new customer profile.</returns>
        Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName);

        /// <summary>Updates the customer profile.</summary>
        /// <param name="payment">           The payment.</param>
        /// <param name="billing">           The billing.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IPaymentWalletResponse.</returns>
        Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName);

        /// <summary>Deletes the customer profile described by payment.</summary>
        /// <param name="payment">           The payment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IPaymentWalletResponse.</returns>
        Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName);

        /// <summary>Gets customer profile.</summary>
        /// <param name="payment">           The payment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The customer profile.</returns>
        Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName);

        /// <summary>Gets customer profile.</summary>
        /// <param name="walletAccountNumber">The account number to get existing wallets.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The customer profile.</returns>
        Task<List<IPaymentWalletResponse>> GetCustomerProfilesAsync(
            string walletAccountNumber,
            string? contextProfileName);
    }
}
