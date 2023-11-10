// <copyright file="SuggestModule.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store suggest module class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Providers.Searching;
    using Nest;

    /// <summary>A store suggest module.</summary>
    /// <seealso cref="SuggestModuleBase{StoreCatalogSearchForm, StoreIndexableModel, ISearchSuggestResult}"/>
    internal class StoreSuggestModule
        : SuggestModuleBase<StoreCatalogSearchForm, StoreIndexableModel, StoreSuggestResult>
    {
        /// <summary>Initializes a new instance of the <see cref="StoreSuggestModule"/> class.</summary>
        /// <param name="suggestFields">  The suggest fields.</param>
        /// <param name="mapSuggestModel">The map suggest model.</param>
        public StoreSuggestModule(
                Func<FieldsDescriptor<StoreIndexableModel>, IPromise<Fields>> suggestFields,
                Func<SuggestOption<StoreIndexableModel>?, StoreSuggestResult?> mapSuggestModel)
            : base(suggestFields, mapSuggestModel)
        {
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<StoreSuggestResult?>> SuggestResultsAsync(
            StoreCatalogSearchForm form,
            string name,
            string? contextProfileName)
        {
            var client = ElasticSearchClientFactory.GetClient();
            var result = await client.SearchAsync<StoreIndexableModel>(s => s
                .Index<StoreIndexableModel>()
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
                    /*
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
                            .Fuzzy(f => f.Fuzziness(Fuzziness.EditDistance(2))))
                    */
                    .Completion(
                        name + "7",
                        c => c
                            .Prefix(form.Query)
                            .Field(p => p.SuggestedByQueryableSerializableAttributes)
                            .Fuzzy(f => f.Fuzziness(Fuzziness.EditDistance(0))))
                    .Completion(
                        name + "8",
                        c => c
                            .Prefix(form.Query)
                            .Field(p => p.SuggestedByQueryableSerializableAttributes)
                            .Fuzzy(f => f.Fuzziness(Fuzziness.EditDistance(2))))
                    .Completion(
                        name + "9",
                        c => c
                            .Prefix(form.Query)
                            .Field(p => p.SuggestedByQueryableSerializableAttributes)
                            .Fuzzy(f => f.Fuzziness(Fuzziness.Auto)))
                    .Term(
                        name + "10",
                        c => c
                            .Text(form.Query)
                            .Field(p => p.SuggestedByQueryableSerializableAttributes)
                            .Analyzer(ProductIndexer.AnalyzerNameForProductName)
                            .MaxEdits(2))
                )
                .Size(Math.Min(100, form.PageSize > 0 ? form.PageSize : 5)))
                .ConfigureAwait(false);
            if (result.ApiCall != null)
            {
                await Log(Newtonsoft.Json.JsonConvert.SerializeObject(result.ApiCall), contextProfileName).ConfigureAwait(false);
            }
            if (!result.IsValid)
            {
                if (result.ServerError != null)
                {
                    Debug.WriteLine(result.ServerError);
                    throw new(
                        result.ServerError.Error.Reason
                        + Environment.NewLine
                        + result.ServerError.Error.RootCause
                            ?.Select(x => x.Reason)
                            .DefaultIfEmpty(string.Empty)
                            .Aggregate((c, n) => c + Environment.NewLine + n),
                        result.OriginalException);
                }
                Debug.WriteLine(result.DebugInformation);
                throw new(result.DebugInformation);
            }
            var results = new List<StoreSuggestResult?>();
            MapAndAddSuggestGroup(name, result, results);
            MapAndAddSuggestGroup(name + "2", result, results);
            MapAndAddSuggestGroup(name + "3", result, results);
            MapAndAddSuggestGroup(name + "4", result, results);
            /*
            MapAndAddSuggestGroup(name + "5", result, results);
            MapAndAddSuggestGroup(name + "6", result, results);
            */
            MapAndAddSuggestGroup(name + "7", result, results);
            MapAndAddSuggestGroup(name + "8", result, results);
            MapAndAddSuggestGroup(name + "9", result, results);
            MapAndAddSuggestGroup(name + "10", result, results);
            return results
                .Where(x => x != null)
                .OrderByDescending(x => x!.Score)
                .Take(Math.Min(100, form.PageSize > 0 ? form.PageSize : 5));
        }
    }
}
