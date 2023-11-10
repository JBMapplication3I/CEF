// <autogenerated>
// <copyright file="SalesReturnStatusService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return status service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of sales return statuses.</summary>
    /// <seealso cref="StatusSearchModel"/>
    /// <seealso cref="IReturn{SalesReturnStatusPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnStatuses", "GET", Priority = 1,
            Summary = "Use to get a list of sales return statuses")]
    public partial class GetSalesReturnStatuses : StatusSearchModel, IReturn<SalesReturnStatusPagedResults> { }

    /// <summary>A ServiceStack Route to get sales return statuses for connect.</summary>
    /// <seealso cref="StatusSearchModel"/>
    /// <seealso cref="IReturn{List{StatusModel}}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.View"),
        PublicAPI,
        Route("/Returning/SalesReturnStatusesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all sales return statuses")]
    public partial class GetSalesReturnStatusesForConnect : StatusSearchModel, IReturn<List<StatusModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all sales return statuses.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.View"),
        PublicAPI,
        Route("/Returning/SalesReturnStatusesDigest", "GET",
            Summary = "Use to get a hash representing each sales return statuses")]
    public partial class GetSalesReturnStatusesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get sales return status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnStatus/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales return status")]
    public partial class GetSalesReturnStatusByID : ImplementsIDBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to get sales return status.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnStatus/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific sales return status by the custom key")]
    public partial class GetSalesReturnStatusByKey : ImplementsKeyBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to get sales return status.</summary>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnStatus/Name", "GET", Priority = 1,
            Summary = "Use to get a specific sales return status by the name")]
    public partial class GetSalesReturnStatusByName : ImplementsNameBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to get sales return status.</summary>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnStatus/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific sales return status by the name")]
    public partial class GetSalesReturnStatusByDisplayName : ImplementsDisplayNameBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to check sales return status exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.View"),
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnStatusExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return status exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.View"),
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnStatusExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return status exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.View"),
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnStatusExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return status exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.View"),
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnStatusExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create sales return status.</summary>
    /// <seealso cref="StatusModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Create", "POST", Priority = 1,
            Summary = "Use to create a new sales return status.")]
    public partial class CreateSalesReturnStatus : StatusModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert sales return status.</summary>
    /// <seealso cref="StatusModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing sales return status (as needed).")]
    public partial class UpsertSalesReturnStatus : StatusModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update sales return status.</summary>
    /// <seealso cref="StatusModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing sales return status.")]
    public partial class UpdateSalesReturnStatus : StatusModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate sales return status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales return status from the system [Soft-Delete]")]
    public partial class DeactivateSalesReturnStatusByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate sales return status by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.Deactivate"),
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales return status from the system [Soft-Delete]")]
    public partial class DeactivateSalesReturnStatusByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales return status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales return status from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesReturnStatusByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales return status by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.Reactivate"),
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales return status from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesReturnStatusByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales return status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific sales return status from the system [Hard-Delete]")]
    public partial class DeleteSalesReturnStatusByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales return status by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnStatus.Delete"),
        PublicAPI,
        Route("/Returning/SalesReturnStatus/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific sales return status from the system [Hard-Delete]")]
    public partial class DeleteSalesReturnStatusByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear sales return status cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnStatus/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all sales return status calls.")]
    public class ClearSalesReturnStatusCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SalesReturnStatusServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSalesReturnStatuses"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnStatuses request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IStatusModel, StatusModel, IStatusSearchModel, SalesReturnStatusPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesReturnStatuses)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnStatusesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSalesReturnStatusesForConnect request)
        {
            return await Workflows.SalesReturnStatuses.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnStatusesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnStatusesDigest request)
        {
            return await Workflows.SalesReturnStatuses.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSalesReturnStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnStatusByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.SalesReturnStatuses, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnStatusByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.SalesReturnStatuses, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnStatusByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnStatusByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.SalesReturnStatuses, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnStatusByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnStatusByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.SalesReturnStatuses, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSalesReturnStatusExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnStatusExistsByID request)
        {
            return await Workflows.SalesReturnStatuses.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnStatusExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnStatusExistsByKey request)
        {
            return await Workflows.SalesReturnStatuses.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnStatusExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnStatusExistsByName request)
        {
            return await Workflows.SalesReturnStatuses.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnStatusExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnStatusExistsByDisplayName request)
        {
            return await Workflows.SalesReturnStatuses.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSalesReturnStatus"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSalesReturnStatus request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnStatusDataAsync,
                    () => Workflows.SalesReturnStatuses.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSalesReturnStatus"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSalesReturnStatus request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnStatusDataAsync,
                    () => Workflows.SalesReturnStatuses.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSalesReturnStatus"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSalesReturnStatus request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnStatusDataAsync,
                    () => Workflows.SalesReturnStatuses.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSalesReturnStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesReturnStatusByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnStatusDataAsync,
                    () => Workflows.SalesReturnStatuses.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSalesReturnStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesReturnStatusByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnStatusDataAsync,
                    () => Workflows.SalesReturnStatuses.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSalesReturnStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesReturnStatusByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnStatusDataAsync,
                    () => Workflows.SalesReturnStatuses.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSalesReturnStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesReturnStatusByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnStatusDataAsync,
                    () => Workflows.SalesReturnStatuses.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSalesReturnStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesReturnStatusByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnStatusDataAsync,
                    () => Workflows.SalesReturnStatuses.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSalesReturnStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesReturnStatusByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnStatusDataAsync,
                    () => Workflows.SalesReturnStatuses.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSalesReturnStatusCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSalesReturnStatusCache request)
        {
            await ClearCachedSalesReturnStatusDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSalesReturnStatusDataAsync()
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
                    UrnId.Create<GetSalesReturnStatuses>(string.Empty),
                    UrnId.Create<GetSalesReturnStatusByID>(string.Empty),
                    UrnId.Create<GetSalesReturnStatusByKey>(string.Empty),
                    UrnId.Create<GetSalesReturnStatusByName>(string.Empty),
                    UrnId.Create<CheckSalesReturnStatusExistsByID>(string.Empty),
                    UrnId.Create<CheckSalesReturnStatusExistsByKey>(string.Empty),
                    UrnId.Create<CheckSalesReturnStatusExistsByName>(string.Empty),
                    UrnId.Create<CheckSalesReturnStatusExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SalesReturnStatusService : SalesReturnStatusServiceBase { }
}
