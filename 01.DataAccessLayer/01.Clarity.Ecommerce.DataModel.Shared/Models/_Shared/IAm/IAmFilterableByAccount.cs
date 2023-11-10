// <copyright file="IAmFilterableByAccount.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByAccount interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for am filterable by account.</summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public interface IAmFilterableByAccount<TEntity> : IBase
    {
        /// <summary>Gets or sets the accounts.</summary>
        /// <value>The accounts.</value>
        ICollection<TEntity>? Accounts { get; set; }
    }

    /// <summary>Interface for am filterable by account id.</summary>
    public interface IAmFilterableByAccount : IBase
    {
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int AccountID { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        Account? Account { get; set; }
    }

    /// <summary>Interface for am filterable by (nullable) account id.</summary>
    public interface IAmFilterableByNullableAccount : IBase
    {
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        Account? Account { get; set; }
    }
}
