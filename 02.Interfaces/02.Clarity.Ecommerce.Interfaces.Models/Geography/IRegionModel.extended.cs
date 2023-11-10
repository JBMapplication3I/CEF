// <copyright file="IRegionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRegionModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for region model.</summary>
    public partial interface IRegionModel
    {
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        string? Code { get; set; }

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

        /// <summary>Gets or sets the ISO 31661.</summary>
        /// <value>The ISO 31661.</value>
        string? ISO31661 { get; set; }

        /// <summary>Gets or sets the ISO 31662.</summary>
        /// <value>The ISO 31662.</value>
        string? ISO31662 { get; set; }

        /// <summary>Gets or sets the ISO 3166 alpha 2.</summary>
        /// <value>The ISO 3166 alpha 2.</value>
        string? ISO3166Alpha2 { get; set; }
        #endregion
    }
}
