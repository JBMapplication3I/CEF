// <copyright file="PageView.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the page view class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IPageView : IHaveATypeBase<PageViewType>, IAmATrackedEventBase<PageViewStatus>
    {
        #region PageView Properties
        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        string? Title { get; set; }

        /// <summary>Gets or sets URI of the document.</summary>
        /// <value>The URI.</value>
        string? URI { get; set; }

        /// <summary>Gets or sets the Date/Time of the viewed on.</summary>
        /// <value>The viewed on.</value>
        DateTime? ViewedOn { get; set; }

        /// <summary>Gets or sets the visit key.</summary>
        /// <value>The visit key.</value>
        string? VisitKey { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the category.</summary>
        /// <value>The identifier of the category.</value>
        int? CategoryID { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        Category? Category { get; set; }

        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int? ProductID { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        Product? Product { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the page view events.</summary>
        /// <value>The page view events.</value>
        ICollection<PageViewEvent>? PageViewEvents { get; set; }
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

    [SqlSchema("Tracking", "PageView")]
    public class PageView : NameableBase, IPageView
    {
        private ICollection<PageViewEvent>? pageViewEvents;

        public PageView()
        {
            pageViewEvents = new HashSet<PageViewEvent>();
        }

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual PageViewType? Type { get; set; }
        #endregion

        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual PageViewStatus? Status { get; set; }
        #endregion

        #region IAmALitelyTrackedEvent Properties
        /// <inheritdoc/>
        [StringLength(20), StringIsUnicode(false), DefaultValue(null)]
        public string? IPAddress { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? Score { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Address)), DefaultValue(null)]
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Address? Address { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(IPOrganization)), DefaultValue(null)]
        public int? IPOrganizationID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual IPOrganization? IPOrganization { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(null)]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }
        #endregion

        #region IAmATrackedEvent
        /// <inheritdoc/>
        [DefaultValue(null)]
        public bool? DidBounce { get; set; }

        /// <inheritdoc/>
        [StringLength(20), StringIsUnicode(false), DefaultValue(null)]
        public string? OperatingSystem { get; set; }

        /// <inheritdoc/>
        [StringLength(10), StringIsUnicode(false), DefaultValue(null)]
        public string? Browser { get; set; }

        /// <inheritdoc/>
        [StringLength(50), StringIsUnicode(false), DefaultValue(null)]
        public string? Language { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public bool? ContainsSocialProfile { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? Delta { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? Duration { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? StartedOn { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? EndedOn { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? Time { get; set; }

        /// <inheritdoc/>
        [StringLength(2000), StringIsUnicode(false), DefaultValue(null)]
        public string? EntryPage { get; set; }

        /// <inheritdoc/>
        [StringLength(2000), StringIsUnicode(false), DefaultValue(null)]
        public string? ExitPage { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public bool? IsFirstTrigger { get; set; }

        /// <inheritdoc/>
        [StringLength(10), StringIsUnicode(false), DefaultValue(null)]
        public string? Flash { get; set; }

        /// <inheritdoc/>
        [StringLength(100), DefaultValue(null)]
        public string? Keywords { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? PartitionKey { get; set; }

        /// <inheritdoc/>
        [StringLength(2000), StringIsUnicode(false), DefaultValue(null)]
        public string? Referrer { get; set; }

        /// <inheritdoc/>
        [StringLength(300), StringIsUnicode(false), DefaultValue(null)]
        public string? ReferringHost { get; set; }

        /// <inheritdoc/>
        [StringLength(50), DefaultValue(null)]
        public string? RowKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? Source { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? TotalTriggers { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Campaign)), DefaultValue(null)]
        public int? CampaignID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual Campaign? Campaign { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Contact)), DefaultValue(null)]
        public int? ContactID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual Contact? Contact { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SiteDomain)), DefaultValue(null)]
        public int? SiteDomainID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual SiteDomain? SiteDomain { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Visitor)), DefaultValue(null)]
        public int? VisitorID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual Visitor? Visitor { get; set; }
        #endregion

        #region PageView Properties
        /// <inheritdoc/>
        [StringLength(500), StringIsUnicode(false), DefaultValue(null)]
        public string? Title { get; set; }

        /// <inheritdoc/>
        [StringLength(2000), StringIsUnicode(false), DefaultValue(null)]
        public string? URI { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? ViewedOn { get; set; }

        /// <inheritdoc/>
        [StringLength(50), DefaultValue(null)]
        public string? VisitKey { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Category)), DefaultValue(null)]
        public int? CategoryID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Category? Category { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Product)), DefaultValue(null)]
        public int? ProductID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Product? Product { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DefaultValue(null), JsonIgnore]
        public virtual ICollection<PageViewEvent>? PageViewEvents { get => pageViewEvents; set => pageViewEvents = value; }
        #endregion
    }
}
