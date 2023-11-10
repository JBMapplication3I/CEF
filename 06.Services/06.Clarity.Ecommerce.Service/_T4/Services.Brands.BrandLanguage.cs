// <autogenerated>
// <copyright file="BrandLanguageService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand language service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of brand languages.</summary>
    /// <seealso cref="BrandLanguageSearchModel"/>
    /// <seealso cref="IReturn{BrandLanguagePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/BrandLanguages", "GET", Priority = 1,
            Summary = "Use to get a list of brand languages")]
    public partial class GetBrandLanguages : BrandLanguageSearchModel, IReturn<BrandLanguagePagedResults> { }

    /// <summary>A ServiceStack Route to get brand languages for connect.</summary>
    /// <seealso cref="BrandLanguageSearchModel"/>
    /// <seealso cref="IReturn{List{BrandLanguageModel}}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.View"),
        PublicAPI,
        Route("/Brands/BrandLanguagesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all brand languages")]
    public partial class GetBrandLanguagesForConnect : BrandLanguageSearchModel, IReturn<List<BrandLanguageModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all brand languages.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.View"),
        PublicAPI,
        Route("/Brands/BrandLanguagesDigest", "GET",
            Summary = "Use to get a hash representing each brand languages")]
    public partial class GetBrandLanguagesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get brand language.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{BrandLanguageModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/BrandLanguage/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific brand language")]
    public partial class GetBrandLanguageByID : ImplementsIDBase, IReturn<BrandLanguageModel> { }

    /// <summary>A ServiceStack Route to get brand language.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{BrandLanguageModel}"/>
    [PublicAPI,
        Route("/Brands/BrandLanguage/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific brand language by the custom key")]
    public partial class GetBrandLanguageByKey : ImplementsKeyBase, IReturn<BrandLanguageModel> { }

    /// <summary>A ServiceStack Route to check brand language exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.View"),
        PublicAPI,
        Route("/Brands/BrandLanguage/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckBrandLanguageExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check brand language exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.View"),
        PublicAPI,
        Route("/Brands/BrandLanguage/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckBrandLanguageExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create brand language.</summary>
    /// <seealso cref="BrandLanguageModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandLanguage/Create", "POST", Priority = 1,
            Summary = "Use to create a new brand language.")]
    public partial class CreateBrandLanguage : BrandLanguageModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert brand language.</summary>
    /// <seealso cref="BrandLanguageModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Brands/BrandLanguage/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing brand language (as needed).")]
    public partial class UpsertBrandLanguage : BrandLanguageModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update brand language.</summary>
    /// <seealso cref="BrandLanguageModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandLanguage/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing brand language.")]
    public partial class UpdateBrandLanguage : BrandLanguageModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate brand language.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandLanguage/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific brand language from the system [Soft-Delete]")]
    public partial class DeactivateBrandLanguageByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate brand language by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.Deactivate"),
        PublicAPI,
        Route("/Brands/BrandLanguage/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific brand language from the system [Soft-Delete]")]
    public partial class DeactivateBrandLanguageByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate brand language.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandLanguage/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific brand language from the system [Restore from Soft-Delete]")]
    public partial class ReactivateBrandLanguageByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate brand language by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.Reactivate"),
        PublicAPI,
        Route("/Brands/BrandLanguage/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific brand language from the system [Restore from Soft-Delete]")]
    public partial class ReactivateBrandLanguageByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete brand language.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandLanguage/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific brand language from the system [Hard-Delete]")]
    public partial class DeleteBrandLanguageByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete brand language by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandLanguage.Delete"),
        PublicAPI,
        Route("/Brands/BrandLanguage/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific brand language from the system [Hard-Delete]")]
    public partial class DeleteBrandLanguageByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear brand language cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/BrandLanguage/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all brand language calls.")]
    public class ClearBrandLanguageCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class BrandLanguageServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetBrandLanguages"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandLanguages request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IBrandLanguageModel, BrandLanguageModel, IBrandLanguageSearchModel, BrandLanguagePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.BrandLanguages)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandLanguagesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetBrandLanguagesForConnect request)
        {
            return await Workflows.BrandLanguages.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandLanguagesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandLanguagesDigest request)
        {
            return await Workflows.BrandLanguages.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetBrandLanguageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandLanguageByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.BrandLanguages, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandLanguageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandLanguageByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.BrandLanguages, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckBrandLanguageExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckBrandLanguageExistsByID request)
        {
            return await Workflows.BrandLanguages.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckBrandLanguageExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckBrandLanguageExistsByKey request)
        {
            return await Workflows.BrandLanguages.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertBrandLanguage"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertBrandLanguage request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandLanguageDataAsync,
                    () => Workflows.BrandLanguages.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateBrandLanguage"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateBrandLanguage request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandLanguageDataAsync,
                    () => Workflows.BrandLanguages.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateBrandLanguage"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateBrandLanguage request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandLanguageDataAsync,
                    () => Workflows.BrandLanguages.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateBrandLanguageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateBrandLanguageByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandLanguageDataAsync,
                    () => Workflows.BrandLanguages.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateBrandLanguageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateBrandLanguageByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandLanguageDataAsync,
                    () => Workflows.BrandLanguages.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateBrandLanguageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateBrandLanguageByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandLanguageDataAsync,
                    () => Workflows.BrandLanguages.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateBrandLanguageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateBrandLanguageByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandLanguageDataAsync,
                    () => Workflows.BrandLanguages.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteBrandLanguageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteBrandLanguageByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandLanguageDataAsync,
                    () => Workflows.BrandLanguages.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteBrandLanguageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteBrandLanguageByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandLanguageDataAsync,
                    () => Workflows.BrandLanguages.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearBrandLanguageCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearBrandLanguageCache request)
        {
            await ClearCachedBrandLanguageDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedBrandLanguageDataAsync()
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
                    UrnId.Create<GetBrandLanguages>(string.Empty),
                    UrnId.Create<GetBrandLanguageByID>(string.Empty),
                    UrnId.Create<GetBrandLanguageByKey>(string.Empty),
                    UrnId.Create<CheckBrandLanguageExistsByID>(string.Empty),
                    UrnId.Create<CheckBrandLanguageExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class BrandLanguageService : BrandLanguageServiceBase { }
}
