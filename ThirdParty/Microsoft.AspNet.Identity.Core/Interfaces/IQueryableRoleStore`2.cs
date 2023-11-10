// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IQueryableRoleStore`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System.Linq;

    /// <summary>Interface that exposes an IQueryable roles.</summary>
    /// <typeparam name="TRole">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    public interface IQueryableRoleStore<TRole, in TKey> : IRoleStore<TRole, TKey>
        where TRole : IRole<TKey>
    {
        /// <summary>IQueryable Roles.</summary>
        /// <value>The roles.</value>
        IQueryable<TRole> Roles { get; }
    }
}
