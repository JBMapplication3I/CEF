// <autogenerated>
// <copyright file="PriceRounding.generated.cs" company="clarity-ventures.com">
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

    /// <summary>Interface for price rounding model.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public partial interface IPriceRoundingSearchModel
        : IBaseSearchModel
    {
        /// <summary>Gets or sets the minimum RoundHow.</summary>
        /// <value>The minimum RoundHow.</value>
        int? MinRoundHow { get; set; }

        /// <summary>Gets or sets the maximum RoundHow.</summary>
        /// <value>The maximum RoundHow.</value>
        int? MaxRoundHow { get; set; }

        /// <summary>Gets or sets the match RoundHow.</summary>
        /// <value>The match RoundHow.</value>
        int? MatchRoundHow { get; set; }

        /// <summary>Gets or sets the minimum RoundingAmount.</summary>
        /// <value>The minimum RoundingAmount.</value>
        int? MinRoundingAmount { get; set; }

        /// <summary>Gets or sets the maximum RoundingAmount.</summary>
        /// <value>The maximum RoundingAmount.</value>
        int? MaxRoundingAmount { get; set; }

        /// <summary>Gets or sets the match RoundingAmount.</summary>
        /// <value>The match RoundingAmount.</value>
        int? MatchRoundingAmount { get; set; }

        /// <summary>Gets or sets the minimum RoundTo.</summary>
        /// <value>The minimum RoundTo.</value>
        int? MinRoundTo { get; set; }

        /// <summary>Gets or sets the maximum RoundTo.</summary>
        /// <value>The maximum RoundTo.</value>
        int? MaxRoundTo { get; set; }

        /// <summary>Gets or sets the match RoundTo.</summary>
        /// <value>The match RoundTo.</value>
        int? MatchRoundTo { get; set; }

        /// <summary>Gets or sets the CurrencyKey.</summary>
        /// <value>The CurrencyKey.</value>
        string? CurrencyKey { get; set; }

        /// <summary>Gets or sets the match CurrencyKey strict requirement.</summary>
        /// <value>The match CurrencyKey strict requirement.</value>
        bool? CurrencyKeyStrict { get; set; }

        /// <summary>Gets or sets the CurrencyKey when matching must include nulls.</summary>
        /// <value>The CurrencyKey when matching must include nulls.</value>
        bool? CurrencyKeyIncludeNull { get; set; }

        /// <summary>Gets or sets the PricePointKey.</summary>
        /// <value>The PricePointKey.</value>
        string? PricePointKey { get; set; }

        /// <summary>Gets or sets the match PricePointKey strict requirement.</summary>
        /// <value>The match PricePointKey strict requirement.</value>
        bool? PricePointKeyStrict { get; set; }

        /// <summary>Gets or sets the PricePointKey when matching must include nulls.</summary>
        /// <value>The PricePointKey when matching must include nulls.</value>
        bool? PricePointKeyIncludeNull { get; set; }

        /// <summary>Gets or sets the ProductKey.</summary>
        /// <value>The ProductKey.</value>
        string? ProductKey { get; set; }

        /// <summary>Gets or sets the match ProductKey strict requirement.</summary>
        /// <value>The match ProductKey strict requirement.</value>
        bool? ProductKeyStrict { get; set; }

        /// <summary>Gets or sets the ProductKey when matching must include nulls.</summary>
        /// <value>The ProductKey when matching must include nulls.</value>
        bool? ProductKeyIncludeNull { get; set; }

        /// <summary>Gets or sets the UnitOfMeasure.</summary>
        /// <value>The UnitOfMeasure.</value>
        string? UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the match UnitOfMeasure strict requirement.</summary>
        /// <value>The match UnitOfMeasure strict requirement.</value>
        bool? UnitOfMeasureStrict { get; set; }

        /// <summary>Gets or sets the UnitOfMeasure when matching must include nulls.</summary>
        /// <value>The UnitOfMeasure when matching must include nulls.</value>
        bool? UnitOfMeasureIncludeNull { get; set; }
    }
}
