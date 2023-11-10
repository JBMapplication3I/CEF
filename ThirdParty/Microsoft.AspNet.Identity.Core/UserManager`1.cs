// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.UserManager`1
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>UserManager for users where the primary key for the User is of type string.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <seealso cref="UserManager{TUser,String}"/>
    /// <seealso cref="UserManager{TUser,String}"/>
    public class UserManager<TUser> : UserManager<TUser, string>
        where TUser : class, IUser<string>
    {
        /// <summary>Constructor.</summary>
        /// <param name="store">.</param>
        public UserManager(IUserStore<TUser> store) : base(store) { }
    }
}
