// <copyright file="Paging.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the paging class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A paging.</summary>
    public class Paging
    {
        /// <summary>Initializes a new instance of the <see cref="Paging"/> class.</summary>
        public Paging()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Paging"/> class.</summary>
        /// <param name="size">      The size.</param>
        /// <param name="startIndex">The start index.</param>
        public Paging(int size, int startIndex)
        {
            Size = size;
            StartIndex = startIndex;
        }

        /// <summary>Gets or sets the size.</summary>
        /// <value>The size.</value>
        [JsonProperty("Size"), DataMember(Name = "Size"),
            ApiMember(Name = "Size", DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? Size { get; set; }

        /// <summary>Gets or sets the zero-based index of the start.</summary>
        /// <value>The start index.</value>
        [JsonProperty("StartIndex"), DataMember(Name = "StartIndex"),
            ApiMember(Name = "StartIndex", DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? StartIndex { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{{ Size: {Size}, StartIndex: {StartIndex} }}";
        }
    }
}
