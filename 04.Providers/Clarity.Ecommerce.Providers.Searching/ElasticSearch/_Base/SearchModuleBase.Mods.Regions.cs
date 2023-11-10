// <copyright file="SearchModuleBase.Mods.Regions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. regions class</summary>
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
        protected const string NestedNameForRecordRegions = "record-regions";
        protected const string NestedNameForRecordRegionSingle = "record-single-region";
        protected const string NestedNameForRecordRegionsAny = "record-regions-any";
        protected const string TermsNameForRecordRegionIDs = "record-regions-ids";
        protected const string TermsNameForRecordRegionNames = "record-regions-names";
        #endregion

        /// <summary>regions single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer RegionsSingleQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (!Contract.CheckValidID(form.RegionID))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Bool(b => b
                    .Name(NestedNameForRecordRegionSingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSingleRegion)
                    .Should(s => s.Term(p => p.ContactRegionID, form.RegionID))
                    .MinimumShouldMatch(MinimumShouldMatch.Fixed(1)));
            return returnQuery;
        }

        /// <summary>Types any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer RegionsAnyQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.RegionIDsAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Bool(b => b
                    .Name(NestedNameForRecordRegionsAny)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyRegion)
                    .Should(s => s.Term(p => p.ContactRegionID, form.RegionID))
                    .MinimumShouldMatch(MinimumShouldMatch.Fixed(1)));
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for regions.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForRegions(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.Regions = result.Aggregations
                ?.Terms(NestedNameForRecordRegions)
                ?.Buckets
                .ToDictionary(
                    regionName => regionName.Key,
                    region => new IndexableRegionModel
                    {
                        DocCount = region.DocCount,
                        ID = int.Parse(
                            region
                                .Terms(TermsNameForRecordRegionIDs)
                                .Buckets
                                .Select(x => x.Key)
                                .First()),
                    });
        }

        /// <summary>Appends the aggregations for regions.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForRegions(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Terms(NestedNameForRecordRegions, tsKey => tsKey
                .Field(p => p.ContactRegionName.Suffix(Keyword))
                .Missing(NotAvailable)
                .Size(100)
                .Aggregations(aaa => aaa
                    .Terms(TermsNameForRecordRegionIDs, tsValue => tsValue
                        .Field(p => p.ContactRegionID)
                        .Missing(0)
                        .Size(100)
                    )
                ).Size(100)
            );
            return returnQuery;
        }
    }
}
