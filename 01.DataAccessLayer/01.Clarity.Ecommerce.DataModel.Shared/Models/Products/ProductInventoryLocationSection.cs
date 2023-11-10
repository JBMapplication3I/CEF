// <copyright file="ProductInventoryLocationSection.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product inventory location section class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IProductInventoryLocationSection
        : IAmAProductRelationshipTableWhereProductIsTheMaster<InventoryLocationSection>
    {
        #region ProductInventoryLocationSection Properties
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal? Quantity { get; set; }

        /// <summary>Gets or sets the quantity allocated.</summary>
        /// <value>The quantity allocated.</value>
        decimal? QuantityAllocated { get; set; }

        /// <summary>Gets or sets the quantity pre-sold.</summary>
        /// <value>The quantity pre-sold.</value>
        decimal? QuantityPreSold { get; set; }

        /// <summary>Gets or sets the quantity broken.</summary>
        /// <value>The quantity broken.</value>
        decimal? QuantityBroken { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Products", "ProductInventoryLocationSection")]
    public class ProductInventoryLocationSection : Base, IProductInventoryLocationSection
    {
        #region IAmARelationshipTable<Product, InventoryLocationSection>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Product? Master { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByProduct.ProductID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Product? IAmFilterableByProduct.Product { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAProductRelationshipTableWhereProductIsTheMaster<InventoryLocationSection>.ProductID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Product? IAmAProductRelationshipTableWhereProductIsTheMaster<InventoryLocationSection>.Product { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual InventoryLocationSection? Slave { get; set; }
        #endregion

        #region ProductInventoryLocationSection Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? Quantity { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityAllocated { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityPreSold { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? QuantityBroken { get; set; }
        #endregion

        #region ICloneable Functions
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // This
            builder.Append("Q: ").AppendLine(Quantity?.ToString("n5") ?? string.Empty);
            builder.Append("Qa: ").AppendLine(QuantityAllocated?.ToString("n5") ?? string.Empty);
            builder.Append("Qp: ").AppendLine(QuantityPreSold?.ToString("n5") ?? string.Empty);
            builder.Append("Qb: ").AppendLine(QuantityBroken?.ToString("n5") ?? string.Empty);
            // Related Objects
            builder.Append("M: ").AppendLine(Master?.ToHashableString() ?? $"No Master={MasterID}");
            builder.Append("S: ").AppendLine(Slave?.ToHashableString() ?? $"No Slave={SlaveID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
