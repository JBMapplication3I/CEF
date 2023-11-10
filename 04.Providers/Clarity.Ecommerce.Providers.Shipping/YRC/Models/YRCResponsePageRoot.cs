// <copyright file="YRCResponsePageRoot.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponsePageRoot class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response page root.</summary>
    [JsonObject("pageRoot")]
    public class YRCResponsePageRoot
    {
        /// <summary>Gets or sets the context.</summary>
        /// <value>The context.</value>
        [JsonProperty("context")]
        public string? Context { get; set; }

        /// <summary>Gets or sets the identifier of the program.</summary>
        /// <value>The identifier of the program.</value>
        [JsonProperty("programId")]
        public string? ProgramId { get; set; }

        /// <summary>Gets or sets the type of the query.</summary>
        /// <value>The type of the query.</value>
        [JsonProperty("queryType")]
        public string? QueryType { get; set; }

        /// <summary>Gets or sets the date time.</summary>
        /// <value>The date time.</value>
        [JsonProperty("dateTime")]
        public string? DateTime { get; set; }

        /// <summary>Gets or sets the return text.</summary>
        /// <value>The return text.</value>
        [JsonProperty("returnText")]
        public string? ReturnText { get; set; }

        /// <summary>Gets or sets the number of records.</summary>
        /// <value>The number of records.</value>
        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }

        /// <summary>Gets or sets the record offset.</summary>
        /// <value>The record offset.</value>
        [JsonProperty("recordOffset")]
        public int RecordOffset { get; set; }

        /// <summary>Gets or sets the maximum records.</summary>
        /// <value>The maximum records.</value>
        [JsonProperty("maxRecords")]
        public int MaxRecords { get; set; }

        /// <summary>Gets or sets the page head.</summary>
        /// <value>The page head.</value>
        [JsonProperty("pageHead")]
        public YRCResponsePageHead? PageHead { get; set; }

        /// <summary>Gets or sets the body head.</summary>
        /// <value>The body head.</value>
        [JsonProperty("bodyHead")]
        public YRCResponseBodyHead? BodyHead { get; set; }

        /// <summary>Gets or sets the body main.</summary>
        /// <value>The body main.</value>
        [JsonProperty("bodyMain")]
        public YRCResponseBodyMain? BodyMain { get; set; }
    }
}
