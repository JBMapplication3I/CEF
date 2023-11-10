// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole`1
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    /// <summary>EntityType that represents a user belonging to a role.</summary>
    /// <typeparam name="TKey">.</typeparam>
    public class IdentityUserRole<TKey>
    {
        /// <summary>RoleId for the role.</summary>
        /// <value>The identifier of the role.</value>
        public virtual TKey RoleId { get; set; }

        /// <summary>UserId for the user that is in the role.</summary>
        /// <value>The identifier of the user.</value>
        public virtual TKey UserId { get; set; }
    }
}
