// <copyright file="ISubscriptionPaymentModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISubscriptionPaymentModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    using System;
    using Models;

    /// <summary>Interface for subscription model.</summary>
    public interface ISubscriptionPaymentModel
    {
        /// <summary>Gets or sets the payment.</summary>
        /// <value>The payment.</value>
        IPaymentModel? Payment { get; set; }

        /// <summary>Gets or sets the subscription.</summary>
        /// <value>The subscription.</value>
        ISubscriptionModel? Subscription { get; set; }

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        decimal Amount { get; set; }

        /// <summary>Gets or sets the number of payments.</summary>
        /// <value>The total number of payments.</value>
        int? NumberOfPayments { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the frequency.</summary>
        /// <value>The frequency.</value>
        string? Frequency { get; set; }

        /// <summary>Gets or sets the subscription reference number.</summary>
        /// <value>The subscription reference number.</value>
        string? SubscriptionReferenceNumber { get; set; }

        /// <summary>Gets or sets a value indicating whether the automatic renewal.</summary>
        /// <value>True if automatic renewal, false if not.</value>
        bool AutomaticRenewal { get; set; }

        /// <summary>Gets or sets the payment remaining.</summary>
        /// <value>The payment remaining.</value>
        int PaymentRemaining { get; set; }
    }
}
