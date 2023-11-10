// <autogenerated>
// <copyright file="ProductImageService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product image service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of product images.</summary>
    /// <seealso cref="ProductImageSearchModel"/>
    /// <seealso cref="IReturn{ProductImagePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Products/ProductImages", "GET", Priority = 1,
            Summary = "Use to get a list of product images")]
    public partial class GetProductImages : ProductImageSearchModel, IReturn<ProductImagePagedResults> { }

    /// <summary>A ServiceStack Route to get product images for connect.</summary>
    /// <seealso cref="ProductImageSearchModel"/>
    /// <seealso cref="IReturn{List{ProductImageModel}}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.View"),
        PublicAPI,
        Route("/Products/ProductImagesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all product images")]
    public partial class GetProductImagesForConnect : ProductImageSearchModel, IReturn<List<ProductImageModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all product images.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.View"),
        PublicAPI,
        Route("/Products/ProductImagesDigest", "GET",
            Summary = "Use to get a hash representing each product images")]
    public partial class GetProductImagesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get product image.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{ProductImageModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Products/ProductImage/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific product image")]
    public partial class GetProductImageByID : ImplementsIDBase, IReturn<ProductImageModel> { }

    /// <summary>A ServiceStack Route to get product image.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{ProductImageModel}"/>
    [PublicAPI,
        Route("/Products/ProductImage/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific product image by the custom key")]
    public partial class GetProductImageByKey : ImplementsKeyBase, IReturn<ProductImageModel> { }

    /// <summary>A ServiceStack Route to get product image.</summary>
    /// <seealso cref="IReturn{ProductImageModel}"/>
    [PublicAPI,
        Route("/Products/ProductImage/Name", "GET", Priority = 1,
            Summary = "Use to get a specific product image by the name")]
    public partial class GetProductImageByName : ImplementsNameBase, IReturn<ProductImageModel> { }

    /// <summary>A ServiceStack Route to check product image exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.View"),
        PublicAPI,
        Route("/Products/ProductImage/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckProductImageExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check product image exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.View"),
        PublicAPI,
        Route("/Products/ProductImage/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckProductImageExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check product image exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.View"),
        PublicAPI,
        Route("/Products/ProductImage/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckProductImageExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create product image.</summary>
    /// <seealso cref="ProductImageModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Products/ProductImage/Create", "POST", Priority = 1,
            Summary = "Use to create a new product image.")]
    public partial class CreateProductImage : ProductImageModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert product image.</summary>
    /// <seealso cref="ProductImageModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Products/ProductImage/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing product image (as needed).")]
    public partial class UpsertProductImage : ProductImageModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update product image.</summary>
    /// <seealso cref="ProductImageModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Products/ProductImage/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing product image.")]
    public partial class UpdateProductImage : ProductImageModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate product image.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Products/ProductImage/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific product image from the system [Soft-Delete]")]
    public partial class DeactivateProductImageByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate product image by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.Deactivate"),
        PublicAPI,
        Route("/Products/ProductImage/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific product image from the system [Soft-Delete]")]
    public partial class DeactivateProductImageByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate product image.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Products/ProductImage/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific product image from the system [Restore from Soft-Delete]")]
    public partial class ReactivateProductImageByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate product image by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.Reactivate"),
        PublicAPI,
        Route("/Products/ProductImage/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific product image from the system [Restore from Soft-Delete]")]
    public partial class ReactivateProductImageByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete product image.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Products/ProductImage/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific product image from the system [Hard-Delete]")]
    public partial class DeleteProductImageByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete product image by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Products.ProductImage.Delete"),
        PublicAPI,
        Route("/Products/ProductImage/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific product image from the system [Hard-Delete]")]
    public partial class DeleteProductImageByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear product image cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Products/ProductImage/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all product image calls.")]
    public class ClearProductImageCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class ProductImageServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetProductImages"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetProductImages request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IProductImageModel, ProductImageModel, IProductImageSearchModel, ProductImagePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.ProductImages)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetProductImagesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetProductImagesForConnect request)
        {
            return await Workflows.ProductImages.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetProductImagesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetProductImagesDigest request)
        {
            return await Workflows.ProductImages.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetProductImageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetProductImageByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.ProductImages, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetProductImageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetProductImageByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.ProductImages, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetProductImageByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetProductImageByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.ProductImages, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckProductImageExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckProductImageExistsByID request)
        {
            return await Workflows.ProductImages.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckProductImageExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckProductImageExistsByKey request)
        {
            return await Workflows.ProductImages.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckProductImageExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckProductImageExistsByName request)
        {
            return await Workflows.ProductImages.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertProductImage"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertProductImage request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductImageDataAsync,
                    () => Workflows.ProductImages.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateProductImage"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateProductImage request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductImageDataAsync,
                    () => Workflows.ProductImages.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateProductImage"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateProductImage request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductImageDataAsync,
                    () => Workflows.ProductImages.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateProductImageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateProductImageByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductImageDataAsync,
                    () => Workflows.ProductImages.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateProductImageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateProductImageByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductImageDataAsync,
                    () => Workflows.ProductImages.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateProductImageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateProductImageByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductImageDataAsync,
                    () => Workflows.ProductImages.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateProductImageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateProductImageByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductImageDataAsync,
                    () => Workflows.ProductImages.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteProductImageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteProductImageByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductImageDataAsync,
                    () => Workflows.ProductImages.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteProductImageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteProductImageByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedProductImageDataAsync,
                    () => Workflows.ProductImages.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearProductImageCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearProductImageCache request)
        {
            await ClearCachedProductImageDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedProductImageDataAsync()
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
                    UrnId.Create<GetProductImages>(string.Empty),
                    UrnId.Create<GetProductImageByID>(string.Empty),
                    UrnId.Create<GetProductImageByKey>(string.Empty),
                    UrnId.Create<GetProductImageByName>(string.Empty),
                    UrnId.Create<CheckProductImageExistsByID>(string.Empty),
                    UrnId.Create<CheckProductImageExistsByKey>(string.Empty),
                    UrnId.Create<CheckProductImageExistsByName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class ProductImageService : ProductImageServiceBase { }
}
