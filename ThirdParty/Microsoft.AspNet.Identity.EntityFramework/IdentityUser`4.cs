// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.IdentityUser`4
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    using System;
    using System.Collections.Generic;

    /// <summary>Default EntityFramework IUser implementation.</summary>
    /// <typeparam name="TKey">  .</typeparam>
    /// <typeparam name="TLogin">.</typeparam>
    /// <typeparam name="TRole"> .</typeparam>
    /// <typeparam name="TClaim">.</typeparam>
    /// <seealso cref="IUser{TKey}"/>
    /// <seealso cref="IUser{TKey}"/>
    public class IdentityUser<TKey, TLogin, TRole, TClaim> : IUser<TKey>
        where TLogin : IdentityUserLogin<TKey>
        where TRole : IdentityUserRole<TKey>
        where TClaim : IdentityUserClaim<TKey>
    {
        /// <summary>Constructor.</summary>
        public IdentityUser()
        {
            Claims = new List<TClaim>();
            Roles = new List<TRole>();
            Logins = new List<TLogin>();
        }

        /// <summary>Used to record failures for the purposes of lockout.</summary>
        /// <value>The number of access failed.</value>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>Navigation property for user claims.</summary>
        /// <value>The claims.</value>
        public virtual ICollection<TClaim> Claims { get; }

        /// <summary>Email.</summary>
        /// <value>The email.</value>
        public virtual string Email { get; set; }

        /// <summary>True if the email is confirmed, default is false.</summary>
        /// <value>True if email confirmed, false if not.</value>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>User ID (Primary Key)</summary>
        /// <value>The identifier.</value>
        public virtual TKey Id { get; set; }

        /// <summary>Is lockout enabled for this user.</summary>
        /// <value>True if lockout enabled, false if not.</value>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>DateTime in UTC when lockout ends, any time in the past is considered not locked out.</summary>
        /// <value>The lockout end date UTC.</value>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>Navigation property for user logins.</summary>
        /// <value>The logins.</value>
        public virtual ICollection<TLogin> Logins { get; }

        /// <summary>The salted/hashed form of the user password.</summary>
        /// <value>The password hash.</value>
        public virtual string PasswordHash { get; set; }

        /// <summary>PhoneNumber for the user.</summary>
        /// <value>The phone number.</value>
        public virtual string PhoneNumber { get; set; }

        /// <summary>True if the phone number is confirmed, default is false.</summary>
        /// <value>True if phone number confirmed, false if not.</value>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>Navigation property for user roles.</summary>
        /// <value>The roles.</value>
        public virtual ICollection<TRole> Roles { get; }

        /// <summary>A random value that should change whenever a users credentials have changed (password changed, login
        /// removed)</summary>
        /// <value>The security stamp.</value>
        public virtual string SecurityStamp { get; set; }

        /// <summary>Is two factor enabled for the user.</summary>
        /// <value>True if two factor enabled, false if not.</value>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>User name.</summary>
        /// <value>The name of the user.</value>
        public virtual string UserName { get; set; }
    }
}
