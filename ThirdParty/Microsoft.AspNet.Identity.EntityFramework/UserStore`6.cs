// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.EntityFramework.UserStore`6
// Assembly: Microsoft.AspNet.Identity.EntityFramework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 67CFF174-6DA7-49B9-BAF6-D5DB9B7CBCAA
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.entityframework\2.2.3\lib\net45\Microsoft.AspNet.Identity.EntityFramework.dll

namespace Microsoft.AspNet.Identity.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using TaskExtensions = System.Data.Entity.SqlServer.Utilities.TaskExtensions;

    /// <summary>EntityFramework based user store implementation that supports IUserStore, IUserLoginStore,
    /// IUserClaimStore and IUserRoleStore.</summary>
    /// <typeparam name="TUser">     .</typeparam>
    /// <typeparam name="TRole">     .</typeparam>
    /// <typeparam name="TKey">      .</typeparam>
    /// <typeparam name="TUserLogin">.</typeparam>
    /// <typeparam name="TUserRole"> .</typeparam>
    /// <typeparam name="TUserClaim">.</typeparam>
    /// <seealso cref="IUserLoginStore{TUser,TKey}"/>
    /// <seealso cref="IUserStore{TUser,TKey}"/>
    /// <seealso cref="IDisposable"/>
    /// <seealso cref="IUserClaimStore{TUser,TKey}"/>
    /// <seealso cref="IUserRoleStore{TUser,TKey}"/>
    /// <seealso cref="IUserPasswordStore{TUser,TKey}"/>
    /// <seealso cref="IUserSecurityStampStore{TUser,TKey}"/>
    /// <seealso cref="IQueryableUserStore{TUser,TKey}"/>
    /// <seealso cref="IUserEmailStore{TUser,TKey}"/>
    /// <seealso cref="IUserPhoneNumberStore{TUser,TKey}"/>
    /// <seealso cref="IUserTwoFactorStore{TUser,TKey}"/>
    /// <seealso cref="IUserLockoutStore{TUser,TKey}"/>
    /// <seealso cref="IUserLoginStore{TUser,TKey}"/>
    /// <seealso cref="IUserStore{TUser,TKey}"/>
    /// <seealso cref="IUserClaimStore{TUser,TKey}"/>
    /// <seealso cref="IUserRoleStore{TUser,TKey}"/>
    /// <seealso cref="IUserPasswordStore{TUser,TKey}"/>
    /// <seealso cref="IUserSecurityStampStore{TUser,TKey}"/>
    /// <seealso cref="IQueryableUserStore{TUser,TKey}"/>
    /// <seealso cref="IUserEmailStore{TUser,TKey}"/>
    /// <seealso cref="IUserPhoneNumberStore{TUser,TKey}"/>
    /// <seealso cref="IUserTwoFactorStore{TUser,TKey}"/>
    /// <seealso cref="IUserLockoutStore{TUser,TKey}"/>
    public class UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim>
        : IUserLoginStore<TUser, TKey>,
          IUserStore<TUser, TKey>,
          IDisposable,
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
        /// <summary>The logins.</summary>
        private readonly IDbSet<TUserLogin> _logins;

        /// <summary>The role store.</summary>
        private readonly EntityStore<TRole> _roleStore;

        /// <summary>The user claims.</summary>
        private readonly IDbSet<TUserClaim> _userClaims;

        /// <summary>The user roles.</summary>
        private readonly IDbSet<TUserRole> _userRoles;

        /// <summary>True if disposed.</summary>
        private bool _disposed;

        /// <summary>The user store.</summary>
        private EntityStore<TUser> _userStore;

        /// <summary>Constructor which takes a db context and wires up the stores with default instances using the
        /// context.</summary>
        /// <param name="context">.</param>
        public UserStore(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            AutoSaveChanges = true;
            _userStore = new EntityStore<TUser>(context);
            _roleStore = new EntityStore<TRole>(context);
            _logins = Context.Set<TUserLogin>();
            _userClaims = Context.Set<TUserClaim>();
            _userRoles = Context.Set<TUserRole>();
        }

        /// <summary>If true will call SaveChanges after Create/Update/Delete.</summary>
        /// <value>True if automatic save changes, false if not.</value>
        public bool AutoSaveChanges { get; set; }

        /// <summary>Context for the store.</summary>
        /// <value>The context.</value>
        public DbContext Context { get; private set; }

        /// <summary>If true will call dispose on the DbContext during Dispose.</summary>
        /// <value>True if dispose context, false if not.</value>
        public bool DisposeContext { get; set; }

        /// <summary>Returns an IQueryable of users.</summary>
        /// <value>The users.</value>
        public IQueryable<TUser> Users => _userStore.EntitySet;

        /// <summary>Add a claim to a user.</summary>
        /// <param name="user"> .</param>
        /// <param name="claim">.</param>
        /// <returns>A Task.</returns>
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
            var userClaims = _userClaims;
            var userClaim = new TUserClaim
            {
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };
            userClaims.Add(userClaim);
            return Task.FromResult(0);
        }

        /// <summary>Add a login to the user.</summary>
        /// <param name="user"> .</param>
        /// <param name="login">.</param>
        /// <returns>A Task.</returns>
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
            var logins = _logins;
            var userLogin = new TUserLogin
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider
            };
            logins.Add(userLogin);
            return Task.FromResult(0);
        }

        /// <summary>Add a user to a role.</summary>
        /// <param name="user">    .</param>
        /// <param name="roleName">.</param>
        /// <returns>A Task.</returns>
        public virtual async Task AddToRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, nameof(roleName));
            }
            var role = await TaskExtensions.WithCurrentCulture(
                _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper()));
            if (role == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, IdentityResources.RoleNotFound, roleName));
            }
            _userRoles.Add(new TUserRole { UserId = user.Id, RoleId = role.Id });
        }

        /// <summary>Insert an entity.</summary>
        /// <param name="user">.</param>
        /// <returns>The new asynchronous.</returns>
        public virtual async Task CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userStore.Create(user);
            await TaskExtensions.WithCurrentCulture(SaveChanges());
        }

        /// <summary>Mark an entity for deletion.</summary>
        /// <param name="user">.</param>
        /// <returns>A Task.</returns>
        public virtual async Task DeleteAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userStore.Delete(user);
            await TaskExtensions.WithCurrentCulture(SaveChanges());
        }

        /// <summary>Dispose the store.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Returns the user associated with this login.</summary>
        /// <param name="login">The login.</param>
        /// <returns>The found asynchronous.</returns>
        public virtual async Task<TUser> FindAsync(UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var userLogin = await TaskExtensions.WithCurrentCulture(
                _logins.FirstOrDefaultAsync(
                    l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey));
            if (userLogin == null)
            {
                return default;
            }
            var userId = userLogin.UserId;
            return await TaskExtensions.WithCurrentCulture(GetUserAggregateAsync(u => u.Id.Equals(userId)));
        }

        /// <summary>Find a user by email.</summary>
        /// <param name="email">.</param>
        /// <returns>The found email asynchronous.</returns>
        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.Email.ToUpper() == email.ToUpper());
        }

        /// <summary>Find a user by id.</summary>
        /// <param name="userId">.</param>
        /// <returns>The found identifier asynchronous.</returns>
        public virtual Task<TUser> FindByIdAsync(TKey userId)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.Id.Equals(userId));
        }

        /// <summary>Find a user by name.</summary>
        /// <param name="userName">.</param>
        /// <returns>The found name asynchronous.</returns>
        public virtual Task<TUser> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.UserName.ToUpper() == userName.ToUpper());
        }

        /// <summary>Returns the current number of failed access attempts.  This number usually will be reset whenever
        /// the password is verified or the account is locked out.</summary>
        /// <param name="user">.</param>
        /// <returns>The access failed count asynchronous.</returns>
        public virtual Task<int> GetAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>Return the claims for a user.</summary>
        /// <param name="user">.</param>
        /// <returns>The claims asynchronous.</returns>
        public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await TaskExtensions.WithCurrentCulture(EnsureClaimsLoaded(user));
            return user.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
        }

        /// <summary>Get the user's email.</summary>
        /// <param name="user">.</param>
        /// <returns>The email asynchronous.</returns>
        public virtual Task<string> GetEmailAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Email);
        }

        /// <summary>Returns whether the user email is confirmed.</summary>
        /// <param name="user">.</param>
        /// <returns>The email confirmed asynchronous.</returns>
        public virtual Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.EmailConfirmed);
        }

        /// <summary>Returns whether the user can be locked out.</summary>
        /// <param name="user">.</param>
        /// <returns>The lockout enabled asynchronous.</returns>
        public virtual Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.LockoutEnabled);
        }

        /// <summary>Returns the DateTimeOffset that represents the end of a user's lockout, any time in the past should
        /// be considered not locked out.</summary>
        /// <param name="user">.</param>
        /// <returns>The lockout end date asynchronous.</returns>
        public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(
                user.LockoutEndDateUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        /// <summary>Get the logins for a user.</summary>
        /// <param name="user">.</param>
        /// <returns>The logins asynchronous.</returns>
        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await TaskExtensions.WithCurrentCulture(EnsureLoginsLoaded(user));
            return user.Logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToList();
        }

        /// <summary>Get the password hash for a user.</summary>
        /// <param name="user">.</param>
        /// <returns>The password hash asynchronous.</returns>
        public virtual Task<string> GetPasswordHashAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PasswordHash);
        }

        /// <summary>Get a user's phone number.</summary>
        /// <param name="user">.</param>
        /// <returns>The phone number asynchronous.</returns>
        public virtual Task<string> GetPhoneNumberAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PhoneNumber);
        }

        /// <summary>Returns whether the user phoneNumber is confirmed.</summary>
        /// <param name="user">.</param>
        /// <returns>The phone number confirmed asynchronous.</returns>
        public virtual Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        /// <summary>Get the names of the roles a user is a member of.</summary>
        /// <param name="user">.</param>
        /// <returns>The roles asynchronous.</returns>
        public virtual async Task<IList<string>> GetRolesAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await TaskExtensions.WithCurrentCulture(
                _userRoles.Where(userRole => userRole.UserId.Equals(user.Id))
                    .Join(
                        _roleStore.DbEntitySet,
                        userRole => userRole.RoleId,
                        role => role.Id,
                        (userRole, role) => role.Name)
                    .ToListAsync());
        }

        /// <summary>Get the security stamp for a user.</summary>
        /// <param name="user">.</param>
        /// <returns>The security stamp asynchronous.</returns>
        public virtual Task<string> GetSecurityStampAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.SecurityStamp);
        }

        /// <summary>Gets whether two factor authentication is enabled for the user.</summary>
        /// <param name="user">.</param>
        /// <returns>The two factor enabled asynchronous.</returns>
        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.TwoFactorEnabled);
        }

        /// <summary>Returns true if the user has a password set.</summary>
        /// <param name="user">.</param>
        /// <returns>A Task{bool}</returns>
        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        /// <summary>Used to record when an attempt to access the user has failed.</summary>
        /// <param name="user">.</param>
        /// <returns>A Task{int}</returns>
        public virtual Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            ++user.AccessFailedCount;
            return Task.FromResult(user.AccessFailedCount);
        }

        /// <summary>Returns true if the user is in the named role.</summary>
        /// <param name="user">    .</param>
        /// <param name="roleName">.</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, nameof(roleName));
            }
            var role = await TaskExtensions.WithCurrentCulture(
                _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper()));
            if (role == null)
            {
                return false;
            }
            return await TaskExtensions.WithCurrentCulture(
                _userRoles.AnyAsync(ur => ur.RoleId.Equals(role.Id) && ur.UserId.Equals(user.Id)));
        }

        /// <summary>Remove a claim from a user.</summary>
        /// <param name="user"> .</param>
        /// <param name="claim">.</param>
        /// <returns>A Task.</returns>
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
            IEnumerable<TUserClaim> userClaims;
            if (AreClaimsLoaded(user))
            {
                userClaims = user.Claims.Where(uc => uc.ClaimValue == claimValue && uc.ClaimType == claimType).ToList();
            }
            else
            {
                var userId = user.Id;
                userClaims = await TaskExtensions.WithCurrentCulture(
                    _userClaims.Where(
                            uc => uc.ClaimValue == claimValue && uc.ClaimType == claimType && uc.UserId.Equals(userId))
                        .ToListAsync());
            }
            foreach (var tUserClaim in userClaims)
            {
                _userClaims.Remove(tUserClaim);
            }
        }

        /// <summary>Remove a user from a role.</summary>
        /// <param name="user">    .</param>
        /// <param name="roleName">.</param>
        /// <returns>A Task.</returns>
        public virtual async Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, nameof(roleName));
            }
            var role = await TaskExtensions.WithCurrentCulture(
                _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper()));
            if (role == null)
            {
                return;
            }
            var userRole = await TaskExtensions.WithCurrentCulture(
                _userRoles.FirstOrDefaultAsync(r => role.Id.Equals(r.RoleId) && r.UserId.Equals(user.Id)));
            if (userRole != null)
            {
                _userRoles.Remove(userRole);
            }
        }

        /// <summary>Remove a login from a user.</summary>
        /// <param name="user"> .</param>
        /// <param name="login">.</param>
        /// <returns>A Task.</returns>
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
            TUserLogin userLogin;
            if (AreLoginsLoaded(user))
            {
                userLogin = user.Logins.SingleOrDefault(
                    ul => ul.LoginProvider == login.LoginProvider && ul.ProviderKey == login.ProviderKey);
            }
            else
            {
                userLogin = await TaskExtensions.WithCurrentCulture(
                    _logins.SingleOrDefaultAsync(
                        ul => ul.LoginProvider == login.LoginProvider
                            && ul.ProviderKey == login.ProviderKey
                            && ul.UserId.Equals(user.Id)));
            }
            if (userLogin != null)
            {
                _logins.Remove(userLogin);
            }
        }

        /// <summary>Used to reset the account access count, typically after the account is successfully accessed.</summary>
        /// <param name="user">.</param>
        /// <returns>A Task.</returns>
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

        /// <summary>Set the user email.</summary>
        /// <param name="user"> .</param>
        /// <param name="email">.</param>
        /// <returns>A Task.</returns>
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

        /// <summary>Set IsConfirmed on the user.</summary>
        /// <param name="user">     .</param>
        /// <param name="confirmed">.</param>
        /// <returns>A Task.</returns>
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

        /// <summary>Sets whether the user can be locked out.</summary>
        /// <param name="user">   .</param>
        /// <param name="enabled">.</param>
        /// <returns>A Task.</returns>
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

        /// <summary>Locks a user out until the specified end date (set to a past date, to unlock a user)</summary>
        /// <param name="user">      .</param>
        /// <param name="lockoutEnd">.</param>
        /// <returns>A Task.</returns>
        public virtual Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? new DateTime?() : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        /// <summary>Set the password hash for a user.</summary>
        /// <param name="user">        .</param>
        /// <param name="passwordHash">.</param>
        /// <returns>A Task.</returns>
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

        /// <summary>Set the user's phone number.</summary>
        /// <param name="user">       .</param>
        /// <param name="phoneNumber">.</param>
        /// <returns>A Task.</returns>
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

        /// <summary>Set PhoneNumberConfirmed on the user.</summary>
        /// <param name="user">     .</param>
        /// <param name="confirmed">.</param>
        /// <returns>A Task.</returns>
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

        /// <summary>Set the security stamp for the user.</summary>
        /// <param name="user"> .</param>
        /// <param name="stamp">.</param>
        /// <returns>A Task.</returns>
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

        /// <summary>Set whether two factor authentication is enabled for the user.</summary>
        /// <param name="user">   .</param>
        /// <param name="enabled">.</param>
        /// <returns>A Task.</returns>
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

        /// <summary>Update an entity.</summary>
        /// <param name="user">.</param>
        /// <returns>A Task.</returns>
        public virtual async Task UpdateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _userStore.Update(user);
            await TaskExtensions.WithCurrentCulture(SaveChanges());
        }

        /// <summary>If disposing, calls dispose on the Context.  Always nulls out the Context.</summary>
        /// <param name="disposing">.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (DisposeContext & disposing && Context != null)
            {
                Context.Dispose();
            }
            _disposed = true;
            Context = null;
            _userStore = null;
        }

        /// <summary>Used to attach child entities to the User aggregate, i.e. Roles, Logins, and Claims.</summary>
        /// <param name="filter">.</param>
        /// <returns>The user aggregate asynchronous.</returns>
        protected virtual async Task<TUser> GetUserAggregateAsync(Expression<Func<TUser, bool>> filter)
        {
            var user = FindByIdFilterParser.TryMatchAndGetId(filter, out var id)
                ? await TaskExtensions.WithCurrentCulture(_userStore.GetByIdAsync(id))
                : await TaskExtensions.WithCurrentCulture(Users.FirstOrDefaultAsync(filter));
            if (user != null)
            {
                await TaskExtensions.WithCurrentCulture(EnsureClaimsLoaded(user));
                await TaskExtensions.WithCurrentCulture(EnsureLoginsLoaded(user));
                await TaskExtensions.WithCurrentCulture(EnsureRolesLoaded(user));
            }
            return user;
        }

        /// <summary>Determine if we are claims loaded.</summary>
        /// <param name="user">The user.</param>
        /// <returns>True if claims loaded, false if not.</returns>
        private bool AreClaimsLoaded(TUser user)
        {
            return Context.Entry(user).Collection(u => u.Claims).IsLoaded;
        }

        /// <summary>Determine if we are logins loaded.</summary>
        /// <param name="user">The user.</param>
        /// <returns>True if logins loaded, false if not.</returns>
        private bool AreLoginsLoaded(TUser user)
        {
            return Context.Entry(user).Collection(u => u.Logins).IsLoaded;
        }

        /// <summary>Ensures that claims loaded.</summary>
        /// <param name="user">The user.</param>
        /// <returns>A Task.</returns>
