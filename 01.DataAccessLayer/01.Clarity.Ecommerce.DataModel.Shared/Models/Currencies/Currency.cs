// <copyright file="Currency.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the currency class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ICurrency
        : INameableBase,
            IHaveImagesBase<Currency, CurrencyImage, CurrencyImageType>
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

        #region Associated Objects
        /// <summary>Gets or sets the starting currencies.</summary>
        /// <value>The starting currencies.</value>
        ICollection<HistoricalCurrencyRate>? HistoricalStartingCurrencies { get; set; }

        /// <summary>Gets or sets the ending currencies.</summary>
        /// <value>The ending currencies.</value>
        ICollection<HistoricalCurrencyRate>? HistoricalEndingCurrencies { get; set; }

        /// <summary>Gets or sets the starting currencies.</summary>
        /// <value>The starting currencies.</value>
        ICollection<CurrencyConversion>? ConversionStartingCurrencies { get; set; }

        /// <summary>Gets or sets the ending currencies.</summary>
        /// <value>The ending currencies.</value>
        ICollection<CurrencyConversion>? ConversionEndingCurrencies { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Currencies", "Currency")]
    public class Currency : NameableBase, ICurrency
    {
        private ICollection<CurrencyImage>? images;
        private ICollection<HistoricalCurrencyRate>? historicalStartingCurrencies;
        private ICollection<HistoricalCurrencyRate>? historicalEndingCurrencies;
        private ICollection<CurrencyConversion>? conversionStartingCurrencies;
        private ICollection<CurrencyConversion>? conversionEndingCurrencies;

        public Currency()
        {
            // IHaveImagesBase
            images = new HashSet<CurrencyImage>();
            // Currency Properties
            historicalStartingCurrencies = new HashSet<HistoricalCurrencyRate>();
            historicalEndingCurrencies = new HashSet<HistoricalCurrencyRate>();
            conversionStartingCurrencies = new HashSet<CurrencyConversion>();
            conversionEndingCurrencies = new HashSet<CurrencyConversion>();
        }

        #region IHaveImagesBase
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapInEver, DontMapOutEver]
        public virtual ICollection<CurrencyImage>? Images { get => images; set => images = value; }
        #endregion

        #region Currency Properties
        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(3), DefaultValue(null)]
        public string? ISO4217Alpha { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ISO4217Numeric { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal UnicodeSymbolValue { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(12), DefaultValue(null)]
        public string? HtmlCharacterCode { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(5), DefaultValue(null)]
        public string? RawCharacter { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? DecimalPlaceAccuracy { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool? UseSeparator { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(5), DefaultValue(null)]
        public string? RawDecimalCharacter { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(12), DefaultValue(null)]
        public string? HtmlDecimalCharacterCode { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(5), DefaultValue(null)]
        public string? RawSeparatorCharacter { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(12), DefaultValue(null)]
        public string? HtmlSeparatorCharacterCode { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<HistoricalCurrencyRate>? HistoricalStartingCurrencies { get => historicalStartingCurrencies; set => historicalStartingCurrencies = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<HistoricalCurrencyRate>? HistoricalEndingCurrencies { get => historicalEndingCurrencies; set => historicalEndingCurrencies = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<CurrencyConversion>? ConversionStartingCurrencies { get => conversionStartingCurrencies; set => conversionStartingCurrencies = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<CurrencyConversion>? ConversionEndingCurrencies { get => conversionEndingCurrencies; set => conversionEndingCurrencies = value; }
        #endregion
    }
}
