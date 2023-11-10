// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.IdentityUser
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    using System;

    /// <summary>Default EntityFramework IUser implementation.</summary>
    /// <seealso cref="IdentityUser{String,IdentityUserLogin,IdentityUserRole,IdentityUserClaim}"/>
    /// <seealso cref="IUser"/>
    /// <seealso cref="IUser{String}"/>
    public class IdentityUser
        : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUser, IUser<string>
    {
        /// <summary>Constructor which creates a new Guid for the Id.</summary>
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>Constructor that takes a userName.</summary>
        /// <param name="userName">.</param>
        public IdentityUser(string userName) : this()
        {
            UserName = userName;
        }
    }
}
