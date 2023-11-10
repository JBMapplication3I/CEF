// <copyright file="GetSurveyPagesResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get survey pages response class</summary>
namespace Clarity.Ecommerce.Providers.Surveys.SurveyMonkey
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A get survey pages response.</summary>
    [JetBrains.Annotations.PublicAPI]
    public class GetSurveyPagesResponse
    {
        /// <summary>Gets or sets the per page.</summary>
        /// <value>The per page.</value>
        [DataMember(Name = "per_page"), JsonProperty("per_page"), ApiMember(Name = "per_page")]
        public int PerPage { get; set; }

        /// <summary>Gets or sets the number of. </summary>
        /// <value>The total.</value>
        [DataMember(Name = "total"), JsonProperty("total"), ApiMember(Name = "total")]
        public int Total { get; set; }

        /// <summary>Gets or sets the data.</summary>
        /// <value>The data.</value>
        [DataMember(Name = "data"), JsonProperty("data"), ApiMember(Name = "data")]
        public GetSurveyPagesResponseDatum[] Data { get; set; }

        /// <summary>Gets or sets the page.</summary>
        /// <value>The page.</value>
        [DataMember(Name = "page"), JsonProperty("page"), ApiMember(Name = "page")]
        public int Page { get; set; }

        /// <summary>Gets or sets the links.</summary>
        /// <value>The links.</value>
        [DataMember(Name = "links"), JsonProperty("links"), ApiMember(Name = "links")]
        public GetSurveyPagesResponseLinks Links { get; set; }
    }
}