#pragma warning disable IDE1006 // Naming Styles
        private async Task EnsureClaimsLoaded(TUser user)
#pragma warning restore IDE1006 // Naming Styles
        {
            if (AreClaimsLoaded(user))
            {
                return;
            }
            await TaskExtensions.WithCurrentCulture(_userClaims.Where(uc => uc.UserId.Equals(user.Id)).LoadAsync());
            Context.Entry(user).Collection(u => u.Claims).IsLoaded = true;
        }

        /// <summary>Ensures that logins loaded.</summary>
        /// <param name="user">The user.</param>
        /// <returns>A Task.</returns>
#pragma warning disable IDE1006 // Naming Styles
        private async Task EnsureLoginsLoaded(TUser user)
#pragma warning restore IDE1006 // Naming Styles
        {
            if (AreLoginsLoaded(user))
            {
                return;
            }
            await TaskExtensions.WithCurrentCulture(_logins.Where(uc => uc.UserId.Equals(user.Id)).LoadAsync());
            Context.Entry(user).Collection(u => u.Logins).IsLoaded = true;
        }

        /// <summary>Ensures that roles loaded.</summary>
        /// <param name="user">The user.</param>
        /// <returns>A Task.</returns>
#pragma warning disable IDE1006 // Naming Styles
        private async Task EnsureRolesLoaded(TUser user)
