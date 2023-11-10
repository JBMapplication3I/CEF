// <autogenerated>
// <copyright file="DiscountManufacturerService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount manufacturer service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of discount manufacturers.</summary>
    /// <seealso cref="DiscountManufacturerSearchModel"/>
    /// <seealso cref="IReturn{DiscountManufacturerPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Discounts/DiscountManufacturers", "GET", Priority = 1,
            Summary = "Use to get a list of discount manufacturers")]
    public partial class GetDiscountManufacturers : DiscountManufacturerSearchModel, IReturn<DiscountManufacturerPagedResults> { }

    /// <summary>A ServiceStack Route to get discount manufacturers for connect.</summary>
    /// <seealso cref="DiscountManufacturerSearchModel"/>
    /// <seealso cref="IReturn{List{DiscountManufacturerModel}}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.View"),
        PublicAPI,
        Route("/Discounts/DiscountManufacturersForConnect", "POST,GET", Priority = 1,
            Summary = "Get all discount manufacturers")]
    public partial class GetDiscountManufacturersForConnect : DiscountManufacturerSearchModel, IReturn<List<DiscountManufacturerModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all discount manufacturers.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.View"),
        PublicAPI,
        Route("/Discounts/DiscountManufacturersDigest", "GET",
            Summary = "Use to get a hash representing each discount manufacturers")]
    public partial class GetDiscountManufacturersDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get discount manufacturer.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{DiscountManufacturerModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Discounts/DiscountManufacturer/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific discount manufacturer")]
    public partial class GetDiscountManufacturerByID : ImplementsIDBase, IReturn<DiscountManufacturerModel> { }

    /// <summary>A ServiceStack Route to get discount manufacturer.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{DiscountManufacturerModel}"/>
    [PublicAPI,
        Route("/Discounts/DiscountManufacturer/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific discount manufacturer by the custom key")]
    public partial class GetDiscountManufacturerByKey : ImplementsKeyBase, IReturn<DiscountManufacturerModel> { }

    /// <summary>A ServiceStack Route to check discount manufacturer exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.View"),
        PublicAPI,
        Route("/Discounts/DiscountManufacturer/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckDiscountManufacturerExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check discount manufacturer exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.View"),
        PublicAPI,
        Route("/Discounts/DiscountManufacturer/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckDiscountManufacturerExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create discount manufacturer.</summary>
    /// <seealso cref="DiscountManufacturerModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Discounts/DiscountManufacturer/Create", "POST", Priority = 1,
            Summary = "Use to create a new discount manufacturer.")]
    public partial class CreateDiscountManufacturer : DiscountManufacturerModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert discount manufacturer.</summary>
    /// <seealso cref="DiscountManufacturerModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Discounts/DiscountManufacturer/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing discount manufacturer (as needed).")]
    public partial class UpsertDiscountManufacturer : DiscountManufacturerModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update discount manufacturer.</summary>
    /// <seealso cref="DiscountManufacturerModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Discounts/DiscountManufacturer/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing discount manufacturer.")]
    public partial class UpdateDiscountManufacturer : DiscountManufacturerModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate discount manufacturer.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Discounts/DiscountManufacturer/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific discount manufacturer from the system [Soft-Delete]")]
    public partial class DeactivateDiscountManufacturerByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate discount manufacturer by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.Deactivate"),
        PublicAPI,
        Route("/Discounts/DiscountManufacturer/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific discount manufacturer from the system [Soft-Delete]")]
    public partial class DeactivateDiscountManufacturerByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate discount manufacturer.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Discounts/DiscountManufacturer/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific discount manufacturer from the system [Restore from Soft-Delete]")]
    public partial class ReactivateDiscountManufacturerByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate discount manufacturer by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.Reactivate"),
        PublicAPI,
        Route("/Discounts/DiscountManufacturer/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific discount manufacturer from the system [Restore from Soft-Delete]")]
    public partial class ReactivateDiscountManufacturerByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete discount manufacturer.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Discounts/DiscountManufacturer/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific discount manufacturer from the system [Hard-Delete]")]
    public partial class DeleteDiscountManufacturerByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete discount manufacturer by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Discounts.DiscountManufacturer.Delete"),
        PublicAPI,
        Route("/Discounts/DiscountManufacturer/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific discount manufacturer from the system [Hard-Delete]")]
    public partial class DeleteDiscountManufacturerByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear discount manufacturer cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Discounts/DiscountManufacturer/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all discount manufacturer calls.")]
    public class ClearDiscountManufacturerCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class DiscountManufacturerServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetDiscountManufacturers"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetDiscountManufacturers request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IDiscountManufacturerModel, DiscountManufacturerModel, IDiscountManufacturerSearchModel, DiscountManufacturerPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.DiscountManufacturers)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetDiscountManufacturersForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetDiscountManufacturersForConnect request)
        {
            return await Workflows.DiscountManufacturers.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetDiscountManufacturersDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetDiscountManufacturersDigest request)
        {
            return await Workflows.DiscountManufacturers.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetDiscountManufacturerByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetDiscountManufacturerByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.DiscountManufacturers, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetDiscountManufacturerByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetDiscountManufacturerByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.DiscountManufacturers, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckDiscountManufacturerExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckDiscountManufacturerExistsByID request)
        {
            return await Workflows.DiscountManufacturers.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckDiscountManufacturerExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckDiscountManufacturerExistsByKey request)
        {
            return await Workflows.DiscountManufacturers.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertDiscountManufacturer"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertDiscountManufacturer request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedDiscountManufacturerDataAsync,
                    () => Workflows.DiscountManufacturers.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateDiscountManufacturer"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateDiscountManufacturer request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedDiscountManufacturerDataAsync,
                    () => Workflows.DiscountManufacturers.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateDiscountManufacturer"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateDiscountManufacturer request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedDiscountManufacturerDataAsync,
                    () => Workflows.DiscountManufacturers.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateDiscountManufacturerByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateDiscountManufacturerByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedDiscountManufacturerDataAsync,
                    () => Workflows.DiscountManufacturers.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateDiscountManufacturerByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateDiscountManufacturerByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedDiscountManufacturerDataAsync,
                    () => Workflows.DiscountManufacturers.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateDiscountManufacturerByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateDiscountManufacturerByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedDiscountManufacturerDataAsync,
                    () => Workflows.DiscountManufacturers.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateDiscountManufacturerByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateDiscountManufacturerByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedDiscountManufacturerDataAsync,
                    () => Workflows.DiscountManufacturers.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteDiscountManufacturerByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteDiscountManufacturerByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedDiscountManufacturerDataAsync,
                    () => Workflows.DiscountManufacturers.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteDiscountManufacturerByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteDiscountManufacturerByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedDiscountManufacturerDataAsync,
                    () => Workflows.DiscountManufacturers.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearDiscountManufacturerCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearDiscountManufacturerCache request)
        {
            await ClearCachedDiscountManufacturerDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedDiscountManufacturerDataAsync()
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
                    UrnId.Create<GetDiscountManufacturers>(string.Empty),
                    UrnId.Create<GetDiscountManufacturerByID>(string.Empty),
                    UrnId.Create<GetDiscountManufacturerByKey>(string.Empty),
                    UrnId.Create<CheckDiscountManufacturerExistsByID>(string.Empty),
                    UrnId.Create<CheckDiscountManufacturerExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class DiscountManufacturerService : DiscountManufacturerServiceBase { }
}
