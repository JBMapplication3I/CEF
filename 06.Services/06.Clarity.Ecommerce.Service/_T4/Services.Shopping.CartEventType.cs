// <autogenerated>
// <copyright file="CartEventTypeService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart event type service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of cart event types.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{CartEventTypePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Shopping/CartEventTypes", "GET", Priority = 1,
            Summary = "Use to get a list of cart event types")]
    public partial class GetCartEventTypes : TypeSearchModel, IReturn<CartEventTypePagedResults> { }

    /// <summary>A ServiceStack Route to get cart event types for connect.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{List{TypeModel}}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.View"),
        PublicAPI,
        Route("/Shopping/CartEventTypesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all cart event types")]
    public partial class GetCartEventTypesForConnect : TypeSearchModel, IReturn<List<TypeModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all cart event types.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.View"),
        PublicAPI,
        Route("/Shopping/CartEventTypesDigest", "GET",
            Summary = "Use to get a hash representing each cart event types")]
    public partial class GetCartEventTypesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get cart event type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Shopping/CartEventType/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific cart event type")]
    public partial class GetCartEventTypeByID : ImplementsIDBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get cart event type.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Shopping/CartEventType/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific cart event type by the custom key")]
    public partial class GetCartEventTypeByKey : ImplementsKeyBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get cart event type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Shopping/CartEventType/Name", "GET", Priority = 1,
            Summary = "Use to get a specific cart event type by the name")]
    public partial class GetCartEventTypeByName : ImplementsNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get cart event type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Shopping/CartEventType/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific cart event type by the name")]
    public partial class GetCartEventTypeByDisplayName : ImplementsDisplayNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to check cart event type exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.View"),
        PublicAPI,
        Route("/Shopping/CartEventType/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckCartEventTypeExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check cart event type exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.View"),
        PublicAPI,
        Route("/Shopping/CartEventType/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckCartEventTypeExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check cart event type exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.View"),
        PublicAPI,
        Route("/Shopping/CartEventType/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckCartEventTypeExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check cart event type exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.View"),
        PublicAPI,
        Route("/Shopping/CartEventType/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckCartEventTypeExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create cart event type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Shopping/CartEventType/Create", "POST", Priority = 1,
            Summary = "Use to create a new cart event type.")]
    public partial class CreateCartEventType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert cart event type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Shopping/CartEventType/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing cart event type (as needed).")]
    public partial class UpsertCartEventType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update cart event type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Shopping/CartEventType/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing cart event type.")]
    public partial class UpdateCartEventType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate cart event type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Shopping/CartEventType/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific cart event type from the system [Soft-Delete]")]
    public partial class DeactivateCartEventTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate cart event type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.Deactivate"),
        PublicAPI,
        Route("/Shopping/CartEventType/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific cart event type from the system [Soft-Delete]")]
    public partial class DeactivateCartEventTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate cart event type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Shopping/CartEventType/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific cart event type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateCartEventTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate cart event type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.Reactivate"),
        PublicAPI,
        Route("/Shopping/CartEventType/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific cart event type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateCartEventTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete cart event type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Shopping/CartEventType/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific cart event type from the system [Hard-Delete]")]
    public partial class DeleteCartEventTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete cart event type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Shopping.CartEventType.Delete"),
        PublicAPI,
        Route("/Shopping/CartEventType/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific cart event type from the system [Hard-Delete]")]
    public partial class DeleteCartEventTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear cart event type cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Shopping/CartEventType/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all cart event type calls.")]
    public class ClearCartEventTypeCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class CartEventTypeServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetCartEventTypes"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCartEventTypes request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ITypeModel, TypeModel, ITypeSearchModel, CartEventTypePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.CartEventTypes)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCartEventTypesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetCartEventTypesForConnect request)
        {
            return await Workflows.CartEventTypes.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCartEventTypesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCartEventTypesDigest request)
        {
            return await Workflows.CartEventTypes.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetCartEventTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCartEventTypeByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.CartEventTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCartEventTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCartEventTypeByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.CartEventTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCartEventTypeByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCartEventTypeByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.CartEventTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCartEventTypeByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCartEventTypeByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.CartEventTypes, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckCartEventTypeExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCartEventTypeExistsByID request)
        {
            return await Workflows.CartEventTypes.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckCartEventTypeExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCartEventTypeExistsByKey request)
        {
            return await Workflows.CartEventTypes.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckCartEventTypeExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCartEventTypeExistsByName request)
        {
            return await Workflows.CartEventTypes.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckCartEventTypeExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCartEventTypeExistsByDisplayName request)
        {
            return await Workflows.CartEventTypes.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertCartEventType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertCartEventType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCartEventTypeDataAsync,
                    () => Workflows.CartEventTypes.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateCartEventType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateCartEventType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCartEventTypeDataAsync,
                    () => Workflows.CartEventTypes.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateCartEventType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateCartEventType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCartEventTypeDataAsync,
                    () => Workflows.CartEventTypes.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateCartEventTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateCartEventTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCartEventTypeDataAsync,
                    () => Workflows.CartEventTypes.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateCartEventTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateCartEventTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCartEventTypeDataAsync,
                    () => Workflows.CartEventTypes.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateCartEventTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateCartEventTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCartEventTypeDataAsync,
                    () => Workflows.CartEventTypes.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateCartEventTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateCartEventTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCartEventTypeDataAsync,
                    () => Workflows.CartEventTypes.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteCartEventTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteCartEventTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCartEventTypeDataAsync,
                    () => Workflows.CartEventTypes.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteCartEventTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteCartEventTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCartEventTypeDataAsync,
                    () => Workflows.CartEventTypes.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearCartEventTypeCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearCartEventTypeCache request)
        {
            await ClearCachedCartEventTypeDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedCartEventTypeDataAsync()
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
                    UrnId.Create<GetCartEventTypes>(string.Empty),
                    UrnId.Create<GetCartEventTypeByID>(string.Empty),
                    UrnId.Create<GetCartEventTypeByKey>(string.Empty),
                    UrnId.Create<GetCartEventTypeByName>(string.Empty),
                    UrnId.Create<CheckCartEventTypeExistsByID>(string.Empty),
                    UrnId.Create<CheckCartEventTypeExistsByKey>(string.Empty),
                    UrnId.Create<CheckCartEventTypeExistsByName>(string.Empty),
                    UrnId.Create<CheckCartEventTypeExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class CartEventTypeService : CartEventTypeServiceBase { }
}
