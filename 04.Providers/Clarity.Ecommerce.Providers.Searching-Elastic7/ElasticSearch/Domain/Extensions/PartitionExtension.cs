// <copyright file="PartitionExtension.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the partition extension class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Domain.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>A partition extension.</summary>
    public static class PartitionExtension
    {
        /// <summary>Enumerates partition in this collection.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="source">The source to act on.</param>
        /// <param name="size">  The size.</param>
        /// <returns>An enumerator that allows foreach to be used to process partition in this collection.</returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
        {
            T[] array = null;
            var count = 0;
            foreach (var item in source)
            {
                array ??= new T[size];
                array[count] = item;
                count++;
                if (count != size)
                {
                    continue;
                }
                yield return new ReadOnlyCollection<T>(array);
                array = null;
                count = 0;
            }
            if (array == null)
            {
                yield break;
            }
            Array.Resize(ref array, count);
            yield return new ReadOnlyCollection<T>(array);
        }
    }
}
