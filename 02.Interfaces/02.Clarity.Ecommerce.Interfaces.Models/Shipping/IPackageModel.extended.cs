// <copyright file="IPackageModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPackageModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for package model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public partial interface IPackageModel
    {
        #region Package Properties
        /// <summary>Gets or sets the weight.</summary>
        /// <value>The weight.</value>
        decimal Weight { get; set; }

        /// <summary>Gets or sets the weight unit of measure.</summary>
        /// <value>The weight unit of measure.</value>
        string? WeightUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        decimal Width { get; set; }

        /// <summary>Gets or sets the width unit of measure.</summary>
        /// <value>The width unit of measure.</value>
        string? WidthUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        decimal Height { get; set; }

        /// <summary>Gets or sets the height unit of measure.</summary>
        /// <value>The height unit of measure.</value>
        string? HeightUnitOfMeasure { get; set; }

        /// <summary>Gets or sets the depth.</summary>
        /// <value>The depth.</value>
        decimal Depth { get; set; }

        /// <summary>Gets or sets the depth unit of measure.</summary>
        /// <value>The depth unit of measure.</value>
        string? DepthUnitOfMeasure { get; set; }

        /// <summary>Gets or sets a value indicating whether this IPackageModel is custom.</summary>
        /// <value>True if this IPackageModel is custom, false if not.</value>
        bool IsCustom { get; set; }

        /// <summary>Gets or sets the dimensional weight.</summary>
        /// <value>The dimensional weight.</value>
        decimal DimensionalWeight { get; set; }

        /// <summary>Gets or sets the dimensional weight unit of measure.</summary>
        /// <value>The dimensional weight unit of measure.</value>
        string? DimensionalWeightUnitOfMeasure { get; set; }
        #endregion

        #region Functions
        /// <summary>Gets the validate.</summary>
        /// <returns>A Tuple{bool,string}.</returns>
        Tuple<bool, string> Validate();

        /// <summary>Gets match code.</summary>
        /// <param name="includeID">True to include, false to exclude the identifier.</param>
        /// <returns>The match code.</returns>
        ulong GetMatchCode(bool includeID = false);
        #endregion
    }
}
