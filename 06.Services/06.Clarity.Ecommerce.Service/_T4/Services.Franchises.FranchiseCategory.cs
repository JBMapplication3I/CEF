// <autogenerated>
// <copyright file="FranchiseCategoryService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise category service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of franchise categories.</summary>
    /// <seealso cref="FranchiseCategorySearchModel"/>
    /// <seealso cref="IReturn{FranchiseCategoryPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Franchises/FranchiseCategories", "GET", Priority = 1,
            Summary = "Use to get a list of franchise categories")]
    public partial class GetFranchiseCategories : FranchiseCategorySearchModel, IReturn<FranchiseCategoryPagedResults> { }

    /// <summary>A ServiceStack Route to get franchise categories for connect.</summary>
    /// <seealso cref="FranchiseCategorySearchModel"/>
    /// <seealso cref="IReturn{List{FranchiseCategoryModel}}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.View"),
        PublicAPI,
        Route("/Franchises/FranchiseCategoriesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all franchise categories")]
    public partial class GetFranchiseCategoriesForConnect : FranchiseCategorySearchModel, IReturn<List<FranchiseCategoryModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all franchise categories.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.View"),
        PublicAPI,
        Route("/Franchises/FranchiseCategoriesDigest", "GET",
            Summary = "Use to get a hash representing each franchise categories")]
    public partial class GetFranchiseCategoriesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get franchise category.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{FranchiseCategoryModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Franchises/FranchiseCategory/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific franchise category")]
    public partial class GetFranchiseCategoryByID : ImplementsIDBase, IReturn<FranchiseCategoryModel> { }

    /// <summary>A ServiceStack Route to get franchise category.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{FranchiseCategoryModel}"/>
    [PublicAPI,
        Route("/Franchises/FranchiseCategory/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific franchise category by the custom key")]
    public partial class GetFranchiseCategoryByKey : ImplementsKeyBase, IReturn<FranchiseCategoryModel> { }

    /// <summary>A ServiceStack Route to check franchise category exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.View"),
        PublicAPI,
        Route("/Franchises/FranchiseCategory/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckFranchiseCategoryExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check franchise category exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.View"),
        PublicAPI,
        Route("/Franchises/FranchiseCategory/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckFranchiseCategoryExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create franchise category.</summary>
    /// <seealso cref="FranchiseCategoryModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Franchises/FranchiseCategory/Create", "POST", Priority = 1,
            Summary = "Use to create a new franchise category.")]
    public partial class CreateFranchiseCategory : FranchiseCategoryModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert franchise category.</summary>
    /// <seealso cref="FranchiseCategoryModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Franchises/FranchiseCategory/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing franchise category (as needed).")]
    public partial class UpsertFranchiseCategory : FranchiseCategoryModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update franchise category.</summary>
    /// <seealso cref="FranchiseCategoryModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Franchises/FranchiseCategory/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing franchise category.")]
    public partial class UpdateFranchiseCategory : FranchiseCategoryModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate franchise category.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Franchises/FranchiseCategory/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific franchise category from the system [Soft-Delete]")]
    public partial class DeactivateFranchiseCategoryByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate franchise category by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.Deactivate"),
        PublicAPI,
        Route("/Franchises/FranchiseCategory/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific franchise category from the system [Soft-Delete]")]
    public partial class DeactivateFranchiseCategoryByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate franchise category.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Franchises/FranchiseCategory/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific franchise category from the system [Restore from Soft-Delete]")]
    public partial class ReactivateFranchiseCategoryByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate franchise category by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.Reactivate"),
        PublicAPI,
        Route("/Franchises/FranchiseCategory/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific franchise category from the system [Restore from Soft-Delete]")]
    public partial class ReactivateFranchiseCategoryByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete franchise category.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Franchises/FranchiseCategory/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific franchise category from the system [Hard-Delete]")]
    public partial class DeleteFranchiseCategoryByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete franchise category by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseCategory.Delete"),
        PublicAPI,
        Route("/Franchises/FranchiseCategory/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific franchise category from the system [Hard-Delete]")]
    public partial class DeleteFranchiseCategoryByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear franchise category cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Franchises/FranchiseCategory/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all franchise category calls.")]
    public class ClearFranchiseCategoryCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class FranchiseCategoryServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetFranchiseCategories"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetFranchiseCategories request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IFranchiseCategoryModel, FranchiseCategoryModel, IFranchiseCategorySearchModel, FranchiseCategoryPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.FranchiseCategories)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetFranchiseCategoriesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetFranchiseCategoriesForConnect request)
        {
            return await Workflows.FranchiseCategories.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetFranchiseCategoriesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetFranchiseCategoriesDigest request)
        {
            return await Workflows.FranchiseCategories.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetFranchiseCategoryByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetFranchiseCategoryByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.FranchiseCategories, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetFranchiseCategoryByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetFranchiseCategoryByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.FranchiseCategories, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckFranchiseCategoryExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckFranchiseCategoryExistsByID request)
        {
            return await Workflows.FranchiseCategories.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckFranchiseCategoryExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckFranchiseCategoryExistsByKey request)
        {
            return await Workflows.FranchiseCategories.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertFranchiseCategory"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertFranchiseCategory request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseCategoryDataAsync,
                    () => Workflows.FranchiseCategories.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateFranchiseCategory"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateFranchiseCategory request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseCategoryDataAsync,
                    () => Workflows.FranchiseCategories.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateFranchiseCategory"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateFranchiseCategory request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseCategoryDataAsync,
                    () => Workflows.FranchiseCategories.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateFranchiseCategoryByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateFranchiseCategoryByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseCategoryDataAsync,
                    () => Workflows.FranchiseCategories.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateFranchiseCategoryByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateFranchiseCategoryByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseCategoryDataAsync,
                    () => Workflows.FranchiseCategories.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateFranchiseCategoryByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateFranchiseCategoryByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseCategoryDataAsync,
                    () => Workflows.FranchiseCategories.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateFranchiseCategoryByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateFranchiseCategoryByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseCategoryDataAsync,
                    () => Workflows.FranchiseCategories.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteFranchiseCategoryByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteFranchiseCategoryByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseCategoryDataAsync,
                    () => Workflows.FranchiseCategories.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteFranchiseCategoryByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteFranchiseCategoryByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseCategoryDataAsync,
                    () => Workflows.FranchiseCategories.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearFranchiseCategoryCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearFranchiseCategoryCache request)
        {
            await ClearCachedFranchiseCategoryDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedFranchiseCategoryDataAsync()
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
                    UrnId.Create<GetFranchiseCategories>(string.Empty),
                    UrnId.Create<GetFranchiseCategoryByID>(string.Empty),
                    UrnId.Create<GetFranchiseCategoryByKey>(string.Empty),
                    UrnId.Create<CheckFranchiseCategoryExistsByID>(string.Empty),
                    UrnId.Create<CheckFranchiseCategoryExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class FranchiseCategoryService : FranchiseCategoryServiceBase { }
}
