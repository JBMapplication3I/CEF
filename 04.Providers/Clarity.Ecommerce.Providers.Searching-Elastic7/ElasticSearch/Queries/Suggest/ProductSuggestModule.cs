// <copyright file="ProductSuggestModule.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product suggest module class</summary>
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
    /// <seealso cref="SuggestModuleBase{ProductCatalogSearchForm, IndexableProductModel, ISearchSuggestResult}"/>
    public class ProductSuggestModule
        : SuggestModuleBase<ProductCatalogSearchForm, IndexableProductModel, ISearchSuggestResult>
    {
        /// <summary>Initializes a new instance of the <see cref="ProductSuggestModule"/> class.</summary>
        /// <param name="suggestFields">  The suggest fields.</param>
        /// <param name="mapSuggestModel">The map suggest model.</param>
        public ProductSuggestModule(
                Func<FieldsDescriptor<IndexableProductModel>, IPromise<Fields>> suggestFields,
                Func<ISuggestOption<IndexableProductModel>, ISearchSuggestResult> mapSuggestModel)
            : base(suggestFields, mapSuggestModel)
        {
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<ISearchSuggestResult>> SuggestResultsAsync(
            ProductCatalogSearchForm form,
            string name)
        {
            var client = ElasticSearchClientFactory.GetClient();
            var result = await client.SearchAsync<IndexableProductModel>(s => s
                .Index<IndexableProductModel>()
                .Source(sf => sf.Includes(SuggestFields))
                .Suggest(su => su
                    .Completion(
                        name,
                        c => c
                            .Prefix(form.Query)
                            .Field(p => p.SuggestedByName)
                            .Fuzzy(f => f.Fuzziness(Fuzziness.EditDistance(0))))
                    .Completion(
                        name + "2",
                        c => c
                            .Prefix(form.Query)
                            .Field(p => p.SuggestedByName)
                            .Fuzzy(f => f.Fuzziness(Fuzziness.EditDistance(2))))
                    .Completion(
                        name + "3",
                        c => c
                            .Prefix(form.Query)
                            .Field(p => p.SuggestedByCustomKey)
                            .Fuzzy(f => f.Fuzziness(Fuzziness.EditDistance(0))))
                    .Completion(
                        name + "4",
                        c => c
                            .Prefix(form.Query)
                            .Field(p => p.SuggestedByCustomKey)
                            .Fuzzy(f => f.Fuzziness(Fuzziness.EditDistance(2))))
                    .Completion(
                        name + "5",
                        c => c
                            .Prefix(form.Query)
                            .Field(p => p.SuggestedByBrandName)
                            .Fuzzy(f => f.Fuzziness(Fuzziness.EditDistance(2))))
                    .Completion(
                        name + "6",
                        c => c
                            .Prefix(form.Query)
                            .Field(p => p.SuggestedByManufacturerPartNumber)
                            .Fuzzy(f => f.Fuzziness(Fuzziness.EditDistance(2)))))
                .Size(Math.Min(100, form.PageSize > 0 ? form.PageSize : 5)))
                .ConfigureAwait(false);
            if (!result.IsValid)
            {
                if (result.ServerError != null)
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
                Debug.WriteLine(result.DebugInformation);
                throw new(result.DebugInformation);
            }
            var results = new List<ISearchSuggestResult>();
            MapAndAddSuggestGroup(name, result, results);
            MapAndAddSuggestGroup(name + "2", result, results);
            MapAndAddSuggestGroup(name + "3", result, results);
            MapAndAddSuggestGroup(name + "4", result, results);
            MapAndAddSuggestGroup(name + "5", result, results);
            MapAndAddSuggestGroup(name + "6", result, results);
            return results
                .OrderByDescending(p => p.Score)
                .Take(Math.Min(100, form.PageSize > 0 ? form.PageSize : 5));
        }

        /// <summary>Map and add suggest group.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="result"> The result.</param>
        /// <param name="results">The results.</param>
        private void MapAndAddSuggestGroup(
            string name,
            ISearchResponse<IndexableProductModel> result,
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
