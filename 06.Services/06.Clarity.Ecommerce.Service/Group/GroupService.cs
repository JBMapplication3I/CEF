// <copyright file="GroupService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the group service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Models;
    using ServiceStack;

    /// <summary>A create group.</summary>
    /// <seealso cref="GroupModel"/>
    /// <seealso cref="IReturn{GroupModel}"/>
    [Authenticate, RequiredPermission("Groups.Group.Create")]
    [Route("/Groups/Group/CurrentUser/Create", "POST", Summary = "Use to create a new group", Priority = 1)]
    public class CreateGroupForCurrentUser : GroupModel, IReturn<GroupModel>
    {
    }

    public abstract partial class GroupServiceBase
    {
        public virtual async Task<object?> Post(CreateGroupForCurrentUser request)
        {
            request.GroupOwnerID ??= CurrentUserIDOrThrow401;
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedGroupDataAsync,
                    () => Workflows.Groups.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
    }
}
