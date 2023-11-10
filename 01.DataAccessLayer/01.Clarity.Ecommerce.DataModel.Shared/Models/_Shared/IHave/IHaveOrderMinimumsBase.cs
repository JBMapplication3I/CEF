﻿// <copyright file="IHaveOrderMinimumsBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveOrderMinimumsBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for have order minimums base.</summary>
    public interface IHaveOrderMinimumsBase : IBase
    {
        /// <summary>Gets or sets the minimum order amount.</summary>
        /// <value>The minimum order amount.</value>
        decimal? MinimumOrderDollarAmount { get; set; }

        /// <summary>Gets or sets the minimum order amount after.</summary>
        /// <value>The minimum order amount after.</value>
        decimal? MinimumOrderDollarAmountAfter { get; set; }

        /// <summary>Gets or sets a message describing the minimum order dollar amount warning.</summary>
        /// <value>A message describing the minimum order dollar amount warning.</value>
        string? MinimumOrderDollarAmountWarningMessage { get; set; }

        /// <summary>Gets or sets the minimum order dollar amount override fee.</summary>
        /// <value>The minimum order dollar amount override fee.</value>
        decimal? MinimumOrderDollarAmountOverrideFee { get; set; }

        /// <summary>Gets or sets a value indicating whether the minimum order dollar amount override fee is percent.</summary>
        /// <value>True if minimum order dollar amount override fee is percent, false if not.</value>
        bool MinimumOrderDollarAmountOverrideFeeIsPercent { get; set; }

        /// <summary>Gets or sets a message describing the minimum order dollar amount override fee warning.</summary>
        /// <value>A message describing the minimum order dollar amount override fee warning.</value>
        string? MinimumOrderDollarAmountOverrideFeeWarningMessage { get; set; }

        /// <summary>Gets or sets a message describing the minimum order dollar amount override fee accepted.</summary>
        /// <value>A message describing the minimum order dollar amount override fee accepted.</value>
        string? MinimumOrderDollarAmountOverrideFeeAcceptedMessage { get; set; }

        /// <summary>Gets or sets the minimum order quantity amount.</summary>
        /// <value>The minimum order quantity amount.</value>
        decimal? MinimumOrderQuantityAmount { get; set; }

        /// <summary>Gets or sets the minimum order quantity amount after.</summary>
        /// <value>The minimum order quantity amount after.</value>
        decimal? MinimumOrderQuantityAmountAfter { get; set; }

        /// <summary>Gets or sets a message describing the minimum order quantity amount warning.</summary>
        /// <value>A message describing the minimum order quantity amount warning.</value>
        string? MinimumOrderQuantityAmountWarningMessage { get; set; }

        /// <summary>Gets or sets the minimum order quantity amount override fee.</summary>
        /// <value>The minimum order quantity amount override fee.</value>
        decimal? MinimumOrderQuantityAmountOverrideFee { get; set; }

        /// <summary>Gets or sets a value indicating whether the minimum order quantity amount override fee is
        /// percent.</summary>
        /// <value>True if minimum order quantity amount override fee is percent, false if not.</value>
        bool MinimumOrderQuantityAmountOverrideFeeIsPercent { get; set; }

        /// <summary>Gets or sets a message describing the minimum order quantity amount override fee warning.</summary>
        /// <value>A message describing the minimum order quantity amount override fee warning.</value>
        string? MinimumOrderQuantityAmountOverrideFeeWarningMessage { get; set; }

        /// <summary>Gets or sets a message describing the minimum order quantity amount override fee accepted.</summary>
        /// <value>A message describing the minimum order quantity amount override fee accepted.</value>
        string? MinimumOrderQuantityAmountOverrideFeeAcceptedMessage { get; set; }

        /// <summary>Gets or sets the identifier of the minimum order dollar amount buffer product.</summary>
        /// <value>The identifier of the minimum order dollar amount buffer product.</value>
        int? MinimumOrderDollarAmountBufferProductID { get; set; }

        /// <summary>Gets or sets the minimum order dollar amount buffer product.</summary>
        /// <value>The minimum order dollar amount buffer product.</value>
        Product? MinimumOrderDollarAmountBufferProduct { get; set; }

        /// <summary>Gets or sets the identifier of the minimum order quantity amount buffer product.</summary>
        /// <value>The identifier of the minimum order quantity amount buffer product.</value>
        int? MinimumOrderQuantityAmountBufferProductID { get; set; }

        /// <summary>Gets or sets the minimum order quantity amount buffer product.</summary>
        /// <value>The minimum order quantity amount buffer product.</value>
        Product? MinimumOrderQuantityAmountBufferProduct { get; set; }

        /// <summary>Gets or sets the identifier of the minimum order dollar amount buffer category.</summary>
        /// <value>The identifier of the minimum order dollar amount buffer category.</value>
        int? MinimumOrderDollarAmountBufferCategoryID { get; set; }

        /// <summary>Gets or sets the category the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer category.</value>
        Category? MinimumOrderDollarAmountBufferCategory { get; set; }

        /// <summary>Gets or sets the identifier of the minimum order quantity amount buffer category.</summary>
        /// <value>The identifier of the minimum order quantity amount buffer category.</value>
        int? MinimumOrderQuantityAmountBufferCategoryID { get; set; }

        /// <summary>Gets or sets the category the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer category.</value>
        Category? MinimumOrderQuantityAmountBufferCategory { get; set; }
    }
}
