// <copyright file="FranchiseModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the franchise.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IFranchiseModel"/>
    public partial class FranchiseModel
    {
        #region Associated Objects
        /// <inheritdoc cref="IFranchiseModel.FranchiseRegions"/>
        [ApiMember(Name = nameof(FranchiseRegions), DataType = "List<FranchiseRegionModel>", ParameterType = "body", IsRequired = false,
            Description = "Region Book for the Franchise")]
        public List<FranchiseRegionModel>? FranchiseRegions { get; set; }

        /// <inheritdoc/>
        List<IFranchiseRegionModel>? IFranchiseModel.FranchiseRegions { get => FranchiseRegions?.ToList<IFranchiseRegionModel>(); set => FranchiseRegions = value?.Cast<FranchiseRegionModel>().ToList(); }

        /// <inheritdoc cref="IFranchiseModel.FranchiseCountries"/>
        [ApiMember(Name = nameof(FranchiseCountries), DataType = "List<FranchiseRegionModel>", ParameterType = "body", IsRequired = false,
            Description = "Region Book for the Franchise")]
        public List<FranchiseCountryModel>? FranchiseCountries { get; set; }

        /// <inheritdoc/>
        List<IFranchiseCountryModel>? IFranchiseModel.FranchiseCountries { get => FranchiseCountries?.ToList<IFranchiseCountryModel>(); set => FranchiseCountries = value?.Cast<FranchiseCountryModel>().ToList(); }

        /// <inheritdoc cref="IFranchiseModel.FranchiseDistricts"/>
        [ApiMember(Name = nameof(FranchiseDistricts), DataType = "List<FranchiseDistrictModel>", ParameterType = "body", IsRequired = false,
            Description = "Region Book for the Franchise")]
        public List<FranchiseDistrictModel>? FranchiseDistricts { get; set; }

        /// <inheritdoc/>
        List<IFranchiseDistrictModel>? IFranchiseModel.FranchiseDistricts { get => FranchiseDistricts?.ToList<IFranchiseDistrictModel>(); set => FranchiseDistricts = value?.Cast<FranchiseDistrictModel>().ToList(); }

        /// <inheritdoc cref="IFranchiseModel.FranchiseCurrencies"/>
        [ApiMember(Name = nameof(FranchiseCurrencies), DataType = "List<FranchiseCurrencyModel>", ParameterType = "body", IsRequired = false,
            Description = "Region Book for the Franchise")]
        public List<FranchiseCurrencyModel>? FranchiseCurrencies { get; set; }

        /// <inheritdoc/>
        List<IFranchiseCurrencyModel>? IFranchiseModel.FranchiseCurrencies { get => FranchiseCurrencies?.ToList<IFranchiseCurrencyModel>(); set => FranchiseCurrencies = value?.Cast<FranchiseCurrencyModel>().ToList(); }

        /// <inheritdoc cref="IFranchiseModel.FranchiseInventoryLocations"/>
        [ApiMember(Name = nameof(FranchiseInventoryLocations), DataType = "List<FranchiseInventoryLocationModel>", ParameterType = "body", IsRequired = false,
            Description = "Region Book for the Franchise")]
        public List<FranchiseInventoryLocationModel>? FranchiseInventoryLocations { get; set; }

        /// <inheritdoc/>
        List<IFranchiseInventoryLocationModel>? IFranchiseModel.FranchiseInventoryLocations { get => FranchiseInventoryLocations?.ToList<IFranchiseInventoryLocationModel>(); set => FranchiseInventoryLocations = value?.Cast<FranchiseInventoryLocationModel>().ToList(); }

        /// <inheritdoc cref="IFranchiseModel.FranchiseLanguages"/>
        [ApiMember(Name = nameof(FranchiseLanguages), DataType = "List<FranchiseLanguageModel>", ParameterType = "body", IsRequired = false,
            Description = "Region Book for the Franchise")]
        public List<FranchiseLanguageModel>? FranchiseLanguages { get; set; }

        /// <inheritdoc/>
        List<IFranchiseLanguageModel>? IFranchiseModel.FranchiseLanguages { get => FranchiseLanguages?.ToList<IFranchiseLanguageModel>(); set => FranchiseLanguages = value?.Cast<FranchiseLanguageModel>().ToList(); }

        /// <inheritdoc cref="IFranchiseModel.FranchiseSiteDomains"/>
        [ApiMember(Name = nameof(FranchiseSiteDomains), DataType = "List<FranchiseSiteDomainModel>", ParameterType = "body", IsRequired = false,
            Description = "Region Book for the Franchise")]
        public List<FranchiseSiteDomainModel>? FranchiseSiteDomains { get; set; }

        /// <inheritdoc/>
        List<IFranchiseSiteDomainModel>? IFranchiseModel.FranchiseSiteDomains { get => FranchiseSiteDomains?.ToList<IFranchiseSiteDomainModel>(); set => FranchiseSiteDomains = value?.Cast<FranchiseSiteDomainModel>().ToList(); }
        #endregion
    }
}
