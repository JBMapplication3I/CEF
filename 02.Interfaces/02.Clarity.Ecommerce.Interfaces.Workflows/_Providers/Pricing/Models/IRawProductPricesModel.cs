// <copyright file="IRawProductPricesModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRawProductPricesModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for raw product prices model.</summary>
    public interface IRawProductPricesModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int ID { get; set; }

        /// <summary>Gets or sets the price base.</summary>
        /// <value>The price base.</value>
        public decimal PriceBase { get; set; }

        /// <summary>Gets or sets the price sale.</summary>
        /// <value>The price sale.</value>
        public decimal? PriceSale { get; set; }

        /// <summary>Gets or sets the price msrp.</summary>
        /// <value>The price msrp.</value>
        public decimal? PriceMsrp { get; set; }

        /// <summary>Gets or sets the price reduction.</summary>
        /// <value>The price reduction.</value>
        public decimal? PriceReduction { get; set; }
    }
}
