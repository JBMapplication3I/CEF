// <autogenerated>
// <copyright file="CountryCurrencySearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Country Currency search.</summary>
    public partial class CountryCurrencySearchModel
        : AmARelationshipTableBaseSearchModel
        , ICountryCurrencySearchModel
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
    }
}
