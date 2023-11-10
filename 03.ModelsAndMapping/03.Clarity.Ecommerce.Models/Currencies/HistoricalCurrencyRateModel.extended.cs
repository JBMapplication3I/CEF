// <copyright file="HistoricalCurrencyRateModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the historical currency rate model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the historical currency rate.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IHistoricalCurrencyRateModel"/>
    public partial class HistoricalCurrencyRateModel
    {
        #region HistoricalCurrencyRate Properties
        /// <inheritdoc/>
        public decimal Rate { get; set; }

        /// <inheritdoc/>
        public DateTime OnDate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int StartingCurrencyID { get; set; }

        /// <inheritdoc/>
        public string? StartingCurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? StartingCurrencyName { get; set; }

        /// <inheritdoc cref="IHistoricalCurrencyRateModel.StartingCurrency"/>
        public CurrencyModel? StartingCurrency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IHistoricalCurrencyRateModel.StartingCurrency { get => StartingCurrency; set => StartingCurrency = (CurrencyModel?)value; }

        /// <inheritdoc/>
        public int EndingCurrencyID { get; set; }

        /// <inheritdoc/>
        public string? EndingCurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? EndingCurrencyName { get; set; }

        /// <inheritdoc cref="IHistoricalCurrencyRateModel.EndingCurrency"/>
        public CurrencyModel? EndingCurrency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IHistoricalCurrencyRateModel.EndingCurrency { get => EndingCurrency; set => EndingCurrency = (CurrencyModel?)value; }
        #endregion
    }
}
