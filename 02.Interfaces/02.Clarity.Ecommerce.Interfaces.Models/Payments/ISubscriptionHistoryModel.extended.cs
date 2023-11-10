// <copyright file="ISubscriptionHistoryModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISubscriptionHistoryModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for subscription history model.</summary>
    /// <seealso cref="IBaseModel"/>
    public partial interface ISubscriptionHistoryModel
    {
        /// <summary>Gets or sets the payment date.</summary>
        /// <value>The payment date.</value>
        DateTime PaymentDate { get; set; }

        /// <summary>Gets or sets a value indicating whether the payment success.</summary>
        /// <value>True if payment success, false if not.</value>
        bool PaymentSuccess { get; set; }

        /// <summary>Gets or sets the memo.</summary>
        /// <value>The memo.</value>
        string? Memo { get; set; }

        /// <summary>Gets or sets the billing periods paid.</summary>
        /// <value>The billing periods paid.</value>
        int BillingPeriodsPaid { get; set; }
    }
}
