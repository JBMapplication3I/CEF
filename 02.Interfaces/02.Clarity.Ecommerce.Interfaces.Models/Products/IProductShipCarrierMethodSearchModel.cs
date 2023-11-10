// <copyright file="IProductShipCarrierMethodSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductShipCarrierMethodSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for product price point search model.</summary>
    public partial interface IProductShipCarrierMethodSearchModel
    {
        /// <summary>Gets or sets the minimum quantity.</summary>
        /// <value>The minimum quantity.</value>
        decimal? MinQuantity { get; set; }

        /// <summary>Gets or sets the maximum quantity.</summary>
        /// <value>The maximum quantity.</value>
        decimal? MaxQuantity { get; set; }

        /// <summary>Gets or sets the Date/Time of from.</summary>
        /// <value>The from value.</value>
        DateTime? From { get; set; }

        /// <summary>Gets or sets the Date/Time of to.</summary>
        /// <value>The to value.</value>
        DateTime? To { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        string? CurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the currency.</summary>
        /// <value>The name of the currency.</value>
        string? CurrencyName { get; set; }

        /// <summary>Gets or sets the ship carrier method i ds.</summary>
        /// <value>The ship carrier method i ds.</value>
        List<int?>? ShipCarrierMethodIDs { get; set; }
        #endregion
    }
}
