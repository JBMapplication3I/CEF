// <autogenerated>
// <copyright file="AdService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ad service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of ads.</summary>
    /// <seealso cref="AdSearchModel"/>
    /// <seealso cref="IReturn{AdPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Advertising/Ads", "GET", Priority = 1,
            Summary = "Use to get a list of ads")]
    public partial class GetAds : AdSearchModel, IReturn<AdPagedResults> { }

    /// <summary>A ServiceStack Route to get ads for connect.</summary>
    /// <seealso cref="AdSearchModel"/>
    /// <seealso cref="IReturn{List{AdModel}}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.View"),
        PublicAPI,
        Route("/Advertising/AdsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all ads")]
    public partial class GetAdsForConnect : AdSearchModel, IReturn<List<AdModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all ads.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.View"),
        PublicAPI,
        Route("/Advertising/AdsDigest", "GET",
            Summary = "Use to get a hash representing each ads")]
    public partial class GetAdsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get ad.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{AdModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Advertising/Ad/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific ad")]
    public partial class GetAdByID : ImplementsIDBase, IReturn<AdModel> { }

    /// <summary>A ServiceStack Route to get ad.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{AdModel}"/>
    [PublicAPI,
        Route("/Advertising/Ad/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific ad by the custom key")]
    public partial class GetAdByKey : ImplementsKeyBase, IReturn<AdModel> { }

    /// <summary>A ServiceStack Route to get ad.</summary>
    /// <seealso cref="IReturn{AdModel}"/>
    [PublicAPI,
        Route("/Advertising/Ad/Name", "GET", Priority = 1,
            Summary = "Use to get a specific ad by the name")]
    public partial class GetAdByName : ImplementsNameBase, IReturn<AdModel> { }

    /// <summary>A ServiceStack Route to check ad exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.View"),
        PublicAPI,
        Route("/Advertising/Ad/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckAdExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check ad exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.View"),
        PublicAPI,
        Route("/Advertising/Ad/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckAdExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check ad exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.View"),
        PublicAPI,
        Route("/Advertising/Ad/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckAdExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create ad.</summary>
    /// <seealso cref="AdModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/Ad/Create", "POST", Priority = 1,
            Summary = "Use to create a new ad.")]
    public partial class CreateAd : AdModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert ad.</summary>
    /// <seealso cref="AdModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Advertising/Ad/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing ad (as needed).")]
    public partial class UpsertAd : AdModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update ad.</summary>
    /// <seealso cref="AdModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/Ad/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing ad.")]
    public partial class UpdateAd : AdModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate ad.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/Ad/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific ad from the system [Soft-Delete]")]
    public partial class DeactivateAdByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate ad by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.Deactivate"),
        PublicAPI,
        Route("/Advertising/Ad/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific ad from the system [Soft-Delete]")]
    public partial class DeactivateAdByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate ad.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/Ad/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific ad from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAdByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate ad by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.Reactivate"),
        PublicAPI,
        Route("/Advertising/Ad/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific ad from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAdByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete ad.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/Ad/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific ad from the system [Hard-Delete]")]
    public partial class DeleteAdByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete ad by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Ad.Delete"),
        PublicAPI,
        Route("/Advertising/Ad/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific ad from the system [Hard-Delete]")]
    public partial class DeleteAdByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear ad cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Advertising/Ad/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all ad calls.")]
    public class ClearAdCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class AdServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetAds"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAds request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IAdModel, AdModel, IAdSearchModel, AdPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.Ads)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAdsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetAdsForConnect request)
        {
            return await Workflows.Ads.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAdsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAdsDigest request)
        {
            return await Workflows.Ads.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetAdByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAdByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.Ads, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAdByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAdByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.Ads, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAdByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAdByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.Ads, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckAdExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAdExistsByID request)
        {
            return await Workflows.Ads.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAdExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAdExistsByKey request)
        {
            return await Workflows.Ads.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAdExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAdExistsByName request)
        {
            return await Workflows.Ads.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertAd"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertAd request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdDataAsync,
                    () => Workflows.Ads.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateAd"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateAd request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdDataAsync,
                    () => Workflows.Ads.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateAd"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateAd request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdDataAsync,
                    () => Workflows.Ads.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateAdByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAdByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdDataAsync,
                    () => Workflows.Ads.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateAdByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAdByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdDataAsync,
                    () => Workflows.Ads.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateAdByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAdByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdDataAsync,
                    () => Workflows.Ads.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateAdByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAdByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdDataAsync,
                    () => Workflows.Ads.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteAdByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAdByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdDataAsync,
                    () => Workflows.Ads.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteAdByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAdByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdDataAsync,
                    () => Workflows.Ads.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearAdCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearAdCache request)
        {
            await ClearCachedAdDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedAdDataAsync()
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
                    UrnId.Create<GetAds>(string.Empty),
                    UrnId.Create<GetAdByID>(string.Empty),
                    UrnId.Create<GetAdByKey>(string.Empty),
                    UrnId.Create<GetAdByName>(string.Empty),
                    UrnId.Create<CheckAdExistsByID>(string.Empty),
                    UrnId.Create<CheckAdExistsByKey>(string.Empty),
                    UrnId.Create<CheckAdExistsByName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class AdService : AdServiceBase { }
}
