// <copyright file="AdZone.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ad zone class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for ad zone.</summary>
    public interface IAdZone : IAmARelationshipTable<Ad, Zone>, IHaveAdCounters
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the ad zone access.</summary>
        /// <value>The identifier of the ad zone access.</value>
        int? AdZoneAccessID { get; set; }

        /// <summary>Gets or sets the ad zone access.</summary>
        /// <value>The ad zone access.</value>
        AdZoneAccess? AdZoneAccess { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Advertising", "AdZone")]
    public class AdZone : Base, IAdZone
    {
        #region IAmARelationshipTable<Ad, Zone>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Ad? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Zone? Slave { get; set; }
        #endregion

        #region IHaveAdCounters
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ImpressionCounter)), DefaultValue(null)]
        public int? ImpressionCounterID { get; set; }

        [DefaultValue(null), JsonIgnore]
        public virtual Counter? ImpressionCounter { get; set; }

        [InverseProperty(nameof(ID)), ForeignKey(nameof(ClickCounter)), DefaultValue(null)]
        public int? ClickCounterID { get; set; }

        [DefaultValue(null), JsonIgnore]
        public virtual Counter? ClickCounter { get; set; }
        #endregion

        #region Related Objects
        [InverseProperty(nameof(ID)), ForeignKey(nameof(AdZoneAccess)), DefaultValue(null)]
        public int? AdZoneAccessID { get; set; }

        [DefaultValue(null), JsonIgnore]
        public virtual AdZoneAccess? AdZoneAccess { get; set; }
        #endregion
    }
}
