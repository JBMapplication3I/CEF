namespace StackExchange.Redis.Extensions.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>Adds behavior to System.Linq.</summary>
    public static class LinqExtensions
    {
        /// <summary>Performs a syncronous ForEach operation on an enumerable.</summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="source">The enumeration.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }
        }

        /// <summary>Performs an asyncronous ForEach operation as a Task.WhenAll on an enumeragble.</summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="body">  The body.</param>
        /// <returns>A Task.</returns>
        public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body)
        {
            return Task.WhenAll(
                from item in source
                select Task.Run(() => body(item)));
        }
    }
}