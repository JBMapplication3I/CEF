// <autogenerated>
// <copyright file="AppliedCartDiscount.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ISearchModel Interfaces generated to provide base setups.</summary>
// <remarks>This file was auto-generated by ISearchModels.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable BadEmptyBracesLineBreaks, PartialTypeWithSinglePart, RedundantExtendsListEntry, RedundantUsingDirective
#pragma warning disable IDE0005_gen
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for applied cart discount model.</summary>
    /// <seealso cref="IAmARelationshipTableBaseSearchModel"/>
    /// <seealso cref="IAmARelationshipTableBaseSearchModel"/>
    public partial interface IAppliedCartDiscountSearchModel
        : IBaseSearchModel
            , IAmARelationshipTableBaseSearchModel
    {
        /// <summary>Gets or sets the minimum DiscountTotal.</summary>
        /// <value>The minimum DiscountTotal.</value>
        decimal? MinDiscountTotal { get; set; }

        /// <summary>Gets or sets the maximum DiscountTotal.</summary>
        /// <value>The maximum DiscountTotal.</value>
        decimal? MaxDiscountTotal { get; set; }

        /// <summary>Gets or sets the match DiscountTotal.</summary>
        /// <value>The match DiscountTotal.</value>
        decimal? MatchDiscountTotal { get; set; }

        /// <summary>Gets or sets the minimum ApplicationsUsed.</summary>
        /// <value>The minimum ApplicationsUsed.</value>
        int? MinApplicationsUsed { get; set; }

        /// <summary>Gets or sets the maximum ApplicationsUsed.</summary>
        /// <value>The maximum ApplicationsUsed.</value>
        int? MaxApplicationsUsed { get; set; }

        /// <summary>Gets or sets the match ApplicationsUsed.</summary>
        /// <value>The match ApplicationsUsed.</value>
        int? MatchApplicationsUsed { get; set; }

        /// <summary>Gets or sets the ApplicationsUsed when matching must include nulls.</summary>
        /// <value>The ApplicationsUsed when matching must include nulls.</value>
        bool? MatchApplicationsUsedIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum TargetApplicationsUsed.</summary>
        /// <value>The minimum TargetApplicationsUsed.</value>
        int? MinTargetApplicationsUsed { get; set; }

        /// <summary>Gets or sets the maximum TargetApplicationsUsed.</summary>
        /// <value>The maximum TargetApplicationsUsed.</value>
        int? MaxTargetApplicationsUsed { get; set; }

        /// <summary>Gets or sets the match TargetApplicationsUsed.</summary>
        /// <value>The match TargetApplicationsUsed.</value>
        int? MatchTargetApplicationsUsed { get; set; }

        /// <summary>Gets or sets the TargetApplicationsUsed when matching must include nulls.</summary>
        /// <value>The TargetApplicationsUsed when matching must include nulls.</value>
        bool? MatchTargetApplicationsUsedIncludeNull { get; set; }

        /// <summary>Gets or sets the name of the slave.</summary>
        /// <value>The name of the slave.</value>
        string? SlaveName { get; set; }
    }
}
