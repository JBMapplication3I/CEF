// <copyright file="IHistoricalCurrencyRateModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHistoricalCurrencyRateModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for historical currency rate model.</summary>
    public partial interface IHistoricalCurrencyRateModel
    {
        #region HistoricalCurrencyRate Properties
        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal Rate { get; set; }

        /// <summary>Gets or sets the on date.</summary>
        /// <value>The on date.</value>
        DateTime OnDate { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the starting currency.</summary>
        /// <value>The identifier of the starting currency.</value>
        int StartingCurrencyID { get; set; }

        /// <summary>Gets or sets the starting currency.</summary>
        /// <value>The starting currency.</value>
        ICurrencyModel? StartingCurrency { get; set; }

        /// <summary>Gets or sets the starting currency key.</summary>
        /// <value>The starting currency key.</value>
        string? StartingCurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the starting currency.</summary>
        /// <value>The name of the starting currency.</value>
        string? StartingCurrencyName { get; set; }

        /// <summary>Gets or sets the identifier of the ending currency.</summary>
        /// <value>The identifier of the ending currency.</value>
        int EndingCurrencyID { get; set; }

        /// <summary>Gets or sets the ending currency.</summary>
        /// <value>The ending currency.</value>
        ICurrencyModel? EndingCurrency { get; set; }

        /// <summary>Gets or sets the ending currency key.</summary>
        /// <value>The ending currency key.</value>
        string? EndingCurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the ending currency.</summary>
        /// <value>The name of the ending currency.</value>
        string? EndingCurrencyName { get; set; }
        #endregion
    }
}
