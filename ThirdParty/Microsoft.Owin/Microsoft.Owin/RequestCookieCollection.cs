// <copyright file="RequestCookieCollection.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the request cookie collection class</summary>
namespace Microsoft.Owin
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>A wrapper for the request Cookie header.</summary>
    /// <seealso cref="IEnumerable{KeyValuePair{string,string}}"/>
    /// <seealso cref="IEnumerable"/>
    public class RequestCookieCollection : IEnumerable<KeyValuePair<string, string>>, IEnumerable
    {
        /// <summary>Create a new wrapper.</summary>
        /// <param name="store">.</param>
        public RequestCookieCollection(IDictionary<string, string> store)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
        }

        /// <summary>Gets the store.</summary>
        /// <value>The store.</value>
        private IDictionary<string, string> Store
        {
            get;
        }

        /// <summary>Returns null rather than throwing KeyNotFoundException.</summary>
        /// <param name="key">.</param>
        /// <returns>The indexed item.</returns>
        public string this[string key]
        {
            get
            {
                if (!Store.TryGetValue(key, out var str) && !Store.TryGetValue(Uri.EscapeDataString(key), out str))
                {
                    return null;
                }
                return str;
            }
        }

        /// <summary>Gets the enumerator.</summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return Store.GetEnumerator();
        }

        /// <summary>Gets the enumerator.</summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
