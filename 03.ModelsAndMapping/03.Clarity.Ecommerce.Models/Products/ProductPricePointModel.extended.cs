// <copyright file="ProductPricePointModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product price point model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the product price point.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IProductPricePointModel"/>
    public partial class ProductPricePointModel
    {
        #region ProductPricePoint Properties
        /// <inheritdoc/>
        public decimal? MinQuantity { get; set; } // 1

        /// <inheritdoc/>
        public decimal? MaxQuantity { get; set; } // decimal.Maximum

        /// <inheritdoc/>
        public string? UnitOfMeasure { get; set; } // Each

        /// <inheritdoc/>
        public decimal? Price { get; set; } // $1.00

        /// <inheritdoc/>
        public decimal? PercentDiscount { get; set; } // 0.10 (10%)

        /// <inheritdoc/>
        public DateTime? From { get; set; }

        /// <inheritdoc/>
        public DateTime? To { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        public string? CurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? CurrencyName { get; set; }

        /// <inheritdoc cref="IProductPricePointModel.Currency"/>
        public CurrencyModel? Currency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IProductPricePointModel.Currency { get => Currency; set => Currency = (CurrencyModel?)value; }

        /// <inheritdoc/>
        public int? PriceRoundingID { get; set; }

        /// <inheritdoc/>
        public string? PriceRoundingKey { get; set; }

        /// <inheritdoc cref="IProductPricePointModel.PriceRounding"/>
        public PriceRoundingModel? PriceRounding { get; set; } // Round to dollar, cents or other

        /// <inheritdoc/>
        IPriceRoundingModel? IProductPricePointModel.PriceRounding { get => PriceRounding; set => PriceRounding = (PriceRoundingModel?)value; }
        #endregion
    }
}
