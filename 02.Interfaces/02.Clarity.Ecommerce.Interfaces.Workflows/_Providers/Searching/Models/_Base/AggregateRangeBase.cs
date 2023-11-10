// <copyright file="AggregateRangeBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the aggregate range base class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using Models;

    /// <summary>An aggregate range base.</summary>
    public abstract class AggregateRangeBase
    {
        /// <summary>Gets or sets the minimum.</summary>
        /// <value>The from.</value>
        public double? From { get; set; }

        /// <summary>Gets or sets the maximum.</summary>
        /// <value>The to.</value>
        public double? To { get; set; }

        /// <summary>Gets or sets the label.</summary>
        /// <value>The label.</value>
        public string? Label { get; set; }

        /// <summary>Gets or sets the number of documents.</summary>
        /// <value>The number of documents.</value>
        public long? DocCount { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                this,
                SerializableAttributesDictionaryExtensions.JsonSettings);
        }
    }
}
