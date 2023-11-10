// <copyright file="JobFailedException.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the job failed exception class</summary>
namespace Clarity.Ecommerce.Tasks
{
    using System;

    /// <summary>Exception for signaling job failed errors.</summary>
    /// <seealso cref="Exception"/>
    public class JobFailedException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="JobFailedException"/> class.</summary>
        public JobFailedException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="JobFailedException"/> class.</summary>
        /// <param name="message">The message.</param>
        public JobFailedException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="JobFailedException"/> class.</summary>
        /// <param name="message">       The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public JobFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
