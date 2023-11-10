// <copyright file="SuggestModuleBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the suggest module base class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Providers.Searching;
    using Nest;

    /// <summary>A suggest module base.</summary>
    /// <typeparam name="TSearchForm">   Type of the search model.</typeparam>
    /// <typeparam name="TIndexModel">   Type of the index model.</typeparam>
    /// <typeparam name="TSuggestResult">Type of the suggest result.</typeparam>
    internal abstract partial class SuggestModuleBase<TSearchForm, TIndexModel, TSuggestResult>
        : LoggingBase
        where TSearchForm : SearchFormBase
        where TIndexModel : IndexableModelBase
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="SuggestModuleBase{TSearchModel, TIndexModel, TSuggestResult}"/> class.</summary>
        /// <param name="suggestFields">  The suggest fields.</param>
        /// <param name="mapSuggestModel">The map suggest model.</param>
        protected SuggestModuleBase(
            Func<FieldsDescriptor<TIndexModel>, IPromise<Fields>> suggestFields,
            Func<SuggestOption<TIndexModel>?, TSuggestResult?> mapSuggestModel)
        {
            SuggestFields = suggestFields;
            MapSuggestModel = mapSuggestModel;
        }

        /// <summary>Gets the map suggest model.</summary>
        /// <value>The map suggest model.</value>
        protected Func<SuggestOption<TIndexModel>?, TSuggestResult?> MapSuggestModel { get; }

        /// <summary>Gets the suggest fields.</summary>
        /// <value>The suggest fields.</value>
        protected Func<FieldsDescriptor<TIndexModel>, IPromise<Fields>> SuggestFields { get; }

        /// <summary>Suggest results.</summary>
        /// <param name="form">              The form.</param>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A string.</returns>
        public abstract Task<IEnumerable<TSuggestResult?>> SuggestResultsAsync(
            TSearchForm form,
            string name,
            string? contextProfileName);

        /// <summary>Map and add suggest group.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="result"> The result.</param>
        /// <param name="results">The results.</param>
        protected virtual void MapAndAddSuggestGroup(
            string name,
            ISearchResponse<TIndexModel> result,
            List<TSuggestResult?> results)
        {
            if (!result.Suggest.ContainsKey(name) || !result.Suggest[name].Any())
            {
                return;
            }
            results.AddRange(result.Suggest[name][0].Options.Select(MapSuggestModel));
        }
    }
}
