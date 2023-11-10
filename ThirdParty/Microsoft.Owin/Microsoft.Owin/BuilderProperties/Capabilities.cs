// <copyright file="Capabilities.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the capabilities class</summary>
namespace Microsoft.Owin.BuilderProperties
{
    using System.Collections.Generic;

    /// <summary>Represents the capabilities for the builder properties.</summary>
    public struct Capabilities
    {
        /// <summary>The underling IDictionary.</summary>
        /// <value>The dictionary.</value>
        public IDictionary<string, object> Dictionary { get; }

        /// <summary>Gets or sets the string value for "sendfile.Version".</summary>
        /// <value>the string value for "sendfile.Version".</value>
        public string SendFileVersion
        {
            get => Get<string>("sendfile.Version");
            set => Set("sendfile.Version", value);
        }

        /// <summary>Gets or sets the websocket version.</summary>
        /// <value>The websocket version.</value>
        public string WebSocketVersion
        {
            get => Get<string>("websocket.Version");
            set => Set("websocket.Version", value);
        }

        /// <summary>Initializes a new instance of the <see cref="Capabilities" />
        /// class.</summary>
        /// <param name="dictionary">.</param>
        public Capabilities(IDictionary<string, object> dictionary)
        {
            Dictionary = dictionary;
        }

        /// <summary>Initializes a new instance of the <see cref="Capabilities" />
        /// class.</summary>
        /// <returns>A new instance of the <see cref="Capabilities" /> class.</returns>
        public static Capabilities Create()
        {
            return new Capabilities(new Dictionary<string, object>());
        }

        /// <summary>Determines whether the current Capabilities instance is equal to the specified Capabilities.</summary>
        /// <param name="other">The other Capabilities to compare with the current instance.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public bool Equals(Capabilities other)
        {
            return Equals(Dictionary, other.Dictionary);
        }

        /// <summary>Determines whether the current Capabilities is equal to the specified object.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if the current Capabilities is equal to the specified object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Capabilities))
            {
                return false;
            }
            return Equals((Capabilities)obj);
        }

        /// <summary>Gets the value from the dictionary with the specified key.</summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value with the specified key.</returns>
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
        /// <see cref="Capabilities" /> are equal.</summary>
        /// <param name="left"> The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if the two specified instances of
        /// <see cref="Capabilities" /> are equal; otherwise, false.</returns>
        public static bool operator ==(Capabilities left, Capabilities right)
        {
            return left.Equals(right);
        }

        /// <summary>Determines whether two specified instances of
        /// <see cref="Capabilities" /> are not equal.</summary>
        /// <param name="left"> The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns>true if the two specified instances of
        /// <see cref="Capabilities" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(Capabilities left, Capabilities right)
        {
            return !left.Equals(right);
        }

        /// <summary>Sets the given key and value in the underlying dictionary.</summary>
        /// <param name="key">  The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>This instance.</returns>
        public Capabilities Set(string key, object value)
        {
            Dictionary[key] = value;
            return this;
        }
    }
}
