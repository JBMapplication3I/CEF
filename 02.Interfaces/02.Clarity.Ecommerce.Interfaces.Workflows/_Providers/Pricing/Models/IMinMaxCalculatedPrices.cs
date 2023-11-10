// <copyright file="IMinMaxCalculatedPrices.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Calculated Price class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Wraps minimum and maximum prices for a product into a model.</summary>
    public class IMinMaxCalculatedPrices
    {
        /// <summary>Initializes a new instance of the <see cref="IMinMaxCalculatedPrices"/> class.</summary>
        /// <param name="min">The minimum price for the product.</param>
        /// <param name="max">The maximum price for the product.</param>
        public IMinMaxCalculatedPrices(ICalculatedPrice min, ICalculatedPrice max)
        {
            MinPrice = min;
            MaxPrice = max;
        }

        /// <summary>Gets or sets the minimum price.</summary>
        /// <value>The minimum price.</value>
        public ICalculatedPrice? MinPrice { get; set; }

        /// <summary>Gets or sets the maximum price.</summary>
        /// <value>The maximum price.</value>
        public ICalculatedPrice? MaxPrice { get; set; }
    }
}
