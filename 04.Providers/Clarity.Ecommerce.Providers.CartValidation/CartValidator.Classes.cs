// <copyright file="CartValidator.Classes.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator class</summary>
namespace Clarity.Ecommerce.Providers.CartValidation
{
    /// <content>A cart validator.</content>
    public partial class CartValidator
    {
        /// <summary>A category summary.</summary>
        /// <seealso cref="Clarity.Ecommerce.Providers.CartValidation.CartValidator.Summary"/>
        protected class CategorySummary : Summary
        {
            /// <summary>Gets or sets the name of the display.</summary>
            /// <value>The name of the display.</value>
            public string? DisplayName { get; set; }
        }

        /// <summary>A summary.</summary>
        protected class Summary
        {
            /// <summary>Gets or sets the identifier.</summary>
            /// <value>The identifier.</value>
            public int ID { get; set; }

            /// <summary>Gets or sets the name.</summary>
            /// <value>The name.</value>
            public string? Name { get; set; }

            /// <summary>Gets or sets the JSON attributes.</summary>
            /// <value>The JSON attributes.</value>
            public string? JsonAttributes { get; set; }

            /// <summary>Gets or sets the dollar amount.</summary>
            /// <value>The dollar amount.</value>
            public decimal? DollarAmount { get; set; }

            /// <summary>Gets or sets the dollar amount after.</summary>
            /// <value>The dollar amount after.</value>
            public decimal? DollarAmountAfter { get; set; }

            /// <summary>Gets or sets a message describing the dollar amount warning.</summary>
            /// <value>A message describing the dollar amount warning.</value>
            public string? DollarAmountWarningMessage { get; set; }

            /// <summary>Gets or sets the dollar amount override fee.</summary>
            /// <value>The dollar amount override fee.</value>
            public decimal? DollarAmountOverrideFee { get; set; }

            /// <summary>Gets or sets a value indicating whether the dollar amount override fee is percent.</summary>
            /// <value>True if dollar amount override fee is percent, false if not.</value>
            public bool DollarAmountOverrideFeeIsPercent { get; set; }

            /// <summary>Gets or sets a message describing the dollar amount override fee warning.</summary>
            /// <value>A message describing the dollar amount override fee warning.</value>
            public string? DollarAmountOverrideFeeWarningMessage { get; set; }

            /// <summary>Gets or sets a message describing the dollar amount override fee accepted.</summary>
            /// <value>A message describing the dollar amount override fee accepted.</value>
            public string? DollarAmountOverrideFeeAcceptedMessage { get; set; }

            /// <summary>Gets or sets the name of the dollar amount buffer product.</summary>
            /// <value>The name of the dollar amount buffer product.</value>
            public string? DollarAmountBufferProductName { get; set; }

            /// <summary>Gets or sets URL of the dollar amount buffer product seo.</summary>
            /// <value>The dollar amount buffer product seo URL.</value>
            public string? DollarAmountBufferProductSeoUrl { get; set; }

            /// <summary>Gets or sets the name of the dollar amount buffer category.</summary>
            /// <value>The name of the dollar amount buffer category.</value>
            public string? DollarAmountBufferCategoryName { get; set; }

            /// <summary>Gets or sets the name of the dollar amount buffer category display.</summary>
            /// <value>The name of the dollar amount buffer category display.</value>
            public string? DollarAmountBufferCategoryDisplayName { get; set; }

            /// <summary>Gets or sets URL of the dollar amount buffer category seo.</summary>
            /// <value>The dollar amount buffer category seo URL.</value>
            public string? DollarAmountBufferCategorySeoUrl { get; set; }

            /// <summary>Gets or sets the quantity amount.</summary>
            /// <value>The quantity amount.</value>
            public decimal? QuantityAmount { get; set; }

            /// <summary>Gets or sets the quantity amount after.</summary>
            /// <value>The quantity amount after.</value>
            public decimal? QuantityAmountAfter { get; set; }

            /// <summary>Gets or sets a message describing the quantity amount warning.</summary>
            /// <value>A message describing the quantity amount warning.</value>
            public string? QuantityAmountWarningMessage { get; set; }

            /// <summary>Gets or sets the quantity amount override fee.</summary>
            /// <value>The quantity amount override fee.</value>
            public decimal? QuantityAmountOverrideFee { get; set; }

            /// <summary>Gets or sets a value indicating whether the quantity amount override fee is percent.</summary>
            /// <value>True if quantity amount override fee is percent, false if not.</value>
            public bool QuantityAmountOverrideFeeIsPercent { get; set; }

            /// <summary>Gets or sets a message describing the quantity amount override fee warning.</summary>
            /// <value>A message describing the quantity amount override fee warning.</value>
            public string? QuantityAmountOverrideFeeWarningMessage { get; set; }

            /// <summary>Gets or sets a message describing the quantity amount override fee accepted.</summary>
            /// <value>A message describing the quantity amount override fee accepted.</value>
            public string? QuantityAmountOverrideFeeAcceptedMessage { get; set; }

            /// <summary>Gets or sets the name of the quantity amount buffer product.</summary>
            /// <value>The name of the quantity amount buffer product.</value>
            public string? QuantityAmountBufferProductName { get; set; }

