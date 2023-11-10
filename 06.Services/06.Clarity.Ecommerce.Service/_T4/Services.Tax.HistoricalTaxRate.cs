// <autogenerated>
// <copyright file="HistoricalTaxRateService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the historical tax rate service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of historical tax rates.</summary>
    /// <seealso cref="HistoricalTaxRateSearchModel"/>
    /// <seealso cref="IReturn{HistoricalTaxRatePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Tax/HistoricalTaxRates", "GET", Priority = 1,
            Summary = "Use to get a list of historical tax rates")]
    public partial class GetHistoricalTaxRates : HistoricalTaxRateSearchModel, IReturn<HistoricalTaxRatePagedResults> { }

    /// <summary>A ServiceStack Route to get historical tax rates for connect.</summary>
    /// <seealso cref="HistoricalTaxRateSearchModel"/>
    /// <seealso cref="IReturn{List{HistoricalTaxRateModel}}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.View"),
        PublicAPI,
        Route("/Tax/HistoricalTaxRatesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all historical tax rates")]
    public partial class GetHistoricalTaxRatesForConnect : HistoricalTaxRateSearchModel, IReturn<List<HistoricalTaxRateModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all historical tax rates.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.View"),
        PublicAPI,
        Route("/Tax/HistoricalTaxRatesDigest", "GET",
            Summary = "Use to get a hash representing each historical tax rates")]
    public partial class GetHistoricalTaxRatesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get historical tax rate.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{HistoricalTaxRateModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Tax/HistoricalTaxRate/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific historical tax rate")]
    public partial class GetHistoricalTaxRateByID : ImplementsIDBase, IReturn<HistoricalTaxRateModel> { }

    /// <summary>A ServiceStack Route to get historical tax rate.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{HistoricalTaxRateModel}"/>
    [PublicAPI,
        Route("/Tax/HistoricalTaxRate/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific historical tax rate by the custom key")]
    public partial class GetHistoricalTaxRateByKey : ImplementsKeyBase, IReturn<HistoricalTaxRateModel> { }

    /// <summary>A ServiceStack Route to check historical tax rate exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.View"),
        PublicAPI,
        Route("/Tax/HistoricalTaxRate/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckHistoricalTaxRateExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check historical tax rate exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.View"),
        PublicAPI,
        Route("/Tax/HistoricalTaxRate/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckHistoricalTaxRateExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create historical tax rate.</summary>
    /// <seealso cref="HistoricalTaxRateModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Tax/HistoricalTaxRate/Create", "POST", Priority = 1,
            Summary = "Use to create a new historical tax rate.")]
    public partial class CreateHistoricalTaxRate : HistoricalTaxRateModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert historical tax rate.</summary>
    /// <seealso cref="HistoricalTaxRateModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Tax/HistoricalTaxRate/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing historical tax rate (as needed).")]
    public partial class UpsertHistoricalTaxRate : HistoricalTaxRateModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update historical tax rate.</summary>
    /// <seealso cref="HistoricalTaxRateModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Tax/HistoricalTaxRate/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing historical tax rate.")]
    public partial class UpdateHistoricalTaxRate : HistoricalTaxRateModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate historical tax rate.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Tax/HistoricalTaxRate/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific historical tax rate from the system [Soft-Delete]")]
    public partial class DeactivateHistoricalTaxRateByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate historical tax rate by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.Deactivate"),
        PublicAPI,
        Route("/Tax/HistoricalTaxRate/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific historical tax rate from the system [Soft-Delete]")]
    public partial class DeactivateHistoricalTaxRateByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate historical tax rate.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Tax/HistoricalTaxRate/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific historical tax rate from the system [Restore from Soft-Delete]")]
    public partial class ReactivateHistoricalTaxRateByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate historical tax rate by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.Reactivate"),
        PublicAPI,
        Route("/Tax/HistoricalTaxRate/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific historical tax rate from the system [Restore from Soft-Delete]")]
    public partial class ReactivateHistoricalTaxRateByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete historical tax rate.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Tax/HistoricalTaxRate/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific historical tax rate from the system [Hard-Delete]")]
    public partial class DeleteHistoricalTaxRateByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete historical tax rate by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Tax.HistoricalTaxRate.Delete"),
        PublicAPI,
        Route("/Tax/HistoricalTaxRate/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific historical tax rate from the system [Hard-Delete]")]
    public partial class DeleteHistoricalTaxRateByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear historical tax rate cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Tax/HistoricalTaxRate/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all historical tax rate calls.")]
    public class ClearHistoricalTaxRateCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class HistoricalTaxRateServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetHistoricalTaxRates"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetHistoricalTaxRates request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IHistoricalTaxRateModel, HistoricalTaxRateModel, IHistoricalTaxRateSearchModel, HistoricalTaxRatePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.HistoricalTaxRates)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetHistoricalTaxRatesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetHistoricalTaxRatesForConnect request)
        {
            return await Workflows.HistoricalTaxRates.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetHistoricalTaxRatesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetHistoricalTaxRatesDigest request)
        {
            return await Workflows.HistoricalTaxRates.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetHistoricalTaxRateByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetHistoricalTaxRateByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.HistoricalTaxRates, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetHistoricalTaxRateByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetHistoricalTaxRateByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.HistoricalTaxRates, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckHistoricalTaxRateExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckHistoricalTaxRateExistsByID request)
        {
            return await Workflows.HistoricalTaxRates.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckHistoricalTaxRateExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckHistoricalTaxRateExistsByKey request)
        {
            return await Workflows.HistoricalTaxRates.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertHistoricalTaxRate"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertHistoricalTaxRate request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedHistoricalTaxRateDataAsync,
                    () => Workflows.HistoricalTaxRates.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateHistoricalTaxRate"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateHistoricalTaxRate request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedHistoricalTaxRateDataAsync,
                    () => Workflows.HistoricalTaxRates.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateHistoricalTaxRate"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateHistoricalTaxRate request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedHistoricalTaxRateDataAsync,
                    () => Workflows.HistoricalTaxRates.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateHistoricalTaxRateByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateHistoricalTaxRateByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedHistoricalTaxRateDataAsync,
                    () => Workflows.HistoricalTaxRates.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateHistoricalTaxRateByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateHistoricalTaxRateByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedHistoricalTaxRateDataAsync,
                    () => Workflows.HistoricalTaxRates.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateHistoricalTaxRateByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateHistoricalTaxRateByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedHistoricalTaxRateDataAsync,
                    () => Workflows.HistoricalTaxRates.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateHistoricalTaxRateByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateHistoricalTaxRateByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedHistoricalTaxRateDataAsync,
                    () => Workflows.HistoricalTaxRates.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteHistoricalTaxRateByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteHistoricalTaxRateByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedHistoricalTaxRateDataAsync,
                    () => Workflows.HistoricalTaxRates.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteHistoricalTaxRateByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteHistoricalTaxRateByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedHistoricalTaxRateDataAsync,
                    () => Workflows.HistoricalTaxRates.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearHistoricalTaxRateCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearHistoricalTaxRateCache request)
        {
            await ClearCachedHistoricalTaxRateDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedHistoricalTaxRateDataAsync()
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
                    UrnId.Create<GetHistoricalTaxRates>(string.Empty),
                    UrnId.Create<GetHistoricalTaxRateByID>(string.Empty),
                    UrnId.Create<GetHistoricalTaxRateByKey>(string.Empty),
                    UrnId.Create<CheckHistoricalTaxRateExistsByID>(string.Empty),
                    UrnId.Create<CheckHistoricalTaxRateExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class HistoricalTaxRateService : HistoricalTaxRateServiceBase { }
}
