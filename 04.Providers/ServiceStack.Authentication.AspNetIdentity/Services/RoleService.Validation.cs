// <copyright file="RoleService.Validation.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication service class</summary>
// ReSharper disable AsyncApostle.AsyncMethodNamingHighlighting
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using JetBrains.Annotations;
    using ServiceStack;
    using ServiceStack.Auth;

    [PublicAPI, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Roles/User/{ID}", "GET",
            Summary = "Get the roles assigned to a specific user (as models).")]
    public class GetRolesForUser : ImplementsIDBase, IReturn<RoleForUserModel[]>
    {
    }

    [PublicAPI, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Roles/Account/{ID}", "GET",
            Summary = "Get the roles assigned to a specific account (as models).")]
    public class GetRolesForAccount : ImplementsIDBase, IReturn<RoleForAccountModel[]>
    {
    }

    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInVendorAdmin, UsedInAdmin,
        Authenticate,
        Route("/Authentication/CurrentUser/HasRole", "POST",
            Summary = "Check if the currently logged in user has a specific role by name.")]
    public class CurrentUserHasRole : IReturnVoid
    {
        [ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The Name of the Role to check")]
        public string Name { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInVendorAdmin, UsedInAdmin,
        Authenticate,
        Route("/Authentication/CurrentUser/HasAnyRole", "POST",
            Summary = "Check if the currently logged in user has any Roles by Regular Expression")]
    public class CurrentUserHasAnyRole : IReturnVoid
    {
        [ApiMember(Name = nameof(Regex), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The Regular Expression of the Roles to match against")]
        public string Regex { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInVendorAdmin, UsedInAdmin,
        Authenticate,
        Route("/Authentication/CurrentUser/HasPermission", "POST",
            Summary = "Check if the currently logged in user has a specific Permission by name")]
    public class CurrentUserHasPermission : IReturnVoid
    {
        [ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The Name of the Permission to check")]
        public string Name { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInStoreAdmin, UsedInBrandAdmin, UsedInVendorAdmin, UsedInAdmin,
        Authenticate,
        Route("/Authentication/CurrentUser/HasAnyPermission", "POST",
            Summary = "Check if the currently logged in user has any Permissions by Regular Expression")]
    public class CurrentUserHasAnyPermission : IReturnVoid
    {
        [ApiMember(Name = nameof(Regex), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The Regular Expression of the Permissions to match against")]
        public string Regex { get; set; } = null!;
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Get(GetRolesForUser request)
        {
            return await Workflows.Authentication.GetRolesForUserAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetRolesForAccount request)
        {
            return await Workflows.Authentication.GetRolesForAccountAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public Task Post(CurrentUserHasRole request)
        {
            Response.SetCookie(MakeACookie(
                (Globals.HasRoleCookiePrefix + request.Name).Replace(" ", "%20"),
                SessionAs<CMSAuthUserSession>().HasRole(request.Name, null!) ? "1" : "0",
                TimeSpan.FromMinutes(30)));
            return Task.CompletedTask;
        }

        public Task Post(CurrentUserHasAnyRole request)
        {
            Response.SetCookie(MakeACookie(
                Globals.HasAnyRoleCookiePrefix + request.Regex,
                SessionAs<CMSAuthUserSession>().HasAnyRole(new(request.Regex.Trim('/'))) ? "1" : "0",
                TimeSpan.FromMinutes(30)));
            return Task.CompletedTask;
        }

        public Task Post(CurrentUserHasPermission request)
        {
            Response.SetCookie(MakeACookie(
                Globals.HasPermissionCookiePrefix + request.Name,
                SessionAs<CMSAuthUserSession>().HasPermission(request.Name, null!) ? "1" : "0",
                TimeSpan.FromMinutes(30)));
            return Task.CompletedTask;
        }

        public Task Post(CurrentUserHasAnyPermission request)
        {
            Response.SetCookie(MakeACookie(
                Globals.HasAnyPermissionCookiePrefix + request.Regex,
                SessionAs<CMSAuthUserSession>().HasAnyPermission(new(request.Regex.Trim('/'))) ? "1" : "0",
                TimeSpan.FromMinutes(30)));
            return Task.CompletedTask;
        }
    }
}
