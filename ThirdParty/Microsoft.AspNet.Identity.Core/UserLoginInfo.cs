// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.UserLoginInfo
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    /// <summary>Represents a linked login for a user (i.e. a facebook/google account)</summary>
    public sealed class UserLoginInfo
    {
        /// <summary>Constructor.</summary>
        /// <param name="loginProvider">.</param>
        /// <param name="providerKey">  .</param>
        public UserLoginInfo(string loginProvider, string providerKey)
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }

        /// <summary>Provider for the linked login, i.e. Facebook, Google, etc.</summary>
        /// <value>The login provider.</value>
        public string LoginProvider { get; set; }

        /// <summary>User specific key for the login provider.</summary>
        /// <value>The provider key.</value>
        public string ProviderKey { get; set; }
    }
}
