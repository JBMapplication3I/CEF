// <copyright file="IHistoricalTaxRateModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHistoricalTaxRateModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for historical tax rate model.</summary>
    public partial interface IHistoricalTaxRateModel
    {
        /// <summary>Gets or sets the provider.</summary>
        /// <value>The provider.</value>
        string? Provider { get; set; }

        /// <summary>Gets or sets the cart hash.</summary>
        /// <value>The cart hash.</value>
        long? CartHash { get; set; }

        /// <summary>Gets or sets the on date.</summary>
        /// <value>The on date.</value>
        DateTime OnDate { get; set; }

        /// <summary>Gets or sets the country level rate.</summary>
        /// <value>The country level rate.</value>
        decimal? CountryLevelRate { get; set; }

        /// <summary>Gets or sets the region level rate.</summary>
        /// <value>The region level rate.</value>
        decimal? RegionLevelRate { get; set; }

        /// <summary>Gets or sets the district level rate.</summary>
        /// <value>The district level rate.</value>
        decimal? DistrictLevelRate { get; set; }

        /// <summary>Gets or sets the total number of amount.</summary>
        /// <value>The total number of amount.</value>
        decimal? TotalAmount { get; set; }

        /// <summary>Gets or sets the total number of taxable.</summary>
        /// <value>The total number of taxable.</value>
        decimal? TotalTaxable { get; set; }

        /// <summary>Gets or sets the total number of tax.</summary>
        /// <value>The total number of tax.</value>
        decimal? TotalTax { get; set; }

        /// <summary>Gets or sets the total number of tax calculated.</summary>
        /// <value>The total number of tax calculated.</value>
        decimal? TotalTaxCalculated { get; set; }

        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal Rate { get; set; }

        /// <summary>Gets or sets the serialized request.</summary>
        /// <value>The serialized request.</value>
        string? SerializedRequest { get; set; }

        /// <summary>Gets or sets the serialized response.</summary>
        /// <value>The serialized response.</value>
        string? SerializedResponse { get; set; }
    }
}
