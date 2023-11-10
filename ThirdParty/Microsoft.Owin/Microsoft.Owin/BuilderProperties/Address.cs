// <copyright file="Address.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address class</summary>
namespace Microsoft.Owin.BuilderProperties
{
    using System.Collections.Generic;

    /// <summary>Contains the parts of an address.</summary>
    public struct Address
    {
        /// <summary>Gets the internal dictionary for this collection.</summary>
        /// <value>The internal dictionary for this collection.</value>
        public IDictionary<string, object> Dictionary { get; }

        /// <summary>The uri host.</summary>
        /// <value>The host.</value>
        public string Host
        {
            get => Get<string>("host");
            set => Set("host", value);
        }

        /// <summary>The uri path.</summary>
        /// <value>The full pathname of the file.</value>
        public string Path
        {
            get => Get<string>("path");
            set => Set("path", value);
        }

        /// <summary>The uri port.</summary>
        /// <value>The port.</value>
        public string Port
        {
            get => Get<string>("port");
            set => Set("port", value);
        }

        /// <summary>The uri scheme.</summary>
        /// <value>The scheme.</value>
        public string Scheme
        {
            get => Get<string>("scheme");
            set => Set("scheme", value);
        }

        /// <summary>Initializes a new instance.</summary>
        /// <param name="dictionary">.</param>
        public Address(IDictionary<string, object> dictionary)
        {
            Dictionary = dictionary;
        }

        /// <summary>Initializes a new <see cref="Address" /> with the given parts.</summary>
        /// <param name="scheme">The scheme.</param>
        /// <param name="host">  The host.</param>
        /// <param name="port">  The port.</param>
        /// <param name="path">  The path.</param>
        public Address(string scheme, string host, string port, string path) : this(new Dictionary<string, object>())
        {
            Scheme = scheme;
            Host = host;
            Port = port;
            Path = path;
        }

        /// <summary>Creates a new <see cref="Address" /></summary>
        /// <returns>A new <see cref="Address" /></returns>
        public static Address Create()
        {
            return new Address(new Dictionary<string, object>());
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="other">The other object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(Address other)
        {
            return Equals(Dictionary, other.Dictionary);
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The other object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Address))
            {
                return false;
            }
            return Equals((Address)obj);
        }

        /// <summary>Gets a specified key and value from the underlying dictionary.</summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>A T.</returns>
        public T Get<T>(string key)
        {
            if (Dictionary.TryGetValue(key, out var obj))
            {
                return (T)obj;
            }
            return default;
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            if (Dictionary == null)
            {
                return 0;
            }
            return Dictionary.GetHashCode();
        }

        /// <summary>Determines whether two specified instances of
        /// <see cref="Address" /> are equal.</summary>
        /// <param name="left"> The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if left and right represent the same address; otherwise, false.</returns>
        public static bool operator ==(Address left, Address right)
        {
            return left.Equals(right);
        }

        /// <summary>Determines whether two specified instances of
        /// <see cref="Address" /> are not equal.</summary>
        /// <param name="left"> The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if left and right do not represent the same address; otherwise, false.</returns>
        public static bool operator !=(Address left, Address right)
        {
            return !left.Equals(right);
        }

        /// <summary>Sets a specified key and value in the underlying dictionary.</summary>
        /// <param name="key">  The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>The Address.</returns>
        public Address Set(string key, object value)
        {
            Dictionary[key] = value;
            return this;
        }
    }
}
