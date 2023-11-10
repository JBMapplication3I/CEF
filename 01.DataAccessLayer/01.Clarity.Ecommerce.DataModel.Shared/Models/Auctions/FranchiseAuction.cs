// <copyright file="FranchiseAuction.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand franchise class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IFranchiseAuction
        : IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Auction>
    {
        /// <summary>Gets or sets a value indicating whether this IFranchiseAuction is visible in.</summary>
        /// <value>True if this IFranchiseAuction is visible in, false if not.</value>
        bool IsVisibleIn { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Auctions", "FranchiseAuction")]
    public class FranchiseAuction : Base, IFranchiseAuction
    {
        #region IAmARelationshipTable<Franchise, Auction>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Franchise? Master { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByFranchise.FranchiseID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Franchise? IAmFilterableByFranchise.Franchise { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Auction>.FranchiseID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Franchise? IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Auction>.Franchise { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Auction? Slave { get; set; }
        #endregion

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsVisibleIn { get; set; }
    }
}
