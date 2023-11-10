// <copyright file="Zone.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the zone class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for zone.</summary>
    public interface IZone : INameableBase, IHaveATypeBase<ZoneType>, IHaveAStatusBase<ZoneStatus>
    {
        #region Zone Properties
        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        int Width { get; set; }

        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        int Height { get; set; }
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Advertising", "Zone")]
    public class Zone : NameableBase, IZone
    {
        private ICollection<AdZone>? adZones;

        public Zone()
        {
            adZones = new HashSet<AdZone>();
        }

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual ZoneType? Type { get; set; }
        #endregion

        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual ZoneStatus? Status { get; set; }
        #endregion

        #region Zone Properties
        /// <inheritdoc/>
        [DefaultValue(0)]
        public int Width { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int Height { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<AdZone>? AdZones { get => adZones; set => adZones = value; }
        #endregion
    }
}
