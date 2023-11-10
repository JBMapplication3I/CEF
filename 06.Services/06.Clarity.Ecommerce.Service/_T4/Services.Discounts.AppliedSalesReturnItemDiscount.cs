// <autogenerated>
// <copyright file="AppliedSalesReturnItemDiscountService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied sales return item discount service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of applied sales return item discounts.</summary>
    /// <seealso cref="AppliedSalesReturnItemDiscountSearchModel"/>
    /// <seealso cref="IReturn{AppliedSalesReturnItemDiscountPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Discounts/AppliedSalesReturnItemDiscounts", "GET", Priority = 1,
            Summary = "Use to get a list of applied sales return item discounts")]
    public partial class GetAppliedSalesReturnItemDiscounts : AppliedSalesReturnItemDiscountSearchModel, IReturn<AppliedSalesReturnItemDiscountPagedResults> { }

    /// <summary>A ServiceStack Route to get applied sales return item discounts for connect.</summary>
    /// <seealso cref="AppliedSalesReturnItemDiscountSearchModel"/>
    /// <seealso cref="IReturn{List{AppliedSalesReturnItemDiscountModel}}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.View"),
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscountsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all applied sales return item discounts")]
    public partial class GetAppliedSalesReturnItemDiscountsForConnect : AppliedSalesReturnItemDiscountSearchModel, IReturn<List<AppliedSalesReturnItemDiscountModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all applied sales return item discounts.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.View"),
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscountsDigest", "GET",
            Summary = "Use to get a hash representing each applied sales return item discounts")]
    public partial class GetAppliedSalesReturnItemDiscountsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get applied sales return item discount.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{AppliedSalesReturnItemDiscountModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Discounts/AppliedSalesReturnItemDiscount/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific applied sales return item discount")]
    public partial class GetAppliedSalesReturnItemDiscountByID : ImplementsIDBase, IReturn<AppliedSalesReturnItemDiscountModel> { }

    /// <summary>A ServiceStack Route to get applied sales return item discount.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{AppliedSalesReturnItemDiscountModel}"/>
    [PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific applied sales return item discount by the custom key")]
    public partial class GetAppliedSalesReturnItemDiscountByKey : ImplementsKeyBase, IReturn<AppliedSalesReturnItemDiscountModel> { }

    /// <summary>A ServiceStack Route to check applied sales return item discount exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.View"),
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckAppliedSalesReturnItemDiscountExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check applied sales return item discount exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.View"),
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckAppliedSalesReturnItemDiscountExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create applied sales return item discount.</summary>
    /// <seealso cref="AppliedSalesReturnItemDiscountModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Create", "POST", Priority = 1,
            Summary = "Use to create a new applied sales return item discount.")]
    public partial class CreateAppliedSalesReturnItemDiscount : AppliedSalesReturnItemDiscountModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert applied sales return item discount.</summary>
    /// <seealso cref="AppliedSalesReturnItemDiscountModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing applied sales return item discount (as needed).")]
    public partial class UpsertAppliedSalesReturnItemDiscount : AppliedSalesReturnItemDiscountModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update applied sales return item discount.</summary>
    /// <seealso cref="AppliedSalesReturnItemDiscountModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing applied sales return item discount.")]
    public partial class UpdateAppliedSalesReturnItemDiscount : AppliedSalesReturnItemDiscountModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate applied sales return item discount.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific applied sales return item discount from the system [Soft-Delete]")]
    public partial class DeactivateAppliedSalesReturnItemDiscountByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate applied sales return item discount by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.Deactivate"),
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific applied sales return item discount from the system [Soft-Delete]")]
    public partial class DeactivateAppliedSalesReturnItemDiscountByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate applied sales return item discount.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific applied sales return item discount from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAppliedSalesReturnItemDiscountByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate applied sales return item discount by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.Reactivate"),
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific applied sales return item discount from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAppliedSalesReturnItemDiscountByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete applied sales return item discount.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific applied sales return item discount from the system [Hard-Delete]")]
    public partial class DeleteAppliedSalesReturnItemDiscountByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete applied sales return item discount by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.AppliedSalesReturnItemDiscount.Delete"),
        PublicAPI,
        Route("/Discounts/AppliedSalesReturnItemDiscount/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific applied sales return item discount from the system [Hard-Delete]")]
    public partial class DeleteAppliedSalesReturnItemDiscountByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear applied sales return item discount cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Discounts/AppliedSalesReturnItemDiscount/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all applied sales return item discount calls.")]
    public class ClearAppliedSalesReturnItemDiscountCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class AppliedSalesReturnItemDiscountServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetAppliedSalesReturnItemDiscounts"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAppliedSalesReturnItemDiscounts request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IAppliedSalesReturnItemDiscountModel, AppliedSalesReturnItemDiscountModel, IAppliedSalesReturnItemDiscountSearchModel, AppliedSalesReturnItemDiscountPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.AppliedSalesReturnItemDiscounts)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAppliedSalesReturnItemDiscountsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetAppliedSalesReturnItemDiscountsForConnect request)
        {
            return await Workflows.AppliedSalesReturnItemDiscounts.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAppliedSalesReturnItemDiscountsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAppliedSalesReturnItemDiscountsDigest request)
        {
            return await Workflows.AppliedSalesReturnItemDiscounts.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetAppliedSalesReturnItemDiscountByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAppliedSalesReturnItemDiscountByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.AppliedSalesReturnItemDiscounts, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAppliedSalesReturnItemDiscountByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAppliedSalesReturnItemDiscountByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.AppliedSalesReturnItemDiscounts, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckAppliedSalesReturnItemDiscountExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAppliedSalesReturnItemDiscountExistsByID request)
        {
            return await Workflows.AppliedSalesReturnItemDiscounts.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAppliedSalesReturnItemDiscountExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAppliedSalesReturnItemDiscountExistsByKey request)
        {
            return await Workflows.AppliedSalesReturnItemDiscounts.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertAppliedSalesReturnItemDiscount"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertAppliedSalesReturnItemDiscount request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAppliedSalesReturnItemDiscountDataAsync,
                    () => Workflows.AppliedSalesReturnItemDiscounts.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateAppliedSalesReturnItemDiscount"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateAppliedSalesReturnItemDiscount request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAppliedSalesReturnItemDiscountDataAsync,
                    () => Workflows.AppliedSalesReturnItemDiscounts.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateAppliedSalesReturnItemDiscount"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateAppliedSalesReturnItemDiscount request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAppliedSalesReturnItemDiscountDataAsync,
                    () => Workflows.AppliedSalesReturnItemDiscounts.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateAppliedSalesReturnItemDiscountByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAppliedSalesReturnItemDiscountByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAppliedSalesReturnItemDiscountDataAsync,
                    () => Workflows.AppliedSalesReturnItemDiscounts.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateAppliedSalesReturnItemDiscountByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAppliedSalesReturnItemDiscountByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAppliedSalesReturnItemDiscountDataAsync,
                    () => Workflows.AppliedSalesReturnItemDiscounts.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateAppliedSalesReturnItemDiscountByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAppliedSalesReturnItemDiscountByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAppliedSalesReturnItemDiscountDataAsync,
                    () => Workflows.AppliedSalesReturnItemDiscounts.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateAppliedSalesReturnItemDiscountByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAppliedSalesReturnItemDiscountByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAppliedSalesReturnItemDiscountDataAsync,
                    () => Workflows.AppliedSalesReturnItemDiscounts.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteAppliedSalesReturnItemDiscountByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAppliedSalesReturnItemDiscountByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAppliedSalesReturnItemDiscountDataAsync,
                    () => Workflows.AppliedSalesReturnItemDiscounts.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteAppliedSalesReturnItemDiscountByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAppliedSalesReturnItemDiscountByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAppliedSalesReturnItemDiscountDataAsync,
                    () => Workflows.AppliedSalesReturnItemDiscounts.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearAppliedSalesReturnItemDiscountCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearAppliedSalesReturnItemDiscountCache request)
        {
            await ClearCachedAppliedSalesReturnItemDiscountDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedAppliedSalesReturnItemDiscountDataAsync()
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
                    UrnId.Create<GetAppliedSalesReturnItemDiscounts>(string.Empty),
                    UrnId.Create<GetAppliedSalesReturnItemDiscountByID>(string.Empty),
                    UrnId.Create<GetAppliedSalesReturnItemDiscountByKey>(string.Empty),
                    UrnId.Create<CheckAppliedSalesReturnItemDiscountExistsByID>(string.Empty),
                    UrnId.Create<CheckAppliedSalesReturnItemDiscountExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class AppliedSalesReturnItemDiscountService : AppliedSalesReturnItemDiscountServiceBase { }
}
