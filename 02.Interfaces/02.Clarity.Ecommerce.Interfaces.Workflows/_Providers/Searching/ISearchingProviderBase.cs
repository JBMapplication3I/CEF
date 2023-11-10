// <copyright file="ISearchingProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISearchingProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>Interface for searching provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public partial interface ISearchingProviderBase : IProviderBase
    {
        /// <summary>Purges the given index.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="index">             The name of the elastic search index.</param>
        /// <returns>A Task.</returns>
        Task PurgeAsync(string? contextProfileName, string index);

        /// <summary>Indexes the given index.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="index">             The name of the elastic search index.</param>
        /// <param name="ct">                The cancellation token.</param>
        /// <returns>A Task.</returns>
        Task IndexAsync(string? contextProfileName, string index, CancellationToken ct);

        /// <summary>Suggestions from elastic search.</summary>
        /// <typeparam name="TSearchForm">   Type of the search form.</typeparam>
        /// <typeparam name="TSuggestResult">Type of the suggest result.</typeparam>
        /// <param name="form">              The form.</param>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process suggestions in this collection.</returns>
        Task<IEnumerable<TSuggestResult?>> SuggestionsAsync<TSearchForm, TSuggestResult>(
                TSearchForm form,
                string name,
                string? contextProfileName)
            where TSearchForm : SearchFormBase
            where TSuggestResult : SuggestResultBase;

        /// <summary>Gets the search view model.</summary>
        /// <typeparam name="TSearchViewModel">Type of the search view model.</typeparam>
        /// <typeparam name="TSearchForm">     Type of the search form.</typeparam>
        /// <typeparam name="TIndexModel">     Type of the index model.</typeparam>
        /// <param name="form">              The form.</param>
        /// <param name="index">             The name of the elastic search index.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The search view model.</returns>
        Task<TSearchViewModel> QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                TSearchForm form,
                string index,
                string? contextProfileName)
            where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>
            where TSearchForm : SearchFormBase, new()
            where TIndexModel : IndexableModelBase;

        /// <summary>Gets all suggestions from the provider for the given form.</summary>
        /// <param name="form">              The form.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The suggestions from provider.</returns>
        Task<List<SuggestResultBase?>> GetAllSuggestionsFromProviderAsync(
            SearchFormBase form,
            string? contextProfileName);

        /// <summary>Gets search results from provider.</summary>
        /// <typeparam name="TSearchViewModel">Type of the search view model.</typeparam>
        /// <typeparam name="TSearchForm">     Type of the search form.</typeparam>
        /// <typeparam name="TIndexModel">     Type of the index model.</typeparam>
        /// <param name="form">              The form.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="roles">             The roles.</param>
        /// <returns>The search results from provider.</returns>
        Task<TSearchViewModel> GetSearchResultsFromProviderAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                TSearchForm form,
                string? contextProfileName,
                List<string>? roles = null)
            where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>
            where TSearchForm : SearchFormBase, new()
            where TIndexModel : IndexableModelBase;

        /// <summary>Gets search results from provider last modified date for caching.</summary>
        /// <param name="form">              The form.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="roles">             The roles.</param>
        /// <returns>The search results from provider last modified date for caching.</returns>
        Task<DateTime?> GetAllSearchResultsAsViewModelLastModifiedAsync(
                SearchFormBase form,
                string? contextProfileName,
                List<string>? roles = null);

        /// <summary>Gets search results from provider.</summary>
        /// <param name="form">              The form.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="roles">             The roles.</param>
        /// <returns>The search results from provider.</returns>
        Task<List<SearchViewModelBase>> GetAllSearchResultsAsViewModelsAsync(
            SearchFormBase form,
            string? contextProfileName,
            List<string>? roles = null);
    }
}
