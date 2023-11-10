// <copyright file="IFranchiseModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IFranchiseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for franchise model.</summary>
    public partial interface IFranchiseModel
    {
        #region Associated Objects
        /// <summary>Gets or sets the franchise regions.</summary>
        /// <value>The franchise regions.</value>
        List<IFranchiseRegionModel>? FranchiseRegions { get; set; }

        /// <summary>Gets or sets the franchise countries.</summary>
        /// <value>The franchise countries.</value>
        List<IFranchiseCountryModel>? FranchiseCountries { get; set; }

        /// <summary>Gets or sets the franchise districts.</summary>
        /// <value>The franchise districts.</value>
        List<IFranchiseDistrictModel>? FranchiseDistricts { get; set; }

        /// <summary>Gets or sets the franchise currencies.</summary>
        /// <value>The franchise currencies.</value>
        List<IFranchiseCurrencyModel>? FranchiseCurrencies { get; set; }

        /// <summary>Gets or sets the franchise inventory locations.</summary>
        /// <value>The franchise inventory locations.</value>
        List<IFranchiseInventoryLocationModel>? FranchiseInventoryLocations { get; set; }

        /// <summary>Gets or sets the franchise languages.</summary>
        /// <value>The franchise languages.</value>
        List<IFranchiseLanguageModel>? FranchiseLanguages { get; set; }

        /// <summary>Gets or sets the franchise site domains.</summary>
        /// <value>The franchise site domains.</value>
        List<IFranchiseSiteDomainModel>? FranchiseSiteDomains { get; set; }
        #endregion
    }
}
