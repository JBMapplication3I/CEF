// <copyright file="SerializableAttributesDictionaryExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the serializable attributes dictionary extensions class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>A serializable attributes dictionary extensions.</summary>
    public static class SerializableAttributesDictionaryExtensions
    {
        /// <summary>Gets the JSON settings.</summary>
        /// <value>The JSON settings.</value>
        public static JsonSerializerSettings JsonSettings { get; } = new()
        {
            NullValueHandling = NullValueHandling.Ignore, // Get rid of NULLs
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, // Get rid of default values
            Formatting = Formatting.None, // no whitespace, keeps the size down
            DateFormatHandling = DateFormatHandling.IsoDateFormat, // Use a legible format
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new SkipEmptyContractResolver(),
        };

        /// <summary>A SerializableAttributesDictionary extension method that serialize attributes dictionary.</summary>
        /// <param name="dictionary">The dictionary to act on.</param>
        /// <returns>A string.</returns>
        public static string SerializeAttributesDictionary(this SerializableAttributesDictionary? dictionary)
        {
            return dictionary is null
                ? "{}"
                : JsonConvert.SerializeObject(dictionary, JsonSettings);
        }

        /// <summary>A string extension method that deserialize attributes dictionary.</summary>
        /// <param name="jsonSource">The jsonSource to act on.</param>
        /// <returns>A SerializableAttributesDictionary.</returns>
        public static SerializableAttributesDictionary DeserializeAttributesDictionary(this string jsonSource)
        {
            if (string.IsNullOrWhiteSpace(jsonSource) || jsonSource == "{}")
            {
                return new();
            }
            // Note: ConcurrentDictionary doesn't deserialize correctly, so converting string to a regular dictionary
            // and then pushing that into a SAD
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, SerializableAttributeObject>>(
                jsonSource,
                JsonSettings);
            return dictionary is null
                ? new()
                : new SerializableAttributesDictionary(dictionary);
        }

        /// <summary>A skip empty contract resolver.</summary>
        /// <seealso cref="Newtonsoft.Json.Serialization.DefaultContractResolver"/>
        /// <seealso cref="DefaultContractResolver"/>
        public class SkipEmptyContractResolver : DefaultContractResolver
        {
            /// <inheritdoc/>
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);
                var isDefaultValueIgnored = ((property.DefaultValueHandling ?? DefaultValueHandling.Ignore)
                    & DefaultValueHandling.Ignore) != 0;
                if (!isDefaultValueIgnored
                    || typeof(string).GetTypeInfo().IsAssignableFrom((property.PropertyType ?? throw new InvalidOperationException()).GetTypeInfo())
                    || !typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(property.PropertyType.GetTypeInfo()))
                {
                    return property;
                }
                bool NewShouldSerialize(object obj)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    return property.ValueProvider?.GetValue(obj) is not ICollection { Count: 0 };
                }
                var oldShouldSerialize = property.ShouldSerialize;
                property.ShouldSerialize = oldShouldSerialize is not null
                    ? o => oldShouldSerialize(o) && NewShouldSerialize(o)
                    : (Predicate<object>)NewShouldSerialize;
                return property;
            }
        }
    }
}
