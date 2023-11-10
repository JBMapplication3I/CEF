// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.HeaderDictionary
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure;

    /// <summary>Represents a wrapper for owin.RequestHeaders and owin.ResponseHeaders.</summary>
    /// <seealso cref="IHeaderDictionary"/>
    /// <seealso cref="IReadableStringCollection"/>
    /// <seealso cref="IEnumerable{KeyValuePair{string,string[]}}"/>
    /// <seealso cref="IEnumerable"/>
    /// <seealso cref="IDictionary{String,String[]}"/>
    /// <seealso cref="ICollection{KeyValuePair{string,string[]}}"/>
    /// <seealso cref="IHeaderDictionary"/>
    /// <seealso cref="IReadableStringCollection"/>
    public class HeaderDictionary
        : IHeaderDictionary,
          IReadableStringCollection,
          IEnumerable<KeyValuePair<string, string[]>>,
          IEnumerable,
          IDictionary<string, string[]>,
          ICollection<KeyValuePair<string, string[]>>
    {
        /// <summary>Initializes a new instance of the <see cref="HeaderDictionary" /> class.</summary>
        /// <param name="store">The underlying data store.</param>
        public HeaderDictionary(IDictionary<string, string[]> store)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
        }

        /// <summary>Gets the number of elements contained in the <see cref="HeaderDictionary" />;.</summary>
        /// <value>The number of elements contained in the <see cref="HeaderDictionary" />.</value>
        public int Count => Store.Count;

        /// <summary>Gets a value that indicates whether the <see cref="HeaderDictionary" /> is in read-
        /// only mode.</summary>
        /// <value>true if the <see cref="HeaderDictionary" /> is in read-only mode; otherwise, false.</value>
        public bool IsReadOnly => Store.IsReadOnly;

        /// <summary>Gets an <see cref="ICollection" /> that contains the keys in the
        /// <see cref="HeaderDictionary" />;.</summary>
        /// <value>An <see cref="ICollection" /> that contains the keys in the
        /// <see cref="HeaderDictionary" />.</value>
        public ICollection<string> Keys => Store.Keys;

        /// <summary>Gets the values.</summary>
        /// <value>The values.</value>
        public ICollection<string[]> Values => Store.Values;

        /// <summary>Gets the store.</summary>
        /// <value>The store.</value>
        private IDictionary<string, string[]> Store { get; }

        /// <summary>Get or sets the associated value from the collection as a single string.</summary>
        /// <param name="key">The header name.</param>
        /// <returns>the associated value from the collection as a single string or null if the key is not present.</returns>
        public string this[string key]
        {
            get => Get(key);
            set => Set(key, value);
        }

        /// <inheritdoc/>
        string[] IDictionary<string, string[]>.this[string key]
        {
            get => Store[key];
            set => Store[key] = value;
        }

        /// <summary>Adds the given header and values to the collection.</summary>
        /// <param name="key">  The header name.</param>
        /// <param name="value">The header values.</param>
        public void Add(string key, string[] value)
        {
            Store.Add(key, value);
        }

        /// <summary>Adds a new list of items to the collection.</summary>
        /// <param name="item">The item to add.</param>
        public void Add(KeyValuePair<string, string[]> item)
        {
            Store.Add(item);
        }

        /// <summary>Add a new value. Appends to the header if already present.</summary>
        /// <param name="key">  The header name.</param>
        /// <param name="value">The header value.</param>
        public void Append(string key, string value)
        {
            OwinHelpers.AppendHeader(Store, key, value);
        }

        /// <summary>Quotes any values containing comas, and then coma joins all of the values with any existing values.</summary>
        /// <param name="key">   The header name.</param>
        /// <param name="values">The header values.</param>
        public void AppendCommaSeparatedValues(string key, params string[] values)
        {
            OwinHelpers.AppendHeaderJoined(Store, key, values);
        }

        /// <summary>Add new values. Each item remains a separate array entry.</summary>
        /// <param name="key">   The header name.</param>
        /// <param name="values">The header values.</param>
        public void AppendValues(string key, params string[] values)
        {
            OwinHelpers.AppendHeaderUnmodified(Store, key, values);
        }

        /// <summary>Clears the entire list of objects.</summary>
        public void Clear()
        {
            Store.Clear();
        }

        /// <summary>Returns a value indicating whether the specified object occurs within this collection.</summary>
        /// <param name="item">The item.</param>
        /// <returns>true if the specified object occurs within this collection; otherwise, false.</returns>
        public bool Contains(KeyValuePair<string, string[]> item)
        {
            return Store.Contains(item);
        }

        /// <summary>Determines whether the <see cref="HeaderDictionary" /> contains a specific key.</summary>
        /// <param name="key">The key.</param>
        /// <returns>true if the <see cref="HeaderDictionary" /> contains a specific key; otherwise, false.</returns>
        public bool ContainsKey(string key)
        {
            return Store.ContainsKey(key);
        }

        /// <summary>Copies the <see cref="HeaderDictionary" /> elements to a one-dimensional Array
        /// instance at the specified index.</summary>
        /// <param name="array">     The one-dimensional Array that is the destination of the specified objects copied
        ///                          from the
        ///                          <see cref="HeaderDictionary" />.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(KeyValuePair<string, string[]>[] array, int arrayIndex)
        {
            Store.CopyTo(array, arrayIndex);
        }

        /// <summary>Get the associated value from the collection as a single string.</summary>
        /// <param name="key">The header name.</param>
        /// <returns>the associated value from the collection as a single string or null if the key is not present.</returns>
        public string Get(string key)
        {
            return OwinHelpers.GetHeader(Store, key);
        }

        /// <summary>Get the associated values from the collection separated into individual values. Quoted values will
        /// not be split, and the quotes will be removed.</summary>
        /// <param name="key">The header name.</param>
        /// <returns>the associated values from the collection separated into individual values, or null if the key is
        /// not present.</returns>
        public IList<string> GetCommaSeparatedValues(string key)
        {
            var headerSplit = OwinHelpers.GetHeaderSplit(Store, key);
            if (headerSplit == null)
            {
                return null;
            }
            return headerSplit.ToList();
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the
        /// collection.</returns>
        public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator()
        {
            return Store.GetEnumerator();
        }

        /// <summary>Get the associated values from the collection without modification.</summary>
        /// <param name="key">The header name.</param>
        /// <returns>the associated value from the collection without modification, or null if the key is not present.</returns>
        public IList<string> GetValues(string key)
        {
            return OwinHelpers.GetHeaderUnmodified(Store, key);
        }

        /// <summary>Removes the given header from the collection.</summary>
        /// <param name="key">The header name.</param>
        /// <returns>true if the specified object was removed from the collection; otherwise, false.</returns>
        public bool Remove(string key)
        {
            return Store.Remove(key);
        }

        /// <summary>Removes the given item from the the collection.</summary>
        /// <param name="item">The item.</param>
        /// <returns>true if the specified object was removed from the collection; otherwise, false.</returns>
        public bool Remove(KeyValuePair<string, string[]> item)
        {
            return Store.Remove(item);
        }

        /// <summary>Sets a specific header value.</summary>
        /// <param name="key">  The header name.</param>
        /// <param name="value">The header value.</param>
        public void Set(string key, string value)
        {
            OwinHelpers.SetHeader(Store, key, value);
        }

        /// <summary>Quotes any values containing comas, and then coma joins all of the values.</summary>
        /// <param name="key">   The header name.</param>
        /// <param name="values">The header values.</param>
        public void SetCommaSeparatedValues(string key, params string[] values)
        {
            OwinHelpers.SetHeaderJoined(Store, key, values);
        }

        /// <summary>Sets the specified header values without modification.</summary>
        /// <param name="key">   The header name.</param>
        /// <param name="values">The header values.</param>
        public void SetValues(string key, params string[] values)
        {
            OwinHelpers.SetHeaderUnmodified(Store, key, values);
        }

        /// <summary>Retrieves a value from the dictionary.</summary>
        /// <param name="key">  The header name.</param>
        /// <param name="value">The value.</param>
        /// <returns>true if the <see cref="HeaderDictionary" /> contains the key; otherwise, false.</returns>
        public bool TryGetValue(string key, out string[] value)
        {
            return Store.TryGetValue(key, out value);
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the
        /// collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
