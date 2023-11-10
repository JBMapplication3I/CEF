// <autogenerated>
// <copyright file="SalesInvoiceFileService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice file service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of sales invoice files.</summary>
    /// <seealso cref="SalesInvoiceFileSearchModel"/>
    /// <seealso cref="IReturn{SalesInvoiceFilePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Invoicing/SalesInvoiceFiles", "GET", Priority = 1,
            Summary = "Use to get a list of sales invoice files")]
    public partial class GetSalesInvoiceFiles : SalesInvoiceFileSearchModel, IReturn<SalesInvoiceFilePagedResults> { }

    /// <summary>A ServiceStack Route to get sales invoice files for connect.</summary>
    /// <seealso cref="SalesInvoiceFileSearchModel"/>
    /// <seealso cref="IReturn{List{SalesInvoiceFileModel}}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFilesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all sales invoice files")]
    public partial class GetSalesInvoiceFilesForConnect : SalesInvoiceFileSearchModel, IReturn<List<SalesInvoiceFileModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all sales invoice files.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFilesDigest", "GET",
            Summary = "Use to get a hash representing each sales invoice files")]
    public partial class GetSalesInvoiceFilesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get sales invoice file.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SalesInvoiceFileModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Invoicing/SalesInvoiceFile/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales invoice file")]
    public partial class GetSalesInvoiceFileByID : ImplementsIDBase, IReturn<SalesInvoiceFileModel> { }

    /// <summary>A ServiceStack Route to get sales invoice file.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{SalesInvoiceFileModel}"/>
    [PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific sales invoice file by the custom key")]
    public partial class GetSalesInvoiceFileByKey : ImplementsKeyBase, IReturn<SalesInvoiceFileModel> { }

    /// <summary>A ServiceStack Route to get sales invoice file.</summary>
    /// <seealso cref="IReturn{SalesInvoiceFileModel}"/>
    [PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Name", "GET", Priority = 1,
            Summary = "Use to get a specific sales invoice file by the name")]
    public partial class GetSalesInvoiceFileByName : ImplementsNameBase, IReturn<SalesInvoiceFileModel> { }

    /// <summary>A ServiceStack Route to get sales invoice file.</summary>
    /// <seealso cref="IReturn{SalesInvoiceFileModel}"/>
    [PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/SeoUrl", "GET", Priority = 1,
            Summary = "Use to get a specific sales invoice file by the SEO URL")]
    public partial class GetSalesInvoiceFileBySeoUrl : ImplementsSeoUrlBase, IReturn<SalesInvoiceFileModel> { }

    /// <summary>A ServiceStack Route to check sales invoice file exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesInvoiceFileExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales invoice file exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesInvoiceFileExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales invoice file exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesInvoiceFileExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales invoice file exists by SEO URL.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Exists/SeoUrl", "GET", Priority = 1,
            Summary = "Check if this SEO URL exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesInvoiceFileExistsBySeoUrl : ImplementsSeoUrlBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create sales invoice file.</summary>
    /// <seealso cref="SalesInvoiceFileModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Create", "POST", Priority = 1,
            Summary = "Use to create a new sales invoice file.")]
    public partial class CreateSalesInvoiceFile : SalesInvoiceFileModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert sales invoice file.</summary>
    /// <seealso cref="SalesInvoiceFileModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing sales invoice file (as needed).")]
    public partial class UpsertSalesInvoiceFile : SalesInvoiceFileModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update sales invoice file.</summary>
    /// <seealso cref="SalesInvoiceFileModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing sales invoice file.")]
    public partial class UpdateSalesInvoiceFile : SalesInvoiceFileModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate sales invoice file.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales invoice file from the system [Soft-Delete]")]
    public partial class DeactivateSalesInvoiceFileByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate sales invoice file by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.Deactivate"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales invoice file from the system [Soft-Delete]")]
    public partial class DeactivateSalesInvoiceFileByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales invoice file.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales invoice file from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesInvoiceFileByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales invoice file by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.Reactivate"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales invoice file from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesInvoiceFileByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales invoice file.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific sales invoice file from the system [Hard-Delete]")]
    public partial class DeleteSalesInvoiceFileByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales invoice file by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceFile.Delete"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceFile/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific sales invoice file from the system [Hard-Delete]")]
    public partial class DeleteSalesInvoiceFileByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear sales invoice file cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Invoicing/SalesInvoiceFile/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all sales invoice file calls.")]
    public class ClearSalesInvoiceFileCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SalesInvoiceFileServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSalesInvoiceFiles"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceFiles request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ISalesInvoiceFileModel, SalesInvoiceFileModel, ISalesInvoiceFileSearchModel, SalesInvoiceFilePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesInvoiceFiles)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoiceFilesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSalesInvoiceFilesForConnect request)
        {
            return await Workflows.SalesInvoiceFiles.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoiceFilesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceFilesDigest request)
        {
            return await Workflows.SalesInvoiceFiles.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSalesInvoiceFileByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceFileByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.SalesInvoiceFiles, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoiceFileByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceFileByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.SalesInvoiceFiles, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoiceFileByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceFileByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.SalesInvoiceFiles, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoiceFileBySeoUrl"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceFileBySeoUrl request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultBySeoUrlSingleAsync(request, Workflows.SalesInvoiceFiles, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSalesInvoiceFileExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesInvoiceFileExistsByID request)
        {
            return await Workflows.SalesInvoiceFiles.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesInvoiceFileExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesInvoiceFileExistsByKey request)
        {
            return await Workflows.SalesInvoiceFiles.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesInvoiceFileExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesInvoiceFileExistsByName request)
        {
            return await Workflows.SalesInvoiceFiles.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesInvoiceFileExistsBySeoUrl"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesInvoiceFileExistsBySeoUrl request)
        {
            return await Workflows.SalesInvoiceFiles.CheckExistsBySeoUrlAsync(request.SeoUrl, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSalesInvoiceFile"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSalesInvoiceFile request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceFileDataAsync,
                    () => Workflows.SalesInvoiceFiles.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSalesInvoiceFile"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSalesInvoiceFile request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceFileDataAsync,
                    () => Workflows.SalesInvoiceFiles.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSalesInvoiceFile"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSalesInvoiceFile request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceFileDataAsync,
                    () => Workflows.SalesInvoiceFiles.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSalesInvoiceFileByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesInvoiceFileByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceFileDataAsync,
                    () => Workflows.SalesInvoiceFiles.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSalesInvoiceFileByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesInvoiceFileByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceFileDataAsync,
                    () => Workflows.SalesInvoiceFiles.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSalesInvoiceFileByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesInvoiceFileByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceFileDataAsync,
                    () => Workflows.SalesInvoiceFiles.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSalesInvoiceFileByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesInvoiceFileByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceFileDataAsync,
                    () => Workflows.SalesInvoiceFiles.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSalesInvoiceFileByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesInvoiceFileByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceFileDataAsync,
                    () => Workflows.SalesInvoiceFiles.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSalesInvoiceFileByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesInvoiceFileByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceFileDataAsync,
                    () => Workflows.SalesInvoiceFiles.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSalesInvoiceFileCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSalesInvoiceFileCache request)
        {
            await ClearCachedSalesInvoiceFileDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSalesInvoiceFileDataAsync()
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
                    UrnId.Create<GetSalesInvoiceFiles>(string.Empty),
                    UrnId.Create<GetSalesInvoiceFileByID>(string.Empty),
                    UrnId.Create<GetSalesInvoiceFileByKey>(string.Empty),
                    UrnId.Create<GetSalesInvoiceFileByName>(string.Empty),
                    UrnId.Create<GetSalesInvoiceFileBySeoUrl>(string.Empty),
                    UrnId.Create<CheckSalesInvoiceFileExistsByID>(string.Empty),
                    UrnId.Create<CheckSalesInvoiceFileExistsByKey>(string.Empty),
                    UrnId.Create<CheckSalesInvoiceFileExistsByName>(string.Empty),
                    UrnId.Create<CheckSalesInvoiceFileExistsBySeoUrl>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SalesInvoiceFileService : SalesInvoiceFileServiceBase { }
}
