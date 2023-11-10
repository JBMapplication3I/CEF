// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.IHeaderDictionary
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>Represents a wrapper for owin.RequestHeaders and owin.ResponseHeaders.</summary>
    public interface IHeaderDictionary
        : IReadableStringCollection,
          IEnumerable<KeyValuePair<string, string[]>>,
          IEnumerable,
          IDictionary<string, string[]>,
          ICollection<KeyValuePair<string, string[]>>
    {
        /// <summary>Get or sets the associated value from the collection as a single string.</summary>
        /// <param name="key">The header name.</param>
        /// <returns>the associated value from the collection as a single string or null if the key is not present.</returns>
        new string this[string key] { get; set; }

        /// <summary>Add a new value. Appends to the header if already present.</summary>
        /// <param name="key">  The header name.</param>
        /// <param name="value">The header value.</param>
        void Append(string key, string value);

        /// <summary>Quotes any values containing comas, and then coma joins all of the values with any existing values.</summary>
        /// <param name="key">   The header name.</param>
        /// <param name="values">The header values.</param>
        void AppendCommaSeparatedValues(string key, params string[] values);

        /// <summary>Add new values. Each item remains a separate array entry.</summary>
        /// <param name="key">   The header name.</param>
        /// <param name="values">The header values.</param>
        void AppendValues(string key, params string[] values);

        /// <summary>Get the associated values from the collection separated into individual values. Quoted values will
        /// not be split, and the quotes will be removed.</summary>
        /// <param name="key">The header name.</param>
        /// <returns>the associated values from the collection separated into individual values, or null if the key is
        /// not present.</returns>
        IList<string> GetCommaSeparatedValues(string key);

        /// <summary>Sets a specific header value.</summary>
        /// <param name="key">  The header name.</param>
        /// <param name="value">The header value.</param>
        void Set(string key, string value);

        /// <summary>Quotes any values containing comas, and then coma joins all of the values.</summary>
        /// <param name="key">   The header name.</param>
        /// <param name="values">The header values.</param>
        void SetCommaSeparatedValues(string key, params string[] values);

        /// <summary>Sets the specified header values without modification.</summary>
        /// <param name="key">   The header name.</param>
        /// <param name="values">The header values.</param>
        void SetValues(string key, params string[] values);
    }
}
