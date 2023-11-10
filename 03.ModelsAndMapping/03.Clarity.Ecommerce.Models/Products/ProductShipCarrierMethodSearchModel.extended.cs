// <copyright file="ProductShipCarrierMethodSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product ship carrier method search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;

    /// <summary>A data Model for the product ship carrier method search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IProductShipCarrierMethodSearchModel"/>
    public partial class ProductShipCarrierMethodSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinQuantity), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MinQuantity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxQuantity), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MaxQuantity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(From), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? From { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(To), DataType = "DateTime?", ParameterType = "query", IsRequired = false)]
        public DateTime? To { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ShipCarrierMethodIDs), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public List<int?>? ShipCarrierMethodIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CurrencyKey), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? CurrencyKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CurrencyName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? CurrencyName { get; set; }
    }
}
