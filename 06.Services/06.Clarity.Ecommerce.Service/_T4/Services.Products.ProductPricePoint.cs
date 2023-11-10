// <autogenerated>
// <copyright file="ProductPricePointService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product price point service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of product price points.</summary>
    /// <seealso cref="ProductPricePointSearchModel"/>
    /// <seealso cref="IReturn{ProductPricePointPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Products/ProductPricePoints", "GET", Priority = 1,
            Summary = "Use to get a list of product price points")]
    public partial class GetProductPricePoints : ProductPricePointSearchModel, IReturn<ProductPricePointPagedResults> { }

    /// <summary>A ServiceStack Route to get product price points for connect.</summary>
    /// <seealso cref="ProductPricePointSearchModel"/>
    /// <seealso cref="IReturn{List{ProductPricePointModel}}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.View"),
        PublicAPI,
        Route("/Products/ProductPricePointsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all product price points")]
    public partial class GetProductPricePointsForConnect : ProductPricePointSearchModel, IReturn<List<ProductPricePointModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all product price points.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.View"),
        PublicAPI,
        Route("/Products/ProductPricePointsDigest", "GET",
            Summary = "Use to get a hash representing each product price points")]
    public partial class GetProductPricePointsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get product price point.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{ProductPricePointModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Products/ProductPricePoint/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific product price point")]
    public partial class GetProductPricePointByID : ImplementsIDBase, IReturn<ProductPricePointModel> { }

    /// <summary>A ServiceStack Route to get product price point.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{ProductPricePointModel}"/>
    [PublicAPI,
        Route("/Products/ProductPricePoint/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific product price point by the custom key")]
    public partial class GetProductPricePointByKey : ImplementsKeyBase, IReturn<ProductPricePointModel> { }

    /// <summary>A ServiceStack Route to check product price point exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.View"),
        PublicAPI,
        Route("/Products/ProductPricePoint/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckProductPricePointExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check product price point exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.View"),
        PublicAPI,
        Route("/Products/ProductPricePoint/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckProductPricePointExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create product price point.</summary>
    /// <seealso cref="ProductPricePointModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Products/ProductPricePoint/Create", "POST", Priority = 1,
            Summary = "Use to create a new product price point.")]
    public partial class CreateProductPricePoint : ProductPricePointModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert product price point.</summary>
    /// <seealso cref="ProductPricePointModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Products/ProductPricePoint/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing product price point (as needed).")]
    public partial class UpsertProductPricePoint : ProductPricePointModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update product price point.</summary>
    /// <seealso cref="ProductPricePointModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Products/ProductPricePoint/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing product price point.")]
    public partial class UpdateProductPricePoint : ProductPricePointModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate product price point.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Products/ProductPricePoint/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific product price point from the system [Soft-Delete]")]
    public partial class DeactivateProductPricePointByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate product price point by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.Deactivate"),
        PublicAPI,
        Route("/Products/ProductPricePoint/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific product price point from the system [Soft-Delete]")]
    public partial class DeactivateProductPricePointByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate product price point.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Products/ProductPricePoint/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific product price point from the system [Restore from Soft-Delete]")]
    public partial class ReactivateProductPricePointByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate product price point by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.Reactivate"),
        PublicAPI,
        Route("/Products/ProductPricePoint/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific product price point from the system [Restore from Soft-Delete]")]
    public partial class ReactivateProductPricePointByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete product price point.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Products/ProductPricePoint/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific product price point from the system [Hard-Delete]")]
    public partial class DeleteProductPricePointByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete product price point by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductPricePoint.Delete"),
        PublicAPI,
        Route("/Products/ProductPricePoint/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific product price point from the system [Hard-Delete]")]
    public partial class DeleteProductPricePointByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear product price point cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Products/ProductPricePoint/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all product price point calls.")]
    public class ClearProductPricePointCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class ProductPricePointServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetProductPricePoints"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetProductPricePoints request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IProductPricePointModel, ProductPricePointModel, IProductPricePointSearchModel, ProductPricePointPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.ProductPricePoints)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetProductPricePointsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetProductPricePointsForConnect request)
        {
            return await Workflows.ProductPricePoints.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetProductPricePointsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetProductPricePointsDigest request)
        {
            return await Workflows.ProductPricePoints.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetProductPricePointByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetProductPricePointByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.ProductPricePoints, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetProductPricePointByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetProductPricePointByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.ProductPricePoints, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckProductPricePointExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckProductPricePointExistsByID request)
        {
            return await Workflows.ProductPricePoints.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckProductPricePointExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckProductPricePointExistsByKey request)
        {
            return await Workflows.ProductPricePoints.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertProductPricePoint"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertProductPricePoint request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductPricePointDataAsync,
                    () => Workflows.ProductPricePoints.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateProductPricePoint"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateProductPricePoint request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductPricePointDataAsync,
                    () => Workflows.ProductPricePoints.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateProductPricePoint"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateProductPricePoint request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductPricePointDataAsync,
                    () => Workflows.ProductPricePoints.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateProductPricePointByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateProductPricePointByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductPricePointDataAsync,
                    () => Workflows.ProductPricePoints.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateProductPricePointByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateProductPricePointByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductPricePointDataAsync,
                    () => Workflows.ProductPricePoints.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateProductPricePointByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateProductPricePointByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductPricePointDataAsync,
                    () => Workflows.ProductPricePoints.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateProductPricePointByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateProductPricePointByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductPricePointDataAsync,
                    () => Workflows.ProductPricePoints.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteProductPricePointByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteProductPricePointByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductPricePointDataAsync,
                    () => Workflows.ProductPricePoints.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteProductPricePointByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteProductPricePointByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductPricePointDataAsync,
                    () => Workflows.ProductPricePoints.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearProductPricePointCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearProductPricePointCache request)
        {
            await ClearCachedProductPricePointDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedProductPricePointDataAsync()
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
                    UrnId.Create<GetProductPricePoints>(string.Empty),
                    UrnId.Create<GetProductPricePointByID>(string.Empty),
                    UrnId.Create<GetProductPricePointByKey>(string.Empty),
                    UrnId.Create<CheckProductPricePointExistsByID>(string.Empty),
                    UrnId.Create<CheckProductPricePointExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class ProductPricePointService : ProductPricePointServiceBase { }
}
