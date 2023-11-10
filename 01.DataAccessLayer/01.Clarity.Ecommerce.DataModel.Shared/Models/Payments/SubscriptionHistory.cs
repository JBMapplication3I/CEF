// <copyright file="SubscriptionHistory.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription history class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface ISubscriptionHistory : IAmARelationshipTable<Subscription, Payment>
    {
        #region SubscriptionHistory Properties
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
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Payments", "SubscriptionHistory")]
    public class SubscriptionHistory : Base, ISubscriptionHistory
    {
        #region IAmARelationshipTable<Subscription, Payment>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Subscription? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Payment? Slave { get; set; }
        #endregion

        #region SubscriptionHistory Properties
        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime PaymentDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool PaymentSuccess { get; set; }

        /// <inheritdoc/>
        [Required, StringIsUnicode(false), StringLength(128), DefaultValue(null)]
        public string? Memo { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int BillingPeriodsPaid { get; set; }
        #endregion
    }
}
