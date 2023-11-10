// <copyright file="SubscriptionSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using ServiceStack;

    /// <summary>A data Model for the subscription search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.ISubscriptionSearchModel"/>
    public partial class SubscriptionSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinCoverDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "Min Cover Date")]
        public DateTime? MinCoverDate { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxCoverDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "Max Cover Date")]
        public DateTime? MaxCoverDate { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CanUpgradeState), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "Can Upgrade State")]
        public bool? CanUpgradeState { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AutoRenewState), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "Auto Renew State")]
        public bool? AutoRenewState { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PaymentID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Payment ID")]
        public int? PaymentID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PaymentTypeID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "PaymentType ID")]
        public int? PaymentTypeID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Account Key")]
        public string? AccountKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "User Key")]
        public string? UserKey { get; set; }
    }
}
