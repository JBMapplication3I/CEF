// <copyright file="IAmFilterableByAccountModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByAccountModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for am filterable by account model.</summary>
    /// <typeparam name="TModel">Type of the account relationship model.</typeparam>
    public interface IAmFilterableByAccountModel<TModel>
    {
        /// <summary>Gets or sets the accounts.</summary>
        /// <value>The accounts.</value>
        List<TModel>? Accounts { get; set; }
    }

    /// <summary>Interface for am filterable by account model.</summary>
    public interface IAmFilterableByAccountModel
    {
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int AccountID { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        IAccountModel? Account { get; set; }

        /// <summary>Gets or sets the account key.</summary>
        /// <value>The account key.</value>
        string? AccountKey { get; set; }

        /// <summary>Gets or sets the name of the account.</summary>
        /// <value>The name of the account.</value>
        string? AccountName { get; set; }
    }
}
