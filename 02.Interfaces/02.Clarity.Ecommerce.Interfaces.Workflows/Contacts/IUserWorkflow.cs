// <copyright file="IUserWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUserWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for user workflow.</summary>
    public partial interface IUserWorkflow
    {
        /// <summary>Gets by key.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by key.</returns>
        Task<IUserModel?> GetByKeyAsync(string email, string? contextProfileName);

        /// <summary>Gets by key (Lite mapping instead of Full).</summary>
        /// <param name="email">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by key.</returns>
        Task<IUserModel?> GetLiteByKeyAsync(string email, string? contextProfileName);

        /// <summary>Gets the user by email.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The user model.</returns>
        Task<IUserModel?> GetByEmailAsync(string email, string? contextProfileName);

        /// <summary>Gets the user by username.</summary>
        /// <param name="username"> The username.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The user model.</returns>
        Task<IUserModel?> GetByUserNameAsync(string username, string? contextProfileName);

        /// <summary>Gets for pricing.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>for pricing.</returns>
        Task<IUserForPricingModel?> GetForPricingAsync(int id, string? contextProfileName);

        /// <summary>Check exists by email.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsByEmailAsync(string email, string? contextProfileName);

        /// <summary>Check exists by username.</summary>
        /// <param name="username">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsByUsernameAsync(string username, string? contextProfileName);

        /// <summary>Creates for identity.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new for identity.</returns>
        Task<IUserModel?> CreateForIdentityAsync(IUserModel model, string? contextProfileName);

        /// <summary>Creates for identity.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>The new for identity.</returns>
        Task<IUserModel?> CreateForIdentityAsync(IUserModel model, IClarityEcommerceEntities context);

        /// <summary>Creates for identity.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new for identity.</returns>
        Task<IUser> CreateForIdentityEntityAsync(IUserModel model, string? contextProfileName);

        /// <summary>Creates for identity.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>The new for identity.</returns>
        Task<IUser> CreateForIdentityEntityAsync(IUserModel model, IClarityEcommerceEntities context);

        /// <summary>Searches for the first as identifier list.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The found as identifier list.</returns>
        Task<List<int>> SearchAsIDListAsync(IUserSearchModel request, string? contextProfileName);

        /// <summary>Upsert selected language.</summary>
        /// <param name="username">          The username.</param>
        /// <param name="locale">            The locale.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> UpsertSelectedLanguageAsync(string username, string locale, string? contextProfileName);

        /// <summary>Upsert selected currency.</summary>
        /// <param name="username">          The username.</param>
        /// <param name="currency">          The currency.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> UpsertSelectedCurrencyAsync(string username, string currency, string? contextProfileName);

        /// <summary>Invite with code.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="currentUser">       The current user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A ValueTuple containing the new userID and the invite code string.</returns>
        Task<(int userID, string inviteCode)> InviteWithCodeAsync(
            string email,
            IUserModel currentUser,
            string? contextProfileName);

        /// <summary>Gets by email and invitation code.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="invitationCode">    The invitation code.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by email and invitation code.</returns>
        Task<IUserModel> GetByEmailAndInvitationCodeAsync(string email, string invitationCode, string? contextProfileName);

        /// <summary>Gets online status.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The online status.</returns>
        Task<IStatusModel?> GetOnlineStatusAsync(int userID, string? contextProfileName);

        /// <summary>Sets online status.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="onlineStatus">      The online status.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetOnlineStatusAsync(int userID, string onlineStatus, string? contextProfileName);

        /// <summary>Changes the type of the user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="typeKey">           New Type Key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ChangeTypeAsync(int userID, string typeKey, string? contextProfileName);

        /// <summary>Export users to excel.</summary>
        /// <param name="request">           user search params.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>DataSet of Users.</returns>
        Task<DataSet> ExportToExcelAsync(IUserSearchModel request, string? contextProfileName);

        /// <summary>Gets users account assignments.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The users account assignments.</returns>
        Task<List<UserAccountAssignment>> GetUsersAccountAssignmentsAsync(string? contextProfileName);

        /// <summary>Updates the users account assignments.</summary>
        /// <param name="toUpdate">          to update.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> UpdateUsersAccountAssignmentsAsync(
            List<UserAccountAssignment> toUpdate,
            string? contextProfileName);

        /// <summary>Gets username for identifier.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The username for identifier.</returns>
        Task<string> GetUsernameForIDAsync(int id, string? contextProfileName);

        /// <summary>Gets supervisors for identifier.</summary>
        /// <param name="userID">            The identifier of the current user.</param>
        /// <param name="currentAccountID">  The identifier of the current account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The supervisors for identifier.</returns>
        Task<List<IUserModel?>?> GetSupervisorsOnAccountForUserAsync(int userID, int currentAccountID, string? contextProfileName);
    }
}
