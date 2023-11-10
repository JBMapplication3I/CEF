// <copyright file="StoreCountry.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store country class, which describes
// a relationship between a Store and a Country</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IStoreCountry
        : IAmAStoreRelationshipTableWhereStoreIsTheMaster<Country>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Clarity.Ecommerce.Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Stores", "StoreCountry")]
    public class StoreCountry : Base, IStoreCountry
    {
        #region Store as Master
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Store? Master { get; set; }
        #endregion

        #region Country as Slave
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual Country? Slave { get; set; }
        #endregion

        #region IAmAStoreRelationshipTableWhereStoreIsTheMaster
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAStoreRelationshipTableWhereStoreIsTheMaster<Country>.StoreID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Store? IAmAStoreRelationshipTableWhereStoreIsTheMaster<Country>.Store { get => Master; set => Master = value; }
        #endregion

        #region IAmFilterableByStore
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByStore.StoreID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Store? IAmFilterableByStore.Store { get => Master; set => Master = value; }
        #endregion
    }
}
