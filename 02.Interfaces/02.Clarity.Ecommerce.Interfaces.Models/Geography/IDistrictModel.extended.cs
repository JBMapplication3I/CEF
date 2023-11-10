// <copyright file="IDistrictModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDistrictModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for district model.</summary>
    public partial interface IDistrictModel
    {
        #region Properties
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        string? Code { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the region.</summary>
        /// <value>The identifier of the region.</value>
        int? RegionID { get; set; }

        /// <summary>Gets or sets the region.</summary>
        /// <value>The region.</value>
        IRegionModel? Region { get; set; }

        /// <summary>Gets or sets the region key.</summary>
        /// <value>The region key.</value>
        string? RegionKey { get; set; }

        /// <summary>Gets or sets the name of the region.</summary>
        /// <value>The name of the region.</value>
        string? RegionName { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the country.</summary>
        /// <value>The identifier of the country.</value>
        int CountryID { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        ICountryModel? Country { get; set; }

        /// <summary>Gets or sets the country key.</summary>
        /// <value>The country key.</value>
        string? CountryKey { get; set; }

        /// <summary>Gets or sets the name of the country.</summary>
        /// <value>The name of the country.</value>
        string? CountryName { get; set; }
        #endregion
    }
}
