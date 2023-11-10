// <copyright file="AsyncExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the asynchronous extensions class</summary>
namespace Clarity.Ecommerce
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>An asynchronous extensions.</summary>
    public static class AsyncExtensions
    {
        /// <summary>my task factory.</summary>
        private static readonly TaskFactory MyTaskFactory = new(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default);

        /// <summary>Executes the synchronise operation.</summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="func">The function.</param>
        /// <returns>A TResult.</returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func) => MyTaskFactory
            .StartNew(func)
            .Unwrap()
            .GetAwaiter()
            .GetResult();

        /// <summary>Executes the synchronise operation.</summary>
        /// <param name="func">The function.</param>
        public static void RunSync(Func<Task> func) => MyTaskFactory
            .StartNew(func)
            .Unwrap()
            .GetAwaiter()
            .GetResult();

        /// <summary>An IEnumerable{T} extension method that for eaches a function to run on each member of the
        /// enumerable in a partition that allows for FIFO running. This prevents overloading the CPU while maintaining
        /// multi-threading for the action to take on each member. This does not provide an Async Yield.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="source">The source to act on.</param>
        /// <param name="dop">   The dop.</param>
        /// <param name="body">  The body.</param>
        /// <returns>A Task.</returns>
        public static Task ForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
            return Task.WhenAll(
                from partition in Partitioner.Create(source).GetPartitions(dop)
                select Task.Run(async () =>
                {
                    using (partition)
                    {
                        while (partition.MoveNext())
                        {
                            await body(partition.Current).ConfigureAwait(false);
                        }
                    }
                }));
        }

        /// <summary>An IEnumerable{T} extension method that for eaches a function to run on each member of the
        /// enumerable in a partition that allows for FIFO running. This prevents overloading the CPU while maintaining
        /// multi-threading for the action to take on each member. This does not provide an Async Yield.</summary>
        /// <typeparam name="TIter">  Type of the iterated object.</typeparam>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="source">The source to act on.</param>
        /// <param name="dop">   The dop.</param>
        /// <param name="body">  The body.</param>
        /// <returns>A Task{TResult}.</returns>
        public static async Task<ConcurrentDictionary<TIter, TResult>> ForEachAsync<TIter, TResult>(
                this IEnumerable<TIter> source,
                int dop,
                Func<TIter, Task<TResult>> body)
            where TIter : notnull
        {
            // ReSharper disable once PossibleMultipleEnumeration
            var results = new ConcurrentDictionary<TIter, TResult>(dop, source.Count());
            // ReSharper disable once PossibleMultipleEnumeration
            await Task.WhenAll(
                from partition in Partitioner.Create(source).GetPartitions(dop)
                select Task.Run(
                    async () =>
                    {
                        using (partition)
                        {
                            while (partition.MoveNext())
                            {
                                if (partition.Current is null)
                                {
                                    // Can't record result to concurrent dictionary
                                    await body(partition.Current!).ConfigureAwait(false);
                                }
                                else
                                {
                                    results[partition.Current] = await body(partition.Current).ConfigureAwait(false);
                                }
                            }
                        }
                    }));
            return results;
        }
    }
}
