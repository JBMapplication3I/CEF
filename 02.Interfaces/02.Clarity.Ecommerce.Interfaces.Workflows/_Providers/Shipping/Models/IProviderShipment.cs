// <copyright file="IProviderShipment.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProviderShipment interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers
{
    /// <summary>Interface for provider shipments.</summary>
    public interface IProviderShipment
    {
        /// <summary>Gets or sets the item code.</summary>
        /// <value>The item code.</value>
        string? ItemCode { get; set; }

        /// <summary>Gets or sets the name of the item.</summary>
        /// <value>The name of the item.</value>
        string? ItemName { get; set; }

        /// <summary>Gets or sets the weight.</summary>
        /// <value>The weight.</value>
        decimal Weight { get; set; }

        /// <summary>Gets or sets the weight unit of measure.</summary>
        /// <value>The weight unit of measure.</value>
        string? WeightUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        decimal? Width { get; set; }

        /// <summary>Gets or sets the width unit of measure.</summary>
        /// <value>The width unit of measure.</value>
        string? WidthUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the hazard class.</summary>
        /// <value>The hazard class.</value>
        string? HazardClass { get; set; }

        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        decimal? Height { get; set; }

        /// <summary>Gets or sets the height unit of measure.</summary>
        /// <value>The height unit of measure.</value>
        string? HeightUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the depth.</summary>
        /// <value>The depth.</value>
        decimal? Depth { get; set; }

        /// <summary>Gets or sets the depth unit of measure.</summary>
        /// <value>The depth unit of measure.</value>
        string? DepthUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the dimensional weight.</summary>
        /// <value>The dimensional weight.</value>
        decimal DimensionalWeight { get; set; }

        /// <summary>Gets or sets the dimensional weight unit of measure.</summary>
        /// <value>The dimensional weight unit of measure.</value>
        string? DimensionalWeightUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the package quantity.</summary>
        /// <value>The package quantity.</value>
        decimal? PackageQuantity { get; set; }

        /// <summary>Gets or sets a value indicating whether the product is free shipping.</summary>
        /// <value>True if product is free shipping, false if not.</value>
        bool ProductIsFreeShipping { get; set; }
    }
}
