// <autogenerated>
// <copyright file="StoreAccountService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store account service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of store accounts.</summary>
    /// <seealso cref="StoreAccountSearchModel"/>
    /// <seealso cref="IReturn{StoreAccountPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Stores/StoreAccounts", "GET", Priority = 1,
            Summary = "Use to get a list of store accounts")]
    public partial class GetStoreAccounts : StoreAccountSearchModel, IReturn<StoreAccountPagedResults> { }

    /// <summary>A ServiceStack Route to get store accounts for connect.</summary>
    /// <seealso cref="StoreAccountSearchModel"/>
    /// <seealso cref="IReturn{List{StoreAccountModel}}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.View"),
        PublicAPI,
        Route("/Stores/StoreAccountsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all store accounts")]
    public partial class GetStoreAccountsForConnect : StoreAccountSearchModel, IReturn<List<StoreAccountModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all store accounts.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.View"),
        PublicAPI,
        Route("/Stores/StoreAccountsDigest", "GET",
            Summary = "Use to get a hash representing each store accounts")]
    public partial class GetStoreAccountsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get store account.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{StoreAccountModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Stores/StoreAccount/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific store account")]
    public partial class GetStoreAccountByID : ImplementsIDBase, IReturn<StoreAccountModel> { }

    /// <summary>A ServiceStack Route to get store account.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{StoreAccountModel}"/>
    [PublicAPI,
        Route("/Stores/StoreAccount/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific store account by the custom key")]
    public partial class GetStoreAccountByKey : ImplementsKeyBase, IReturn<StoreAccountModel> { }

    /// <summary>A ServiceStack Route to check store account exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.View"),
        PublicAPI,
        Route("/Stores/StoreAccount/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckStoreAccountExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check store account exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.View"),
        PublicAPI,
        Route("/Stores/StoreAccount/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckStoreAccountExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create store account.</summary>
    /// <seealso cref="StoreAccountModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Stores/StoreAccount/Create", "POST", Priority = 1,
            Summary = "Use to create a new store account.")]
    public partial class CreateStoreAccount : StoreAccountModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert store account.</summary>
    /// <seealso cref="StoreAccountModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Stores/StoreAccount/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing store account (as needed).")]
    public partial class UpsertStoreAccount : StoreAccountModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update store account.</summary>
    /// <seealso cref="StoreAccountModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Stores/StoreAccount/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing store account.")]
    public partial class UpdateStoreAccount : StoreAccountModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate store account.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Stores/StoreAccount/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific store account from the system [Soft-Delete]")]
    public partial class DeactivateStoreAccountByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate store account by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.Deactivate"),
        PublicAPI,
        Route("/Stores/StoreAccount/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific store account from the system [Soft-Delete]")]
    public partial class DeactivateStoreAccountByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate store account.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Stores/StoreAccount/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific store account from the system [Restore from Soft-Delete]")]
    public partial class ReactivateStoreAccountByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate store account by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.Reactivate"),
        PublicAPI,
        Route("/Stores/StoreAccount/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific store account from the system [Restore from Soft-Delete]")]
    public partial class ReactivateStoreAccountByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete store account.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Stores/StoreAccount/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific store account from the system [Hard-Delete]")]
    public partial class DeleteStoreAccountByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete store account by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Stores.StoreAccount.Delete"),
        PublicAPI,
        Route("/Stores/StoreAccount/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific store account from the system [Hard-Delete]")]
    public partial class DeleteStoreAccountByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear store account cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Stores/StoreAccount/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all store account calls.")]
    public class ClearStoreAccountCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class StoreAccountServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetStoreAccounts"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetStoreAccounts request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IStoreAccountModel, StoreAccountModel, IStoreAccountSearchModel, StoreAccountPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.StoreAccounts)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetStoreAccountsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetStoreAccountsForConnect request)
        {
            return await Workflows.StoreAccounts.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetStoreAccountsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetStoreAccountsDigest request)
        {
            return await Workflows.StoreAccounts.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetStoreAccountByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetStoreAccountByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.StoreAccounts, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetStoreAccountByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetStoreAccountByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.StoreAccounts, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckStoreAccountExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckStoreAccountExistsByID request)
        {
            return await Workflows.StoreAccounts.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckStoreAccountExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckStoreAccountExistsByKey request)
        {
            return await Workflows.StoreAccounts.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertStoreAccount"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertStoreAccount request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedStoreAccountDataAsync,
                    () => Workflows.StoreAccounts.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateStoreAccount"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateStoreAccount request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedStoreAccountDataAsync,
                    () => Workflows.StoreAccounts.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateStoreAccount"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateStoreAccount request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedStoreAccountDataAsync,
                    () => Workflows.StoreAccounts.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateStoreAccountByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateStoreAccountByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedStoreAccountDataAsync,
                    () => Workflows.StoreAccounts.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateStoreAccountByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateStoreAccountByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedStoreAccountDataAsync,
                    () => Workflows.StoreAccounts.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateStoreAccountByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateStoreAccountByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedStoreAccountDataAsync,
                    () => Workflows.StoreAccounts.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateStoreAccountByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateStoreAccountByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedStoreAccountDataAsync,
                    () => Workflows.StoreAccounts.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteStoreAccountByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteStoreAccountByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedStoreAccountDataAsync,
                    () => Workflows.StoreAccounts.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteStoreAccountByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteStoreAccountByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedStoreAccountDataAsync,
                    () => Workflows.StoreAccounts.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearStoreAccountCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearStoreAccountCache request)
        {
            await ClearCachedStoreAccountDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedStoreAccountDataAsync()
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
                    UrnId.Create<GetStoreAccounts>(string.Empty),
                    UrnId.Create<GetStoreAccountByID>(string.Empty),
                    UrnId.Create<GetStoreAccountByKey>(string.Empty),
                    UrnId.Create<CheckStoreAccountExistsByID>(string.Empty),
                    UrnId.Create<CheckStoreAccountExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class StoreAccountService : StoreAccountServiceBase { }
}
