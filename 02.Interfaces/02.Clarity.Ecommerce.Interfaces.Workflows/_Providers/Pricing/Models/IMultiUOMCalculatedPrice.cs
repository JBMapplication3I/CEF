// <copyright file="IMultiUOMCalculatedPrice.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMultiUOMCalculatedPrice interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for calculated price.</summary>
    public interface IMultiUOMCalculatedPrice
    {
        /// <summary>Gets or sets the base price.</summary>
        /// <value>The base price.</value>
        decimal BasePrice { get; set; }

        /// <summary>Gets or sets the sale price.</summary>
        /// <value>The sale price.</value>
        decimal? SalePrice { get; set; }

        /// <summary>Gets or sets the msrp price.</summary>
        /// <value>The msrp price.</value>
        decimal? MsrpPrice { get; set; }

        /// <summary>Gets or sets the reduction price.</summary>
        /// <value>The reduction price.</value>
        decimal? ReductionPrice { get; set; }

        /// <summary>Gets or sets the pricing provider.</summary>
        /// <value>The pricing provider.</value>
        string? PricingProvider { get; set; }

        /// <summary>Gets or sets the price key.</summary>
        /// <value>The price key.</value>
        string? PriceKey { get; set; }

        /// <summary>Gets or sets the price key alternate.</summary>
        /// <value>The price key alternate.</value>
        string? PriceKeyAlt { get; set; }

        /// <summary>Gets or sets the relevant rules.</summary>
        /// <value>The relevant rules.</value>
        List<IPriceRuleModel>? RelevantRules { get; set; }

        /// <summary>Gets or sets the used rules.</summary>
        /// <value>The used rules.</value>
        List<IPriceRuleModel>? UsedRules { get; set; }

        /// <summary>Gets a value indicating whether this CalculatedPrice is valid.</summary>
        /// <value>True if this CalculatedPrice is valid, false if not.</value>
        bool IsValid { get; }

        string? ProductUnitOfMeasure { get; set; }
    }
}