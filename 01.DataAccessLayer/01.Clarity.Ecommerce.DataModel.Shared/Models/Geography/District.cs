// <copyright file="District.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the region class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IDistrict
        : IAmAGeographicalLocation<District, DistrictLanguage, DistrictCurrency, DistrictImage, DistrictImageType, TaxDistrict>
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the region.</summary>
        /// <value>The identifier of the region.</value>
        int? RegionID { get; set; }

        /// <summary>Gets or sets the region.</summary>
        /// <value>The region.</value>
        Region? Region { get; set; }

        /// <summary>Gets or sets the identifier of the country.</summary>
        /// <value>The identifier of the country.</value>
        int CountryID { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        Country? Country { get; set; }
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

    [SqlSchema("Geography", "District")]
    public class District : NameableBase, IDistrict
    {
        private ICollection<DistrictImage>? images;
        private ICollection<DistrictLanguage>? languages;
        private ICollection<DistrictCurrency>? currencies;
        private ICollection<TaxDistrict>? taxes;

        public District()
        {
            // IHaveImagesBase Properties
            images = new HashSet<DistrictImage>();
            // IHaveLanguagesBase Properties
            languages = new HashSet<DistrictLanguage>();
            // IHaveCurrenciesBase Properties
            currencies = new HashSet<DistrictCurrency>();
            // IAmAGeographicalLocation Properties
            taxes = new HashSet<TaxDistrict>();
        }

        #region IHaveImagesBase
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<DistrictImage>? Images { get => images; set => images = value; }
        #endregion

        #region IHaveLanguagesBase
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<DistrictLanguage>? Languages { get => languages; set => languages = value; }
        #endregion

        #region IHaveCurrenciesBase
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<DistrictCurrency>? Currencies { get => currencies; set => currencies = value; }
        #endregion

        #region IAmAGeographicalLocation
        /// <inheritdoc/>
        [Required, StringLength(50), StringIsUnicode(false), DefaultValue(null)]
        public string? Code { get; set; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<TaxDistrict>? Taxes { get => taxes; set => taxes = value; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Region))]
        public int? RegionID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Region? Region { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Country))]
        public int CountryID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Country? Country { get; set; }
        #endregion

        #region IClonable Functions
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Address
            builder.Append("Cd: ").AppendLine(Code ?? string.Empty);
            // Related Objects
            builder.Append("Re: ").AppendLine(Region?.ToHashableString() ?? $"No Region={RegionID}");
            builder.Append("Co: ").AppendLine(Country?.ToHashableString() ?? $"No Country={CountryID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
