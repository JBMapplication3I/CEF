// <copyright file="IAmFilterableByStoreSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByStoreSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by store search model.</summary>
    public interface IAmFilterableByStoreSearchModel
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        int? StoreID { get; set; }

        /// <summary>Gets or sets the store identifier include null.</summary>
        /// <value>The store identifier include null.</value>
        bool? StoreIDIncludeNull { get; set; }

        /// <summary>Gets or sets the store key.</summary>
        /// <value>The store key.</value>
        string? StoreKey { get; set; }

        /// <summary>Gets or sets the name of the store.</summary>
        /// <value>The name of the store.</value>
        string? StoreName { get; set; }

        /// <summary>Gets or sets the SEO URL of the store.</summary>
        /// <value>The SEO URL of the store.</value>
        string? StoreSeoUrl { get; set; }

        /// <summary>Gets or sets the identifier of the store country.</summary>
        /// <value>The identifier of the store country.</value>
        int? StoreCountryID { get; set; }

        /// <summary>Gets or sets the identifier of the store region.</summary>
        /// <value>The identifier of the store region.</value>
        int? StoreRegionID { get; set; }

        /// <summary>Gets or sets the store city.</summary>
        /// <value>The store city.</value>
        string? StoreCity { get; set; }

        /// <summary>Gets or sets the identifier of the store any country.</summary>
        /// <value>The identifier of the store any country.</value>
        int? StoreAnyCountryID { get; set; }

        /// <summary>Gets or sets the identifier of the store any region.</summary>
        /// <value>The identifier of the store any region.</value>
        int? StoreAnyRegionID { get; set; }

        /// <summary>Gets or sets the identifier of the store any district.</summary>
        /// <value>The identifier of the store any district.</value>
        int? StoreAnyDistrictID { get; set; }

        /// <summary>Gets or sets the store any city.</summary>
        /// <value>The store any city.</value>
        string? StoreAnyCity { get; set; }

        /// <summary>Gets or sets the zip code.</summary>
        /// <value>The zip code.</value>
        string? StoreAnyZipCode { get; set; }

        /// <summary>Gets or sets the latitude.</summary>
        /// <value>The latitude.</value>
        double? StoreAnyLatitude { get; set; }

        /// <summary>Gets or sets the longitude.</summary>
        /// <value>The longitude.</value>
        double? StoreAnyLongitude { get; set; }

        /// <summary>Gets or sets the radius.</summary>
        /// <value>The radius.</value>
        int? StoreAnyRadius { get; set; }

        /// <summary>Gets or sets the units.</summary>
        /// <value>The units.</value>
        Enums.LocatorUnits? StoreAnyUnits { get; set; }
    }
}
