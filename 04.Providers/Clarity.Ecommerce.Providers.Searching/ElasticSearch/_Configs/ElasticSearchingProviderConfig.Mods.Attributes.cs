// <copyright file="ElasticSearchingProviderConfig.Mods.Attributes.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System.ComponentModel;
    using JSConfigs;

    /// <summary>An elastic searching provider configuration.</summary>
    internal static partial class ElasticSearchingProviderConfig
    {
        /// <summary>Gets the searching boosts all attributes.</summary>
        /// <value>The searching boosts all attributes.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AllAttributes"),
            DefaultValue(110)]
        internal static double SearchingBoostsAllAttributes
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts any attributes.</summary>
        /// <value>The searching boosts any attributes.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.AnyAttributes"),
            DefaultValue(110)]
        internal static double SearchingBoostsAnyAttributes
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 110;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts queryable serializable attribute value match keyword.</summary>
        /// <value>The searching boosts queryable serializable attribute value match keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.QueryableSerializableAttributeValue.Match.Keyword"),
            DefaultValue(500)]
        internal static double SearchingBoostsQueryableSerializableAttributeValueMatchKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 500;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts queryable serializable attribute value prefix keyword.</summary>
        /// <value>The searching boosts queryable serializable attribute value prefix keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.QueryableSerializableAttributeValue.Prefix.Keyword"),
            DefaultValue(250)]
        internal static double SearchingBoostsQueryableSerializableAttributeValuePrefixKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 250;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts queryable serializable attribute value term keyword.</summary>
        /// <value>The searching boosts queryable serializable attribute value term keyword.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.QueryableSerializableAttributeValue.Term.Keyword"),
            DefaultValue(250)]
        internal static double SearchingBoostsQueryableSerializableAttributeValueTermKeyword
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 250;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts seo keywords keyword function score factor.</summary>
        /// <value>The searching boosts seo keywords keyword function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SerializableAttributeValues.Keyword.FunctionScore.Factor"),
            DefaultValue(125)]
        internal static double SearchingBoostsSerializableAttributeValuesKeywordFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts seo keywords function score factor.</summary>
        /// <value>The searching boosts seo keywords function score factor.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.SerializableAttributeValues.FunctionScore.Factor"),
            DefaultValue(125)]
        internal static double SearchingBoostsSerializableAttributeValuesFunctionScoreFactor
        {
            get => CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                ? asValue
                : 125;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }
    }
}
