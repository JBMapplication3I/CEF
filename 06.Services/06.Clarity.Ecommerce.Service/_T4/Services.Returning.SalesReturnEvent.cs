// <autogenerated>
// <copyright file="SalesReturnEventService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return event service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of sales return events.</summary>
    /// <seealso cref="SalesReturnEventSearchModel"/>
    /// <seealso cref="IReturn{SalesReturnEventPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnEvents", "GET", Priority = 1,
            Summary = "Use to get a list of sales return events")]
    public partial class GetSalesReturnEvents : SalesReturnEventSearchModel, IReturn<SalesReturnEventPagedResults> { }

    /// <summary>A ServiceStack Route to get sales return events for connect.</summary>
    /// <seealso cref="SalesReturnEventSearchModel"/>
    /// <seealso cref="IReturn{List{SalesReturnEventModel}}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.View"),
        PublicAPI,
        Route("/Returning/SalesReturnEventsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all sales return events")]
    public partial class GetSalesReturnEventsForConnect : SalesReturnEventSearchModel, IReturn<List<SalesReturnEventModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all sales return events.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.View"),
        PublicAPI,
        Route("/Returning/SalesReturnEventsDigest", "GET",
            Summary = "Use to get a hash representing each sales return events")]
    public partial class GetSalesReturnEventsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get sales return event.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SalesReturnEventModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnEvent/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales return event")]
    public partial class GetSalesReturnEventByID : ImplementsIDBase, IReturn<SalesReturnEventModel> { }

    /// <summary>A ServiceStack Route to get sales return event.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{SalesReturnEventModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnEvent/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific sales return event by the custom key")]
    public partial class GetSalesReturnEventByKey : ImplementsKeyBase, IReturn<SalesReturnEventModel> { }

    /// <summary>A ServiceStack Route to get sales return event.</summary>
    /// <seealso cref="IReturn{SalesReturnEventModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnEvent/Name", "GET", Priority = 1,
            Summary = "Use to get a specific sales return event by the name")]
    public partial class GetSalesReturnEventByName : ImplementsNameBase, IReturn<SalesReturnEventModel> { }

    /// <summary>A ServiceStack Route to check sales return event exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.View"),
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnEventExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return event exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.View"),
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnEventExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return event exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.View"),
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnEventExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create sales return event.</summary>
    /// <seealso cref="SalesReturnEventModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Create", "POST", Priority = 1,
            Summary = "Use to create a new sales return event.")]
    public partial class CreateSalesReturnEvent : SalesReturnEventModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert sales return event.</summary>
    /// <seealso cref="SalesReturnEventModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing sales return event (as needed).")]
    public partial class UpsertSalesReturnEvent : SalesReturnEventModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update sales return event.</summary>
    /// <seealso cref="SalesReturnEventModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing sales return event.")]
    public partial class UpdateSalesReturnEvent : SalesReturnEventModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate sales return event.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales return event from the system [Soft-Delete]")]
    public partial class DeactivateSalesReturnEventByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate sales return event by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.Deactivate"),
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales return event from the system [Soft-Delete]")]
    public partial class DeactivateSalesReturnEventByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales return event.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales return event from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesReturnEventByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales return event by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.Reactivate"),
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales return event from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesReturnEventByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales return event.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific sales return event from the system [Hard-Delete]")]
    public partial class DeleteSalesReturnEventByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales return event by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnEvent.Delete"),
        PublicAPI,
        Route("/Returning/SalesReturnEvent/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific sales return event from the system [Hard-Delete]")]
    public partial class DeleteSalesReturnEventByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear sales return event cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnEvent/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all sales return event calls.")]
    public class ClearSalesReturnEventCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SalesReturnEventServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSalesReturnEvents"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnEvents request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ISalesReturnEventModel, SalesReturnEventModel, ISalesReturnEventSearchModel, SalesReturnEventPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesReturnEvents)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnEventsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSalesReturnEventsForConnect request)
        {
            return await Workflows.SalesReturnEvents.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnEventsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnEventsDigest request)
        {
            return await Workflows.SalesReturnEvents.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSalesReturnEventByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnEventByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.SalesReturnEvents, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnEventByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnEventByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.SalesReturnEvents, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnEventByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnEventByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.SalesReturnEvents, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSalesReturnEventExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnEventExistsByID request)
        {
            return await Workflows.SalesReturnEvents.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnEventExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnEventExistsByKey request)
        {
            return await Workflows.SalesReturnEvents.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnEventExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnEventExistsByName request)
        {
            return await Workflows.SalesReturnEvents.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSalesReturnEvent"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSalesReturnEvent request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnEventDataAsync,
                    () => Workflows.SalesReturnEvents.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSalesReturnEvent"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSalesReturnEvent request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnEventDataAsync,
                    () => Workflows.SalesReturnEvents.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSalesReturnEvent"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSalesReturnEvent request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnEventDataAsync,
                    () => Workflows.SalesReturnEvents.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSalesReturnEventByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesReturnEventByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnEventDataAsync,
                    () => Workflows.SalesReturnEvents.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSalesReturnEventByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesReturnEventByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnEventDataAsync,
                    () => Workflows.SalesReturnEvents.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSalesReturnEventByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesReturnEventByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnEventDataAsync,
                    () => Workflows.SalesReturnEvents.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSalesReturnEventByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesReturnEventByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnEventDataAsync,
                    () => Workflows.SalesReturnEvents.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSalesReturnEventByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesReturnEventByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnEventDataAsync,
                    () => Workflows.SalesReturnEvents.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSalesReturnEventByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesReturnEventByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnEventDataAsync,
                    () => Workflows.SalesReturnEvents.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSalesReturnEventCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSalesReturnEventCache request)
        {
            await ClearCachedSalesReturnEventDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSalesReturnEventDataAsync()
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
                    UrnId.Create<GetSalesReturnEvents>(string.Empty),
                    UrnId.Create<GetSalesReturnEventByID>(string.Empty),
                    UrnId.Create<GetSalesReturnEventByKey>(string.Empty),
                    UrnId.Create<GetSalesReturnEventByName>(string.Empty),
                    UrnId.Create<CheckSalesReturnEventExistsByID>(string.Empty),
                    UrnId.Create<CheckSalesReturnEventExistsByKey>(string.Empty),
                    UrnId.Create<CheckSalesReturnEventExistsByName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SalesReturnEventService : SalesReturnEventServiceBase { }
}
