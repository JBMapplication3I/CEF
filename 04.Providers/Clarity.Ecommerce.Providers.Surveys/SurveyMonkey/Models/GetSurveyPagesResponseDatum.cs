// <copyright file="GetSurveyPagesResponseDatum.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get survey pages response datum class</summary>
namespace Clarity.Ecommerce.Providers.Surveys.SurveyMonkey
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A get survey pages response datum.</summary>
    [JetBrains.Annotations.PublicAPI]
    public class GetSurveyPagesResponseDatum
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [DataMember(Name = "id"), JsonProperty("id"), ApiMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>Gets or sets the position.</summary>
        /// <value>The position.</value>
        [DataMember(Name = "position"), JsonProperty("position"), ApiMember(Name = "position")]
        public int Position { get; set; }

        /// <summary>Gets or sets the hRef.</summary>
        /// <value>The hRef.</value>
        [DataMember(Name = "href"), JsonProperty("href"), ApiMember(Name = "href")]
        public string Href { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [DataMember(Name = "description"), JsonProperty("description"), ApiMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        [DataMember(Name = "title"), JsonProperty("title"), ApiMember(Name = "title")]
        public string Title { get; set; }
    }
}
