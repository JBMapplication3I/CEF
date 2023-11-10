// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.RoleManager`1
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>Exposes role related api which will automatically save changes to the RoleStore.</summary>
    /// <typeparam name="TRole">.</typeparam>
    /// <seealso cref="RoleManager{TRole,String}"/>
    /// <seealso cref="RoleManager{TRole,String}"/>
    public class RoleManager<TRole> : RoleManager<TRole, string>
        where TRole : class, IRole<string>
    {
        /// <summary>Constructor.</summary>
        /// <param name="store">.</param>
        public RoleManager(IRoleStore<TRole, string> store) : base(store) { }
    }
}
