// <copyright file="ClarityCustomExecutionStrategy.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the clarity custom execution strategy class</summary>
// <remarks>This class was discovered at:
// http://www.entityframework.info/Home/MetadataValidation
// </remarks>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;
    using System.Linq;

    /// <summary>A clarity custom execution strategy.</summary>
    /// <seealso cref="DbExecutionStrategy"/>
    public class ClarityCustomExecutionStrategy : DbExecutionStrategy
    {
        private readonly List<int> errorCodesToRetry = new()
        {
            SqlRetryErrorCodes.Deadlock,
            SqlRetryErrorCodes.TimeoutExpired,
            SqlRetryErrorCodes.CouldNotOpenConnection,
            SqlRetryErrorCodes.TransportFail,
        };

        /// <summary>Initializes a new instance of the <see cref="ClarityCustomExecutionStrategy"/> class.</summary>
        /// <param name="maxRetryCount">Number of maximum retries.</param>
        /// <param name="maxDelay">     The maximum delay.</param>
        public ClarityCustomExecutionStrategy(int maxRetryCount, TimeSpan maxDelay)
            : base(maxRetryCount, maxDelay)
        {
        }

        /// <inheritdoc/>
        protected override bool ShouldRetryOn(Exception exception)
        {
            return exception is SqlException sqlException
                // Enumerate through all errors found in the exception
                && sqlException.Errors.Cast<SqlError>().Any(err => errorCodesToRetry.Contains(err.Number));
        }
    }
}
