// <copyright file="PriceRoundingModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rounding model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the price rounding.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="Interfaces.Models.IPriceRoundingModel"/>
    public partial class PriceRoundingModel
    {
        /// <inheritdoc/>
        public string? ProductKey { get; set; }

        /// <inheritdoc/>
        public string? CurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? PricePointKey { get; set; }

        /// <inheritdoc/>
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public int RoundHow { get; set; }

        /// <inheritdoc/>
        public int RoundTo { get; set; }

        /// <inheritdoc/>
        public int RoundingAmount { get; set; }
    }
}
