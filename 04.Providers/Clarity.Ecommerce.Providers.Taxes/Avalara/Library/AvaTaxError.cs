// <copyright file="AvaTaxError.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ava tax error class</summary>
namespace Avalara.AvaTax.RestClient
{
    using System;
    using System.Net;

    /// <summary>Represents an error returned by AvaTax.</summary>
    /// <seealso cref="T:System.Exception"/>
    public class AvaTaxError : Exception
    {
        /// <summary>The HTTP status code.</summary>
        /// <value>The status code.</value>
        public HttpStatusCode statusCode { get; set; }

        /// <summary>The raw error message from the client.</summary>
        /// <value>The error.</value>
        public ErrorResult? error { get; set; }

        /// <summary>Constructs an error object for an API call.</summary>
        /// <param name="err"> The error.</param>
        /// <param name="code">The code.</param>
        public AvaTaxError(ErrorResult? err, HttpStatusCode code)
        {
            error = err;
            statusCode = code;
        }
    }
}
