// <copyright file="VendorProduct.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor product class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IVendorProduct
        : IAmAVendorRelationshipTableWhereVendorIsTheMaster<Product>,
            IAmAProductRelationshipTableWhereProductIsTheSlave<Vendor>
    {
        #region VendorProduct Properties
        /// <summary>Gets or sets the bin.</summary>
        /// <value>The bin.</value>
        string? Bin { get; set; }

        /// <summary>Gets or sets the minimum inventory.</summary>
        /// <value>The minimum inventory.</value>
        int? MinimumInventory { get; set; }

        /// <summary>Gets or sets the maximum inventory.</summary>
        /// <value>The maximum inventory.</value>
        int? MaximumInventory { get; set; }

        /// <summary>Gets or sets the number of inventories.</summary>
        /// <value>The number of inventories.</value>
        int? InventoryCount { get; set; }

        /// <summary>Gets or sets the cost multiplier.</summary>
        /// <value>The cost multiplier.</value>
        decimal? CostMultiplier { get; set; }

        /// <summary>Gets or sets the actual cost.</summary>
        /// <value>The actual cost.</value>
        decimal? ActualCost { get; set; }

        /// <summary>Gets or sets the listed price.</summary>
        /// <value>The listed price.</value>
        decimal? ListedPrice { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Vendors", "VendorProduct")]
    public class VendorProduct : Base, IVendorProduct
    {
        #region IAmARelationshipTable<Vendor, Product>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Vendor? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Product? Slave { get; set; }
        #endregion

        #region IAmFilterableByVendor
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByVendor.VendorID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Vendor? IAmFilterableByVendor.Vendor { get => Master; set => Master = value; }
        #endregion

        #region IAmAVendorRelationshipTableWhereVendorIsTheMaster
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAVendorRelationshipTableWhereVendorIsTheMaster<Product>.VendorID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Vendor? IAmAVendorRelationshipTableWhereVendorIsTheMaster<Product>.Vendor { get => Master; set => Master = value; }
        #endregion

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByProduct.ProductID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Product? IAmFilterableByProduct.Product { get => Slave; set => Slave = value; }
        #endregion

        #region IAmAProductRelationshipTableWhereProductIsTheSlave
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAProductRelationshipTableWhereProductIsTheSlave<Vendor>.ProductID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Product? IAmAProductRelationshipTableWhereProductIsTheSlave<Vendor>.Product { get => Slave; set => Slave = value; }
        #endregion

        #region VendorProduct Properties
        /// <inheritdoc/>
        [StringLength(1000), StringIsUnicode(false), DefaultValue(null)]
        public string? Bin { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? MinimumInventory { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? MaximumInventory { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? InventoryCount { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 0), DefaultValue(null)]
        public decimal? CostMultiplier { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? ListedPrice { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? ActualCost { get; set; }
        #endregion

        #region ICloneable Functions
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // This
            builder.Append("Bn: ").AppendLine(Bin);
            builder.Append("CM: ").AppendLine(CostMultiplier?.ToString("n5") ?? string.Empty);
            builder.Append("LP: ").AppendLine(ListedPrice?.ToString("c5") ?? string.Empty);
            builder.Append("AC: ").AppendLine(ActualCost?.ToString("c5") ?? string.Empty);
            builder.Append("IC: ").AppendLine(InventoryCount?.ToString("n0") ?? string.Empty);
            builder.Append("Mi: ").AppendLine(MinimumInventory?.ToString("n0") ?? string.Empty);
            builder.Append("Mx: ").AppendLine(MaximumInventory?.ToString("n0") ?? string.Empty);
            // Related Objects
            builder.Append("M: ").AppendLine(Master?.ToHashableString() ?? $"No Master={MasterID}");
            builder.Append("S: ").AppendLine(Slave?.ToHashableString() ?? $"No Slave={SlaveID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
