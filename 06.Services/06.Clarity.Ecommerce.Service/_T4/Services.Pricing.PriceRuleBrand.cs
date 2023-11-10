// <autogenerated>
// <copyright file="PriceRuleBrandService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule brand service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of price rule brands.</summary>
    /// <seealso cref="PriceRuleBrandSearchModel"/>
    /// <seealso cref="IReturn{PriceRuleBrandPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Pricing/PriceRuleBrands", "GET", Priority = 1,
            Summary = "Use to get a list of price rule brands")]
    public partial class GetPriceRuleBrands : PriceRuleBrandSearchModel, IReturn<PriceRuleBrandPagedResults> { }

    /// <summary>A ServiceStack Route to get price rule brands for connect.</summary>
    /// <seealso cref="PriceRuleBrandSearchModel"/>
    /// <seealso cref="IReturn{List{PriceRuleBrandModel}}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.View"),
        PublicAPI,
        Route("/Pricing/PriceRuleBrandsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all price rule brands")]
    public partial class GetPriceRuleBrandsForConnect : PriceRuleBrandSearchModel, IReturn<List<PriceRuleBrandModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all price rule brands.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.View"),
        PublicAPI,
        Route("/Pricing/PriceRuleBrandsDigest", "GET",
            Summary = "Use to get a hash representing each price rule brands")]
    public partial class GetPriceRuleBrandsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get price rule brand.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{PriceRuleBrandModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Pricing/PriceRuleBrand/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific price rule brand")]
    public partial class GetPriceRuleBrandByID : ImplementsIDBase, IReturn<PriceRuleBrandModel> { }

    /// <summary>A ServiceStack Route to get price rule brand.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{PriceRuleBrandModel}"/>
    [PublicAPI,
        Route("/Pricing/PriceRuleBrand/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific price rule brand by the custom key")]
    public partial class GetPriceRuleBrandByKey : ImplementsKeyBase, IReturn<PriceRuleBrandModel> { }

    /// <summary>A ServiceStack Route to check price rule brand exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.View"),
        PublicAPI,
        Route("/Pricing/PriceRuleBrand/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckPriceRuleBrandExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check price rule brand exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.View"),
        PublicAPI,
        Route("/Pricing/PriceRuleBrand/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckPriceRuleBrandExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create price rule brand.</summary>
    /// <seealso cref="PriceRuleBrandModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Pricing/PriceRuleBrand/Create", "POST", Priority = 1,
            Summary = "Use to create a new price rule brand.")]
    public partial class CreatePriceRuleBrand : PriceRuleBrandModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert price rule brand.</summary>
    /// <seealso cref="PriceRuleBrandModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Pricing/PriceRuleBrand/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing price rule brand (as needed).")]
    public partial class UpsertPriceRuleBrand : PriceRuleBrandModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update price rule brand.</summary>
    /// <seealso cref="PriceRuleBrandModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Pricing/PriceRuleBrand/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing price rule brand.")]
    public partial class UpdatePriceRuleBrand : PriceRuleBrandModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate price rule brand.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Pricing/PriceRuleBrand/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific price rule brand from the system [Soft-Delete]")]
    public partial class DeactivatePriceRuleBrandByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate price rule brand by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.Deactivate"),
        PublicAPI,
        Route("/Pricing/PriceRuleBrand/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific price rule brand from the system [Soft-Delete]")]
    public partial class DeactivatePriceRuleBrandByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate price rule brand.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Pricing/PriceRuleBrand/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific price rule brand from the system [Restore from Soft-Delete]")]
    public partial class ReactivatePriceRuleBrandByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate price rule brand by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.Reactivate"),
        PublicAPI,
        Route("/Pricing/PriceRuleBrand/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific price rule brand from the system [Restore from Soft-Delete]")]
    public partial class ReactivatePriceRuleBrandByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete price rule brand.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Pricing/PriceRuleBrand/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific price rule brand from the system [Hard-Delete]")]
    public partial class DeletePriceRuleBrandByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete price rule brand by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Pricing.PriceRuleBrand.Delete"),
        PublicAPI,
        Route("/Pricing/PriceRuleBrand/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific price rule brand from the system [Hard-Delete]")]
    public partial class DeletePriceRuleBrandByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear price rule brand cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Pricing/PriceRuleBrand/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all price rule brand calls.")]
    public class ClearPriceRuleBrandCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class PriceRuleBrandServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetPriceRuleBrands"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetPriceRuleBrands request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IPriceRuleBrandModel, PriceRuleBrandModel, IPriceRuleBrandSearchModel, PriceRuleBrandPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.PriceRuleBrands)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetPriceRuleBrandsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetPriceRuleBrandsForConnect request)
        {
            return await Workflows.PriceRuleBrands.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetPriceRuleBrandsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetPriceRuleBrandsDigest request)
        {
            return await Workflows.PriceRuleBrands.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetPriceRuleBrandByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetPriceRuleBrandByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.PriceRuleBrands, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetPriceRuleBrandByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetPriceRuleBrandByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.PriceRuleBrands, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckPriceRuleBrandExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckPriceRuleBrandExistsByID request)
        {
            return await Workflows.PriceRuleBrands.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckPriceRuleBrandExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckPriceRuleBrandExistsByKey request)
        {
            return await Workflows.PriceRuleBrands.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertPriceRuleBrand"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertPriceRuleBrand request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPriceRuleBrandDataAsync,
                    () => Workflows.PriceRuleBrands.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreatePriceRuleBrand"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreatePriceRuleBrand request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPriceRuleBrandDataAsync,
                    () => Workflows.PriceRuleBrands.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdatePriceRuleBrand"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdatePriceRuleBrand request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPriceRuleBrandDataAsync,
                    () => Workflows.PriceRuleBrands.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivatePriceRuleBrandByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivatePriceRuleBrandByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPriceRuleBrandDataAsync,
                    () => Workflows.PriceRuleBrands.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivatePriceRuleBrandByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivatePriceRuleBrandByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPriceRuleBrandDataAsync,
                    () => Workflows.PriceRuleBrands.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivatePriceRuleBrandByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivatePriceRuleBrandByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPriceRuleBrandDataAsync,
                    () => Workflows.PriceRuleBrands.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivatePriceRuleBrandByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivatePriceRuleBrandByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPriceRuleBrandDataAsync,
                    () => Workflows.PriceRuleBrands.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeletePriceRuleBrandByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeletePriceRuleBrandByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPriceRuleBrandDataAsync,
                    () => Workflows.PriceRuleBrands.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeletePriceRuleBrandByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeletePriceRuleBrandByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPriceRuleBrandDataAsync,
                    () => Workflows.PriceRuleBrands.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearPriceRuleBrandCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearPriceRuleBrandCache request)
        {
            await ClearCachedPriceRuleBrandDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedPriceRuleBrandDataAsync()
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
                    UrnId.Create<GetPriceRuleBrands>(string.Empty),
                    UrnId.Create<GetPriceRuleBrandByID>(string.Empty),
                    UrnId.Create<GetPriceRuleBrandByKey>(string.Empty),
                    UrnId.Create<CheckPriceRuleBrandExistsByID>(string.Empty),
                    UrnId.Create<CheckPriceRuleBrandExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class PriceRuleBrandService : PriceRuleBrandServiceBase { }
}
