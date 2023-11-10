// <copyright file="AuthorizePaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authorize payments provider extensions class</summary>
#if !NET5_0_OR_GREATER // Authorize.NET doesn't have .net 5.0+ builds
namespace Clarity.Ecommerce.Providers.Payments.Authorize
{
    using Interfaces.Providers.Payments;
    using Utilities;
    using AuthV1 = AuthorizeNet.Api.Contracts.V1;

    /// <summary>An authorize payments provider extensions.</summary>
    public static class AuthorizePaymentsProviderExtensions
    {
#pragma warning disable CS0618 // Type or member is obsolete
        /// <summary>An <seealso cref="AuthorizeNet.IGatewayResponse"/> extension method that converts a response to a payment response.</summary>
        /// <param name="response">The response to act on.</param>
        /// <returns>response as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(this AuthorizeNet.IGatewayResponse response)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            Contract.RequiresNotNull(response);
            return new PaymentResponse
            {
                Amount = response.Amount,
                Approved = response.Approved,
                AuthorizationCode = response.AuthorizationCode,
                ResponseCode = response.ResponseCode,
                TransactionID = response.TransactionID,
            };
        }

        /// <summary>An <seealso cref="AuthV1.createTransactionResponse"/> extension method that converts this object to
        /// a payment response.</summary>
        /// <param name="response">The response to act on.</param>
        /// <param name="amount">  The amount.</param>
        /// <returns>The given data converted to an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(
            this AuthV1.createTransactionResponse? response,
            decimal amount)
        {
            Contract.RequiresNotNull(response);
            return new PaymentResponse
            {
                Amount = amount,
                Approved = response!.messages.resultCode == AuthV1.messageTypeEnum.Ok,
                AuthorizationCode = response.transactionResponse.authCode,
                ResponseCode = response.transactionResponse.responseCode,
                TransactionID = response.transactionResponse.transId,
            };
        }

        /// <summary>Generates a response.</summary>
        /// <param name="approved">         True if approved.</param>
        /// <param name="customerProfileId">Identifier for the customer profile.</param>
        /// <param name="responseCode">     The response code.</param>
        /// <returns>The response.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(
            bool approved,
            string? customerProfileId,
            string responseCode)
        {
            return new PaymentWalletResponse
            {
                Approved = approved,
                Token = customerProfileId,
                ResponseCode = responseCode,
            };
        }
    }
}
#endif
