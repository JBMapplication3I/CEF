// <copyright file="IStoreSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IStoreSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IStoreSearchModel
    {
        /// <summary>Gets or sets URL of the host.</summary>
        /// <value>The host URL.</value>
        string? HostUrl { get; set; }

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

        /// <summary>Gets or sets the identifier of the region.</summary>
        /// <value>The identifier of the region.</value>
        int? RegionID { get; set; }

        /// <summary>Gets or sets the identifier of the store contact region.</summary>
        /// <value>The identifier of the store contact region.</value>
        int? StoreContactRegionID { get; set; }

        /// <summary>Gets or sets the identifier of the country.</summary>
        /// <value>The identifier of the country.</value>
        int? CountryID { get; set; }

        /// <summary>Gets or sets the identifier of the store contact country.</summary>
        /// <value>The identifier of the store contact country.</value>
        int? StoreContactCountryID { get; set; }

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        string? City { get; set; }

        /// <summary>Gets or sets the store contact city.</summary>
        /// <value>The store contact city.</value>
        string? StoreContactCity { get; set; }

        /// <summary>Gets or sets the sort by membership level.</summary>
        /// <value>The sort by membership level.</value>
        bool? SortByMembershipLevel { get; set; }

        /// <summary>Gets or sets the identifier of the district.</summary>
        /// <value>The identifier of the district.</value>
        int? DistrictID { get; set; }
    }
}
