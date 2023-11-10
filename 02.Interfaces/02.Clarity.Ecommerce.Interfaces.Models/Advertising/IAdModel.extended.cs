// <copyright file="IAdModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAdModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for ad model.</summary>
    public partial interface IAdModel
    {
        #region Ad Properties
        /// <summary>Gets or sets URL of the target.</summary>
        /// <value>The target URL.</value>
        string? TargetURL { get; set; }

        /// <summary>Gets or sets the caption.</summary>
        /// <value>The caption.</value>
        string? Caption { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime EndDate { get; set; }

        /// <summary>Gets or sets the expiration date.</summary>
        /// <value>The expiration date.</value>
        DateTime ExpirationDate { get; set; }

        /// <summary>Gets or sets the weight.</summary>
        /// <value>The weight.</value>
        decimal Weight { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the ad accounts.</summary>
        /// <value>The ad accounts.</value>
        List<IAdAccountModel>? AdAccounts { get; set; }

        /// <summary>Gets or sets the ad stores.</summary>
        /// <value>The ad stores.</value>
        List<IAdStoreModel>? AdStores { get; set; }

        /// <summary>Gets or sets the ad zones.</summary>
        /// <value>The ad zones.</value>
        List<IAdZoneModel>? AdZones { get; set; }

        /// <summary>Gets or sets the campaign ads.</summary>
        /// <value>The campaign ads.</value>
        List<ICampaignAdModel>? CampaignAds { get; set; }
        #endregion
    }
}
