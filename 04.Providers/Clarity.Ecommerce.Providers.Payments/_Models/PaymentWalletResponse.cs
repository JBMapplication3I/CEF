// <copyright file="PaymentWalletResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payment wallet response class</summary>
namespace Clarity.Ecommerce.Providers.Payments
{
    using Interfaces.Providers.Payments;

    /// <summary>A payment wallet response.</summary>
    /// <seealso cref="IPaymentWalletResponse"/>
    public sealed class PaymentWalletResponse : IPaymentWalletResponse
    {
        /// <inheritdoc/>
        public bool Approved { get; set; }

        /// <inheritdoc/>
        public string? Token { get; set; }

        /// <inheritdoc/>
        public string? ResponseCode { get; set; }

        /// <inheritdoc/>
        public string? CardType { get; set; }

        /// <inheritdoc/>
        public string? Account { get; set; }

        /// <inheritdoc/>
        public string? ExpDate { get; set; }

        /// <inheritdoc/>
        public string? CardName { get; set; }

        /// <inheritdoc/>
        public string? Customer { get; set; }
    }
}
