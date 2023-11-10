// <copyright file="ProductShipCarrierMethodModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product price point model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the product price point.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IProductShipCarrierMethodModel"/>
    public partial class ProductShipCarrierMethodModel
    {
        #region ProductShipCarrierMethod Properties
        /// <inheritdoc/>
        public decimal? MinQuantity { get; set; } // 1

        /// <inheritdoc/>
        public decimal? MaxQuantity { get; set; } // decimal.Maximum

        /// <inheritdoc/>
        public DateTime? From { get; set; }

        /// <inheritdoc/>
        public DateTime? To { get; set; }

        /// <inheritdoc/>
        public string? UnitOfMeasure { get; set; } // Each

        /// <inheritdoc/>
        public decimal Price { get; set; } // $1.00
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        public string? CurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? CurrencyName { get; set; }

        /// <inheritdoc cref="IProductShipCarrierMethodModel.Currency"/>
        public CurrencyModel? Currency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IProductShipCarrierMethodModel.Currency { get => Currency; set => Currency = (CurrencyModel?)value; }
        #endregion
    }
}
