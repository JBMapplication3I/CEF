// <autogenerated>
// <copyright file="SalesInvoiceService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of sales invoices.</summary>
    /// <seealso cref="SalesInvoiceSearchModel"/>
    /// <seealso cref="IReturn{SalesInvoicePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Invoicing/SalesInvoices", "GET", Priority = 1,
            Summary = "Use to get a list of sales invoices")]
    public partial class GetSalesInvoices : SalesInvoiceSearchModel, IReturn<SalesInvoicePagedResults> { }

    /// <summary>A ServiceStack Route to get sales invoices for connect.</summary>
    /// <seealso cref="SalesInvoiceSearchModel"/>
    /// <seealso cref="IReturn{List{SalesInvoiceModel}}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoicesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all sales invoices")]
    public partial class GetSalesInvoicesForConnect : SalesInvoiceSearchModel, IReturn<List<SalesInvoiceModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all sales invoices.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoicesDigest", "GET",
            Summary = "Use to get a hash representing each sales invoices")]
    public partial class GetSalesInvoicesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get sales invoice.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SalesInvoiceModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Invoicing/SalesInvoice/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales invoice")]
    public partial class GetSalesInvoiceByID : ImplementsIDBase, IReturn<SalesInvoiceModel> { }

    /// <summary>A ServiceStack Route to get sales invoice.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{SalesInvoiceModel}"/>
    [PublicAPI,
        Route("/Invoicing/SalesInvoice/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific sales invoice by the custom key")]
    public partial class GetSalesInvoiceByKey : ImplementsKeyBase, IReturn<SalesInvoiceModel> { }

    /// <summary>A ServiceStack Route to check sales invoice exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoice/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesInvoiceExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales invoice exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoice/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesInvoiceExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create sales invoice.</summary>
    /// <seealso cref="SalesInvoiceModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoice/Create", "POST", Priority = 1,
            Summary = "Use to create a new sales invoice.")]
    public partial class CreateSalesInvoice : SalesInvoiceModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert sales invoice.</summary>
    /// <seealso cref="SalesInvoiceModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Invoicing/SalesInvoice/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing sales invoice (as needed).")]
    public partial class UpsertSalesInvoice : SalesInvoiceModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update sales invoice.</summary>
    /// <seealso cref="SalesInvoiceModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoice/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing sales invoice.")]
    public partial class UpdateSalesInvoice : SalesInvoiceModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate sales invoice.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoice/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales invoice from the system [Soft-Delete]")]
    public partial class DeactivateSalesInvoiceByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate sales invoice by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.Deactivate"),
        PublicAPI,
        Route("/Invoicing/SalesInvoice/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales invoice from the system [Soft-Delete]")]
    public partial class DeactivateSalesInvoiceByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales invoice.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoice/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales invoice from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesInvoiceByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales invoice by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.Reactivate"),
        PublicAPI,
        Route("/Invoicing/SalesInvoice/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales invoice from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesInvoiceByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales invoice.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoice/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific sales invoice from the system [Hard-Delete]")]
    public partial class DeleteSalesInvoiceByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales invoice by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoice.Delete"),
        PublicAPI,
        Route("/Invoicing/SalesInvoice/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific sales invoice from the system [Hard-Delete]")]
    public partial class DeleteSalesInvoiceByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear sales invoice cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Invoicing/SalesInvoice/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all sales invoice calls.")]
    public class ClearSalesInvoiceCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SalesInvoiceServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSalesInvoices"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoices request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ISalesInvoiceModel, SalesInvoiceModel, ISalesInvoiceSearchModel, SalesInvoicePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesInvoices)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoicesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSalesInvoicesForConnect request)
        {
            return await Workflows.SalesInvoices.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoicesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoicesDigest request)
        {
            return await Workflows.SalesInvoices.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSalesInvoiceByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.SalesInvoices, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoiceByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.SalesInvoices, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSalesInvoiceExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesInvoiceExistsByID request)
        {
            return await Workflows.SalesInvoices.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesInvoiceExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesInvoiceExistsByKey request)
        {
            return await Workflows.SalesInvoices.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSalesInvoice"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSalesInvoice request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceDataAsync,
                    () => Workflows.SalesInvoices.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSalesInvoice"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSalesInvoice request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceDataAsync,
                    () => Workflows.SalesInvoices.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSalesInvoice"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSalesInvoice request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceDataAsync,
                    () => Workflows.SalesInvoices.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSalesInvoiceByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesInvoiceByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceDataAsync,
                    () => Workflows.SalesInvoices.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSalesInvoiceByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesInvoiceByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceDataAsync,
                    () => Workflows.SalesInvoices.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSalesInvoiceByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesInvoiceByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceDataAsync,
                    () => Workflows.SalesInvoices.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSalesInvoiceByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesInvoiceByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceDataAsync,
                    () => Workflows.SalesInvoices.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSalesInvoiceByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesInvoiceByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceDataAsync,
                    () => Workflows.SalesInvoices.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSalesInvoiceByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesInvoiceByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceDataAsync,
                    () => Workflows.SalesInvoices.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSalesInvoiceCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSalesInvoiceCache request)
        {
            await ClearCachedSalesInvoiceDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSalesInvoiceDataAsync()
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
                    UrnId.Create<GetSalesInvoices>(string.Empty),
                    UrnId.Create<GetSalesInvoiceByID>(string.Empty),
                    UrnId.Create<GetSalesInvoiceByKey>(string.Empty),
                    UrnId.Create<CheckSalesInvoiceExistsByID>(string.Empty),
                    UrnId.Create<CheckSalesInvoiceExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SalesInvoiceService : SalesInvoiceServiceBase { }
}
