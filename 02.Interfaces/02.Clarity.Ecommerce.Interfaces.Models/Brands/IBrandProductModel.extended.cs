// <copyright file="IBrandProductModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IBrandProductModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for brand product model.</summary>
    public partial interface IBrandProductModel
    {
        /// <summary>Gets or sets a value indicating whether this IBrandProduct is visible in brand.</summary>
        /// <value>True if this IBrandProduct is visible in brand, false if not.</value>
        bool IsVisibleIn { get; set; }

        /// <summary>Gets or sets the price base.</summary>
        /// <value>The price base.</value>
        decimal? PriceBase { get; set; }

        /// <summary>Gets or sets the price msrp.</summary>
        /// <value>The price msrp.</value>
        decimal? PriceMsrp { get; set; }

        /// <summary>Gets or sets the price reduction.</summary>
        /// <value>The price reduction.</value>
        decimal? PriceReduction { get; set; }

        /// <summary>Gets or sets the price sale.</summary>
        /// <value>The price sale.</value>
        decimal? PriceSale { get; set; }
    }
}
