// <copyright file="MultipleMatchedElementsException.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the multiple matched elements exception class</summary>
namespace ServiceStack.CodeGenerator.TypeScript.XmlDocumentationReader
{
    using System;

    /// <summary>Exception for signaling multiple matched elements errors.</summary>
    /// <seealso cref="Exception"/>
    internal class MultipleMatchedElementsException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="MultipleMatchedElementsException"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">  The inner.</param>
        public MultipleMatchedElementsException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MultipleMatchedElementsException"/> class.</summary>
        /// <param name="message">The message.</param>
        public MultipleMatchedElementsException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="MultipleMatchedElementsException"/> class.</summary>
        public MultipleMatchedElementsException()
        {
        }
    }
}
