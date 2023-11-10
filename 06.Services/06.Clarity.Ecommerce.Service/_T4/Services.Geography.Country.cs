// <autogenerated>
// <copyright file="CountryService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the country service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of countries.</summary>
    /// <seealso cref="CountrySearchModel"/>
    /// <seealso cref="IReturn{CountryPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Geography/Countries", "GET", Priority = 1,
            Summary = "Use to get a list of countries")]
    public partial class GetCountries : CountrySearchModel, IReturn<CountryPagedResults> { }

    /// <summary>A ServiceStack Route to get countries for connect.</summary>
    /// <seealso cref="CountrySearchModel"/>
    /// <seealso cref="IReturn{List{CountryModel}}"/>
    [Authenticate, RequiredPermission("Geography.Country.View"),
        PublicAPI,
        Route("/Geography/CountriesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all countries")]
    public partial class GetCountriesForConnect : CountrySearchModel, IReturn<List<CountryModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all countries.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Geography.Country.View"),
        PublicAPI,
        Route("/Geography/CountriesDigest", "GET",
            Summary = "Use to get a hash representing each countries")]
    public partial class GetCountriesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get country.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CountryModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Geography/Country/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific country")]
    public partial class GetCountryByID : ImplementsIDBase, IReturn<CountryModel> { }

    /// <summary>A ServiceStack Route to get country.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CountryModel}"/>
    [PublicAPI,
        Route("/Geography/Country/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific country by the custom key")]
    public partial class GetCountryByKey : ImplementsKeyBase, IReturn<CountryModel> { }

    /// <summary>A ServiceStack Route to get country.</summary>
    /// <seealso cref="IReturn{CountryModel}"/>
    [PublicAPI,
        Route("/Geography/Country/Name", "GET", Priority = 1,
            Summary = "Use to get a specific country by the name")]
    public partial class GetCountryByName : ImplementsNameBase, IReturn<CountryModel> { }

    /// <summary>A ServiceStack Route to check country exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Geography.Country.View"),
        PublicAPI,
        Route("/Geography/Country/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckCountryExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check country exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Geography.Country.View"),
        PublicAPI,
        Route("/Geography/Country/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckCountryExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check country exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Geography.Country.View"),
        PublicAPI,
        Route("/Geography/Country/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckCountryExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create country.</summary>
    /// <seealso cref="CountryModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Geography.Country.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Geography/Country/Create", "POST", Priority = 1,
            Summary = "Use to create a new country.")]
    public partial class CreateCountry : CountryModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert country.</summary>
    /// <seealso cref="CountryModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Geography/Country/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing country (as needed).")]
    public partial class UpsertCountry : CountryModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update country.</summary>
    /// <seealso cref="CountryModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Geography.Country.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Geography/Country/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing country.")]
    public partial class UpdateCountry : CountryModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate country.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Geography.Country.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Geography/Country/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific country from the system [Soft-Delete]")]
    public partial class DeactivateCountryByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate country by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Geography.Country.Deactivate"),
        PublicAPI,
        Route("/Geography/Country/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific country from the system [Soft-Delete]")]
    public partial class DeactivateCountryByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate country.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Geography.Country.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Geography/Country/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific country from the system [Restore from Soft-Delete]")]
    public partial class ReactivateCountryByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate country by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Geography.Country.Reactivate"),
        PublicAPI,
        Route("/Geography/Country/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific country from the system [Restore from Soft-Delete]")]
    public partial class ReactivateCountryByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete country.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Geography.Country.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Geography/Country/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific country from the system [Hard-Delete]")]
    public partial class DeleteCountryByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete country by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Geography.Country.Delete"),
        PublicAPI,
        Route("/Geography/Country/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific country from the system [Hard-Delete]")]
    public partial class DeleteCountryByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear country cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Geography/Country/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all country calls.")]
    public class ClearCountryCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class CountryServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetCountries"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCountries request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ICountryModel, CountryModel, ICountrySearchModel, CountryPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.Countries)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCountriesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetCountriesForConnect request)
        {
            return await Workflows.Countries.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCountriesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCountriesDigest request)
        {
            return await Workflows.Countries.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetCountryByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCountryByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.Countries, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCountryByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCountryByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.Countries, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCountryByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCountryByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.Countries, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckCountryExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCountryExistsByID request)
        {
            return await Workflows.Countries.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckCountryExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCountryExistsByKey request)
        {
            return await Workflows.Countries.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckCountryExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCountryExistsByName request)
        {
            return await Workflows.Countries.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertCountry"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertCountry request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCountryDataAsync,
                    () => Workflows.Countries.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateCountry"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateCountry request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCountryDataAsync,
                    () => Workflows.Countries.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateCountry"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateCountry request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCountryDataAsync,
                    () => Workflows.Countries.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateCountryByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateCountryByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCountryDataAsync,
                    () => Workflows.Countries.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateCountryByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateCountryByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCountryDataAsync,
                    () => Workflows.Countries.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateCountryByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateCountryByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCountryDataAsync,
                    () => Workflows.Countries.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateCountryByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateCountryByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCountryDataAsync,
                    () => Workflows.Countries.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteCountryByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteCountryByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCountryDataAsync,
                    () => Workflows.Countries.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteCountryByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteCountryByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCountryDataAsync,
                    () => Workflows.Countries.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearCountryCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearCountryCache request)
        {
            await ClearCachedCountryDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedCountryDataAsync()
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
                    UrnId.Create<GetCountries>(string.Empty),
                    UrnId.Create<GetCountryByID>(string.Empty),
                    UrnId.Create<GetCountryByKey>(string.Empty),
                    UrnId.Create<GetCountryByName>(string.Empty),
                    UrnId.Create<CheckCountryExistsByID>(string.Empty),
                    UrnId.Create<CheckCountryExistsByKey>(string.Empty),
                    UrnId.Create<CheckCountryExistsByName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class CountryService : CountryServiceBase { }
}
