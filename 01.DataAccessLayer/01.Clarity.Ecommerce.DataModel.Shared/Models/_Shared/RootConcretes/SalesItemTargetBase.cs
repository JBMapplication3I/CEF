// <copyright file="SalesItemTargetBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item target base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Globalization;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    /// <summary>A sales collection base.</summary>
    /// <typeparam name="TMaster">Type of the master entity.</typeparam>
    /// <seealso cref="NameableBase"/>
    public abstract class SalesItemTargetBase<TMaster>
        : Base,
            ISalesItemTargetBase
    {
        #region IHaveATypeBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), ForceMapOutWithLite]
        public virtual SalesItemTargetType? Type { get; set; }
        #endregion

        #region SalesItemTargetBase Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal Quantity { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool NothingToShip { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(DestinationContact)), DefaultValue(0)]
        public int DestinationContactID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual Contact? DestinationContact { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(OriginProductInventoryLocationSection)), DefaultValue(null)]
        public int? OriginProductInventoryLocationSectionID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapInEver, DontMapOutEver]
        public virtual ProductInventoryLocationSection? OriginProductInventoryLocationSection { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(OriginStoreProduct)), DefaultValue(null)]
        public int? OriginStoreProductID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapInEver, DontMapOutEver]
        public virtual StoreProduct? OriginStoreProduct { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(BrandProduct)), DefaultValue(null)]
        public int? BrandProductID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual BrandProduct? BrandProduct { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(OriginVendorProduct)), DefaultValue(null)]
        public int? OriginVendorProductID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapInEver, DontMapOutEver]
        public virtual VendorProduct? OriginVendorProduct { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SelectedRateQuote)), DefaultValue(null)]
        public int? SelectedRateQuoteID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual RateQuote? SelectedRateQuote { get; set; }

        /// <summary>Gets or sets the master.</summary>
        /// <value>The master.</value>
        /// <remarks>This property is only for generation of the appropriate EF code, do not use it anywhere.</remarks>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual TMaster? Master { get; set; }
        #endregion

        #region ICloneable
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // IHaveAType
            builder.Append("T: ").AppendLine(Type?.ToString() ?? $"No Type={TypeID}");
            // SalesItemTargetBase
            builder.Append("Q: ").AppendLine(Quantity.ToString("n5", CultureInfo.InvariantCulture));
            builder.Append("N: ").AppendLine(NothingToShip.ToString());
            // Related Objects
            builder.Append("S: ").AppendLine(OriginStoreProduct?.ToHashableString() ?? $"No OriginStoreProduct={OriginStoreProductID}");
            builder.Append("V: ").AppendLine(OriginVendorProduct?.ToHashableString() ?? $"No OriginVendorProduct={OriginVendorProductID}");
            builder.Append("I: ").AppendLine(OriginProductInventoryLocationSection?.ToHashableString() ?? $"No OriginProductInventoryLocationSection={OriginProductInventoryLocationSectionID}");
            builder.Append("D: ").AppendLine(DestinationContact?.ToHashableString() ?? $"No DestinationContact={DestinationContactID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
