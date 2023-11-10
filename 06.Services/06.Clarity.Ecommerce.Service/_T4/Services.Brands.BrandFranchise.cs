// <autogenerated>
// <copyright file="BrandFranchiseService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand franchise service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of brand franchises.</summary>
    /// <seealso cref="BrandFranchiseSearchModel"/>
    /// <seealso cref="IReturn{BrandFranchisePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/BrandFranchises", "GET", Priority = 1,
            Summary = "Use to get a list of brand franchises")]
    public partial class GetBrandFranchises : BrandFranchiseSearchModel, IReturn<BrandFranchisePagedResults> { }

    /// <summary>A ServiceStack Route to get brand franchises for connect.</summary>
    /// <seealso cref="BrandFranchiseSearchModel"/>
    /// <seealso cref="IReturn{List{BrandFranchiseModel}}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.View"),
        PublicAPI,
        Route("/Brands/BrandFranchisesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all brand franchises")]
    public partial class GetBrandFranchisesForConnect : BrandFranchiseSearchModel, IReturn<List<BrandFranchiseModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all brand franchises.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.View"),
        PublicAPI,
        Route("/Brands/BrandFranchisesDigest", "GET",
            Summary = "Use to get a hash representing each brand franchises")]
    public partial class GetBrandFranchisesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get brand franchise.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{BrandFranchiseModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/BrandFranchise/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific brand franchise")]
    public partial class GetBrandFranchiseByID : ImplementsIDBase, IReturn<BrandFranchiseModel> { }

    /// <summary>A ServiceStack Route to get brand franchise.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{BrandFranchiseModel}"/>
    [PublicAPI,
        Route("/Brands/BrandFranchise/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific brand franchise by the custom key")]
    public partial class GetBrandFranchiseByKey : ImplementsKeyBase, IReturn<BrandFranchiseModel> { }

    /// <summary>A ServiceStack Route to check brand franchise exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.View"),
        PublicAPI,
        Route("/Brands/BrandFranchise/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckBrandFranchiseExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check brand franchise exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.View"),
        PublicAPI,
        Route("/Brands/BrandFranchise/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckBrandFranchiseExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create brand franchise.</summary>
    /// <seealso cref="BrandFranchiseModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandFranchise/Create", "POST", Priority = 1,
            Summary = "Use to create a new brand franchise.")]
    public partial class CreateBrandFranchise : BrandFranchiseModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert brand franchise.</summary>
    /// <seealso cref="BrandFranchiseModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Brands/BrandFranchise/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing brand franchise (as needed).")]
    public partial class UpsertBrandFranchise : BrandFranchiseModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update brand franchise.</summary>
    /// <seealso cref="BrandFranchiseModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandFranchise/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing brand franchise.")]
    public partial class UpdateBrandFranchise : BrandFranchiseModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate brand franchise.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandFranchise/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific brand franchise from the system [Soft-Delete]")]
    public partial class DeactivateBrandFranchiseByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate brand franchise by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.Deactivate"),
        PublicAPI,
        Route("/Brands/BrandFranchise/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific brand franchise from the system [Soft-Delete]")]
    public partial class DeactivateBrandFranchiseByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate brand franchise.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandFranchise/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific brand franchise from the system [Restore from Soft-Delete]")]
    public partial class ReactivateBrandFranchiseByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate brand franchise by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.Reactivate"),
        PublicAPI,
        Route("/Brands/BrandFranchise/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific brand franchise from the system [Restore from Soft-Delete]")]
    public partial class ReactivateBrandFranchiseByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete brand franchise.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandFranchise/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific brand franchise from the system [Hard-Delete]")]
    public partial class DeleteBrandFranchiseByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete brand franchise by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandFranchise.Delete"),
        PublicAPI,
        Route("/Brands/BrandFranchise/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific brand franchise from the system [Hard-Delete]")]
    public partial class DeleteBrandFranchiseByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear brand franchise cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/BrandFranchise/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all brand franchise calls.")]
    public class ClearBrandFranchiseCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class BrandFranchiseServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetBrandFranchises"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandFranchises request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IBrandFranchiseModel, BrandFranchiseModel, IBrandFranchiseSearchModel, BrandFranchisePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.BrandFranchises)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandFranchisesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetBrandFranchisesForConnect request)
        {
            return await Workflows.BrandFranchises.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandFranchisesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandFranchisesDigest request)
        {
            return await Workflows.BrandFranchises.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetBrandFranchiseByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandFranchiseByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.BrandFranchises, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandFranchiseByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandFranchiseByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.BrandFranchises, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckBrandFranchiseExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckBrandFranchiseExistsByID request)
        {
            return await Workflows.BrandFranchises.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckBrandFranchiseExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckBrandFranchiseExistsByKey request)
        {
            return await Workflows.BrandFranchises.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertBrandFranchise"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertBrandFranchise request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandFranchiseDataAsync,
                    () => Workflows.BrandFranchises.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateBrandFranchise"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateBrandFranchise request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandFranchiseDataAsync,
                    () => Workflows.BrandFranchises.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateBrandFranchise"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateBrandFranchise request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandFranchiseDataAsync,
                    () => Workflows.BrandFranchises.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateBrandFranchiseByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateBrandFranchiseByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandFranchiseDataAsync,
                    () => Workflows.BrandFranchises.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateBrandFranchiseByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateBrandFranchiseByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandFranchiseDataAsync,
                    () => Workflows.BrandFranchises.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateBrandFranchiseByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateBrandFranchiseByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandFranchiseDataAsync,
                    () => Workflows.BrandFranchises.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateBrandFranchiseByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateBrandFranchiseByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandFranchiseDataAsync,
                    () => Workflows.BrandFranchises.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteBrandFranchiseByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteBrandFranchiseByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandFranchiseDataAsync,
                    () => Workflows.BrandFranchises.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteBrandFranchiseByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteBrandFranchiseByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandFranchiseDataAsync,
                    () => Workflows.BrandFranchises.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearBrandFranchiseCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearBrandFranchiseCache request)
        {
            await ClearCachedBrandFranchiseDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedBrandFranchiseDataAsync()
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
                    UrnId.Create<GetBrandFranchises>(string.Empty),
                    UrnId.Create<GetBrandFranchiseByID>(string.Empty),
                    UrnId.Create<GetBrandFranchiseByKey>(string.Empty),
                    UrnId.Create<CheckBrandFranchiseExistsByID>(string.Empty),
                    UrnId.Create<CheckBrandFranchiseExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class BrandFranchiseService : BrandFranchiseServiceBase { }
}
