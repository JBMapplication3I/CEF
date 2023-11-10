// <copyright file="ISubscriptionSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISubscriptionSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for subscription search model.</summary>
    /// <seealso cref="INameableBaseSearchModel"/>
    public partial interface ISubscriptionSearchModel
    {
        /// <summary>Gets or sets the minimum cover date.</summary>
        /// <value>The minimum cover date.</value>
        DateTime? MinCoverDate { get; set; }

        /// <summary>Gets or sets the maximum cover date.</summary>
        /// <value>The maximum cover date.</value>
        DateTime? MaxCoverDate { get; set; }

        /// <summary>Gets or sets the state of the can upgrade.</summary>
        /// <value>The can upgrade state.</value>
        bool? CanUpgradeState { get; set; }

        /// <summary>Gets or sets the state of the automatic renew.</summary>
        /// <value>The automatic renew state.</value>
        bool? AutoRenewState { get; set; }

        /// <summary>Gets or sets the identifier of the payment.</summary>
        /// <value>The identifier of the payment.</value>
        int? PaymentID { get; set; }

        /// <summary>Gets or sets the identifier of the payment type.</summary>
        /// <value>The identifier of the payment type.</value>
        int? PaymentTypeID { get; set; }

        /// <summary>Gets or sets the account key.</summary>
        /// <value>The account key.</value>
        string? AccountKey { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        string? UserKey { get; set; }
    }
}
