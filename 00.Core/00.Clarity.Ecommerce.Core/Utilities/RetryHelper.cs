// <copyright file="RetryHelper.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the retry helper class</summary>
namespace Clarity.Ecommerce
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>A retry helper.</summary>
    public static class RetryHelper
    {
        ////private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Gets the delay per attempt in seconds.</summary>
        /// <value>The delay per attempt in seconds.</value>
        internal static TimeSpan[] DelayPerAttemptInSeconds { get; } =
        {
            TimeSpan.FromSeconds(02),
            TimeSpan.FromSeconds(30),
            TimeSpan.FromMinutes(02),
            TimeSpan.FromMinutes(10),
            TimeSpan.FromMinutes(30),
        };

        /// <summary>Retry on exception.</summary>
        /// <param name="operation">The operation.</param>
        /// <param name="times">    The times.</param>
        /// <returns>A Task.</returns>
        public static Task RetryOnExceptionAsync(Func<Task> operation, int times = 5)
        {
            return RetryOnExceptionAsync<Exception>(operation, times);
        }

        /// <summary>Retry on exception.</summary>
        /// <typeparam name="TException">Type of the exception.</typeparam>
        /// <param name="operation">The operation.</param>
        /// <param name="times">    The times.</param>
        /// <returns>A Task.</returns>
        public static async Task RetryOnExceptionAsync<TException>(Func<Task> operation, int times = 5)
            where TException : Exception
        {
            if (times <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(times));
            }
            var attempts = 0;
            while (true)
            {
                try
                {
                    attempts++;
                    await operation().ConfigureAwait(false);
                    break;
                }
                catch (TException) when (attempts != times)
                {
                    await CreateDelayForExceptionAsync(attempts).ConfigureAwait(false);
                }
            }
        }

        /// <summary>Retry on exception with result.</summary>
        /// <typeparam name="TException">Type of the exception.</typeparam>
        /// <typeparam name="TResult">   Type of the result.</typeparam>
        /// <param name="operation">The operation.</param>
        /// <param name="times">    The times.</param>
        /// <returns>A Task{TResult}.</returns>
        public static async Task<TResult> RetryOnExceptionAsync<TException, TResult>(Func<Task<TResult>> operation, int times = 5)
            where TException : Exception
        {
            if (times <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(times));
            }
            var attempts = 0;
            while (true)
            {
                try
                {
                    attempts++;
                    return await operation().ConfigureAwait(false);
                }
                catch (TException) when (attempts != times)
                {
                    await CreateDelayForExceptionAsync(attempts).ConfigureAwait(false);
                }
            }
        }

        /// <summary>Creates delay for exception.</summary>
        /// <param name="attempts">The attempts.</param>
        /// <returns>The new delay for exception.</returns>
        private static Task CreateDelayForExceptionAsync(int attempts)
        {
            var newDelay = IncreasingDelayInSeconds(attempts);
            // Log.Warn($"Exception on attempt {attempts} of {times}. Will retry after sleeping for {delay}.", ex);
            return Task.Delay(newDelay);
        }

        /// <summary>Increasing delay in seconds.</summary>
        /// <param name="failedAttempts">The failed attempts.</param>
        /// <returns>A TimeSpan.</returns>
        private static TimeSpan IncreasingDelayInSeconds(int failedAttempts)
        {
            if (failedAttempts <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(failedAttempts));
            }
            return failedAttempts > DelayPerAttemptInSeconds.Length
                ? DelayPerAttemptInSeconds.Last()
                : DelayPerAttemptInSeconds[failedAttempts];
        }
    }
}
