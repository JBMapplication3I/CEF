// <copyright file="ProductIndexableModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product indexable model class</summary>
// TODO: Versions as Variants of Master so the Master always shows up but the contents of variants can bring up the master
// TODO: Versions as Kit Components for relevance?
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System;
    using System.Collections.Generic;

    /// <summary>A data Model for the product indexable.</summary>
    /// <seealso cref="IndexableModelBase"/>
    public class ProductIndexableModel : IndexableModelBase
    {
        #region Suggestions
        /// <summary>Gets or sets the name of the suggested by brand.</summary>
        /// <value>The name of the suggested by brand.</value>
        public object? SuggestedByBrandName { get; set; }

        /// <summary>Gets or sets the suggested by manufacturer part number.</summary>
        /// <value>The suggested by manufacturer part number.</value>
        public object? SuggestedByManufacturerPartNumber { get; set; }

        /// <summary>Gets or sets the suggested by short description.</summary>
        /// <value>The suggested by short description.</value>
        public object? SuggestedByShortDescription { get; set; }
        #endregion

        #region Product Properties
        /// <summary>Gets or sets the manufacturer part number.</summary>
        /// <value>The manufacturer part number.</value>
        public string? ManufacturerPartNumber { get; set; }

        /// <summary>Gets or sets information describing the short.</summary>
        /// <value>Information describing the short.</value>
        public string? ShortDescription { get; set; }

        /// <summary>Gets or sets the name of the brand.</summary>
        /// <value>The name of the brand.</value>
        public string? BrandName { get; set; }

        /// <summary>Gets or sets the brand name aggregate.</summary>
        /// <value>The brand name aggregate.</value>
        public string? BrandNameAgg { get; set; }

        /// <summary>Gets or sets a value indicating whether this IndexableProductModel is visible.</summary>
        /// <value>True if this IndexableProductModel is visible, false if not.</value>
        public bool IsVisible { get; set; }

        /// <summary>Gets or sets the is quotable.</summary>
        /// <value>The is quotable.</value>
        public bool? IsQuotable { get; set; }

        /// <summary>Gets or sets the is taxable.</summary>
        /// <value>The is taxable.</value>
        public bool? IsTaxable { get; set; }

        /// <summary>Gets or sets the is sale.</summary>
        /// <value>The is sale.</value>
        public bool? IsSale { get; set; }

        /// <summary>Gets or sets the is free shipping.</summary>
        /// <value>The is free shipping.</value>
        public bool? IsFreeShipping { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        public string? UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the available start date.</summary>
        /// <value>The available start date.</value>
        public DateTime? AvailableStartDate { get; set; }

        /// <summary>Gets or sets the available end date.</summary>
        /// <value>The available end date.</value>
        public DateTime? AvailableEndDate { get; set; }

        /// <summary>Gets or sets the weight.</summary>
        /// <value>The weight.</value>
        public decimal? Weight { get; set; }

        /// <summary>Gets or sets the weight unit of measure.</summary>
        /// <value>The weight unit of measure.</value>
        public string? WeightUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        public decimal? Width { get; set; }

        /// <summary>Gets or sets the width unit of measure.</summary>
        /// <value>The width unit of measure.</value>
        public string? WidthUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the depth.</summary>
        /// <value>The depth.</value>
        public decimal? Depth { get; set; }

        /// <summary>Gets or sets the depth unit of measure.</summary>
        /// <value>The depth unit of measure.</value>
        public string? DepthUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        public decimal? Height { get; set; }

        /// <summary>Gets or sets the height unit of measure.</summary>
        /// <value>The height unit of measure.</value>
        public string? HeightUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the total number of purchased amount.</summary>
        /// <value>The total number of purchased amount.</value>
        public decimal? TotalPurchasedAmount { get; set; }

        /// <summary>Gets or sets the total number of purchased amount currency identifier.</summary>
        /// <value>The total number of purchased amount currency identifier.</value>
        public int? TotalPurchasedAmountCurrencyID { get; set; }

        /// <summary>Gets or sets the total number of purchased quantity.</summary>
        /// <value>The total number of purchased quantity.</value>
        public decimal? TotalPurchasedQuantity { get; set; }

        /// <summary>Gets or sets the attributes of the variants.</summary>
        /// <value>The attributes of the variants.</value>
        public IEnumerable<IndexableVariantAttributesFilter>? ProductVariantAttributes { get; set; }
        #endregion
    }
}
