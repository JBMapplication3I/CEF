// <copyright file="NameValueCollection.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the name value collection class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Sites
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>Collection of name values.</summary>
    /// <remarks>
    /// See https://github.com/danielcrenna/hammock/blob/master/src/net35/Hammock.Silverlight/Compat/NameValueCollection.cs .
    /// </remarks>
    public class NameValueCollection : Collection<KeyValuePair<string, string>>
    {
        /// <summary>Gets all keys.</summary>
        /// <value>all keys.</value>
        public IEnumerable<string> AllKeys => this.Select(pair => pair.Key);

        /// <summary>Indexer to get items within this collection using array index syntax.</summary>
        /// <param name="index">Zero-based index of the entry to access.</param>
        /// <returns>The indexed item.</returns>
        public new string this[int index] => base[index].Value;

        /// <summary>Indexer to get items within this collection using array index syntax.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The indexed item.</returns>
        public string this[string name] => this.SingleOrDefault(kv => kv.Key.Equals(name)).Value;

        /// <summary>Adds name.</summary>
        /// <param name="name"> The name.</param>
        /// <param name="value">The value.</param>
        public void Add(string name, string value)
        {
            Collection<KeyValuePair<string, string>> list = this;
            for (var i = Count - 1; i >= 0; --i)
            {
                if (!string.Equals(list[i].Key, name))
                {
                    continue;
                }
                list[i] = new(name, list[i].Value + "," + value);
                return;
            }
            Add(new(name, value));
        }

        /// <summary>Query if this NameValueCollection has keys.</summary>
        /// <returns>True if keys, false if not.</returns>
        public bool HasKeys()
        {
            return this.Any();
        }
    }
}
