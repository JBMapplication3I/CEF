// <copyright file="CEFUserStore.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF user store class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;
    using Microsoft.AspNet.Identity.EntityFramework2;

    /// <summary>A CEF user store.</summary>
    /// <seealso cref="UserStore{TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim}"/>
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public class CEFUserStore : UserStore<User, UserRole, int, UserLogin, RoleUser, UserClaim>, ICEFUserStore
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    {
        /// <summary>Initializes a new instance of the <see cref="CEFUserStore"/> class.</summary>
        /// <param name="context">The context.</param>
        public CEFUserStore(IDbContext context)
            : base(context)
        {
        }

        /// <inheritdoc/>
        public virtual new IClarityEcommerceEntities Context => (IClarityEcommerceEntities)base.Context;
    }
}

#region Assembly Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// location unknown
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

namespace Microsoft.AspNet.Identity.EntityFramework2
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Utilities;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.Interfaces.DataModel;
    using EntityFramework;

    // Summary:
    //     EntityFramework based user store implementation that supports IUserStore, IUserLoginStore,
    //     IUserClaimStore and IUserRoleStore
    //
    // Type parameters:
    //   TUser:
    //
    //   TRole:
    //
    //   TKey:
    //
    //   TUserLogin:
    //
    //   TUserRole:
    //
    //   TUserClaim:
    public class UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>
        : IUserLoginStore<TUser, TKey>,
            ////IUserStore<TUser, TKey>,
            ////IDisposable,
            IUserClaimStore<TUser, TKey>,
            IUserRoleStore<TUser, TKey>,
            IUserPasswordStore<TUser, TKey>,
            IUserSecurityStampStore<TUser, TKey>,
            IQueryableUserStore<TUser, TKey>,
            IUserEmailStore<TUser, TKey>,
            IUserPhoneNumberStore<TUser, TKey>,
            IUserTwoFactorStore<TUser, TKey>,
            IUserLockoutStore<TUser, TKey>
        where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : IdentityRole<TKey, TUserRole>
        where TKey : IEquatable<TKey>
        where TUserLogin : IdentityUserLogin<TKey>, new()
        where TUserRole : IdentityUserRole<TKey>, new()
        where TUserClaim : IdentityUserClaim<TKey>, new()
    {
        private readonly IDbSet<TUserLogin> logins;

        private readonly IDbSet<TUserClaim> userClaims;

        private readonly IDbSet<TUserRole> userRoles;

        private readonly EntityStore<TRole> roleStore;

        private EntityStore<TUser> userStore;

        private bool disposed;

        // Summary:
        //     Constructor which takes a db context and wires up the stores with default instances
        //     using the context
        //
        // Parameters:
        //   context:
        public UserStore(IDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            AutoSaveChanges = true;
            userStore = new(context);
            roleStore = new(context);
            logins = Context.Set<TUserLogin>();
            userClaims = Context.Set<TUserClaim>();
            userRoles = Context.Set<TUserRole>();
        }

        // Summary:
        //     Context for the store
        public IDbContext Context { get; private set; }

        // Summary:
        //     If true will call dispose on the DbContext during Dispose
        public bool DisposeContext { get; set; }

        // Summary:
        //     If true will call SaveChanges after Create/Update/Delete
        public bool AutoSaveChanges { get; set; }

        // Summary:
        //     Returns an IQueryable of users
        public IQueryable<TUser> Users => userStore.EntitySet;

        // Summary:
        //     Return the claims for a user
        //
        // Parameters:
        //   user:
        public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await EnsureClaimsLoadedAsync(user).WithCurrentCulture();
            return user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
        }

        // Summary:
        //     Add a claim to a user
        //
        // Parameters:
        //   user:
        //
        //   claim:
        public virtual Task AddClaimAsync(TUser user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            var val = new TUserClaim
            {
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            };
            userClaims.Add(val);
            return Task.FromResult(0);
        }

        // Summary:
        //     Remove a claim from a user
        //
        // Parameters:
        //   user:
        //
        //   claim:
        public virtual async Task RemoveClaimAsync(TUser user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            var claimValue = claim.Value;
            var claimType = claim.Type;
            IEnumerable<TUserClaim> enumerable;
            if (AreClaimsLoaded(user))
            {
                enumerable = user.Claims
                    .Where(uc => uc.ClaimValue == claimValue
                        && uc.ClaimType == claimType)
                    .ToList();
            }
            else
            {
                var userId = user.Id;
                enumerable = await userClaims
                    .Where(uc => uc.ClaimValue == claimValue
                        && uc.ClaimType == claimType
                        && uc.UserId.Equals(userId))
                    .ToListAsync()
                    .WithCurrentCulture();
            }
            foreach (var item in enumerable)
            {
                userClaims.Remove(item);
            }
        }

        // Summary:
        //     Returns whether the user email is confirmed
        //
        // Parameters:
        //   user:
        public virtual Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.EmailConfirmed);
        }

        // Summary:
        //     Set IsConfirmed on the user
        //
        // Parameters:
        //   user:
        //
        //   confirmed:
        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        // Summary:
        //     Set the user email
        //
        // Parameters:
        //   user:
        //
        //   email:
        public virtual Task SetEmailAsync(TUser user, string email)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.Email = email;
            return Task.FromResult(0);
        }

        // Summary:
        //     Get the user's email
        //
        // Parameters:
        //   user:
        public virtual Task<string> GetEmailAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Email);
        }

        // Summary:
        //     Find a user by email
        //
        // Parameters:
        //   email:
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public virtual Task<TUser?> FindByEmailAsync(string email)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.Email == email);
        }

        // Summary:
        //     Returns the DateTimeOffset that represents the end of a user's lockout, any time
        //     in the past should be considered not locked out.
        //
        // Parameters:
        //   user:
        public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.LockoutEndDateUtc.HasValue
                ? new(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                : default(DateTimeOffset));
        }

        // Summary:
        //     Locks a user out until the specified end date (set to a past date, to unlock
        //     a user)
        //
        // Parameters:
        //   user:
        //
        //   lockoutEnd:
        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue
                ? null
                : new DateTime?(lockoutEnd.UtcDateTime);
            return Task.FromResult(0);
        }

        // Summary:
        //     Used to record when an attempt to access the user has failed
        //
        // Parameters:
        //   user:
        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        // Summary:
        //     Used to reset the account access count, typically after the account is successfully
        //     accessed
        //
        // Parameters:
        //   user:
        public virtual Task ResetAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        // Summary:
        //     Returns the current number of failed access attempts. This number usually will
        //     be reset whenever the password is verified or the account is locked out.
        //
        // Parameters:
        //   user:
        public virtual Task<int> GetAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.AccessFailedCount);
        }

        // Summary:
        //     Returns whether the user can be locked out.
        //
        // Parameters:
        //   user:
        public virtual Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.LockoutEnabled);
        }

        // Summary:
        //     Sets whether the user can be locked out.
        //
        // Parameters:
        //   user:
        //
        //   enabled:
        public virtual Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        // Summary:
        //     Find a user by id
        //
        // Parameters:
        //   userId:
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public virtual Task<TUser?> FindByIdAsync(TKey userId)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.Id.Equals(userId));
        }

        // Summary:
        //     Find a user by name
        //
        // Parameters:
        //   userName:
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public virtual Task<TUser?> FindByNameAsync(string userName)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            ThrowIfDisposed();
            // ReSharper disable SpecifyStringComparison
