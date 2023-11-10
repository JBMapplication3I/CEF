// <copyright file="GetSurveyPagesResponseLinks.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get survey pages response links class</summary>
namespace Clarity.Ecommerce.Providers.Surveys.SurveyMonkey
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A get survey pages response links.</summary>
    [JetBrains.Annotations.PublicAPI]
    public class GetSurveyPagesResponseLinks
    {
        /// <summary>Gets or sets the self.</summary>
        /// <value>The self.</value>
        [DataMember(Name = "self"), JsonProperty("self"), ApiMember(Name = "self")]
        public string Self { get; set; }
    }
}
