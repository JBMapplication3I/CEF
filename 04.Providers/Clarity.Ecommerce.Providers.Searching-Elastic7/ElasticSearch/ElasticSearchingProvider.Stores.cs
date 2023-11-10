// <copyright file="ElasticSearchingProvider.Stores.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
#if STORES
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Indexer;
    using Interfaces.Models;
    using Interfaces.Providers.Searching;
    using MoreLinq;
    using Nest;
    using Newtonsoft.Json;
    using Utilities;
    using Web.Search;

    public partial class ElasticSearchingProvider
    {
        /// <summary>The map store suggest model.</summary>
        private static readonly Func<SuggestOption<IndexableStoreModel>, ISearchSuggestResult> MapStoreSuggestModel =
            suggest => new StoreSuggestOption
            {
                ID = suggest.Source.ID,
                Name = suggest.Source.Name,
                CustomKey = suggest.Source.CustomKey,
                SeoUrl = suggest.Source.SeoUrl,
                Score = suggest.Score,
            };

        /// <summary>The store suggest module.</summary>
        private static StoreSuggestModule storeSuggestModule;

        /// <summary>Gets the store suggest module.</summary>
        /// <value>The store suggest module.</value>
        private static StoreSuggestModule StoreSuggestModule => storeSuggestModule
            ??= new(StoreSuggestFields, MapStoreSuggestModel);

        /// <summary>Stores suggest fields.</summary>
        /// <typeparam name="TIndexModel">Type of the index model.</typeparam>
        /// <param name="f">The FieldsDescriptor{TIndexModel} to process.</param>
        /// <returns>An IPromise{Fields}.</returns>
        private static IPromise<Fields> StoreSuggestFields<TIndexModel>(FieldsDescriptor<TIndexModel> f)
            where TIndexModel : IndexableStoreModel
        {
            return f.Field(ff => ff.ID)
                    .Field(ff => ff.Name)
                    .Field(ff => ff.CustomKey)
                    .Field(ff => ff.SuggestedByName)
                    .Field(ff => ff.SuggestedByCustomKey);
        }

        /// <inheritdoc/>
        public async Task<List<StoreSuggestOption>> GetStoreSuggestionsFromProviderAsync(
            IStoreCatalogSearchForm form,
            string contextProfileName)
        {
            return (await RegistryLoaderWrapper.GetSearchingProvider(contextProfileName)
                .SuggestionsAsync<StoreCatalogSearchForm, StoreSuggestOption>(
                    (StoreCatalogSearchForm)form,
                    CEFConfigDictionary.SearchingStoreIndex,
                    contextProfileName)
                .ConfigureAwait(false))
                .Cast<StoreSuggestOption>()
                .ToList();
        }

        /// <inheritdoc/>
        public Task<StoreSearchViewModel> GetStoreSearchResultsFromProviderAsync(
            IStoreCatalogSearchForm form,
            string contextProfileName)
        {
            return RegistryLoaderWrapper.GetSearchingProvider(contextProfileName)
                .QueryAsync<StoreSearchViewModel, StoreCatalogSearchForm, IndexableStoreModel>(
                    (StoreCatalogSearchForm)form,
                    CEFConfigDictionary.SearchingStoreIndex,
                    contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<StoreSearchViewModel> GetStoreSearchResultsFromProviderAndMapEachWithAdditionalDataAsync(
            IStoreCatalogSearchForm form,
            bool includeStoreModelMaps,
            string contextProfileName)
        {
            var results = await GetStoreSearchResultsFromProviderAsync(form, contextProfileName).ConfigureAwait(false);
            if (results.Documents == null)
            {
                return results;
            }
            var storeIDsFound = results.Documents.Select(x => x.ID).ToArray();
            if (storeIDsFound.Length == 0)
            {
                return results;
            }
            // ReSharper disable once InvertIf
            if (includeStoreModelMaps)
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                results.WithMaps = context.Stores
                    .AsNoTracking()
                    .FilterByIDs(storeIDsFound)
                    .SelectListStoreAndMapToStoreModel()
                    .ToList();
            }
            return results;
        }
    }
#endif
}
