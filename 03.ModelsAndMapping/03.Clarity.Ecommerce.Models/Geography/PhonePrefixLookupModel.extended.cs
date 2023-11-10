// <copyright file="PhonePrefixLookupModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the phone prefix lookup model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the phone prefix lookup.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IPhonePrefixLookupModel"/>
    public partial class PhonePrefixLookupModel
    {
        /// <inheritdoc/>
        public string? Prefix { get; set; }

        /// <inheritdoc/>
        public string? TimeZone { get; set; }

        /// <inheritdoc/>
        public string? CityName { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int? CountryID { get; set; }

        /// <inheritdoc/>
        public string? CountryKey { get; set; }

        /// <inheritdoc/>
        public string? CountryCode { get; set; }

        /// <inheritdoc/>
        public string? CountryName { get; set; }

        /// <inheritdoc cref="IPhonePrefixLookupModel.Country"/>
        public CountryModel? Country { get; set; }

        /// <inheritdoc/>
        ICountryModel? IPhonePrefixLookupModel.Country { get => Country; set => Country = (CountryModel?)value; }

        /// <inheritdoc/>
        public int? RegionID { get; set; }

        /// <inheritdoc/>
        public string? RegionKey { get; set; }

        /// <inheritdoc/>
        public string? RegionCode { get; set; }

        /// <inheritdoc/>
        public string? RegionName { get; set; }

        /// <inheritdoc cref="IPhonePrefixLookupModel.Region"/>
        public RegionModel? Region { get; set; }

        /// <inheritdoc/>
        IRegionModel? IPhonePrefixLookupModel.Region { get => Region; set => Region = (RegionModel?)value; }
        #endregion
    }
}
