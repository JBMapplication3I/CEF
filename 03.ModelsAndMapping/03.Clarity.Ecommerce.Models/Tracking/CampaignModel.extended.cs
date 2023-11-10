// <copyright file="CampaignModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the campaign model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using Newtonsoft.Json;

    /// <summary>A data Model for the campaign.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="ICampaignModel"/>
    public partial class CampaignModel
    {
        #region Campaign Properties
        /// <inheritdoc/>
        public DateTime? ProposedStart { get; set; }

        /// <inheritdoc/>
        public DateTime? ProposedEnd { get; set; }

        /// <inheritdoc/>
        public DateTime? ActualStart { get; set; }

        /// <inheritdoc/>
        public DateTime? ActualEnd { get; set; }

        /// <inheritdoc/>
        public decimal? BudgetedCost { get; set; }

        /// <inheritdoc/>
        public decimal? OtherCost { get; set; }

        /// <inheritdoc/>
        public decimal? ExpectedRevenue { get; set; }

        /// <inheritdoc/>
        public decimal? TotalActualCost { get; set; }

        /// <inheritdoc/>
        public decimal? TotalCampaignActivityActualCost { get; set; }

        /// <inheritdoc/>
        public decimal? ExchangeRate { get; set; }

        /// <inheritdoc/>
        public string? CodeName { get; set; }

        /// <inheritdoc/>
        public string? PromotionCodeName { get; set; }

        /// <inheritdoc/>
        public string? Message { get; set; }

        /// <inheritdoc/>
        public string? Objective { get; set; }

        /// <inheritdoc/>
        public int? ExpectedResponse { get; set; }

        /// <inheritdoc/>
        public int? UTCConversionTimeZoneCode { get; set; }

        /// <inheritdoc/>
        public bool? IsTemplate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? CreatedByUserID { get; set; }

        /// <inheritdoc/>
        public string? CreatedByUserKey { get; set; }

        /// <inheritdoc cref="ICampaignModel.CreatedByUser"/>
        [JsonIgnore]
        public UserModel? CreatedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? ICampaignModel.CreatedByUser { get => CreatedByUser; set => CreatedByUser = (UserModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ICampaignModel.CampaignAds"/>
        public List<CampaignAdModel>? CampaignAds { get; set; }

        /// <inheritdoc/>
        List<ICampaignAdModel>? ICampaignModel.CampaignAds { get => CampaignAds?.ToList<ICampaignAdModel>(); set => CampaignAds = value?.Cast<CampaignAdModel>().ToList(); }
        #endregion
    }
}
