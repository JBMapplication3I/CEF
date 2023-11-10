// <autogenerated>
// <copyright file="AppliedSalesOrderDiscountSearchModel.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SearchModel Classes generated to provide base setups.</summary>
// <remarks>This file was auto-generated by SearchModels.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable MissingXmlDoc, PartialTypeWithSinglePart, RedundantExtendsListEntry, RedundantUsingDirective, UnusedMember.Global
#nullable enable
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data model for the Applied Sales Order Discount search.</summary>
    public partial class AppliedSalesOrderDiscountSearchModel
        : AmARelationshipTableBaseSearchModel
        , IAppliedSalesOrderDiscountSearchModel
    {
        #region IAmARelationshipTableBaseSearchModel

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SlaveName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Name of the Slave Record [Optional]")]
        public string? SlaveName { get; set; }
        #endregion
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinDiscountTotal), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MinDiscountTotal { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxDiscountTotal), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MaxDiscountTotal { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchDiscountTotal), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MatchDiscountTotal { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinApplicationsUsed), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MinApplicationsUsed { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxApplicationsUsed), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MaxApplicationsUsed { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchApplicationsUsed), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MatchApplicationsUsed { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchApplicationsUsedIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchApplicationsUsedIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinTargetApplicationsUsed), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MinTargetApplicationsUsed { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxTargetApplicationsUsed), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MaxTargetApplicationsUsed { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchTargetApplicationsUsed), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MatchTargetApplicationsUsed { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchTargetApplicationsUsedIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchTargetApplicationsUsedIncludeNull { get; set; }
    }
}
