// <copyright file="YRCResponseLineItem.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseLineItem class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response line item.</summary>
    [JsonObject("lineItem")]
    public class YRCResponseLineItem
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>Gets or sets the handling units.</summary>
        /// <value>The handling units.</value>
        [JsonProperty("handlingUnits")]
        public YRCResponseHandlingUnits? HandlingUnits { get; set; }

        /// <summary>Gets or sets the pieces.</summary>
        /// <value>The pieces.</value>
        public YRCResponsePieces? Pieces { get; set; }

        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        [JsonProperty("code")]
        public string? Code { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>Gets or sets the hazardous.</summary>
        /// <value>The hazardous.</value>
        [JsonProperty("hazardous")]
        public string? Hazardous { get; set; }

        /// <summary>Gets or sets the nmfc.</summary>
        /// <value>The nmfc.</value>
        [JsonProperty("nmfc")]
        public YRCResponseNMFC? Nmfc { get; set; }

        /// <summary>Gets or sets the weight.</summary>
        /// <value>The weight.</value>
        [JsonProperty("weight")]
        public int Weight { get; set; }

        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        [JsonProperty("quantity")]
        public string? Quantity { get; set; }

        /// <summary>Gets or sets the density.</summary>
        /// <value>The density.</value>
        [JsonProperty("density")]
        public string? Density { get; set; }

        /// <summary>Gets or sets the type of the rated.</summary>
        /// <value>The type of the rated.</value>
        [JsonProperty("ratedType")]
        public string? RatedType { get; set; }
    }
}
