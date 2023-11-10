// <copyright file="ICMSAuthUserSession.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ICMSAuthUserSession class</summary>
namespace ServiceStack.Auth
{
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.Interfaces.Models;

    /// <summary>Interface for CMS authentication user session.</summary>
    public interface ICMSAuthUserSession : IAuthSession
    {
        /// <summary>Gets or sets the CMS roles.</summary>
        /// <value>The CMS roles.</value>
        string[]? CMSRoles { get; set; }

        /// <summary>Gets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int? AccountID { get; }

        /// <summary>Gets or sets the identifier of the selected account.</summary>
        /// <value>The identifier of the selected account.</value>
        int? SelectedAccountID { get; set; }

        /// <summary>Gets the customKey of the account.</summary>
        /// <value>The customKey of the account.</value>
        string? AccountKey { get; }

        /// <summary>Gets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; }

        /// <summary>Gets the user's custom key.</summary>
        /// <value>The user's custom key.</value>
        string? UserKey { get; }

        /// <summary>Gets the user's username.</summary>
        /// <value>The user's username.</value>
        string? UserUsername { get; }

        /// <summary>Gets the account.</summary>
        /// <returns>The account.</returns>
        Task<IAccountModel?> AccountAsync();

        /// <summary>Gets the user.</summary>
        /// <returns>The user.</returns>
        Task<IUserModel?> UserAsync();

        /// <summary>Query if 'role' has any role.</summary>
        /// <param name="role">The role.</param>
        /// <returns>True if any role, false if not.</returns>
        bool HasAnyRole(Regex role);

        /// <summary>Query if 'permission' has any permission.</summary>
        /// <param name="permission">The permission.</param>
        /// <returns>True if any permission, false if not.</returns>
        bool HasAnyPermission(Regex permission);

        /// <summary>Clears the account cached value.</summary>
        /// <param name="key">The key. When null, will generate the necessary key.</param>
        /// <returns>A Task.</returns>
        Task ClearSessionAccountAsync(string? key = null);

        /// <summary>Clears the user cached value.</summary>
        /// <param name="key">The key. When null, will generate the necessary key.</param>
        /// <returns>A Task.</returns>
        Task ClearSessionUserAsync(string? key = null);
    }
}
