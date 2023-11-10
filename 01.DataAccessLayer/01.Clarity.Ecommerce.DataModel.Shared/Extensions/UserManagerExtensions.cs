// <copyright file="UserManagerExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user manager extensions class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Utilities;

    /// <summary>A user manager extensions.</summary>
    public static class UserManagerExtensions
    {
        /// <summary>A <seealso cref="CEFUserManager"/> extension method that assign role if not assigned.</summary>
        /// <param name="userManager">The userManager to act on.</param>
        /// <param name="userID">     Identifier for the user.</param>
        /// <param name="roleName">   Name of the role.</param>
        /// <param name="startDate">  The start date.</param>
        /// <param name="endDate">    The end date.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static async Task<(bool success, string message)> AssignRoleIfNotAssignedAsync(
            this ICEFUserManager userManager,
            int userID,
            string roleName,
            DateTime? startDate,
            DateTime? endDate)
        {
            Contract.RequiresValidKey(roleName);
            var now = DateExtensions.GenDateTime;
            if ((await Contract.RequiresNotNull(userManager).GetRolesForUserAsync(Contract.RequiresValidID(userID)).ConfigureAwait(false))
                .Any(x => x.Name == roleName && (x.EndDate == null || x.EndDate > now) && (!startDate.HasValue || startDate.Value < x.EndDate)))
            {
                return (true, "User already has this role");
            }
            var result = await userManager.AddRoleToUserAsync(userID, roleName, startDate, endDate).ConfigureAwait(false);
            if (result.Succeeded)
            {
                // TODO: Logging successful role assignment
                return (true, "Role successfully added to the User");
            }
            var message = result.Errors.Aggregate(
                $"There were errors assigning the role {roleName} to user id {userID}",
                (c, n) => c + string.Empty + n);
            System.Diagnostics.Debug.WriteLine(message);
            // TODO: Logging user role assignment error
            return (false, message);
        }

        /// <summary>An <seealso cref="ICEFUserManager"/> extension method that removes the role if assigned.</summary>
        /// <param name="userManager">The userManager to act on.</param>
        /// <param name="userID">     Identifier for the user.</param>
        /// <param name="roleName">   Name of the role.</param>
        /// <returns>A Tuple.</returns>
        public static async Task<(bool success, string message)> RemoveRoleIfAssignedAsync(
            this ICEFUserManager userManager,
            int userID,
            string roleName)
        {
            Contract.RequiresValidKey(roleName);
            if ((await Contract.RequiresNotNull(userManager).GetUserRolesAsync(Contract.RequiresValidID(userID)).ConfigureAwait(false)).All(x => x != roleName))
            {
                return (true, "User already doesn't have this role");
            }
            var result = await userManager.RemoveRoleFromUserAsync(userID, roleName).ConfigureAwait(false);
            if (result.Succeeded)
            {
                // TODO: Logging successful role removal
                return (true, "Role successfully remove from the User");
            }
            var message = result.Errors.Aggregate(
                $"There were errors removing the role {roleName} from user id {userID}",
                (c, n) => c + string.Empty + n);
            System.Diagnostics.Debug.WriteLine(message);
            // TODO: Logging user role removal error
            return (false, message);
        }
    }
}
