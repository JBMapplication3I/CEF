// <copyright file="RawProductPricesModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the raw product prices model class</summary>
// ReSharper disable MissingXmlDoc
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;
    using ServiceStack;

    /// <summary>A data Model for the raw product prices.</summary>
    [PublicAPI]
    public partial class RawProductPricesModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = "ID", DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "")]
        public int ID { get; set; }

        /// <summary>Gets or sets the price base.</summary>
        /// <value>The price base.</value>
        [ApiMember(Name = "PriceBase", DataType = "decimal", ParameterType = "body", IsRequired = true,
            Description = "")]
        public decimal PriceBase { get; set; }

        /// <summary>Gets or sets the price sale.</summary>
        /// <value>The price sale.</value>
        [ApiMember(Name = "PriceSale", DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "")]
        public decimal? PriceSale { get; set; }

        /// <summary>Gets or sets the price msrp.</summary>
        /// <value>The price msrp.</value>
        [ApiMember(Name = "PriceMsrp", DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "")]
        public decimal? PriceMsrp { get; set; }

        /// <summary>Gets or sets the price reduction.</summary>
        /// <value>The price reduction.</value>
        [ApiMember(Name = "PriceReduction", DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "")]
        public decimal? PriceReduction { get; set; }
    }
}
