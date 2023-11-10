// <copyright file="PriceRuleProductModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule product model class</summary>
namespace Clarity.Ecommerce.Models
{
    public partial class PriceRuleProductModel
    {
        /// <inheritdoc/>
        public bool OverridePrice { get; set; }

        /// <inheritdoc/>
        public decimal? OverrideBasePrice { get; set; }

        /// <inheritdoc/>
        public decimal? OverrideSalePrice { get; set; }
    }
}
