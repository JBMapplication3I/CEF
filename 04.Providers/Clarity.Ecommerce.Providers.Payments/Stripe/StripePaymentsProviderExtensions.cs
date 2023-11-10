// <copyright file="StripePaymentsProviderExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the stripe payments provider extensions class</summary>
namespace Clarity.Ecommerce.Providers.Payments.StripeInt
{
    using Interfaces.Providers.Payments;

    /// <summary>A stripe payments provider extensions.</summary>
    public static class StripePaymentsProviderExtensions
    {
        /// <summary>Converts a charge to a payment response.</summary>
        /// <param name="approved">    True if approved.</param>
        /// <param name="responseCode">The response code.</param>
        /// <returns>Charge as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(bool approved, string responseCode)
        {
            return new PaymentResponse
            {
                Approved = approved,
                ResponseCode = responseCode,
            };
        }

        /// <summary>Converts a charge to a payment response.</summary>
        /// <param name="charge">The charge.</param>
        /// <returns>Charge as an IPaymentResponse.</returns>
        public static IPaymentResponse ToPaymentResponse(Stripe.Charge charge)
        {
            if (charge.Paid)
            {
                return new PaymentResponse
                {
                    Approved = true,
                    Amount = (decimal)charge.Amount / 100, // Stripe uses cents
                    AuthorizationCode = charge.FailureCode ?? "N/A",
                    ResponseCode = charge.FailureMessage ?? "N/A",
                    TransactionID = charge.Id,
                };
            }
            return new PaymentResponse
            {
                Approved = false,
                AuthorizationCode = charge.FailureCode,
                ResponseCode = charge.FailureMessage,
            };
        }
    }
}
