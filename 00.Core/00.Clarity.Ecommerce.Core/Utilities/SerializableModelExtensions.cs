// <copyright file="SerializableModelExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the serializable model extensions class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections;
    using System.Linq;
    using ServiceStack;

    /// <summary>A serializable model extensions.</summary>
    public static class SerializableModelExtensions
    {
        /// <summary>A T extension method that ignore empty data.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="instance">The instance to act on.</param>
        /// <param name="name">    The name.</param>
        /// <returns>A bool?.</returns>
        public static bool? IgnoreEmptyData<T>(this T instance, string name)
            where T : class
        {
            var accessor = TypeProperties.Get(instance.GetType()).GetAccessor(name);
            if (accessor is null)
            {
                return null;
            }
            if (accessor.PropertyInfo.PropertyType == typeof(string))
            {
                return !string.IsNullOrEmpty((string)accessor.PublicGetter(instance));
            }
            if (accessor.PropertyInfo.PropertyType.GetInterfaces().Any(x => x == typeof(ICollection)))
            {
                return (ICollection)accessor.PublicGetter(instance) is { Count: > 0 };
            }
            return null;
        }
    }
}
