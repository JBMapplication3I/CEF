// <copyright file="StoreProductModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store product model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the store product.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IStoreProductModel"/>
    public partial class StoreProductModel
    {
        /// <inheritdoc/>
        public bool IsVisibleIn { get; set; }

        /// <inheritdoc/>
        public decimal? PriceBase { get; set; }

        /// <inheritdoc/>
        public decimal? PriceMsrp { get; set; }

        /// <inheritdoc/>
        public decimal? PriceReduction { get; set; }

        /// <inheritdoc/>
        public decimal? PriceSale { get; set; }

        #region ICloneable Functions
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Contact
            builder.Append("VS: ").AppendLine(IsVisibleIn.ToString());
            builder.Append("PM: ").AppendLine(PriceMsrp?.ToString("c5") ?? string.Empty);
            builder.Append("PR: ").AppendLine(PriceReduction?.ToString("c5") ?? string.Empty);
            builder.Append("PB: ").AppendLine(PriceBase?.ToString("c5") ?? string.Empty);
            builder.Append("PS: ").AppendLine(PriceSale?.ToString("c5") ?? string.Empty);
            // Related Objects
            builder.Append("M: ").AppendLine(Master?.ToHashableString() ?? $"No Master={MasterID}");
            builder.Append("S: ").AppendLine(Slave?.ToHashableString() ?? $"No Slave={SlaveID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
