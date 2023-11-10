// <copyright file="Ad.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ad class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IAd
        : INameableBase,
            IHaveATypeBase<AdType>,
            IHaveAStatusBase<AdStatus>,
            IAmFilterableByBrand<AdBrand>,
            IAmFilterableByFranchise<AdFranchise>,
            IAmFilterableByStore<AdStore>,
            IAmFilterableByAccount<AdAccount>,
            IHaveAdCounters,
            IHaveImagesBase<Ad, AdImage, AdImageType>
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
        /// <summary>Gets or sets the ad zones.</summary>
        /// <value>The ad zones.</value>
        ICollection<AdZone>? AdZones { get; set; }

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

    [SqlSchema("Advertising", "Ad")]
    public class Ad : NameableBase, IAd
    {
        private ICollection<AdImage>? images;
        private ICollection<AdZone>? adZones;
        private ICollection<AdBrand>? brands;
        private ICollection<AdFranchise>? franchises;
        private ICollection<AdStore>? stores;
        private ICollection<AdAccount>? accounts;
        private ICollection<CampaignAd>? campaignAds;

        public Ad()
        {
            // IHaveImagesBase
            images = new HashSet<AdImage>();
            // IAmFilterableByBrand
            brands = new HashSet<AdBrand>();
            // IAmFilterableByFranchise
            franchises = new HashSet<AdFranchise>();
            // IAmFilterableByStore
            stores = new HashSet<AdStore>();
            // IAmFilterableByAccount
            accounts = new HashSet<AdAccount>();
            // Ad Properties
            adZones = new HashSet<AdZone>();
            campaignAds = new HashSet<CampaignAd>();
        }

        #region IHaveATypeBase
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual AdType? Type { get; set; }
        #endregion

        #region IHaveAStatusBase
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual AdStatus? Status { get; set; }
        #endregion

        #region IHaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<AdImage>? Images { get => images; set => images = value; }
        #endregion

        #region IAmFilterableByBrand Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<AdBrand>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByFranchise Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<AdFranchise>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByStore Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<AdStore>? Stores { get => stores; set => stores = value; }
        #endregion

        #region IAmFilterableByAccount Properties
        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<AdAccount>? Accounts { get => accounts; set => accounts = value; }
        #endregion

        #region Ad Properties
        /// <inheritdoc/>
        [Required, StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? TargetURL { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DefaultValue(null)]
        public string? Caption { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime StartDate { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime EndDate { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime ExpirationDate { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal Weight { get; set; }
        #endregion

        #region IHaveAdCounters
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ImpressionCounter)), DefaultValue(null)]
        public int? ImpressionCounterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Counter? ImpressionCounter { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ClickCounter)), DefaultValue(null)]
        public int? ClickCounterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Counter? ClickCounter { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<AdZone>? AdZones { get => adZones; set => adZones = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<CampaignAd>? CampaignAds { get => campaignAds; set => campaignAds = value; }
        #endregion
    }
}
