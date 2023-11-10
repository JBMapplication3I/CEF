// <copyright file="ICloneable.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICloneable interface</summary>
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Models;
    using Newtonsoft.Json;

    /// <summary>Interface for cloneable.</summary>
    public interface ICloneable
    {
        /// <summary>Makes a shallow copy of this object.</summary>
        /// <returns>A shallow copy of this object.</returns>
        object Clone();

        /// <summary>Writes to a string without database identifiers, dates, custom keys, etc. so that only the
        /// meaningful properties remain for comparison by turning into a hash.</summary>
        /// <returns>The string, ready to hash.</returns>
        string ToHashableString();
    }

    /// <summary>A clone extensions.</summary>
    public static class CloneExtensions
    {
        /// <summary>A T extension method that copies the given obj.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="obj">The obj to act on.</param>
        /// <returns>A T.</returns>
        public static T Copy<T>(this T obj)
            where T : ICloneable
        {
            return (T)obj.Clone();
        }

        /// <summary>Deep clone, serializes the object to json string and then deserializes to make a fully separated
        /// object in memory.</summary>
        /// <param name="obj">The object to act on.</param>
        /// <returns>A copy of this object.</returns>
        public static object DeepCopy(this object obj)
        {
            // Use serialization to ensure no memory matching assignments.
            // Review this if it takes too long
            return JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(
                    obj,
                    SerializableAttributesDictionaryExtensions.JsonSettings),
                obj.GetType())!;
        }
    }
}
