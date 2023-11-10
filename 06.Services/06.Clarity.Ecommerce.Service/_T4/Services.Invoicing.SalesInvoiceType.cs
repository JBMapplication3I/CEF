// <autogenerated>
// <copyright file="SalesInvoiceTypeService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice type service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of sales invoice types.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{SalesInvoiceTypePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Invoicing/SalesInvoiceTypes", "GET", Priority = 1,
            Summary = "Use to get a list of sales invoice types")]
    public partial class GetSalesInvoiceTypes : TypeSearchModel, IReturn<SalesInvoiceTypePagedResults> { }

    /// <summary>A ServiceStack Route to get sales invoice types for connect.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{List{TypeModel}}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceTypesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all sales invoice types")]
    public partial class GetSalesInvoiceTypesForConnect : TypeSearchModel, IReturn<List<TypeModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all sales invoice types.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceTypesDigest", "GET",
            Summary = "Use to get a hash representing each sales invoice types")]
    public partial class GetSalesInvoiceTypesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get sales invoice type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Invoicing/SalesInvoiceType/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales invoice type")]
    public partial class GetSalesInvoiceTypeByID : ImplementsIDBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get sales invoice type.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific sales invoice type by the custom key")]
    public partial class GetSalesInvoiceTypeByKey : ImplementsKeyBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get sales invoice type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Name", "GET", Priority = 1,
            Summary = "Use to get a specific sales invoice type by the name")]
    public partial class GetSalesInvoiceTypeByName : ImplementsNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get sales invoice type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Invoicing/SalesInvoiceType/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific sales invoice type by the name")]
    public partial class GetSalesInvoiceTypeByDisplayName : ImplementsDisplayNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to check sales invoice type exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesInvoiceTypeExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales invoice type exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesInvoiceTypeExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales invoice type exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesInvoiceTypeExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check sales invoice type exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.View"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckSalesInvoiceTypeExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create sales invoice type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Create", "POST", Priority = 1,
            Summary = "Use to create a new sales invoice type.")]
    public partial class CreateSalesInvoiceType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert sales invoice type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing sales invoice type (as needed).")]
    public partial class UpsertSalesInvoiceType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update sales invoice type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing sales invoice type.")]
    public partial class UpdateSalesInvoiceType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate sales invoice type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales invoice type from the system [Soft-Delete]")]
    public partial class DeactivateSalesInvoiceTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate sales invoice type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.Deactivate"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific sales invoice type from the system [Soft-Delete]")]
    public partial class DeactivateSalesInvoiceTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales invoice type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales invoice type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesInvoiceTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate sales invoice type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.Reactivate"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific sales invoice type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSalesInvoiceTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales invoice type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific sales invoice type from the system [Hard-Delete]")]
    public partial class DeleteSalesInvoiceTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete sales invoice type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Invoicing.SalesInvoiceType.Delete"),
        PublicAPI,
        Route("/Invoicing/SalesInvoiceType/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific sales invoice type from the system [Hard-Delete]")]
    public partial class DeleteSalesInvoiceTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear sales invoice type cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Invoicing/SalesInvoiceType/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all sales invoice type calls.")]
    public class ClearSalesInvoiceTypeCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SalesInvoiceTypeServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSalesInvoiceTypes"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceTypes request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ITypeModel, TypeModel, ITypeSearchModel, SalesInvoiceTypePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesInvoiceTypes)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoiceTypesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSalesInvoiceTypesForConnect request)
        {
            return await Workflows.SalesInvoiceTypes.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoiceTypesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceTypesDigest request)
        {
            return await Workflows.SalesInvoiceTypes.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSalesInvoiceTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceTypeByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.SalesInvoiceTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoiceTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceTypeByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.SalesInvoiceTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoiceTypeByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceTypeByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.SalesInvoiceTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSalesInvoiceTypeByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSalesInvoiceTypeByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.SalesInvoiceTypes, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSalesInvoiceTypeExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesInvoiceTypeExistsByID request)
        {
            return await Workflows.SalesInvoiceTypes.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesInvoiceTypeExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesInvoiceTypeExistsByKey request)
        {
            return await Workflows.SalesInvoiceTypes.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesInvoiceTypeExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesInvoiceTypeExistsByName request)
        {
            return await Workflows.SalesInvoiceTypes.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSalesInvoiceTypeExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSalesInvoiceTypeExistsByDisplayName request)
        {
            return await Workflows.SalesInvoiceTypes.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSalesInvoiceType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSalesInvoiceType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceTypeDataAsync,
                    () => Workflows.SalesInvoiceTypes.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSalesInvoiceType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSalesInvoiceType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceTypeDataAsync,
                    () => Workflows.SalesInvoiceTypes.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSalesInvoiceType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSalesInvoiceType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceTypeDataAsync,
                    () => Workflows.SalesInvoiceTypes.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSalesInvoiceTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesInvoiceTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceTypeDataAsync,
                    () => Workflows.SalesInvoiceTypes.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSalesInvoiceTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSalesInvoiceTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceTypeDataAsync,
                    () => Workflows.SalesInvoiceTypes.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSalesInvoiceTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesInvoiceTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceTypeDataAsync,
                    () => Workflows.SalesInvoiceTypes.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSalesInvoiceTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSalesInvoiceTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceTypeDataAsync,
                    () => Workflows.SalesInvoiceTypes.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSalesInvoiceTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesInvoiceTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceTypeDataAsync,
                    () => Workflows.SalesInvoiceTypes.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSalesInvoiceTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSalesInvoiceTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSalesInvoiceTypeDataAsync,
                    () => Workflows.SalesInvoiceTypes.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSalesInvoiceTypeCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSalesInvoiceTypeCache request)
        {
            await ClearCachedSalesInvoiceTypeDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSalesInvoiceTypeDataAsync()
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
                    UrnId.Create<GetSalesInvoiceTypes>(string.Empty),
                    UrnId.Create<GetSalesInvoiceTypeByID>(string.Empty),
                    UrnId.Create<GetSalesInvoiceTypeByKey>(string.Empty),
                    UrnId.Create<GetSalesInvoiceTypeByName>(string.Empty),
                    UrnId.Create<CheckSalesInvoiceTypeExistsByID>(string.Empty),
                    UrnId.Create<CheckSalesInvoiceTypeExistsByKey>(string.Empty),
                    UrnId.Create<CheckSalesInvoiceTypeExistsByName>(string.Empty),
                    UrnId.Create<CheckSalesInvoiceTypeExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SalesInvoiceTypeService : SalesInvoiceTypeServiceBase { }
}
