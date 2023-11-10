// <copyright file="YRCResponseRequestedServiceType.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseRequestedServiceType class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response requested service type.</summary>
    public class YRCResponseRequestedServiceType
    {
        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        [JsonProperty("value")]
        public string? Value { get; set; }
    }
}
