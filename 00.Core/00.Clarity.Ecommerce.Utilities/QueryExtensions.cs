// <copyright file="QueryExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the query extensions class</summary>
namespace Clarity.Ecommerce.Utilities
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Web;
    using Newtonsoft.Json;

    /// <summary>A query extension.</summary>
    public static class QueryExtensions
    {
        /// <summary>Not best practice but during workshop allows us to not worry about how folks model their search form.</summary>
        /// <param name="obj">The obj to act on.</param>
        /// <returns>Obj as a query string.</returns>
        public static string ToQueryString(this object? obj)
        {
            if (obj is null)
            {
                return string.Empty;
            }
            var builder = new StringBuilder();
            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.CustomAttributes
                    .Any(x => x.AttributeType == typeof(JsonIgnoreAttribute)
                        || x.AttributeType == typeof(IgnoreDataMemberAttribute)))
                {
                    // Skip
                    continue;
                }
                var value = prop.GetValue(obj, null);
                if (value == null)
                {
                    // Skip
                    continue;
                }
                if (value is Dictionary<string, string[]> asDict)
                {
                    if (Contract.CheckEmpty(asDict))
                    {
                        // Skip
                        continue;
                    }
                    var builder2 = new StringBuilder();
                    foreach (var pair in asDict)
                    {
                        if (Contract.CheckEmpty(pair.Value))
                        {
                            continue;
                        }
                        var builder3 = new StringBuilder()
                            .Append('"')
                            .Append(pair.Key)
                            .Append("\":[");
                        var count = 0;
                        foreach (var v in pair.Value)
                        {
                            if (!Contract.CheckValidKey(v))
                            {
                                // Skip
                                continue;
                            }
                            builder3
                                .AppendIf(count > 0, ",")
                                .Append('"')
                                .Append(v)
                                .Append('"');
                            count++;
                        }
                        if (count == 0)
                        {
                            // Skip
                            continue;
                        }
                        builder3.Append(']');
                        builder2
                            .AppendIf(builder2.Length > 0, ",")
                            .Append(HttpUtility.UrlEncode(builder3.ToString()));
                    }
                    if (builder2.Length == 0)
                    {
                        // Skip
                        continue;
                    }
                    builder
                        .AppendIf(builder.Length == 0, "?", "&")
                        .Append(prop.Name.ToLowerInvariant())
                        .Append('=')
                        .Append("%7b") // {
                        .Append(builder2)
                        .Append("%7d"); // }
                    continue;
                }
                if (value is IEnumerable<string> asArrayStr)
                {
                    var arrayStr = asArrayStr as string[] ?? asArrayStr.ToArray();
                    if (Contract.CheckEmpty(arrayStr))
                    {
                        // Skip
                        continue;
                    }
                    var count = 0;
                    var builder2 = new StringBuilder();
                    foreach (var v in arrayStr)
                    {
                        if (!Contract.CheckValidKey(v))
                        {
                            // Skip
                            continue;
                        }
                        builder2
                            .AppendIf(count > 0, ",")
                            .Append('"')
                            .Append(v)
                            .Append('"');
                        count++;
                    }
                    if (builder2.Length == 0)
                    {
                        // Skip
                        continue;
                    }
                    builder2
                        .Insert(0, '[')
                        .Append(']');
                    builder
                        .AppendIf(builder.Length == 0, "?", "&")
                        .Append(prop.Name.ToLowerInvariant())
                        .Append('=')
                        .Append(HttpUtility.UrlEncode(builder2.ToString()));
                    continue;
                }
                if (value is IEnumerable<int> asArrayInt)
                {
                    var arrayInt = asArrayInt as int[] ?? asArrayInt.ToArray();
                    if (Contract.CheckEmpty(arrayInt))
                    {
                        // Skip
                        continue;
                    }
                    var count = 0;
                    var builder2 = new StringBuilder();
                    foreach (var v in arrayInt)
                    {
                        if (Contract.CheckInvalidID(v))
                        {
                            // Skip
                            continue;
                        }
                        builder2
                            .AppendIf(count > 0, ",")
                            .Append('"')
                            .Append(v)
                            .Append('"');
                        count++;
                    }
                    if (builder2.Length == 0)
                    {
                        // Skip
                        continue;
                    }
                    builder2
                        .Insert(0, '[')
                        .Append(']');
                    builder
                        .AppendIf(builder.Length == 0, "?", "&")
                        .Append(prop.Name.ToLowerInvariant())
                        .Append('=')
                        .Append(HttpUtility.UrlEncode(builder2.ToString()));
                    continue;
                }
                // Else, consider it an object and rely on ToString
                var toString = value.ToString();
                if (!Contract.CheckValidKey(toString))
                {
                    // Skip
                    continue;
                }
                var encodedValue = HttpUtility.UrlEncode(toString);
                builder
                    .AppendIf(builder.Length == 0, "?", "&")
                    .Append(prop.Name.ToLowerInvariant())
                    .Append('=')
                    .Append(encodedValue);
            }
            return builder.ToString();
            /*
            var properties = obj.GetType().GetProperties()
                .Where(p => p.GetValue(obj, null) != null)
                .Select(p => new
                {
                    p,
                    value = HttpUtility.UrlEncode(p.GetValue(obj, null).ToString()),
                })
                .Where(t => !string.IsNullOrEmpty(t.value))
                .Select(t => t.p.Name.ToLowerInvariant() + "=" + t.value);
            return "?" + string.Join("&", properties.ToArray());
            */
        }
    }
}
