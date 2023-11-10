// <copyright file="Country.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the country class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ICountry
        : IAmAGeographicalLocation<Country, CountryLanguage, CountryCurrency, CountryImage, CountryImageType, TaxCountry>
    {
        #region Country Properties
        /// <summary>Gets or sets the ISO 3166 alpha 2 code.</summary>
        /// <value>The ISO 3166 alpha 2 code.</value>
        string? ISO3166Alpha2 { get; set; }

        /// <summary>Gets or sets the ISO 3166 alpha 3 code.</summary>
        /// <value>The ISO 3166 alpha 3 code.</value>
        string? ISO3166Alpha3 { get; set; }

        /// <summary>Gets or sets the ISO 3166 numeric code.</summary>
        /// <value>The ISO 3166 numeric code.</value>
        int? ISO3166Numeric { get; set; }

        /// <summary>Gets or sets the phone RegEx.</summary>
        /// <value>The phone RegEx.</value>
        string? PhoneRegEx { get; set; }

        /// <summary>Gets or sets the phone prefix.</summary>
        /// <value>The phone prefix.</value>
        string? PhonePrefix { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the regions.</summary>
        /// <value>The regions.</value>
        ICollection<Region>? Regions { get; set; }

        /// <summary>Gets or sets the districts.</summary>
        /// <value>The districts.</value>
        ICollection<District>? Districts { get; set; }
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

    [SqlSchema("Geography", "Country")]
    public class Country : NameableBase, ICountry
    {
        private ICollection<Region>? regions;
        private ICollection<District>? districts;
        private ICollection<CountryImage>? images;
        private ICollection<TaxCountry>? taxes;
        private ICollection<CountryLanguage>? languages;
        private ICollection<CountryCurrency>? currencies;

        public Country()
        {
            // IHaveImagesBase Properties
            images = new HashSet<CountryImage>();
            // IHaveLanguagesBase Properties
            languages = new HashSet<CountryLanguage>();
            // IHaveCurrenciesBase Properties
            currencies = new HashSet<CountryCurrency>();
            // IAmAGeographicalLocation Properties
            taxes = new HashSet<TaxCountry>();
            // Country Properties
            regions = new HashSet<Region>();
            districts = new HashSet<District>();
        }

        #region IHaveImagesBase
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CountryImage>? Images { get => images; set => images = value; }
        #endregion

        #region IHaveLanguagesBase
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CountryLanguage>? Languages { get => languages; set => languages = value; }
        #endregion

        #region IHaveCurrenciesBase
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CountryCurrency>? Currencies { get => currencies; set => currencies = value; }
        #endregion

        #region IAmAGeographicalLocation
        /// <inheritdoc/>
        [Required, StringLength(50), StringIsUnicode(false), DefaultValue(null)]
        public string? Code { get; set; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<TaxCountry>? Taxes { get => taxes; set => taxes = value; }
        #endregion

        #region Country Properties
        /// <inheritdoc/>
        [StringLength(2), StringIsUnicode(false), DefaultValue(null)]
        public string? ISO3166Alpha2 { get; set; }

        /// <inheritdoc/>
        [StringLength(3), StringIsUnicode(false), DefaultValue(null)]
        public string? ISO3166Alpha3 { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ISO3166Numeric { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? PhoneRegEx { get; set; }

        /// <inheritdoc/>
        [StringLength(10), StringIsUnicode(false), DefaultValue(null)]
        public string? PhonePrefix { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Region>? Regions { get => regions; set => regions = value; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<District>? Districts { get => districts; set => districts = value; }
        #endregion

        #region IClonable Functions
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Address
            builder.Append("Cd: ").AppendLine(Code ?? string.Empty);
            builder.Append("Rx: ").AppendLine(PhoneRegEx ?? string.Empty);
            builder.Append("Px: ").AppendLine(PhonePrefix ?? string.Empty);
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
