// <copyright file="SagePaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage payments provider extensions class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using Interfaces.Providers.Payments;

    /// <summary>A sage payments provider extensions.</summary>
    public static class SagePaymentsProviderExtensions
    {
        /// <summary>Converts a values to a payment wallet response.</summary>
        /// <param name="response">The Deserialized Wallet Response Object from Sage.</param>
        /// <param name="approved">Whether the response is considered approved.</param>
        /// <returns>An IPaymentWalletResponse.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(this SageWalletResponse? response, bool approved)
        {
            if (response is null)
            {
                return new PaymentWalletResponse();
            }
            return new PaymentWalletResponse
            {
                Approved = approved,
                ResponseCode = response.code,
                Token = response.token,
            };
        }

        /// <summary>Converts a values to a payment wallet response.</summary>
        /// <param name="response">The Deserialized Error Response Object from Sage.</param>
        /// <returns>An IPaymentWalletResponse.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(this SageErrorResponse? response)
        {
            if (response is null)
            {
                return new PaymentWalletResponse();
            }
            return new PaymentWalletResponse
            {
                Approved = false,
                ResponseCode = response.code,
                Token = "Server Error",
            };
        }

        /// <summary>Converts a values to a payment wallet response.</summary>
        /// <param name="_">The Exception Object generated when the transaction attempt failed.</param>
        /// <returns>An IPaymentWalletResponse.</returns>
        public static IPaymentWalletResponse ToPaymentWalletResponse(this Exception _)
        {
            return new PaymentWalletResponse
            {
                Approved = false,
                ResponseCode = "Server Error",
                Token = "Server Error",
            };
        }

        /// <summary>Converts this SagePaymentsResponse to a payment response.</summary>
        /// <param name="response">The Deserialized Payment Response object from Sage.</param>
        /// <returns>An IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(this SagePaymentResponse? response)
        {
            if (response is null)
            {
                return new PaymentResponse();
            }
            return new PaymentResponse
            {
                Approved = response.status == "Approved",
                ResponseCode = response.code,
                TransactionID = response.referencea,
            };
        }

        /// <summary>Converts a values to a payment response.</summary>
        /// <param name="response">The Deserialized Error Response Object from Sage.</param>
        /// <returns>An IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(this SageErrorResponse? response)
        {
            if (response is null)
            {
                return new PaymentResponse();
            }
            return new PaymentResponse
            {
                Approved = false,
                ResponseCode = response.code,
                AuthorizationCode = "Server Error",
            };
        }

        /// <summary>Converts a values to a payment response.</summary>
        /// <param name="_">The Exception Object generated when the transaction attempt failed.</param>
        /// <returns>An IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(this Exception _)
        {
            return new PaymentResponse
            {
                Approved = false,
                ResponseCode = "Server Error",
                AuthorizationCode = "Server Error",
            };
        }
    }
}
