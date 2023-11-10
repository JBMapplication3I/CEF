// <autogenerated>
// <copyright file="BrandAccountSearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Brand Account search.</summary>
    public partial class BrandAccountSearchModel
        : AmARelationshipTableBaseSearchModel
        , IBrandAccountSearchModel
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
        #region IAmFilterableByAccountSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Account ID For Search, Note: This will be overridden on data calls automatically")]
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, allow matches to null for AccountID field")]
        public bool? AccountIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Account Key for objects")]
        public string? AccountKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Account Name for objects")]
        public string? AccountName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, the value of the AccountName field must match exactly, otherwise, a case-insentive contains check is run.")]
        public bool? AccountNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, allow matches to null for AccountName field")]
        public bool? AccountNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountIDOrCustomKeyOrNameOrDescription), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? AccountIDOrCustomKeyOrNameOrDescription { get; set; }
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
        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsVisibleIn), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? IsVisibleIn { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PricePointID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? PricePointID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PricePointIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? PricePointIDIncludeNull { get; set; }
    }
}
