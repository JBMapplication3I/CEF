// <autogenerated>
// <copyright file="RegionSearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Region search.</summary>
    public partial class RegionSearchModel
        : NameableBaseSearchModel
        , IRegionSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(CountryID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? CountryID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Code), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? Code { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CodeStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? CodeStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CodeIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? CodeIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO31661), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? ISO31661 { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO31661Strict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ISO31661Strict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO31661IncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ISO31661IncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO31662), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? ISO31662 { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO31662Strict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ISO31662Strict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO31662IncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ISO31662IncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO3166Alpha2), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? ISO3166Alpha2 { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO3166Alpha2Strict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ISO3166Alpha2Strict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO3166Alpha2IncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ISO3166Alpha2IncludeNull { get; set; }
    }
}
