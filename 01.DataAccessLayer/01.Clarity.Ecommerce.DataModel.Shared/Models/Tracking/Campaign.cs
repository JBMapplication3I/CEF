// <copyright file="Campaign.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the campaign class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ICampaign : INameableBase, IHaveATypeBase<CampaignType>, IHaveAStatusBase<CampaignStatus>
    {
        #region Campaign Properties
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
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the created by user.</summary>
        /// <value>The identifier of the created by user.</value>
        int? CreatedByUserID { get; set; }

        /// <summary>Gets or sets the created by user.</summary>
        /// <value>The created by user.</value>
        User? CreatedByUser { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the campaign ads.</summary>
        /// <value>The campaign ads.</value>
        ICollection<CampaignAd>? CampaignAds { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Tracking", "Campaign")]
    public class Campaign : NameableBase, ICampaign
    {
        private ICollection<CampaignAd>? campaignAds;

        public Campaign()
        {
            campaignAds = new HashSet<CampaignAd>();
        }

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual CampaignType? Type { get; set; }
        #endregion

        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual CampaignStatus? Status { get; set; }
        #endregion

        #region Campaign Properties
        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ProposedStart { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ProposedEnd { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ActualStart { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ActualEnd { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? BudgetedCost { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? OtherCost { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? ExpectedRevenue { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? TotalActualCost { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? TotalCampaignActivityActualCost { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 8), DefaultValue(null)]
        public decimal? ExchangeRate { get; set; }

        /// <inheritdoc/>
        [StringLength(32), DefaultValue(null)]
        public string? CodeName { get; set; }

        /// <inheritdoc/>
        [StringLength(128), DefaultValue(null)]
        public string? PromotionCodeName { get; set; }

        /// <inheritdoc/>
        [StringLength(256), DefaultValue(null)]
        public string? Message { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? Objective { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ExpectedResponse { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? UTCConversionTimeZoneCode { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public bool? IsTemplate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(CreatedByUser)), DefaultValue(null)]
        public int? CreatedByUserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? CreatedByUser { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CampaignAd>? CampaignAds { get => campaignAds; set => campaignAds = value; }
        #endregion
    }
}
