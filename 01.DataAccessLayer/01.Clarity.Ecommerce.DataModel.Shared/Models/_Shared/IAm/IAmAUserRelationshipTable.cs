// <copyright file="IAmAUserRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAUserRelationshipTable interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    /// <summary>Interface for am a user relationship table base.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    public interface IAmAUserRelationshipTableWhereUserIsTheSlave<out TMaster>
        : IAmARelationshipTable<TMaster, User>, IAmFilterableByUser
        where TMaster : IBase
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        new int UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        [Obsolete("Cannot use in queries, use Slave instead.", true)]
        new User? User { get; set; }
    }

    /// <summary>Interface for am a user relationship table base.</summary>
    /// <typeparam name="TSlave">Type of the master.</typeparam>
    public interface IAmAUserRelationshipTableWhereUserIsTheMaster<TSlave>
        : IAmARelationshipTable<User, TSlave>, IAmFilterableByUser
        where TSlave : IBase
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [Obsolete("Cannot use in queries, use MasterID instead.", true)]
        new int UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        [Obsolete("Cannot use in queries, use Master instead.", true)]
        new User? User { get; set; }
    }
}
