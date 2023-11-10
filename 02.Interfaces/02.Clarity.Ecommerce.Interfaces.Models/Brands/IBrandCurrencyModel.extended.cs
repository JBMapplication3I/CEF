// <copyright file="IBrandCurrencyModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IBrandCurrencyModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IBrandCurrencyModel
    {
        #region BrandCurrency
        /// <summary>Gets or sets a value indicating whether this  is primary.</summary>
        /// <value>True if this  is primary, false if not.</value>
        bool IsPrimary { get; set; }

        /// <summary>Gets or sets the name of the custom.</summary>
        /// <value>The name of the custom.</value>
        string? CustomName { get; set; }

        /// <summary>Gets or sets the custom translation key.</summary>
        /// <value>The custom translation key.</value>
        string? CustomTranslationKey { get; set; }

        /// <summary>Gets or sets the override unicode symbol value.</summary>
        /// <value>The override unicode symbol value.</value>
        decimal OverrideUnicodeSymbolValue { get; set; }

        /// <summary>Gets or sets the override HTML character code.</summary>
        /// <value>The override HTML character code.</value>
        string? OverrideHtmlCharacterCode { get; set; }

        /// <summary>Gets or sets the override raw character.</summary>
        /// <value>The override raw character.</value>
        string? OverrideRawCharacter { get; set; }

        /// <summary>Gets or sets the override decimal place accuracy.</summary>
        /// <value>The override decimal place accuracy.</value>
        int? OverrideDecimalPlaceAccuracy { get; set; }

        /// <summary>Gets or sets the override use separator.</summary>
        /// <value>The override use separator.</value>
        bool? OverrideUseSeparator { get; set; }

        /// <summary>Gets or sets the override raw decimal character.</summary>
        /// <value>The override raw decimal character.</value>
        string? OverrideRawDecimalCharacter { get; set; }

        /// <summary>Gets or sets the override HTML decimal character code.</summary>
        /// <value>The override HTML decimal character code.</value>
        string? OverrideHtmlDecimalCharacterCode { get; set; }

        /// <summary>Gets or sets the override raw separator character.</summary>
        /// <value>The override raw separator character.</value>
        string? OverrideRawSeparatorCharacter { get; set; }

        /// <summary>Gets or sets the override HTML separator character code.</summary>
        /// <value>The override HTML separator character code.</value>
        string? OverrideHtmlSeparatorCharacterCode { get; set; }
        #endregion
    }
}
