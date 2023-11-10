// <copyright file="MultiUOMCalculatedPrice.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the MultiUOMCalculatedPrice class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A Calculated Price.</summary>
    /// <seealso cref="IMultiUOMCalculatedPrice"/>
    public class MultiUOMCalculatedPrice : IMultiUOMCalculatedPrice
    {
        /// <summary>Gets or sets the base price.</summary>
        /// <value>The base price.</value>
        public decimal BasePrice { get; set; }

        /// <summary>Gets or sets the sale price.</summary>
        /// <value>The sale price.</value>
        public decimal? SalePrice { get; set; }

        /// <summary>Gets or sets the msrp price.</summary>
        /// <value>The msrp price.</value>
        public decimal? MsrpPrice { get; set; }

        /// <summary>Gets or sets the reduction price.</summary>
        /// <value>The reduction price.</value>
        public decimal? ReductionPrice { get; set; }

        /// <summary>Gets or sets the pricing provider.</summary>
        /// <value>The pricing provider.</value>
        public string? PricingProvider { get; set; }

        /// <summary>Gets or sets the price key.</summary>
        /// <value>The price key.</value>
        public string? PriceKey { get; set; }

        /// <summary>Gets or sets the price key alternate.</summary>
        /// <value>The price key alternate.</value>
        public string? PriceKeyAlt { get; set; }

        /// <summary>Gets or sets the relevant rules.</summary>
        /// <value>The relevant rules.</value>
        public List<IPriceRuleModel>? RelevantRules { get; set; }

        /// <summary>Gets or sets the used rules.</summary>
        /// <value>The used rules.</value>
        public List<IPriceRuleModel>? UsedRules { get; set; }

        /// <summary>Gets a value indicating whether this CalculatedPrice is valid.</summary>
        /// <value>True if this CalculatedPrice is valid, false if not.</value>
        public bool IsValid { get; }

        public string? ProductUnitOfMeasure { get; set; }

        public string? PriceListName { get; set; }
    }
}