            /// <summary>Gets or sets URL of the quantity amount buffer product seo.</summary>
            /// <value>The quantity amount buffer product seo URL.</value>
            public string? QuantityAmountBufferProductSeoUrl { get; set; }

            /// <summary>Gets or sets the name of the quantity amount buffer category.</summary>
            /// <value>The name of the quantity amount buffer category.</value>
            public string? QuantityAmountBufferCategoryName { get; set; }

            /// <summary>Gets or sets the name of the quantity amount buffer category display.</summary>
            /// <value>The name of the quantity amount buffer category display.</value>
            public string? QuantityAmountBufferCategoryDisplayName { get; set; }

            /// <summary>Gets or sets URL of the quantity amount buffer category seo.</summary>
            /// <value>The quantity amount buffer category seo URL.</value>
            public string? QuantityAmountBufferCategorySeoUrl { get; set; }

            /// <summary>Gets or sets the free shipping dollar amount.</summary>
            /// <value>The free shipping dollar amount.</value>
            public decimal? FreeShippingDollarAmount { get; set; }

            /// <summary>Gets or sets the free shipping dollar amount after.</summary>
            /// <value>The free shipping dollar amount after.</value>
            public decimal? FreeShippingDollarAmountAfter { get; set; }

            /// <summary>Gets or sets a message describing the free shipping dollar amount warning.</summary>
            /// <value>A message describing the free shipping dollar amount warning.</value>
            public string? FreeShippingDollarAmountWarningMessage { get; set; }

            /// <summary>Gets or sets a message describing the free shipping dollar amount ignored accepted.</summary>
            /// <value>A message describing the free shipping dollar amount ignored accepted.</value>
            public string? FreeShippingDollarAmountIgnoredAcceptedMessage { get; set; }

            /// <summary>Gets or sets the name of the free shipping dollar amount buffer product.</summary>
            /// <value>The name of the free shipping dollar amount buffer product.</value>
            public string? FreeShippingDollarAmountBufferProductName { get; set; }

            /// <summary>Gets or sets URL of the free shipping dollar amount buffer product seo.</summary>
            /// <value>The free shipping dollar amount buffer product seo URL.</value>
            public string? FreeShippingDollarAmountBufferProductSeoUrl { get; set; }

            /// <summary>Gets or sets the name of the free shipping dollar amount buffer category.</summary>
            /// <value>The name of the free shipping dollar amount buffer category.</value>
            public string? FreeShippingDollarAmountBufferCategoryName { get; set; }

            /// <summary>Gets or sets the name of the free shipping dollar amount buffer category display.</summary>
            /// <value>The name of the free shipping dollar amount buffer category display.</value>
            public string? FreeShippingDollarAmountBufferCategoryDisplayName { get; set; }

            /// <summary>Gets or sets URL of the free shipping dollar amount buffer category seo.</summary>
            /// <value>The free shipping dollar amount buffer category seo URL.</value>
            public string? FreeShippingDollarAmountBufferCategorySeoUrl { get; set; }

            /// <summary>Gets or sets the free shipping quantity amount.</summary>
            /// <value>The free shipping quantity amount.</value>
            public decimal? FreeShippingQuantityAmount { get; set; }

            /// <summary>Gets or sets the free shipping quantity amount after.</summary>
            /// <value>The free shipping quantity amount after.</value>
            public decimal? FreeShippingQuantityAmountAfter { get; set; }

            /// <summary>Gets or sets a message describing the free shipping quantity amount warning.</summary>
            /// <value>A message describing the free shipping quantity amount warning.</value>
            public string? FreeShippingQuantityAmountWarningMessage { get; set; }

            /// <summary>Gets or sets a message describing the free shipping quantity amount ignored accepted.</summary>
            /// <value>A message describing the free shipping quantity amount ignored accepted.</value>
            public string? FreeShippingQuantityAmountIgnoredAcceptedMessage { get; set; }

            /// <summary>Gets or sets the name of the free shipping quantity amount buffer product.</summary>
            /// <value>The name of the free shipping quantity amount buffer product.</value>
            public string? FreeShippingQuantityAmountBufferProductName { get; set; }

            /// <summary>Gets or sets URL of the free shipping quantity amount buffer product seo.</summary>
            /// <value>The free shipping quantity amount buffer product seo URL.</value>
            public string? FreeShippingQuantityAmountBufferProductSeoUrl { get; set; }

            /// <summary>Gets or sets the name of the free shipping quantity amount buffer category.</summary>
            /// <value>The name of the free shipping quantity amount buffer category.</value>
            public string? FreeShippingQuantityAmountBufferCategoryName { get; set; }

            /// <summary>Gets or sets the name of the free shipping quantity amount buffer category display.</summary>
            /// <value>The name of the free shipping quantity amount buffer category display.</value>
            public string? FreeShippingQuantityAmountBufferCategoryDisplayName { get; set; }

            /// <summary>Gets or sets URL of the free shipping quantity amount buffer category seo.</summary>
            /// <value>The free shipping quantity amount buffer category seo URL.</value>
            public string? FreeShippingQuantityAmountBufferCategorySeoUrl { get; set; }
        }
    }
}
