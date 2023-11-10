// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IUserPhoneNumberStore`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System.Threading.Tasks;

    /// <summary>Stores a user's phone number.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    public interface IUserPhoneNumberStore<TUser, in TKey> : IUserStore<TUser, TKey>
        where TUser : class, IUser<TKey>
    {
        /// <summary>Get the user phone number.</summary>
        /// <param name="user">.</param>
        /// <returns>The phone number asynchronous.</returns>
        Task<string> GetPhoneNumberAsync(TUser user);

        /// <summary>Returns true if the user phone number is confirmed.</summary>
        /// <param name="user">.</param>
        /// <returns>The phone number confirmed asynchronous.</returns>
        Task<bool> GetPhoneNumberConfirmedAsync(TUser user);

        /// <summary>Set the user's phone number.</summary>
        /// <param name="user">       .</param>
        /// <param name="phoneNumber">.</param>
        /// <returns>A Task.</returns>
        Task SetPhoneNumberAsync(TUser user, string phoneNumber);

        /// <summary>Sets whether the user phone number is confirmed.</summary>
        /// <param name="user">     .</param>
        /// <param name="confirmed">.</param>
        /// <returns>A Task.</returns>
        Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed);
    }
}
