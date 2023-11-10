// <copyright file="FranchiseCurrencyModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise currency model class</summary>
namespace Clarity.Ecommerce.Models
{
    public partial class FranchiseCurrencyModel
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
