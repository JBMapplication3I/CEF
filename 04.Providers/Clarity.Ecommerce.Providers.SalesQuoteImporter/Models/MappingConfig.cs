// <copyright file="MappingConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the mapping configuration class</summary>
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global, MemberCanBePrivate.Global, UnusedAutoPropertyAccessor.Global, UnusedMember.Global
// ReSharper disable CollectionNeverUpdated.Global
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A mapping configuration.</summary>
    [DataContract]
    public class MappingConfig
    {
        /// <summary>Initializes a new instance of the <see cref="MappingConfig"/> class.</summary>
        public MappingConfig()
        {
            Mappings = new List<MapFromIncomingProperty>();
        }

        /// <summary>Gets or sets the name of the configuration.</summary>
        /// <value>The name of the configuration.</value>
        [DataMember(Name = "configName"), JsonProperty("configName")]
        [ApiMember(Name = "configName", DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The name of this mapping config.")]
        public string ConfigName { get; set; }

        /// <summary>Gets or sets the name of the sheet.</summary>
        /// <value>The name of the sheet.</value>
        [DataMember(Name = "sheetName"), JsonProperty("sheetName")]
        [ApiMember(Name = "sheetName", DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The name of the sheet to parse. Optional, when not present or null, will use the 'sheetIndex' property instead.")]
        public string SheetName { get; set; }

        /// <summary>Gets or sets the zero-based index of the sheet.</summary>
        /// <value>The sheet index.</value>
        [DataMember(Name = "sheetIndex"), JsonProperty("sheetIndex")]
        [ApiMember(Name = "sheetIndex", DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The zero-based index of the sheet to read. Optional, when not present or null and 'sheetName' is also not present or null, will default to 0.")]
        public int? SheetIndex { get; set; }

        /// <summary>Gets or sets the number of header rows.</summary>
        /// <value>The number of header rows.</value>
        [DataMember(Name = "rowSkip"), JsonProperty("rowSkip")]
        [ApiMember(Name = "rowSkip", DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "How many rows to skip before reading in the Header. Optional, when not present or null, will default to 0.")]
        public int? RowSkip { get; set; }

        /// <summary>Gets or sets the number of header rows.</summary>
        /// <value>The number of header rows.</value>
        [DataMember(Name = "columnSkip"), JsonProperty("columnSkip")]
        [ApiMember(Name = "columnSkip", DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "How many columns to skip before reading in the Header. Optional, when not present or null, will default to 0.")]
        public int? ColumnSkip { get; set; }

        /// <summary>Gets or sets the number of header rows.</summary>
        /// <value>The number of header rows.</value>
        [DataMember(Name = "headerRowCount"), JsonProperty("headerRowCount")]
        [ApiMember(Name = "headerRowCount", DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The number of rows to read in as the Header. Optional, when not present or null, will default to 1.")]
        public int? HeaderRowCount { get; set; }

        /// <summary>Gets or sets the name of the sheet.</summary>
        /// <value>The name of the sheet.</value>
        [DataMember(Name = "coreEntity"), JsonProperty("coreEntity")]
        [ApiMember(Name = "coreEntity", DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The rows in this mapping are instances of the specific Type or Property")]
        public string CoreEntity { get; set; }

        /// <summary>Gets or sets the name of the sheet.</summary>
        /// <value>The name of the sheet.</value>
        [DataMember(Name = "recordsAre"), JsonProperty("recordsAre")]
        [ApiMember(Name = "recordsAre", DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The rows in this mapping are instances of the specific Type or Property")]
        public string RecordsAre { get; set; }

        /// <summary>Gets or sets the map unmapped to.</summary>
        /// <value>The map unmapped to.</value>
        [DataMember(Name = "mapUnmappedTo"), JsonProperty("mapUnmappedTo")]
        [ApiMember(Name = "mapUnmappedTo", DataType = "MappingConfigPropertyBase", ParameterType = "body", IsRequired = true,
            Description = "The number of rows to read in as the Header. Optional, when not present or null, will default to 1.")]
        public MapToEntityProperties MapUnmappedTo { get; set; }

        /// <summary>Gets or sets the mappings.</summary>
        /// <value>The mappings.</value>
        [DataMember(Name = "mappings"), JsonProperty("mappings")]
        [ApiMember(Name = "mappings", DataType = "MappingConfigProperty[]", ParameterType = "body", IsRequired = true,
            Description = "The number of rows to read in as the Header. Optional, when not present or null, will default to 1.")]
        public List<MapFromIncomingProperty> Mappings { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, SerializableAttributesDictionaryExtensions.JsonSettings);
        }
    }
}
