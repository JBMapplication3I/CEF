// <copyright file="ProductInventoryLocationSectionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product inventory location section model class</summary>
namespace Clarity.Ecommerce.Models
{
    public partial class ProductInventoryLocationSectionModel
    {
        #region ProductInventoryLocationSection Properties
        /// <inheritdoc/>
        public decimal? Quantity { get; set; }

        /// <inheritdoc/>
        public decimal? QuantityAllocated { get; set; }

        /// <inheritdoc/>
        public decimal? QuantityPreSold { get; set; }

        /// <inheritdoc/>
        public decimal? QuantityBroken { get; set; }

        /// <inheritdoc/>
        public decimal? FlatQuantity { get; set; } // Flat from the Product

        /// <inheritdoc/>
        public decimal? FlatQuantityAllocated { get; set; } // Flat from the Product
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? InventoryLocationSectionInventoryLocationID { get; set; }

        /// <inheritdoc/>
        public string? InventoryLocationSectionInventoryLocationKey { get; set; }

        /// <inheritdoc/>
        public string? InventoryLocationSectionInventoryLocationName { get; set; }
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
