// <autogenerated>
// <copyright file="BrandUserSearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Brand User search.</summary>
    public partial class BrandUserSearchModel
        : AmARelationshipTableBaseSearchModel
        , IBrandUserSearchModel
    {
        #region IAmARelationshipTableBaseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Name of the Master Record [Optional]")]
        public string? MasterName { get; set; }
        #endregion
        #region IAmFilterableByBrandSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Brand ID For Search, Note: This will be overridden on data calls automatically")]
        public int? BrandID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? BrandIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Brand Key for objects")]
        public string? BrandKey { get => MasterKey; set => MasterKey = value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Brand Name for objects")]
        public string? BrandName { get => MasterName; set => MasterName = value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? BrandNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? BrandNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandCategoryID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a brand which uses this category")]
        public int? BrandCategoryID { get; set; }
        #endregion
        #region IAmFilterableByUserSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "User ID For Search, Note: This will be overridden on data calls automatically")]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, allow matches to null for UserID field")]
        public bool? UserIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The User Key for objects")]
        public string? UserKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserUsername), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The User Name for objects")]
        public string? UserUsername { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserIDOrCustomKeyOrUserName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "General search against User assigned to the object (includes UserName even though the name of the property doesn't say it)")]
        public string? UserIDOrCustomKeyOrUserName { get; set; }
        #endregion
    }
}
