// <copyright file="IProductPricePointSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductPricePointSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for product price point search model.</summary>
    public partial interface IProductPricePointSearchModel
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

        #region Currency
        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        string? CurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the currency.</summary>
        /// <value>The name of the currency.</value>
        string? CurrencyName { get; set; }
        #endregion

        #region Account
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets the account key.</summary>
        /// <value>The account key.</value>
        string? AccountKey { get; set; }

        /// <summary>Gets or sets the name of the account.</summary>
        /// <value>The name of the account.</value>
        string? AccountName { get; set; }
        #endregion

        #region Price Point
        /// <summary>Gets or sets the price point i ds.</summary>
        /// <value>The price point i ds.</value>
        List<int?>? PricePointIDs { get; set; }
        #endregion
    }
}
