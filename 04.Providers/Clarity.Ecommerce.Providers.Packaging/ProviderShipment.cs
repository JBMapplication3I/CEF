// <copyright file="ProviderShipment.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the provider shipment class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Providers;

    /// <summary>A shipment item.</summary>
    /// <seealso cref="IProviderShipment"/>
    public class ProviderShipment : IProviderShipment
    {
        /// <inheritdoc/>
        public string? ItemCode { get; set; }

        /// <inheritdoc/>
        public string? ItemName { get; set; }

        /// <inheritdoc/>
        public decimal Weight { get; set; }

        /// <inheritdoc/>
        public string? WeightUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal? Width { get; set; }

        /// <inheritdoc/>
        public string? WidthUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public string? HazardClass { get; set; }

        /// <inheritdoc/>
        public decimal? Height { get; set; }

        /// <inheritdoc/>
        public string? HeightUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal? Depth { get; set; }

        /// <inheritdoc/>
        public string? DepthUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal DimensionalWeight { get; set; }

        /// <inheritdoc/>
        public string? DimensionalWeightUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal? PackageQuantity { get; set; }

        /// <inheritdoc/>
        public bool ProductIsFreeShipping { get; set; }
    }
}
