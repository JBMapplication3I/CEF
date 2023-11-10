// <autogenerated>
// <copyright file="Ad.generated.cs" company="clarity-ventures.com">
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

    /// <summary>Interface for ad model.</summary>
    /// <seealso cref="INameableBaseSearchModel"/>
    /// <seealso cref="IAmFilterableByAccountSearchModel"/>
    /// <seealso cref="IAmFilterableByBrandSearchModel"/>
    /// <seealso cref="IAmFilterableByFranchiseSearchModel"/>
    /// <seealso cref="IAmFilterableByStoreSearchModel"/>
    /// <seealso cref="IHaveATypeBaseSearchModel"/>
    /// <seealso cref="IHaveAStatusBaseSearchModel"/>
    public partial interface IAdSearchModel
        : INameableBaseSearchModel
            , IAmFilterableByAccountSearchModel
            , IAmFilterableByBrandSearchModel
            , IAmFilterableByFranchiseSearchModel
            , IAmFilterableByStoreSearchModel
            , IHaveATypeBaseSearchModel
            , IHaveAStatusBaseSearchModel
    {
        /// <summary>Gets or sets the minimum EndDate.</summary>
        /// <value>The minimum EndDate.</value>
        DateTime? MinEndDate { get; set; }

        /// <summary>Gets or sets the maximum EndDate.</summary>
        /// <value>The maximum EndDate.</value>
        DateTime? MaxEndDate { get; set; }

        /// <summary>Gets or sets the match EndDate.</summary>
        /// <value>The match EndDate.</value>
        DateTime? MatchEndDate { get; set; }

        /// <summary>Gets or sets the minimum ExpirationDate.</summary>
        /// <value>The minimum ExpirationDate.</value>
        DateTime? MinExpirationDate { get; set; }

        /// <summary>Gets or sets the maximum ExpirationDate.</summary>
        /// <value>The maximum ExpirationDate.</value>
        DateTime? MaxExpirationDate { get; set; }

        /// <summary>Gets or sets the match ExpirationDate.</summary>
        /// <value>The match ExpirationDate.</value>
        DateTime? MatchExpirationDate { get; set; }

        /// <summary>Gets or sets the minimum StartDate.</summary>
        /// <value>The minimum StartDate.</value>
        DateTime? MinStartDate { get; set; }

        /// <summary>Gets or sets the maximum StartDate.</summary>
        /// <value>The maximum StartDate.</value>
        DateTime? MaxStartDate { get; set; }

        /// <summary>Gets or sets the match StartDate.</summary>
        /// <value>The match StartDate.</value>
        DateTime? MatchStartDate { get; set; }

        /// <summary>Gets or sets the minimum Weight.</summary>
        /// <value>The minimum Weight.</value>
        decimal? MinWeight { get; set; }

        /// <summary>Gets or sets the maximum Weight.</summary>
        /// <value>The maximum Weight.</value>
        decimal? MaxWeight { get; set; }

        /// <summary>Gets or sets the match Weight.</summary>
        /// <value>The match Weight.</value>
        decimal? MatchWeight { get; set; }

        /// <summary>Gets or sets the ClickCounterID.</summary>
        /// <value>The ClickCounterID.</value>
        int? ClickCounterID { get; set; }

        /// <summary>Gets or sets the ClickCounterID when matching must include nulls.</summary>
        /// <value>The ClickCounterID when matching must include nulls.</value>
        bool? ClickCounterIDIncludeNull { get; set; }

        /// <summary>Gets or sets the ImpressionCounterID.</summary>
        /// <value>The ImpressionCounterID.</value>
        int? ImpressionCounterID { get; set; }

        /// <summary>Gets or sets the ImpressionCounterID when matching must include nulls.</summary>
        /// <value>The ImpressionCounterID when matching must include nulls.</value>
        bool? ImpressionCounterIDIncludeNull { get; set; }

        /// <summary>Gets or sets the Caption.</summary>
        /// <value>The Caption.</value>
        string? Caption { get; set; }

        /// <summary>Gets or sets the match Caption strict requirement.</summary>
        /// <value>The match Caption strict requirement.</value>
        bool? CaptionStrict { get; set; }

        /// <summary>Gets or sets the Caption when matching must include nulls.</summary>
        /// <value>The Caption when matching must include nulls.</value>
        bool? CaptionIncludeNull { get; set; }

        /// <summary>Gets or sets the TargetURL.</summary>
        /// <value>The TargetURL.</value>
        string? TargetURL { get; set; }

        /// <summary>Gets or sets the match TargetURL strict requirement.</summary>
        /// <value>The match TargetURL strict requirement.</value>
        bool? TargetURLStrict { get; set; }

        /// <summary>Gets or sets the TargetURL when matching must include nulls.</summary>
        /// <value>The TargetURL when matching must include nulls.</value>
        bool? TargetURLIncludeNull { get; set; }
    }
}
