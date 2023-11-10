// <copyright file="AccountCurrencyModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account currency model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the account currency.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="Interfaces.Models.IAccountCurrencyModel"/>
    public partial class AccountCurrencyModel
    {
        /// <inheritdoc/>
        public bool IsPrimary { get; set; }

        /// <inheritdoc/>
        public string? CustomName { get; set; }

        /// <inheritdoc/>
        public string? CustomTranslationKey { get; set; }

        /// <inheritdoc/>
        public decimal OverrideUnicodeSymbolValue { get; set; }

        /// <inheritdoc/>
        public string? OverrideHtmlCharacterCode { get; set; }

        /// <inheritdoc/>
        public string? OverrideRawCharacter { get; set; }

        /// <inheritdoc/>
        public int? OverrideDecimalPlaceAccuracy { get; set; }

        /// <inheritdoc/>
        public bool? OverrideUseSeparator { get; set; }

        /// <inheritdoc/>
        public string? OverrideRawDecimalCharacter { get; set; }

        /// <inheritdoc/>
        public string? OverrideHtmlDecimalCharacterCode { get; set; }

        /// <inheritdoc/>
        public string? OverrideRawSeparatorCharacter { get; set; }

        /// <inheritdoc/>
        public string? OverrideHtmlSeparatorCharacterCode { get; set; }
    }
}
