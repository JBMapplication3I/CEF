// <copyright file="MonkeyContact.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the monkey contact class</summary>
namespace Clarity.Ecommerce.Providers.Surveys.SurveyMonkey
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A monkey contact.</summary>
    [JetBrains.Annotations.PublicAPI]
    public class MonkeyContact
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [DataMember(Name = "email"), JsonProperty("email"), ApiMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>Gets or sets the person's first name.</summary>
        /// <value>The name of the first.</value>
        [DataMember(Name = "first_name"), JsonProperty("first_name"), ApiMember(Name = "first_name")]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the person's last name.</summary>
        /// <value>The name of the last.</value>
        [DataMember(Name = "last_name"), JsonProperty("last_name"), ApiMember(Name = "last_name")]
        public string LastName { get; set; }
    }
}
