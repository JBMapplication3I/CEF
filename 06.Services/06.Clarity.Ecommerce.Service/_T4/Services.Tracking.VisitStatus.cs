// <autogenerated>
// <copyright file="VisitStatusService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the visit status service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of visit statuses.</summary>
    /// <seealso cref="StatusSearchModel"/>
    /// <seealso cref="IReturn{VisitStatusPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Tracking/VisitStatuses", "GET", Priority = 1,
            Summary = "Use to get a list of visit statuses")]
    public partial class GetVisitStatuses : StatusSearchModel, IReturn<VisitStatusPagedResults> { }

    /// <summary>A ServiceStack Route to get visit statuses for connect.</summary>
    /// <seealso cref="StatusSearchModel"/>
    /// <seealso cref="IReturn{List{StatusModel}}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.View"),
        PublicAPI,
        Route("/Tracking/VisitStatusesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all visit statuses")]
    public partial class GetVisitStatusesForConnect : StatusSearchModel, IReturn<List<StatusModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all visit statuses.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.View"),
        PublicAPI,
        Route("/Tracking/VisitStatusesDigest", "GET",
            Summary = "Use to get a hash representing each visit statuses")]
    public partial class GetVisitStatusesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get visit status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Tracking/VisitStatus/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific visit status")]
    public partial class GetVisitStatusByID : ImplementsIDBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to get visit status.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        Route("/Tracking/VisitStatus/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific visit status by the custom key")]
    public partial class GetVisitStatusByKey : ImplementsKeyBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to get visit status.</summary>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        Route("/Tracking/VisitStatus/Name", "GET", Priority = 1,
            Summary = "Use to get a specific visit status by the name")]
    public partial class GetVisitStatusByName : ImplementsNameBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to get visit status.</summary>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        Route("/Tracking/VisitStatus/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific visit status by the name")]
    public partial class GetVisitStatusByDisplayName : ImplementsDisplayNameBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to check visit status exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.View"),
        PublicAPI,
        Route("/Tracking/VisitStatus/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckVisitStatusExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check visit status exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.View"),
        PublicAPI,
        Route("/Tracking/VisitStatus/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckVisitStatusExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check visit status exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.View"),
        PublicAPI,
        Route("/Tracking/VisitStatus/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckVisitStatusExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check visit status exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.View"),
        PublicAPI,
        Route("/Tracking/VisitStatus/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckVisitStatusExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create visit status.</summary>
    /// <seealso cref="StatusModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Tracking/VisitStatus/Create", "POST", Priority = 1,
            Summary = "Use to create a new visit status.")]
    public partial class CreateVisitStatus : StatusModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert visit status.</summary>
    /// <seealso cref="StatusModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Tracking/VisitStatus/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing visit status (as needed).")]
    public partial class UpsertVisitStatus : StatusModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update visit status.</summary>
    /// <seealso cref="StatusModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Tracking/VisitStatus/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing visit status.")]
    public partial class UpdateVisitStatus : StatusModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate visit status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Tracking/VisitStatus/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific visit status from the system [Soft-Delete]")]
    public partial class DeactivateVisitStatusByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate visit status by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.Deactivate"),
        PublicAPI,
        Route("/Tracking/VisitStatus/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific visit status from the system [Soft-Delete]")]
    public partial class DeactivateVisitStatusByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate visit status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Tracking/VisitStatus/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific visit status from the system [Restore from Soft-Delete]")]
    public partial class ReactivateVisitStatusByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate visit status by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.Reactivate"),
        PublicAPI,
        Route("/Tracking/VisitStatus/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific visit status from the system [Restore from Soft-Delete]")]
    public partial class ReactivateVisitStatusByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete visit status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Tracking/VisitStatus/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific visit status from the system [Hard-Delete]")]
    public partial class DeleteVisitStatusByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete visit status by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tracking.VisitStatus.Delete"),
        PublicAPI,
        Route("/Tracking/VisitStatus/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific visit status from the system [Hard-Delete]")]
    public partial class DeleteVisitStatusByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear visit status cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Tracking/VisitStatus/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all visit status calls.")]
    public class ClearVisitStatusCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class VisitStatusServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetVisitStatuses"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetVisitStatuses request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IStatusModel, StatusModel, IStatusSearchModel, VisitStatusPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.VisitStatuses)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetVisitStatusesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetVisitStatusesForConnect request)
        {
            return await Workflows.VisitStatuses.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetVisitStatusesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetVisitStatusesDigest request)
        {
            return await Workflows.VisitStatuses.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetVisitStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetVisitStatusByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.VisitStatuses, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetVisitStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetVisitStatusByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.VisitStatuses, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetVisitStatusByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetVisitStatusByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.VisitStatuses, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetVisitStatusByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetVisitStatusByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.VisitStatuses, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckVisitStatusExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckVisitStatusExistsByID request)
        {
            return await Workflows.VisitStatuses.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckVisitStatusExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckVisitStatusExistsByKey request)
        {
            return await Workflows.VisitStatuses.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckVisitStatusExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckVisitStatusExistsByName request)
        {
            return await Workflows.VisitStatuses.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckVisitStatusExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckVisitStatusExistsByDisplayName request)
        {
            return await Workflows.VisitStatuses.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertVisitStatus"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertVisitStatus request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitStatusDataAsync,
                    () => Workflows.VisitStatuses.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateVisitStatus"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateVisitStatus request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitStatusDataAsync,
                    () => Workflows.VisitStatuses.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateVisitStatus"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateVisitStatus request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitStatusDataAsync,
                    () => Workflows.VisitStatuses.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateVisitStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateVisitStatusByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitStatusDataAsync,
                    () => Workflows.VisitStatuses.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateVisitStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateVisitStatusByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitStatusDataAsync,
                    () => Workflows.VisitStatuses.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateVisitStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateVisitStatusByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitStatusDataAsync,
                    () => Workflows.VisitStatuses.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateVisitStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateVisitStatusByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitStatusDataAsync,
                    () => Workflows.VisitStatuses.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteVisitStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteVisitStatusByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitStatusDataAsync,
                    () => Workflows.VisitStatuses.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteVisitStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteVisitStatusByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitStatusDataAsync,
                    () => Workflows.VisitStatuses.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearVisitStatusCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearVisitStatusCache request)
        {
            await ClearCachedVisitStatusDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedVisitStatusDataAsync()
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
                    UrnId.Create<GetVisitStatuses>(string.Empty),
                    UrnId.Create<GetVisitStatusByID>(string.Empty),
                    UrnId.Create<GetVisitStatusByKey>(string.Empty),
                    UrnId.Create<GetVisitStatusByName>(string.Empty),
                    UrnId.Create<CheckVisitStatusExistsByID>(string.Empty),
                    UrnId.Create<CheckVisitStatusExistsByKey>(string.Empty),
                    UrnId.Create<CheckVisitStatusExistsByName>(string.Empty),
                    UrnId.Create<CheckVisitStatusExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class VisitStatusService : VisitStatusServiceBase { }
}
