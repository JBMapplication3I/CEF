// <copyright file="SearchViewModelBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SearchViewModelBase class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using Newtonsoft.Json;

    /// <summary>A search view model base.</summary>
    /// <typeparam name="TSearchForm">Type of the search form.</typeparam>
    /// <typeparam name="TIndexModel">Type of the index model.</typeparam>
    /// <seealso cref="SearchViewModelBase"/>
    [PublicAPI]
    public abstract class SearchViewModelBase<TSearchForm, TIndexModel>
        : SearchViewModelBase
        where TSearchForm : SearchFormBase, new()
        where TIndexModel : IndexableModelBase
    {
        /// <summary>Gets or sets the form.</summary>
        /// <value>The form.</value>
        public TSearchForm? Form { get; set; } = new();

        /// <summary>Gets or sets the documents.</summary>
        /// <value>The documents.</value>
        public IEnumerable<TIndexModel>? Documents { get; set; } = Enumerable.Empty<TIndexModel>();
    }

    /// <summary>A search view model base.</summary>
    [PublicAPI]
    public abstract class SearchViewModelBase
    {
        /// <summary>The attributes.</summary>
        private List<KeyValuePair<string, List<KeyValuePair<string, long?>>>>? attributes = new();

        /// <summary>Dictionary of attributes.</summary>
        private Dictionary<string, Dictionary<string, long?>>? attributesDict = new();

        /// <summary>Gets or sets the elapsed milliseconds.</summary>
        /// <value>The elapsed milliseconds.</value>
        public long ElapsedMilliseconds { get; set; }

        /// <summary>Gets or sets the number of. </summary>
        /// <value>The total.</value>
        public long Total { get; set; }

        /// <summary>Gets or sets the total number of pages.</summary>
        /// <value>The total number of pages.</value>
        public int TotalPages { get; set; }

        /// <summary>Gets or sets the server error.</summary>
        /// <value>The server error.</value>
        public object? ServerError { get; set; }

        /// <summary>Gets or sets information describing the debug.</summary>
        /// <value>Information describing the debug.</value>
        public string? DebugInformation { get; set; }

        /// <summary>Gets or sets a value indicating whether this SearchViewModelBase is valid.</summary>
        /// <value>True if this SearchViewModelBase is valid, false if not.</value>
        public bool IsValid { get; set; }

        /// <summary>Gets or sets the hits meta data maximum score.</summary>
        /// <value>The hits meta data maximum score.</value>
        public double HitsMetaDataMaxScore { get; set; }

        /// <summary>Gets or sets the hits meta data total.</summary>
        /// <value>The hits meta data total.</value>
        public long HitsMetaDataTotal { get; set; }

        /// <summary>Gets or sets the hits meta data hit scores.</summary>
        /// <value>The hits meta data hit scores.</value>
        public Dictionary<string, double?>? HitsMetaDataHitScores { get; set; }

        /// <summary>Gets or sets the attributes as a dictionary for the query. The serialized version is not a dictionary.</summary>
        /// <value>The attributes.</value>
        [JsonIgnore]
        public Dictionary<string, Dictionary<string, long?>>? AttributesDict
        {
            get => attributesDict;

            set
            {
                attributesDict = value;
                attributes = null; // Force recalculate on next read
            }
        }

        /// <summary>Gets or sets the attributes.</summary>
        /// <value>The attributes.</value>
        public List<KeyValuePair<string, List<KeyValuePair<string, long?>>>>? Attributes
        {
            get
            {
                if (attributes == null && attributesDict != null)
                {
                    attributes = attributesDict
                        .ToDictionary(x => x.Key, x => x.Value.ToList())
                        .ToList();
                }
                return attributes;
            }

            set
            {
                attributes = value;
                attributesDict = attributes
                    ?.ToDictionary(
                        x => x.Key,
                        x => x.Value
                            .ToDictionary(
                                y => y.Key,
                                y => y.Value));
            }
        }

        /// <summary>Gets or sets the brand IDs.</summary>
        /// <value>The brand IDs.</value>
        public Dictionary<string, long?>? BrandIDs { get; set; } = new();

        /// <summary>Gets or sets the brand names.</summary>
        /// <value>The brand names.</value>
        public Dictionary<string, long?>? BrandNames { get; set; } = new();

        /// <summary>Gets or sets the categories tree.</summary>
        /// <value>The categories tree.</value>
        public AggregateTree? CategoriesTree { get; set; } = new();

        /// <summary>Gets or sets the district IDs.</summary>
        /// <value>The district IDs.</value>
        public Dictionary<string, IndexableDistrictModel>? Districts { get; set; } = new();

        /// <summary>Gets or sets the franchise IDs.</summary>
        /// <value>The franchise IDs.</value>
        public Dictionary<string, long?>? FranchiseIDs { get; set; } = new();

        /// <summary>Gets or sets the product IDs.</summary>
        /// <value>The product IDs.</value>
        public Dictionary<string, long?>? ProductIDs { get; set; } = new();

        /// <summary>Gets or sets the manufacturer IDs.</summary>
        /// <value>The manufacturer IDs.</value>
        public Dictionary<string, long?>? ManufacturerIDs { get; set; } = new();

        /// <summary>Gets or sets the region IDs.</summary>
        /// <value>The reegion IDs.</value>
        public Dictionary<string, IndexableRegionModel>? Regions { get; set; } = new();

        /// <summary>Gets or sets the store IDs.</summary>
        /// <value>The store IDs.</value>
        public Dictionary<string, long?>? StoreIDs { get; set; } = new();

        /// <summary>Gets or sets the types.</summary>
        /// <value>The types.</value>
        public Dictionary<string, IndexableTypeModel>? Types { get; set; } = new();

        /// <summary>Gets or sets the vendor IDs.</summary>
        /// <value>The vendor IDs.</value>
        public Dictionary<string, long?>? VendorIDs { get; set; } = new();

        /// <summary>Gets or sets the pricing ranges.</summary>
        /// <value>The pricing ranges.</value>
        public List<AggregatePricingRange>? PricingRanges { get; set; } = new();

        /// <summary>Gets or sets the rating ranges.</summary>
        /// <value>The rating ranges.</value>
        public List<AggregateRatingRange>? RatingRanges { get; set; } = new();

        /// <summary>Gets or sets the result IDs.</summary>
        /// <value>The result IDs.</value>
        public List<int> ResultIDs { get; set; } = new();

        /// <summary>Gets or sets the cities.</summary>
        /// <value>The cities.</value>
        public Dictionary<string, long?>? Cities { get; set; }

        /// <summary>Gets or sets the names.</summary>
        /// <value>The cities.</value>
        public Dictionary<string, long?>? Names { get; set; }
    }
}
