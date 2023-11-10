// <copyright file="RoleWorkflow.Validation.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the role workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Threading.Tasks;
    using DataModel;

    /// <summary>An authentication workflow.</summary>
    public partial class AuthenticationWorkflow
    {
        /// <inheritdoc/>
        public async Task<IRoleForUserModel[]> GetRolesForUserAsync(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            return await userManager.GetRolesForUserAsync(userID).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IRoleForAccountModel[]> GetRolesForAccountAsync(int accountID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            return await userManager.GetRolesForAccountAsync(accountID).ConfigureAwait(false);
        }
    }
}
