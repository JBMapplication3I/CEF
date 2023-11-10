// <copyright file="StoreIndexableModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store indexable model class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System.Collections.Generic;

    /// <summary>A data Model for the store indexable.</summary>
    /// <seealso cref="IndexableModelBase"/>
    public class StoreIndexableModel : IndexableModelBase
    {
        #region SuggestedBy's
        /// <summary>Gets or sets the suggested by badges.</summary>
        /// <value>The suggested by badges.</value>
        public object? SuggestedByBadges { get; set; }
        #endregion

        /// <summary>Gets or sets the badges.</summary>
        /// <value>The badges.</value>
        public List<IndexableBadgeFilter>? Badges { get; set; }

        #region Store Properties
        /// <summary>Gets or sets the slogan.</summary>
        /// <value>The slogan.</value>
        public string? Slogan { get; set; }

        /// <summary>Gets or sets the mission statement.</summary>
        /// <value>The mission statement.</value>
        public string? MissionStatement { get; set; }

        /// <summary>Gets or sets URL of the external.</summary>
        /// <value>The external URL.</value>
        public string? ExternalUrl { get; set; }

        /// <summary>Gets or sets the about.</summary>
        /// <value>The about.</value>
        public string? About { get; set; }

        /// <summary>Gets or sets the overview.</summary>
        /// <value>The overview.</value>
        public string? Overview { get; set; }

        /// <summary>Gets or sets the identifier of the operating hours time zone.</summary>
        /// <value>The identifier of the operating hours time zone.</value>
        public string? OperatingHoursTimeZoneId { get; set; }

        /// <summary>Gets or sets the operating hours monday start.</summary>
        /// <value>The operating hours monday start.</value>
        public decimal? OperatingHoursMondayStart { get; set; }

        /// <summary>Gets or sets the operating hours monday end.</summary>
        /// <value>The operating hours monday end.</value>
        public decimal? OperatingHoursMondayEnd { get; set; }

        /// <summary>Gets or sets the operating hours tuesday start.</summary>
        /// <value>The operating hours tuesday start.</value>
        public decimal? OperatingHoursTuesdayStart { get; set; }

        /// <summary>Gets or sets the operating hours tuesday end.</summary>
        /// <value>The operating hours tuesday end.</value>
        public decimal? OperatingHoursTuesdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours wednesday start.</summary>
        /// <value>The operating hours wednesday start.</value>
        public decimal? OperatingHoursWednesdayStart { get; set; }

        /// <summary>Gets or sets the operating hours wednesday end.</summary>
        /// <value>The operating hours wednesday end.</value>
        public decimal? OperatingHoursWednesdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours thursday start.</summary>
        /// <value>The operating hours thursday start.</value>
        public decimal? OperatingHoursThursdayStart { get; set; }

        /// <summary>Gets or sets the operating hours thursday end.</summary>
        /// <value>The operating hours thursday end.</value>
        public decimal? OperatingHoursThursdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours friday start.</summary>
        /// <value>The operating hours friday start.</value>
        public decimal? OperatingHoursFridayStart { get; set; }

        /// <summary>Gets or sets the operating hours friday end.</summary>
        /// <value>The operating hours friday end.</value>
        public decimal? OperatingHoursFridayEnd { get; set; }

        /// <summary>Gets or sets the operating hours saturday start.</summary>
        /// <value>The operating hours saturday start.</value>
        public decimal? OperatingHoursSaturdayStart { get; set; }

        /// <summary>Gets or sets the operating hours saturday end.</summary>
        /// <value>The operating hours saturday end.</value>
        public decimal? OperatingHoursSaturdayEnd { get; set; }

        /// <summary>Gets or sets the operating hours sunday start.</summary>
        /// <value>The operating hours sunday start.</value>
        public decimal? OperatingHoursSundayStart { get; set; }

        /// <summary>Gets or sets the operating hours sunday end.</summary>
        /// <value>The operating hours sunday end.</value>
        public decimal? OperatingHoursSundayEnd { get; set; }

        /// <summary>Gets or sets the operating hours closed statement.</summary>
        /// <value>The operating hours closed statement.</value>
        public string? OperatingHoursClosedStatement { get; set; }

        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        public int? CurrencyID { get; set; }

        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        public string? CurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the currency.</summary>
        /// <value>The name of the currency.</value>
        public string? CurrencyName { get; set; }

        /// <summary>Gets or sets the identifier of the language.</summary>
        /// <value>The identifier of the language.</value>
        public int? LanguageID { get; set; }

        /// <summary>Gets or sets the language key.</summary>
        /// <value>The language key.</value>
        public string? LanguageKey { get; set; }

        /// <summary>Gets or sets the name of the language.</summary>
        /// <value>The name of the language.</value>
        public string? LanguageName { get; set; }
        #endregion
    }
}
