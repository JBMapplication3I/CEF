// <copyright file="ZipCodeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the zip code model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the zip code.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="Interfaces.Models.IZipCodeModel"/>
    public partial class ZipCodeModel
    {
        /// <inheritdoc/>
        public string? ZipCodeValue { get; set; }

        /// <inheritdoc/>
        public string? ZipType { get; set; }

        /// <inheritdoc/>
        public string? CityName { get; set; }

        /// <inheritdoc/>
        public string? CityType { get; set; }

        /// <inheritdoc/>
        public string? CountyName { get; set; }

        /// <inheritdoc/>
        public long? CountyFIPS { get; set; }

        /// <inheritdoc/>
        public string? StateName { get; set; }

        /// <inheritdoc/>
        public string? StateAbbreviation { get; set; }

        /// <inheritdoc/>
        public long? StateFIPS { get; set; }

        /// <inheritdoc/>
        public long? MSACode { get; set; }

        /// <inheritdoc/>
        public string? AreaCode { get; set; }

        /// <inheritdoc/>
        public string? TimeZone { get; set; }

        /// <inheritdoc/>
        public long? UTC { get; set; }

        /// <inheritdoc/>
        public string? DST { get; set; }

        /// <inheritdoc/>
        public decimal? Latitude { get; set; }

        /// <inheritdoc/>
        public decimal? Longitude { get; set; }
    }
}
