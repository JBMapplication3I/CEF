// <autogenerated>
// <copyright file="SalesReturnFileService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return file service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of sales return files.</summary>
    /// <seealso cref="SalesReturnFileSearchModel"/>
    /// <seealso cref="IReturn{SalesReturnFilePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnFiles", "GET", Priority = 1,
            Summary = "Use to get a list of sales return files")]
    public partial class GetSalesReturnFiles : SalesReturnFileSearchModel, IReturn<SalesReturnFilePagedResults> { }

    /// <summary>A ServiceStack Route to get sales return files for connect.</summary>
    /// <seealso cref="SalesReturnFileSearchModel"/>
    /// <seealso cref="IReturn{List{SalesReturnFileModel}}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.View"),
        PublicAPI,
        Route("/Returning/SalesReturnFilesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all sales return files")]
    public partial class GetSalesReturnFilesForConnect : SalesReturnFileSearchModel, IReturn<List<SalesReturnFileModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all sales return files.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.View"),
        PublicAPI,
        Route("/Returning/SalesReturnFilesDigest", "GET",
            Summary = "Use to get a hash representing each sales return files")]
    public partial class GetSalesReturnFilesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get sales return file.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SalesReturnFileModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnFile/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales return file")]
    public partial class GetSalesReturnFileByID : ImplementsIDBase, IReturn<SalesReturnFileModel> { }

    /// <summary>A ServiceStack Route to get sales return file.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{SalesReturnFileModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnFile/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific sales return file by the custom key")]
    public partial class GetSalesReturnFileByKey : ImplementsKeyBase, IReturn<SalesReturnFileModel> { }

    /// <summary>A ServiceStack Route to get sales return file.</summary>
    /// <seealso cref="IReturn{SalesReturnFileModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnFile/Name", "GET", Priority = 1,
            Summary = "Use to get a specific sales return file by the name")]
    public partial class GetSalesReturnFileByName : ImplementsNameBase, IReturn<SalesReturnFileModel> { }

    /// <summary>A ServiceStack Route to get sales return file.</summary>
    /// <seealso cref="IReturn{SalesReturnFileModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnFile/SeoUrl", "GET", Priority = 1,
            Summary = "Use to get a specific sales return file by the SEO URL")]
    public partial class GetSalesReturnFileBySeoUrl : ImplementsSeoUrlBase, IReturn<SalesReturnFileModel> { }

    /// <summary>A ServiceStack Route to check sales return file exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.View"),
        PublicAPI,
        Route("/Returning/SalesReturnFile/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnFileExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return file exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.View"),
        PublicAPI,
        Route("/Returning/SalesReturnFile/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnFileExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return file exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.View"),
        PublicAPI,
        Route("/Returning/SalesReturnFile/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnFileExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return file exists by SEO URL.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.View"),
        PublicAPI,
        Route("/Returning/SalesReturnFile/Exists/SeoUrl", "GET", Priority = 1,
            Summary = "Check if this SEO URL exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnFileExistsBySeoUrl : ImplementsSeoUrlBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create sales return file.</summary>
    /// <seealso cref="SalesReturnFileModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnFile/Create", "POST", Priority = 1,
            Summary = "Use to create a new sales return file.")]
    public partial class CreateSalesReturnFile : SalesReturnFileModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert sales return file.</summary>
    /// <seealso cref="SalesReturnFileModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Returning/SalesReturnFile/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing sales return file (as needed).")]
    public partial class UpsertSalesReturnFile : SalesReturnFileModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update sales return file.</summary>
    /// <seealso cref="SalesReturnFileModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnFile/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing sales return file.")]
    public partial class UpdateSalesReturnFile : SalesReturnFileModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate sales return file.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnFile/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales return file from the system [Soft-Delete]")]
    public partial class DeactivateSalesReturnFileByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate sales return file by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.Deactivate"),
        PublicAPI,
        Route("/Returning/SalesReturnFile/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales return file from the system [Soft-Delete]")]
    public partial class DeactivateSalesReturnFileByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales return file.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnFile/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales return file from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesReturnFileByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales return file by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.Reactivate"),
        PublicAPI,
        Route("/Returning/SalesReturnFile/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales return file from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesReturnFileByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales return file.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnFile/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific sales return file from the system [Hard-Delete]")]
    public partial class DeleteSalesReturnFileByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales return file by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnFile.Delete"),
        PublicAPI,
        Route("/Returning/SalesReturnFile/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific sales return file from the system [Hard-Delete]")]
    public partial class DeleteSalesReturnFileByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear sales return file cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnFile/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all sales return file calls.")]
    public class ClearSalesReturnFileCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SalesReturnFileServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSalesReturnFiles"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnFiles request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ISalesReturnFileModel, SalesReturnFileModel, ISalesReturnFileSearchModel, SalesReturnFilePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesReturnFiles)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnFilesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSalesReturnFilesForConnect request)
        {
            return await Workflows.SalesReturnFiles.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnFilesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnFilesDigest request)
        {
            return await Workflows.SalesReturnFiles.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSalesReturnFileByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnFileByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.SalesReturnFiles, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnFileByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnFileByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.SalesReturnFiles, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnFileByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnFileByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.SalesReturnFiles, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnFileBySeoUrl"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnFileBySeoUrl request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultBySeoUrlSingleAsync(request, Workflows.SalesReturnFiles, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSalesReturnFileExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnFileExistsByID request)
        {
            return await Workflows.SalesReturnFiles.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnFileExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnFileExistsByKey request)
        {
            return await Workflows.SalesReturnFiles.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnFileExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnFileExistsByName request)
        {
            return await Workflows.SalesReturnFiles.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnFileExistsBySeoUrl"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnFileExistsBySeoUrl request)
        {
            return await Workflows.SalesReturnFiles.CheckExistsBySeoUrlAsync(request.SeoUrl, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSalesReturnFile"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSalesReturnFile request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnFileDataAsync,
                    () => Workflows.SalesReturnFiles.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSalesReturnFile"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSalesReturnFile request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnFileDataAsync,
                    () => Workflows.SalesReturnFiles.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSalesReturnFile"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSalesReturnFile request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnFileDataAsync,
                    () => Workflows.SalesReturnFiles.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSalesReturnFileByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesReturnFileByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnFileDataAsync,
                    () => Workflows.SalesReturnFiles.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSalesReturnFileByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesReturnFileByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnFileDataAsync,
                    () => Workflows.SalesReturnFiles.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSalesReturnFileByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesReturnFileByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnFileDataAsync,
                    () => Workflows.SalesReturnFiles.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSalesReturnFileByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesReturnFileByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnFileDataAsync,
                    () => Workflows.SalesReturnFiles.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSalesReturnFileByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesReturnFileByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnFileDataAsync,
                    () => Workflows.SalesReturnFiles.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSalesReturnFileByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesReturnFileByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnFileDataAsync,
                    () => Workflows.SalesReturnFiles.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSalesReturnFileCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSalesReturnFileCache request)
        {
            await ClearCachedSalesReturnFileDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSalesReturnFileDataAsync()
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
                    UrnId.Create<GetSalesReturnFiles>(string.Empty),
                    UrnId.Create<GetSalesReturnFileByID>(string.Empty),
                    UrnId.Create<GetSalesReturnFileByKey>(string.Empty),
                    UrnId.Create<GetSalesReturnFileByName>(string.Empty),
                    UrnId.Create<GetSalesReturnFileBySeoUrl>(string.Empty),
                    UrnId.Create<CheckSalesReturnFileExistsByID>(string.Empty),
                    UrnId.Create<CheckSalesReturnFileExistsByKey>(string.Empty),
                    UrnId.Create<CheckSalesReturnFileExistsByName>(string.Empty),
                    UrnId.Create<CheckSalesReturnFileExistsBySeoUrl>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SalesReturnFileService : SalesReturnFileServiceBase { }
}
