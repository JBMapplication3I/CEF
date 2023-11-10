// <copyright file="IUserSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUserSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for user search model.</summary>
    /// <seealso cref="IHaveATypeBaseSearchModel"/>
    /// <seealso cref="IHaveAStatusBaseSearchModel"/>
    public partial interface IUserSearchModel
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string? Name { get; set; }

        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
        string? FirstName { get; set; }

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
        string? LastName { get; set; }

        /// <summary>Gets or sets the identifier of the accessible from account.</summary>
        /// <value>The identifier of the accessible from account.</value>
        int? AccessibleFromAccountID { get; set; }

        /// <summary>Gets or sets the account name.</summary>
        /// <value>The name of the account.</value>
        string? AccountName { get; set; }

        /// <summary>Gets or sets the account key.</summary>
        /// <value>The account key.</value>
        string? AccountKey { get; set; }

        /// <summary>Gets or sets the search value for the filter for username or contact name.</summary>
        /// <value>The value to search for.</value>
        string? UserNameOrContactName { get; set; }

        /// <summary>Gets or sets the name of the user name or custom key or email or contact.</summary>
        /// <value>The name of the user name or custom key or email or contact.</value>
        string? UserNameOrCustomKeyOrEmailOrContactName { get; set; }

        /// <summary>Gets or sets the name of the identifier or user name or custom key or email or contact.</summary>
        /// <value>The name of the identifier or user name or custom key or email or contact.</value>
        string? IDOrUserNameOrCustomKeyOrEmailOrContactName { get; set; }

        /// <summary>Gets or sets the user name or custom key or email.</summary>
        /// <value>The user name or custom key or email.</value>
        string? UserNameOrCustomKeyOrEmail { get; set; }

        /// <summary>Gets or sets the user name or email.</summary>
        /// <value>The user name or email.</value>
        string? UserNameOrEmail { get; set; }

        /// <summary>Gets or sets the identifier of the online status.</summary>
        /// <value>The identifier of the online status.</value>
        int? OnlineStatusID { get; set; }

        /// <summary>Gets or sets the number of association levels searching for. </summary>
        /// <value>The number of account association levels for which we are searching for.</value>
        int? AccessibleLevels { get; set; }
    }
}
