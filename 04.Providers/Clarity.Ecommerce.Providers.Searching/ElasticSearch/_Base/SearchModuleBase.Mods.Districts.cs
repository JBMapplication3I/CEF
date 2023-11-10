// <copyright file="SearchModuleBase.Mods.Districts.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. districts class</summary>
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
        protected const string NestedNameForRecordDistrictSingle = "record-single-district";
        protected const string NestedNameForRecordDistrictsAny = "record-districts-any";
        protected const string NestedNameForRecordDistrictsAllPrefix = "record-districts-all-";
        protected const string TermsNameForRecordDistrictIDs = "record-districts-ids";
        protected const string TermsNameForRecordDistrictKeys = "record-districts-keys";
        protected const string TermsNameForRecordDistrictNames = "record-districts-names";
        #endregion

        /// <summary>Districts single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer DistrictsSingleQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (!Contract.CheckValidID(form.DistrictID))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Bool(b => b
                    .Name(NestedNameForRecordDistrictSingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSingleDistrict)
                    .Should(
                        s => s.Term(p => p.Districts!.First().ID, form.DistrictID))
                    .MinimumShouldMatch(MinimumShouldMatch.Fixed(1)));
            return returnQuery;
        }

        /// <summary>Districts any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer DistrictsAnyQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.DistrictIDsAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForRecordDistrictsAny)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyDistricts)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Districts)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Terms(t => t.Field(p => p.Districts!.First().ID).Terms(form.DistrictIDsAny))
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Districts all query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer DistrictsAllQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.DistrictIDsAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.DistrictIDsAll.Where(x => Contract.CheckValidID(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForRecordDistrictsAllPrefix}{filter}")
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsAllDistricts)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.Districts)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.Districts!.First().ID).Terms(filter))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for districts.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForDistricts(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.Districts = result.Aggregations
                ?.Terms(TermsNameForRecordDistrictNames)
                ?.Buckets
                .ToDictionary(
                    districtName => districtName.Key,
                    district => new IndexableDistrictModel
                    {
                        DocCount = district.DocCount,
                        ID = int.Parse(
                            district
                                .Terms(TermsNameForRecordDistrictIDs)
                                .Buckets
                                .Select(x => x.Key)
                                .First()),
                    });
        }

        /// <summary>Appends the aggregations for districts.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForDistricts(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Terms(TermsNameForRecordDistrictNames, tsKey => tsKey
                .Field(p => p.Districts!.First().Name.Suffix(Keyword))
                .Missing(NotAvailable)
                .Size(100)
                .Aggregations(aaa => aaa
                    .Terms(TermsNameForRecordDistrictIDs, tsValue => tsValue
                        .Field(p => p.Districts!.First().ID)
                        .Missing(0)
                        .Size(100)
                    )
                ).Size(100)
            );
            return returnQuery;
        }
    }
}
