// <copyright file="RawProductPricesModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the raw product prices model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the raw product prices.</summary>
    /// <seealso cref="IRawProductPricesModel"/>
    public class RawProductPricesModel : IRawProductPricesModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int ID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PriceBase), DataType = "decimal", ParameterType = "body", IsRequired = true)]
        public decimal PriceBase { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PriceSale), DataType = "decimal?", ParameterType = "body", IsRequired = false)]
        public decimal? PriceSale { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PriceMsrp), DataType = "decimal?", ParameterType = "body", IsRequired = false)]
        public decimal? PriceMsrp { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PriceReduction), DataType = "decimal?", ParameterType = "body", IsRequired = false)]
        public decimal? PriceReduction { get; set; }
    }
}
