// <copyright file="FusionResponseBase.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FusionResponseBase class</summary>

namespace Clarity.Clients.JBM
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class FusionResponseBase
    {
        [JsonProperty("count")]
        public int? Count { get; set; }

        [JsonProperty("hasMore")]
        public bool? HasMore { get; set; }

        [JsonProperty("limit")]
        public int? Limit { get; set; }

        [JsonProperty("offset")]
        public int? Offset { get; set; }

        [JsonProperty("links")]
        public List<Link>? Links { get; set; }
    }

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class Link
    {
        [JsonProperty("rel")]
        public string? Rel { get; set; }

        [JsonProperty("href")]
        public string? Href { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("kind")]
        public string? Kind { get; set; }

        [JsonProperty("properties")]
        public Properties? Properties { get; set; }
    }

    public class Properties
    {
        [JsonProperty("changeIndicator", NullValueHandling = NullValueHandling.Ignore)]
        public string? ChangeIndicator { get; set; }
    }
}
