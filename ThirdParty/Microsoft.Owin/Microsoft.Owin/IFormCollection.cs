// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.IFormCollection
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>Contains the parsed form values.</summary>
    public interface IFormCollection
        : IReadableStringCollection,
            IEnumerable<KeyValuePair<string, string[]>>,
            IEnumerable
    {
    }
}
