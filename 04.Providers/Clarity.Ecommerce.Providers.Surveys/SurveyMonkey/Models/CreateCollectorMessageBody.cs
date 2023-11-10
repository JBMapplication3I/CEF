// <copyright file="CreateCollectorMessageBody.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create collector message body class</summary>
namespace Clarity.Ecommerce.Providers.Surveys.SurveyMonkey
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A create collector message body.</summary>
    [JetBrains.Annotations.PublicAPI]
    public class CreateCollectorMessageBody
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [DataMember(Name = "type"), JsonProperty("type"), ApiMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>Gets or sets the subject.</summary>
        /// <value>The subject.</value>
        [DataMember(Name = "subject"), JsonProperty("subject"), ApiMember(Name = "subject")]
        public string Subject { get; set; }

        /// <summary>Gets or sets the body HTML.</summary>
        /// <value>The body HTML.</value>
        [DataMember(Name = "body_html"), JsonProperty("body_html"), ApiMember(Name = "body_html")]
        public string BodyHTML { get; set; }

        /// <summary>Gets or sets a value indicating whether a branding is enabled.</summary>
        /// <value>True if a branding is enabled, false if not.</value>
        [DataMember(Name = "is_branding_enabled"), JsonProperty("is_branding_enabled"), ApiMember(Name = "is_branding_enabled")]
        public bool IsBrandingEnabled { get; set; }
    }
}
