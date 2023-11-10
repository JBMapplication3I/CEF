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
        /// <summary>(Immutable) The map product suggest model.</summary>
        private static readonly Func<SuggestOption<ProductIndexableModel>?, ProductSuggestResult?> MapProductSuggestModel
            = suggest => suggest?.Source == null ? null : new()
            {
                ID = suggest.Source.ID,
                Name = suggest.Source.Name,
                CustomKey = suggest.Source.CustomKey,
                SeoUrl = suggest.Source.SeoUrl,
                Score = suggest.Score,
                QueryableAttributes = Contract.CheckValidKey(ElasticSearchingProviderConfig.SearchingStoreIndexSuggestOptionAttributeKeys)
                    ? suggest.Source.QueryableAttributes?.ToDictionary(x => x.Key!, x => x.Value!)
                    : null,
                BrandName = suggest.Source.BrandName,
                ManufacturerPartNumber = suggest.Source.ManufacturerPartNumber,
                TotalPurchasedQuantity = suggest.Source.TotalPurchasedQuantity,
            };

        /// <summary>Gets or sets the event type identifier for product catalog search.</summary>
        /// <value>The event type identifier for product catalog search.</value>
        private static int EventTypeIDForProductCatalogSearch { get; set; }

        /// <summary>Gets or sets the event status identifier for normal.</summary>
        /// <value>The event status identifier for normal.</value>
        private static int EventStatusIDForNormal { get; set; }

        /// <summary>Gets the product suggest module.</summary>
        /// <value>The product suggest module.</value>
        private static ProductSuggestModule ProductSuggestModule { get; }
            = new(ProductSuggestFields, MapProductSuggestModel);

        /// <summary>Product suggest fields.</summary>
        /// <typeparam name="TIndexModel">Type of the index model.</typeparam>
        /// <param name="f">The FieldsDescriptor{TIndexModel} to process.</param>
        /// <returns>An IPromise{Fields}.</returns>
        private static IPromise<Fields> ProductSuggestFields<TIndexModel>(FieldsDescriptor<TIndexModel> f)
            where TIndexModel : ProductIndexableModel
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
                .Field(ff => ff.BrandName)
                .Field(ff => ff.ManufacturerPartNumber)
                .Field(ff => ff.ShortDescription)
                .Field(ff => ff.SuggestedByBrandName)
                .Field(ff => ff.SuggestedByManufacturerPartNumber)
                .Field(ff => ff.SuggestedByShortDescription)
                ;
        }
    }
}
