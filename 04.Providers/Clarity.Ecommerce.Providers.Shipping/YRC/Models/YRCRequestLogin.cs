// <copyright file="YRCRequestLogin.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCRequestLogin class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc request login.</summary>
    [JsonObject("login")]
    public class YRCRequestLogin
    {
        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        [JsonProperty("username")]
        public string? Username { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        [JsonProperty("password")]
        public string? Password { get; set; }

        /// <summary>Gets or sets the identifier of the bus.</summary>
        /// <value>The identifier of the bus.</value>
        [JsonProperty("busId")]
        public string? BusId { get; set; }

        /// <summary>Gets or sets the bus role.</summary>
        /// <value>The bus role.</value>
        [JsonProperty("busRole")]
        public string? BusRole { get; set; }

        /// <summary>Gets or sets the payment terms.</summary>
        /// <value>The payment terms.</value>
        [JsonProperty("paymentTerms")]
        public string? PaymentTerms { get; set; }
    }
}
