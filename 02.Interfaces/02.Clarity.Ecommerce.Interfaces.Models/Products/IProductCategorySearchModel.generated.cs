// <autogenerated>
// <copyright file="ProductCategory.generated.cs" company="clarity-ventures.com">
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

    /// <summary>Interface for product category model.</summary>
    /// <seealso cref="IAmARelationshipTableBaseSearchModel"/>
    /// <seealso cref="IAmARelationshipTableBaseSearchModel"/>
    /// <seealso cref="IAmFilterableByCategorySearchModel"/>
    /// <seealso cref="IAmFilterableByProductSearchModel"/>
    public partial interface IProductCategorySearchModel
        : IBaseSearchModel
            , IAmARelationshipTableBaseSearchModel
            , IAmFilterableByCategorySearchModel
            , IAmFilterableByProductSearchModel
    {
        /// <summary>Gets or sets the minimum SortOrder.</summary>
        /// <value>The minimum SortOrder.</value>
        int? MinSortOrder { get; set; }

        /// <summary>Gets or sets the maximum SortOrder.</summary>
        /// <value>The maximum SortOrder.</value>
        int? MaxSortOrder { get; set; }

        /// <summary>Gets or sets the match SortOrder.</summary>
        /// <value>The match SortOrder.</value>
        int? MatchSortOrder { get; set; }

        /// <summary>Gets or sets the SortOrder when matching must include nulls.</summary>
        /// <value>The SortOrder when matching must include nulls.</value>
        bool? MatchSortOrderIncludeNull { get; set; }

        /// <summary>Gets or sets the name of the master.</summary>
        /// <value>The name of the master.</value>
        string? MasterName { get; set; }

        /// <summary>Gets or sets the name of the slave.</summary>
        /// <value>The name of the slave.</value>
        string? SlaveName { get; set; }
    }
}
