// <copyright file="Region.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the region class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IRegion
        : IAmAGeographicalLocation<Region, RegionLanguage, RegionCurrency, RegionImage, RegionImageType, TaxRegion>
    {
        /// <summary>Gets or sets the ISO 3166-1 code.</summary>
        /// <value>The ISO 3166-1 code.</value>
        string? ISO31661 { get; set; }

        /// <summary>Gets or sets the ISO 3166-2 code.</summary>
        /// <value>The ISO 3166-2 code.</value>
        string? ISO31662 { get; set; }

        /// <summary>Gets or sets the ISO 3166 alpha 2 code.</summary>
        /// <value>The ISO 3166 alpha 2 code.</value>
        string? ISO3166Alpha2 { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the country.</summary>
        /// <value>The identifier of the country.</value>
        int CountryID { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        Country? Country { get; set; }
        #endregion

        #region Associated Objects
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
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Geography", "Region")]
    public class Region : NameableBase, IRegion
    {
        private ICollection<RegionImage>? images;
        private ICollection<RegionLanguage>? languages;
        private ICollection<RegionCurrency>? currencies;
        private ICollection<TaxRegion>? taxes;
        private ICollection<District>? districts;

        public Region()
        {
            // IHaveImagesBase Properties
            images = new HashSet<RegionImage>();
            // IHaveLanguagesBase Properties
            languages = new HashSet<RegionLanguage>();
            // IHaveCurrenciesBase Properties
            currencies = new HashSet<RegionCurrency>();
            // IAmAGeographicalLocation Properties
            taxes = new HashSet<TaxRegion>();
            // Associated Objects
            districts = new HashSet<District>();
        }

        #region IHaveImagesBase
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<RegionImage>? Images { get => images; set => images = value; }
        #endregion

        #region IHaveLanguagesBase
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<RegionLanguage>? Languages { get => languages; set => languages = value; }
        #endregion

        #region IHaveCurrenciesBase
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<RegionCurrency>? Currencies { get => currencies; set => currencies = value; }
        #endregion

        #region IAmAGeographicalLocation
        /// <inheritdoc/>
        [Required, StringLength(50), StringIsUnicode(false), DefaultValue(null)]
        public string? Code { get; set; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<TaxRegion>? Taxes { get => taxes; set => taxes = value; }
        #endregion

        #region Region Properties
        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(10), DefaultValue(null)]
        public string? ISO31661 { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(10), DefaultValue(null)]
        public string? ISO31662 { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(10), DefaultValue(null)]
        public string? ISO3166Alpha2 { get; set; }
        #endregion

        #region Related Objects
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Country))]
        public int CountryID { get; set; }

        [ForceMapOutWithLite]
        public virtual Country? Country { get; set; }
        #endregion

        #region Associated Objects
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
            // Related Objects
            builder.Append("Co: ").AppendLine(Country?.ToHashableString() ?? $"No Country={CountryID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
