// <copyright file="StoreAuction.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store store class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IStoreAuction
        : IAmAStoreRelationshipTableWhereStoreIsTheMaster<Auction>
    {
        /// <summary>Gets or sets a value indicating whether this IStoreAuction is visible in.</summary>
        /// <value>True if this IStoreAuction is visible in, false if not.</value>
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

    [SqlSchema("Auctions", "StoreAuction")]
    public class StoreAuction : Base, IStoreAuction
    {
        #region IAmARelationshipTable<Store, Auction>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Store? Master { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByStore.StoreID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Store? IAmFilterableByStore.Store { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAStoreRelationshipTableWhereStoreIsTheMaster<Auction>.StoreID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Store? IAmAStoreRelationshipTableWhereStoreIsTheMaster<Auction>.Store { get => Master; set => Master = value; }

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
