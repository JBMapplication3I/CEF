// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext`6
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Validation;
    using System.Globalization;
    using System.Linq;

    /// <summary>Generic IdentityDbContext base that can be customized with entity types that extend from the base
    /// IdentityUserXXX types.</summary>
    /// <typeparam name="TUser">     .</typeparam>
    /// <typeparam name="TRole">     .</typeparam>
    /// <typeparam name="TKey">      .</typeparam>
    /// <typeparam name="TUserLogin">.</typeparam>
    /// <typeparam name="TUserRole"> .</typeparam>
    /// <typeparam name="TUserClaim">.</typeparam>
    /// <seealso cref="DbContext"/>
    /// <seealso cref="DbContext"/>
    public class IdentityDbContext<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> : DbContext
        where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : IdentityRole<TKey, TUserRole>
        where TUserLogin : IdentityUserLogin<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
    {
        /// <summary>Default constructor which uses the "DefaultConnection" connectionString.</summary>
        public IdentityDbContext() : this("DefaultConnection") { }

        /// <summary>Constructor which takes the connection string to use.</summary>
        /// <param name="nameOrConnectionString">.</param>
        public IdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>Constructs a new context instance using the existing connection to connect to a database, and
        /// initializes it from the given model.  The connection will not be disposed when the context is disposed if
        /// contextOwnsConnection is false.</summary>
        /// <param name="existingConnection">   An existing connection to use for the new context.</param>
        /// <param name="model">                The model that will back this context.</param>
        /// <param name="contextOwnsConnection">Constructs a new context instance using the existing connection to
        ///                                     connect to a database, and initializes it from the given model.  The
        ///                                     connection will not be disposed when the context is disposed if
        ///                                     contextOwnsConnection is false.</param>
        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <summary>Constructs a new context instance using conventions to create the name of the database to which a
        /// connection will be made, and initializes it from the given model.  The by-convention name is the full name
        /// (namespace + class name) of the derived context class.  See the class remarks for how this is used to create
        /// a connection.</summary>
        /// <param name="model">The model that will back this context.</param>
        public IdentityDbContext(DbCompiledModel model)
            : base(model)
        {
        }

        /// <summary>Constructs a new context instance using the existing connection to connect to a database.  The
        /// connection will not be disposed when the context is disposed if contextOwnsConnection is false.</summary>
        /// <param name="existingConnection">   An existing connection to use for the new context.</param>
        /// <param name="contextOwnsConnection">If set to true the connection is disposed when the context is disposed,
        ///                                     otherwise the caller must dispose the connection.</param>
        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>Constructs a new context instance using the given string as the name or connection string for the
        /// database to which a connection will be made, and initializes it from the given model.  See the class remarks
        /// for how this is used to create a connection.</summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        /// <param name="model">                 The model that will back this context.</param>
        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        /// <summary>If true validates that emails are unique.</summary>
        /// <value>True if require unique email, false if not.</value>
        public bool RequireUniqueEmail { get; set; }

        /// <summary>IDbSet of Roles.</summary>
        /// <value>The roles.</value>
        public virtual IDbSet<TRole> Roles { get; set; }

        /// <summary>IDbSet of Users.</summary>
        /// <value>The users.</value>
        public virtual IDbSet<TUser> Users { get; set; }

        /// <summary>Maps table names, and sets up relationships between the various user entities.</summary>
        /// <param name="modelBuilder">.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            // User Table
            var userTable = modelBuilder.Entity<TUser>().ToTable("AspNetUsers");
            userTable.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            userTable.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            userTable.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            var propertyConfiguration1 = userTable.Property(u => u.UserName).IsRequired().HasMaxLength(256);
            var indexAttribute1 = new IndexAttribute("UserNameIndex")
            {
                IsUnique = true
            };
            var indexAnnotation1 = new IndexAnnotation(indexAttribute1);
            propertyConfiguration1.HasColumnAnnotation("Index", indexAnnotation1);
            userTable.Property(u => u.Email).HasMaxLength(256);

            // User Role Table
            modelBuilder.Entity<TUserRole>().HasKey(r => new { r.UserId, r.RoleId }).ToTable("AspNetUserRoles");

            // User Login Table
            modelBuilder.Entity<TUserLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("AspNetUserLogins");

            // User Claim Table
            modelBuilder.Entity<TUserClaim>().ToTable("AspNetUserClaims");

            // User Role Table
            var rolesTable = modelBuilder.Entity<TRole>().ToTable("AspNetRoles");
            var propertyConfiguration2 = rolesTable.Property(r => r.Name).IsRequired().HasMaxLength(256);
            var indexAttribute2 = new IndexAttribute("RoleNameIndex")
            {
                IsUnique = true
            };
            var indexAnnotation2 = new IndexAnnotation(indexAttribute2);
            propertyConfiguration2.HasColumnAnnotation("Index", indexAnnotation2);
            rolesTable.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);
        }

        /// <summary>Validates that UserNames are unique and case insensitive.</summary>
        /// <param name="entityEntry">.</param>
        /// <param name="items">      .</param>
        /// <returns>A DbEntityValidationResult.</returns>
        protected override DbEntityValidationResult ValidateEntity(
            DbEntityEntry entityEntry,
            IDictionary<object, object> items)
        {
            if (entityEntry != null && entityEntry.State == (EntityState)4)
            {
                var dbValidationErrors = new List<DbValidationError>();
                if (entityEntry.Entity is not TUser user)
                {
                    if (entityEntry.Entity is TRole role && Roles.Any(r => r.Name == role.Name))
                    {
                        dbValidationErrors.Add(
                            new DbValidationError(
                                "Role",
                                string.Format(
                                    CultureInfo.CurrentCulture,
                                    IdentityResources.RoleAlreadyExists,
                                    role.Name)));
                    }
                }
                else
                {
                    if (Users.Any(u => u.UserName == user.UserName))
                    {
                        dbValidationErrors.Add(
                            new DbValidationError(
                                "User",
                                string.Format(
                                    CultureInfo.CurrentCulture,
                                    IdentityResources.DuplicateUserName,
                                    user.UserName)));
                    }
                    if (RequireUniqueEmail)
                    {
                        if (Users.Any(u => u.Email == user.Email))
                        {
                            dbValidationErrors.Add(
                                new DbValidationError(
                                    "User",
                                    string.Format(
                                        CultureInfo.CurrentCulture,
                                        IdentityResources.DuplicateEmail,
                                        user.Email)));
                        }
                    }
                }
                if (dbValidationErrors.Any())
                {
                    return new DbEntityValidationResult(entityEntry, dbValidationErrors);
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }
    }
}
