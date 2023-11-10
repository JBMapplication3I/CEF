// <copyright file="FetchResult.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the fetch result class</summary>
namespace Avalara.AvaTax.RestClient
{
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>Represents a result set fetched from AvaTax.</summary>
    /// <typeparam name="T">Type param.</typeparam>
    public class FetchResult<T>
    {
        /// <summary>The number of rows returned by your query, prior to pagination.</summary>
        /// <value>The count.</value>
        [JsonProperty("@recordsetCount")]
        public int count { get; set; }

        /// <summary>The paginated and filtered list of records matching the parameters you supplied.</summary>
        /// <value>The value.</value>
        public List<T> value { get; set; }

        /// <summary>The link to the next page of results.</summary>
        /// <value>The next link.</value>
        [JsonProperty("@nextLink")]
        public string? nextLink { get; set; }

        /// <summary>Simple Constructor.</summary>
        public FetchResult()
        {
            value = new();
            count = 0;
        }

        /// <summary>Construct this from a different FetchResult, but maintain the count.</summary>
        /// <param name="originalRowCount">Number of original rows.</param>
        /// <param name="newList">         The new list.</param>
        public FetchResult(int originalRowCount, List<T> newList)
        {
            count = originalRowCount;
            value = newList;
        }

        /// <summary>Converts the result set to a printable text object.</summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class FetchResult {\n");
            sb.Append("  count: ").Append(count).Append('\n');
            sb.Append("  value: [").Append('\n');
            foreach (var obj in value)
            {
                sb.Append("    ").Append(obj).Append('\n');
            }
            sb.Append("  ]").Append('\n');
            sb.Append("  @nextLink: ").Append(nextLink).Append('\n');
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
