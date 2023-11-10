// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.ClaimsIdentityFactory`1
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>Creates a ClaimsIdentity from a User.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <seealso cref="Microsoft.AspNet.Identity.ClaimsIdentityFactory{TUser,String}"/>
    /// <seealso cref="ClaimsIdentityFactory{TUser,String}"/>
    public class ClaimsIdentityFactory<TUser> : ClaimsIdentityFactory<TUser, string>
        where TUser : class, IUser<string>
    {
    }
}
