// <copyright file="IHaveDimensions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveDimensions interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for have dimensions.</summary>
    public interface IHaveDimensions
    {
        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        decimal Width { get; set; }

        /// <summary>Gets or sets the width unit of measure.</summary>
        /// <value>The width unit of measure.</value>
        string? WidthUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the depth.</summary>
        /// <value>The depth.</value>
        decimal Depth { get; set; }

        /// <summary>Gets or sets the depth unit of measure.</summary>
        /// <value>The depth unit of measure.</value>
        string? DepthUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        decimal Height { get; set; }

        /// <summary>Gets or sets the height unit of measure.</summary>
        /// <value>The height unit of measure.</value>
        string? HeightUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the weight.</summary>
        /// <value>The weight.</value>
        decimal Weight { get; set; }

        /// <summary>Gets or sets the weight unit of measure.</summary>
        /// <value>The weight unit of measure.</value>
        string? WeightUnitOfMeasure { get; set; }
    }

    /// <summary>Interface for have nullable dimensions.</summary>
    public interface IHaveNullableDimensions
    {
        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        decimal? Width { get; set; }

        /// <summary>Gets or sets the width unit of measure.</summary>
        /// <value>The width unit of measure.</value>
        string? WidthUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the depth.</summary>
        /// <value>The depth.</value>
        decimal? Depth { get; set; }

        /// <summary>Gets or sets the depth unit of measure.</summary>
        /// <value>The depth unit of measure.</value>
        string? DepthUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        decimal? Height { get; set; }

        /// <summary>Gets or sets the height unit of measure.</summary>
        /// <value>The height unit of measure.</value>
        string? HeightUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the weight.</summary>
        /// <value>The weight.</value>
        decimal? Weight { get; set; }

        /// <summary>Gets or sets the weight unit of measure.</summary>
        /// <value>The weight unit of measure.</value>
        string? WeightUnitOfMeasure { get; set; }
    }
}
