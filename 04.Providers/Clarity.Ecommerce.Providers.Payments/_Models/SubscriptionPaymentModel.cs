// <copyright file="SubscriptionPaymentModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription payment model class</summary>
namespace Clarity.Ecommerce.Providers.Payments
{
    using System;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;

    /// <summary>A data model for the subscription payment.</summary>
    /// <seealso cref="ISubscriptionPaymentModel"/>
    public class SubscriptionPaymentModel : ISubscriptionPaymentModel
    {
        /// <inheritdoc/>
        public IPaymentModel? Payment { get; set; }

        /// <inheritdoc/>
        public ISubscriptionModel? Subscription { get; set; }

        /// <inheritdoc/>
        public decimal Amount { get; set; }

        /// <inheritdoc/>
        public int? NumberOfPayments { get; set; }

        /// <inheritdoc/>
        public DateTime? StartDate { get; set; }

        /// <inheritdoc/>
        public string? Frequency { get; set; }

        /// <inheritdoc/>
        public string? SubscriptionReferenceNumber { get; set; }

        /// <inheritdoc/>
        public bool AutomaticRenewal { get; set; }

        /// <inheritdoc/>
        public int PaymentRemaining { get; set; }
    }
}
