// <autogenerated>
// <copyright file="AdTypeService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ad type service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of ad types.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{AdTypePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Advertising/AdTypes", "GET", Priority = 1,
            Summary = "Use to get a list of ad types")]
    public partial class GetAdTypes : TypeSearchModel, IReturn<AdTypePagedResults> { }

    /// <summary>A ServiceStack Route to get ad types for connect.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{List{TypeModel}}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.View"),
        PublicAPI,
        Route("/Advertising/AdTypesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all ad types")]
    public partial class GetAdTypesForConnect : TypeSearchModel, IReturn<List<TypeModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all ad types.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.View"),
        PublicAPI,
        Route("/Advertising/AdTypesDigest", "GET",
            Summary = "Use to get a hash representing each ad types")]
    public partial class GetAdTypesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get ad type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Advertising/AdType/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific ad type")]
    public partial class GetAdTypeByID : ImplementsIDBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get ad type.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Advertising/AdType/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific ad type by the custom key")]
    public partial class GetAdTypeByKey : ImplementsKeyBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get ad type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Advertising/AdType/Name", "GET", Priority = 1,
            Summary = "Use to get a specific ad type by the name")]
    public partial class GetAdTypeByName : ImplementsNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get ad type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Advertising/AdType/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific ad type by the name")]
    public partial class GetAdTypeByDisplayName : ImplementsDisplayNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to check ad type exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.View"),
        PublicAPI,
        Route("/Advertising/AdType/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckAdTypeExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check ad type exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.View"),
        PublicAPI,
        Route("/Advertising/AdType/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckAdTypeExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check ad type exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.View"),
        PublicAPI,
        Route("/Advertising/AdType/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckAdTypeExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check ad type exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.View"),
        PublicAPI,
        Route("/Advertising/AdType/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckAdTypeExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create ad type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/AdType/Create", "POST", Priority = 1,
            Summary = "Use to create a new ad type.")]
    public partial class CreateAdType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert ad type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Advertising/AdType/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing ad type (as needed).")]
    public partial class UpsertAdType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update ad type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/AdType/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing ad type.")]
    public partial class UpdateAdType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate ad type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/AdType/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific ad type from the system [Soft-Delete]")]
    public partial class DeactivateAdTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate ad type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.Deactivate"),
        PublicAPI,
        Route("/Advertising/AdType/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific ad type from the system [Soft-Delete]")]
    public partial class DeactivateAdTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate ad type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/AdType/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific ad type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAdTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate ad type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.Reactivate"),
        PublicAPI,
        Route("/Advertising/AdType/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific ad type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAdTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete ad type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/AdType/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific ad type from the system [Hard-Delete]")]
    public partial class DeleteAdTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete ad type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.AdType.Delete"),
        PublicAPI,
        Route("/Advertising/AdType/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific ad type from the system [Hard-Delete]")]
    public partial class DeleteAdTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear ad type cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Advertising/AdType/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all ad type calls.")]
    public class ClearAdTypeCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class AdTypeServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetAdTypes"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAdTypes request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ITypeModel, TypeModel, ITypeSearchModel, AdTypePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.AdTypes)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAdTypesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetAdTypesForConnect request)
        {
            return await Workflows.AdTypes.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAdTypesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAdTypesDigest request)
        {
            return await Workflows.AdTypes.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetAdTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAdTypeByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.AdTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAdTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAdTypeByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.AdTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAdTypeByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAdTypeByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.AdTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAdTypeByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAdTypeByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.AdTypes, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckAdTypeExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAdTypeExistsByID request)
        {
            return await Workflows.AdTypes.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAdTypeExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAdTypeExistsByKey request)
        {
            return await Workflows.AdTypes.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAdTypeExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAdTypeExistsByName request)
        {
            return await Workflows.AdTypes.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAdTypeExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAdTypeExistsByDisplayName request)
        {
            return await Workflows.AdTypes.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertAdType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertAdType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdTypeDataAsync,
                    () => Workflows.AdTypes.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateAdType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateAdType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdTypeDataAsync,
                    () => Workflows.AdTypes.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateAdType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateAdType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdTypeDataAsync,
                    () => Workflows.AdTypes.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateAdTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAdTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdTypeDataAsync,
                    () => Workflows.AdTypes.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateAdTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAdTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdTypeDataAsync,
                    () => Workflows.AdTypes.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateAdTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAdTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdTypeDataAsync,
                    () => Workflows.AdTypes.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateAdTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAdTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdTypeDataAsync,
                    () => Workflows.AdTypes.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteAdTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAdTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdTypeDataAsync,
                    () => Workflows.AdTypes.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteAdTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAdTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAdTypeDataAsync,
                    () => Workflows.AdTypes.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearAdTypeCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearAdTypeCache request)
        {
            await ClearCachedAdTypeDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedAdTypeDataAsync()
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
                    UrnId.Create<GetAdTypes>(string.Empty),
                    UrnId.Create<GetAdTypeByID>(string.Empty),
                    UrnId.Create<GetAdTypeByKey>(string.Empty),
                    UrnId.Create<GetAdTypeByName>(string.Empty),
                    UrnId.Create<CheckAdTypeExistsByID>(string.Empty),
                    UrnId.Create<CheckAdTypeExistsByKey>(string.Empty),
                    UrnId.Create<CheckAdTypeExistsByName>(string.Empty),
                    UrnId.Create<CheckAdTypeExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class AdTypeService : AdTypeServiceBase { }
}
