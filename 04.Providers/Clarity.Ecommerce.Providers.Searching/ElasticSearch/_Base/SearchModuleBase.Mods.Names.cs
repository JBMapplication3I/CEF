// <copyright file="SearchModuleBase.Mods.Names.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. names class</summary>
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
        protected const string NestedNameForRecordNames = "record-names";
        protected const string NestedNameForRecordNameSingle = "record-single-name";
        #endregion

        /// <summary>Searches for the first view model additional assignments for names.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForNames(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.Names = result.Aggregations
                ?.Terms(NestedNameForRecordNames)
                ?.Buckets
                .ToDictionary(x => x.Key, x => x.DocCount);
        }

        /// <summary>Appends the aggregations for names.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForNames(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Terms(NestedNameForRecordNames, tsKey => tsKey
                .Field(p => p.Name.Suffix(Raw))
                .Missing(NotAvailable)
                .Size(100)
            );
            return returnQuery;
        }
    }
}
