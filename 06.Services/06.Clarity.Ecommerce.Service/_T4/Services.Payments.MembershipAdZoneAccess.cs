// <autogenerated>
// <copyright file="MembershipAdZoneAccessService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership ad zone access service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of membership ad zone accesses.</summary>
    /// <seealso cref="MembershipAdZoneAccessSearchModel"/>
    /// <seealso cref="IReturn{MembershipAdZoneAccessPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/MembershipAdZoneAccesses", "GET", Priority = 1,
            Summary = "Use to get a list of membership ad zone accesses")]
    public partial class GetMembershipAdZoneAccesses : MembershipAdZoneAccessSearchModel, IReturn<MembershipAdZoneAccessPagedResults> { }

    /// <summary>A ServiceStack Route to get membership ad zone accesses for connect.</summary>
    /// <seealso cref="MembershipAdZoneAccessSearchModel"/>
    /// <seealso cref="IReturn{List{MembershipAdZoneAccessModel}}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.View"),
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccessesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all membership ad zone accesses")]
    public partial class GetMembershipAdZoneAccessesForConnect : MembershipAdZoneAccessSearchModel, IReturn<List<MembershipAdZoneAccessModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all membership ad zone accesses.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.View"),
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccessesDigest", "GET",
            Summary = "Use to get a hash representing each membership ad zone accesses")]
    public partial class GetMembershipAdZoneAccessesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get membership ad zone access.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{MembershipAdZoneAccessModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/MembershipAdZoneAccess/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific membership ad zone access")]
    public partial class GetMembershipAdZoneAccessByID : ImplementsIDBase, IReturn<MembershipAdZoneAccessModel> { }

    /// <summary>A ServiceStack Route to get membership ad zone access.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{MembershipAdZoneAccessModel}"/>
    [PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific membership ad zone access by the custom key")]
    public partial class GetMembershipAdZoneAccessByKey : ImplementsKeyBase, IReturn<MembershipAdZoneAccessModel> { }

    /// <summary>A ServiceStack Route to check membership ad zone access exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.View"),
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckMembershipAdZoneAccessExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check membership ad zone access exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.View"),
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckMembershipAdZoneAccessExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create membership ad zone access.</summary>
    /// <seealso cref="MembershipAdZoneAccessModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Create", "POST", Priority = 1,
            Summary = "Use to create a new membership ad zone access.")]
    public partial class CreateMembershipAdZoneAccess : MembershipAdZoneAccessModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert membership ad zone access.</summary>
    /// <seealso cref="MembershipAdZoneAccessModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing membership ad zone access (as needed).")]
    public partial class UpsertMembershipAdZoneAccess : MembershipAdZoneAccessModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update membership ad zone access.</summary>
    /// <seealso cref="MembershipAdZoneAccessModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing membership ad zone access.")]
    public partial class UpdateMembershipAdZoneAccess : MembershipAdZoneAccessModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate membership ad zone access.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific membership ad zone access from the system [Soft-Delete]")]
    public partial class DeactivateMembershipAdZoneAccessByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate membership ad zone access by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.Deactivate"),
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific membership ad zone access from the system [Soft-Delete]")]
    public partial class DeactivateMembershipAdZoneAccessByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate membership ad zone access.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific membership ad zone access from the system [Restore from Soft-Delete]")]
    public partial class ReactivateMembershipAdZoneAccessByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate membership ad zone access by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.Reactivate"),
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific membership ad zone access from the system [Restore from Soft-Delete]")]
    public partial class ReactivateMembershipAdZoneAccessByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete membership ad zone access.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific membership ad zone access from the system [Hard-Delete]")]
    public partial class DeleteMembershipAdZoneAccessByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete membership ad zone access by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipAdZoneAccess.Delete"),
        PublicAPI,
        Route("/Payments/MembershipAdZoneAccess/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific membership ad zone access from the system [Hard-Delete]")]
    public partial class DeleteMembershipAdZoneAccessByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear membership ad zone access cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/MembershipAdZoneAccess/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all membership ad zone access calls.")]
    public class ClearMembershipAdZoneAccessCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class MembershipAdZoneAccessServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetMembershipAdZoneAccesses"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetMembershipAdZoneAccesses request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IMembershipAdZoneAccessModel, MembershipAdZoneAccessModel, IMembershipAdZoneAccessSearchModel, MembershipAdZoneAccessPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.MembershipAdZoneAccesses)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetMembershipAdZoneAccessesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetMembershipAdZoneAccessesForConnect request)
        {
            return await Workflows.MembershipAdZoneAccesses.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetMembershipAdZoneAccessesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetMembershipAdZoneAccessesDigest request)
        {
            return await Workflows.MembershipAdZoneAccesses.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetMembershipAdZoneAccessByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetMembershipAdZoneAccessByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.MembershipAdZoneAccesses, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetMembershipAdZoneAccessByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetMembershipAdZoneAccessByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.MembershipAdZoneAccesses, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckMembershipAdZoneAccessExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckMembershipAdZoneAccessExistsByID request)
        {
            return await Workflows.MembershipAdZoneAccesses.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckMembershipAdZoneAccessExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckMembershipAdZoneAccessExistsByKey request)
        {
            return await Workflows.MembershipAdZoneAccesses.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertMembershipAdZoneAccess"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertMembershipAdZoneAccess request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipAdZoneAccessDataAsync,
                    () => Workflows.MembershipAdZoneAccesses.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateMembershipAdZoneAccess"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateMembershipAdZoneAccess request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipAdZoneAccessDataAsync,
                    () => Workflows.MembershipAdZoneAccesses.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateMembershipAdZoneAccess"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateMembershipAdZoneAccess request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipAdZoneAccessDataAsync,
                    () => Workflows.MembershipAdZoneAccesses.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateMembershipAdZoneAccessByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateMembershipAdZoneAccessByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipAdZoneAccessDataAsync,
                    () => Workflows.MembershipAdZoneAccesses.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateMembershipAdZoneAccessByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateMembershipAdZoneAccessByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipAdZoneAccessDataAsync,
                    () => Workflows.MembershipAdZoneAccesses.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateMembershipAdZoneAccessByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateMembershipAdZoneAccessByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipAdZoneAccessDataAsync,
                    () => Workflows.MembershipAdZoneAccesses.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateMembershipAdZoneAccessByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateMembershipAdZoneAccessByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipAdZoneAccessDataAsync,
                    () => Workflows.MembershipAdZoneAccesses.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteMembershipAdZoneAccessByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteMembershipAdZoneAccessByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipAdZoneAccessDataAsync,
                    () => Workflows.MembershipAdZoneAccesses.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteMembershipAdZoneAccessByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteMembershipAdZoneAccessByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipAdZoneAccessDataAsync,
                    () => Workflows.MembershipAdZoneAccesses.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearMembershipAdZoneAccessCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearMembershipAdZoneAccessCache request)
        {
            await ClearCachedMembershipAdZoneAccessDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedMembershipAdZoneAccessDataAsync()
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
                    UrnId.Create<GetMembershipAdZoneAccesses>(string.Empty),
                    UrnId.Create<GetMembershipAdZoneAccessByID>(string.Empty),
                    UrnId.Create<GetMembershipAdZoneAccessByKey>(string.Empty),
                    UrnId.Create<CheckMembershipAdZoneAccessExistsByID>(string.Empty),
                    UrnId.Create<CheckMembershipAdZoneAccessExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class MembershipAdZoneAccessService : MembershipAdZoneAccessServiceBase { }
}
