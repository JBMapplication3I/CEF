﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IQueryableUserStore`1
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>Interface that exposes an IQueryable users.</summary>
    /// <typeparam name="TUser">.</typeparam>
    public interface IQueryableUserStore<TUser>
        : IQueryableUserStore<TUser, string>
        where TUser : class, IUser<string>
    {
    }
}
