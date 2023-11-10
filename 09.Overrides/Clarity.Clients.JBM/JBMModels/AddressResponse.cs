// <copyright file="AddressResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the UOMResponse class</summary>

namespace Clarity.Clients.JBM
{
    using Newtonsoft.Json;

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class JBMAddressResponse
    {
        [JsonProperty("PartyId")]
        public long? PartyId { get; set; }

        [JsonProperty("PartyNumber")]
        public string? PartyNumber { get; set; }

        [JsonProperty("AddressId")]
        public long? AddressId { get; set; }

        [JsonProperty("AddressNumber")]
        public string? AddressNumber { get; set; }

        [JsonProperty("CreatedByModule")]
        public string? CreatedByModule { get; set; }

        [JsonProperty("FormattedAddress")]
        public string? FormattedAddress { get; set; }

        [JsonProperty("FormattedMultilineAddress")]
        public string? FormattedMultilineAddress { get; set; }

        [JsonProperty("City")]
        public string? City { get; set; }

        [JsonProperty("Country")]
        public string? Country { get; set; }

        [JsonProperty("State")]
        public string? State { get; set; }
    }
}