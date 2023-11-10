// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.RoleValidator`1
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>Validates roles before they are saved.</summary>
    /// <typeparam name="TRole">.</typeparam>
    /// <seealso cref="RoleValidator{TRole,String}"/>
    /// <seealso cref="RoleValidator{TRole,String}"/>
    public class RoleValidator<TRole> : RoleValidator<TRole, string>
        where TRole : class, IRole<string>
    {
        /// <summary>Constructor.</summary>
        /// <param name="manager">.</param>
        public RoleValidator(RoleManager<TRole, string> manager) : base(manager) { }
    }
}
