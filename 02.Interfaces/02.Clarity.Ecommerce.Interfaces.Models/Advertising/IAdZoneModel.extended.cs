// <copyright file="IAdZoneModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAdZoneModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for ad zone model.</summary>
    public partial interface IAdZoneModel
    {
        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime EndDate { get; set; }

        /// <summary>Gets or sets the impression limit.</summary>
        /// <value>The impression limit.</value>
        int ImpressionLimit { get; set; }

        /// <summary>Gets or sets the click limit.</summary>
        /// <value>The click limit.</value>
        int ClickLimit { get; set; }

        /// <summary>Gets a value indicating whether the expired.</summary>
        /// <value>True if expired, false if not.</value>
        bool Expired { get; }

        /// <summary>Gets a value indicating whether the impression limit reached.</summary>
        /// <value>True if impression limit reached, false if not.</value>
        bool ImpressionLimitReached { get; }

        /// <summary>Gets a value indicating whether the click limit reached.</summary>
        /// <value>True if click limit reached, false if not.</value>
        bool ClickLimitReached { get; }

        /// <summary>Gets a value indicating whether we can show ads.</summary>
        /// <value>True if we can show ads, false if not.</value>
        bool CanShowAds { get; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the ad zone access.</summary>
        /// <value>The identifier of the ad zone access.</value>
        int? AdZoneAccessID { get; set; }

        /// <summary>Gets or sets the ad zone access key.</summary>
        /// <value>The ad zone access key.</value>
        string? AdZoneAccessKey { get; set; }

        /// <summary>Gets or sets the name of the ad zone access.</summary>
        /// <value>The name of the ad zone access.</value>
        string? AdZoneAccessName { get; set; }

        /// <summary>Gets or sets the ad zone access.</summary>
        /// <value>The ad zone access.</value>
        IAdZoneAccessModel? AdZoneAccess { get; set; }
        #endregion
    }
}
