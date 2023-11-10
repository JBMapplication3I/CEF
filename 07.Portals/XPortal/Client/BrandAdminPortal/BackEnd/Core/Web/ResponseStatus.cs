// <copyright file="ResponseStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the templated controller base class</summary>
namespace Clarity.Ecommerce.MVC.Core
{
    using System;

    /// <summary>A response status.</summary>
    public class ResponseStatus
    {
        /// <summary>The error code.</summary>
        public string? ErrorCode;

        /// <summary>The errors.</summary>
        public string[] Errors = Array.Empty<string>();

        /// <summary>The message.</summary>
        public string? Message;

        /// <summary>Gets or sets the stack trace.</summary>
        /// <value>The stack trace.</value>
        public string? StackTrace { get; set; }
    }
}
