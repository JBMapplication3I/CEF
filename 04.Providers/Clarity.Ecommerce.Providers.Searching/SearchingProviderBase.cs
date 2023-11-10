// <copyright file="SearchingProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the searching provider base class</summary>
namespace Clarity.Ecommerce.Providers.Searching
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces.Providers.Searching;
    using Utilities;

    /// <summary>A searching provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISearchingProviderBase"/>
    public abstract class SearchingProviderBase : ProviderBase, ISearchingProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Searching;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task PurgeAsync(string? contextProfileName, string index);

        /// <inheritdoc/>
        public abstract Task IndexAsync(string? contextProfileName, string index, CancellationToken ct);

        /// <inheritdoc/>
        public abstract Task<IEnumerable<TSuggestResult?>> SuggestionsAsync<TSearchForm, TSuggestResult>(
                TSearchForm form,
                string name,
                string? contextProfileName)
            where TSearchForm : SearchFormBase
            where TSuggestResult : SuggestResultBase;

        /// <inheritdoc/>
        public abstract Task<TSearchViewModel> QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                TSearchForm form,
                string index,
                string? contextProfileName)
            where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>
            where TSearchForm : SearchFormBase, new()
            where TIndexModel : IndexableModelBase;

        /// <inheritdoc/>
        public abstract Task<List<SuggestResultBase?>> GetAllSuggestionsFromProviderAsync(
            SearchFormBase form,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<TSearchViewModel> GetSearchResultsFromProviderAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                TSearchForm form,
                string? contextProfileName,
                List<string>? roles = null)
            where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>
            where TSearchForm : SearchFormBase, new()
            where TIndexModel : IndexableModelBase;

        /// <inheritdoc/>
        public abstract Task<DateTime?> GetAllSearchResultsAsViewModelLastModifiedAsync(
            SearchFormBase form,
            string? contextProfileName,
            List<string>? roles = null);

        /// <inheritdoc/>
        public abstract Task<List<SearchViewModelBase>> GetAllSearchResultsAsViewModelsAsync(
            SearchFormBase form,
            string? contextProfileName,
            List<string>? roles = null);

        /// <summary>Replace placeholder with commas in attributes all/any.</summary>
        /// <param name="form">The form.</param>
        protected static void ReplacePlaceholderWithCommas(SearchFormBase form)
        {
            if (Contract.CheckNotEmpty(form.AttributesAll))
            {
                foreach (var value in form.AttributesAll!.Values)
                {
                    for (var i = 0; i < value.Length; i++)
                    {
                        value[i] = value[i].Replace("`", ",");
                    }
                }
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckNotEmpty(form.AttributesAny))
            {
                foreach (var value in form.AttributesAny!.Values)
                {
                    for (var i = 0; i < value.Length; i++)
                    {
                        value[i] = value[i].Replace("`", ",");
                    }
                }
            }
        }
    }
}
