// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.UserValidator`1
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>Validates users before they are saved.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <seealso cref="UserValidator{TUser,String}"/>
    /// <seealso cref="UserValidator{TUser,String}"/>
    public class UserValidator<TUser> : UserValidator<TUser, string>
        where TUser : class, IUser<string>
    {
        /// <summary>Constructor.</summary>
        /// <param name="manager">.</param>
        public UserValidator(UserManager<TUser, string> manager) : base(manager) { }
    }
}
