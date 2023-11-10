// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.IdentityRole`2
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    using System.Collections.Generic;

    /// <summary>Represents a Role entity.</summary>
    /// <typeparam name="TKey">     .</typeparam>
    /// <typeparam name="TUserRole">.</typeparam>
    /// <seealso cref="IRole{TKey}"/>
    /// <seealso cref="IRole{TKey}"/>
    public class IdentityRole<TKey, TUserRole> : IRole<TKey>
        where TUserRole : IdentityUserRole<TKey>
    {
        /// <summary>Constructor.</summary>
        public IdentityRole()
        {
            Users = new List<TUserRole>();
        }

        /// <summary>Role id.</summary>
        /// <value>The identifier.</value>
        public TKey Id { get; set; }

        /// <summary>Role name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Navigation property for users in the role.</summary>
        /// <value>The users.</value>
        public virtual ICollection<TUserRole> Users { get; }
    }
}
