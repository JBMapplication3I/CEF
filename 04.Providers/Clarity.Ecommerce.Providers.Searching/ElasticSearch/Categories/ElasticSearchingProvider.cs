// <copyright file="ElasticSearchingProvider.cs" company="clarity-ventures.com">
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
        /// <summary>(Immutable) The map category suggest model.</summary>
        private static readonly Func<SuggestOption<CategoryIndexableModel>?, CategorySuggestResult?> MapCategorySuggestModel
            = suggest => suggest?.Source == null ? null : new()
            {
                ID = suggest.Source.ID,
                Name = suggest.Source.Name,
                CustomKey = suggest.Source.CustomKey,
                SeoUrl = suggest.Source.SeoUrl,
                Score = suggest.Score,
                QueryableAttributes = Contract.CheckValidKey(ElasticSearchingProviderConfig.SearchingCategoryIndexSuggestOptionAttributeKeys)
                    ? suggest.Source.QueryableAttributes?.ToDictionary(x => x.Key!, x => x.Value!)
                    : null,
            };

        /// <summary>Gets or sets the event type identifier for category catalog search.</summary>
        /// <value>The event type identifier for category catalog search.</value>
        private static int EventTypeIDForCategoryCatalogSearch { get; set; }

        /// <summary>Gets the category suggest module.</summary>
        /// <value>The category suggest module.</value>
        private static CategorySuggestModule CategorySuggestModule { get; }
            = new(CategorySuggestFields, MapCategorySuggestModel);

        /// <summary>Category suggest fields.</summary>
        /// <typeparam name="TIndexModel">Type of the index model.</typeparam>
        /// <param name="f">The FieldsDescriptor{TIndexModel} to process.</param>
        /// <returns>An IPromise{Fields}.</returns>
        private static IPromise<Fields> CategorySuggestFields<TIndexModel>(FieldsDescriptor<TIndexModel> f)
            where TIndexModel : CategoryIndexableModel
        {
            return f
                .Field(ff => ff.ID)
                .Field(ff => ff.Name)
                .Field(ff => ff.CustomKey)
                .Field(ff => ff.SeoUrl)
                .Field(ff => ff.SuggestedByName)
                .Field(ff => ff.SuggestedByCustomKey)
                .Field(ff => ff.SuggestedByQueryableSerializableAttributes)
                ;
        }
    }
}
