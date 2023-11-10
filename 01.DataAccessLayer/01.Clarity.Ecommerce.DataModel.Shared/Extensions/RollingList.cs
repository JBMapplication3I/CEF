// <copyright file="RollingList.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the rolling list class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>List of rollings.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public class RollingList<T> : IEnumerable<T>
    {
        /// <summary>(Immutable) The list.</summary>
        private readonly LinkedList<T> list = new();

        /// <summary>Initializes a new instance of the <see cref="RollingList{T}"/> class.</summary>
        /// <param name="maximumCount">Number of maximums.</param>
        public RollingList(int maximumCount)
        {
            if (maximumCount <= 0)
            {
                throw new ArgumentException(null, nameof(maximumCount));
            }
            MaximumCount = maximumCount;
        }

        /// <summary>Gets the number of maximums.</summary>
        /// <value>The number of maximums.</value>
        public int MaximumCount { get; }

        /// <summary>Gets the number of. </summary>
        /// <value>The count.</value>
        public int Count => list.Count;

        /// <summary>Indexer to get items within this collection using array index syntax.</summary>
        /// <param name="index">Zero-based index of the entry to access.</param>
        /// <returns>The indexed item.</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return list.Skip(index).First();
            }
        }

        /// <summary>Adds value.</summary>
        /// <param name="value">The value to add.</param>
        public void Add(T value)
        {
            while (list.Count >= MaximumCount)
            {
                list.RemoveFirst();
            }
            list.AddLast(value);
        }

        /// <summary>Gets the enumerator.</summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        /// <summary>Gets the enumerator.</summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
