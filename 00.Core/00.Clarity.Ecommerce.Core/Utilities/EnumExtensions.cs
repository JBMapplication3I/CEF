// <copyright file="EnumExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the enum extensions class</summary>
namespace Clarity.Ecommerce
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using Utilities;

    /// <summary>An enum extensions.</summary>
    public static class EnumExtensions
    {
        /// <summary>An Enum extension method that names the given value.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="value">The string name of an enum option.</param>
        /// <returns>A string.</returns>
        public static string Name(this Enum value)
        {
            Contract.RequiresNotNull(value);
            return value.ToString();
        }

        /// <summary>An Enum extension method that descriptions the given value.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="value">The string name of an enum option.</param>
        /// <returns>A string.</returns>
        public static string Description(this Enum value)
        {
            Contract.RequiresNotNull(value);
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            // ReSharper disable once InvertIf
            if (memberInfo.Length > 0)
            {
                var attribute = memberInfo[0].GetCustomAttribute<DescriptionAttribute>(false);
                if (attribute != null)
                {
                    return attribute.Description;
                }
            }
            return value.ToString();
        }

        /////// <summary>Gets value from description.</summary>
        /////// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
        /////// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///////                                             null.</exception>
        /////// <typeparam name="T">Generic type parameter.</typeparam>
        /////// <param name="description">The description.</param>
        /////// <returns>The value from description.</returns>
        ////public static T GetValueFromDescription<T>(string description)
        ////{
        ////    var type = typeof(T);
        ////    if (!type.IsEnum)
        ////    {
        ////        throw new InvalidOperationException();
        ////    }
        ////    foreach (var field in type.GetFields())
        ////    {
        ////        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        ////        if (attribute != null && attribute.Description == description)
        ////        {
        ////            return (T)field.GetValue(null)!;
        ////        }
        ////        if (field.Name == description)
        ////        {
        ////            return (T)field.GetValue(null)!;
        ////        }
        ////    }
        ////    throw new ArgumentNullException(nameof(description));
        ////}

        /// <summary>Enumerates values in this collection.</summary>
        /// <exception cref="ArgumentException">Thrown when one or more arguments have unsupported or
        ///                                     illegal values.</exception>
        /// <typeparam name="TEnum">Type of the enum.</typeparam>
        /// <returns>An enumerator that allows foreach to be used to process values in this collection.</returns>
        public static IEnumerable<TEnum> AsValues<TEnum>()
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var enumType = typeof(TEnum);
            // Optional runtime check for completeness
            if (!enumType.IsEnum)
            {
                throw new ArgumentException();
            }
            return Enum.GetValues(enumType).Cast<TEnum>();
        }

        /////// <summary>Converts a string to a specific type of enum.</summary>
        /////// <typeparam name="T">The type of enum.</typeparam>
        /////// <param name="value">The string name of an enum option.</param>
        /////// <returns>The specific enum value specified in a string.</returns>
        ////public static T ParseEnum<T>(string value)
        ////{
        ////    return (T)Enum.Parse(typeof(T), value, true);
        ////}
    }
}
