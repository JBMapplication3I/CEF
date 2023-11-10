// <copyright file="IAddressSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAddressSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for address search model.</summary>
    public partial interface IAddressSearchModel
    {
        /// <summary>Gets or sets the country key.</summary>
        /// <value>The country key.</value>
        string? CountryKey { get; set; }

        /// <summary>Gets or sets the name of the country.</summary>
        /// <value>The name of the country.</value>
        string? CountryName { get; set; }

        /// <summary>Gets or sets information describing the country.</summary>
        /// <value>Information describing the country.</value>
        string? CountryDescription { get; set; }

        /// <summary>Gets or sets the region key.</summary>
        /// <value>The region key.</value>
        string? RegionKey { get; set; }

        /// <summary>Gets or sets the name of the region.</summary>
        /// <value>The name of the region.</value>
        string? RegionName { get; set; }

        /// <summary>Gets or sets information describing the region.</summary>
        /// <value>Information describing the region.</value>
        string? RegionDescription { get; set; }

        /// <summary>Gets or sets the zip code.</summary>
        /// <value>The zip code.</value>
        string? ZipCode { get; set; }

        /// <summary>Gets or sets the latitude.</summary>
        /// <value>The latitude.</value>
        double? Latitude { get; set; }

        /// <summary>Gets or sets the longitude.</summary>
        /// <value>The longitude.</value>
        double? Longitude { get; set; }

        /// <summary>Gets or sets the radius.</summary>
        /// <value>The radius.</value>
        int? Radius { get; set; }

        /// <summary>Gets or sets the units.</summary>
        /// <value>The units.</value>
        Enums.LocatorUnits? Units { get; set; }
    }
}
