// <copyright file="ElasticSearchingProviderConfig.Mods.RatingRanges.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System.ComponentModel;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using JSConfigs;

    /// <summary>An elastic searching provider configuration.</summary>
    internal static partial class ElasticSearchingProviderConfig
    {
        /// <summary>Gets a value indicating whether the searching rating ranges is enabled.</summary>
        /// <value>True if searching rating ranges enabled, false if not.</value>
        [AppSettingsKey("Clarity.Searching.RatingRanges.Enabled"),
            DefaultValue(true)]
        internal static bool SearchingRatingRangesEnabled
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, typeof(ElasticSearchingProviderConfig)) || asValue;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching rating ranges increments.</summary>
        /// <value>The searching rating ranges increments.</value>
        [AppSettingsKey("Clarity.Searching.RatingRanges.Increments"),
            DefaultValue("[{\"From\":0,\"To\":1},{\"From\":1,\"To\":2},{\"From\":2,\"To\":3},{\"From\":3,\"To\":4},{\"From\":4,\"To\":5}]"),
            DependsOn(nameof(SearchingRatingRangesEnabled))]
        internal static RatingsRange[]? SearchingRatingRangesIncrements
        {
            get => SearchingRatingRangesEnabled
                ? CEFConfigDictionary.TryGet(out RatingsRange[] asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : new[] { new RatingsRange(0, 1), new RatingsRange(1, 2), new RatingsRange(2, 3), new RatingsRange(3, 4), new RatingsRange(4, 5) }
                : null;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>Gets the searching boosts rating ranges.</summary>
        /// <value>The searching boosts rating ranges.</value>
        [AppSettingsKey("Clarity.Searching.Boosts.RatingRanges"),
            DefaultValue(110d),
            DependsOn(nameof(SearchingRatingRangesEnabled))]
        internal static double SearchingBoostsRatingRanges
        {
            get => SearchingRatingRangesEnabled
                ? CEFConfigDictionary.TryGet(out double asValue, typeof(ElasticSearchingProviderConfig))
                    ? asValue
                    : 110d
                : 0d;
            private set => CEFConfigDictionary.TrySet(value, typeof(ElasticSearchingProviderConfig));
        }

        /// <summary>The ratings range.</summary>
        [PublicAPI]
        public class RatingsRange
        {
            /// <summary>Initializes a new instance of the <see cref="RatingsRange"/> class.</summary>
            public RatingsRange()
            {
            }

            /// <summary>Initializes a new instance of the <see cref="RatingsRange"/> class.</summary>
            /// <param name="from">The minimum value for this range.</param>
            /// <param name="to">  The maximum value for this range.</param>
            public RatingsRange(double from, double to)
            {
                From = from;
                To = to;
            }

            /// <summary>Gets or sets the minimum value for this range.</summary>
            /// <value>The minimum value for this range.</value>
            public double From { get; set; }

            /// <summary>Gets or sets the maximum value for this range.</summary>
            /// <value>The maximum value for this range.</value>
            public double To { get; set; }

            /// <inheritdoc/>
            public override string ToString()
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(
                    this,
                    SerializableAttributesDictionaryExtensions.JsonSettings);
            }
        }
    }
}
