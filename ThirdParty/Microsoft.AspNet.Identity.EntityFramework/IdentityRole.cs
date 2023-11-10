// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.IdentityRole
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    using System;

    /// <summary>Represents a Role entity.</summary>
    /// <seealso cref="IdentityRole{String,IdentityUserRole}"/>
    public class IdentityRole : IdentityRole<string, IdentityUserRole>
    {
        /// <summary>Constructor.</summary>
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>Constructor.</summary>
        /// <param name="roleName">.</param>
        public IdentityRole(string roleName) : this()
        {
            Name = roleName;
        }
    }
}