#pragma warning disable RCS1155 // Use StringComparison when comparing strings.
            return GetUserAggregateAsync(u => u.UserName.ToUpper() == userName.ToUpper());
#pragma warning restore RCS1155 // Use StringComparison when comparing strings.
            // ReSharper restore SpecifyStringComparison
        }

        // Summary:
        //     Insert an entity
        //
        // Parameters:
        //   user:
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
        public virtual async Task CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            userStore.Create(user);
            await SaveChangesAsync().WithCurrentCulture();
        }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage

        // Summary:
        //     Mark an entity for deletion
        //
        // Parameters:
        //   user:
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
        public virtual async Task DeleteAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            userStore.Delete(user);
            await SaveChangesAsync().WithCurrentCulture();
        }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage

        // Summary:
        //     Update an entity
        //
        // Parameters:
        //   user:
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
        public virtual async Task UpdateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            userStore.Update(user);
            await SaveChangesAsync().WithCurrentCulture();
        }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage

        // Summary:
        //     Dispose the store
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        // Summary:
        //     Returns the user associated with this login
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public virtual async Task<TUser?> FindAsync(UserLoginInfo login)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            ThrowIfDisposed();
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var provider = login.LoginProvider;
            var key = login.ProviderKey;
            var val = await logins.FirstOrDefaultAsync(l => l.LoginProvider == provider && l.ProviderKey == key).WithCurrentCulture();
            if (val == null)
            {
                return null;
            }
            var userId = val.UserId;
            return await GetUserAggregateAsync(u => u.Id.Equals(userId)).WithCurrentCulture();
        }

        // Summary:
        //     Add a login to the user
        //
        // Parameters:
        //   user:
        //
        //   login:
        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var val = new TUserLogin
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
            };
            logins.Add(val);
            return Task.FromResult(0);
        }

        // Summary:
        //     Remove a login from a user
        //
        // Parameters:
        //   user:
        //
        //   login:
        public virtual async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var provider = login.LoginProvider;
            var key = login.ProviderKey;
            TUserLogin? val;
            if (AreLoginsLoaded(user))
            {
                val = user.Logins
                    .SingleOrDefault(ul => ul.LoginProvider == provider
                        && ul.ProviderKey == key);
            }
            else
            {
                var userId = user.Id;
                val = await logins
                    .SingleOrDefaultAsync(ul => ul.LoginProvider == provider
                        && ul.ProviderKey == key
                        && ul.UserId.Equals(userId))
                    .WithCurrentCulture();
            }
            if (val != null)
            {
                logins.Remove(val);
            }
        }

        // Summary:
        //     Get the logins for a user
        //
        // Parameters:
        //   user:
        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await EnsureLoginsLoadedAsync(user).WithCurrentCulture();
            return user.Logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList();
        }

        // Summary:
        //     Set the password hash for a user
        //
        // Parameters:
        //   user:
        //
        //   passwordHash:
        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        // Summary:
        //     Get the password hash for a user
        //
        // Parameters:
        //   user:
        public virtual Task<string> GetPasswordHashAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PasswordHash);
        }

        // Summary:
        //     Returns true if the user has a password set
        //
        // Parameters:
        //   user:
        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        // Summary:
        //     Set the user's phone number
        //
        // Parameters:
        //   user:
        //
        //   phoneNumber:
        public virtual Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        // Summary:
        //     Get a user's phone number
        //
        // Parameters:
        //   user:
        public virtual Task<string> GetPhoneNumberAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PhoneNumber);
        }

        // Summary:
        //     Returns whether the user phoneNumber is confirmed
        //
        // Parameters:
        //   user:
        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        // Summary:
        //     Set PhoneNumberConfirmed on the user
        //
        // Parameters:
        //   user:
        //
        //   confirmed:
        public virtual Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        // Summary:
        //     Add a user to a role
        //
        // Parameters:
        //   user:
        //
        //   roleName:
        public virtual async Task AddToRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(
                    "Value cannot be null or empty"/*IdentityResources.ValueCannotBeNullOrEmpty*/,
                    nameof(roleName));
            }
            var val = await roleStore.DbEntitySet
                .SingleOrDefaultAsync(r => string.Equals(r.Name, roleName, StringComparison.CurrentCultureIgnoreCase))
                .WithCurrentCulture();
            if (val == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "Role '{0}' not found"/*IdentityResources.RoleNotFound*/,
                        new object[] { roleName, }));
            }
            var val2 = new TUserRole
            {
                UserId = user.Id,
                RoleId = val.Id,
            };
            var entity = val2;
            userRoles.Add(entity);
        }

        // Summary:
        //     Remove a user from a role
        //
        // Parameters:
        //   user:
        //
        //   roleName:
        public virtual async Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value cannot be null or empty"/*IdentityResources.ValueCannotBeNullOrEmpty*/, nameof(roleName));
            }
            var val = await roleStore.DbEntitySet
                .SingleOrDefaultAsync(r => string.Equals(r.Name, roleName, StringComparison.CurrentCultureIgnoreCase))
                .WithCurrentCulture();
            if (val == null)
            {
                return;
            }
            var roleId = val.Id;
            var userId = user.Id;
            var val2 = await userRoles
                .FirstOrDefaultAsync(r => roleId.Equals(r.RoleId) && r.UserId.Equals(userId))
                .WithCurrentCulture();
            if (val2 != null)
            {
                userRoles.Remove(val2);
            }
        }

        // Summary:
        //     Get the names of the roles a user is a member of
        //
        // Parameters:
        //   user:
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
        public virtual async Task<IList<string>> GetRolesAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.Id;
            return await (
                    from userRole in userRoles
                    where userRole.UserId.Equals(userId)
                    join role in roleStore.DbEntitySet on userRole.RoleId equals role.Id
                    select role.Name)
                .ToListAsync()
                .WithCurrentCulture();
        }
