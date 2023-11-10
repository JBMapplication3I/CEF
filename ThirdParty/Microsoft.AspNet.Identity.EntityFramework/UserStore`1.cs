// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.UserStore`1
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    using System;
    using System.Data.Entity;

    /// <summary>EntityFramework based user store implementation that supports IUserStore, IUserLoginStore,
    /// IUserClaimStore and IUserRoleStore.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <seealso cref="IDisposable"/>
    /// <seealso cref="UserStore{TUser,IdentityRole,String,IdentityUserLogin,IdentityUserRole,IdentityUserClaim}"/>
    /// <seealso cref="IUserStore{TUser}"/>
    /// <seealso cref="IUserStore{TUser,String}"/>
    public class UserStore<TUser>
        : UserStore<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>,
          IUserStore<TUser>,
          IUserStore<TUser, string>
        where TUser : IdentityUser
    {
        /// <summary>Default constuctor which uses a new instance of a default EntityyDbContext.</summary>
        public UserStore() : this(new IdentityDbContext())
        {
            DisposeContext = true;
        }

        /// <summary>Constructor.</summary>
        /// <param name="context">.</param>
        public UserStore(DbContext context) : base(context) { }
    }
}
