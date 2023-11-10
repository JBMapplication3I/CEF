// <copyright file="AggregateTree.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the aggregate tree class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System.Collections.Generic;

    /// <summary>An aggregate tree.</summary>
    public class AggregateTree
    {
        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        public string? Key { get; set; }

        /// <summary>Gets or sets the number of documents.</summary>
        /// <value>The number of documents.</value>
        public long? DocCount { get; set; }

        /// <summary>Gets or sets the document count error upper bound.</summary>
        /// <value>The document count error upper bound.</value>
        public long? DocCountErrorUpperBound { get; set; }

        /// <summary>Gets or sets a value indicating whether this TreeAggregate has children.</summary>
        /// <value>True if this TreeAggregate has children, false if not.</value>
        public bool HasChildren { get; set; }

        /// <summary>Gets or sets the children.</summary>
        /// <value>The children.</value>
        public List<AggregateTree>? Children { get; set; }

        // This will only get used at the top level

        /// <summary>Gets or sets the number of sum other documents.</summary>
        /// <value>The total number of other document count.</value>
        public long? SumOtherDocCount { get; set; }

        /// <summary>Gets or sets the number of original documents.</summary>
        /// <value>The number of original documents.</value>
        public long? OriginalDocCount { get; set; }

        /// <summary>Gets or sets the number of merged documents.</summary>
        /// <value>The number of merged documents.</value>
        public long? MergedDocCount { get; set; }

        /// <summary>Gets or sets the number of suspected this level documents.</summary>
        /// <value>The number of suspected this level documents.</value>
        public long? SuspectedThisLevelDocCount { get; set; }
    }
}