#pragma warning restore AsyncFixer01 // Unnecessary async/await usage

        // Summary:
        //     Returns true if the user is in the named role
        //
        // Parameters:
        //   user:
        //
        //   roleName:
        public virtual async Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Value cannot be null or empty"/*IdentityResources.ValueCannotBeNullOrEmpty*/, nameof(roleName));
            }
#pragma warning disable RCS1155 // Use StringComparison when comparing strings.
            var val = await roleStore.DbEntitySet
                .SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper())
                .WithCurrentCulture();
#pragma warning restore RCS1155 // Use StringComparison when comparing strings.
            if (val == null)
            {
                return false;
            }
            var userId = user.Id;
            var roleId = val.Id;
            return await userRoles.AnyAsync(ur => ur.RoleId.Equals(roleId) && ur.UserId.Equals(userId)).WithCurrentCulture();
        }

        // Summary:
        //     Set the security stamp for the user
        //
        // Parameters:
        //   user:
        //
        //   stamp:
        public virtual Task SetSecurityStampAsync(TUser user, string stamp)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        // Summary:
        //     Get the security stamp for a user
        //
        // Parameters:
        //   user:
        public virtual Task<string> GetSecurityStampAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.SecurityStamp);
        }

        // Summary:
        //     Set whether two factor authentication is enabled for the user
        //
        // Parameters:
        //   user:
        //
        //   enabled:
        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        // Summary:
        //     Gets whether two factor authentication is enabled for the user
        //
        // Parameters:
        //   user:
        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.TwoFactorEnabled);
        }

        // Summary:
        //     Used to attach child entities to the User aggregate, i.e. Roles, Logins, and
        //     Claims
        //
        // Parameters:
        //   filter:
        protected virtual async Task<TUser?> GetUserAggregateAsync(Expression<Func<TUser, bool>> filter)
        {
            var user = FindByIdFilterParser.TryMatchAndGetId(filter, out var id)
                ? await userStore.GetByIdAsync(id!).WithCurrentCulture()
                : await Users.FirstOrDefaultAsync(filter).WithCurrentCulture();
            if (user == null)
            {
                return user;
            }
            await EnsureClaimsLoadedAsync(user).WithCurrentCulture();
            await EnsureLoginsLoadedAsync(user).WithCurrentCulture();
            await EnsureRolesLoadedAsync(user).WithCurrentCulture();
            return user;
        }

        // Summary:
        //     If disposing, calls dispose on the Context. Always nulls out the Context
        //
        // Parameters:
        //   disposing:
        protected virtual void Dispose(bool disposing)
        {
            if (DisposeContext && disposing && Context != null!)
            {
                Context.Dispose();
            }
            disposed = true;
            Context = null!;
            userStore = null!;
        }

        private async Task SaveChangesAsync()
        {
            if (AutoSaveChanges)
            {
                await Context.SaveChangesAsync().WithCurrentCulture();
            }
        }

        private bool AreClaimsLoaded(TUser user)
        {
            return Context.Entry(user).Collection(u => u.Claims).IsLoaded;
        }

        private async Task EnsureClaimsLoadedAsync(TUser user)
        {
            if (AreClaimsLoaded(user))
            {
                return;
            }
            var userId = user.Id;
            await userClaims.Where(uc => uc.UserId.Equals(userId)).LoadAsync().WithCurrentCulture();
            Context.Entry(user).Collection(u => u.Claims).IsLoaded = true;
        }

        private async Task EnsureRolesLoadedAsync(TUser user)
        {
            if (Context.Entry(user).Collection(u => u.Roles).IsLoaded)
            {
                return;
            }
            var userId = user.Id;
            await userRoles.Where(uc => uc.UserId.Equals(userId)).LoadAsync().WithCurrentCulture();
            Context.Entry(user).Collection(u => u.Roles).IsLoaded = true;
        }

        private bool AreLoginsLoaded(TUser user)
        {
            return Context.Entry(user).Collection(u => u.Logins).IsLoaded;
        }

        private async Task EnsureLoginsLoadedAsync(TUser user)
        {
            if (AreLoginsLoaded(user))
            {
                return;
            }
            var userId = user.Id;
            await logins.Where(uc => uc.UserId.Equals(userId)).LoadAsync().WithCurrentCulture();
            Context.Entry(user).Collection(u => u.Logins).IsLoaded = true;
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        private static class FindByIdFilterParser
        {
            private static readonly Expression<Func<TUser, bool>> Predicate = u => u.Id.Equals(default!);

            // ReSharper disable once StaticMemberInGenericType
            private static readonly MethodInfo EqualsMethodInfo
                = ((MethodCallExpression)Predicate.Body).Method;

            // ReSharper disable once StaticMemberInGenericType
            private static readonly MemberInfo UserIdMemberInfo
                = ((MemberExpression)((MethodCallExpression)Predicate.Body).Object!).Member;

            internal static bool TryMatchAndGetId(Expression<Func<TUser, bool>> filter, out TKey? id)
            {
                id = default!;
                if (filter.Body.NodeType != ExpressionType.Call)
                {
                    return false;
                }
                var methodCallExpression = (MethodCallExpression)filter.Body;
                if (methodCallExpression.Method != EqualsMethodInfo)
                {
                    return false;
                }
                if (methodCallExpression.Object is not { NodeType: ExpressionType.MemberAccess }
                    || ((MemberExpression)methodCallExpression.Object).Member != UserIdMemberInfo)
                {
                    return false;
                }
                if (methodCallExpression.Arguments.Count != 1)
                {
                    return false;
                }
                MemberExpression memberExpression;
                if (methodCallExpression.Arguments[0].NodeType == ExpressionType.Convert)
                {
                    var unaryExpression = (UnaryExpression)methodCallExpression.Arguments[0];
                    if (unaryExpression.Operand.NodeType != ExpressionType.MemberAccess)
                    {
                        return false;
                    }
                    memberExpression = (MemberExpression)unaryExpression.Operand;
                }
                else
                {
                    if (methodCallExpression.Arguments[0].NodeType != ExpressionType.MemberAccess)
                    {
                        return false;
                    }
                    memberExpression = (MemberExpression)methodCallExpression.Arguments[0];
                }
                if (memberExpression.Member.MemberType != MemberTypes.Field
                    || memberExpression.Expression!.NodeType != ExpressionType.Constant)
                {
                    return false;
                }
                var fieldInfo = (FieldInfo)memberExpression.Member;
                var value = ((ConstantExpression)memberExpression.Expression).Value;
                id = (TKey?)fieldInfo.GetValue(value);
                return true;
            }
        }
    }
}

