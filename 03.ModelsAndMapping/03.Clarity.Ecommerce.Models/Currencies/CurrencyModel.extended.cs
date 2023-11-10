// <copyright file="CurrencyModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the currency model class</summary>
namespace Clarity.Ecommerce.Models
{
    public partial class CurrencyModel
    {
        /// <inheritdoc/>
        public string? ISO4217Alpha { get; set; }

        /// <inheritdoc/>
        public int? ISO4217Numeric { get; set; }

        /// <inheritdoc/>
        public decimal UnicodeSymbolValue { get; set; }

        /// <inheritdoc/>
        public string? HtmlCharacterCode { get; set; }

        /// <inheritdoc/>
        public string? RawCharacter { get; set; }

        /// <inheritdoc/>
        public int? DecimalPlaceAccuracy { get; set; }

        /// <inheritdoc/>
        public bool? UseSeparator { get; set; }

        /// <inheritdoc/>
        public string? RawDecimalCharacter { get; set; }

        /// <inheritdoc/>
        public string? HtmlDecimalCharacterCode { get; set; }

        /// <inheritdoc/>
        public string? RawSeparatorCharacter { get; set; }

        /// <inheritdoc/>
        public string? HtmlSeparatorCharacterCode { get; set; }
    }
}
