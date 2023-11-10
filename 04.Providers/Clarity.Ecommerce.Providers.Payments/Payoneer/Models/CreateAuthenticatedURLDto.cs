// <copyright file="CreateAuthenticatedURLDto.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create authenticated URL dto class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>A create authenticated URL data transfer object.</summary>
    [DataContract]
    internal class CreateAuthenticatedURLDto
    {
        /// <summary>Gets or sets URI of the document.</summary>
        /// <value>The URI.</value>
        [JsonProperty("uri")]
        [DataMember(Name = "uri")]
        public string? Uri { get; set; } // "/accounts/290465/bankaccounts"

        /// <summary>Gets or sets the action.</summary>
        /// <value>The action.</value>
        [JsonProperty("action")]
        [DataMember(Name = "action")]
        public string? Action { get; set; } // "create", "view"
    }
}
