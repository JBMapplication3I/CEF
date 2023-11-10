// <copyright file="VendorProductModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the vendor product model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the vendor product.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="Interfaces.Models.IVendorProductModel"/>
    public partial class VendorProductModel
    {
        /// <inheritdoc/>
        public string? Bin { get; set; }

        /// <inheritdoc/>
        public int? MinimumInventory { get; set; }

        /// <inheritdoc/>
        public int? MaximumInventory { get; set; }

        /// <inheritdoc/>
        public decimal? CostMultiplier { get; set; }

        /// <inheritdoc/>
        public decimal? ActualCost { get; set; }

        /// <inheritdoc/>
        public decimal? ListedPrice { get; set; }

        /// <inheritdoc/>
        public int? InventoryCount { get; set; }

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
