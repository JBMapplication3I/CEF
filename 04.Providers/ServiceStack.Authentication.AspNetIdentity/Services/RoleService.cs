// <copyright file="RoleService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the role service class</summary>
// ReSharper disable AsyncApostle.AsyncMethodNamingHighlighting
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;
    using Utilities;

    /// <summary>A get roles.</summary>
    /// <seealso cref="IReturn{Dictionary_string_int}"/>
    [PublicAPI, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Roles", "POST",
            Summary = "Get the list of all roles in the server (as ID/Name key/value pairs).")]
    public class GetRoles : IReturn<Dictionary<string, int>>
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int?", ParameterType = "body", IsRequired = false)]
        public int? ID { get; set; }

        /// <summary>Gets or sets the custom key.</summary>
        /// <value>The custom key.</value>
        [ApiMember(Name = nameof(CustomKey), DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? CustomKey { get; set; }
    }

    /// <summary>A get roles as listing.</summary>
    /// <seealso cref="IReturn{Array_RoleUserModel}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/RolesListing", "GET",
            Summary = "Get the list of roles in the server (as models).")]
    public class GetRolesAsListing : IReturn<RoleUserModel[]>
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? ID { get; set; }

        /// <summary>Gets or sets the custom key.</summary>
        /// <value>The custom key.</value>
        [ApiMember(Name = nameof(CustomKey), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? CustomKey { get; set; }
    }

    /// <summary>A get role.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{RoleUserModel}"/>
    [PublicAPI, UsedInAdmin,
        Route("/Authentication/Role/{ID}", "GET",
            Summary = "Use to get a specific role")]
    public class GetRole : ImplementsIDBase, IReturn<RoleUserModel>
    {
    }

    /// <summary>A create role.</summary>
    /// <seealso cref="IReturn{RoleUserModel}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Role/Create", "POST",
            Summary = "Use to create a new role")]
    public class CreateRole : IReturn<RoleUserModel>
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Name of the new Role to create")]
        public string Name { get; set; } = null!;

        /// <summary>Gets or sets the included permissions.</summary>
        /// <value>The included permissions.</value>
        [ApiMember(Name = nameof(IncludedPermissions), DataType = "List<PermissionModel>", ParameterType = "body", IsRequired = true,
            Description = "Permissions this Role has")]
        public List<PermissionModel> IncludedPermissions { get; set; } = null!;
    }

    /// <summary>An update role.</summary>
    /// <seealso cref="IReturn{RoleUserModel}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Role/Update", "PUT",
            Summary = "Use to create a new role")]
    public class UpdateRole : IReturn<RoleUserModel>
    {
        /// <summary>Gets or sets the name of the old.</summary>
        /// <value>The name of the old.</value>
        [ApiMember(Name = nameof(OldName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Old name for the Role")]
        public string OldName { get; set; } = null!;

        /// <summary>Gets or sets the name of the new.</summary>
        /// <value>The name of the new.</value>
        [ApiMember(Name = nameof(NewName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "New name for the Role")]
        public string NewName { get; set; } = null!;

        /// <summary>Gets or sets the included permissions.</summary>
        /// <value>The included permissions.</value>
        [ApiMember(Name = nameof(IncludedPermissions), DataType = "List<PermissionModel>", ParameterType = "body", IsRequired = true,
            Description = "Permissions this Role has")]
        public List<PermissionModel> IncludedPermissions { get; set; } = null!;
    }

    /// <summary>A delete role.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Role/Delete", "DELETE",
            Summary = "Removes a specific role from the system [Hard-Delete]")]
    public class DeleteRole : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Name of the Role to delete")]
        public string Name { get; set; } = null!;
    }

    /// <summary>A get permissions.</summary>
    /// <seealso cref="IReturn{List_PermissionModel}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Permissions", "GET",
            Summary = "Get the list of Permissions available in the server")]
    public class GetPermissions : IReturn<List<PermissionModel>>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetRoles request)
        {
            var roles = await Workflows.Authentication.GetRolesAsync(
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            if (Contract.CheckValidID(request.ID))
            {
                return roles
                    .Where(x => x.Value == request.ID!.Value)
                    .ToDictionary(x => x.Key, x => x.Value);
            }
            if (Contract.CheckValidKey(request.CustomKey))
            {
                var search = request.CustomKey!.Trim().ToLower();
                return roles
                    .Where(x => x.Key.ToLower().Contains(search))
                    .ToDictionary(x => x.Key, x => x.Value);
            }
            return roles;
        }

        public async Task<object?> Get(GetRolesAsListing request)
        {
            var roles = await Workflows.Authentication.GetRolesAsync(
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            if (Contract.CheckValidID(request.ID))
            {
                roles = roles
                    .Where(x => x.Value == request.ID!.Value)
                    .ToDictionary(x => x.Key, x => x.Value);
            }
            else if (Contract.CheckValidKey(request.CustomKey))
            {
                var search = request.CustomKey!.Trim().ToLower();
                roles = roles
                    .Where(x => x.Key.ToLower().Contains(search))
                    .ToDictionary(x => x.Key, x => x.Value);
            }
            return roles
                .Select(x => new RoleUserModel
                {
                    Active = true,
                    CustomKey = x.Key,
                    ID = x.Value,
                })
                .ToArray();
        }

        public async Task<object?> Get(GetRole request)
        {
            return await Workflows.Authentication.GetAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(CreateRole request)
        {
            return (RoleUserModel)(await Workflows.Authentication.CreateRoleAsync(
                        name: request.Name,
                        permissions: request.IncludedPermissions.ConvertAll(x => x.Name)!,
                        ServiceContextProfileName)
                    .ConfigureAwait(false))
                .Result!;
        }

        public async Task<object?> Put(UpdateRole request)
        {
            return (RoleUserModel)(await Workflows.Authentication.RenameRoleAsync(
                        name: request.OldName,
                        newName: request.NewName,
                        permissions: request.IncludedPermissions.ConvertAll(x => x.Name)!,
                        ServiceContextProfileName)
                    .ConfigureAwait(false))
                .Result!;
        }

        public async Task<object?> Delete(DeleteRole request)
        {
            return await Workflows.Authentication.DeleteRoleAsync(
                    request.Name,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetPermissions _)
        {
            return await Workflows.Authentication.GetPermissionsAsync(
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
