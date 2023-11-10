// <copyright file="NoDocumentationFoundException.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the no documentation found exception class</summary>
namespace ServiceStack.CodeGenerator.TypeScript.XmlDocumentationReader
{
    using System;

    /// <summary>Exception for signaling no documentation found errors.</summary>
    /// <seealso cref="Exception"/>
    internal class NoDocumentationFoundException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="NoDocumentationFoundException"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">  The inner.</param>
        public NoDocumentationFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="NoDocumentationFoundException"/> class.</summary>
        /// <param name="message">The message.</param>
        public NoDocumentationFoundException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="NoDocumentationFoundException"/> class.</summary>
        public NoDocumentationFoundException()
        {
        }
    }
}
