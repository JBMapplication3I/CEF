// <copyright file="Sort.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sort class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A sort.</summary>
    public class Sort
    {
        /// <summary>Gets or sets the field.</summary>
        /// <value>The field.</value>
        [JsonProperty("field"), DataMember(Name = "field"),
         ApiMember(Name = "field", DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? Field { get; set; }

        /// <summary>Gets or sets the field.</summary>
        /// <value>The field.</value>
        [JsonProperty("serverField"), DataMember(Name = "serverField"),
         ApiMember(Name = "serverField", DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? ServerField { get; set; }

        /// <summary>Gets or sets the order in which sorts are applied.</summary>
        /// <value>The order.</value>
        [JsonProperty("order"), DataMember(Name = "order"),
         ApiMember(Name = "order", DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? Order { get; set; }

        /// <summary>Gets or sets the direction.</summary>
        /// <value>The direction.</value>
        [JsonProperty("dir"), DataMember(Name = "dir"),
         ApiMember(Name = "dir", DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? Dir { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{{ Field: {Field}, Order: {Order}, Dir: {Dir} }}";
        }
    }
}
