// <copyright file="RateQuoteSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the rate quote search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the rate quote search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IRateQuoteSearchModel"/>
    public partial class RateQuoteSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ShipCarrierID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? ShipCarrierID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ShipCarrierKey), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? ShipCarrierKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ShipCarrierName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? ShipCarrierName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ShipCarrierMethodKey), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? ShipCarrierMethodKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ShipCarrierMethodName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? ShipCarrierMethodName { get; set; }
    }
}
