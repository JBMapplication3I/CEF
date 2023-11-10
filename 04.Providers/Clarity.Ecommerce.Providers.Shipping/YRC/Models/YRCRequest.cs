// <copyright file="YRCRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCRequest class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc request.</summary>
    public class YRCRequest
    {
        /// <summary>Gets or sets the login.</summary>
        /// <value>The login.</value>
        [JsonProperty("login")]
        public YRCRequestLogin? Login { get; set; }

        /// <summary>Gets or sets the details.</summary>
        /// <value>The details.</value>
        [JsonProperty("details")]
        public YRCRequestDetails? Details { get; set; }

        /// <summary>Gets or sets the origin location.</summary>
        /// <value>The origin location.</value>
        [JsonProperty("originLocation")]
        public YRCRequestOriginLocation? OriginLocation { get; set; }

        /// <summary>Gets or sets destination location.</summary>
        /// <value>The destination location.</value>
        [JsonProperty("destinationLocation")]
        public YRCRequestDestinationLocation? DestinationLocation { get; set; }

        /// <summary>Gets or sets the list of commodities.</summary>
        /// <value>The list of commodities.</value>
        [JsonProperty("listOfCommodities")]
        public YRCRequestListOfCommodities? ListOfCommodities { get; set; }

        /// <summary>Gets or sets options for controlling the service.</summary>
        /// <value>Options that control the service.</value>
        [JsonProperty("serviceOpts")]
        public YRCRequestServiceOpts? ServiceOpts { get; set; }
    }
}
