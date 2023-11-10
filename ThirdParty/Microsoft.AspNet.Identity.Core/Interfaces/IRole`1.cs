// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IRole`1
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>Mimimal set of data needed to persist role information.</summary>
    /// <typeparam name="TKey">.</typeparam>
    public interface IRole<out TKey>
    {
        /// <summary>Id of the role.</summary>
        /// <value>The identifier.</value>
        TKey Id { get; }

        /// <summary>Name of the role.</summary>
        /// <value>The name.</value>
        string Name { get; set; }
    }
}
