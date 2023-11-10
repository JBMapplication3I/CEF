// <copyright file="BrandProductModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand product model class</summary>
namespace Clarity.Ecommerce.Models
{
    public partial class BrandProductModel
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
