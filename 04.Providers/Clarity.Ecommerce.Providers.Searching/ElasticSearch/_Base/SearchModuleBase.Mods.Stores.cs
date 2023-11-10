// <copyright file="SearchModuleBase.Mods.Stores.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. stores class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System.Linq;
    using Interfaces.Providers.Searching;
    using Nest;
    using Utilities;

    internal abstract partial class SearchModuleBase<TSearchViewModel, TSearchForm, TIndexModel>
        where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>, new()
        where TSearchForm : SearchFormBase, new()
        where TIndexModel : IndexableModelBase
    {
        #region Constant Strings
        protected const string NestedNameForRecordStores = "record-stores";
        protected const string NestedNameForRecordStoreSingle = "record-single-store";
        protected const string NestedNameForRecordStoresAny = "record-stores-any";
        protected const string NestedNameForRecordStoresAllPrefix = "record-stores-all-";
        protected const string TermsNameForRecordStoreIDs = "record-stores-ids";
        protected const string TermsNameForRecordStoreKeys = "record-stores-keys";
        protected const string TermsNameForRecordStoreNames = "record-stores-names";
        #endregion

        /// <summary>Stores single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer StoresSingleQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (!Contract.CheckValidID(form.StoreID))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForRecordStoreSingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSingleStore)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Stores)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Term(p => p.Stores!.First().ID.Suffix(Keyword), form.StoreID)
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Stores any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer StoresAnyQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.StoreIDsAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForRecordStoresAny)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyStores)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Stores)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Terms(t => t.Field(p => p.Stores!.First().ID.Suffix(Keyword)).Terms(form.StoreIDsAny))
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Stores all query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer StoresAllQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.StoreIDsAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.StoreIDsAll.Where(x => Contract.CheckValidID(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForRecordStoresAllPrefix}{filter}")
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsAllStores)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.Stores)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.Stores!.First().ID.Suffix(Keyword)).Terms(filter))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for stores.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForStores(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.StoreIDs = result.Aggregations.Nested(NestedNameForRecordStores)
                ////?.Aggregations
                ?.Terms(TermsNameForRecordStoreIDs)
                ?.Buckets
                .ToDictionary(x => x.Key, x => x.DocCount);
        }

        /// <summary>Appends the aggregations for stores.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForStores(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Nested(NestedNameForRecordStores, n => n
                .Path(p => p.Stores)
                .Aggregations(ap => ap
                    .Terms(TermsNameForRecordStoreIDs, ts => ts
                        .Field(p => p.Stores!.First().ID.Suffix(Keyword))
                        .Missing(0).Size(30)
                    )
                    .Terms(TermsNameForRecordStoreKeys, ts => ts
                        .Field(p => p.Stores!.First().Key.Suffix(Keyword))
                        .Missing(string.Empty).Size(30)
                    )
                    .Terms(TermsNameForRecordStoreNames, ts => ts
                        .Field(p => p.Stores!.First().Name.Suffix(Keyword))
                        .Missing(string.Empty).Size(30)
                    )
                )
            );
            return returnQuery;
        }
    }
}
