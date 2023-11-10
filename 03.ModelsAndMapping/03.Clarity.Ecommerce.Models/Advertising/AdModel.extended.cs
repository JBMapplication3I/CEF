// <copyright file="AdModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ad model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the ad.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IAdModel"/>
    public partial class AdModel
    {
        #region Ad Properties
        /// <inheritdoc/>
        public string? TargetURL { get; set; }

        /// <inheritdoc/>
        public string? Caption { get; set; }

        /// <inheritdoc/>
        public DateTime StartDate { get; set; }

        /// <inheritdoc/>
        public DateTime EndDate { get; set; }

        /// <inheritdoc/>
        public DateTime ExpirationDate { get; set; }

        /// <inheritdoc/>
        public decimal Weight { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IAdModel.AdAccounts"/>
        public List<AdAccountModel>? AdAccounts { get; set; }

        /// <inheritdoc/>
        List<IAdAccountModel>? IAdModel.AdAccounts { get => AdAccounts?.ToList<IAdAccountModel>(); set => AdAccounts = value?.Cast<AdAccountModel>().ToList(); }

        /// <inheritdoc cref="IAdModel.AdStores"/>
        public List<AdStoreModel>? AdStores { get; set; }

        /// <inheritdoc/>
        List<IAdStoreModel>? IAdModel.AdStores { get => AdStores?.ToList<IAdStoreModel>(); set => AdStores = value?.Cast<AdStoreModel>().ToList(); }

        /// <inheritdoc cref="IAdModel.AdZones"/>
        public List<AdZoneModel>? AdZones { get; set; }

        /// <inheritdoc/>
        List<IAdZoneModel>? IAdModel.AdZones { get => AdZones?.ToList<IAdZoneModel>(); set => AdZones = value?.Cast<AdZoneModel>().ToList(); }

        /// <inheritdoc cref="IAdModel.CampaignAds"/>
        public List<CampaignAdModel>? CampaignAds { get; set; }

        /// <inheritdoc/>
        List<ICampaignAdModel>? IAdModel.CampaignAds { get => CampaignAds?.ToList<ICampaignAdModel>(); set => CampaignAds = value?.Cast<CampaignAdModel>().ToList(); }
        #endregion
    }
}
