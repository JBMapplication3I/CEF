// <autogenerated>
// <copyright file="CurrencyImageTypeService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the currency image type service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of currency image types.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{CurrencyImageTypePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Currencies/CurrencyImageTypes", "GET", Priority = 1,
            Summary = "Use to get a list of currency image types")]
    public partial class GetCurrencyImageTypes : TypeSearchModel, IReturn<CurrencyImageTypePagedResults> { }

    /// <summary>A ServiceStack Route to get currency image types for connect.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{List{TypeModel}}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.View"),
        PublicAPI,
        Route("/Currencies/CurrencyImageTypesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all currency image types")]
    public partial class GetCurrencyImageTypesForConnect : TypeSearchModel, IReturn<List<TypeModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all currency image types.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.View"),
        PublicAPI,
        Route("/Currencies/CurrencyImageTypesDigest", "GET",
            Summary = "Use to get a hash representing each currency image types")]
    public partial class GetCurrencyImageTypesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get currency image type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Currencies/CurrencyImageType/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific currency image type")]
    public partial class GetCurrencyImageTypeByID : ImplementsIDBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get currency image type.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Currencies/CurrencyImageType/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific currency image type by the custom key")]
    public partial class GetCurrencyImageTypeByKey : ImplementsKeyBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get currency image type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Currencies/CurrencyImageType/Name", "GET", Priority = 1,
            Summary = "Use to get a specific currency image type by the name")]
    public partial class GetCurrencyImageTypeByName : ImplementsNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get currency image type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/Currencies/CurrencyImageType/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific currency image type by the name")]
    public partial class GetCurrencyImageTypeByDisplayName : ImplementsDisplayNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to check currency image type exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.View"),
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckCurrencyImageTypeExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check currency image type exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.View"),
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckCurrencyImageTypeExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check currency image type exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.View"),
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckCurrencyImageTypeExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check currency image type exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.View"),
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckCurrencyImageTypeExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create currency image type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Create", "POST", Priority = 1,
            Summary = "Use to create a new currency image type.")]
    public partial class CreateCurrencyImageType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert currency image type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing currency image type (as needed).")]
    public partial class UpsertCurrencyImageType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update currency image type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing currency image type.")]
    public partial class UpdateCurrencyImageType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate currency image type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific currency image type from the system [Soft-Delete]")]
    public partial class DeactivateCurrencyImageTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate currency image type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.Deactivate"),
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific currency image type from the system [Soft-Delete]")]
    public partial class DeactivateCurrencyImageTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate currency image type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific currency image type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateCurrencyImageTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate currency image type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.Reactivate"),
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific currency image type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateCurrencyImageTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete currency image type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific currency image type from the system [Hard-Delete]")]
    public partial class DeleteCurrencyImageTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete currency image type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Currencies.CurrencyImageType.Delete"),
        PublicAPI,
        Route("/Currencies/CurrencyImageType/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific currency image type from the system [Hard-Delete]")]
    public partial class DeleteCurrencyImageTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear currency image type cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Currencies/CurrencyImageType/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all currency image type calls.")]
    public class ClearCurrencyImageTypeCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class CurrencyImageTypeServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetCurrencyImageTypes"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCurrencyImageTypes request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ITypeModel, TypeModel, ITypeSearchModel, CurrencyImageTypePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.CurrencyImageTypes)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCurrencyImageTypesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetCurrencyImageTypesForConnect request)
        {
            return await Workflows.CurrencyImageTypes.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCurrencyImageTypesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCurrencyImageTypesDigest request)
        {
            return await Workflows.CurrencyImageTypes.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetCurrencyImageTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCurrencyImageTypeByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.CurrencyImageTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCurrencyImageTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCurrencyImageTypeByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.CurrencyImageTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCurrencyImageTypeByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCurrencyImageTypeByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.CurrencyImageTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCurrencyImageTypeByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCurrencyImageTypeByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.CurrencyImageTypes, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckCurrencyImageTypeExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCurrencyImageTypeExistsByID request)
        {
            return await Workflows.CurrencyImageTypes.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckCurrencyImageTypeExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCurrencyImageTypeExistsByKey request)
        {
            return await Workflows.CurrencyImageTypes.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckCurrencyImageTypeExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCurrencyImageTypeExistsByName request)
        {
            return await Workflows.CurrencyImageTypes.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckCurrencyImageTypeExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCurrencyImageTypeExistsByDisplayName request)
        {
            return await Workflows.CurrencyImageTypes.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertCurrencyImageType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertCurrencyImageType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCurrencyImageTypeDataAsync,
                    () => Workflows.CurrencyImageTypes.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateCurrencyImageType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateCurrencyImageType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCurrencyImageTypeDataAsync,
                    () => Workflows.CurrencyImageTypes.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateCurrencyImageType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateCurrencyImageType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCurrencyImageTypeDataAsync,
                    () => Workflows.CurrencyImageTypes.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateCurrencyImageTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateCurrencyImageTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCurrencyImageTypeDataAsync,
                    () => Workflows.CurrencyImageTypes.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateCurrencyImageTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateCurrencyImageTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCurrencyImageTypeDataAsync,
                    () => Workflows.CurrencyImageTypes.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateCurrencyImageTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateCurrencyImageTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCurrencyImageTypeDataAsync,
                    () => Workflows.CurrencyImageTypes.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateCurrencyImageTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateCurrencyImageTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCurrencyImageTypeDataAsync,
                    () => Workflows.CurrencyImageTypes.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteCurrencyImageTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteCurrencyImageTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCurrencyImageTypeDataAsync,
                    () => Workflows.CurrencyImageTypes.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteCurrencyImageTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteCurrencyImageTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCurrencyImageTypeDataAsync,
                    () => Workflows.CurrencyImageTypes.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearCurrencyImageTypeCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearCurrencyImageTypeCache request)
        {
            await ClearCachedCurrencyImageTypeDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedCurrencyImageTypeDataAsync()
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
                    UrnId.Create<GetCurrencyImageTypes>(string.Empty),
                    UrnId.Create<GetCurrencyImageTypeByID>(string.Empty),
                    UrnId.Create<GetCurrencyImageTypeByKey>(string.Empty),
                    UrnId.Create<GetCurrencyImageTypeByName>(string.Empty),
                    UrnId.Create<CheckCurrencyImageTypeExistsByID>(string.Empty),
                    UrnId.Create<CheckCurrencyImageTypeExistsByKey>(string.Empty),
                    UrnId.Create<CheckCurrencyImageTypeExistsByName>(string.Empty),
                    UrnId.Create<CheckCurrencyImageTypeExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class CurrencyImageTypeService : CurrencyImageTypeServiceBase { }
}
