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
        /// <summary>(Immutable) The map manufacturer suggest model.</summary>
        private static readonly Func<SuggestOption<ManufacturerIndexableModel>?, ManufacturerSuggestResult?> MapManufacturerSuggestModel
            = suggest => suggest?.Source == null ? null : new()
            {
                ID = suggest.Source.ID,
                Name = suggest.Source.Name,
                CustomKey = suggest.Source.CustomKey,
                Score = suggest.Score,
                QueryableAttributes = Contract.CheckValidKey(
                        ElasticSearchingProviderConfig.SearchingManufacturerIndexSuggestOptionAttributeKeys)
                    ? suggest.Source.QueryableAttributes?.ToDictionary(x => x.Key!, x => x.Value!)
                    : null,
            };

        /// <summary>Gets or sets the event type identifier for manufacturer catalog search.</summary>
        /// <value>The event type identifier for manufacturer catalog search.</value>
        private static int EventTypeIDForManufacturerCatalogSearch { get; set; }

        /// <summary>Gets the manufacturer suggest module.</summary>
        /// <value>The manufacturer suggest module.</value>
        private static ManufacturerSuggestModule ManufacturerSuggestModule { get; }
            = new(ManufacturerSuggestFields, MapManufacturerSuggestModel);

        /// <summary>Manufacturers suggest fields.</summary>
        /// <typeparam name="TIndexModel">Type of the index model.</typeparam>
        /// <param name="f">The FieldsDescriptor{TIndexModel} to process.</param>
        /// <returns>An IPromise{Fields}.</returns>
        private static IPromise<Fields> ManufacturerSuggestFields<TIndexModel>(FieldsDescriptor<TIndexModel> f)
            where TIndexModel : ManufacturerIndexableModel
        {
            return f
                .Field(ff => ff.ID)
                .Field(ff => ff.Name)
                .Field(ff => ff.CustomKey)
                .Field(ff => ff.SuggestedByName)
                .Field(ff => ff.SuggestedByCustomKey)
                .Field(ff => ff.SuggestedByQueryableSerializableAttributes)
                ;
        }
    }
}
