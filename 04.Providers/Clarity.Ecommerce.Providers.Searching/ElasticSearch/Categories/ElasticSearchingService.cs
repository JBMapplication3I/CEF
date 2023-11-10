// <copyright file="ElasticSearchingService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic search service. categories class</summary>
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

    /// <summary>An index categories.</summary>
    /// <seealso cref="IReturn{Boolean}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Providers/Searching/Categories/Index", "GET",
            Summary = "Calls the searching provider and re-indexes the categories in the database")]
    public class IndexCategories : IReturn<CEFActionResponse>
    {
    }

    /// <summary>A suggest category catalog with provider.</summary>
    /// <seealso cref="CategoryCatalogSearchForm"/>
    /// <seealso cref="IReturn{List_CategorySuggestOption}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Categories/Suggest", "GET",
            Summary = "Search the category catalog. Returns a range of data")]
    public class SuggestCategoryCatalogWithProvider
        : CategoryCatalogSearchForm,
            IReturn<List<CategorySuggestResult>>
    {
    }

    /// <summary>A search category catalog with provider.</summary>
    /// <seealso cref="CategoryCatalogSearchForm"/>
    /// <seealso cref="IReturn{CategorySearchViewModel}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Categories/Query", "GET",
            Summary = "Search the category catalog. Returns a range of data")]
    public class SearchCategoryCatalogWithProvider
        : CategoryCatalogSearchForm,
            IReturn<CategorySearchViewModel>
    {
    }

    public partial class ElasticSearchingService
    {
        /// <summary>A GET handler for the <seealso cref="IndexCategories" /> endpoint.</summary>
        /// <param name="_">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(IndexCategories _)
        {
            try
            {
                await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                    .IndexAsync(
                        contextProfileName: ServiceContextProfileName,
                        index: ElasticSearchingProviderConfig.SearchingCategoryIndex,
                        ct: default)
                    .ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync($"{nameof(IndexCategories)}.Error", ex.Message, ex, null).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>A GET handler for the <seealso cref="SuggestCategoryCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(SuggestCategoryCatalogWithProvider request)
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

        /// <summary>A GET handler for the <seealso cref="SearchCategoryCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object?> Get(SearchCategoryCatalogWithProvider request)
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
            //return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
            //        request,
            //        () => RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
            //            .GetAllSearchResultsAsViewModelLastModifiedAsync(
            //                form: request,
            //                contextProfileName: ServiceContextProfileName,
            //                roles: pricingFactoryContext.UserRoles),
            //        async () =>
            //        {
            //            var viewModel = await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
            //                .GetSearchResultsFromProviderAsync<CategorySearchViewModel, CategoryCatalogSearchForm, CategoryIndexableModel>(
            //                    form: request,
            //                    contextProfileName: ServiceContextProfileName,
            //                    roles: pricingFactoryContext.UserRoles)
            //                .ConfigureAwait(false);
            //            viewModel.Form!.RolesAny = null;
            //            return viewModel;
            //        },
            //        varyByUser: true)
            //    .ConfigureAwait(false);
            var viewModel = await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                            .GetSearchResultsFromProviderAsync<CategorySearchViewModel, CategoryCatalogSearchForm, CategoryIndexableModel>(
                                form: request,
                                contextProfileName: ServiceContextProfileName,
                                roles: pricingFactoryContext.UserRoles)
                            .ConfigureAwait(false);
            viewModel.Form!.RolesAny = null;
            viewModel.Documents = null;
            return viewModel;
        }
    }
}
