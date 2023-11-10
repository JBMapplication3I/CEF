// <copyright file="ISubscriptionHistorySearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISubscriptionHistorySearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for subscription history search model.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public partial interface ISubscriptionHistorySearchModel
    {
        /// <summary>Gets or sets the minimum date.</summary>
        /// <value>The minimum date.</value>
        DateTime? MinDate { get; set; }

        /// <summary>Gets or sets the maximum date.</summary>
        /// <value>The maximum date.</value>
        DateTime? MaxDate { get; set; }

        /// <summary>Gets or sets the identifier of the payment.</summary>
        /// <value>The identifier of the payment.</value>
        int? PaymentID { get; set; }

        /// <summary>Gets or sets the identifier of the subscription.</summary>
        /// <value>The identifier of the subscription.</value>
        int? SubscriptionID { get; set; }

        /// <summary>Gets or sets the state of the succeeded.</summary>
        /// <value>The succeeded state.</value>
        bool? SucceededState { get; set; }
    }
}
