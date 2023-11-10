// <copyright file="AddRecipientsBulkBody.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the add recipients bulk body class</summary>
namespace Clarity.Ecommerce.Providers.Surveys.SurveyMonkey
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>An add recipients bulk body.</summary>
    public class AddRecipientsBulkBody
    {
        /// <summary>Gets or sets the contacts.</summary>
        /// <value>The contacts.</value>
        [DataMember(Name = "contacts"), JsonProperty("contacts"), ApiMember(Name = "contacts")]
        public MonkeyContact[] Contacts { get; set; }
    }
}
