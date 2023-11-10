// <copyright file="YRCRequestDetails.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCRequestDetails class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc request details.</summary>
    [JsonObject("details")]
    public class YRCRequestDetails
    {
        /// <summary>Gets or sets the service class.</summary>
        /// <value>The service class.</value>
        [JsonProperty("serviceClass")]
        public string? ServiceClass { get; set; }

        /// <summary>Gets or sets the type query.</summary>
        /// <value>The type query.</value>
        [JsonProperty("typeQuery")]
        public string? TypeQuery { get; set; }

        /// <summary>Gets or sets the pickup date.</summary>
        /// <value>The pickup date.</value>
        [JsonProperty("pickupDate")]
        public string? PickupDate { get; set; } // Format is yyyyMMdd
    }
}
