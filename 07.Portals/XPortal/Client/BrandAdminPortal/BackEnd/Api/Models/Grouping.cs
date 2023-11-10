// <copyright file="Grouping.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the grouping class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A group.</summary>
    public class Grouping
    {
        /// <summary>Gets or sets the field.</summary>
        /// <value>The field.</value>
        [JsonProperty("field"),
            DataMember(Name = "field"),
            ApiMember(Name = "field", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string Field { get; set; } = "ID";

        /// <summary>Gets or sets the order in which Groups are applied.</summary>
        /// <value>The order.</value>
        [JsonProperty("order"),
            DataMember(Name = "order"),
            ApiMember(Name = "order", DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? Order { get; set; }

        /// <summary>Gets or sets the order in which Groups are applied.</summary>
        /// <value>The order.</value>
        [JsonProperty("sortOrder"),
            DataMember(Name = "sortOrder"),
            ApiMember(Name = "sortOrder", DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? SortOrder { get; set; }

        /// <summary>Gets or sets the direction.</summary>
        /// <value>The direction.</value>
        [JsonProperty("dir"),
            DataMember(Name = "dir"),
            ApiMember(Name = "dir", DataType = "string", ParameterType = "query", IsRequired = false)]
        public string Dir { get; set; } = "asc";

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{{ Field: {Field}, Order: {Order}, SortOrder: {SortOrder}, Dir: {Dir} }}";
        }
    }
}
