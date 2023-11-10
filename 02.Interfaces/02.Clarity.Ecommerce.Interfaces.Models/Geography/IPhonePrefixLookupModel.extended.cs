// <copyright file="IPhonePrefixLookupModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the iPhone prefix lookup model class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for iPhone prefix lookup model.</summary>
    public partial interface IPhonePrefixLookupModel
    {
        /// <summary>Gets or sets the prefix.</summary>
        /// <value>The prefix.</value>
        string? Prefix { get; set; }

        /// <summary>Gets or sets the time zone.</summary>
        /// <value>The time zone.</value>
        string? TimeZone { get; set; }

        /// <summary>Gets or sets the name of the city.</summary>
        /// <value>The name of the city.</value>
        string? CityName { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the country.</summary>
        /// <value>The identifier of the country.</value>
        int? CountryID { get; set; }

        /// <summary>Gets or sets the country key.</summary>
        /// <value>The country key.</value>
        string? CountryKey { get; set; }

        /// <summary>Gets or sets the country code.</summary>
        /// <value>The country code.</value>
        string? CountryCode { get; set; }

        /// <summary>Gets or sets the name of the country.</summary>
        /// <value>The name of the country.</value>
        string? CountryName { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        ICountryModel? Country { get; set; }

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
        #endregion
    }
}
