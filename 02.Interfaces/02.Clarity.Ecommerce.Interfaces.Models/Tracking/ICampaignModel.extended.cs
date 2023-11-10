﻿// <copyright file="ICampaignModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICampaignModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for campaign model.</summary>
    public partial interface ICampaignModel
    {
        /// <summary>Gets or sets the Date/Time of the proposed start.</summary>
        /// <value>The proposed start.</value>
        DateTime? ProposedStart { get; set; }

        /// <summary>Gets or sets the Date/Time of the proposed end.</summary>
        /// <value>The proposed end.</value>
        DateTime? ProposedEnd { get; set; }

        /// <summary>Gets or sets the Date/Time of the actual start.</summary>
        /// <value>The actual start.</value>
        DateTime? ActualStart { get; set; }

        /// <summary>Gets or sets the Date/Time of the actual end.</summary>
        /// <value>The actual end.</value>
        DateTime? ActualEnd { get; set; }

        /// <summary>Gets or sets the budgeted cost.</summary>
        /// <value>The budgeted cost.</value>
        decimal? BudgetedCost { get; set; }

        /// <summary>Gets or sets the other cost.</summary>
        /// <value>The other cost.</value>
        decimal? OtherCost { get; set; }

        /// <summary>Gets or sets the expected revenue.</summary>
        /// <value>The expected revenue.</value>
        decimal? ExpectedRevenue { get; set; }

        /// <summary>Gets or sets the total number of actual cost.</summary>
        /// <value>The total number of actual cost.</value>
        decimal? TotalActualCost { get; set; }

        /// <summary>Gets or sets the total number of campaign activity actual cost.</summary>
        /// <value>The total number of campaign activity actual cost.</value>
        decimal? TotalCampaignActivityActualCost { get; set; }

        /// <summary>Gets or sets the exchange rate.</summary>
        /// <value>The exchange rate.</value>
        decimal? ExchangeRate { get; set; }

        /// <summary>Gets or sets the name of the code.</summary>
        /// <value>The name of the code.</value>
        string? CodeName { get; set; }

        /// <summary>Gets or sets the name of the promotion code.</summary>
        /// <value>The name of the promotion code.</value>
        string? PromotionCodeName { get; set; }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        string? Message { get; set; }

        /// <summary>Gets or sets the objective.</summary>
        /// <value>The objective.</value>
        string? Objective { get; set; }

        /// <summary>Gets or sets the expected response.</summary>
        /// <value>The expected response.</value>
        int? ExpectedResponse { get; set; }

        /// <summary>Gets or sets the UTC conversion time zone code.</summary>
        /// <value>The UTC conversion time zone code.</value>
        int? UTCConversionTimeZoneCode { get; set; }

        /// <summary>Gets or sets the is template.</summary>
        /// <value>The is template.</value>
        bool? IsTemplate { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the created by user.</summary>
        /// <value>The identifier of the created by user.</value>
        int? CreatedByUserID { get; set; }

        /// <summary>Gets or sets the created by user key.</summary>
        /// <value>The created by user key.</value>
        string? CreatedByUserKey { get; set; }

        /// <summary>Gets or sets the created by user.</summary>
        /// <value>The created by user.</value>
        IUserModel? CreatedByUser { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the campaign ads.</summary>
        /// <value>The campaign ads.</value>
        List<ICampaignAdModel>? CampaignAds { get; set; }
        #endregion
    }
}
