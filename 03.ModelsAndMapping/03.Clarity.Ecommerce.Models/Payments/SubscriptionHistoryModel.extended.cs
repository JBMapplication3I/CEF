// <copyright file="SubscriptionHistoryModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription history model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;

    public partial class SubscriptionHistoryModel
    {
        /// <inheritdoc/>
        public DateTime PaymentDate { get; set; }

        /// <inheritdoc/>
        public bool PaymentSuccess { get; set; }

        /// <inheritdoc/>
        public string? Memo { get; set; }

        /// <inheritdoc/>
        public int BillingPeriodsPaid { get; set; }
    }
}
