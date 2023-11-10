// <autogenerated>
// <copyright file="AccountImageService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account image service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of account images.</summary>
    /// <seealso cref="AccountImageSearchModel"/>
    /// <seealso cref="IReturn{AccountImagePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Accounts/AccountImages", "GET", Priority = 1,
            Summary = "Use to get a list of account images")]
    public partial class GetAccountImages : AccountImageSearchModel, IReturn<AccountImagePagedResults> { }

    /// <summary>A ServiceStack Route to get account images for connect.</summary>
    /// <seealso cref="AccountImageSearchModel"/>
    /// <seealso cref="IReturn{List{AccountImageModel}}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.View"),
        PublicAPI,
        Route("/Accounts/AccountImagesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all account images")]
    public partial class GetAccountImagesForConnect : AccountImageSearchModel, IReturn<List<AccountImageModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all account images.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.View"),
        PublicAPI,
        Route("/Accounts/AccountImagesDigest", "GET",
            Summary = "Use to get a hash representing each account images")]
    public partial class GetAccountImagesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get account image.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{AccountImageModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Accounts/AccountImage/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific account image")]
    public partial class GetAccountImageByID : ImplementsIDBase, IReturn<AccountImageModel> { }

    /// <summary>A ServiceStack Route to get account image.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{AccountImageModel}"/>
    [PublicAPI,
        Route("/Accounts/AccountImage/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific account image by the custom key")]
    public partial class GetAccountImageByKey : ImplementsKeyBase, IReturn<AccountImageModel> { }

    /// <summary>A ServiceStack Route to get account image.</summary>
    /// <seealso cref="IReturn{AccountImageModel}"/>
    [PublicAPI,
        Route("/Accounts/AccountImage/Name", "GET", Priority = 1,
            Summary = "Use to get a specific account image by the name")]
    public partial class GetAccountImageByName : ImplementsNameBase, IReturn<AccountImageModel> { }

    /// <summary>A ServiceStack Route to check account image exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.View"),
        PublicAPI,
        Route("/Accounts/AccountImage/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckAccountImageExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check account image exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.View"),
        PublicAPI,
        Route("/Accounts/AccountImage/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckAccountImageExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check account image exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.View"),
        PublicAPI,
        Route("/Accounts/AccountImage/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckAccountImageExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create account image.</summary>
    /// <seealso cref="AccountImageModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Accounts/AccountImage/Create", "POST", Priority = 1,
            Summary = "Use to create a new account image.")]
    public partial class CreateAccountImage : AccountImageModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert account image.</summary>
    /// <seealso cref="AccountImageModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Accounts/AccountImage/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing account image (as needed).")]
    public partial class UpsertAccountImage : AccountImageModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update account image.</summary>
    /// <seealso cref="AccountImageModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Accounts/AccountImage/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing account image.")]
    public partial class UpdateAccountImage : AccountImageModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate account image.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Accounts/AccountImage/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific account image from the system [Soft-Delete]")]
    public partial class DeactivateAccountImageByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate account image by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.Deactivate"),
        PublicAPI,
        Route("/Accounts/AccountImage/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific account image from the system [Soft-Delete]")]
    public partial class DeactivateAccountImageByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate account image.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Accounts/AccountImage/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific account image from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAccountImageByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate account image by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.Reactivate"),
        PublicAPI,
        Route("/Accounts/AccountImage/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific account image from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAccountImageByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete account image.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Accounts/AccountImage/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific account image from the system [Hard-Delete]")]
    public partial class DeleteAccountImageByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete account image by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Accounts.AccountImage.Delete"),
        PublicAPI,
        Route("/Accounts/AccountImage/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific account image from the system [Hard-Delete]")]
    public partial class DeleteAccountImageByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear account image cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Accounts/AccountImage/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all account image calls.")]
    public class ClearAccountImageCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class AccountImageServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetAccountImages"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAccountImages request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IAccountImageModel, AccountImageModel, IAccountImageSearchModel, AccountImagePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.AccountImages)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAccountImagesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetAccountImagesForConnect request)
        {
            return await Workflows.AccountImages.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAccountImagesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAccountImagesDigest request)
        {
            return await Workflows.AccountImages.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetAccountImageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAccountImageByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.AccountImages, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAccountImageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAccountImageByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.AccountImages, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAccountImageByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAccountImageByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.AccountImages, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckAccountImageExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAccountImageExistsByID request)
        {
            return await Workflows.AccountImages.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAccountImageExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAccountImageExistsByKey request)
        {
            return await Workflows.AccountImages.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAccountImageExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAccountImageExistsByName request)
        {
            return await Workflows.AccountImages.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertAccountImage"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertAccountImage request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountImageDataAsync,
                    () => Workflows.AccountImages.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateAccountImage"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateAccountImage request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountImageDataAsync,
                    () => Workflows.AccountImages.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateAccountImage"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateAccountImage request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountImageDataAsync,
                    () => Workflows.AccountImages.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateAccountImageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAccountImageByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountImageDataAsync,
                    () => Workflows.AccountImages.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateAccountImageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAccountImageByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountImageDataAsync,
                    () => Workflows.AccountImages.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateAccountImageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAccountImageByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountImageDataAsync,
                    () => Workflows.AccountImages.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateAccountImageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAccountImageByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountImageDataAsync,
                    () => Workflows.AccountImages.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteAccountImageByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAccountImageByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountImageDataAsync,
                    () => Workflows.AccountImages.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteAccountImageByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAccountImageByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAccountImageDataAsync,
                    () => Workflows.AccountImages.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearAccountImageCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearAccountImageCache request)
        {
            await ClearCachedAccountImageDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedAccountImageDataAsync()
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
                    UrnId.Create<GetAccountImages>(string.Empty),
                    UrnId.Create<GetAccountImageByID>(string.Empty),
                    UrnId.Create<GetAccountImageByKey>(string.Empty),
                    UrnId.Create<GetAccountImageByName>(string.Empty),
                    UrnId.Create<CheckAccountImageExistsByID>(string.Empty),
                    UrnId.Create<CheckAccountImageExistsByKey>(string.Empty),
                    UrnId.Create<CheckAccountImageExistsByName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class AccountImageService : AccountImageServiceBase { }
}
