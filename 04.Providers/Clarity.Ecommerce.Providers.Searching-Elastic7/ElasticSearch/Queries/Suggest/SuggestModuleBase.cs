// <copyright file="SuggestModuleBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the suggest module base class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Web.Search
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Providers.Searching;
    using Nest;

    /// <summary>A suggest module.</summary>
    /// <typeparam name="TSearchForm">Type of the search model.</typeparam>
    /// <typeparam name="TIndexModel"> Type of the index model.</typeparam>
    /// <typeparam name="TResult">     Type of the result.</typeparam>
    public abstract class SuggestModuleBase<TSearchForm, TIndexModel, TResult>
        where TSearchForm : ISearchFormBase
        where TIndexModel : class, IIndexableModelBase
    {
        /// <summary>Initializes a new instance of the <see cref="SuggestModuleBase{TSearchModel, TIndexModel, TResult}"/> class.</summary>
        /// <param name="suggestFields">  The suggest fields.</param>
        /// <param name="mapSuggestModel">The map suggest model.</param>
        // ReSharper disable once StyleCop.SA1642
        protected SuggestModuleBase(
            Func<FieldsDescriptor<TIndexModel>, IPromise<Fields>> suggestFields,
            Func<ISuggestOption<TIndexModel>, TResult> mapSuggestModel)
        {
            SuggestFields = suggestFields;
            MapSuggestModel = mapSuggestModel;
        }

        /// <summary>Gets the map suggest model.</summary>
        /// <value>The map suggest model.</value>
        protected Func<ISuggestOption<TIndexModel>, TResult> MapSuggestModel { get; }

        /// <summary>Gets the suggest fields.</summary>
        /// <value>The suggest fields.</value>
        protected Func<FieldsDescriptor<TIndexModel>, IPromise<Fields>> SuggestFields { get; }

        /// <summary>Suggest results.</summary>
        /// <param name="form">The form.</param>
        /// <param name="name">The name.</param>
        /// <returns>A string.</returns>
        public abstract Task<IEnumerable<TResult>> SuggestResultsAsync(TSearchForm form, string name);
    }
}
