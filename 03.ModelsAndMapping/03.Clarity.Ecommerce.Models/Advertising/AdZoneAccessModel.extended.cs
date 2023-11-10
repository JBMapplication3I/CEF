// <copyright file="AdZoneAccessModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ad ZoneAccess model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the ad zone access.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IAdZoneAccessModel"/>
    public partial class AdZoneAccessModel
    {
        #region AdZoneAccess Properties
        /// <inheritdoc/>
        public DateTime StartDate { get; set; }

        /// <inheritdoc/>
        public DateTime EndDate { get; set; }

        /// <inheritdoc/>
        public int UniqueAdLimit { get; set; }

        /// <inheritdoc/>
        public int ImpressionLimit { get; set; }

        /// <inheritdoc/>
        public int ClickLimit { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IAdZoneAccessModel.AdZones"/>
        public List<AdZoneModel>? AdZones { get; set; }

        /// <inheritdoc/>
        List<IAdZoneModel>? IAdZoneAccessModel.AdZones { get => AdZones?.ToList<IAdZoneModel>(); set => AdZones = value?.Cast<AdZoneModel>().ToList(); }

        /// <inheritdoc/>
        public int? ZoneID { get; set; }

        /// <inheritdoc/>
        public string? ZoneKey { get; set; }

        /// <inheritdoc/>
        public string? ZoneName { get; set; }

        /// <inheritdoc cref="IAdZoneAccessModel.Zone"/>
        public ZoneModel? Zone { get; set; }

        /// <inheritdoc/>
        IZoneModel? IAdZoneAccessModel.Zone { get => Zone; set => Zone = (ZoneModel?)value; }

        /// <inheritdoc/>
        public int? SubscriptionID { get; set; }

        /// <inheritdoc/>
        public string? SubscriptionKey { get; set; }

        /// <inheritdoc/>
        public string? SubscriptionName { get; set; }

        /// <inheritdoc cref="IAdZoneAccessModel.Subscription"/>
        public SubscriptionModel? Subscription { get; set; }

        /// <inheritdoc/>
        ISubscriptionModel? IAdZoneAccessModel.Subscription { get => Subscription; set => Subscription = (SubscriptionModel?)value; }
        #endregion

        #region Convenience Properties
        /// <inheritdoc/>
        public bool Expired => DateExtensions.GenDateTime >= EndDate;

        /// <inheritdoc/>
        public bool UniqueAdLimitReached => (AdZones?.Count(a => a.Active) ?? 0) >= UniqueAdLimit;

        /// <inheritdoc/>
        public bool ImpressionLimitReached => ImpressionCounter?.Value >= ImpressionLimit;

        /// <inheritdoc/>
        public bool ClickLimitReached => ClickCounter?.Value >= ClickLimit;

        /// <inheritdoc/>
        public bool CanAddAds => Active && !Expired && !UniqueAdLimitReached;
        #endregion
    }
}
