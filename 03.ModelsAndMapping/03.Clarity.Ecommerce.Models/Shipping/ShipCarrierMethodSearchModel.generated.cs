// <autogenerated>
// <copyright file="ShipCarrierMethodSearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Ship Carrier Method search.</summary>
    public partial class ShipCarrierMethodSearchModel
        : NameableBaseSearchModel
        , IShipCarrierMethodSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ShipCarrierID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? ShipCarrierID { get; set; }
    }
}
