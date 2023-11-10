// <autogenerated>
// <copyright file="SalesOrderEventService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order event service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of sales order events.</summary>
    /// <seealso cref="SalesOrderEventSearchModel"/>
    /// <seealso cref="IReturn{SalesOrderEventPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Ordering/SalesOrderEvents", "GET", Priority = 1,
            Summary = "Use to get a list of sales order events")]
    public partial class GetSalesOrderEvents : SalesOrderEventSearchModel, IReturn<SalesOrderEventPagedResults> { }

    /// <summary>A ServiceStack Route to get sales order events for connect.</summary>
    /// <seealso cref="SalesOrderEventSearchModel"/>
    /// <seealso cref="IReturn{List{SalesOrderEventModel}}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.View"),
        PublicAPI,
        Route("/Ordering/SalesOrderEventsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all sales order events")]
    public partial class GetSalesOrderEventsForConnect : SalesOrderEventSearchModel, IReturn<List<SalesOrderEventModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all sales order events.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.View"),
        PublicAPI,
        Route("/Ordering/SalesOrderEventsDigest", "GET",
            Summary = "Use to get a hash representing each sales order events")]
    public partial class GetSalesOrderEventsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get sales order event.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SalesOrderEventModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Ordering/SalesOrderEvent/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales order event")]
    public partial class GetSalesOrderEventByID : ImplementsIDBase, IReturn<SalesOrderEventModel> { }

    /// <summary>A ServiceStack Route to get sales order event.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{SalesOrderEventModel}"/>
    [PublicAPI,
        Route("/Ordering/SalesOrderEvent/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific sales order event by the custom key")]
    public partial class GetSalesOrderEventByKey : ImplementsKeyBase, IReturn<SalesOrderEventModel> { }

    /// <summary>A ServiceStack Route to get sales order event.</summary>
    /// <seealso cref="IReturn{SalesOrderEventModel}"/>
    [PublicAPI,
        Route("/Ordering/SalesOrderEvent/Name", "GET", Priority = 1,
            Summary = "Use to get a specific sales order event by the name")]
    public partial class GetSalesOrderEventByName : ImplementsNameBase, IReturn<SalesOrderEventModel> { }

    /// <summary>A ServiceStack Route to check sales order event exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.View"),
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesOrderEventExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales order event exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.View"),
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesOrderEventExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales order event exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.View"),
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesOrderEventExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create sales order event.</summary>
    /// <seealso cref="SalesOrderEventModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Create", "POST", Priority = 1,
            Summary = "Use to create a new sales order event.")]
    public partial class CreateSalesOrderEvent : SalesOrderEventModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert sales order event.</summary>
    /// <seealso cref="SalesOrderEventModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing sales order event (as needed).")]
    public partial class UpsertSalesOrderEvent : SalesOrderEventModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update sales order event.</summary>
    /// <seealso cref="SalesOrderEventModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing sales order event.")]
    public partial class UpdateSalesOrderEvent : SalesOrderEventModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate sales order event.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales order event from the system [Soft-Delete]")]
    public partial class DeactivateSalesOrderEventByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate sales order event by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.Deactivate"),
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales order event from the system [Soft-Delete]")]
    public partial class DeactivateSalesOrderEventByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales order event.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales order event from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesOrderEventByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales order event by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.Reactivate"),
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales order event from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesOrderEventByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales order event.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific sales order event from the system [Hard-Delete]")]
    public partial class DeleteSalesOrderEventByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales order event by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrderEvent.Delete"),
        PublicAPI,
        Route("/Ordering/SalesOrderEvent/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific sales order event from the system [Hard-Delete]")]
    public partial class DeleteSalesOrderEventByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear sales order event cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Ordering/SalesOrderEvent/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all sales order event calls.")]
    public class ClearSalesOrderEventCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SalesOrderEventServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSalesOrderEvents"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesOrderEvents request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ISalesOrderEventModel, SalesOrderEventModel, ISalesOrderEventSearchModel, SalesOrderEventPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesOrderEvents)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesOrderEventsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSalesOrderEventsForConnect request)
        {
            return await Workflows.SalesOrderEvents.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesOrderEventsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesOrderEventsDigest request)
        {
            return await Workflows.SalesOrderEvents.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSalesOrderEventByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesOrderEventByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.SalesOrderEvents, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesOrderEventByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesOrderEventByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.SalesOrderEvents, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesOrderEventByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesOrderEventByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.SalesOrderEvents, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSalesOrderEventExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesOrderEventExistsByID request)
        {
            return await Workflows.SalesOrderEvents.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesOrderEventExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesOrderEventExistsByKey request)
        {
            return await Workflows.SalesOrderEvents.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesOrderEventExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesOrderEventExistsByName request)
        {
            return await Workflows.SalesOrderEvents.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSalesOrderEvent"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSalesOrderEvent request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderEventDataAsync,
                    () => Workflows.SalesOrderEvents.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSalesOrderEvent"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSalesOrderEvent request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderEventDataAsync,
                    () => Workflows.SalesOrderEvents.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSalesOrderEvent"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSalesOrderEvent request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderEventDataAsync,
                    () => Workflows.SalesOrderEvents.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSalesOrderEventByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesOrderEventByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderEventDataAsync,
                    () => Workflows.SalesOrderEvents.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSalesOrderEventByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesOrderEventByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderEventDataAsync,
                    () => Workflows.SalesOrderEvents.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSalesOrderEventByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesOrderEventByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderEventDataAsync,
                    () => Workflows.SalesOrderEvents.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSalesOrderEventByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesOrderEventByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderEventDataAsync,
                    () => Workflows.SalesOrderEvents.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSalesOrderEventByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesOrderEventByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderEventDataAsync,
                    () => Workflows.SalesOrderEvents.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSalesOrderEventByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesOrderEventByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderEventDataAsync,
                    () => Workflows.SalesOrderEvents.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSalesOrderEventCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSalesOrderEventCache request)
        {
            await ClearCachedSalesOrderEventDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSalesOrderEventDataAsync()
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
                    UrnId.Create<GetSalesOrderEvents>(string.Empty),
                    UrnId.Create<GetSalesOrderEventByID>(string.Empty),
                    UrnId.Create<GetSalesOrderEventByKey>(string.Empty),
                    UrnId.Create<GetSalesOrderEventByName>(string.Empty),
                    UrnId.Create<CheckSalesOrderEventExistsByID>(string.Empty),
                    UrnId.Create<CheckSalesOrderEventExistsByKey>(string.Empty),
                    UrnId.Create<CheckSalesOrderEventExistsByName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SalesOrderEventService : SalesOrderEventServiceBase { }
}
