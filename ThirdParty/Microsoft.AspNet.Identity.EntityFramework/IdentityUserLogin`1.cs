// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin`1
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    /// <summary>Entity type for a user's login (i.e. facebook, google)</summary>
    /// <typeparam name="TKey">.</typeparam>
    public class IdentityUserLogin<TKey>
    {
        /// <summary>The login provider for the login (i.e. facebook, google)</summary>
        /// <value>The login provider.</value>
        public virtual string LoginProvider { get; set; }

        /// <summary>Key representing the login for the provider.</summary>
        /// <value>The provider key.</value>
        public virtual string ProviderKey { get; set; }

        /// <summary>User Id for the user who owns this login.</summary>
        /// <value>The identifier of the user.</value>
        public virtual TKey UserId { get; set; }
    }
}
