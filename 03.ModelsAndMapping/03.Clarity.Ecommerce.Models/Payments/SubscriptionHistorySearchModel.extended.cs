// <copyright file="SubscriptionHistorySearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription history search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using ServiceStack;

    /// <summary>A data Model for the subscription history search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.ISubscriptionHistorySearchModel"/>
    public partial class SubscriptionHistorySearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "Minimum Date")]
        public DateTime? MinDate { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "Maximum Date")]
        public DateTime? MaxDate { get; set; }

        /// <inheritdoc/>
        public int? PaymentID { get; set; }

        /// <inheritdoc/>
        public int? SubscriptionID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SucceededState), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "Succeeded State")]
        public bool? SucceededState { get; set; }
    }
}
