// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim`1
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    /// <summary>EntityType that represents one specific user claim.</summary>
    /// <typeparam name="TKey">.</typeparam>
    public class IdentityUserClaim<TKey>
    {
        /// <summary>Claim type.</summary>
        /// <value>The type of the claim.</value>
        public virtual string ClaimType { get; set; }

        /// <summary>Claim value.</summary>
        /// <value>The claim value.</value>
        public virtual string ClaimValue { get; set; }

        /// <summary>Primary key.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; set; }

        /// <summary>User Id for the user who owns this login.</summary>
        /// <value>The identifier of the user.</value>
        public virtual TKey UserId { get; set; }
    }
}
