// <autogenerated>
// <copyright file="SalesReturnReasonService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return reason service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of sales return reasons.</summary>
    /// <seealso cref="SalesReturnReasonSearchModel"/>
    /// <seealso cref="IReturn{SalesReturnReasonPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnReasons", "GET", Priority = 1,
            Summary = "Use to get a list of sales return reasons")]
    public partial class GetSalesReturnReasons : SalesReturnReasonSearchModel, IReturn<SalesReturnReasonPagedResults> { }

    /// <summary>A ServiceStack Route to get sales return reasons for connect.</summary>
    /// <seealso cref="SalesReturnReasonSearchModel"/>
    /// <seealso cref="IReturn{List{SalesReturnReasonModel}}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.View"),
        PublicAPI,
        Route("/Returning/SalesReturnReasonsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all sales return reasons")]
    public partial class GetSalesReturnReasonsForConnect : SalesReturnReasonSearchModel, IReturn<List<SalesReturnReasonModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all sales return reasons.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.View"),
        PublicAPI,
        Route("/Returning/SalesReturnReasonsDigest", "GET",
            Summary = "Use to get a hash representing each sales return reasons")]
    public partial class GetSalesReturnReasonsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get sales return reason.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SalesReturnReasonModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnReason/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales return reason")]
    public partial class GetSalesReturnReasonByID : ImplementsIDBase, IReturn<SalesReturnReasonModel> { }

    /// <summary>A ServiceStack Route to get sales return reason.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{SalesReturnReasonModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnReason/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific sales return reason by the custom key")]
    public partial class GetSalesReturnReasonByKey : ImplementsKeyBase, IReturn<SalesReturnReasonModel> { }

    /// <summary>A ServiceStack Route to get sales return reason.</summary>
    /// <seealso cref="IReturn{SalesReturnReasonModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnReason/Name", "GET", Priority = 1,
            Summary = "Use to get a specific sales return reason by the name")]
    public partial class GetSalesReturnReasonByName : ImplementsNameBase, IReturn<SalesReturnReasonModel> { }

    /// <summary>A ServiceStack Route to get sales return reason.</summary>
    /// <seealso cref="IReturn{SalesReturnReasonModel}"/>
    [PublicAPI,
        Route("/Returning/SalesReturnReason/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific sales return reason by the name")]
    public partial class GetSalesReturnReasonByDisplayName : ImplementsDisplayNameBase, IReturn<SalesReturnReasonModel> { }

    /// <summary>A ServiceStack Route to check sales return reason exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.View"),
        PublicAPI,
        Route("/Returning/SalesReturnReason/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnReasonExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return reason exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.View"),
        PublicAPI,
        Route("/Returning/SalesReturnReason/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnReasonExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return reason exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.View"),
        PublicAPI,
        Route("/Returning/SalesReturnReason/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnReasonExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales return reason exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.View"),
        PublicAPI,
        Route("/Returning/SalesReturnReason/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesReturnReasonExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create sales return reason.</summary>
    /// <seealso cref="SalesReturnReasonModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnReason/Create", "POST", Priority = 1,
            Summary = "Use to create a new sales return reason.")]
    public partial class CreateSalesReturnReason : SalesReturnReasonModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert sales return reason.</summary>
    /// <seealso cref="SalesReturnReasonModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Returning/SalesReturnReason/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing sales return reason (as needed).")]
    public partial class UpsertSalesReturnReason : SalesReturnReasonModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update sales return reason.</summary>
    /// <seealso cref="SalesReturnReasonModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnReason/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing sales return reason.")]
    public partial class UpdateSalesReturnReason : SalesReturnReasonModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate sales return reason.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnReason/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales return reason from the system [Soft-Delete]")]
    public partial class DeactivateSalesReturnReasonByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate sales return reason by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.Deactivate"),
        PublicAPI,
        Route("/Returning/SalesReturnReason/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales return reason from the system [Soft-Delete]")]
    public partial class DeactivateSalesReturnReasonByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales return reason.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnReason/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales return reason from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesReturnReasonByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales return reason by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.Reactivate"),
        PublicAPI,
        Route("/Returning/SalesReturnReason/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales return reason from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesReturnReasonByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales return reason.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Returning/SalesReturnReason/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific sales return reason from the system [Hard-Delete]")]
    public partial class DeleteSalesReturnReasonByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales return reason by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Returning.SalesReturnReason.Delete"),
        PublicAPI,
        Route("/Returning/SalesReturnReason/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific sales return reason from the system [Hard-Delete]")]
    public partial class DeleteSalesReturnReasonByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear sales return reason cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Returning/SalesReturnReason/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all sales return reason calls.")]
    public class ClearSalesReturnReasonCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SalesReturnReasonServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSalesReturnReasons"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnReasons request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ISalesReturnReasonModel, SalesReturnReasonModel, ISalesReturnReasonSearchModel, SalesReturnReasonPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesReturnReasons)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnReasonsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSalesReturnReasonsForConnect request)
        {
            return await Workflows.SalesReturnReasons.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnReasonsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnReasonsDigest request)
        {
            return await Workflows.SalesReturnReasons.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSalesReturnReasonByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnReasonByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.SalesReturnReasons, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnReasonByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnReasonByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.SalesReturnReasons, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnReasonByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnReasonByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.SalesReturnReasons, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesReturnReasonByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesReturnReasonByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.SalesReturnReasons, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSalesReturnReasonExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnReasonExistsByID request)
        {
            return await Workflows.SalesReturnReasons.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnReasonExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnReasonExistsByKey request)
        {
            return await Workflows.SalesReturnReasons.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnReasonExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnReasonExistsByName request)
        {
            return await Workflows.SalesReturnReasons.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesReturnReasonExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesReturnReasonExistsByDisplayName request)
        {
            return await Workflows.SalesReturnReasons.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSalesReturnReason"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSalesReturnReason request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnReasonDataAsync,
                    () => Workflows.SalesReturnReasons.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSalesReturnReason"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSalesReturnReason request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnReasonDataAsync,
                    () => Workflows.SalesReturnReasons.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSalesReturnReason"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSalesReturnReason request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnReasonDataAsync,
                    () => Workflows.SalesReturnReasons.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSalesReturnReasonByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesReturnReasonByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnReasonDataAsync,
                    () => Workflows.SalesReturnReasons.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSalesReturnReasonByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesReturnReasonByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnReasonDataAsync,
                    () => Workflows.SalesReturnReasons.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSalesReturnReasonByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesReturnReasonByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnReasonDataAsync,
                    () => Workflows.SalesReturnReasons.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSalesReturnReasonByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesReturnReasonByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnReasonDataAsync,
                    () => Workflows.SalesReturnReasons.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSalesReturnReasonByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesReturnReasonByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnReasonDataAsync,
                    () => Workflows.SalesReturnReasons.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSalesReturnReasonByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesReturnReasonByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesReturnReasonDataAsync,
                    () => Workflows.SalesReturnReasons.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSalesReturnReasonCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSalesReturnReasonCache request)
        {
            await ClearCachedSalesReturnReasonDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSalesReturnReasonDataAsync()
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
                    UrnId.Create<GetSalesReturnReasons>(string.Empty),
                    UrnId.Create<GetSalesReturnReasonByID>(string.Empty),
                    UrnId.Create<GetSalesReturnReasonByKey>(string.Empty),
                    UrnId.Create<GetSalesReturnReasonByName>(string.Empty),
                    UrnId.Create<CheckSalesReturnReasonExistsByID>(string.Empty),
                    UrnId.Create<CheckSalesReturnReasonExistsByKey>(string.Empty),
                    UrnId.Create<CheckSalesReturnReasonExistsByName>(string.Empty),
                    UrnId.Create<CheckSalesReturnReasonExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SalesReturnReasonService : SalesReturnReasonServiceBase { }
}
