// <copyright file="IHaveFreeShippingMinimumsBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveFreeShippingMinimumsBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have free shipping minimums base model.</summary>
    public interface IHaveFreeShippingMinimumsBaseModel : IBaseModel
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

        /// <summary>Gets or sets the identifier of the minimum for free shipping dollar amount buffer product.</summary>
        /// <value>The identifier of the minimum for free shipping dollar amount buffer product.</value>
        int? MinimumForFreeShippingDollarAmountBufferProductID { get; set; }

        /// <summary>Gets or sets the minimum for free shipping dollar amount buffer product.</summary>
        /// <value>The minimum for free shipping dollar amount buffer product.</value>
        IProductModel? MinimumForFreeShippingDollarAmountBufferProduct { get; set; }

        /// <summary>Gets or sets the minimum for free shipping dollar amount buffer product key.</summary>
        /// <value>The minimum for free shipping dollar amount buffer product key.</value>
        string? MinimumForFreeShippingDollarAmountBufferProductKey { get; set; }

        /// <summary>Gets or sets the name of the minimum for free shipping dollar amount buffer product.</summary>
        /// <value>The name of the minimum for free shipping dollar amount buffer product.</value>
        string? MinimumForFreeShippingDollarAmountBufferProductName { get; set; }

        /// <summary>Gets or sets the identifier of the minimum for free shipping quantity amount buffer product.</summary>
        /// <value>The identifier of the minimum for free shipping quantity amount buffer product.</value>
        int? MinimumForFreeShippingQuantityAmountBufferProductID { get; set; }

        /// <summary>Gets or sets the minimum for free shipping quantity amount buffer product.</summary>
        /// <value>The minimum for free shipping quantity amount buffer product.</value>
        IProductModel? MinimumForFreeShippingQuantityAmountBufferProduct { get; set; }

        /// <summary>Gets or sets the minimum for free shipping quantity amount buffer product key.</summary>
        /// <value>The minimum for free shipping quantity amount buffer product key.</value>
        string? MinimumForFreeShippingQuantityAmountBufferProductKey { get; set; }

        /// <summary>Gets or sets the name of the minimum for free shipping quantity amount buffer product.</summary>
        /// <value>The name of the minimum for free shipping quantity amount buffer product.</value>
        string? MinimumForFreeShippingQuantityAmountBufferProductName { get; set; }

        /// <summary>Gets or sets the identifier of the minimum for free shipping dollar amount buffer category.</summary>
        /// <value>The identifier of the minimum for free shipping dollar amount buffer category.</value>
        int? MinimumForFreeShippingDollarAmountBufferCategoryID { get; set; }

        /// <summary>Gets or sets the category the minimum for free shipping dollar amount buffer belongs to.</summary>
        /// <value>The minimum for free shipping dollar amount buffer category.</value>
        ICategoryModel? MinimumForFreeShippingDollarAmountBufferCategory { get; set; }

        /// <summary>Gets or sets the minimum for free shipping dollar amount buffer category key.</summary>
        /// <value>The minimum for free shipping dollar amount buffer category key.</value>
        string? MinimumForFreeShippingDollarAmountBufferCategoryKey { get; set; }

        /// <summary>Gets or sets the name of the minimum for free shipping dollar amount buffer category.</summary>
        /// <value>The name of the minimum for free shipping dollar amount buffer category.</value>
        string? MinimumForFreeShippingDollarAmountBufferCategoryName { get; set; }

        /// <summary>Gets or sets the identifier of the minimum for free shipping quantity amount buffer category.</summary>
        /// <value>The identifier of the minimum for free shipping quantity amount buffer category.</value>
        int? MinimumForFreeShippingQuantityAmountBufferCategoryID { get; set; }

        /// <summary>Gets or sets the category the minimum for free shipping quantity amount buffer belongs to.</summary>
        /// <value>The minimum for free shipping quantity amount buffer category.</value>
        ICategoryModel? MinimumForFreeShippingQuantityAmountBufferCategory { get; set; }

        /// <summary>Gets or sets the minimum for free shipping quantity amount buffer category key.</summary>
        /// <value>The minimum for free shipping quantity amount buffer category key.</value>
        string? MinimumForFreeShippingQuantityAmountBufferCategoryKey { get; set; }

        /// <summary>Gets or sets the name of the minimum for free shipping quantity amount buffer category.</summary>
        /// <value>The name of the minimum for free shipping quantity amount buffer category.</value>
        string? MinimumForFreeShippingQuantityAmountBufferCategoryName { get; set; }
    }
}
