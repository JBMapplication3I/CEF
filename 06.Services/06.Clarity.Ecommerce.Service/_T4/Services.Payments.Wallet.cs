// <autogenerated>
// <copyright file="WalletService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the wallet service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of wallets.</summary>
    /// <seealso cref="WalletSearchModel"/>
    /// <seealso cref="IReturn{WalletPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/Wallets", "GET", Priority = 1,
            Summary = "Use to get a list of wallets")]
    public partial class GetWallets : WalletSearchModel, IReturn<WalletPagedResults> { }

    /// <summary>A ServiceStack Route to get wallets for connect.</summary>
    /// <seealso cref="WalletSearchModel"/>
    /// <seealso cref="IReturn{List{WalletModel}}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.View"),
        PublicAPI,
        Route("/Payments/WalletsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all wallets")]
    public partial class GetWalletsForConnect : WalletSearchModel, IReturn<List<WalletModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all wallets.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.View"),
        PublicAPI,
        Route("/Payments/WalletsDigest", "GET",
            Summary = "Use to get a hash representing each wallets")]
    public partial class GetWalletsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get wallet.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{WalletModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/Wallet/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific wallet")]
    public partial class GetWalletByID : ImplementsIDBase, IReturn<WalletModel> { }

    /// <summary>A ServiceStack Route to get wallet.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{WalletModel}"/>
    [PublicAPI,
        Route("/Payments/Wallet/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific wallet by the custom key")]
    public partial class GetWalletByKey : ImplementsKeyBase, IReturn<WalletModel> { }

    /// <summary>A ServiceStack Route to get wallet.</summary>
    /// <seealso cref="IReturn{WalletModel}"/>
    [PublicAPI,
        Route("/Payments/Wallet/Name", "GET", Priority = 1,
            Summary = "Use to get a specific wallet by the name")]
    public partial class GetWalletByName : ImplementsNameBase, IReturn<WalletModel> { }

    /// <summary>A ServiceStack Route to check wallet exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.View"),
        PublicAPI,
        Route("/Payments/Wallet/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckWalletExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check wallet exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.View"),
        PublicAPI,
        Route("/Payments/Wallet/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckWalletExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check wallet exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.View"),
        PublicAPI,
        Route("/Payments/Wallet/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckWalletExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create wallet.</summary>
    /// <seealso cref="WalletModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/Wallet/Create", "POST", Priority = 1,
            Summary = "Use to create a new wallet.")]
    public partial class CreateWallet : WalletModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert wallet.</summary>
    /// <seealso cref="WalletModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Payments/Wallet/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing wallet (as needed).")]
    public partial class UpsertWallet : WalletModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update wallet.</summary>
    /// <seealso cref="WalletModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/Wallet/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing wallet.")]
    public partial class UpdateWallet : WalletModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate wallet.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/Wallet/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific wallet from the system [Soft-Delete]")]
    public partial class DeactivateWalletByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate wallet by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.Deactivate"),
        PublicAPI,
        Route("/Payments/Wallet/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific wallet from the system [Soft-Delete]")]
    public partial class DeactivateWalletByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate wallet.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/Wallet/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific wallet from the system [Restore from Soft-Delete]")]
    public partial class ReactivateWalletByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate wallet by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.Reactivate"),
        PublicAPI,
        Route("/Payments/Wallet/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific wallet from the system [Restore from Soft-Delete]")]
    public partial class ReactivateWalletByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete wallet.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Payments/Wallet/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific wallet from the system [Hard-Delete]")]
    public partial class DeleteWalletByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete wallet by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Payments.Wallet.Delete"),
        PublicAPI,
        Route("/Payments/Wallet/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific wallet from the system [Hard-Delete]")]
    public partial class DeleteWalletByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear wallet cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Payments/Wallet/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all wallet calls.")]
    public class ClearWalletCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class WalletServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetWallets"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetWallets request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IWalletModel, WalletModel, IWalletSearchModel, WalletPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.Wallets)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetWalletsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetWalletsForConnect request)
        {
            return await Workflows.Wallets.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetWalletsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetWalletsDigest request)
        {
            return await Workflows.Wallets.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetWalletByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetWalletByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.Wallets, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetWalletByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetWalletByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.Wallets, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetWalletByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetWalletByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.Wallets, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckWalletExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckWalletExistsByID request)
        {
            return await Workflows.Wallets.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckWalletExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckWalletExistsByKey request)
        {
            return await Workflows.Wallets.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckWalletExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckWalletExistsByName request)
        {
            return await Workflows.Wallets.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertWallet"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertWallet request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedWalletDataAsync,
                    () => Workflows.Wallets.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateWallet"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateWallet request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedWalletDataAsync,
                    () => Workflows.Wallets.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateWallet"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateWallet request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedWalletDataAsync,
                    () => Workflows.Wallets.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateWalletByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateWalletByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedWalletDataAsync,
                    () => Workflows.Wallets.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateWalletByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateWalletByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedWalletDataAsync,
                    () => Workflows.Wallets.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateWalletByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateWalletByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedWalletDataAsync,
                    () => Workflows.Wallets.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateWalletByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateWalletByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedWalletDataAsync,
                    () => Workflows.Wallets.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteWalletByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteWalletByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedWalletDataAsync,
                    () => Workflows.Wallets.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteWalletByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteWalletByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedWalletDataAsync,
                    () => Workflows.Wallets.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearWalletCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearWalletCache request)
        {
            await ClearCachedWalletDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedWalletDataAsync()
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
                    UrnId.Create<GetWallets>(string.Empty),
                    UrnId.Create<GetWalletByID>(string.Empty),
                    UrnId.Create<GetWalletByKey>(string.Empty),
                    UrnId.Create<GetWalletByName>(string.Empty),
                    UrnId.Create<CheckWalletExistsByID>(string.Empty),
                    UrnId.Create<CheckWalletExistsByKey>(string.Empty),
                    UrnId.Create<CheckWalletExistsByName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class WalletService : WalletServiceBase { }
}
