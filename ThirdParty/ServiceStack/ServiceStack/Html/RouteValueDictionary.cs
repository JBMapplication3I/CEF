//
// RouteValueDictionary.cs
//
// Author:
//	Atsushi Enomoto <atsushi@ximian.com>
//
// Copyright (C) 2008 Novell Inc. http://novell.com
//

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace ServiceStack.Html
{
#if NET_4_0
	[TypeForwardedFrom ("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
#endif
    public class RouteValueDictionary : IDictionary<string, object>
    {
        internal class CaseInsensitiveStringComparer : IEqualityComparer<string>
        {
            public static readonly CaseInsensitiveStringComparer Instance = new();

            public int GetHashCode(string obj)
            {
                return obj.ToLower().GetHashCode();
            }

            public bool Equals(string obj1, string obj2)
            {
                return string.Equals(obj1, obj2, StringComparison.OrdinalIgnoreCase);
            }
        }

        private Dictionary<string, object> d = new(CaseInsensitiveStringComparer.Instance);

        public RouteValueDictionary() { }

        public RouteValueDictionary(IDictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            foreach (var p in dictionary)
            {
                Add(p.Key, p.Value);
            }
        }

        public RouteValueDictionary(object values) // anonymous type instance
        {
            if (values == null)
            {
                return;
            }

            foreach (var pi in values.GetType().GetPropertyInfos())
            {
                try
                {
                    Add(pi.Name, pi.GetValue(values, null));
                }
                catch
                {
                    // ignore
                }
            }
        }

        public int Count => d.Count;

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly => ((ICollection<KeyValuePair<string, object>>)d).IsReadOnly;

        ICollection<string> IDictionary<string, object>.Keys => d.Keys;

        ICollection<object> IDictionary<string, object>.Values => d.Values;

        public object this[string key]
        {
            get { return d.TryGetValue(key, out var v) ? v : null; }
            set => d[key] = value;
        }

        public Dictionary<string, object>.KeyCollection Keys => d.Keys;

        public Dictionary<string, object>.ValueCollection Values => d.Values;

        public void Add(string key, object value)
        {
            d.Add(key, value);
        }

        public void Clear()
        {
            d.Clear();
        }

        public bool ContainsKey(string key)
        {
            return d.ContainsKey(key);
        }

        public bool ContainsValue(object value)
        {
            return d.ContainsValue(value);
        }

        public Dictionary<string, object>.Enumerator GetEnumerator()
        {
            return d.GetEnumerator();
        }

        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            ((ICollection<KeyValuePair<string, object>>)d).Add(item);
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)d).Contains(item);
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, object>>)d).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)d).Remove(item);
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return d.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return d.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return d.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return d.TryGetValue(key, out value);
        }
    }
}