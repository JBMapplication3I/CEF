// <copyright file="IPriceRuleProductModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPriceRuleProductModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IPriceRuleProductModel
    {
        /// <summary>Gets or sets a value indicating whether the override price.</summary>
        /// <value>True if override price, false if not.</value>
        bool OverridePrice { get; set; }

        /// <summary>Gets or sets the override base price.</summary>
        /// <value>The override base price.</value>
        decimal? OverrideBasePrice { get; set; }

        /// <summary>Gets or sets the override sale price.</summary>
        /// <value>The override sale price.</value>
        decimal? OverrideSalePrice { get; set; }
    }
}
