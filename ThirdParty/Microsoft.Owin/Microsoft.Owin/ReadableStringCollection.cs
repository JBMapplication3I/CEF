// <copyright file="ReadableStringCollection.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the readable string collection class</summary>
namespace Microsoft.Owin
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Infrastructure;

    /// <summary>Accessors for query, forms, etc.</summary>
    /// <seealso cref="IReadableStringCollection"/>
    /// <seealso cref="IEnumerable{KeyValuePair{string,string[]}}"/>
    /// <seealso cref="IEnumerable"/>
    /// <seealso cref="IReadableStringCollection"/>
    public class ReadableStringCollection
        : IReadableStringCollection, IEnumerable<KeyValuePair<string, string[]>>, IEnumerable
    {
        /// <summary>Create a new wrapper.</summary>
        /// <param name="store">.</param>
        public ReadableStringCollection(IDictionary<string, string[]> store)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
        }

        /// <summary>Gets the store.</summary>
        /// <value>The store.</value>
        private IDictionary<string, string[]> Store
        {
            get;
        }

        /// <summary>Get the associated value from the collection.  Multiple values will be merged. Returns null if the
        /// key is not present.</summary>
        /// <param name="key">.</param>
        /// <returns>The indexed item.</returns>
        public string this[string key] => Get(key);

        /// <summary>Get the associated value from the collection.  Multiple values will be merged. Returns null if the
        /// key is not present.</summary>
        /// <param name="key">.</param>
        /// <returns>A string.</returns>
        public string Get(string key)
        {
            return OwinHelpers.GetJoinedValue(Store, key);
        }

        /// <summary>Gets the enumerator.</summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator()
        {
            return Store.GetEnumerator();
        }

        /// <summary>Get the associated values from the collection in their original format. Returns null if the key is
        /// not present.</summary>
        /// <param name="key">.</param>
        /// <returns>The values.</returns>
        public IList<string> GetValues(string key)
        {
            Store.TryGetValue(key, out var strArrays);
            return strArrays;
        }

        /// <summary>Gets the enumerator.</summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
