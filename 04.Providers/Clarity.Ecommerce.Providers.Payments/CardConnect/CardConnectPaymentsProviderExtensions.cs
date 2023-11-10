// <copyright file="CardConnectPaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CardConnect payments provider extensions class</summary>
namespace Clarity.Ecommerce.Providers.Payments.CardConnect
{
    using Interfaces.Providers.Payments;
    using Models;

    /// <summary>A CardConnect payments provider extensions.</summary>
    public static class CardConnectPaymentsProviderExtensions
    {
        /// <summary>Converts an authorizationResponse to a payment response.</summary>
        /// <param name="response">The response.</param>
        /// <returns>Charge as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(CardConnectAuthorizationResponse? response)
        {
            if (response is not null && response.ResponseStatus == ApprovalStatus.Approved)
            {
                return new PaymentResponse
                {
                    Approved = true,
                    Amount = decimal.Parse(response.AmountInCents!) / 100, // CardConnect uses cents
                    AuthorizationCode = response.AuthorizationCode,
                    ResponseCode = response.ResponseCode,
                    TransactionID = response.RetrievalReference,
                };
            }
            return new PaymentResponse
            {
                Approved = false,
                ResponseCode = response?.ResponseText,
            };
        }

        /// <summary>Converts an authorizationResponse to a payment response.</summary>
        /// <param name="response">The response.</param>
        /// <returns>Charge as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(CardConnectCaptureResponse? response)
        {
            if (response?.SettlementStatus is "Authorized" or "Queued for Capture")
            {
                return new PaymentResponse
                {
                    Approved = true,
                    Amount = decimal.Parse(response.AmountInCents!) / 100, // CardConnect uses cents
                    AuthorizationCode = response.BatchId,
                    ResponseCode = response.SettlementStatus,
                    TransactionID = response.RetrievalReference,
                };
            }
            return new PaymentResponse
            {
                Approved = false,
                ResponseCode = response?.SettlementStatus,
            };
        }

        /// <summary>Converts values to a payment wallet response.</summary>
        /// <param name="response">The response.</param>
        /// <returns>This CardConnectProfileResponse converted to IPaymentWalletResponse.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(CardConnectProfileResponse? response)
        {
            if (!string.IsNullOrEmpty(response?.ProfileId))
            {
                return new PaymentWalletResponse
                {
                    Approved = true,
                    ResponseCode = response!.ResponseCode,
                    Token = response.Token,
                };
            }
            return new PaymentWalletResponse
            {
                Approved = false,
                ResponseCode = response?.ResponseCode,
                Token = null,
            };
        }
    }
}
