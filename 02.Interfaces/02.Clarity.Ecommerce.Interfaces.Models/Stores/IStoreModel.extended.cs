// <copyright file="IStoreModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IStoreModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for store model.</summary>
    public partial interface IStoreModel
    {
        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets the slogan.</summary>
        /// <value>The slogan.</value>
        string? Slogan { get; set; }

        /// <summary>Gets or sets the mission statement.</summary>
        /// <value>The mission statement.</value>
        string? MissionStatement { get; set; }

        /// <summary>Gets or sets URL of the external.</summary>
        /// <value>The external URL.</value>
        string? ExternalUrl { get; set; }

        /// <summary>Gets or sets the about.</summary>
        /// <value>The about.</value>
        string? About { get; set; }

        /// <summary>Gets or sets the overview.</summary>
        /// <value>The overview.</value>
        string? Overview { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime? EndDate { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the identifier of the operating hours time zone.</summary>
        /// <value>The identifier of the operating hours time zone.</value>
        string? OperatingHoursTimeZoneId { get; set; }

        /// <summary>Gets or sets the operating hours monday start.</summary>
        /// <value>The operating hours monday start.</value>
        decimal? OperatingHoursMondayStart { get; set; }

        /// <summary>Gets or sets the operating hours monday end.</summary>
        /// <value>The operating hours monday end.</value>
        decimal? OperatingHoursMondayEnd { get; set; }

        /// <summary>Gets or sets the operating hours tuesday start.</summary>
        /// <value>The operating hours tuesday start.</value>
        decimal? OperatingHoursTuesdayStart { get; set; }

        /// <summary>Gets or sets the operating hours tuesday end.</summary>
        /// <value>The operating hours tuesday end.</value>
        decimal? OperatingHoursTuesdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours wednesday start.</summary>
        /// <value>The operating hours wednesday start.</value>
        decimal? OperatingHoursWednesdayStart { get; set; }

        /// <summary>Gets or sets the operating hours wednesday end.</summary>
        /// <value>The operating hours wednesday end.</value>
        decimal? OperatingHoursWednesdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours thursday start.</summary>
        /// <value>The operating hours thursday start.</value>
        decimal? OperatingHoursThursdayStart { get; set; }

        /// <summary>Gets or sets the operating hours thursday end.</summary>
        /// <value>The operating hours thursday end.</value>
        decimal? OperatingHoursThursdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours friday start.</summary>
        /// <value>The operating hours friday start.</value>
        decimal? OperatingHoursFridayStart { get; set; }

        /// <summary>Gets or sets the operating hours friday end.</summary>
        /// <value>The operating hours friday end.</value>
        decimal? OperatingHoursFridayEnd { get; set; }

        /// <summary>Gets or sets the operating hours saturday start.</summary>
        /// <value>The operating hours saturday start.</value>
        decimal? OperatingHoursSaturdayStart { get; set; }

        /// <summary>Gets or sets the operating hours saturday end.</summary>
        /// <value>The operating hours saturday end.</value>
        decimal? OperatingHoursSaturdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours sunday start.</summary>
        /// <value>The operating hours sunday start.</value>
        decimal? OperatingHoursSundayStart { get; set; }

        /// <summary>Gets or sets the operating hours sunday end.</summary>
        /// <value>The operating hours sunday end.</value>
        decimal? OperatingHoursSundayEnd { get; set; }

        /// <summary>Gets or sets the operating hours closed statement.</summary>
        /// <value>The operating hours closed statement.</value>
        string? OperatingHoursClosedStatement { get; set; }

        /// <summary>Gets or sets the distance.</summary>
        /// <value>The distance.</value>
        decimal? Distance { get; set; }

        /// <summary>Gets or sets the displayinstorefront.</summary>
        /// <value>The displayinstorefront.</value>
        bool? DisplayInStorefront { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int? CurrencyID { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        ICurrencyModel? Currency { get; set; }

        /// <summary>Gets or sets the identifier of the language.</summary>
        /// <value>The identifier of the language.</value>
        int? LanguageID { get; set; }

        /// <summary>Gets or sets the language.</summary>
        /// <value>The language.</value>
        ILanguageModel? Language { get; set; }

        /// <summary>Gets or sets the language key.</summary>
        /// <value>The language key.</value>
        string? LanguageKey { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the store badges.</summary>
        /// <value>The store badges.</value>
        List<IStoreBadgeModel>? StoreBadges { get; set; }

        /// <summary>Gets or sets the store contacts.</summary>
        /// <value>The store contacts.</value>
        List<IStoreContactModel>? StoreContacts { get; set; }

        /// <summary>Gets or sets the store inventory locations.</summary>
        /// <value>The store inventory locations.</value>
        List<IStoreInventoryLocationModel>? StoreInventoryLocations { get; set; }

        /// <summary>Gets or sets the store subscriptions.</summary>
        /// <value>The store subscriptions.</value>
        List<IStoreSubscriptionModel>? StoreSubscriptions { get; set; }

        /// <summary>Gets or sets the store countries.</summary>
        /// <value>The store countries.</value>
        List<IStoreCountryModel>? StoreCountries { get; set; }

        /// <summary>Gets or sets the store regions.</summary>
        /// <value>The store regions.</value>
        List<IStoreRegionModel>? StoreRegions { get; set; }

        /// <summary>Gets or sets the store districts.</summary>
        /// <value>The store districts.</value>
        List<IStoreDistrictModel>? StoreDistricts { get; set; }
        #endregion
    }
}
