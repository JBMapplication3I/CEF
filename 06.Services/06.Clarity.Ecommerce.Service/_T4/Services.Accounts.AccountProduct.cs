// <autogenerated>
// <copyright file="AccountProductService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account product service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of account products.</summary>
    /// <seealso cref="AccountProductSearchModel"/>
    /// <seealso cref="IReturn{AccountProductPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Accounts/AccountProducts", "GET", Priority = 1,
            Summary = "Use to get a list of account products")]
    public partial class GetAccountProducts : AccountProductSearchModel, IReturn<AccountProductPagedResults> { }

    /// <summary>A ServiceStack Route to get account products for connect.</summary>
    /// <seealso cref="AccountProductSearchModel"/>
    /// <seealso cref="IReturn{List{AccountProductModel}}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.View"),
        PublicAPI,
        Route("/Accounts/AccountProductsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all account products")]
    public partial class GetAccountProductsForConnect : AccountProductSearchModel, IReturn<List<AccountProductModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all account products.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.View"),
        PublicAPI,
        Route("/Accounts/AccountProductsDigest", "GET",
            Summary = "Use to get a hash representing each account products")]
    public partial class GetAccountProductsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get account product.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{AccountProductModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Accounts/AccountProduct/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific account product")]
    public partial class GetAccountProductByID : ImplementsIDBase, IReturn<AccountProductModel> { }

    /// <summary>A ServiceStack Route to get account product.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{AccountProductModel}"/>
    [PublicAPI,
        Route("/Accounts/AccountProduct/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific account product by the custom key")]
    public partial class GetAccountProductByKey : ImplementsKeyBase, IReturn<AccountProductModel> { }

    /// <summary>A ServiceStack Route to check account product exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.View"),
        PublicAPI,
        Route("/Accounts/AccountProduct/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckAccountProductExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check account product exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.View"),
        PublicAPI,
        Route("/Accounts/AccountProduct/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckAccountProductExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create account product.</summary>
    /// <seealso cref="AccountProductModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Accounts/AccountProduct/Create", "POST", Priority = 1,
            Summary = "Use to create a new account product.")]
    public partial class CreateAccountProduct : AccountProductModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert account product.</summary>
    /// <seealso cref="AccountProductModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Accounts/AccountProduct/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing account product (as needed).")]
    public partial class UpsertAccountProduct : AccountProductModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update account product.</summary>
    /// <seealso cref="AccountProductModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Accounts/AccountProduct/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing account product.")]
    public partial class UpdateAccountProduct : AccountProductModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate account product.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Accounts/AccountProduct/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific account product from the system [Soft-Delete]")]
    public partial class DeactivateAccountProductByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate account product by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.Deactivate"),
        PublicAPI,
        Route("/Accounts/AccountProduct/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific account product from the system [Soft-Delete]")]
    public partial class DeactivateAccountProductByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate account product.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Accounts/AccountProduct/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific account product from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAccountProductByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate account product by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.Reactivate"),
        PublicAPI,
        Route("/Accounts/AccountProduct/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific account product from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAccountProductByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete account product.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Accounts/AccountProduct/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific account product from the system [Hard-Delete]")]
    public partial class DeleteAccountProductByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete account product by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountProduct.Delete"),
        PublicAPI,
        Route("/Accounts/AccountProduct/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific account product from the system [Hard-Delete]")]
    public partial class DeleteAccountProductByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear account product cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Accounts/AccountProduct/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all account product calls.")]
    public class ClearAccountProductCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class AccountProductServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetAccountProducts"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAccountProducts request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IAccountProductModel, AccountProductModel, IAccountProductSearchModel, AccountProductPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.AccountProducts)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAccountProductsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetAccountProductsForConnect request)
        {
            return await Workflows.AccountProducts.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAccountProductsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAccountProductsDigest request)
        {
            return await Workflows.AccountProducts.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetAccountProductByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAccountProductByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.AccountProducts, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAccountProductByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAccountProductByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.AccountProducts, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckAccountProductExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAccountProductExistsByID request)
        {
            return await Workflows.AccountProducts.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAccountProductExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAccountProductExistsByKey request)
        {
            return await Workflows.AccountProducts.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertAccountProduct"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertAccountProduct request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountProductDataAsync,
                    () => Workflows.AccountProducts.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateAccountProduct"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateAccountProduct request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountProductDataAsync,
                    () => Workflows.AccountProducts.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateAccountProduct"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateAccountProduct request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountProductDataAsync,
                    () => Workflows.AccountProducts.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateAccountProductByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAccountProductByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountProductDataAsync,
                    () => Workflows.AccountProducts.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateAccountProductByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAccountProductByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountProductDataAsync,
                    () => Workflows.AccountProducts.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateAccountProductByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAccountProductByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountProductDataAsync,
                    () => Workflows.AccountProducts.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateAccountProductByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAccountProductByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountProductDataAsync,
                    () => Workflows.AccountProducts.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteAccountProductByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAccountProductByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountProductDataAsync,
                    () => Workflows.AccountProducts.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteAccountProductByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAccountProductByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountProductDataAsync,
                    () => Workflows.AccountProducts.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearAccountProductCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearAccountProductCache request)
        {
            await ClearCachedAccountProductDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedAccountProductDataAsync()
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
                    UrnId.Create<GetAccountProducts>(string.Empty),
                    UrnId.Create<GetAccountProductByID>(string.Empty),
                    UrnId.Create<GetAccountProductByKey>(string.Empty),
                    UrnId.Create<CheckAccountProductExistsByID>(string.Empty),
                    UrnId.Create<CheckAccountProductExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class AccountProductService : AccountProductServiceBase { }
}
