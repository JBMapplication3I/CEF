// <copyright file="SearchModuleBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Web.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Interfaces.Providers.Searching;
    using Nest;

    /// <summary>A search module base.</summary>
    /// <typeparam name="TSearchViewModel">Type of the search view model.</typeparam>
    /// <typeparam name="TSearchForm">     Type of the search form.</typeparam>
    /// <typeparam name="TIndexModel">     Type of the index model.</typeparam>
    public abstract class SearchModuleBase<TSearchViewModel, TSearchForm, TIndexModel>
        where TSearchViewModel : class, ISearchViewModelBase<TSearchForm, TIndexModel>, new()
        where TSearchForm : class, ISearchFormBase, new()
        where TIndexModel : class, IIndexableModelBase
    {
        /// <summary>Searches for the first results.</summary>
        /// <param name="form">The form.</param>
        /// <returns>The found results.</returns>
        public virtual async Task<TSearchViewModel> SearchResultsAsync(TSearchForm form)
        {
            var model = new TSearchViewModel();
            var client = ElasticSearchClientFactory.GetClient();
            var result = await client.SearchAsync<TIndexModel>(
                s => s
                    .From((form.Page - 1) * form.PageSize)
                    .Size(form.PageSize)
                    .Sort(so => Sort(so, form))
                    .Aggregations(a => Aggregations(a, form))
                    .Query(q => Query(q, form)))
                .ConfigureAwait(false);
            model.IsValid = result.IsValid;
            if (!result.IsValid)
            {
                model.ServerError = result.ServerError;
                model.DebugInformation = result.DebugInformation;
            }
            else
            {
                SearchViewModelAdditionalAssignments(model, result);
                model.Documents = result.Documents;
                model.HitsMetaDataTotal = result.HitsMetadata.Total.Value;
                model.HitsMetaDataMaxScore = result.HitsMetadata.MaxScore ?? 0d;
                model.HitsMetaDataHitScores = result.HitsMetadata.Hits.ToDictionary(x => x.Id, x => x.Score);
                model.Total = result.Total;
                model.TotalPages = form.PageSize <= 0 ? 0 : (int)Math.Ceiling(result.Total / (double)form.PageSize);
            }
            model.ElapsedMilliseconds = result.Took;
            model.Form = form;
            return model;
        }

        /// <summary>Searches for the first view model additional assignments.</summary>
        /// <param name="model"> The model.</param>
        /// <param name="result">The result.</param>
        protected virtual void SearchViewModelAdditionalAssignments(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result)
        {
            // Do Nothing
        }

        /// <summary>Sorts the given sort.</summary>
        /// <param name="sort">The sort.</param>
        /// <param name="form">The form.</param>
        /// <returns>The sorted values.</returns>
        protected abstract IPromise<IList<ISort>> Sort(SortDescriptor<TIndexModel> sort, TSearchForm form);

        /// <summary>Aggregations the given a.</summary>
        /// <param name="a">   The AggregationContainerDescriptor{TIndexModel} to process.</param>
        /// <param name="form">The form.</param>
        /// <returns>An IAggregationContainer.</returns>
        protected abstract IAggregationContainer Aggregations(
            AggregationContainerDescriptor<TIndexModel> a,
            TSearchForm form);

        /// <summary>Queries the given q.</summary>
        /// <param name="q">   The QueryContainerDescriptor{TIndexModel} to process.</param>
        /// <param name="form">The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected abstract QueryContainer Query(QueryContainerDescriptor<TIndexModel> q, TSearchForm form);
    }
}
