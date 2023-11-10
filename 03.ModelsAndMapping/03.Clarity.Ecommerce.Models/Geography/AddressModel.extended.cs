// <copyright file="AddressModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the address.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IAddressModel"/>
    public partial class AddressModel
    {
        #region Address Properties
        /// <inheritdoc/>
        public string? Company { get; set; }

        /// <inheritdoc/>
        public string? Street1 { get; set; }

        /// <inheritdoc/>
        public string? Street2 { get; set; }

        /// <inheritdoc/>
        public string? Street3 { get; set; }

        /// <inheritdoc/>
        public string? City { get; set; }

        /// <inheritdoc/>
        public string? PostalCode { get; set; }

        /// <inheritdoc/>
        public decimal? Latitude { get; set; }

        /// <inheritdoc/>
        public decimal? Longitude { get; set; }

        /// <inheritdoc/>
        public string? RegionCustom { get; set; }

        /// <inheritdoc/>
        public string? CountryCustom { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? RegionID { get; set; }

        /// <inheritdoc/>
        public string? RegionKey { get; set; }

        /// <inheritdoc/>
        public string? RegionName { get; set; }

        /// <inheritdoc/>
        public string? RegionCode { get; set; }

        /// <inheritdoc cref="IAddressModel.Region"/>
        public RegionModel? Region { get; set; }

        /// <inheritdoc/>
        IRegionModel? IAddressModel.Region { get => Region; set => Region = (RegionModel?)value; }

        /// <inheritdoc/>
        public int? CountryID { get; set; }

        /// <inheritdoc/>
        public string? CountryKey { get; set; }

        /// <inheritdoc/>
        public string? CountryName { get; set; }

        /// <inheritdoc/>
        public string? CountryCode { get; set; }

        /// <inheritdoc cref="IAddressModel.Country"/>
        public CountryModel? Country { get; set; }

        /// <inheritdoc/>
        ICountryModel? IAddressModel.Country { get => Country; set => Country = (CountryModel?)value; }
        #endregion

        #region IClonable
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            // Base
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
