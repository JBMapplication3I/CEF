// <copyright file="Contract.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the contract class</summary>
// <remarks>
// See https://www.jetbrains.com/help/resharper/Reference__Code_Annotation_Attributes.html
// </remarks>
// ReSharper disable UnusedMember.Global
namespace Clarity.Ecommerce.MVC.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary>A contract.</summary>
    [PublicAPI]
    public static class Contract
    {
        /// <summary>Requires a condition be true or throw an exception of the specified type.</summary>
        /// <typeparam name="TException">Type of the exception to throw if the condition is false.</typeparam>
        /// <param name="condition">True to condition.</param>
        /// <param name="message">  The message to send in the event of an exception.</param>
        [ContractAnnotation("halt <= condition: false"), System.Diagnostics.DebuggerStepThrough]
        public static void Requires<TException>(bool condition, string? message = null)
            where TException : Exception
        {
            if (condition)
            {
                return;
            }
            if (message is null)
            {
                throw Activator.CreateInstance<TException>();
            }
            if (typeof(TException) == typeof(ArgumentNullException))
            {
                throw new ArgumentNullException(message, message);
            }
#pragma warning disable CS8597 // Thrown value may be null.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            throw (TException)Activator.CreateInstance(typeof(TException), message);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8597 // Thrown value may be null.
        }

        /// <summary>Requires not null.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="obj">    The object.</param>
        /// <param name="message">The message.</param>
        /// <returns>A T.</returns>
        [Pure, ContractAnnotation("halt <= obj:null"), System.Diagnostics.DebuggerStepThrough]
        public static T RequiresNotNull<T>(T obj, string? message = null)
        {
            Requires<ArgumentNullException>(
                obj is not null,
                message ?? $"This operation requires the object of type {GetTypeName(typeof(T))} to not be null.");
            return obj;
        }

        /// <summary>Requires not null.</summary>
        /// <typeparam name="TException">Type of the exception.</typeparam>
        /// <typeparam name="T">         Generic type parameter.</typeparam>
        /// <param name="obj">    The object.</param>
        /// <param name="message">The message.</param>
        /// <returns>A T.</returns>
        [Pure, ContractAnnotation("halt <= obj:null"), System.Diagnostics.DebuggerStepThrough]
        public static T RequiresNotNull<TException, T>(T obj, string? message = null)
            where TException : Exception
        {
            Requires<TException>(obj is not null, message ?? "This operation requires the object to not be null.");
            return obj;
        }

        /// <summary>Enumerates requires not empty in this collection.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="arr">    The array.</param>
        /// <param name="message">The message.</param>
        /// <returns>An enumerator that allows foreach to be used to process requires not empty in this collection.</returns>
        [Pure, ContractAnnotation("halt <= arr:null"), System.Diagnostics.DebuggerStepThrough]
        public static IEnumerable<T> RequiresNotEmpty<T>(IEnumerable<T>? arr, string? message = null)
        {
            Requires<ArgumentNullException>(arr is not null, message ?? "This operation requires the enumerable object to not be null.");
            // ReSharper disable once PossibleMultipleEnumeration
            Requires<ArgumentNullException>(CheckNotEmpty(arr), message ?? "This operation requires the enumerable object not be empty.");
            // ReSharper disable once PossibleMultipleEnumeration
            return arr!;
        }

        /// <summary>Requires not null.</summary>
        /// <param name="objects">A variable-length parameters list containing objects.</param>
        /// <returns>An object[].</returns>
        [Pure, System.Diagnostics.DebuggerStepThrough]
        public static object[] RequiresNotNull(params object?[] objects)
        {
            Requires<ArgumentNullException>(
                RequiresNotNull(
                        objects,
                        "This operation requires one of these objects to not be null.")
                    .Any(obj => obj is not null),
                "This operation requires one of these objects to not be null.");
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return objects;
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        /// <summary>Check not null.</summary>
        /// <param name="objects">A variable-length parameters list containing objects.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("halt <= objects:null"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckNotNull(params object?[] objects)
        {
            return objects.All(obj => obj is not null);
        }

        /// <summary>Requires valid identifier.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="message">The message.</param>
        /// <returns>An int.</returns>
        [Pure, ContractAnnotation("halt <= id:null"), System.Diagnostics.DebuggerStepThrough]
        public static int RequiresValidID(int? id, string? message = null)
        {
            Requires<InvalidOperationException>(CheckValidID(id), message ?? "This operation requires a valid ID");
            return id!.Value;
        }

        /// <summary>Requires valid identifier.</summary>
        /// <typeparam name="TException">Type of the exception.</typeparam>
        /// <param name="id">     The identifier.</param>
        /// <param name="message">The message.</param>
        /// <returns>An int.</returns>
        [Pure, ContractAnnotation("halt <= id:null"), System.Diagnostics.DebuggerStepThrough]
        public static int RequiresValidID<TException>(int? id, string? message = null)
            where TException : Exception
        {
            Requires<TException>(CheckValidID(id), message ?? "This operation requires a valid ID");
            return id!.Value;
        }

        /// <summary>Requires valid identifier.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="message">The message.</param>
        /// <returns>A long.</returns>
        [Pure, ContractAnnotation("halt <= id:null"), System.Diagnostics.DebuggerStepThrough]
        public static long RequiresValidID(long? id, string? message = null)
        {
            Requires<InvalidOperationException>(CheckValidID(id), message ?? "This operation requires a valid ID");
            return id!.Value;
        }

        /// <summary>Check valid identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("id:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckValidID(int? id)
        {
            return id > 0 && id != int.MaxValue;
        }

        /// <summary>Check valid identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("id:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckValidID(long? id)
        {
            return id > 0 && id != int.MaxValue && id != long.MaxValue;
        }

        /// <summary>Check valid identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("id:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckValidID(decimal? id)
        {
            return id > 0 && id != int.MaxValue && id != decimal.MaxValue;
        }

        /// <summary>Check valid identifier.</summary>
        /// <param name="value">The value.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("value:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckValidID(double? value)
        {
            return value > 0
                && Math.Abs(value.Value - int.MaxValue) <= 0.01d
                && Math.Abs(value.Value - double.MaxValue) <= 0.01d;
        }

        /// <summary>Check valid identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("id:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckValidID(Guid? id)
        {
            return id.HasValue && id != default(Guid);
        }

        /// <summary>Check valid date.</summary>
        /// <param name="date">The date.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("date:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckValidDate(DateTime? date)
        {
            return date.HasValue && date != DateTime.MinValue && date != DateTime.MaxValue;
        }

        /// <summary>Requires valid key.</summary>
        /// <param name="key">    The key.</param>
        /// <param name="message">The message.</param>
        /// <returns>A string.</returns>
        [Pure, ContractAnnotation("halt <= key:null"), System.Diagnostics.DebuggerStepThrough]
        public static string RequiresValidKey(string key, string? message = null)
        {
            Requires<InvalidOperationException>(CheckValidKey(key), message ?? "This operation requires a valid Key");
            return key;
        }

        /// <summary>Requires valid key.</summary>
        /// <typeparam name="TError">Type of the error.</typeparam>
        /// <param name="key">    The key.</param>
        /// <param name="message">The message.</param>
        /// <returns>A string.</returns>
        [Pure, ContractAnnotation("halt <= key:null"), System.Diagnostics.DebuggerStepThrough]
        public static string RequiresValidKey<TError>(string key, string? message = null)
            where TError : Exception
        {
            Requires<TError>(CheckValidKey(key), message ?? "This operation requires a valid Key");
            return key;
        }

        /// <summary>Check valid key.</summary>
        /// <param name="key">The key.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("key:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckValidKey(string? key)
        {
            return !string.IsNullOrWhiteSpace(key);
        }

        /// <summary>Requires any valid key.</summary>
        /// <typeparam name="TError">Type of the error.</typeparam>
        /// <param name="keys">A variable-length parameters list containing keys.</param>
        /// <returns>A string[].</returns>
        [Pure, ContractAnnotation("halt <= keys:null"), System.Diagnostics.DebuggerStepThrough]
        public static string[] RequiresAnyValidKey<TError>(params string?[]? keys)
            where TError : Exception
        {
            Requires<TError>(!CheckAnyValidKey(keys));
            // ReSharper disable once RedundantEnumerableCastCall
            return keys!.Where(x => x is not null).Cast<string>().ToArray();
        }

        /// <summary>Requires any valid key.</summary>
        /// <param name="keys">A variable-length parameters list containing keys.</param>
        /// <returns>A string[].</returns>
        [Pure, ContractAnnotation("halt <= keys:null"), System.Diagnostics.DebuggerStepThrough]
        public static string[] RequiresAnyValidKey(params string?[]? keys)
        {
            Requires<ArgumentNullException>(CheckAnyValidKey(keys));
            return keys!;
        }

        /// <summary>Check valid identifier or any valid key.</summary>
        /// <param name="id">  The identifier.</param>
        /// <param name="keys">A variable-length parameters list containing keys.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, System.Diagnostics.DebuggerStepThrough]
        public static bool CheckValidIDOrAnyValidKey(int? id, params string?[]? keys)
        {
            return CheckValidID(id) || CheckAnyValidKey(keys);
        }

        /// <summary>Requires valid identifier or any valid key.</summary>
        /// <typeparam name="TError">Type of the error.</typeparam>
        /// <param name="id">  The identifier.</param>
        /// <param name="keys">A variable-length parameters list containing keys.</param>
        /// <returns>A Tuple.</returns>
        [Pure, System.Diagnostics.DebuggerStepThrough]
        public static (int? id, string[]? keys) RequiresValidIDOrAnyValidKey<TError>(
                int? id,
                params string?[]? keys)
            where TError : Exception
        {
            Requires<TError>(CheckValidIDOrAnyValidKey(id, keys));
            // ReSharper disable once RedundantEnumerableCastCall
            return (id, keys?.Where(x => x is not null).Cast<string>().ToArray());
        }

        /// <summary>Requires valid identifier or any valid key.</summary>
        /// <param name="id">  The identifier.</param>
        /// <param name="keys">A variable-length parameters list containing keys.</param>
        /// <returns>A Tuple.</returns>
        [Pure, System.Diagnostics.DebuggerStepThrough]
        public static (int? id, string[]? keys) RequiresValidIDOrAnyValidKey(
            int? id,
            params string?[]? keys)
        {
            Requires<ArgumentNullException>(CheckValidIDOrAnyValidKey(id, keys));
            // ReSharper disable once RedundantEnumerableCastCall
            return (id, keys?.Where(x => x is not null).Cast<string>().ToArray());
        }

        /// <summary>Check any valid key.</summary>
        /// <param name="keys">A variable-length parameters list containing keys.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("keys:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckAnyValidKey(params string?[]? keys)
        {
            if (keys is null)
            {
                return false;
            }
            return keys.Any(CheckValidKey);
        }

        /// <summary>Requires all valid i ds.</summary>
        /// <typeparam name="TError">Type of the error.</typeparam>
        /// <param name="ids">A variable-length parameters list containing identifiers.</param>
        /// <returns>An int[].</returns>
        [Pure, ContractAnnotation("halt <= ids:null"), System.Diagnostics.DebuggerStepThrough]
        public static int[] RequiresAllValidIDs<TError>(params int?[] ids)
            where TError : Exception
        {
            Requires<TError>(CheckAllValidIDs(ids));
            return ids.Select(x => x!.Value).ToArray();
        }

        /// <summary>Requires all valid i ds.</summary>
        /// <param name="ids">A variable-length parameters list containing identifiers.</param>
        /// <returns>An int[].</returns>
        [Pure, ContractAnnotation("halt <= ids:null"), System.Diagnostics.DebuggerStepThrough]
        public static int[] RequiresAllValidIDs(params int?[] ids)
        {
            Requires<ArgumentNullException>(CheckAllValidIDs(ids));
            return ids.Select(x => x!.Value).ToArray();
        }

        /// <summary>Check all valid keys.</summary>
        /// <param name="keys">A variable-length parameters list containing keys.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("keys:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckAllValidKeys(params string?[] keys)
        {
            return keys.All(CheckValidKey);
        }

        /// <summary>Check all invalid keys.</summary>
        /// <param name="keys">A variable-length parameters list containing keys.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("keys:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckAllInvalidKeys(params string?[]? keys)
        {
            return keys?.Any(CheckValidKey) != true;
        }

        /// <summary>Check all valid i ds.</summary>
        /// <param name="ids">A variable-length parameters list containing identifiers.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("ids:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckAllValidIDs(params int?[] ids)
        {
            return ids.All(CheckValidID);
        }

        /// <summary>Check any valid identifier.</summary>
        /// <param name="ids">A variable-length parameters list containing identifiers.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("ids:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckAnyValidID(params int?[] ids)
        {
            return ids.Any(CheckValidID);
        }

        /// <summary>Requires valid identifier or key.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="key">    The key.</param>
        /// <param name="message">The message.</param>
        /// <returns>A Tuple.</returns>
        [Pure, ContractAnnotation("halt <= id:null, key:null"), System.Diagnostics.DebuggerStepThrough]
        public static (int? id, string key) RequiresValidIDOrKey(int? id, string key, string? message = null)
        {
            Requires<InvalidOperationException>(CheckValidIDOrKey(id, key), message ?? "This operation requires a valid ID or Key");
            return (id, key);
        }

        /// <summary>Check valid identifier or key.</summary>
        /// <param name="id"> The identifier.</param>
        /// <param name="key">The key.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("id:null, key:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckValidIDOrKey(int? id, string key)
        {
            return CheckValidID(id) || CheckValidKey(key);
        }

        /// <summary>Requires null identifier.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="message">The message.</param>
        [ContractAnnotation("halt <= id:notnull"), System.Diagnostics.DebuggerStepThrough]
        public static void RequiresNullID(int? id, string? message = null)
        {
            Requires<InvalidOperationException>(CheckNullID(id), message ?? "This operation requires a null ID");
        }

        /// <summary>Check null identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("id:notnull => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckNullID(int? id)
        {
            return id is not > 0 or int.MaxValue;
        }

        /// <summary>Requires null key.</summary>
        /// <param name="key">    The key.</param>
        /// <param name="message">The message.</param>
        [ContractAnnotation("halt <= key:notnull"), System.Diagnostics.DebuggerStepThrough]
        public static void RequiresNullKey(string key, string? message = null)
        {
            Requires<InvalidOperationException>(CheckNullKey(key), message ?? "This operation requires a null Key");
        }

        /// <summary>Check null key.</summary>
        /// <param name="key">The key.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("key:notnull => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckNullKey(string key)
        {
            return !string.IsNullOrWhiteSpace(key);
        }

        /// <summary>Check empty.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="source">Source for the.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("source:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckEmpty<T>(IEnumerable<T>? source)
        {
            return source?.Any() != true;
        }

        /// <summary>Check not empty.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="source">Source for the.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("source:null => false"), System.Diagnostics.DebuggerStepThrough]
        public static bool CheckNotEmpty<T>(IEnumerable<T>? source)
        {
            return source?.Any() == true;
        }

        /// <summary>Requires all valid.</summary>
        /// <param name="objectsToValidate">A variable-length parameters list containing objects to validate.</param>
        [ContractAnnotation("objectsToValidate:null => halt"), System.Diagnostics.DebuggerStepThrough]
        public static void RequiresAllValid(params object[] objectsToValidate)
        {
            foreach (var obj in RequiresNotEmpty(objectsToValidate))
            {
                switch (obj)
                {
                    case string asString:
                    {
                        _ = RequiresValidKey(asString);
                        continue;
                    }
                    case int asInt:
                    {
                        _ = RequiresValidID(asInt);
                        continue;
                    }
                    case long asLong:
                    {
                        _ = RequiresNotNull(asLong);
                        continue;
                    }
                    case double asDouble:
                    {
                        _ = RequiresNotNull(asDouble);
                        continue;
                    }
                    case decimal asDecimal:
                    {
                        _ = RequiresNotNull(asDecimal);
                        continue;
                    }
                    default:
                    {
                        _ = RequiresNotNull(obj);
                        continue;
                    }
                }
            }
        }

        /// <summary>Check all valid.</summary>
        /// <param name="objectsToValidate">A variable-length parameters list containing objects to validate.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [Pure, ContractAnnotation("objectsToValidate:null => false"), System.Diagnostics.DebuggerStepThrough]
        // ReSharper disable once CognitiveComplexity
        public static bool CheckAllValid(params object[] objectsToValidate)
        {
            foreach (var obj in RequiresNotEmpty(objectsToValidate))
            {
                switch (obj)
                {
                    case string asString:
                    {
                        if (!CheckValidKey(asString))
                        {
                            return false;
                        }
                        continue;
                    }
                    case int asInt:
                    {
                        if (!CheckValidID(asInt))
                        {
                            return false;
                        }
                        continue;
                    }
                    case long asLong:
                    {
                        if (!CheckNotNull(asLong))
                        {
                            return false;
                        }
                        continue;
                    }
                    case double asDouble:
                    {
                        if (!CheckNotNull(asDouble))
                        {
                            return false;
                        }
                        continue;
                    }
                    case decimal asDecimal:
                    {
                        if (!CheckNotNull(asDecimal))
                        {
                            return false;
                        }
                        continue;
                    }
                    default:
                    {
                        if (!CheckNotNull(obj))
                        {
                            return false;
                        }
                        continue;
                    }
                }
            }
            return true;
        }

        [Pure, System.Diagnostics.DebuggerStepThrough]
        private static string GetTypeName(Type t)
        {
            return t.Name
                + (t.GenericTypeArguments.Length > 0
                    ? "{" + t.GenericTypeArguments.Select(x => x.Name).Aggregate((c, n) => c + "," + n) + "}"
                    : string.Empty);
        }
    }
}
