// <copyright file="FranchiseProductModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise product model class</summary>
namespace Clarity.Ecommerce.Models
{
    public partial class FranchiseProductModel
    {
        /// <inheritdoc/>
        public bool IsVisibleIn { get; set; }

        /// <inheritdoc/>
        public decimal? PriceBase { get; set; }

        /// <inheritdoc/>
        public decimal? PriceMsrp { get; set; }

        /// <inheritdoc/>
        public decimal? PriceReduction { get; set; }

        /// <inheritdoc/>
        public decimal? PriceSale { get; set; }
    }
}
