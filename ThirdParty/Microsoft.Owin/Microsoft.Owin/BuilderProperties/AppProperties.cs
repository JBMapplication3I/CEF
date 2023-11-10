// <copyright file="AppProperties.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the application properties class</summary>
namespace Microsoft.Owin.BuilderProperties
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>A wrapper for the <see cref="Builder.AppBuilder.Properties" /> IDictionary.</summary>
    public struct AppProperties
    {
        /// <summary>Gets or sets the address collection for “host.Addresses”.</summary>
        /// <value>The address collection for “host.Addresses”.</value>
        public AddressCollection Addresses
        {
            get => new(Get<IList<IDictionary<string, object>>>("host.Addresses"));
            set => Set("host.Addresses", value.List);
        }

        /// <summary>Gets or sets the action delegate for “builder.AddSignatureConversion”.</summary>
        /// <value>The action delegate for “builder.AddSignatureConversion”.</value>
        public Action<Delegate> AddSignatureConversionDelegate
        {
            get => Get<Action<Delegate>>("builder.AddSignatureConversion");
            set => Set("builder.AddSignatureConversion", value);
        }

        /// <summary>Gets or sets the string value for “host.AppName”.</summary>
        /// <value>The string value for “host.AppName”.</value>
        public string AppName
        {
            get => Get<string>("host.AppName");
            set => Set("host.AppName", value);
        }

        /// <summary>Gets or sets the list of “server.Capabilities”.</summary>
        /// <value>The list of “server.Capabilities”.</value>
        public Capabilities Capabilities
        {
            get => new(Get<IDictionary<string, object>>("server.Capabilities"));
            set => Set("server.Capabilities", value.Dictionary);
        }

        /// <summary>Gets or sets the function delegate for “builder.DefaultApp”.</summary>
        /// <value>The function delegate for “builder.DefaultApp”.</value>
        public Func<IDictionary<string, object>, Task> DefaultApp
        {
            get => Get<Func<IDictionary<string, object>, Task>>("builder.DefaultApp");
            set => Set("builder.DefaultApp", value);
        }

        /// <summary>Gets the underlying dictionary for this
        /// <see cref="AppProperties" /> instance.</summary>
        /// <value>The underlying dictionary for this <see cref="AppProperties" />
        /// instance.</value>
        public IDictionary<string, object> Dictionary { get; }

        /// <summary>Gets or sets the cancellation token for “host.OnAppDisposing”.</summary>
        /// <value>The cancellation token for “host.OnAppDisposing”.</value>
        public CancellationToken OnAppDisposing
        {
            get => Get<CancellationToken>("host.OnAppDisposing");
            set => Set("host.OnAppDisposing", value);
        }

        /// <summary>Gets or sets the string value for “owin.Version”.</summary>
        /// <value>The string value for “owin.Version”.</value>
        public string OwinVersion
        {
            get => Get<string>("owin.Version");
            set => Set("owin.Version", value);
        }

        /// <summary>Gets or sets the text writer for “host.TraceOutput”.</summary>
        /// <value>The text writer for “host.TraceOutput”.</value>
        public TextWriter TraceOutput
        {
            get => Get<TextWriter>("host.TraceOutput");
            set => Set("host.TraceOutput", value);
        }

        /// <summary>Initializes a new instance of the <see cref="AppProperties" />
        /// class.</summary>
        /// <param name="dictionary">.</param>
        public AppProperties(IDictionary<string, object> dictionary)
        {
            Dictionary = dictionary;
        }

        /// <summary>Determines whether the current AppProperties is equal to the specified AppProperties.</summary>
        /// <param name="other">The other AppProperties to compare with the current instance.</param>
        /// <returns>true if the current AppProperties is equal to the specified AppProperties; otherwise, false.</returns>
        public bool Equals(AppProperties other)
        {
            return Equals(Dictionary, other.Dictionary);
        }

        /// <summary>Determines whether the current AppProperties is equal to the specified object.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if the current AppProperties is equal to the specified object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is AppProperties))
            {
                return false;
            }
            return Equals((AppProperties)obj);
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

        /// <summary>Determines whether the first AppPProperties is equal to the second AppProperties.</summary>
        /// <param name="left"> The first AppPropeties to compare.</param>
        /// <param name="right">The second AppPropeties to compare.</param>
        /// <returns>true if both AppProperties are equal; otherwise, false.</returns>
        public static bool operator ==(AppProperties left, AppProperties right)
        {
            return left.Equals(right);
        }

        /// <summary>Determines whether the first AppPProperties is not equal to the second AppProperties.</summary>
        /// <param name="left"> The first AppPropeties to compare.</param>
        /// <param name="right">The second AppPropeties to compare.</param>
        /// <returns>true if both AppProperties are not equal; otherwise, false.</returns>
        public static bool operator !=(AppProperties left, AppProperties right)
        {
            return !left.Equals(right);
        }

        /// <summary>Sets the value with the specified key.</summary>
        /// <param name="key">  The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>This instance.</returns>
        public AppProperties Set(string key, object value)
        {
            Dictionary[key] = value;
            return this;
        }
    }
}
