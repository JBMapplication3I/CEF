// <copyright file="ICurrencyModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICurrencyModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for currency model.</summary>
    public partial interface ICurrencyModel
    {
        #region Currency Properties
        /// <summary>Gets or sets the ISO 4217 alphabetical code.</summary>
        /// <value>The ISO 4217 alphabetical code.</value>
        string? ISO4217Alpha { get; set; }

        /// <summary>Gets or sets the ISO 4217 numeric code.</summary>
        /// <value>The ISO 4217 numeric code.</value>
        int? ISO4217Numeric { get; set; }

        /// <summary>Gets or sets the unicode symbol value.</summary>
        /// <value>The unicode symbol value.</value>
        decimal UnicodeSymbolValue { get; set; }

        /// <summary>Gets or sets the HTML character code.</summary>
        /// <value>The HTML character code.</value>
        string? HtmlCharacterCode { get; set; }

        /// <summary>Gets or sets the raw character.</summary>
        /// <value>The raw character.</value>
        string? RawCharacter { get; set; }

        /// <summary>Gets or sets the decimal place accuracy (how far passed the decimal point).</summary>
        /// <value>The decimal place accuracy.</value>
        int? DecimalPlaceAccuracy { get; set; }

        /// <summary>Gets or sets the use separator flag (e.g.- ###0 vs #,##0).</summary>
        /// <value>The use separator.</value>
        bool? UseSeparator { get; set; }

        /// <summary>Gets or sets the raw decimal character.</summary>
        /// <value>The raw decimal character.</value>
        string? RawDecimalCharacter { get; set; }

        /// <summary>Gets or sets the HTML decimal character code.</summary>
        /// <value>The HTML decimal character code.</value>
        string? HtmlDecimalCharacterCode { get; set; }

        /// <summary>Gets or sets the raw separator character.</summary>
        /// <value>The raw separator character.</value>
        string? RawSeparatorCharacter { get; set; }

        /// <summary>Gets or sets the HTML separator character code.</summary>
        /// <value>The HTML separator character code.</value>
        string? HtmlSeparatorCharacterCode { get; set; }
        #endregion
    }
}
