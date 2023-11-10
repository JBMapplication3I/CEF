// <autogenerated>
// <copyright file="VisitorSearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Visitor search.</summary>
    public partial class VisitorSearchModel
        : NameableBaseSearchModel
        , IVisitorSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(AddressID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AddressIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? AddressIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IPOrganizationID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? IPOrganizationID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IPOrganizationIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? IPOrganizationIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinScore), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MinScore { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxScore), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MaxScore { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchScore), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MatchScore { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchScoreIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchScoreIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? UserIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IPAddress), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? IPAddress { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IPAddressStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? IPAddressStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IPAddressIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? IPAddressIncludeNull { get; set; }
    }
}
