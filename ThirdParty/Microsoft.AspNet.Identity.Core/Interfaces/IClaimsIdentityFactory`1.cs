// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IClaimsIdentityFactory`1
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>Interface for creating a ClaimsIdentity from a user.</summary>
    /// <typeparam name="TUser">.</typeparam>
    public interface IClaimsIdentityFactory<TUser>
        where TUser : class, IUser
    {
        /// <summary>Create a ClaimsIdentity from an user using a UserManager.</summary>
        /// <param name="manager">           .</param>
        /// <param name="user">              .</param>
        /// <param name="authenticationType">.</param>
        /// <returns>The new asynchronous.</returns>
        Task<ClaimsIdentity> CreateAsync(UserManager<TUser> manager, TUser user, string authenticationType);
    }
}
