// <copyright file="SearchModuleBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Searching;
    using Nest;

    /// <summary>A search module base.</summary>
    /// <typeparam name="TSearchViewModel">Type of the search view model.</typeparam>
    /// <typeparam name="TSearchForm">     Type of the search form.</typeparam>
    /// <typeparam name="TIndexModel">     Type of the index model.</typeparam>
    internal abstract partial class SearchModuleBase<TSearchViewModel, TSearchForm, TIndexModel>
        : LoggingBase
        where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>, new()
        where TSearchForm : SearchFormBase, new()
        where TIndexModel : IndexableModelBase
    {
        #region Constant Strings
        // General
        protected const string Keyword = "keyword";
        protected const string Raw = "raw";
        protected const string NotAvailable = "N/A";
        #endregion

        /// <summary>Searches for the first results.</summary>
        /// <param name="form">              The form.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The found results.</returns>
        public virtual async Task<TSearchViewModel> SearchResultsAsync(TSearchForm form, string? contextProfileName)
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
            if (result.ApiCall != null)
            {
                await Log(
                        body: Newtonsoft.Json.JsonConvert.SerializeObject(
                            result.ApiCall,
                            SerializableAttributesDictionaryExtensions.JsonSettings),
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            if (!result.IsValid)
            {
                model.ServerError = result.ServerError;
                model.DebugInformation = result.DebugInformation;
            }
            else
            {
                SearchViewModelAdditionalAssignments(model, result);
                model.Documents = result.Documents;
                model.HitsMetaDataTotal = result.HitsMetadata.Total;
                model.HitsMetaDataMaxScore = result.HitsMetadata.MaxScore;
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
        protected abstract void SearchViewModelAdditionalAssignments(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result);

        /// <summary>Sorts the given sort.</summary>
        /// <param name="sort">The sort.</param>
        /// <param name="form">The form.</param>
        /// <returns>The sorted values.</returns>
        protected abstract IPromise<IList<ISort>> Sort(
            SortDescriptor<TIndexModel> sort,
            TSearchForm form);

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
        protected abstract QueryContainer Query(
            QueryContainerDescriptor<TIndexModel> q,
            TSearchForm form);
    }
}
