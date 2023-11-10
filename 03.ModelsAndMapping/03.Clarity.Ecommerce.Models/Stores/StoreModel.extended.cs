// <copyright file="StoreModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using JetBrains.Annotations;

    /// <summary>A data Model for the store.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IStoreModel"/>
    [PublicAPI]
    public partial class StoreModel
    {
        #region Store Properties
        /// <inheritdoc/>
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        public string? Slogan { get; set; }

        /// <inheritdoc/>
        public string? MissionStatement { get; set; }

        /// <inheritdoc/>
        public string? ExternalUrl { get; set; }

        /// <inheritdoc/>
        public string? About { get; set; }

        /// <inheritdoc/>
        public string? Overview { get; set; }

        /// <inheritdoc/>
        public string? OperatingHoursTimeZoneId { get; set; }

        /// <inheritdoc/>
        public DateTime? EndDate { get; set; }

        /// <inheritdoc/>
        public DateTime? StartDate { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursMondayStart { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursMondayEnd { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursTuesdayStart { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursTuesdayEnd { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursWednesdayStart { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursWednesdayEnd { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursThursdayStart { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursThursdayEnd { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursFridayStart { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursFridayEnd { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursSaturdayStart { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursSaturdayEnd { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursSundayStart { get; set; }

        /// <inheritdoc/>
        public decimal? OperatingHoursSundayEnd { get; set; }

        /// <inheritdoc/>
        public string? OperatingHoursClosedStatement { get; set; }

        /// <inheritdoc/>
        public decimal? Distance { get; set; }

        /// <inheritdoc/>
        public bool? DisplayInStorefront { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? CurrencyID { get; set; }

        /// <inheritdoc cref="IStoreModel.Currency"/>
        public CurrencyModel? Currency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IStoreModel.Currency { get => Currency; set => Currency = (CurrencyModel?)value; }

        /// <inheritdoc/>
        public int? LanguageID { get; set; }

        /// <inheritdoc/>
        public string? LanguageKey { get; set; }

        /// <inheritdoc cref="IStoreModel.Language"/>
        public LanguageModel? Language { get; set; }

        /// <inheritdoc/>
        ILanguageModel? IStoreModel.Language { get => Language; set => Language = (LanguageModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IStoreModel.StoreBadges"/>
        public List<StoreBadgeModel>? StoreBadges { get; set; }

        /// <inheritdoc/>
        List<IStoreBadgeModel>? IStoreModel.StoreBadges { get => StoreBadges?.Cast<IStoreBadgeModel>().ToList(); set => StoreBadges = value?.Cast<StoreBadgeModel>().ToList(); }

        /// <inheritdoc cref="IStoreModel.StoreContacts"/>
        public List<StoreContactModel>? StoreContacts { get; set; }

        /// <inheritdoc/>
        List<IStoreContactModel>? IStoreModel.StoreContacts { get => StoreContacts?.Cast<IStoreContactModel>().ToList(); set => StoreContacts = value?.Cast<StoreContactModel>().ToList(); }

        /// <inheritdoc cref="IStoreModel.StoreInventoryLocations"/>
        public List<StoreInventoryLocationModel>? StoreInventoryLocations { get; set; }

        /// <inheritdoc/>
        List<IStoreInventoryLocationModel>? IStoreModel.StoreInventoryLocations { get => StoreInventoryLocations?.Cast<IStoreInventoryLocationModel>().ToList(); set => StoreInventoryLocations = value?.Cast<StoreInventoryLocationModel>().ToList(); }

        /// <inheritdoc cref="IStoreModel.StoreSubscriptions"/>
        public List<StoreSubscriptionModel>? StoreSubscriptions { get; set; }

        /// <inheritdoc/>
        List<IStoreSubscriptionModel>? IStoreModel.StoreSubscriptions { get => StoreSubscriptions?.Cast<IStoreSubscriptionModel>().ToList(); set => StoreSubscriptions = value?.Cast<StoreSubscriptionModel>().ToList(); }

        /// <inheritdoc cref="IStoreModel.StoreCountries"/>
        public List<StoreCountryModel>? StoreCountries { get; set; }

        /// <inheritdoc/>
        List<IStoreCountryModel>? IStoreModel.StoreCountries { get => StoreCountries?.Cast<IStoreCountryModel>().ToList(); set => StoreCountries = value?.Cast<StoreCountryModel>().ToList(); }

        /// <inheritdoc cref="IStoreModel.StoreRegions"/>
        public List<StoreRegionModel>? StoreRegions { get; set; }

        /// <inheritdoc/>
        List<IStoreRegionModel>? IStoreModel.StoreRegions { get => StoreRegions?.Cast<IStoreRegionModel>().ToList(); set => StoreRegions = value?.Cast<StoreRegionModel>().ToList(); }

        /// <inheritdoc cref="IStoreModel.StoreDistricts"/>
        public List<StoreDistrictModel>? StoreDistricts { get; set; }

        /// <inheritdoc/>
        List<IStoreDistrictModel>? IStoreModel.StoreDistricts { get => StoreDistricts?.Cast<IStoreDistrictModel>().ToList(); set => StoreDistricts = value?.Cast<StoreDistrictModel>().ToList(); }
        #endregion
    }
}