#region Assembly Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// location unknown
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

namespace Microsoft.AspNet.Identity.EntityFramework2
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.Interfaces.DataModel;

    // Summary:
    //     EntityFramework based IIdentityEntityStore that allows query/manipulation of
    //     a TEntity set
    //
    // Type parameters:
    //   TEntity:
    //     Concrete entity type, i.e .User
    internal class EntityStore<TEntity>
        where TEntity : class
    {
        // Summary:
        //     Constructor that takes a Context
        //
        // Parameters:
        //   context:
        public EntityStore(IDbContext context)
        {
            Context = context;
            DbEntitySet = context.Set<TEntity>();
        }

        // Summary:
        //     Context for the store
        public IDbContext Context { get; }

        // Summary:
        //     EntitySet for this store
        public DbSet<TEntity> DbEntitySet { get; }

        // Summary:
        //     Used to query the entities
        public IQueryable<TEntity> EntitySet => DbEntitySet;

        // Summary:
        //     FindAsync an entity by ID
        //
        // Parameters:
        //   id:
        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return DbEntitySet.FindAsync(id);
        }

        // Summary:
        //     Insert an entity
        //
        // Parameters:
        //   entity:
        public void Create(TEntity entity)
        {
            DbEntitySet.Add(entity);
        }

        // Summary:
        //     Mark an entity for deletion
        //
        // Parameters:
        //   entity:
        public void Delete(TEntity entity)
        {
            DbEntitySet.Remove(entity);
        }

        // Summary:
        //     Update an entity
        //
        // Parameters:
        //   entity:
        public virtual void Update(TEntity? entity)
        {
            if (entity is not null)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
