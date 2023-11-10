// <copyright file="AdZoneAccess.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store PurchasedAdZone class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IAdZoneAccess : INameableBase, IHaveAdCounters
    {
        #region AdZoneAccess Properties
        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime EndDate { get; set; }

        /// <summary>Gets or sets the unique ad limit.</summary>
        /// <value>The unique ad limit.</value>
        int UniqueAdLimit { get; set; }

        /// <summary>Gets or sets the impression limit.</summary>
        /// <value>The impression limit.</value>
        int ImpressionLimit { get; set; }

        /// <summary>Gets or sets the click limit.</summary>
        /// <value>The click limit.</value>
        int ClickLimit { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the zone.</summary>
        /// <value>The identifier of the zone.</value>
        int? ZoneID { get; set; }

        /// <summary>Gets or sets the zone.</summary>
        /// <value>The zone.</value>
        Zone? Zone { get; set; }

        /// <summary>Gets or sets the identifier of the subscription.</summary>
        /// <value>The identifier of the subscription.</value>
        int? SubscriptionID { get; set; }

        /// <summary>Gets or sets the subscription.</summary>
        /// <value>The subscription.</value>
        Subscription? Subscription { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the ad zones.</summary>
        /// <value>The ad zones.</value>
        ICollection<AdZone>? AdZones { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Advertising", "AdZoneAccess")]
    public class AdZoneAccess : NameableBase, IAdZoneAccess
    {
        private ICollection<AdZone>? adZones;

        public AdZoneAccess()
        {
            adZones = new HashSet<AdZone>();
        }

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

        #region AdZoneAccess Properties
        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime StartDate { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime EndDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int UniqueAdLimit { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int ImpressionLimit { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int ClickLimit { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Zone)), DefaultValue(null)]
        public int? ZoneID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Zone? Zone { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Subscription)), DefaultValue(null)]
        public int? SubscriptionID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Subscription? Subscription { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<AdZone>? AdZones { get => adZones; set => adZones = value; }
        #endregion
    }
}
