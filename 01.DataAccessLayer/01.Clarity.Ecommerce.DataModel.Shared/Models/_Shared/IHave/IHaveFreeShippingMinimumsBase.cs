// <copyright file="IHaveFreeShippingMinimumsBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveFreeShippingMinimumsBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for have free shipping minimums base.</summary>
    public interface IHaveFreeShippingMinimumsBase : IBase
    {
        /// <summary>Gets or sets the minimum for free shipping dollar amount.</summary>
        /// <value>The minimum for free shipping dollar amount.</value>
        decimal? MinimumForFreeShippingDollarAmount { get; set; }

        /// <summary>Gets or sets the minimum for free shipping dollar amount after.</summary>
        /// <value>The minimum for free shipping dollar amount after.</value>
        decimal? MinimumForFreeShippingDollarAmountAfter { get; set; }

        /// <summary>Gets or sets a message describing the minimum for free shipping dollar amount warning.</summary>
        /// <value>A message describing the minimum for free shipping dollar amount warning.</value>
        string? MinimumForFreeShippingDollarAmountWarningMessage { get; set; }

        /// <summary>Gets or sets a message describing the minimum for free shipping dollar amount ignored accepted.</summary>
        /// <value>A message describing the minimum for free shipping dollar amount ignored accepted.</value>
        string? MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage { get; set; }

        /// <summary>Gets or sets the minimum for free shipping quantity amount.</summary>
        /// <value>The minimum for free shipping quantity amount.</value>
        decimal? MinimumForFreeShippingQuantityAmount { get; set; }

        /// <summary>Gets or sets the minimum for free shipping quantity amount after.</summary>
        /// <value>The minimum for free shipping quantity amount after.</value>
        decimal? MinimumForFreeShippingQuantityAmountAfter { get; set; }

        /// <summary>Gets or sets a message describing the minimum for free shipping quantity amount warning.</summary>
        /// <value>A message describing the minimum for free shipping quantity amount warning.</value>
        string? MinimumForFreeShippingQuantityAmountWarningMessage { get; set; }

        /// <summary>Gets or sets a message describing the minimum for free shipping quantity amount ignored accepted.</summary>
        /// <value>A message describing the minimum for free shipping quantity amount ignored accepted.</value>
        string? MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage { get; set; }

        /// <summary>Gets or sets the identifier of the minimum order dollar amount buffer product.</summary>
        /// <value>The identifier of the minimum order dollar amount buffer product.</value>
        int? MinimumForFreeShippingDollarAmountBufferProductID { get; set; }

        /// <summary>Gets or sets the minimum order dollar amount buffer product.</summary>
        /// <value>The minimum order dollar amount buffer product.</value>
        Product? MinimumForFreeShippingDollarAmountBufferProduct { get; set; }

        /// <summary>Gets or sets the identifier of the minimum order quantity amount buffer product.</summary>
        /// <value>The identifier of the minimum order quantity amount buffer product.</value>
        int? MinimumForFreeShippingQuantityAmountBufferProductID { get; set; }

        /// <summary>Gets or sets the minimum order quantity amount buffer product.</summary>
        /// <value>The minimum order quantity amount buffer product.</value>
        Product? MinimumForFreeShippingQuantityAmountBufferProduct { get; set; }

        /// <summary>Gets or sets the identifier of the minimum order dollar amount buffer category.</summary>
        /// <value>The identifier of the minimum order dollar amount buffer category.</value>
        int? MinimumForFreeShippingDollarAmountBufferCategoryID { get; set; }

        /// <summary>Gets or sets the category the minimum order dollar amount buffer belongs to.</summary>
        /// <value>The minimum order dollar amount buffer category.</value>
        Category? MinimumForFreeShippingDollarAmountBufferCategory { get; set; }

        /// <summary>Gets or sets the identifier of the minimum order quantity amount buffer category.</summary>
        /// <value>The identifier of the minimum order quantity amount buffer category.</value>
        int? MinimumForFreeShippingQuantityAmountBufferCategoryID { get; set; }

        /// <summary>Gets or sets the category the minimum order quantity amount buffer belongs to.</summary>
        /// <value>The minimum order quantity amount buffer category.</value>
        Category? MinimumForFreeShippingQuantityAmountBufferCategory { get; set; }
    }
}
