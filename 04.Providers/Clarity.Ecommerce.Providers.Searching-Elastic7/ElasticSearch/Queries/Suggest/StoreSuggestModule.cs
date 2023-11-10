// <copyright file="StoreSuggestModule.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store suggest module class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Web.Search
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Interfaces.Providers.Searching;
    using Nest;

    /// <summary>A product suggest module.</summary>
    /// <seealso cref="SuggestModuleBase{StoreCatalogSearchForm, IndexableStoreModel, ISearchSuggestResult}"/>
    public class StoreSuggestModule
        : SuggestModuleBase<StoreCatalogSearchForm, IndexableStoreModel, ISearchSuggestResult>
    {
        /// <summary>Initializes a new instance of the <see cref="StoreSuggestModule"/> class.</summary>
        /// <param name="suggestFields">  The suggest fields.</param>
        /// <param name="mapSuggestModel">The map suggest model.</param>
        public StoreSuggestModule(
            Func<FieldsDescriptor<IndexableStoreModel>, IPromise<Fields>> suggestFields,
            Func<SuggestOption<IndexableStoreModel>, ISearchSuggestResult> mapSuggestModel)
            : base(suggestFields, mapSuggestModel)
        {
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<ISearchSuggestResult>> SuggestResultsAsync(
            StoreCatalogSearchForm form,
            string name)
        {
            var client = SearchConfiguration.GetClient();
            var result = await client.SearchAsync<IndexableStoreModel>(s => s
                .Index<IndexableStoreModel>()
                .Source(sf => sf.Includes(SuggestFields))
                .Suggest(su => su
                    .Completion(
                        name,
                        c => c
                            .Prefix(form.Query)
                            .Field(p => p.SuggestedByName)
                            .Fuzzy(f => f.Fuzziness(Fuzziness.Auto)))
                    .Completion(
                        name + "2",
                        c => c
                            .Prefix(form.Query)
                            .Field(p => p.SuggestedByCustomKey)
                            .Fuzzy(f => f.Fuzziness(Fuzziness.Auto))))
                .Size(Math.Min(100, form.PageSize > 0 ? form.PageSize : 5)))
                .ConfigureAwait(false);
            if (!result.IsValid)
            {
                Debug.WriteLine(result.ServerError);
                throw new Exception(
                    result.ServerError.Error.Reason
                    + "\r\n"
                    + result.ServerError.Error.RootCause
                        ?.Select(x => x.Reason)
                        .DefaultIfEmpty(string.Empty)
                        .Aggregate((c, n) => c + "\r\n" + n),
                    result.OriginalException);
            }
            var results = new List<ISearchSuggestResult>();
            MapAndAddSuggestGroup(name, result, results);
            MapAndAddSuggestGroup(name + "2", result, results);
            return results;
        }

        /// <summary>Map and add suggest group.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="result"> The result.</param>
        /// <param name="results">The results.</param>
        private void MapAndAddSuggestGroup(
            string name,
            ISearchResponse<IndexableStoreModel> result,
            List<ISearchSuggestResult> results)
        {
            if (!result.Suggest.ContainsKey(name))
            {
                return;
            }
            results.AddRange(result.Suggest[name][0].Options.Select(MapSuggestModel));
        }
    }
}
