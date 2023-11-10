// <autogenerated>
// <copyright file="PriceRuleManufacturerSearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Price Rule Manufacturer search.</summary>
    public partial class PriceRuleManufacturerSearchModel
        : AmARelationshipTableBaseSearchModel
        , IPriceRuleManufacturerSearchModel
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
        #region IAmFilterableByManufacturerSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ManufacturerID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Manufacturer ID For Search, Note: This will be overridden on data calls automatically")]
        public int? ManufacturerID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ManufacturerKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Manufacturer Key for objects")]
        public string? ManufacturerKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ManufacturerName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Manufacturer Name for objects")]
        public string? ManufacturerName { get; set; }
        #endregion
    }
}
