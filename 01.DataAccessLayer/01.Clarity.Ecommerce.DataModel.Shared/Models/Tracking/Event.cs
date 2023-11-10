// <copyright file="Event.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the event class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IEvent : IHaveATypeBase<EventType>, IAmATrackedEventBase<EventStatus>
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the visit.</summary>
        /// <value>The identifier of the visit.</value>
        int? VisitID { get; set; }

        /// <summary>Gets or sets the visit.</summary>
        /// <value>The visit.</value>
        Visit? Visit { get; set; }
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

    [SqlSchema("Tracking", "Event")]
    public class Event : NameableBase, IEvent
    {
        private ICollection<PageViewEvent>? pageViewEvents;

        public Event()
        {
            pageViewEvents = new HashSet<PageViewEvent>();
        }

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual EventType? Type { get; set; }
        #endregion

        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual EventStatus? Status { get; set; }
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

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Visit)), DefaultValue(null)]
        public int? VisitID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Visit? Visit { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DefaultValue(null), JsonIgnore]
        public virtual ICollection<PageViewEvent>? PageViewEvents { get => pageViewEvents; set => pageViewEvents = value; }
        #endregion
    }
}
