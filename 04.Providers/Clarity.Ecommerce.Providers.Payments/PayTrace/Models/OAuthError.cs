// <copyright file="OAuthError.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace OAuthError class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>Class that holds Error for the OAuth token.</summary>
    public class OAuthError
    {
        /// <summary>Json key - returned by PayTrace API for error.</summary>
        /// <value>The error.</value>
        [DataMember(Name = "error"), JsonProperty("error"), ApiMember(Name = "error")]
        public string? Error { get; set; }

        /// <summary>Json key - returned by PayTrace API for error.</summary>
        /// <value>Information describing the error.</value>
        [DataMember(Name = "error_description"), JsonProperty("error_description"), ApiMember(Name = "error_description")]
        public string? ErrorDescription { get; set; }

        /// <summary>this property is user-defined and it has been used to store a http error.</summary>
        /// <value>The HTTP token error.</value>
        public string? HttpTokenError { get; set; }
    }
}
