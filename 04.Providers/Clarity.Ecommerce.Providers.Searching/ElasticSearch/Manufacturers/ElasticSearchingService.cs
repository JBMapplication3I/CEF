// <copyright file="ElasticSearchingService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic search service. manufacturers class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Providers.Searching;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>An index manufacturers.</summary>
    /// <seealso cref="IReturn{Boolean}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Providers/Searching/Manufacturers/Index", "GET",
            Summary = "Calls the searching provider and re-indexes the manufacturers in the database")]
    public class IndexManufacturers : IReturn<CEFActionResponse>
    {
    }

    /// <summary>A suggest manufacturer catalog with provider.</summary>
    /// <seealso cref="ManufacturerCatalogSearchForm"/>
    /// <seealso cref="IReturn{List_ManufacturerSuggestOption}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Manufacturers/Suggest", "GET",
            Summary = "Search the manufacturer catalog. Returns a range of data")]
    public class SuggestManufacturerCatalogWithProvider
        : ManufacturerCatalogSearchForm,
            IReturn<List<ManufacturerSuggestResult>>
    {
    }

    /// <summary>A search manufacturer catalog with provider.</summary>
    /// <seealso cref="ManufacturerCatalogSearchForm"/>
    /// <seealso cref="IReturn{ManufacturerSearchViewModel}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Manufacturers/Query", "GET",
            Summary = "Search the manufacturer catalog. Returns a range of data")]
    public class SearchManufacturerCatalogWithProvider
        : ManufacturerCatalogSearchForm,
            IReturn<ManufacturerSearchViewModel>
    {
    }

    public partial class ElasticSearchingService
    {
        /// <summary>A GET handler for the <seealso cref="IndexManufacturers" /> endpoint.</summary>
        /// <param name="_">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(IndexManufacturers _)
        {
            try
            {
                await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                    .IndexAsync(
                        contextProfileName: ServiceContextProfileName,
                        index: ElasticSearchingProviderConfig.SearchingManufacturerIndex,
                        ct: default)
                    .ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync($"{nameof(IndexManufacturers)}.Error", ex.Message, ex, null).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>A GET handler for the <seealso cref="SuggestManufacturerCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(SuggestManufacturerCatalogWithProvider request)
        {
            if (GetSession()?.IsAuthenticated == true)
            {
                request.RolesAny = GetSession().Roles.ToArray();
            }
            return await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                .GetAllSuggestionsFromProviderAsync(
                    form: request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>A GET handler for the <seealso cref="SearchManufacturerCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object?> Get(SearchManufacturerCatalogWithProvider request)
        {
            var pricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            if (Contract.CheckValidID(request.BrandID))
            {
                pricingFactoryContext.BrandID = request.BrandID;
            }
            if (Contract.CheckValidID(request.FranchiseID))
            {
                pricingFactoryContext.FranchiseID = request.FranchiseID;
            }
            if (Contract.CheckValidID(request.StoreID))
            {
                pricingFactoryContext.StoreID = request.StoreID;
            }
            if (GetSession()?.IsAuthenticated == true)
            {
                request.RolesAny = GetSession().Roles.ToArray();
            }
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                        .GetAllSearchResultsAsViewModelLastModifiedAsync(
                            form: request,
                            contextProfileName: ServiceContextProfileName,
                            roles: pricingFactoryContext.UserRoles),
                    async () =>
                    {
                        var viewModel = await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                            .GetSearchResultsFromProviderAsync<ManufacturerSearchViewModel, ManufacturerCatalogSearchForm, ManufacturerIndexableModel>(
                                form: request,
                                contextProfileName: ServiceContextProfileName,
                                roles: pricingFactoryContext.UserRoles)
                            .ConfigureAwait(false);
                        viewModel.Form!.RolesAny = null;
                        return viewModel;
                    },
                    varyByUser: true)
                .ConfigureAwait(false);
        }
    }
}
