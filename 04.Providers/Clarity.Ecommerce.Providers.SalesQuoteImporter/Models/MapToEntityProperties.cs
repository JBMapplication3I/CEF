// <copyright file="MapToEntityProperties.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the map to entity properties class</summary>
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter
{
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A mapping configuration property base.</summary>
    [DataContract]
    public class MapToEntityProperties
    {
        /// <summary>Gets or sets the Entity to map to. A value of 'Default' will map to the current default entity object
        /// type. Can use nested object references. If the nested property is an array, it is assumed to be the type that the
        /// array contains.</summary>
        /// <value>The entity.</value>
        [DataMember(Name = "entity"), JsonProperty("entity")]
        [ApiMember(Name = "entity", DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The Entity to map to. A value of 'Default' will map to the current default entity object type. Can use nested object references. If the nested property is an array, it is assumed to be the type that the array contains.")]
        public string Entity { get; set; }

        /// <summary>Gets or sets the instance in a collection when multiple sub-records of a collection are being imported at
        /// once (for instance category, sub-category.</summary>
        /// <value>The instance.</value>
        [DataMember(Name = "instance"), JsonProperty("instance")]
        [ApiMember(Name = "instance", DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The instance in a collection when multiple sub-records of a collection are being imported at once (for instance category, sub-category)")]
        public int? Instance { get; set; }

        /// <summary>Gets or sets the name(s) of the property(ies) to map to.
        /// <br/>Note: Some properties have special handling and require specific formatting to import into.</summary>
        /// <value>The name of the property to map to.</value>
        [DataMember(Name = "to"), JsonProperty("to")]
        [ApiMember(Name = "to", DataType = "string[]", ParameterType = "body", IsRequired = true,
            Description = "The name of the property to map to. Note: Some properties have special handling and require specific formatting to import into.")]
        public string[] To { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, SerializableAttributesDictionaryExtensions.JsonSettings);
        }
    }
}
