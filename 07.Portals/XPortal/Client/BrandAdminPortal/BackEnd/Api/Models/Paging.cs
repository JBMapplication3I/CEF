// <copyright file="Paging.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the paging class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A paging.</summary>
    public class Paging
    {
        /// <summary>Gets or sets the size.</summary>
        /// <value>The size.</value>
        [JsonProperty("Size"),
            DataMember(Name = "Size"),
            ApiMember(Name = "Size", DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? Size { get; set; }

        /// <summary>Gets or sets the zero-based index of the start.</summary>
        /// <value>The start index.</value>
        [JsonProperty("StartIndex"),
            DataMember(Name = "StartIndex"),
            ApiMember(Name = "StartIndex", DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? StartIndex { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{{ Size: {Size}, StartIndex: {StartIndex} }}";
        }
    }
}
