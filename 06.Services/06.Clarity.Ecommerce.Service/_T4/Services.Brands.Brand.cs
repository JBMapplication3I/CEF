// <autogenerated>
// <copyright file="BrandService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of brands.</summary>
    /// <seealso cref="BrandSearchModel"/>
    /// <seealso cref="IReturn{BrandPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/Brands", "GET", Priority = 1,
            Summary = "Use to get a list of brands")]
    public partial class GetBrands : BrandSearchModel, IReturn<BrandPagedResults> { }

    /// <summary>A ServiceStack Route to get brands for connect.</summary>
    /// <seealso cref="BrandSearchModel"/>
    /// <seealso cref="IReturn{List{BrandModel}}"/>
    [Authenticate, RequiredPermission("Brands.Brand.View"),
        PublicAPI,
        Route("/Brands/BrandsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all brands")]
    public partial class GetBrandsForConnect : BrandSearchModel, IReturn<List<BrandModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all brands.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Brands.Brand.View"),
        PublicAPI,
        Route("/Brands/BrandsDigest", "GET",
            Summary = "Use to get a hash representing each brands")]
    public partial class GetBrandsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get brand.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{BrandModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/Brand/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific brand")]
    public partial class GetBrandByID : ImplementsIDBase, IReturn<BrandModel> { }

    /// <summary>A ServiceStack Route to get brand.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{BrandModel}"/>
    [PublicAPI,
        Route("/Brands/Brand/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific brand by the custom key")]
    public partial class GetBrandByKey : ImplementsKeyBase, IReturn<BrandModel> { }

    /// <summary>A ServiceStack Route to get brand.</summary>
    /// <seealso cref="IReturn{BrandModel}"/>
    [PublicAPI,
        Route("/Brands/Brand/Name", "GET", Priority = 1,
            Summary = "Use to get a specific brand by the name")]
    public partial class GetBrandByName : ImplementsNameBase, IReturn<BrandModel> { }

    /// <summary>A ServiceStack Route to check brand exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Brands.Brand.View"),
        PublicAPI,
        Route("/Brands/Brand/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckBrandExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check brand exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Brands.Brand.View"),
        PublicAPI,
        Route("/Brands/Brand/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckBrandExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check brand exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Brands.Brand.View"),
        PublicAPI,
        Route("/Brands/Brand/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckBrandExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create brand.</summary>
    /// <seealso cref="BrandModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Brands.Brand.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/Brand/Create", "POST", Priority = 1,
            Summary = "Use to create a new brand.")]
    public partial class CreateBrand : BrandModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert brand.</summary>
    /// <seealso cref="BrandModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Brands/Brand/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing brand (as needed).")]
    public partial class UpsertBrand : BrandModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update brand.</summary>
    /// <seealso cref="BrandModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Brands.Brand.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/Brand/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing brand.")]
    public partial class UpdateBrand : BrandModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate brand.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.Brand.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/Brand/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific brand from the system [Soft-Delete]")]
    public partial class DeactivateBrandByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate brand by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.Brand.Deactivate"),
        PublicAPI,
        Route("/Brands/Brand/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific brand from the system [Soft-Delete]")]
    public partial class DeactivateBrandByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate brand.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.Brand.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/Brand/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific brand from the system [Restore from Soft-Delete]")]
    public partial class ReactivateBrandByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate brand by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.Brand.Reactivate"),
        PublicAPI,
        Route("/Brands/Brand/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific brand from the system [Restore from Soft-Delete]")]
    public partial class ReactivateBrandByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete brand.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.Brand.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/Brand/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific brand from the system [Hard-Delete]")]
    public partial class DeleteBrandByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete brand by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.Brand.Delete"),
        PublicAPI,
        Route("/Brands/Brand/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific brand from the system [Hard-Delete]")]
    public partial class DeleteBrandByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear brand cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/Brand/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all brand calls.")]
    public class ClearBrandCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class BrandServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetBrands"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrands request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IBrandModel, BrandModel, IBrandSearchModel, BrandPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.Brands)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetBrandsForConnect request)
        {
            return await Workflows.Brands.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandsDigest request)
        {
            return await Workflows.Brands.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetBrandByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.Brands, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.Brands, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.Brands, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckBrandExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckBrandExistsByID request)
        {
            return await Workflows.Brands.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckBrandExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckBrandExistsByKey request)
        {
            return await Workflows.Brands.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckBrandExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckBrandExistsByName request)
        {
            return await Workflows.Brands.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertBrand"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertBrand request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandDataAsync,
                    () => Workflows.Brands.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateBrand"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateBrand request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandDataAsync,
                    () => Workflows.Brands.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateBrand"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateBrand request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandDataAsync,
                    () => Workflows.Brands.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateBrandByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateBrandByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandDataAsync,
                    () => Workflows.Brands.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateBrandByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateBrandByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandDataAsync,
                    () => Workflows.Brands.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateBrandByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateBrandByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandDataAsync,
                    () => Workflows.Brands.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateBrandByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateBrandByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandDataAsync,
                    () => Workflows.Brands.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteBrandByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteBrandByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandDataAsync,
                    () => Workflows.Brands.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteBrandByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteBrandByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandDataAsync,
                    () => Workflows.Brands.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearBrandCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearBrandCache request)
        {
            await ClearCachedBrandDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedBrandDataAsync()
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
                    UrnId.Create<GetBrands>(string.Empty),
                    UrnId.Create<GetBrandByID>(string.Empty),
                    UrnId.Create<GetBrandByKey>(string.Empty),
                    UrnId.Create<GetBrandByName>(string.Empty),
                    UrnId.Create<CheckBrandExistsByID>(string.Empty),
                    UrnId.Create<CheckBrandExistsByKey>(string.Empty),
                    UrnId.Create<CheckBrandExistsByName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class BrandService : BrandServiceBase { }
}
