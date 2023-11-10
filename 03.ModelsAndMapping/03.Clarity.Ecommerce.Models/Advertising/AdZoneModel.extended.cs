// <copyright file="AdZoneModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the AdZoneModel class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the ad zone.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IAdZoneModel"/>
    public partial class AdZoneModel
    {
        /// <inheritdoc/>
        public DateTime StartDate { get; set; }

        /// <inheritdoc/>
        public DateTime EndDate { get; set; }

        /// <inheritdoc/>
        public int ImpressionLimit { get; set; }

        /// <inheritdoc/>
        public int ClickLimit { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int? AdZoneAccessID { get; set; }

        /// <inheritdoc/>
        public string? AdZoneAccessKey { get; set; }

        /// <inheritdoc/>
        public string? AdZoneAccessName { get; set; }

        /// <inheritdoc cref="IAdZoneModel.AdZoneAccess"/>
        public AdZoneAccessModel? AdZoneAccess { get; set; }

        /// <inheritdoc/>
        IAdZoneAccessModel? IAdZoneModel.AdZoneAccess { get => AdZoneAccess; set => AdZoneAccess = (AdZoneAccessModel?)value; }
        #endregion

        #region Convenience Properties
        /// <inheritdoc/>
        public bool Expired => DateExtensions.GenDateTime >= EndDate;

        /// <inheritdoc/>
        public bool ImpressionLimitReached => ImpressionCounter?.Value >= ImpressionLimit;

        /// <inheritdoc/>
        public bool ClickLimitReached => ClickCounter?.Value >= ClickLimit;

        /// <inheritdoc/>
        public bool CanShowAds => Active && !Expired && !ImpressionLimitReached && !ClickLimitReached;
        #endregion
    }
}
