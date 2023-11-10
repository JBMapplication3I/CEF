// <autogenerated>
// <copyright file="SubscriptionStatusService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription status service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of subscription statuses.</summary>
    /// <seealso cref="StatusSearchModel"/>
    /// <seealso cref="IReturn{SubscriptionStatusPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/SubscriptionStatuses", "GET", Priority = 1,
            Summary = "Use to get a list of subscription statuses")]
    public partial class GetSubscriptionStatuses : StatusSearchModel, IReturn<SubscriptionStatusPagedResults> { }

    /// <summary>A ServiceStack Route to get subscription statuses for connect.</summary>
    /// <seealso cref="StatusSearchModel"/>
    /// <seealso cref="IReturn{List{StatusModel}}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.View"),
        PublicAPI,
        Route("/Payments/SubscriptionStatusesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all subscription statuses")]
    public partial class GetSubscriptionStatusesForConnect : StatusSearchModel, IReturn<List<StatusModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all subscription statuses.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.View"),
        PublicAPI,
        Route("/Payments/SubscriptionStatusesDigest", "GET",
            Summary = "Use to get a hash representing each subscription statuses")]
    public partial class GetSubscriptionStatusesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get subscription status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/SubscriptionStatus/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific subscription status")]
    public partial class GetSubscriptionStatusByID : ImplementsIDBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to get subscription status.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        Route("/Payments/SubscriptionStatus/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific subscription status by the custom key")]
    public partial class GetSubscriptionStatusByKey : ImplementsKeyBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to get subscription status.</summary>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        Route("/Payments/SubscriptionStatus/Name", "GET", Priority = 1,
            Summary = "Use to get a specific subscription status by the name")]
    public partial class GetSubscriptionStatusByName : ImplementsNameBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to get subscription status.</summary>
    /// <seealso cref="IReturn{StatusModel}"/>
    [PublicAPI,
        Route("/Payments/SubscriptionStatus/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific subscription status by the name")]
    public partial class GetSubscriptionStatusByDisplayName : ImplementsDisplayNameBase, IReturn<StatusModel> { }

    /// <summary>A ServiceStack Route to check subscription status exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.View"),
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSubscriptionStatusExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check subscription status exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.View"),
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSubscriptionStatusExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check subscription status exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.View"),
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSubscriptionStatusExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check subscription status exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.View"),
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSubscriptionStatusExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create subscription status.</summary>
    /// <seealso cref="StatusModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Create", "POST", Priority = 1,
            Summary = "Use to create a new subscription status.")]
    public partial class CreateSubscriptionStatus : StatusModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert subscription status.</summary>
    /// <seealso cref="StatusModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing subscription status (as needed).")]
    public partial class UpsertSubscriptionStatus : StatusModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update subscription status.</summary>
    /// <seealso cref="StatusModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing subscription status.")]
    public partial class UpdateSubscriptionStatus : StatusModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate subscription status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific subscription status from the system [Soft-Delete]")]
    public partial class DeactivateSubscriptionStatusByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate subscription status by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.Deactivate"),
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific subscription status from the system [Soft-Delete]")]
    public partial class DeactivateSubscriptionStatusByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate subscription status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific subscription status from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSubscriptionStatusByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate subscription status by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.Reactivate"),
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific subscription status from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSubscriptionStatusByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete subscription status.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific subscription status from the system [Hard-Delete]")]
    public partial class DeleteSubscriptionStatusByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete subscription status by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.SubscriptionStatus.Delete"),
        PublicAPI,
        Route("/Payments/SubscriptionStatus/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific subscription status from the system [Hard-Delete]")]
    public partial class DeleteSubscriptionStatusByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear subscription status cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/SubscriptionStatus/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all subscription status calls.")]
    public class ClearSubscriptionStatusCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SubscriptionStatusServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSubscriptionStatuses"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSubscriptionStatuses request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IStatusModel, StatusModel, IStatusSearchModel, SubscriptionStatusPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SubscriptionStatuses)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSubscriptionStatusesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSubscriptionStatusesForConnect request)
        {
            return await Workflows.SubscriptionStatuses.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSubscriptionStatusesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSubscriptionStatusesDigest request)
        {
            return await Workflows.SubscriptionStatuses.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSubscriptionStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSubscriptionStatusByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.SubscriptionStatuses, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSubscriptionStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSubscriptionStatusByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.SubscriptionStatuses, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSubscriptionStatusByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSubscriptionStatusByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.SubscriptionStatuses, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSubscriptionStatusByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSubscriptionStatusByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.SubscriptionStatuses, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSubscriptionStatusExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSubscriptionStatusExistsByID request)
        {
            return await Workflows.SubscriptionStatuses.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSubscriptionStatusExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSubscriptionStatusExistsByKey request)
        {
            return await Workflows.SubscriptionStatuses.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSubscriptionStatusExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSubscriptionStatusExistsByName request)
        {
            return await Workflows.SubscriptionStatuses.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSubscriptionStatusExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSubscriptionStatusExistsByDisplayName request)
        {
            return await Workflows.SubscriptionStatuses.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSubscriptionStatus"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSubscriptionStatus request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSubscriptionStatusDataAsync,
                    () => Workflows.SubscriptionStatuses.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSubscriptionStatus"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSubscriptionStatus request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSubscriptionStatusDataAsync,
                    () => Workflows.SubscriptionStatuses.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSubscriptionStatus"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSubscriptionStatus request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSubscriptionStatusDataAsync,
                    () => Workflows.SubscriptionStatuses.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSubscriptionStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSubscriptionStatusByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSubscriptionStatusDataAsync,
                    () => Workflows.SubscriptionStatuses.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSubscriptionStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSubscriptionStatusByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSubscriptionStatusDataAsync,
                    () => Workflows.SubscriptionStatuses.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSubscriptionStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSubscriptionStatusByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSubscriptionStatusDataAsync,
                    () => Workflows.SubscriptionStatuses.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSubscriptionStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSubscriptionStatusByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSubscriptionStatusDataAsync,
                    () => Workflows.SubscriptionStatuses.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSubscriptionStatusByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSubscriptionStatusByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSubscriptionStatusDataAsync,
                    () => Workflows.SubscriptionStatuses.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSubscriptionStatusByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSubscriptionStatusByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSubscriptionStatusDataAsync,
                    () => Workflows.SubscriptionStatuses.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSubscriptionStatusCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSubscriptionStatusCache request)
        {
            await ClearCachedSubscriptionStatusDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSubscriptionStatusDataAsync()
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
                    UrnId.Create<GetSubscriptionStatuses>(string.Empty),
                    UrnId.Create<GetSubscriptionStatusByID>(string.Empty),
                    UrnId.Create<GetSubscriptionStatusByKey>(string.Empty),
                    UrnId.Create<GetSubscriptionStatusByName>(string.Empty),
                    UrnId.Create<CheckSubscriptionStatusExistsByID>(string.Empty),
                    UrnId.Create<CheckSubscriptionStatusExistsByKey>(string.Empty),
                    UrnId.Create<CheckSubscriptionStatusExistsByName>(string.Empty),
                    UrnId.Create<CheckSubscriptionStatusExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SubscriptionStatusService : SubscriptionStatusServiceBase { }
}
