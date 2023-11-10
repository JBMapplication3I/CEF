// <copyright file="IAddressModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAddressModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using DataModel;

    /// <summary>Interface for address model.</summary>
    public partial interface IAddressModel : ICloneable
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

        /// <summary>Gets or sets the region custom.</summary>
        /// <value>The region custom.</value>
        string? RegionCustom { get; set; }

        /// <summary>Gets or sets the country custom.</summary>
        /// <value>The country custom.</value>
        string? CountryCustom { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the region.</summary>
        /// <value>The identifier of the region.</value>
        int? RegionID { get; set; }

        /// <summary>Gets or sets the region key.</summary>
        /// <value>The region key.</value>
        string? RegionKey { get; set; }

        /// <summary>Gets or sets the region code.</summary>
        /// <value>The region code.</value>
        string? RegionCode { get; set; }

        /// <summary>Gets or sets the name of the region.</summary>
        /// <value>The name of the region.</value>
        string? RegionName { get; set; }

        /// <summary>Gets or sets the region.</summary>
        /// <value>The region.</value>
        IRegionModel? Region { get; set; }

        /// <summary>Gets or sets the identifier of the country.</summary>
        /// <value>The identifier of the country.</value>
        int? CountryID { get; set; }

        /// <summary>Gets or sets the country key.</summary>
        /// <value>The country key.</value>
        string? CountryKey { get; set; }

        /// <summary>Gets or sets the name of the country.</summary>
        /// <value>The name of the country.</value>
        string? CountryName { get; set; }

        /// <summary>Gets or sets the country code.</summary>
        /// <value>The country code.</value>
        string? CountryCode { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        ICountryModel? Country { get; set; }
        #endregion
    }
}
