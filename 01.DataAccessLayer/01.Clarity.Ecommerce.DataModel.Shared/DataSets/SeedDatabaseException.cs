// <copyright file="SeedDatabaseException.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the seed database exception class</summary>
#if ORACLE
namespace Clarity.Ecommerce.DataModel.Oracle.DataSets
#else
namespace Clarity.Ecommerce.DataModel.DataSets
#endif
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>Exception for signaling seed database errors.</summary>
    /// <seealso cref="Exception"/>
    [Serializable]
    public class SeedDatabaseException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="SeedDatabaseException"/> class.</summary>
        public SeedDatabaseException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SeedDatabaseException"/> class.</summary>
        /// <param name="message">The message.</param>
        public SeedDatabaseException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SeedDatabaseException"/> class.</summary>
        /// <param name="message">       The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SeedDatabaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SeedDatabaseException"/> class.</summary>
        /// <param name="info">   The information.</param>
        /// <param name="context">The context.</param>
        protected SeedDatabaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