#pragma warning restore IDE1006 // Naming Styles
        {
            if (Context.Entry(user).Collection(u => u.Roles).IsLoaded)
            {
                return;
            }
            await TaskExtensions.WithCurrentCulture(_userRoles.Where(uc => uc.UserId.Equals(user.Id)).LoadAsync());
            Context.Entry(user).Collection(u => u.Roles).IsLoaded = true;
        }

        /// <summary>Saves the changes.</summary>
        /// <returns>A Task.</returns>
#pragma warning disable IDE1006 // Naming Styles
        private async Task SaveChanges()
#pragma warning restore IDE1006 // Naming Styles
        {
            if (!AutoSaveChanges)
            {
                return;
            }
            await TaskExtensions.WithCurrentCulture(Context.SaveChangesAsync());
        }

        /// <summary>Throw if disposed.</summary>
        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>A find by identifier filter parser.</summary>
        private static class FindByIdFilterParser
        {
            /// <summary>The predicate.</summary>
            private static readonly Expression<Func<TUser, bool>> Predicate = u => u.Id.Equals(default);

            /// <summary>Information describing the equals method.</summary>
            private static readonly MethodInfo EqualsMethodInfo = ((MethodCallExpression)Predicate.Body).Method;

            /// <summary>Information describing the user identifier member.</summary>
            private static readonly MemberInfo UserIdMemberInfo =
                ((MemberExpression)((MethodCallExpression)Predicate.Body).Object).Member;

            /// <summary>Attempts to match and get identifier from the given data.</summary>
            /// <param name="filter">Specifies the filter.</param>
            /// <param name="id">    The identifier.</param>
            /// <returns>True if it succeeds, false if it fails.</returns>
            internal static bool TryMatchAndGetId(Expression<Func<TUser, bool>> filter, out TKey id)
            {
                id = default;
                if (filter.Body.NodeType != ExpressionType.Call)
                {
                    return false;
                }
                var body = (MethodCallExpression)filter.Body;
                if (body.Method != EqualsMethodInfo
                    || body.Object == null
                    || body.Object.NodeType != ExpressionType.MemberAccess
                    || ((MemberExpression)body.Object).Member != UserIdMemberInfo
                    || body.Arguments.Count != 1)
                {
                    return false;
                }
                MemberExpression item;
                if (body.Arguments[0].NodeType == ExpressionType.Convert)
                {
                    var unaryExpression = (UnaryExpression)body.Arguments[0];
                    if (unaryExpression.Operand.NodeType != ExpressionType.MemberAccess)
                    {
                        return false;
                    }
                    item = (MemberExpression)unaryExpression.Operand;
                }
                else
                {
                    if (body.Arguments[0].NodeType != ExpressionType.MemberAccess)
                    {
                        return false;
                    }
                    item = (MemberExpression)body.Arguments[0];
                }
                if (item.Member.MemberType != MemberTypes.Field || item.Expression.NodeType != ExpressionType.Constant)
                {
                    return false;
                }
                var member = (FieldInfo)item.Member;
                id = (TKey)member.GetValue(((ConstantExpression)item.Expression).Value);
                return true;
            }
        }
    }
}
