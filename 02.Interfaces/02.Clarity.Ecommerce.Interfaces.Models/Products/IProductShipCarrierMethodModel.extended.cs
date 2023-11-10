// <copyright file="IProductShipCarrierMethodModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductShipCarrierMethodModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for product price point model.</summary>
    public partial interface IProductShipCarrierMethodModel
    {
        /// <summary>Gets or sets the minimum quantity.</summary>
        /// <value>The minimum quantity.</value>
        decimal? MinQuantity { get; set; } // 1

        /// <summary>Gets or sets the maximum quantity.</summary>
        /// <value>The maximum quantity.</value>
        decimal? MaxQuantity { get; set; } // decimal.Maximum

        /// <summary>Gets or sets the Date/Time of from.</summary>
        /// <value>from.</value>
        DateTime? From { get; set; }

        /// <summary>Gets or sets the Date/Time of to.</summary>
        /// <value>to.</value>
        DateTime? To { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        string? UnitOfMeasure { get; set; } // Each

        /// <summary>Gets or sets the price.</summary>
        /// <value>The price.</value>
        decimal Price { get; set; } // $1.00

        #region Currency
        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int? CurrencyID { get; set; }

        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        string? CurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the currency.</summary>
        /// <value>The name of the currency.</value>
        string? CurrencyName { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        ICurrencyModel? Currency { get; set; }
        #endregion
    }
}
