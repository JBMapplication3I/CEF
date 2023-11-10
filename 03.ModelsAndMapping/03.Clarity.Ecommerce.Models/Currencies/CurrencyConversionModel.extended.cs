// <copyright file="CurrencyConversionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the currency conversion model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    public partial class CurrencyConversionModel
    {
        #region CurrencyConversion Properties
        /// <inheritdoc/>
        public decimal Rate { get; set; }

        /// <inheritdoc/>
        public DateTime? StartDate { get; set; }

        /// <inheritdoc/>
        public DateTime? EndDate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int StartingCurrencyID { get; set; }

        /// <inheritdoc/>
        public string? StartingCurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? StartingCurrencyName { get; set; }

        /// <inheritdoc cref="ICurrencyConversionModel.StartingCurrency"/>
        public CurrencyModel? StartingCurrency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? ICurrencyConversionModel.StartingCurrency { get => StartingCurrency; set => StartingCurrency = (CurrencyModel?)value; }

        /// <inheritdoc/>
        public int EndingCurrencyID { get; set; }

        /// <inheritdoc/>
        public string? EndingCurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? EndingCurrencyName { get; set; }

        /// <inheritdoc cref="ICurrencyConversionModel.EndingCurrency"/>
        public CurrencyModel? EndingCurrency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? ICurrencyConversionModel.EndingCurrency { get => EndingCurrency; set => EndingCurrency = (CurrencyModel?)value; }
        #endregion
    }
}
