// <copyright file="IPaymentsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPaymentsProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for payments provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface IPaymentsProviderBase : IProviderBase
    {
        /// <summary>Initializes the configuration.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task InitConfigurationAsync(string? contextProfileName);

        /// <summary>Authorizes the transaction.</summary>
        /// <param name="payment">                The payment.</param>
        /// <param name="billing">                The billing.</param>
        /// <param name="shipping">               The shipping.</param>
        /// <param name="paymentAlreadyConverted">True if payment already converted.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>An IPaymentResponse.</returns>
        Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName);

        /// <summary>Captures the transaction.</summary>
        /// <param name="authorizationToken">The authorization token.</param>
        /// <param name="payment">           The payment.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IPaymentResponse.</returns>
        Task<IPaymentResponse> CaptureAsync(
            string authorizationToken,
            IProviderPayment payment,
            string? contextProfileName);

        /// <summary>Authorize and a capture.</summary>
        /// <param name="payment">                The payment.</param>
        /// <param name="billing">                The billing.</param>
        /// <param name="shipping">               The shipping.</param>
        /// <param name="paymentAlreadyConverted">True if payment already converted.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <param name="cart">                   The cart.</param>
        /// <param name="useWalletToken">         True if using a Wallet Token.</param>
        /// <returns>An IPaymentResponse.</returns>
        Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel? cart = null,
            bool useWalletToken = false);

        /// <summary>Returns the authentication token.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An authentication token.</returns>
        Task<CEFActionResponse<string>> GetAuthenticationToken(
            string? contextProfileName);
    }
}
