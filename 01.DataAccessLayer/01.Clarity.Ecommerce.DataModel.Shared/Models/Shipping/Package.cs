// <copyright file="Package.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the package class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IPackage
        : INameableBase, IHaveATypeBase<PackageType>, IHaveDimensions, IHaveMatchCodes
    {
        /// <summary>Gets or sets the dimensional weight.</summary>
        /// <value>The dimensional weight.</value>
        decimal DimensionalWeight { get; set; }

        /// <summary>Gets or sets the dimensional weight unit of measure.</summary>
        /// <value>The dimensional weight unit of measure.</value>
        string? DimensionalWeightUnitOfMeasure { get; set; }

        /// <summary>Gets or sets a value indicating whether this IPackage is custom.</summary>
        /// <value>True if this IPackage is custom, false if not.</value>
        bool IsCustom { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Shipping", "Package")]
    public class Package : NameableBase, IPackage
    {
        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual PackageType? Type { get; set; }
        #endregion

        #region HaveDimensions Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal Width { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false), DefaultValue("in")]
        public string? WidthUnitOfMeasure { get; set; } = "in";

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal Depth { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false), DefaultValue("in")]
        public string? DepthUnitOfMeasure { get; set; } = "in";

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal Height { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false), DefaultValue("in")]
        public string? HeightUnitOfMeasure { get; set; } = "in";

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal Weight { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false), DefaultValue("lbs")]
        public string? WeightUnitOfMeasure { get; set; } = "lbs";
        #endregion

        #region Package Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal DimensionalWeight { get; set; }

        /// <inheritdoc/>
        [StringLength(64), StringIsUnicode(false), DefaultValue("lbs")]
        public string? DimensionalWeightUnitOfMeasure { get; set; } = "lbs";

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsCustom { get; set; }
        #endregion

        #region IHaveMatchCodes Functions
        /// <inheritdoc/>
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
                if (includeID)
                {
                    matchCode = (matchCode * 397) ^ ID.GetMatchCode();
                }
                return matchCode;
            }
        }
        #endregion
    }
}
