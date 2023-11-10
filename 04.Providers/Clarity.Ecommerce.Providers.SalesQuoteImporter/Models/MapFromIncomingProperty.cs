// <copyright file="MapFromIncomingProperty.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the map from incoming property class</summary>
// ReSharper disable CollectionNeverUpdated.Global, UnusedAutoPropertyAccessor.Global
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A mapping configuration property.</summary>
    [DataContract]
    public class MapFromIncomingProperty
    {
        /// <summary>Gets or sets the cell value for each row of the header, in order from top to bottom, for the parser to
        /// identify this column.</summary>
        /// <value>The header.</value>
        [DataMember(Name = "header"), JsonProperty("header")]
        [ApiMember(Name = "header", DataType = "string[]", ParameterType = "body", IsRequired = true,
            Description = "The cell value for each row of the header, in order from top to bottom, for the parser to identify this column.")]
        public string[] Header { get; set; }

        /// <summary>Gets or sets the zero-based index of occurrence when this header is defined multiple times. Optional,
        /// when not present or null, will default to 0.</summary>
        /// <value>The zero-based index of occurrence when this header is defined multiple times.</value>
        [DataMember(Name = "headerOccurrence"), JsonProperty("headerOccurrence")]
        [ApiMember(Name = "headerOccurrence", DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The zero-based index of occurrence when this header is defined multiple times. Optional, when not present or null, will default to 0.")]
        public int? HeaderOccurrence { get; set; }

        /// <summary>Gets or sets when the cell value matches an entry of this array, ignore it (do not attempt to apply the
        /// value). Null and Empty String values are automatically ignored and do not need to be added to this list.</summary>
        /// <value>The header.</value>
        [DataMember(Name = "ignore"), JsonProperty("ignore")]
        [ApiMember(Name = "ignore", DataType = "string[]", ParameterType = "body", IsRequired = true,
            Description = "When the cell value matches an entry of this array, ignore it (do not attempt to apply the value). Null and Empty String values are automatically ignored and do not need to be added to this list.")]
        public string[] Ignore { get; set; }

        /// <summary>Gets or sets an example record value for this property, for display reference only.</summary>
        /// <value>The example.</value>
        [DataMember(Name = "examples"), JsonProperty("examples")]
        [ApiMember(Name = "examples", DataType = "string[]", ParameterType = "body", IsRequired = false,
            Description = "An example record value for this property, for display reference only.")]
        public string[] Examples { get; set; }

        /// <summary>Gets or sets the assignments.</summary>
        /// <value>The assignments.</value>
        [DataMember(Name = "assignments"), JsonProperty("assignments")]
        [ApiMember(Name = "assignments", DataType = "MappingConfigPropertyBase[]", ParameterType = "body", IsRequired = true,
            Description = "The list of assignments for this mapping. You can assign to multiple targets within different entities.")]
        public List<MapToEntityProperties> Assignments { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, SerializableAttributesDictionaryExtensions.JsonSettings);
        }
    }
}
