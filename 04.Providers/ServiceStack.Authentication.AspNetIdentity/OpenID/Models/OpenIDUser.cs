// <copyright file="OpenIDUser.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the OpenID User class</summary>
namespace ServiceStack.Auth
{
    using System;
    using Newtonsoft.Json;

    /// <summary>An OpenID user.</summary>
    public class OpenIDUser
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [JsonProperty("sub")]
        public string? Id { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        [JsonProperty("preferred_username")]
        public string? Username { get; set; }

        /// <summary>Gets or sets the name of the full.</summary>
        /// <value>The name of the full.</value>
        [JsonProperty("name")]
        public string? FullName { get; set; }

        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
        [JsonProperty("given_name")]
        public string? FirstName { get; set; }

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
        [JsonProperty("family_name")]
        public string? LastName { get; set; }

        /// <summary>Gets or sets the Date/Time of the updated at.</summary>
        /// <value>The updated at.</value>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>Gets or sets the error.</summary>
        /// <value>The error.</value>
        [JsonProperty("error")]
        public string? Error { get; set; }
    }
}
