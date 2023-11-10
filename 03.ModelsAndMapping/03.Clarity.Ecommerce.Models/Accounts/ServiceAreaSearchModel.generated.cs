// <autogenerated>
// <copyright file="ServiceAreaSearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Service Area search.</summary>
    public partial class ServiceAreaSearchModel
        : BaseSearchModel
        , IServiceAreaSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(AddressID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContractorID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? ContractorID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinRadius), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MinRadius { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxRadius), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MaxRadius { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchRadius), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MatchRadius { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchRadiusIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchRadiusIncludeNull { get; set; }
    }
}
