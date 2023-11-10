// <copyright file="IAmAFranchiseRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAFranchiseRelationshipTable interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    /// <summary>Interface for am a franchise relationship table base.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    public interface IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlave<out TMaster>
        : IAmARelationshipTable<TMaster, Franchise>, IAmFilterableByFranchise
        where TMaster : IBase
    {
        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        [Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        new int FranchiseID { get; set; }

        /// <summary>Gets or sets the franchise.</summary>
        /// <value>The franchise.</value>
        [Obsolete("Cannot use in queries, use Slave instead.", true)]
        new Franchise? Franchise { get; set; }
    }

    /// <summary>Interface for am a franchise relationship table base.</summary>
    /// <typeparam name="TSlave">Type of the master.</typeparam>
    public interface IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<TSlave>
        : IAmARelationshipTable<Franchise, TSlave>, IAmFilterableByFranchise
        where TSlave : IBase
    {
        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        [Obsolete("Cannot use in queries, use MasterID instead.", true)]
        new int FranchiseID { get; set; }

        /// <summary>Gets or sets the franchise.</summary>
        /// <value>The franchise.</value>
        [Obsolete("Cannot use in queries, use Master instead.", true)]
        new Franchise? Franchise { get; set; }
    }
}
