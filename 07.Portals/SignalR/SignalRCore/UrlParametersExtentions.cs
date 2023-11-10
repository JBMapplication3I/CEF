// <copyright file="UrlParametersExtentions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the URL parameters extentions class</summary>
namespace Clarity.Ecommerce.SignalRCore
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    /// <summary>An URL parameters extentions.</summary>
    public static class UrlParametersExtentions
    {
        /// <summary>A Dictionary{string,object} extension method that converts the parameters to a query string.</summary>
        /// <param name="parameters">The parameters to act on.</param>
        /// <returns>A string.</returns>
        public static string AsQueryString(this Dictionary<string, object> parameters)
        {
            if (!parameters.Any())
            {
                return string.Empty;
            }
            var builder = new StringBuilder("?");
            var separator = string.Empty;
            foreach (var kvp in parameters.Where(kvp => kvp.Value != null))
            {
                builder.AppendFormat("{0}{1}={2}", separator, WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value.ToString()));
                separator = "&";
            }
            return builder.ToString();
        }
    }
}
