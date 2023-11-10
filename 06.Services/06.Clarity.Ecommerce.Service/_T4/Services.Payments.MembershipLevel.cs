// <autogenerated>
// <copyright file="MembershipLevelService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership level service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of membership levels.</summary>
    /// <seealso cref="MembershipLevelSearchModel"/>
    /// <seealso cref="IReturn{MembershipLevelPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/MembershipLevels", "GET", Priority = 1,
            Summary = "Use to get a list of membership levels")]
    public partial class GetMembershipLevels : MembershipLevelSearchModel, IReturn<MembershipLevelPagedResults> { }

    /// <summary>A ServiceStack Route to get membership levels for connect.</summary>
    /// <seealso cref="MembershipLevelSearchModel"/>
    /// <seealso cref="IReturn{List{MembershipLevelModel}}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.View"),
        PublicAPI,
        Route("/Payments/MembershipLevelsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all membership levels")]
    public partial class GetMembershipLevelsForConnect : MembershipLevelSearchModel, IReturn<List<MembershipLevelModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all membership levels.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.View"),
        PublicAPI,
        Route("/Payments/MembershipLevelsDigest", "GET",
            Summary = "Use to get a hash representing each membership levels")]
    public partial class GetMembershipLevelsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get membership level.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{MembershipLevelModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/MembershipLevel/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific membership level")]
    public partial class GetMembershipLevelByID : ImplementsIDBase, IReturn<MembershipLevelModel> { }

    /// <summary>A ServiceStack Route to get membership level.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{MembershipLevelModel}"/>
    [PublicAPI,
        Route("/Payments/MembershipLevel/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific membership level by the custom key")]
    public partial class GetMembershipLevelByKey : ImplementsKeyBase, IReturn<MembershipLevelModel> { }

    /// <summary>A ServiceStack Route to get membership level.</summary>
    /// <seealso cref="IReturn{MembershipLevelModel}"/>
    [PublicAPI,
        Route("/Payments/MembershipLevel/Name", "GET", Priority = 1,
            Summary = "Use to get a specific membership level by the name")]
    public partial class GetMembershipLevelByName : ImplementsNameBase, IReturn<MembershipLevelModel> { }

    /// <summary>A ServiceStack Route to get membership level.</summary>
    /// <seealso cref="IReturn{MembershipLevelModel}"/>
    [PublicAPI,
        Route("/Payments/MembershipLevel/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific membership level by the name")]
    public partial class GetMembershipLevelByDisplayName : ImplementsDisplayNameBase, IReturn<MembershipLevelModel> { }

    /// <summary>A ServiceStack Route to check membership level exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.View"),
        PublicAPI,
        Route("/Payments/MembershipLevel/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckMembershipLevelExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check membership level exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.View"),
        PublicAPI,
        Route("/Payments/MembershipLevel/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckMembershipLevelExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check membership level exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.View"),
        PublicAPI,
        Route("/Payments/MembershipLevel/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckMembershipLevelExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check membership level exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.View"),
        PublicAPI,
        Route("/Payments/MembershipLevel/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckMembershipLevelExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create membership level.</summary>
    /// <seealso cref="MembershipLevelModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/MembershipLevel/Create", "POST", Priority = 1,
            Summary = "Use to create a new membership level.")]
    public partial class CreateMembershipLevel : MembershipLevelModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert membership level.</summary>
    /// <seealso cref="MembershipLevelModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Payments/MembershipLevel/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing membership level (as needed).")]
    public partial class UpsertMembershipLevel : MembershipLevelModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update membership level.</summary>
    /// <seealso cref="MembershipLevelModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/MembershipLevel/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing membership level.")]
    public partial class UpdateMembershipLevel : MembershipLevelModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate membership level.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/MembershipLevel/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific membership level from the system [Soft-Delete]")]
    public partial class DeactivateMembershipLevelByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate membership level by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.Deactivate"),
        PublicAPI,
        Route("/Payments/MembershipLevel/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific membership level from the system [Soft-Delete]")]
    public partial class DeactivateMembershipLevelByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate membership level.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/MembershipLevel/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific membership level from the system [Restore from Soft-Delete]")]
    public partial class ReactivateMembershipLevelByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate membership level by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.Reactivate"),
        PublicAPI,
        Route("/Payments/MembershipLevel/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific membership level from the system [Restore from Soft-Delete]")]
    public partial class ReactivateMembershipLevelByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete membership level.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/MembershipLevel/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific membership level from the system [Hard-Delete]")]
    public partial class DeleteMembershipLevelByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete membership level by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.MembershipLevel.Delete"),
        PublicAPI,
        Route("/Payments/MembershipLevel/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific membership level from the system [Hard-Delete]")]
    public partial class DeleteMembershipLevelByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear membership level cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/MembershipLevel/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all membership level calls.")]
    public class ClearMembershipLevelCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class MembershipLevelServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetMembershipLevels"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetMembershipLevels request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IMembershipLevelModel, MembershipLevelModel, IMembershipLevelSearchModel, MembershipLevelPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.MembershipLevels)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetMembershipLevelsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetMembershipLevelsForConnect request)
        {
            return await Workflows.MembershipLevels.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetMembershipLevelsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetMembershipLevelsDigest request)
        {
            return await Workflows.MembershipLevels.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetMembershipLevelByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetMembershipLevelByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.MembershipLevels, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetMembershipLevelByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetMembershipLevelByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.MembershipLevels, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetMembershipLevelByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetMembershipLevelByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.MembershipLevels, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetMembershipLevelByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetMembershipLevelByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.MembershipLevels, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckMembershipLevelExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckMembershipLevelExistsByID request)
        {
            return await Workflows.MembershipLevels.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckMembershipLevelExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckMembershipLevelExistsByKey request)
        {
            return await Workflows.MembershipLevels.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckMembershipLevelExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckMembershipLevelExistsByName request)
        {
            return await Workflows.MembershipLevels.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckMembershipLevelExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckMembershipLevelExistsByDisplayName request)
        {
            return await Workflows.MembershipLevels.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertMembershipLevel"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertMembershipLevel request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipLevelDataAsync,
                    () => Workflows.MembershipLevels.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateMembershipLevel"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateMembershipLevel request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipLevelDataAsync,
                    () => Workflows.MembershipLevels.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateMembershipLevel"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateMembershipLevel request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipLevelDataAsync,
                    () => Workflows.MembershipLevels.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateMembershipLevelByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateMembershipLevelByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipLevelDataAsync,
                    () => Workflows.MembershipLevels.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateMembershipLevelByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateMembershipLevelByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipLevelDataAsync,
                    () => Workflows.MembershipLevels.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateMembershipLevelByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateMembershipLevelByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipLevelDataAsync,
                    () => Workflows.MembershipLevels.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateMembershipLevelByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateMembershipLevelByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipLevelDataAsync,
                    () => Workflows.MembershipLevels.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteMembershipLevelByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteMembershipLevelByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipLevelDataAsync,
                    () => Workflows.MembershipLevels.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteMembershipLevelByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteMembershipLevelByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedMembershipLevelDataAsync,
                    () => Workflows.MembershipLevels.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearMembershipLevelCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearMembershipLevelCache request)
        {
            await ClearCachedMembershipLevelDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedMembershipLevelDataAsync()
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
                    UrnId.Create<GetMembershipLevels>(string.Empty),
                    UrnId.Create<GetMembershipLevelByID>(string.Empty),
                    UrnId.Create<GetMembershipLevelByKey>(string.Empty),
                    UrnId.Create<GetMembershipLevelByName>(string.Empty),
                    UrnId.Create<CheckMembershipLevelExistsByID>(string.Empty),
                    UrnId.Create<CheckMembershipLevelExistsByKey>(string.Empty),
                    UrnId.Create<CheckMembershipLevelExistsByName>(string.Empty),
                    UrnId.Create<CheckMembershipLevelExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class MembershipLevelService : MembershipLevelServiceBase { }
}
