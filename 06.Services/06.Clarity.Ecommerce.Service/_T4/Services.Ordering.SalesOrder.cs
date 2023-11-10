// <autogenerated>
// <copyright file="SalesOrderService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of sales orders.</summary>
    /// <seealso cref="SalesOrderSearchModel"/>
    /// <seealso cref="IReturn{SalesOrderPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Ordering/SalesOrders", "GET", Priority = 1,
            Summary = "Use to get a list of sales orders")]
    public partial class GetSalesOrders : SalesOrderSearchModel, IReturn<SalesOrderPagedResults> { }

    /// <summary>A ServiceStack Route to get sales orders for connect.</summary>
    /// <seealso cref="SalesOrderSearchModel"/>
    /// <seealso cref="IReturn{List{SalesOrderModel}}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.View"),
        PublicAPI,
        Route("/Ordering/SalesOrdersForConnect", "POST,GET", Priority = 1,
            Summary = "Get all sales orders")]
    public partial class GetSalesOrdersForConnect : SalesOrderSearchModel, IReturn<List<SalesOrderModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all sales orders.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.View"),
        PublicAPI,
        Route("/Ordering/SalesOrdersDigest", "GET",
            Summary = "Use to get a hash representing each sales orders")]
    public partial class GetSalesOrdersDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SalesOrderModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Ordering/SalesOrder/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales order")]
    public partial class GetSalesOrderByID : ImplementsIDBase, IReturn<SalesOrderModel> { }

    /// <summary>A ServiceStack Route to get sales order.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{SalesOrderModel}"/>
    [PublicAPI,
        Route("/Ordering/SalesOrder/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific sales order by the custom key")]
    public partial class GetSalesOrderByKey : ImplementsKeyBase, IReturn<SalesOrderModel> { }

    /// <summary>A ServiceStack Route to check sales order exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.View"),
        PublicAPI,
        Route("/Ordering/SalesOrder/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesOrderExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales order exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.View"),
        PublicAPI,
        Route("/Ordering/SalesOrder/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesOrderExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create sales order.</summary>
    /// <seealso cref="SalesOrderModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Ordering/SalesOrder/Create", "POST", Priority = 1,
            Summary = "Use to create a new sales order.")]
    public partial class CreateSalesOrder : SalesOrderModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert sales order.</summary>
    /// <seealso cref="SalesOrderModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Ordering/SalesOrder/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing sales order (as needed).")]
    public partial class UpsertSalesOrder : SalesOrderModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update sales order.</summary>
    /// <seealso cref="SalesOrderModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Ordering/SalesOrder/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing sales order.")]
    public partial class UpdateSalesOrder : SalesOrderModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Ordering/SalesOrder/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales order from the system [Soft-Delete]")]
    public partial class DeactivateSalesOrderByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate sales order by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.Deactivate"),
        PublicAPI,
        Route("/Ordering/SalesOrder/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales order from the system [Soft-Delete]")]
    public partial class DeactivateSalesOrderByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Ordering/SalesOrder/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales order from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesOrderByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales order by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.Reactivate"),
        PublicAPI,
        Route("/Ordering/SalesOrder/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales order from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesOrderByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Ordering/SalesOrder/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific sales order from the system [Hard-Delete]")]
    public partial class DeleteSalesOrderByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales order by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Ordering.SalesOrder.Delete"),
        PublicAPI,
        Route("/Ordering/SalesOrder/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific sales order from the system [Hard-Delete]")]
    public partial class DeleteSalesOrderByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear sales order cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Ordering/SalesOrder/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all sales order calls.")]
    public class ClearSalesOrderCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SalesOrderServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSalesOrders"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesOrders request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ISalesOrderModel, SalesOrderModel, ISalesOrderSearchModel, SalesOrderPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesOrders)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesOrdersForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSalesOrdersForConnect request)
        {
            return await Workflows.SalesOrders.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesOrdersDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesOrdersDigest request)
        {
            return await Workflows.SalesOrders.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSalesOrderByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesOrderByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.SalesOrders, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesOrderByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesOrderByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.SalesOrders, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSalesOrderExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesOrderExistsByID request)
        {
            return await Workflows.SalesOrders.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesOrderExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesOrderExistsByKey request)
        {
            return await Workflows.SalesOrders.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSalesOrder"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSalesOrder request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderDataAsync,
                    () => Workflows.SalesOrders.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSalesOrder"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSalesOrder request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderDataAsync,
                    () => Workflows.SalesOrders.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSalesOrder"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSalesOrder request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderDataAsync,
                    () => Workflows.SalesOrders.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSalesOrderByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesOrderByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderDataAsync,
                    () => Workflows.SalesOrders.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSalesOrderByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesOrderByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderDataAsync,
                    () => Workflows.SalesOrders.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSalesOrderByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesOrderByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderDataAsync,
                    () => Workflows.SalesOrders.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSalesOrderByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesOrderByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderDataAsync,
                    () => Workflows.SalesOrders.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSalesOrderByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesOrderByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderDataAsync,
                    () => Workflows.SalesOrders.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSalesOrderByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesOrderByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesOrderDataAsync,
                    () => Workflows.SalesOrders.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSalesOrderCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSalesOrderCache request)
        {
            await ClearCachedSalesOrderDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSalesOrderDataAsync()
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
                    UrnId.Create<GetSalesOrders>(string.Empty),
                    UrnId.Create<GetSalesOrderByID>(string.Empty),
                    UrnId.Create<GetSalesOrderByKey>(string.Empty),
                    UrnId.Create<CheckSalesOrderExistsByID>(string.Empty),
                    UrnId.Create<CheckSalesOrderExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SalesOrderService : SalesOrderServiceBase { }
}
