// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.RoleStore`1
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll
namespace Microsoft.AspNet.Identity.EntityFramework
{
    using System;
    using System.Data.Entity;

    /// <summary>EntityFramework based implementation.</summary>
    /// <typeparam name="TRole">.</typeparam>
    /// <seealso cref="IDisposable"/>
    /// <seealso cref="RoleStore{TRole,String,IdentityUserRole}"/>
    /// <seealso cref="IQueryableRoleStore{TRole}"/>
    /// <seealso cref="IQueryableRoleStore{TRole,String}"/>
    /// <seealso cref="IRoleStore{TRole,String}"/>
    public class RoleStore<TRole>
        : RoleStore<TRole, string, IdentityUserRole>,
            IQueryableRoleStore<TRole>,
            IQueryableRoleStore<TRole, string>,
            IRoleStore<TRole, string>
        where TRole : IdentityRole, new()
    {
        /// <summary>Constructor.</summary>
        public RoleStore() : base(new IdentityDbContext())
        {
            DisposeContext = true;
        }

        /// <summary>Constructor.</summary>
        /// <param name="context">.</param>
        public RoleStore(DbContext context) : base(context) { }
    }
}
