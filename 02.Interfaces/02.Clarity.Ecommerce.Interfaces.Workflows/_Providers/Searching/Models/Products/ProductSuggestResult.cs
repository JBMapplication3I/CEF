// <copyright file="ProductSuggestResult.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product suggest result class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>A product suggest result.</summary>
    /// <seealso cref="SuggestResultBase"/>
    public class ProductSuggestResult : SuggestResultBase
    {
        /// <summary>Gets or sets the name of the brand.</summary>
        /// <value>The name of the brand.</value>
        public string? BrandName { get; set; }

        /// <summary>Gets or sets the manufacturer part number.</summary>
        /// <value>The manufacturer part number.</value>
        public string? ManufacturerPartNumber { get; set; }

        /// <summary>Gets or sets the total number of purchased quantity.</summary>
        /// <value>The total number of purchased quantity.</value>
        public decimal? TotalPurchasedQuantity { get; set; }
    }
}
