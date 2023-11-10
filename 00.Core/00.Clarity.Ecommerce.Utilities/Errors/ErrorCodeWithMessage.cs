// <copyright file="ErrorCodeWithMessage.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the error code with message class</summary>
namespace Clarity.Ecommerce
{
    using System;
    using JetBrains.Annotations;

    /// <summary>An error code with message.</summary>
    [PublicAPI]
    public struct ErrorCodeWithMessage
    {
        /// <summary>The code.</summary>
        public readonly string Code;

        /// <summary>The message.</summary>
        public readonly string? Message;

        /// <summary>Identifier for the log.</summary>
        public readonly string? LogID;

        /// <summary>Initializes a new instance of the <see cref="ErrorCodeWithMessage"/> struct.</summary>
        /// <param name="code">   The code.</param>
        /// <param name="logID">  Identifier for the log.</param>
        /// <param name="message">(Optional) The message.</param>
        public ErrorCodeWithMessage(string code, string logID, string? message = null)
        {
            Code = code.StartsWith("CEF-") ? code : $"CEF-{code}";
            Message = message;
            LogID = logID;
        }

        /// <summary>Initializes a new instance of the <see cref="ErrorCodeWithMessage"/> struct.</summary>
        /// <param name="code"> The code.</param>
        /// <param name="logID">Identifier for the log.</param>
        /// <param name="ex">   (Optional) The exception.</param>
        public ErrorCodeWithMessage(string code, string logID, Exception ex)
        {
            Code = code.StartsWith("CEF-") ? code : $"CEF-{code}";
            Message = ex.Message;
            // Recursively append messages
            var innerEx = ex.InnerException;
            while (innerEx != null)
            {
                Message += " " + innerEx.Message;
                innerEx = innerEx.InnerException;
            }
            LogID = logID;
        }

        /// <summary>Initializes a new instance of the <see cref="ErrorCodeWithMessage"/> struct.</summary>
        /// <param name="code">   The code.</param>
        /// <param name="logID">  Identifier for the log.</param>
        /// <param name="message">(Optional) The message.</param>
        public ErrorCodeWithMessage(int code, string? logID, string? message = null)
        {
            Code = $"CEF-{code}";
            Message = message;
            LogID = logID;
        }

        /// <summary>Initializes a new instance of the <see cref="ErrorCodeWithMessage"/> struct.</summary>
        /// <param name="code"> The code.</param>
        /// <param name="logID">Identifier for the log.</param>
        /// <param name="ex">   (Optional) The exception.</param>
        public ErrorCodeWithMessage(int code, string logID, Exception ex)
        {
            Code = $"CEF-{code}";
            Message = ex.Message;
            // Recursively append messages
            var innerEx = ex.InnerException;
            while (innerEx != null)
            {
                Message += " " + innerEx.Message;
                innerEx = innerEx.InnerException;
            }
            LogID = logID;
        }
    }
}
