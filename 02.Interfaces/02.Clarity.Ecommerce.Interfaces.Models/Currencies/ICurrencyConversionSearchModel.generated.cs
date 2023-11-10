// <autogenerated>
// <copyright file="CurrencyConversion.generated.cs" company="clarity-ventures.com">
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

    /// <summary>Interface for currency conversion model.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public partial interface ICurrencyConversionSearchModel
        : IBaseSearchModel
    {
        /// <summary>Gets or sets the minimum Rate.</summary>
        /// <value>The minimum Rate.</value>
        decimal? MinRate { get; set; }

        /// <summary>Gets or sets the maximum Rate.</summary>
        /// <value>The maximum Rate.</value>
        decimal? MaxRate { get; set; }

        /// <summary>Gets or sets the match Rate.</summary>
        /// <value>The match Rate.</value>
        decimal? MatchRate { get; set; }

        /// <summary>Gets or sets the EndingCurrencyID.</summary>
        /// <value>The EndingCurrencyID.</value>
        int? EndingCurrencyID { get; set; }

        /// <summary>Gets or sets the StartingCurrencyID.</summary>
        /// <value>The StartingCurrencyID.</value>
        int? StartingCurrencyID { get; set; }

        /// <summary>Gets or sets the minimum EndDate.</summary>
        /// <value>The minimum EndDate.</value>
        DateTime? MinEndDate { get; set; }

        /// <summary>Gets or sets the maximum EndDate.</summary>
        /// <value>The maximum EndDate.</value>
        DateTime? MaxEndDate { get; set; }

        /// <summary>Gets or sets the match EndDate.</summary>
        /// <value>The match EndDate.</value>
        DateTime? MatchEndDate { get; set; }

        /// <summary>Gets or sets the EndDate when matching must include nulls.</summary>
        /// <value>The EndDate when matching must include nulls.</value>
        bool? MatchEndDateIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum StartDate.</summary>
        /// <value>The minimum StartDate.</value>
        DateTime? MinStartDate { get; set; }

        /// <summary>Gets or sets the maximum StartDate.</summary>
        /// <value>The maximum StartDate.</value>
        DateTime? MaxStartDate { get; set; }

        /// <summary>Gets or sets the match StartDate.</summary>
        /// <value>The match StartDate.</value>
        DateTime? MatchStartDate { get; set; }

        /// <summary>Gets or sets the StartDate when matching must include nulls.</summary>
        /// <value>The StartDate when matching must include nulls.</value>
        bool? MatchStartDateIncludeNull { get; set; }
    }
}
