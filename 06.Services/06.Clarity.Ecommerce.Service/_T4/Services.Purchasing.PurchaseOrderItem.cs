// <autogenerated>
// <copyright file="PurchaseOrderItemService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the purchase order item service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of purchase order items.</summary>
    /// <seealso cref="SalesItemBaseSearchModel"/>
    /// <seealso cref="IReturn{PurchaseOrderItemPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Purchasing/PurchaseOrderItems", "GET", Priority = 1,
            Summary = "Use to get a list of purchase order items")]
    public partial class GetPurchaseOrderItems : SalesItemBaseSearchModel, IReturn<PurchaseOrderItemPagedResults> { }

    /// <summary>A ServiceStack Route to get purchase order items for connect.</summary>
    /// <seealso cref="SalesItemBaseSearchModel"/>
    /// <seealso cref="IReturn{List{SalesItemBaseModel}}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.View"),
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItemsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all purchase order items")]
    public partial class GetPurchaseOrderItemsForConnect : SalesItemBaseSearchModel, IReturn<List<SalesItemBaseModel<IAppliedPurchaseOrderItemDiscountModel, AppliedPurchaseOrderItemDiscountModel>>> { }

    /// <summary>A ServiceStack Route to get a digest of all purchase order items.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.View"),
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItemsDigest", "GET",
            Summary = "Use to get a hash representing each purchase order items")]
    public partial class GetPurchaseOrderItemsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get purchase order item.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SalesItemBaseModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Purchasing/PurchaseOrderItem/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific purchase order item")]
    public partial class GetPurchaseOrderItemByID : ImplementsIDBase, IReturn<SalesItemBaseModel<IAppliedPurchaseOrderItemDiscountModel, AppliedPurchaseOrderItemDiscountModel>> { }

    /// <summary>A ServiceStack Route to get purchase order item.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{SalesItemBaseModel}"/>
    [PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific purchase order item by the custom key")]
    public partial class GetPurchaseOrderItemByKey : ImplementsKeyBase, IReturn<SalesItemBaseModel<IAppliedPurchaseOrderItemDiscountModel, AppliedPurchaseOrderItemDiscountModel>> { }

    /// <summary>A ServiceStack Route to get purchase order item.</summary>
    /// <seealso cref="IReturn{SalesItemBaseModel}"/>
    [PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Name", "GET", Priority = 1,
            Summary = "Use to get a specific purchase order item by the name")]
    public partial class GetPurchaseOrderItemByName : ImplementsNameBase, IReturn<SalesItemBaseModel<IAppliedPurchaseOrderItemDiscountModel, AppliedPurchaseOrderItemDiscountModel>> { }

    /// <summary>A ServiceStack Route to check purchase order item exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.View"),
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckPurchaseOrderItemExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check purchase order item exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.View"),
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckPurchaseOrderItemExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check purchase order item exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.View"),
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckPurchaseOrderItemExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create purchase order item.</summary>
    /// <seealso cref="SalesItemBaseModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Create", "POST", Priority = 1,
            Summary = "Use to create a new purchase order item.")]
    public partial class CreatePurchaseOrderItem : SalesItemBaseModel<IAppliedPurchaseOrderItemDiscountModel, AppliedPurchaseOrderItemDiscountModel>, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert purchase order item.</summary>
    /// <seealso cref="SalesItemBaseModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing purchase order item (as needed).")]
    public partial class UpsertPurchaseOrderItem : SalesItemBaseModel<IAppliedPurchaseOrderItemDiscountModel, AppliedPurchaseOrderItemDiscountModel>, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update purchase order item.</summary>
    /// <seealso cref="SalesItemBaseModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing purchase order item.")]
    public partial class UpdatePurchaseOrderItem : SalesItemBaseModel<IAppliedPurchaseOrderItemDiscountModel, AppliedPurchaseOrderItemDiscountModel>, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate purchase order item.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific purchase order item from the system [Soft-Delete]")]
    public partial class DeactivatePurchaseOrderItemByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate purchase order item by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.Deactivate"),
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific purchase order item from the system [Soft-Delete]")]
    public partial class DeactivatePurchaseOrderItemByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate purchase order item.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific purchase order item from the system [Restore from Soft-Delete]")]
    public partial class ReactivatePurchaseOrderItemByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate purchase order item by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.Reactivate"),
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific purchase order item from the system [Restore from Soft-Delete]")]
    public partial class ReactivatePurchaseOrderItemByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete purchase order item.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific purchase order item from the system [Hard-Delete]")]
    public partial class DeletePurchaseOrderItemByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete purchase order item by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Purchasing.PurchaseOrderItem.Delete"),
        PublicAPI,
        Route("/Purchasing/PurchaseOrderItem/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific purchase order item from the system [Hard-Delete]")]
    public partial class DeletePurchaseOrderItemByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear purchase order item cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Purchasing/PurchaseOrderItem/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all purchase order item calls.")]
    public class ClearPurchaseOrderItemCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class PurchaseOrderItemServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetPurchaseOrderItems"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetPurchaseOrderItems request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ISalesItemBaseModel<IAppliedPurchaseOrderItemDiscountModel>, SalesItemBaseModel<IAppliedPurchaseOrderItemDiscountModel, AppliedPurchaseOrderItemDiscountModel>, ISalesItemBaseSearchModel, PurchaseOrderItemPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.PurchaseOrderItems)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetPurchaseOrderItemsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetPurchaseOrderItemsForConnect request)
        {
            return await Workflows.PurchaseOrderItems.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetPurchaseOrderItemsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetPurchaseOrderItemsDigest request)
        {
            return await Workflows.PurchaseOrderItems.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetPurchaseOrderItemByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetPurchaseOrderItemByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.PurchaseOrderItems, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetPurchaseOrderItemByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetPurchaseOrderItemByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.PurchaseOrderItems, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetPurchaseOrderItemByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetPurchaseOrderItemByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.PurchaseOrderItems, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckPurchaseOrderItemExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckPurchaseOrderItemExistsByID request)
        {
            return await Workflows.PurchaseOrderItems.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckPurchaseOrderItemExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckPurchaseOrderItemExistsByKey request)
        {
            return await Workflows.PurchaseOrderItems.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckPurchaseOrderItemExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckPurchaseOrderItemExistsByName request)
        {
            return await Workflows.PurchaseOrderItems.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertPurchaseOrderItem"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertPurchaseOrderItem request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPurchaseOrderItemDataAsync,
                    () => Workflows.PurchaseOrderItems.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreatePurchaseOrderItem"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreatePurchaseOrderItem request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPurchaseOrderItemDataAsync,
                    () => Workflows.PurchaseOrderItems.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdatePurchaseOrderItem"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdatePurchaseOrderItem request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPurchaseOrderItemDataAsync,
                    () => Workflows.PurchaseOrderItems.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivatePurchaseOrderItemByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivatePurchaseOrderItemByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPurchaseOrderItemDataAsync,
                    () => Workflows.PurchaseOrderItems.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivatePurchaseOrderItemByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivatePurchaseOrderItemByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPurchaseOrderItemDataAsync,
                    () => Workflows.PurchaseOrderItems.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivatePurchaseOrderItemByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivatePurchaseOrderItemByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPurchaseOrderItemDataAsync,
                    () => Workflows.PurchaseOrderItems.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivatePurchaseOrderItemByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivatePurchaseOrderItemByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPurchaseOrderItemDataAsync,
                    () => Workflows.PurchaseOrderItems.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeletePurchaseOrderItemByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeletePurchaseOrderItemByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPurchaseOrderItemDataAsync,
                    () => Workflows.PurchaseOrderItems.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeletePurchaseOrderItemByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeletePurchaseOrderItemByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPurchaseOrderItemDataAsync,
                    () => Workflows.PurchaseOrderItems.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearPurchaseOrderItemCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearPurchaseOrderItemCache request)
        {
            await ClearCachedPurchaseOrderItemDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedPurchaseOrderItemDataAsync()
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
                    UrnId.Create<GetPurchaseOrderItems>(string.Empty),
                    UrnId.Create<GetPurchaseOrderItemByID>(string.Empty),
                    UrnId.Create<GetPurchaseOrderItemByKey>(string.Empty),
                    UrnId.Create<GetPurchaseOrderItemByName>(string.Empty),
                    UrnId.Create<CheckPurchaseOrderItemExistsByID>(string.Empty),
                    UrnId.Create<CheckPurchaseOrderItemExistsByKey>(string.Empty),
                    UrnId.Create<CheckPurchaseOrderItemExistsByName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class PurchaseOrderItemService : PurchaseOrderItemServiceBase { }
}
