﻿// <copyright file="ElasticSearchingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Linq;
    using Interfaces.Providers.Searching;
    using Nest;
    using Utilities;

    /// <summary>An elastic searching provider.</summary>
    /// <seealso cref="SearchingProviderBase"/>
    public partial class ElasticSearchingProvider
    {
        /// <summary>(Immutable) The map store suggest model.</summary>
        private static readonly Func<SuggestOption<StoreIndexableModel>?, StoreSuggestResult?> MapStoreSuggestModel
            = suggest => suggest?.Source == null ? null : new()
            {
                ID = suggest.Source.ID,
                Name = suggest.Source.Name,
                CustomKey = suggest.Source.CustomKey,
                SeoUrl = suggest.Source.SeoUrl,
                Score = suggest.Score,
                QueryableAttributes = Contract.CheckValidKey(
                        ElasticSearchingProviderConfig.SearchingProductIndexSuggestOptionAttributeKeys)
                    ? suggest.Source.QueryableAttributes?.ToDictionary(x => x.Key!, x => x.Value!)
                    : null,
            };

        /// <summary>Gets or sets the event type identifier for store catalog search.</summary>
        /// <value>The event type identifier for store catalog search.</value>
        private static int EventTypeIDForStoreCatalogSearch { get; set; }

        /// <summary>Gets the store suggest module.</summary>
        /// <value>The store suggest module.</value>
        private static StoreSuggestModule StoreSuggestModule { get; }
            = new(StoreSuggestFields, MapStoreSuggestModel);

        /// <summary>Stores suggest fields.</summary>
        /// <typeparam name="TIndexModel">Type of the index model.</typeparam>
        /// <param name="f">The FieldsDescriptor{TIndexModel} to process.</param>
        /// <returns>An IPromise{Fields}.</returns>
        private static IPromise<Fields> StoreSuggestFields<TIndexModel>(FieldsDescriptor<TIndexModel> f)
            where TIndexModel : StoreIndexableModel
        {
            return f
                .Field(ff => ff.ID)
                .Field(ff => ff.Name)
                .Field(ff => ff.CustomKey)
                .Field(ff => ff.SeoUrl)
                .Field(ff => ff.SuggestedByName)
                .Field(ff => ff.SuggestedByCustomKey)
                .Field(ff => ff.SuggestedByQueryableSerializableAttributes)
                .Field(ff => ff.CategoryNamePrimary)
                .Field(ff => ff.CategoryNameSecondary)
                .Field(ff => ff.CategoryNameTertiary)
                .Field(ff => ff.SuggestedByCategory)
                .Field(ff => ff.SuggestedByBadges)
                ;
        }
    }
}
