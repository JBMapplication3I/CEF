// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.IReadableStringCollection
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>Accessors for headers, query, forms, etc.</summary>
    public interface IReadableStringCollection : IEnumerable<KeyValuePair<string, string[]>>, IEnumerable
    {
        /// <summary>Get the associated value from the collection.  Multiple values will be merged. Returns null if the
        /// key is not present.</summary>
        /// <param name="key">.</param>
        /// <returns>The indexed item.</returns>
        string this[string key] { get; }

        /// <summary>Get the associated value from the collection.  Multiple values will be merged. Returns null if the
        /// key is not present.</summary>
        /// <param name="key">.</param>
        /// <returns>A string.</returns>
        string Get(string key);

        /// <summary>Get the associated values from the collection in their original format. Returns null if the key is
        /// not present.</summary>
        /// <param name="key">.</param>
        /// <returns>The values.</returns>
        IList<string> GetValues(string key);
    }
}
