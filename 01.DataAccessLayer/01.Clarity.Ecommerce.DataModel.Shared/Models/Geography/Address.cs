// <copyright file="Address.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAddress : IBase
    {
        #region Address Properties
        /// <summary>Gets or sets the company.</summary>
        /// <value>The company.</value>
        string? Company { get; set; }

        /// <summary>Gets or sets the street 1.</summary>
        /// <value>The street 1.</value>
        string? Street1 { get; set; }

        /// <summary>Gets or sets the street 2.</summary>
        /// <value>The street 2.</value>
        string? Street2 { get; set; }

        /// <summary>Gets or sets the street 3.</summary>
        /// <value>The street 3.</value>
        string? Street3 { get; set; }

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        string? City { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        string? PostalCode { get; set; }

        /// <summary>Gets or sets the latitude.</summary>
        /// <value>The latitude.</value>
        decimal? Latitude { get; set; }

        /// <summary>Gets or sets the longitude.</summary>
        /// <value>The longitude.</value>
        decimal? Longitude { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the country.</summary>
        /// <value>The identifier of the country.</value>
        int? CountryID { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        Country? Country { get; set; }

        /// <summary>Gets or sets the country custom.</summary>
        /// <value>The country custom.</value>
        string? CountryCustom { get; set; }

        /// <summary>Gets or sets the identifier of the region.</summary>
        /// <value>The identifier of the region.</value>
        int? RegionID { get; set; }

        /// <summary>Gets or sets the region.</summary>
        /// <value>The region.</value>
        Region? Region { get; set; }

        /// <summary>Gets or sets the region custom.</summary>
        /// <value>The region custom.</value>
        string? RegionCustom { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Geography", "Address")]
    public class Address : Base, IAddress
    {
        #region Address Properties
        /// <inheritdoc/>
        [StringLength(255), StringIsUnicode(false), DefaultValue(null), Column("Name")]
        public string? Company { get; set; }

        /// <inheritdoc/>
        [StringLength(255), DefaultValue(null)]
        public string? Street1 { get; set; }

        /// <inheritdoc/>
        [StringLength(255), DefaultValue(null)]
        public string? Street2 { get; set; }

        /// <inheritdoc/>
        [StringLength(255), DefaultValue(null)]
        public string? Street3 { get; set; }

        /// <inheritdoc/>
        [StringLength(100), DefaultValue(null)]
        public string? City { get; set; }

        /// <inheritdoc/>
        [StringLength(100), DefaultValue(null)]
        public string? RegionCustom { get; set; }

        /// <inheritdoc/>
        [StringLength(100), DefaultValue(null)]
        public string? CountryCustom { get; set; }

        /// <inheritdoc/>
        [StringLength(50), DefaultValue(null)]
        public string? PostalCode { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 8), DefaultValue(null)]
        public decimal? Latitude { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 8), DefaultValue(null)]
        public decimal? Longitude { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Country)), DefaultValue(null)]
        public int? CountryID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Country? Country { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Region)), DefaultValue(null)]
        public int? RegionID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Region? Region { get; set; }
        #endregion

        #region IClonable Functions
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Address
            builder.Append("Co: ").AppendLine(Company ?? string.Empty);
            builder.Append("S1: ").AppendLine(Street1 ?? string.Empty);
            builder.Append("S2: ").AppendLine(Street2 ?? string.Empty);
            builder.Append("S3: ").AppendLine(Street3 ?? string.Empty);
            builder.Append("Ci: ").AppendLine(City ?? string.Empty);
            builder.Append("PC: ").AppendLine(PostalCode ?? string.Empty);
            builder.Append("La: ").AppendLine(Latitude?.ToString("n10") ?? string.Empty);
            builder.Append("Lo: ").AppendLine(Longitude?.ToString("n10") ?? string.Empty);
            // Related Objects
            builder.Append("C: ").AppendLine(Country?.ToHashableString() ?? $"No Country={CountryID}");
            builder.Append("CC: ").AppendLine(CountryCustom ?? string.Empty);
            builder.Append("R: ").AppendLine(Region?.ToHashableString() ?? $"No Region={RegionID}");
            builder.Append("RC: ").AppendLine(RegionCustom ?? string.Empty);
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
