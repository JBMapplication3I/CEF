// <copyright file="HistoricalTaxRateModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the historical tax rate model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;

    /// <summary>A data Model for the historical tax rate.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="Interfaces.Models.IHistoricalTaxRateModel"/>
    public partial class HistoricalTaxRateModel
    {
        /// <inheritdoc/>
        public string? Provider { get; set; }

        /// <inheritdoc/>
        public long? CartHash { get; set; }

        /// <inheritdoc/>
        public DateTime OnDate { get; set; }

        /// <inheritdoc/>
        public decimal? CountryLevelRate { get; set; }

        /// <inheritdoc/>
        public decimal? RegionLevelRate { get; set; }

        /// <inheritdoc/>
        public decimal? DistrictLevelRate { get; set; }

        /// <inheritdoc/>
        public decimal? TotalAmount { get; set; }

        /// <inheritdoc/>
        public decimal? TotalTaxable { get; set; }

        /// <inheritdoc/>
        public decimal? TotalTax { get; set; }

        /// <inheritdoc/>
        public decimal? TotalTaxCalculated { get; set; }

        /// <inheritdoc/>
        public decimal Rate { get; set; }

        /// <inheritdoc/>
        public string? SerializedRequest { get; set; }

        /// <inheritdoc/>
        public string? SerializedResponse { get; set; }
    }
}
