// <autogenerated>
// <copyright file="FranchiseTypeService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise type service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of franchise types.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{FranchiseTypePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Franchises/FranchiseTypes", "GET", Priority = 1,
            Summary = "Use to get a list of franchise types")]
    public partial class GetFranchiseTypes : TypeSearchModel, IReturn<FranchiseTypePagedResults> { }

    /// <summary>A ServiceStack Route to get franchise types for connect.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{List{TypeModel}}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.View"),
        PublicAPI,
        Route("/Franchises/FranchiseTypesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all franchise types")]
    public partial class GetFranchiseTypesForConnect : TypeSearchModel, IReturn<List<TypeModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all franchise types.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.View"),
        PublicAPI,
        Route("/Franchises/FranchiseTypesDigest", "GET",
            Summary = "Use to get a hash representing each franchise types")]
    public partial class GetFranchiseTypesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get franchise type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Franchises/FranchiseType/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific franchise type")]
    public partial class GetFranchiseTypeByID : ImplementsIDBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get franchise type.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Franchises/FranchiseType/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific franchise type by the custom key")]
    public partial class GetFranchiseTypeByKey : ImplementsKeyBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get franchise type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Franchises/FranchiseType/Name", "GET", Priority = 1,
            Summary = "Use to get a specific franchise type by the name")]
    public partial class GetFranchiseTypeByName : ImplementsNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get franchise type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Franchises/FranchiseType/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific franchise type by the name")]
    public partial class GetFranchiseTypeByDisplayName : ImplementsDisplayNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to check franchise type exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.View"),
        PublicAPI,
        Route("/Franchises/FranchiseType/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckFranchiseTypeExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check franchise type exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.View"),
        PublicAPI,
        Route("/Franchises/FranchiseType/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckFranchiseTypeExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check franchise type exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.View"),
        PublicAPI,
        Route("/Franchises/FranchiseType/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckFranchiseTypeExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check franchise type exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.View"),
        PublicAPI,
        Route("/Franchises/FranchiseType/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckFranchiseTypeExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create franchise type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Franchises/FranchiseType/Create", "POST", Priority = 1,
            Summary = "Use to create a new franchise type.")]
    public partial class CreateFranchiseType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert franchise type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Franchises/FranchiseType/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing franchise type (as needed).")]
    public partial class UpsertFranchiseType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update franchise type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Franchises/FranchiseType/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing franchise type.")]
    public partial class UpdateFranchiseType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate franchise type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Franchises/FranchiseType/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific franchise type from the system [Soft-Delete]")]
    public partial class DeactivateFranchiseTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate franchise type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.Deactivate"),
        PublicAPI,
        Route("/Franchises/FranchiseType/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific franchise type from the system [Soft-Delete]")]
    public partial class DeactivateFranchiseTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate franchise type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Franchises/FranchiseType/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific franchise type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateFranchiseTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate franchise type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.Reactivate"),
        PublicAPI,
        Route("/Franchises/FranchiseType/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific franchise type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateFranchiseTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete franchise type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Franchises/FranchiseType/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific franchise type from the system [Hard-Delete]")]
    public partial class DeleteFranchiseTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete franchise type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Franchises.FranchiseType.Delete"),
        PublicAPI,
        Route("/Franchises/FranchiseType/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific franchise type from the system [Hard-Delete]")]
    public partial class DeleteFranchiseTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear franchise type cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Franchises/FranchiseType/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all franchise type calls.")]
    public class ClearFranchiseTypeCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class FranchiseTypeServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetFranchiseTypes"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetFranchiseTypes request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ITypeModel, TypeModel, ITypeSearchModel, FranchiseTypePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.FranchiseTypes)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetFranchiseTypesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetFranchiseTypesForConnect request)
        {
            return await Workflows.FranchiseTypes.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetFranchiseTypesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetFranchiseTypesDigest request)
        {
            return await Workflows.FranchiseTypes.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetFranchiseTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetFranchiseTypeByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.FranchiseTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetFranchiseTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetFranchiseTypeByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.FranchiseTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetFranchiseTypeByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetFranchiseTypeByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.FranchiseTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetFranchiseTypeByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetFranchiseTypeByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.FranchiseTypes, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckFranchiseTypeExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckFranchiseTypeExistsByID request)
        {
            return await Workflows.FranchiseTypes.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckFranchiseTypeExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckFranchiseTypeExistsByKey request)
        {
            return await Workflows.FranchiseTypes.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckFranchiseTypeExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckFranchiseTypeExistsByName request)
        {
            return await Workflows.FranchiseTypes.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckFranchiseTypeExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckFranchiseTypeExistsByDisplayName request)
        {
            return await Workflows.FranchiseTypes.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertFranchiseType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertFranchiseType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseTypeDataAsync,
                    () => Workflows.FranchiseTypes.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateFranchiseType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateFranchiseType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseTypeDataAsync,
                    () => Workflows.FranchiseTypes.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateFranchiseType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateFranchiseType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseTypeDataAsync,
                    () => Workflows.FranchiseTypes.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateFranchiseTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateFranchiseTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseTypeDataAsync,
                    () => Workflows.FranchiseTypes.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateFranchiseTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateFranchiseTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseTypeDataAsync,
                    () => Workflows.FranchiseTypes.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateFranchiseTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateFranchiseTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseTypeDataAsync,
                    () => Workflows.FranchiseTypes.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateFranchiseTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateFranchiseTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseTypeDataAsync,
                    () => Workflows.FranchiseTypes.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteFranchiseTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteFranchiseTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseTypeDataAsync,
                    () => Workflows.FranchiseTypes.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteFranchiseTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteFranchiseTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedFranchiseTypeDataAsync,
                    () => Workflows.FranchiseTypes.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearFranchiseTypeCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearFranchiseTypeCache request)
        {
            await ClearCachedFranchiseTypeDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedFranchiseTypeDataAsync()
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
                    UrnId.Create<GetFranchiseTypes>(string.Empty),
                    UrnId.Create<GetFranchiseTypeByID>(string.Empty),
                    UrnId.Create<GetFranchiseTypeByKey>(string.Empty),
                    UrnId.Create<GetFranchiseTypeByName>(string.Empty),
                    UrnId.Create<CheckFranchiseTypeExistsByID>(string.Empty),
                    UrnId.Create<CheckFranchiseTypeExistsByKey>(string.Empty),
                    UrnId.Create<CheckFranchiseTypeExistsByName>(string.Empty),
                    UrnId.Create<CheckFranchiseTypeExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class FranchiseTypeService : FranchiseTypeServiceBase { }
}
