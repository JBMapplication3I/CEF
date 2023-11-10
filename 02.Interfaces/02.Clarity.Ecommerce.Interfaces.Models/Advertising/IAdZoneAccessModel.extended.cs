// <copyright file="IAdZoneAccessModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAdZoneAccessModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for ad zone access model.</summary>
    public partial interface IAdZoneAccessModel
    {
        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime EndDate { get; set; }

        /// <summary>Gets or sets the unique ad limit.</summary>
        /// <value>The unique ad limit.</value>
        int UniqueAdLimit { get; set; }

        /// <summary>Gets or sets the impression limit.</summary>
        /// <value>The impression limit.</value>
        int ImpressionLimit { get; set; }

        /// <summary>Gets or sets the click limit.</summary>
        /// <value>The click limit.</value>
        int ClickLimit { get; set; }

        /// <summary>Gets a value indicating whether the expired.</summary>
        /// <value>True if expired, false if not.</value>
        bool Expired { get; }

        /// <summary>Gets a value indicating whether the unique ad limit reached.</summary>
        /// <value>True if unique ad limit reached, false if not.</value>
        bool UniqueAdLimitReached { get; }

        /// <summary>Gets a value indicating whether the impression limit reached.</summary>
        /// <value>True if impression limit reached, false if not.</value>
        bool ImpressionLimitReached { get; }

        /// <summary>Gets a value indicating whether the click limit reached.</summary>
        /// <value>True if click limit reached, false if not.</value>
        bool ClickLimitReached { get; }

        /// <summary>Gets a value indicating whether we can add ads.</summary>
        /// <value>True if we can add ads, false if not.</value>
        bool CanAddAds { get; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the zone.</summary>
        /// <value>The identifier of the zone.</value>
        int? ZoneID { get; set; }

        /// <summary>Gets or sets the zone key.</summary>
        /// <value>The zone key.</value>
        string? ZoneKey { get; set; }

        /// <summary>Gets or sets the name of the zone.</summary>
        /// <value>The name of the zone.</value>
        string? ZoneName { get; set; }

        /// <summary>Gets or sets the zone.</summary>
        /// <value>The zone.</value>
        IZoneModel? Zone { get; set; }

        /// <summary>Gets or sets the identifier of the subscription.</summary>
        /// <value>The identifier of the subscription.</value>
        int? SubscriptionID { get; set; }

        /// <summary>Gets or sets the subscription key.</summary>
        /// <value>The subscription key.</value>
        string? SubscriptionKey { get; set; }

        /// <summary>Gets or sets the name of the subscription.</summary>
        /// <value>The name of the subscription.</value>
        string? SubscriptionName { get; set; }

        /// <summary>Gets or sets the subscription.</summary>
        /// <value>The subscription.</value>
        ISubscriptionModel? Subscription { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the ad zones.</summary>
        /// <value>The ad zones.</value>
        List<IAdZoneModel>? AdZones { get; set; }
        #endregion
    }
}
