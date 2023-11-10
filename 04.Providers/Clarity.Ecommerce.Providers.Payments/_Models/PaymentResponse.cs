// <copyright file="PaymentResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payment response class</summary>
namespace Clarity.Ecommerce.Providers.Payments
{
    using Interfaces.Providers.Payments;

    /// <summary>A payment response.</summary>
    /// <seealso cref="IPaymentResponse"/>
    public sealed class PaymentResponse : IPaymentResponse
    {
        /// <inheritdoc/>
        public decimal Amount { get; set; }

        /// <inheritdoc/>
        public bool Approved { get; set; }

        /// <inheritdoc/>
        public string? ResponseCode { get; set; }

        /// <inheritdoc/>
        public string? AuthorizationCode { get; set; }

        /// <inheritdoc/>
        public string? TransactionID { get; set; }

        /// <inheritdoc/>
        public string? ReferenceCode { get; set; }
    }
}
