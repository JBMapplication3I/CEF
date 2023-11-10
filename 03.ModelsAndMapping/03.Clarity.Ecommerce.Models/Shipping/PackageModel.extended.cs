// <copyright file="PackageModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the package model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Text;

    /// <summary>A data Model for the package.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="Interfaces.Models.IPackageModel"/>
    public partial class PackageModel
    {
        /// <inheritdoc/>
        public decimal Weight { get; set; }

        /// <inheritdoc/>
        public string? WeightUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal Width { get; set; }

        /// <inheritdoc/>
        public string? WidthUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal Height { get; set; }

        /// <inheritdoc/>
        public string? HeightUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal Depth { get; set; }

        /// <inheritdoc/>
        public string? DepthUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal DimensionalWeight { get; set; }

        /// <inheritdoc/>
        public string? DimensionalWeightUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public bool IsCustom { get; set; }

        /// <summary>Gets the validate.</summary>
        /// <returns>A Tuple{bool,string}.</returns>
        public Tuple<bool, string> Validate()
        {
            var message = new StringBuilder();
            if (Width <= 0)
            {
                message.AppendLine("WARNING: Width is not valid");
            }
            if (string.IsNullOrWhiteSpace(WidthUnitOfMeasure))
            {
                message.AppendLine("WARNING: WidthUnitOfMeasure is not valid");
            }
            if (Depth <= 0)
            {
                message.AppendLine("WARNING: Depth is not valid");
            }
            if (string.IsNullOrWhiteSpace(DepthUnitOfMeasure))
            {
                message.AppendLine("WARNING: DepthUnitOfMeasure is not valid");
            }
            if (Height <= 0)
            {
                message.AppendLine("WARNING: Height is not valid");
            }
            if (string.IsNullOrWhiteSpace(HeightUnitOfMeasure))
            {
                message.AppendLine("WARNING: HeightUnitOfMeasure is not valid");
            }
            if (Weight <= 0)
            {
                message.AppendLine("WARNING: Weight is not valid");
            }
            if (string.IsNullOrWhiteSpace(WeightUnitOfMeasure))
            {
                message.AppendLine("WARNING: WeightUnitOfMeasure is not valid");
            }
            if (DimensionalWeight <= 0)
            {
                message.AppendLine("WARNING: DimensionalWeight is not valid");
            }
            if (string.IsNullOrWhiteSpace(DimensionalWeightUnitOfMeasure))
            {
                message.AppendLine("WARNING: DimensionalWeightUnitOfMeasure is not valid");
            }
            var valid = !(Width <= 0 && Depth <= 0 && Height <= 0 && Weight <= 0 && DimensionalWeight <= 0);
            if (!valid)
            {
                message.AppendLine(
                    "ERROR: All numerical values were invalid. Requires at least one of Width, Depth, Height, Weight or DimensionalWeight");
            }
            return new(valid, message.ToString().Trim());
        }

        /// <summary>Gets match code.</summary>
        /// <remarks>Match Code is similar to Hash Code except we control the properties to check for matching a Model with
        /// it's Entity.</remarks>
        /// <param name="includeID">(Optional) True to include, false to exclude the identifier.</param>
        /// <returns>The match code.</returns>
        public ulong GetMatchCode(bool includeID = false)
        {
            unchecked
            {
                var matchCode = Weight.GetMatchCode();
                matchCode = (matchCode * 397) ^ WeightUnitOfMeasure!.GetMatchCode();
                matchCode = (matchCode * 397) ^ Width.GetMatchCode();
                matchCode = (matchCode * 397) ^ WidthUnitOfMeasure!.GetMatchCode();
                matchCode = (matchCode * 397) ^ Height.GetMatchCode();
                matchCode = (matchCode * 397) ^ HeightUnitOfMeasure!.GetMatchCode();
                matchCode = (matchCode * 397) ^ Depth.GetMatchCode();
                matchCode = (matchCode * 397) ^ DepthUnitOfMeasure!.GetMatchCode();
                matchCode = (matchCode * 397) ^ DimensionalWeight.GetMatchCode();
                matchCode = (matchCode * 397) ^ DimensionalWeightUnitOfMeasure!.GetMatchCode();
                matchCode = (matchCode * 397) ^ IsCustom.GetMatchCode();
                matchCode = (matchCode * 397) ^ TypeID.GetMatchCode();
                // hashCode = (hashCode * 397) ^ (Type?.GetHashCode() ?? 0);
                // hashCode = (hashCode * 397) ^ TypeKey.GetMatchCode();
                // hashCode = (hashCode * 397) ^ TypeName.GetMatchCode();
                if (includeID)
                {
                    matchCode = (matchCode * 397) ^ ID.GetMatchCode();
                }
                return matchCode;
            }
        }
    }
}
