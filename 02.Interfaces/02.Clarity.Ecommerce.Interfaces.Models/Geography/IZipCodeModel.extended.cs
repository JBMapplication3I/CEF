// <copyright file="IZipCodeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IZipCodeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for zip code model.</summary>
    /// <seealso cref="IBaseModel"/>
    public partial interface IZipCodeModel
    {
        /// <summary>Gets or sets the zip code value.</summary>
        /// <value>The zip code value.</value>
        string? ZipCodeValue { get; set; }

        /// <summary>Gets or sets the type of the zip.</summary>
        /// <value>The type of the zip.</value>
        string? ZipType { get; set; }

        /// <summary>Gets or sets the name of the city.</summary>
        /// <value>The name of the city.</value>
        string? CityName { get; set; }

        /// <summary>Gets or sets the type of the city.</summary>
        /// <value>The type of the city.</value>
        string? CityType { get; set; }

        /// <summary>Gets or sets the name of the county.</summary>
        /// <value>The name of the county.</value>
        string? CountyName { get; set; }

        /// <summary>Gets or sets the county fips.</summary>
        /// <value>The county fips.</value>
        long? CountyFIPS { get; set; }

        /// <summary>Gets or sets the name of the state.</summary>
        /// <value>The name of the state.</value>
        string? StateName { get; set; }

        /// <summary>Gets or sets the state abbreviation.</summary>
        /// <value>The state abbreviation.</value>
        string? StateAbbreviation { get; set; }

        /// <summary>Gets or sets the state fips.</summary>
        /// <value>The state fips.</value>
        long? StateFIPS { get; set; }

        /// <summary>Gets or sets the msa code.</summary>
        /// <value>The msa code.</value>
        long? MSACode { get; set; }

        /// <summary>Gets or sets the area code.</summary>
        /// <value>The area code.</value>
        string? AreaCode { get; set; }

        /// <summary>Gets or sets the time zone.</summary>
        /// <value>The time zone.</value>
        string? TimeZone { get; set; }

        /// <summary>Gets or sets the UTC.</summary>
        /// <value>The UTC.</value>
        long? UTC { get; set; }

        /// <summary>Gets or sets the Destination for the.</summary>
        /// <value>The destination.</value>
        string? DST { get; set; }

        /// <summary>Gets or sets the latitude.</summary>
        /// <value>The latitude.</value>
        decimal? Latitude { get; set; }

        /// <summary>Gets or sets the longitude.</summary>
        /// <value>The longitude.</value>
        decimal? Longitude { get; set; }
    }
}
