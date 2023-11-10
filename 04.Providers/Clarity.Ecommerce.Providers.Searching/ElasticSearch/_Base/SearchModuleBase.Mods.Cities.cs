// <copyright file="SearchModuleBase.Mods.Cities.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. cities class</summary>
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
        protected const string NestedNameForRecordCities = "record-cities";
        protected const string NestedNameForRecordCitySingle = "record-single-city";
        #endregion

        /// <summary>Cities single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer CitiesSingleQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (!Contract.CheckValidKey(form.City))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Bool(b => b
                    .Name(NestedNameForRecordCitySingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCityTermKeyword)
                    .Should(
                        s => s.Term(p => p.ContactCity!.Suffix(Keyword), form.City))
                    .MinimumShouldMatch(MinimumShouldMatch.Fixed(1)));
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for cities.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForCities(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.Cities = result.Aggregations
                ?.Terms(NestedNameForRecordCities)
                ?.Buckets
                .ToDictionary(x => x.Key, x => x.DocCount);
        }

        /// <summary>Appends the aggregations for cities.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForCities(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Terms(NestedNameForRecordCities, tsKey => tsKey
                .Field(p => p.ContactCity.Suffix(Keyword))
                .Missing(NotAvailable)
                .Size(100)
            );
            return returnQuery;
        }
    }
}
