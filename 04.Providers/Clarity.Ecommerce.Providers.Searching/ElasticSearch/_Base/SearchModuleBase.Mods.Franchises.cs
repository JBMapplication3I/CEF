// <copyright file="SearchModuleBase.Mods.Franchises.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. franchises class</summary>
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
        protected const string NestedNameForRecordFranchises = "record-franchises";
        protected const string NestedNameForRecordFranchiseSingle = "record-single-franchise";
        protected const string NestedNameForRecordFranchisesAny = "record-franchises-any";
        protected const string NestedNameForRecordFranchisesAllPrefix = "record-franchises-all-";
        protected const string TermsNameForRecordFranchiseIDs = "record-franchises-ids";
        protected const string TermsNameForRecordFranchiseKeys = "record-franchises-keys";
        protected const string TermsNameForRecordFranchiseNames = "record-franchises-names";
        #endregion

        /// <summary>Franchises single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer FranchisesSingleQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (!Contract.CheckValidID(form.FranchiseID))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForRecordFranchiseSingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSingleFranchise)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Franchises)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Term(p => p.Franchises!.First().ID.Suffix(Keyword), form.FranchiseID)
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Franchises any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer FranchisesAnyQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.FranchiseIDsAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForRecordFranchisesAny)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyFranchises)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Franchises)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Terms(t => t.Field(p => p.Franchises!.First().ID.Suffix(Keyword)).Terms(form.FranchiseIDsAny))
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Franchises all query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer FranchisesAllQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.FranchiseIDsAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.FranchiseIDsAll.Where(x => Contract.CheckValidID(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForRecordFranchisesAllPrefix}{filter}")
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsAllFranchises)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.Franchises)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.Franchises!.First().ID.Suffix(Keyword)).Terms(filter))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for franchises.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForFranchises(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.FranchiseIDs = result.Aggregations.Nested(NestedNameForRecordFranchises)
                ////?.Aggregations
                ?.Terms(TermsNameForRecordFranchiseIDs)
                ?.Buckets
                .ToDictionary(x => x.Key, x => x.DocCount);
        }

        /// <summary>Appends the aggregations for franchises.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForFranchises(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Nested(NestedNameForRecordFranchises, n => n
                .Path(p => p.Franchises)
                .Aggregations(ap => ap
                    .Terms(TermsNameForRecordFranchiseIDs, ts => ts
                        .Field(p => p.Franchises!.First().ID.Suffix(Keyword))
                        .Missing(0).Size(30)
                    )
                    .Terms(TermsNameForRecordFranchiseKeys, ts => ts
                        .Field(p => p.Franchises!.First().Key.Suffix(Keyword))
                        .Missing(string.Empty).Size(30)
                    )
                    .Terms(TermsNameForRecordFranchiseNames, ts => ts
                        .Field(p => p.Franchises!.First().Name.Suffix(Keyword))
                        .Missing(string.Empty).Size(30)
                    )
                )
            );
            return returnQuery;
        }
    }
}
