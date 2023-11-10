// <autogenerated>
// <copyright file="SalesReturnSalesOrderService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return sales order service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of sales return sales orders.</summary>
    /// <seealso cref="SalesReturnSalesOrderSearchModel"/>
    /// <seealso cref="IReturn{SalesReturnSalesOrderPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnSalesOrders", "GET", Priority = 1,
            Summary = "Use to get a list of sales return sales orders")]
    public partial class GetSalesReturnSalesOrders : SalesReturnSalesOrderSearchModel, IReturn<SalesReturnSalesOrderPagedResults> { }

    /// <summary>A ServiceStack Route to get sales return sales orders for connect.</summary>
    /// <seealso cref="SalesReturnSalesOrderSearchModel"/>
    /// <seealso cref="IReturn{List{SalesReturnSalesOrderModel}}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.View"),
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrdersForConnect", "POST,GET", Priority = 1,
            Summary = "Get all sales return sales orders")]
    public partial class GetSalesReturnSalesOrdersForConnect : SalesReturnSalesOrderSearchModel, IReturn<List<SalesReturnSalesOrderModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all sales return sales orders.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.View"),
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrdersDigest", "GET",
            Summary = "Use to get a hash representing each sales return sales orders")]
    public partial class GetSalesReturnSalesOrdersDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get sales return sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SalesReturnSalesOrderModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnSalesOrder/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales return sales order")]
    public partial class GetSalesReturnSalesOrderByID : ImplementsIDBase, IReturn<SalesReturnSalesOrderModel> { }

    /// <summary>A ServiceStack Route to get sales return sales order.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{SalesReturnSalesOrderModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific sales return sales order by the custom key")]
    public partial class GetSalesReturnSalesOrderByKey : ImplementsKeyBase, IReturn<SalesReturnSalesOrderModel> { }

    /// <summary>A ServiceStack Route to check sales return sales order exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.View"),
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnSalesOrderExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return sales order exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.View"),
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnSalesOrderExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create sales return sales order.</summary>
    /// <seealso cref="SalesReturnSalesOrderModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Create", "POST", Priority = 1,
            Summary = "Use to create a new sales return sales order.")]
    public partial class CreateSalesReturnSalesOrder : SalesReturnSalesOrderModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert sales return sales order.</summary>
    /// <seealso cref="SalesReturnSalesOrderModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing sales return sales order (as needed).")]
    public partial class UpsertSalesReturnSalesOrder : SalesReturnSalesOrderModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update sales return sales order.</summary>
    /// <seealso cref="SalesReturnSalesOrderModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing sales return sales order.")]
    public partial class UpdateSalesReturnSalesOrder : SalesReturnSalesOrderModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate sales return sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales return sales order from the system [Soft-Delete]")]
    public partial class DeactivateSalesReturnSalesOrderByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate sales return sales order by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.Deactivate"),
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales return sales order from the system [Soft-Delete]")]
    public partial class DeactivateSalesReturnSalesOrderByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales return sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales return sales order from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesReturnSalesOrderByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales return sales order by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.Reactivate"),
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales return sales order from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesReturnSalesOrderByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales return sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific sales return sales order from the system [Hard-Delete]")]
    public partial class DeleteSalesReturnSalesOrderByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales return sales order by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnSalesOrder.Delete"),
        PublicAPI,
        Route("/Returning/SalesReturnSalesOrder/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific sales return sales order from the system [Hard-Delete]")]
    public partial class DeleteSalesReturnSalesOrderByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear sales return sales order cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnSalesOrder/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all sales return sales order calls.")]
    public class ClearSalesReturnSalesOrderCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SalesReturnSalesOrderServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSalesReturnSalesOrders"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnSalesOrders request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ISalesReturnSalesOrderModel, SalesReturnSalesOrderModel, ISalesReturnSalesOrderSearchModel, SalesReturnSalesOrderPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesReturnSalesOrders)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnSalesOrdersForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSalesReturnSalesOrdersForConnect request)
        {
            return await Workflows.SalesReturnSalesOrders.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnSalesOrdersDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnSalesOrdersDigest request)
        {
            return await Workflows.SalesReturnSalesOrders.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSalesReturnSalesOrderByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnSalesOrderByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.SalesReturnSalesOrders, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnSalesOrderByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnSalesOrderByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.SalesReturnSalesOrders, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSalesReturnSalesOrderExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnSalesOrderExistsByID request)
        {
            return await Workflows.SalesReturnSalesOrders.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnSalesOrderExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnSalesOrderExistsByKey request)
        {
            return await Workflows.SalesReturnSalesOrders.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSalesReturnSalesOrder"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSalesReturnSalesOrder request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnSalesOrderDataAsync,
                    () => Workflows.SalesReturnSalesOrders.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSalesReturnSalesOrder"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSalesReturnSalesOrder request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnSalesOrderDataAsync,
                    () => Workflows.SalesReturnSalesOrders.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSalesReturnSalesOrder"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSalesReturnSalesOrder request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnSalesOrderDataAsync,
                    () => Workflows.SalesReturnSalesOrders.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSalesReturnSalesOrderByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesReturnSalesOrderByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnSalesOrderDataAsync,
                    () => Workflows.SalesReturnSalesOrders.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSalesReturnSalesOrderByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesReturnSalesOrderByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnSalesOrderDataAsync,
                    () => Workflows.SalesReturnSalesOrders.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSalesReturnSalesOrderByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesReturnSalesOrderByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnSalesOrderDataAsync,
                    () => Workflows.SalesReturnSalesOrders.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSalesReturnSalesOrderByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesReturnSalesOrderByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnSalesOrderDataAsync,
                    () => Workflows.SalesReturnSalesOrders.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSalesReturnSalesOrderByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesReturnSalesOrderByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnSalesOrderDataAsync,
                    () => Workflows.SalesReturnSalesOrders.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSalesReturnSalesOrderByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesReturnSalesOrderByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnSalesOrderDataAsync,
                    () => Workflows.SalesReturnSalesOrders.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSalesReturnSalesOrderCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSalesReturnSalesOrderCache request)
        {
            await ClearCachedSalesReturnSalesOrderDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSalesReturnSalesOrderDataAsync()
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
                    UrnId.Create<GetSalesReturnSalesOrders>(string.Empty),
                    UrnId.Create<GetSalesReturnSalesOrderByID>(string.Empty),
                    UrnId.Create<GetSalesReturnSalesOrderByKey>(string.Empty),
                    UrnId.Create<CheckSalesReturnSalesOrderExistsByID>(string.Empty),
                    UrnId.Create<CheckSalesReturnSalesOrderExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SalesReturnSalesOrderService : SalesReturnSalesOrderServiceBase { }
}
