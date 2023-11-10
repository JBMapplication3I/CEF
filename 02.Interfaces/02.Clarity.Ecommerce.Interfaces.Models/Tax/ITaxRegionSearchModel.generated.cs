// <autogenerated>
// <copyright file="TaxRegion.generated.cs" company="clarity-ventures.com">
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

    /// <summary>Interface for tax region model.</summary>
    /// <seealso cref="INameableBaseSearchModel"/>
    public partial interface ITaxRegionSearchModel
        : INameableBaseSearchModel
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

        /// <summary>Gets or sets the RegionID.</summary>
        /// <value>The RegionID.</value>
        int? RegionID { get; set; }
    }
}
