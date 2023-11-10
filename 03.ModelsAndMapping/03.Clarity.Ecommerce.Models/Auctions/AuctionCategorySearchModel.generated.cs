// <autogenerated>
// <copyright file="AuctionCategorySearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Auction Category search.</summary>
    public partial class AuctionCategorySearchModel
        : AmARelationshipTableBaseSearchModel
        , IAuctionCategorySearchModel
    {
        #region IAmARelationshipTableBaseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Name of the Master Record [Optional]")]
        public string? MasterName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SlaveName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Name of the Slave Record [Optional]")]
        public string? SlaveName { get; set; }
        #endregion
        #region IAmFilterableByCategorySearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(CategoryID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Category ID For Search, Note: This will be overridden on data calls automatically")]
        public int? CategoryID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CategoryKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Category Key for objects")]
        public string? CategoryKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CategoryName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Category Name for objects")]
        public string? CategoryName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CategorySeoUrl), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Category SEO URL for objects")]
        public string? CategorySeoUrl { get; set; }
        #endregion
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinSortOrder), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MinSortOrder { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxSortOrder), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MaxSortOrder { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchSortOrder), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MatchSortOrder { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchSortOrderIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchSortOrderIncludeNull { get; set; }
    }
}
