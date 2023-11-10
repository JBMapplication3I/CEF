// <autogenerated>
// <copyright file="ConversationUserService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conversation user service class</summary>
// </autogenerated>
// ReSharper disable InvalidXmlDocComment, PartialTypeWithSinglePart, RedundantUsingDirective
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    /// <summary>A ServiceStack Route to get a list of conversation users.</summary>
    /// <seealso cref="ConversationUserSearchModel"/>
    /// <seealso cref="IReturn{ConversationUserPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Messaging/ConversationUsers", "GET", Priority = 1,
            Summary = "Use to get a list of conversation users")]
    public partial class GetConversationUsers : ConversationUserSearchModel, IReturn<ConversationUserPagedResults> { }

    /// <summary>A ServiceStack Route to get conversation users for connect.</summary>
    /// <seealso cref="ConversationUserSearchModel"/>
    /// <seealso cref="IReturn{List{ConversationUserModel}}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.View"),
        PublicAPI,
        Route("/Messaging/ConversationUsersForConnect", "POST,GET", Priority = 1,
            Summary = "Get all conversation users")]
    public partial class GetConversationUsersForConnect : ConversationUserSearchModel, IReturn<List<ConversationUserModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all conversation users.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.View"),
        PublicAPI,
        Route("/Messaging/ConversationUsersDigest", "GET",
            Summary = "Use to get a hash representing each conversation users")]
    public partial class GetConversationUsersDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get conversation user.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{ConversationUserModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Messaging/ConversationUser/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific conversation user")]
    public partial class GetConversationUserByID : ImplementsIDBase, IReturn<ConversationUserModel> { }

    /// <summary>A ServiceStack Route to get conversation user.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{ConversationUserModel}"/>
    [PublicAPI,
        Route("/Messaging/ConversationUser/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific conversation user by the custom key")]
    public partial class GetConversationUserByKey : ImplementsKeyBase, IReturn<ConversationUserModel> { }

    /// <summary>A ServiceStack Route to check conversation user exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.View"),
        PublicAPI,
        Route("/Messaging/ConversationUser/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckConversationUserExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check conversation user exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.View"),
        PublicAPI,
        Route("/Messaging/ConversationUser/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckConversationUserExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create conversation user.</summary>
    /// <seealso cref="ConversationUserModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Messaging/ConversationUser/Create", "POST", Priority = 1,
            Summary = "Use to create a new conversation user.")]
    public partial class CreateConversationUser : ConversationUserModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert conversation user.</summary>
    /// <seealso cref="ConversationUserModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Messaging/ConversationUser/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing conversation user (as needed).")]
    public partial class UpsertConversationUser : ConversationUserModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update conversation user.</summary>
    /// <seealso cref="ConversationUserModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Messaging/ConversationUser/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing conversation user.")]
    public partial class UpdateConversationUser : ConversationUserModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate conversation user.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Messaging/ConversationUser/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific conversation user from the system [Soft-Delete]")]
    public partial class DeactivateConversationUserByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate conversation user by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.Deactivate"),
        PublicAPI,
        Route("/Messaging/ConversationUser/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific conversation user from the system [Soft-Delete]")]
    public partial class DeactivateConversationUserByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate conversation user.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Messaging/ConversationUser/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific conversation user from the system [Restore from Soft-Delete]")]
    public partial class ReactivateConversationUserByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate conversation user by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.Reactivate"),
        PublicAPI,
        Route("/Messaging/ConversationUser/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific conversation user from the system [Restore from Soft-Delete]")]
    public partial class ReactivateConversationUserByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete conversation user.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Messaging/ConversationUser/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific conversation user from the system [Hard-Delete]")]
    public partial class DeleteConversationUserByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete conversation user by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Messaging.ConversationUser.Delete"),
        PublicAPI,
        Route("/Messaging/ConversationUser/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific conversation user from the system [Hard-Delete]")]
    public partial class DeleteConversationUserByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear conversation user cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Messaging/ConversationUser/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all conversation user calls.")]
    public class ClearConversationUserCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class ConversationUserServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetConversationUsers"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetConversationUsers request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IConversationUserModel, ConversationUserModel, IConversationUserSearchModel, ConversationUserPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.ConversationUsers)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetConversationUsersForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetConversationUsersForConnect request)
        {
            return await Workflows.ConversationUsers.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetConversationUsersDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetConversationUsersDigest request)
        {
            return await Workflows.ConversationUsers.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetConversationUserByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetConversationUserByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.ConversationUsers, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetConversationUserByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetConversationUserByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.ConversationUsers, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckConversationUserExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckConversationUserExistsByID request)
        {
            return await Workflows.ConversationUsers.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckConversationUserExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckConversationUserExistsByKey request)
        {
            return await Workflows.ConversationUsers.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertConversationUser"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertConversationUser request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedConversationUserDataAsync,
                    () => Workflows.ConversationUsers.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateConversationUser"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateConversationUser request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedConversationUserDataAsync,
                    () => Workflows.ConversationUsers.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateConversationUser"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateConversationUser request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedConversationUserDataAsync,
                    () => Workflows.ConversationUsers.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateConversationUserByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateConversationUserByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedConversationUserDataAsync,
                    () => Workflows.ConversationUsers.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateConversationUserByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateConversationUserByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedConversationUserDataAsync,
                    () => Workflows.ConversationUsers.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateConversationUserByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateConversationUserByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedConversationUserDataAsync,
                    () => Workflows.ConversationUsers.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateConversationUserByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateConversationUserByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedConversationUserDataAsync,
                    () => Workflows.ConversationUsers.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteConversationUserByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteConversationUserByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedConversationUserDataAsync,
                    () => Workflows.ConversationUsers.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteConversationUserByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteConversationUserByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedConversationUserDataAsync,
                    () => Workflows.ConversationUsers.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearConversationUserCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearConversationUserCache request)
        {
            await ClearCachedConversationUserDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedConversationUserDataAsync()
        {
            var urn = string.Empty;
            if (JSConfigs.CEFConfigDictionary.BrandsEnabled)
            {
                urn += ":" + new System.Uri(Request.AbsoluteUri).Host.Replace(":", "{colon}");
            }
            foreach (var key in CoreUrnIDs)
            {
                await ClearCachePrefixedAsync($"{key}{urn}").ConfigureAwait(false);
            }
            if (AdditionalUrnIDs == null) { return; }
            foreach (var key in AdditionalUrnIDs)
            {
                await ClearCachePrefixedAsync($"{key}{urn}").ConfigureAwait(false);
            }
        }

        private List<string> CoreUrnIDs
        {
            get
            {
                if (coreUrnIDs != null) { return coreUrnIDs; }
                return coreUrnIDs = new()
                {
                    UrnId.Create<GetConversationUsers>(string.Empty),
                    UrnId.Create<GetConversationUserByID>(string.Empty),
                    UrnId.Create<GetConversationUserByKey>(string.Empty),
                    UrnId.Create<CheckConversationUserExistsByID>(string.Empty),
                    UrnId.Create<CheckConversationUserExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class ConversationUserService : ConversationUserServiceBase { }
}
