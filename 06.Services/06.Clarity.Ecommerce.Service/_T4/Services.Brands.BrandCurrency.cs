// <autogenerated>
// <copyright file="BrandCurrencyService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand currency service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of brand currencies.</summary>
    /// <seealso cref="BrandCurrencySearchModel"/>
    /// <seealso cref="IReturn{BrandCurrencyPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/BrandCurrencies", "GET", Priority = 1,
            Summary = "Use to get a list of brand currencies")]
    public partial class GetBrandCurrencies : BrandCurrencySearchModel, IReturn<BrandCurrencyPagedResults> { }

    /// <summary>A ServiceStack Route to get brand currencies for connect.</summary>
    /// <seealso cref="BrandCurrencySearchModel"/>
    /// <seealso cref="IReturn{List{BrandCurrencyModel}}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.View"),
        PublicAPI,
        Route("/Brands/BrandCurrenciesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all brand currencies")]
    public partial class GetBrandCurrenciesForConnect : BrandCurrencySearchModel, IReturn<List<BrandCurrencyModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all brand currencies.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.View"),
        PublicAPI,
        Route("/Brands/BrandCurrenciesDigest", "GET",
            Summary = "Use to get a hash representing each brand currencies")]
    public partial class GetBrandCurrenciesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get brand currency.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{BrandCurrencyModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/BrandCurrency/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific brand currency")]
    public partial class GetBrandCurrencyByID : ImplementsIDBase, IReturn<BrandCurrencyModel> { }

    /// <summary>A ServiceStack Route to get brand currency.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{BrandCurrencyModel}"/>
    [PublicAPI,
        Route("/Brands/BrandCurrency/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific brand currency by the custom key")]
    public partial class GetBrandCurrencyByKey : ImplementsKeyBase, IReturn<BrandCurrencyModel> { }

    /// <summary>A ServiceStack Route to check brand currency exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.View"),
        PublicAPI,
        Route("/Brands/BrandCurrency/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckBrandCurrencyExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check brand currency exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.View"),
        PublicAPI,
        Route("/Brands/BrandCurrency/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckBrandCurrencyExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create brand currency.</summary>
    /// <seealso cref="BrandCurrencyModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandCurrency/Create", "POST", Priority = 1,
            Summary = "Use to create a new brand currency.")]
    public partial class CreateBrandCurrency : BrandCurrencyModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert brand currency.</summary>
    /// <seealso cref="BrandCurrencyModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Brands/BrandCurrency/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing brand currency (as needed).")]
    public partial class UpsertBrandCurrency : BrandCurrencyModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update brand currency.</summary>
    /// <seealso cref="BrandCurrencyModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandCurrency/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing brand currency.")]
    public partial class UpdateBrandCurrency : BrandCurrencyModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate brand currency.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandCurrency/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific brand currency from the system [Soft-Delete]")]
    public partial class DeactivateBrandCurrencyByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate brand currency by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.Deactivate"),
        PublicAPI,
        Route("/Brands/BrandCurrency/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific brand currency from the system [Soft-Delete]")]
    public partial class DeactivateBrandCurrencyByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate brand currency.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandCurrency/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific brand currency from the system [Restore from Soft-Delete]")]
    public partial class ReactivateBrandCurrencyByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate brand currency by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.Reactivate"),
        PublicAPI,
        Route("/Brands/BrandCurrency/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific brand currency from the system [Restore from Soft-Delete]")]
    public partial class ReactivateBrandCurrencyByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete brand currency.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Brands/BrandCurrency/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific brand currency from the system [Hard-Delete]")]
    public partial class DeleteBrandCurrencyByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete brand currency by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Brands.BrandCurrency.Delete"),
        PublicAPI,
        Route("/Brands/BrandCurrency/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific brand currency from the system [Hard-Delete]")]
    public partial class DeleteBrandCurrencyByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear brand currency cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Brands/BrandCurrency/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all brand currency calls.")]
    public class ClearBrandCurrencyCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class BrandCurrencyServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetBrandCurrencies"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandCurrencies request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IBrandCurrencyModel, BrandCurrencyModel, IBrandCurrencySearchModel, BrandCurrencyPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.BrandCurrencies)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandCurrenciesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetBrandCurrenciesForConnect request)
        {
            return await Workflows.BrandCurrencies.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandCurrenciesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandCurrenciesDigest request)
        {
            return await Workflows.BrandCurrencies.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetBrandCurrencyByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandCurrencyByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.BrandCurrencies, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetBrandCurrencyByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetBrandCurrencyByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.BrandCurrencies, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckBrandCurrencyExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckBrandCurrencyExistsByID request)
        {
            return await Workflows.BrandCurrencies.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckBrandCurrencyExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckBrandCurrencyExistsByKey request)
        {
            return await Workflows.BrandCurrencies.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertBrandCurrency"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertBrandCurrency request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandCurrencyDataAsync,
                    () => Workflows.BrandCurrencies.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateBrandCurrency"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateBrandCurrency request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandCurrencyDataAsync,
                    () => Workflows.BrandCurrencies.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateBrandCurrency"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateBrandCurrency request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandCurrencyDataAsync,
                    () => Workflows.BrandCurrencies.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateBrandCurrencyByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateBrandCurrencyByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandCurrencyDataAsync,
                    () => Workflows.BrandCurrencies.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateBrandCurrencyByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateBrandCurrencyByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandCurrencyDataAsync,
                    () => Workflows.BrandCurrencies.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateBrandCurrencyByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateBrandCurrencyByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandCurrencyDataAsync,
                    () => Workflows.BrandCurrencies.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateBrandCurrencyByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateBrandCurrencyByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandCurrencyDataAsync,
                    () => Workflows.BrandCurrencies.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteBrandCurrencyByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteBrandCurrencyByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandCurrencyDataAsync,
                    () => Workflows.BrandCurrencies.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteBrandCurrencyByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteBrandCurrencyByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedBrandCurrencyDataAsync,
                    () => Workflows.BrandCurrencies.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearBrandCurrencyCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearBrandCurrencyCache request)
        {
            await ClearCachedBrandCurrencyDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedBrandCurrencyDataAsync()
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
                    UrnId.Create<GetBrandCurrencies>(string.Empty),
                    UrnId.Create<GetBrandCurrencyByID>(string.Empty),
                    UrnId.Create<GetBrandCurrencyByKey>(string.Empty),
                    UrnId.Create<CheckBrandCurrencyExistsByID>(string.Empty),
                    UrnId.Create<CheckBrandCurrencyExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class BrandCurrencyService : BrandCurrencyServiceBase { }
}
