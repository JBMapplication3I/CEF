// <copyright file="SearchModuleBase.Mods.Types.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. types class</summary>
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
        protected const string NestedNameForRecordTypes = "record-types";
        protected const string NestedNameForRecordTypeSingle = "record-single-type";
        protected const string NestedNameForRecordTypesAny = "record-types-any";
        protected const string TermsNameForRecordTypeIDs = "record-types-ids";
        protected const string TermsNameForRecordTypeNames = "record-types-names";
        protected const string TermsNameForRecordTypeSortOrders = "record-types-sort-orders";
        #endregion

        /// <summary>Types single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer TypesSingleQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (!Contract.CheckValidID(form.TypeID))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Bool(b => b
                    .Name(NestedNameForRecordTypeSingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSingleType)
                    .Should(s => s.Term(p => p.TypeID, form.TypeID))
                    .MinimumShouldMatch(MinimumShouldMatch.Fixed(1)));
            return returnQuery;
        }

        /// <summary>Types any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer TypesAnyQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.TypeIDsAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Bool(b => b
                    .Name(NestedNameForRecordTypesAny)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyType)
                    .Should(s => s.Term(p => p.TypeID, form.TypeID))
                    .MinimumShouldMatch(MinimumShouldMatch.Fixed(1)));
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for types.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForTypes(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.Types = result.Aggregations
                ?.Terms(TermsNameForRecordTypeNames)
                ?.Buckets
                .ToDictionary(
                    typeName => typeName.Key,
                    type => new IndexableTypeModel
                    {
                        Count = type.DocCount ?? 0,
                        ID = int.Parse(
                            type
                                .Terms(TermsNameForRecordTypeIDs)
                                .Buckets
                                .Select(x => x.Key)
                                .First()),
                        SortOrder = int.Parse(
                            type
                                .Terms(TermsNameForRecordTypeIDs)
                                .Buckets
                                .First()
                                .Terms(TermsNameForRecordTypeSortOrders)
                                .Buckets
                                .Select(x => x.Key)
                                .First()
                        ),
                    });
        }

        /// <summary>Appends the aggregations for types.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForTypes(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Terms(TermsNameForRecordTypeIDs, tsKey => tsKey
                .Field(p => p.TypeName)
                .Missing(NotAvailable)
                .Size(100)
                .Aggregations(aaa => aaa
                    .Terms(TermsNameForRecordTypeIDs, tsValue => tsValue
                        .Field(p => p.TypeID)
                        .Missing(0)
                        .Size(100)
                        .Aggregations(bbb => bbb
                            .Terms(TermsNameForRecordTypeSortOrders, tsValue2 => tsValue2
                                .Field(p => p.TypeSortOrder)
                                .Missing(0)
                                .Size(100)
                            )
                        ).Size(100)
                    )
                ).Size(100)
            );
            return returnQuery;
        }
    }
}
