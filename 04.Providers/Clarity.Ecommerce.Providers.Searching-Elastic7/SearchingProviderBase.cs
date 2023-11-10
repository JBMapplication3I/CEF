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
        public abstract Task PurgeAsync(string contextProfileName, string index);

        /// <inheritdoc/>
        public abstract Task IndexAsync(string contextProfileName, string index, CancellationToken ct);

        /// <inheritdoc/>
        public abstract Task<IEnumerable<ISearchSuggestResult>> SuggestionsAsync<TSearchForm, TResult>(
                TSearchForm form,
                string name,
                string contextProfileName)
            where TSearchForm : ISearchFormBase
            where TResult : ISearchSuggestResult;

        /// <inheritdoc/>
        public abstract Task<TSearchViewModel> QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                TSearchForm form,
                string index,
                string contextProfileName)
            where TSearchViewModel : class, ISearchViewModelBase<TSearchForm, TIndexModel>, new()
            where TSearchForm : class, ISearchFormBase, new()
            where TIndexModel : IIndexableModelBase;

        /// <inheritdoc/>
        public abstract Task<List<ISearchSuggestResult>> GetProductSuggestionsFromProviderAsync(
            IProductCatalogSearchForm form,
            string contextProfileName);

        /// <inheritdoc/>
        public abstract Task<ProductSearchViewModel> GetProductSearchResultsFromProviderAsync(
            IProductCatalogSearchForm form,
            string contextProfileName);

        /// <inheritdoc/>
        public abstract Task<DateTime?> GetProductSearchResultsFromProviderAndMapEachWithAdditionalDataLastModifiedAsync(
            IProductCatalogSearchForm form,
            string contextProfileName,
            List<string> roles);

        /// <inheritdoc/>
        public abstract Task<ProductSearchViewModel> GetProductSearchResultsFromProviderAndMapEachWithAdditionalDataAsync(
            IProductCatalogSearchForm form,
            string contextProfileName,
            List<string> roles);
    }
}
