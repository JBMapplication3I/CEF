// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.PhoneNumberTokenProvider`1
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>TokenProvider that generates tokens from the user's security stamp and notifies a user via their
    /// phone number.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <seealso cref="PhoneNumberTokenProvider{TUser,String}"/>
    /// <seealso cref="PhoneNumberTokenProvider{TUser,String}"/>
    public class PhoneNumberTokenProvider<TUser> : PhoneNumberTokenProvider<TUser, string>
        where TUser : class, IUser<string>
    {
    }
}
