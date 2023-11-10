// <autogenerated>
// <copyright file="SalesQuoteFileSearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Sales Quote File search.</summary>
    public partial class SalesQuoteFileSearchModel
        : AmARelationshipTableNameableBaseSearchModel
        , ISalesQuoteFileSearchModel
    {
        #region IAmARelationshipTableBaseSearchModel

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SlaveName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Name of the Slave Record [Optional]")]
        public string? SlaveName { get; set; }
        #endregion
        #region IHaveSeoBaseModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoKeywords), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "SEO Keywords to use in the Meta tags of the page for this object")]
        public string? SeoKeywords { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoKeywordsStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoKeywordsStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoKeywordsIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoKeywordsIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoUrl), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "SEO URL to use to link to the page for this object")]
        public string? SeoUrl { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoUrlStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoUrlStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoUrlIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoUrlIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoDescription), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "SEO Description to use in the Meta tags of the page for this object")]
        public string? SeoDescription { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoDescriptionStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoDescriptionStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoDescriptionIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoDescriptionIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoMetaData), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "SEO General Meta Data to use in the Meta tags of the page for this object")]
        public string? SeoMetaData { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoMetaDataStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoMetaDataStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoMetaDataIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoMetaDataIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoPageTitle), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "SEO Page Title to use in the Meta tags of the page for this object")]
        public string? SeoPageTitle { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoPageTitleStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoPageTitleStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SeoPageTitleIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? SeoPageTitleIncludeNull { get; set; }
        #endregion
        /// <inheritdoc/>
        [ApiMember(Name = nameof(FileAccessTypeID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? FileAccessTypeID { get; set; }
    }
}
