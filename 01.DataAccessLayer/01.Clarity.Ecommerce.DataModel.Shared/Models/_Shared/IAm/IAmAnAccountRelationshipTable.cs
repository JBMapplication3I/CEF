// <copyright file="IAmAnAccountRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAnAccountRelationshipTable interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    /// <summary>Interface for am an account relationship table base.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    public interface IAmAnAccountRelationshipTableWhereAccountIsTheSlave<out TMaster>
        : IAmARelationshipTable<TMaster, Account>, IAmFilterableByAccount
        where TMaster : IBase
    {
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        new int AccountID { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        [Obsolete("Cannot use in queries, use Slave instead.", true)]
        new Account? Account { get; set; }
    }

    /// <summary>Interface for am an account relationship table base.</summary>
    /// <typeparam name="TSlave">Type of the master.</typeparam>
    public interface IAmAnAccountRelationshipTableWhereAccountIsTheMaster<TSlave>
        : IAmARelationshipTable<Account, TSlave>, IAmFilterableByAccount
        where TSlave : IBase
    {
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [Obsolete("Cannot use in queries, use MasterID instead.", true)]
        new int AccountID { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        [Obsolete("Cannot use in queries, use Master instead.", true)]
        new Account? Account { get; set; }
    }
}
