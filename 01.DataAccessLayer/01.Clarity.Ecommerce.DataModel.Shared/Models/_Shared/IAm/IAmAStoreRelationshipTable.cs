// <copyright file="IAmAStoreRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAStoreRelationshipTable interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    /// <summary>Interface for am a store relationship table base.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    public interface IAmAStoreRelationshipTableWhereStoreIsTheSlave<out TMaster>
        : IAmARelationshipTable<TMaster, Store>, IAmFilterableByStore
        where TMaster : IBase
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        new int StoreID { get; set; }

        /// <summary>Gets or sets the store.</summary>
        /// <value>The store.</value>
        [Obsolete("Cannot use in queries, use Slave instead.", true)]
        new Store? Store { get; set; }
    }

    /// <summary>Interface for am a store relationship table base.</summary>
    /// <typeparam name="TSlave">Type of the master.</typeparam>
    public interface IAmAStoreRelationshipTableWhereStoreIsTheMaster<TSlave>
        : IAmARelationshipTable<Store, TSlave>, IAmFilterableByStore
        where TSlave : IBase
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [Obsolete("Cannot use in queries, use MasterID instead.", true)]
        new int StoreID { get; set; }

        /// <summary>Gets or sets the store.</summary>
        /// <value>The store.</value>
        [Obsolete("Cannot use in queries, use Master instead.", true)]
        new Store? Store { get; set; }
    }
}
