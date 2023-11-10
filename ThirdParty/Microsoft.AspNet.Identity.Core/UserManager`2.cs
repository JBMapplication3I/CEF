// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.UserManager`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>Exposes user related api which will automatically save changes to the UserStore.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    /// <seealso cref="IDisposable"/>
    public class UserManager<TUser, TKey> : IDisposable
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>The factors.</summary>
        private readonly Dictionary<string, IUserTokenProvider<TUser, TKey>> _factors = new();

        /// <summary>The claims factory.</summary>
        private IClaimsIdentityFactory<TUser, TKey> _claimsFactory;

        /// <summary>True if disposed.</summary>
        private bool _disposed;

        /// <summary>The password hasher.</summary>
        private IPasswordHasher _passwordHasher;

        /// <summary>The password validator.</summary>
        private IIdentityValidator<string> _passwordValidator;

        /// <summary>The user validator.</summary>
        private IIdentityValidator<TUser> _userValidator;

        /// <summary>Constructor.</summary>
        /// <param name="store">The IUserStore is responsible for commiting changes via the UpdateAsync/CreateAsync
        ///                     methods.</param>
        public UserManager(IUserStore<TUser, TKey> store)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
            UserValidator = new UserValidator<TUser, TKey>(this);
            PasswordValidator = new MinimumLengthValidator(6);
            PasswordHasher = new PasswordHasher();
            ClaimsIdentityFactory = new ClaimsIdentityFactory<TUser, TKey>();
        }

        /// <summary>Used to create claims identities from users.</summary>
        /// <value>The claims identity factory.</value>
        public IClaimsIdentityFactory<TUser, TKey> ClaimsIdentityFactory
        {
            get
            {
                ThrowIfDisposed();
                return _claimsFactory;
            }

            set
            {
                ThrowIfDisposed();
                _claimsFactory = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>Default amount of time that a user is locked out for after MaxFailedAccessAttemptsBeforeLockout is
        /// reached.</summary>
        /// <value>The default account lockout time span.</value>
        public TimeSpan DefaultAccountLockoutTimeSpan { get; set; } = TimeSpan.Zero;

        /// <summary>Used to send email.</summary>
        /// <value>The email service.</value>
        public IIdentityMessageService EmailService { get; set; }

        /// <summary>Number of access attempts allowed before a user is locked out (if lockout is enabled)</summary>
        /// <value>The maximum failed access attempts before lockout.</value>
        public int MaxFailedAccessAttemptsBeforeLockout { get; set; }

        /// <summary>Used to hash/verify passwords.</summary>
        /// <value>The password hasher.</value>
        public IPasswordHasher PasswordHasher
        {
            get
            {
                ThrowIfDisposed();
                return _passwordHasher;
            }

            set
            {
                ThrowIfDisposed();
                _passwordHasher = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>Used to validate passwords before persisting changes.</summary>
        /// <value>The password validator.</value>
        public IIdentityValidator<string> PasswordValidator
        {
            get
            {
                ThrowIfDisposed();
                return _passwordValidator;
            }

            set
            {
                ThrowIfDisposed();
                _passwordValidator = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>Used to send a sms message.</summary>
        /// <value>The SMS service.</value>
        public IIdentityMessageService SmsService { get; set; }

        /// <summary>Returns true if the store is an IQueryableUserStore.</summary>
        /// <value>True if supports queryable users, false if not.</value>
        public virtual bool SupportsQueryableUsers
        {
            get
            {
                ThrowIfDisposed();
                return Store is IQueryableUserStore<TUser, TKey>;
            }
        }

        /// <summary>Returns true if the store is an IUserClaimStore.</summary>
        /// <value>True if supports user claim, false if not.</value>
        public virtual bool SupportsUserClaim
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserClaimStore<TUser, TKey>;
            }
        }

        /// <summary>Returns true if the store is an IUserEmailStore.</summary>
        /// <value>True if supports user email, false if not.</value>
        public virtual bool SupportsUserEmail
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserEmailStore<TUser, TKey>;
            }
        }

        /// <summary>Returns true if the store is an IUserLockoutStore.</summary>
        /// <value>True if supports user lockout, false if not.</value>
        public virtual bool SupportsUserLockout
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserLockoutStore<TUser, TKey>;
            }
        }

        /// <summary>Returns true if the store is an IUserLoginStore.</summary>
        /// <value>True if supports user login, false if not.</value>
        public virtual bool SupportsUserLogin
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserLoginStore<TUser, TKey>;
            }
        }

        /// <summary>Returns true if the store is an IUserPasswordStore.</summary>
        /// <value>True if supports user password, false if not.</value>
        public virtual bool SupportsUserPassword
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserPasswordStore<TUser, TKey>;
            }
        }

        /// <summary>Returns true if the store is an IUserPhoneNumberStore.</summary>
        /// <value>True if supports user phone number, false if not.</value>
        public virtual bool SupportsUserPhoneNumber
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserPhoneNumberStore<TUser, TKey>;
            }
        }

        /// <summary>Returns true if the store is an IUserRoleStore.</summary>
        /// <value>True if supports user role, false if not.</value>
        public virtual bool SupportsUserRole
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserRoleStore<TUser, TKey>;
            }
        }

        /// <summary>Returns true if the store is an IUserSecurityStore.</summary>
        /// <value>True if supports user security stamp, false if not.</value>
        public virtual bool SupportsUserSecurityStamp
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserSecurityStampStore<TUser, TKey>;
            }
        }

        /// <summary>Returns true if the store is an IUserTwoFactorStore.</summary>
        /// <value>True if supports user two factor, false if not.</value>
        public virtual bool SupportsUserTwoFactor
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserTwoFactorStore<TUser, TKey>;
            }
        }

        /// <summary>Maps the registered two-factor authentication providers for users by their id.</summary>
        /// <value>The two factor providers.</value>
        public IDictionary<string, IUserTokenProvider<TUser, TKey>> TwoFactorProviders => _factors;

        /// <summary>If true, will enable user lockout when users are created.</summary>
        /// <value>True if user lockout enabled by default, false if not.</value>
        public bool UserLockoutEnabledByDefault { get; set; }

        /// <summary>Returns an IQueryable of users if the store is an IQueryableUserStore.</summary>
        /// <value>The users.</value>
        public virtual IQueryable<TUser> Users
            => Store is IQueryableUserStore<TUser, TKey> store
                ? store.Users
                : throw new NotSupportedException(Resources.StoreNotIQueryableUserStore);

        /// <summary>Used for generating reset password and confirmation tokens.</summary>
        /// <value>The user token provider.</value>
        public IUserTokenProvider<TUser, TKey> UserTokenProvider { get; set; }

        /// <summary>Used to validate users before changes are saved.</summary>
        /// <value>The user validator.</value>
        public IIdentityValidator<TUser> UserValidator
        {
            get
            {
                ThrowIfDisposed();
                return _userValidator;
            }

            set
            {
                ThrowIfDisposed();
                _userValidator = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>Persistence abstraction that the UserManager operates against.</summary>
        /// <value>The store.</value>
        protected internal IUserStore<TUser, TKey> Store { get; set; }

        /// <summary>Increments the access failed count for the user and if the failed access account is greater than or
        /// equal to the MaxFailedAccessAttempsBeforeLockout, the user will be locked out for the next
        /// DefaultAccountLockoutTimeSpan and the AccessFailedCount will be reset to 0. This is used for locking out the
        /// user account.</summary>
        /// <param name="userId">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> AccessFailedAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (await store.IncrementAccessFailedCountAsync(user).WithCurrentCulture()
                >= MaxFailedAccessAttemptsBeforeLockout)
            {
                var cultureAwaiter = store.SetLockoutEndDateAsync(
                        user,
                        DateTimeOffset.UtcNow.Add(DefaultAccountLockoutTimeSpan))
                    .WithCurrentCulture();
                await cultureAwaiter;
                cultureAwaiter = store.ResetAccessFailedCountAsync(user).WithCurrentCulture();
                await cultureAwaiter;
            }
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Add a user claim.</summary>
        /// <param name="userId">.</param>
        /// <param name="claim"> .</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> AddClaimAsync(TKey userId, Claim claim)
        {
            ThrowIfDisposed();
            var claimStore = GetClaimStore();
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await claimStore.AddClaimAsync(user, claim).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Associate a login with a user.</summary>
        /// <param name="userId">.</param>
        /// <param name="login"> .</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> AddLoginAsync(TKey userId, UserLoginInfo login)
        {
            ThrowIfDisposed();
            var loginStore = GetLoginStore();
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (await FindAsync(login).WithCurrentCulture() != null)
            {
                return IdentityResult.Failed(Resources.ExternalLoginExists);
            }
            await loginStore.AddLoginAsync(user, login).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Add a user password only if one does not already exist.</summary>
        /// <param name="userId">  .</param>
        /// <param name="password">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> AddPasswordAsync(TKey userId, string password)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (await passwordStore.GetPasswordHashAsync(user).WithCurrentCulture() != null)
            {
                return new IdentityResult(Resources.UserAlreadyHasPassword);
            }
            var identityResult = await UpdatePassword(passwordStore, user, password).WithCurrentCulture();
            return !identityResult.Succeeded ? identityResult : await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Add a user to a role.</summary>
        /// <param name="userId">.</param>
        /// <param name="role">  .</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> AddToRoleAsync(TKey userId, string role)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if ((await userRoleStore.GetRolesAsync(user).WithCurrentCulture()).Contains(role))
            {
                return new IdentityResult(Resources.UserAlreadyInRole);
            }
            await userRoleStore.AddToRoleAsync(user, role).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Method to add user to multiple roles.</summary>
        /// <param name="userId">user id.</param>
        /// <param name="roles"> list of role names.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> AddToRolesAsync(TKey userId, params string[] roles)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            if (roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
            var strArray = roles;
            for (var index = 0; index < strArray.Length; ++index)
            {
                var roleName = strArray[index];
                if (userRoles.Contains(roleName))
                {
                    return new IdentityResult(Resources.UserAlreadyInRole);
                }
                await userRoleStore.AddToRoleAsync(user, roleName).WithCurrentCulture();
            }
            strArray = null;
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Change a user password.</summary>
        /// <param name="userId">         .</param>
        /// <param name="currentPassword">.</param>
        /// <param name="newPassword">    .</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> ChangePasswordAsync(
            TKey userId,
            string currentPassword,
            string newPassword)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (await VerifyPasswordAsync(passwordStore, user, currentPassword).WithCurrentCulture())
            {
                var identityResult = await UpdatePassword(passwordStore, user, newPassword).WithCurrentCulture();
                return !identityResult.Succeeded ? identityResult : await UpdateAsync(user).WithCurrentCulture();
            }
            return IdentityResult.Failed(Resources.PasswordMismatch);
        }

        /// <summary>Set a user's phoneNumber with the verification token.</summary>
        /// <param name="userId">     .</param>
        /// <param name="phoneNumber">.</param>
        /// <param name="token">      .</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> ChangePhoneNumberAsync(TKey userId, string phoneNumber, string token)
        {
            ThrowIfDisposed();
            var store = GetPhoneNumberStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (await VerifyChangePhoneNumberTokenAsync(userId, token, phoneNumber).WithCurrentCulture())
            {
                var cultureAwaiter = store.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
                await cultureAwaiter;
                cultureAwaiter = store.SetPhoneNumberConfirmedAsync(user, true).WithCurrentCulture();
                await cultureAwaiter;
                cultureAwaiter = UpdateSecurityStampInternal(user).WithCurrentCulture();
                await cultureAwaiter;
                return await UpdateAsync(user).WithCurrentCulture();
            }
            return IdentityResult.Failed(Resources.InvalidToken);
        }

        /// <summary>Returns true if the password is valid for the user.</summary>
        /// <param name="user">    .</param>
        /// <param name="password">.</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> CheckPasswordAsync(TUser user, string password)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            return user != null && await VerifyPasswordAsync(passwordStore, user, password).WithCurrentCulture();
        }

        /// <summary>Confirm the user's email with confirmation token.</summary>
        /// <param name="userId">.</param>
        /// <param name="token"> .</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> ConfirmEmailAsync(TKey userId, string token)
        {
            ThrowIfDisposed();
            var store = GetEmailStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (!await VerifyUserTokenAsync(userId, "Confirmation", token).WithCurrentCulture())
            {
                return IdentityResult.Failed(Resources.InvalidToken);
            }
            await store.SetEmailConfirmedAsync(user, true).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Create a user with no password.</summary>
        /// <param name="user">.</param>
        /// <returns>The new asynchronous.</returns>
        public virtual async Task<IdentityResult> CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            await UpdateSecurityStampInternal(user).WithCurrentCulture();
            var identityResult = await UserValidator.ValidateAsync(user).WithCurrentCulture();
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }
            if (UserLockoutEnabledByDefault && SupportsUserLockout)
            {
                await GetUserLockoutStore().SetLockoutEnabledAsync(user, true).WithCurrentCulture();
            }
            await Store.CreateAsync(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>Create a user with the given password.</summary>
        /// <param name="user">    .</param>
        /// <param name="password">.</param>
        /// <returns>The new asynchronous.</returns>
        public virtual async Task<IdentityResult> CreateAsync(TUser user, string password)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            var identityResult = await UpdatePassword(passwordStore, user, password).WithCurrentCulture();
            return !identityResult.Succeeded ? identityResult : await CreateAsync(user).WithCurrentCulture();
        }

        /// <summary>Creates a ClaimsIdentity representing the user.</summary>
        /// <param name="user">              .</param>
        /// <param name="authenticationType">.</param>
        /// <returns>The new identity asynchronous.</returns>
        public virtual Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return ClaimsIdentityFactory.CreateAsync(this, user, authenticationType);
        }

        /// <summary>Delete a user.</summary>
        /// <param name="user">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> DeleteAsync(TUser user)
        {
            ThrowIfDisposed();
            await Store.DeleteAsync(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>Dispose this object.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Return a user with the specified username and password or null if there is no match.</summary>
        /// <param name="userName">.</param>
        /// <param name="password">.</param>
        /// <returns>The found asynchronous.</returns>
        public virtual async Task<TUser> FindAsync(string userName, string password)
        {
            ThrowIfDisposed();
            var user = await FindByNameAsync(userName).WithCurrentCulture();
            return user is null
                ? default
                : await CheckPasswordAsync(user, password).WithCurrentCulture()
                    ? user
                    : default;
        }

        /// <summary>Returns the user associated with this login.</summary>
        /// <param name="login">The login.</param>
        /// <returns>The found asynchronous.</returns>
        public virtual Task<TUser> FindAsync(UserLoginInfo login)
        {
            ThrowIfDisposed();
            return GetLoginStore().FindAsync(login);
        }

        /// <summary>Find a user by his email.</summary>
        /// <param name="email">.</param>
        /// <returns>The found email asynchronous.</returns>
        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            var emailStore = GetEmailStore();
            var email1 = email ?? throw new ArgumentNullException(nameof(email));
            return emailStore.FindByEmailAsync(email1);
        }

        /// <summary>Find a user by id.</summary>
        /// <param name="userId">.</param>
        /// <returns>The found identifier asynchronous.</returns>
        public virtual Task<TUser> FindByIdAsync(TKey userId)
        {
            ThrowIfDisposed();
            return Store.FindByIdAsync(userId);
        }

        /// <summary>Find a user by user name.</summary>
        /// <param name="userName">.</param>
        /// <returns>The found name asynchronous.</returns>
        public virtual Task<TUser> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            return userName != null
                ? Store.FindByNameAsync(userName)
                : throw new ArgumentNullException(nameof(userName));
        }

        /// <summary>Generate a code that the user can use to change their phone number to a specific number.</summary>
        /// <param name="userId">     .</param>
        /// <param name="phoneNumber">.</param>
        /// <returns>The change phone number token asynchronous.</returns>
        public virtual async Task<string> GenerateChangePhoneNumberTokenAsync(TKey userId, string phoneNumber)
        {
            ThrowIfDisposed();
            return Rfc6238AuthenticationService
                .GenerateCode(await CreateSecurityTokenAsync(userId).WithCurrentCulture(), phoneNumber)
                .ToString("D6", CultureInfo.InvariantCulture);
        }

        /// <summary>Get the email confirmation token for the user.</summary>
        /// <param name="userId">.</param>
        /// <returns>The email confirmation token asynchronous.</returns>
        public virtual Task<string> GenerateEmailConfirmationTokenAsync(TKey userId)
        {
            ThrowIfDisposed();
            return GenerateUserTokenAsync("Confirmation", userId);
        }

        /// <summary>Generate a password reset token for the user using the UserTokenProvider.</summary>
        /// <param name="userId">.</param>
        /// <returns>The password reset token asynchronous.</returns>
        public virtual Task<string> GeneratePasswordResetTokenAsync(TKey userId)
        {
            ThrowIfDisposed();
            return GenerateUserTokenAsync("ResetPassword", userId);
        }

        /// <summary>Get a token for a specific two factor provider.</summary>
        /// <param name="userId">           .</param>
        /// <param name="twoFactorProvider">.</param>
        /// <returns>The two factor token asynchronous.</returns>
        public virtual async Task<string> GenerateTwoFactorTokenAsync(TKey userId, string twoFactorProvider)
        {
            var manager = this;
            manager.ThrowIfDisposed();
            var user = await manager.FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (!manager._factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(
                    string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, twoFactorProvider));
            }
            return await manager._factors[twoFactorProvider]
                .GenerateAsync(twoFactorProvider, manager, user)
                .WithCurrentCulture();
        }

        /// <summary>Get a user token for a specific purpose.</summary>
        /// <param name="purpose">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The user token asynchronous.</returns>
        public virtual async Task<string> GenerateUserTokenAsync(string purpose, TKey userId)
        {
            ThrowIfDisposed();
            if (UserTokenProvider == null)
            {
                throw new NotSupportedException(Resources.NoTokenProvider);
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await UserTokenProvider.GenerateAsync(purpose, this, user).WithCurrentCulture();
        }

        /// <summary>Returns the number of failed access attempts for the user.</summary>
        /// <param name="userId">.</param>
        /// <returns>The access failed count asynchronous.</returns>
        public virtual async Task<int> GetAccessFailedCountAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await store.GetAccessFailedCountAsync(user).WithCurrentCulture();
        }

        /// <summary>Get a users's claims.</summary>
        /// <param name="userId">.</param>
        /// <returns>The claims asynchronous.</returns>
        public virtual async Task<IList<Claim>> GetClaimsAsync(TKey userId)
        {
            ThrowIfDisposed();
            var claimStore = GetClaimStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await claimStore.GetClaimsAsync(user).WithCurrentCulture();
        }

        /// <summary>Get a user's email.</summary>
        /// <param name="userId">.</param>
        /// <returns>The email asynchronous.</returns>
        public virtual async Task<string> GetEmailAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetEmailStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await store.GetEmailAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns whether lockout is enabled for the user.</summary>
        /// <param name="userId">.</param>
        /// <returns>The lockout enabled asynchronous.</returns>
        public virtual async Task<bool> GetLockoutEnabledAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await store.GetLockoutEnabledAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns when the user is no longer locked out, dates in the past are considered as not being locked
        /// out.</summary>
        /// <param name="userId">.</param>
        /// <returns>The lockout end date asynchronous.</returns>
        public virtual async Task<DateTimeOffset> GetLockoutEndDateAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await store.GetLockoutEndDateAsync(user).WithCurrentCulture();
        }

        /// <summary>Gets the logins for a user.</summary>
        /// <param name="userId">.</param>
        /// <returns>The logins asynchronous.</returns>
        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TKey userId)
        {
            ThrowIfDisposed();
            var loginStore = GetLoginStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await loginStore.GetLoginsAsync(user).WithCurrentCulture();
        }

        /// <summary>Get a user's phoneNumber.</summary>
        /// <param name="userId">.</param>
        /// <returns>The phone number asynchronous.</returns>
        public virtual async Task<string> GetPhoneNumberAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetPhoneNumberStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await store.GetPhoneNumberAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns the roles for the user.</summary>
        /// <param name="userId">.</param>
        /// <returns>The roles asynchronous.</returns>
        public virtual async Task<IList<string>> GetRolesAsync(TKey userId)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns the current security stamp for a user.</summary>
        /// <param name="userId">.</param>
        /// <returns>The security stamp asynchronous.</returns>
        public virtual async Task<string> GetSecurityStampAsync(TKey userId)
        {
            ThrowIfDisposed();
            var securityStore = GetSecurityStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await securityStore.GetSecurityStampAsync(user).WithCurrentCulture();
        }

        /// <summary>Get whether two factor authentication is enabled for a user.</summary>
        /// <param name="userId">.</param>
        /// <returns>The two factor enabled asynchronous.</returns>
        public virtual async Task<bool> GetTwoFactorEnabledAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserTwoFactorStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await store.GetTwoFactorEnabledAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns a list of valid two factor providers for a user.</summary>
        /// <param name="userId">.</param>
        /// <returns>The valid two factor providers asynchronous.</returns>
        public virtual async Task<IList<string>> GetValidTwoFactorProvidersAsync(TKey userId)
        {
            var manager = this;
            manager.ThrowIfDisposed();
            var user = await manager.FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var results = new List<string>();
            foreach (var twoFactorProvider in manager.TwoFactorProviders)
            {
                var f = twoFactorProvider;
                if (await f.Value.IsValidProviderForUserAsync(manager, user).WithCurrentCulture())
                {
                    results.Add(f.Key);
                }
                f = new KeyValuePair<string, IUserTokenProvider<TUser, TKey>>();
            }
            return results;
        }

        /// <summary>Returns true if the user has a password.</summary>
        /// <param name="userId">.</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> HasPasswordAsync(TKey userId)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await passwordStore.HasPasswordAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns true if the user's email has been confirmed.</summary>
        /// <param name="userId">.</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> IsEmailConfirmedAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetEmailStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await store.GetEmailConfirmedAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns true if the user is in the specified role.</summary>
        /// <param name="userId">.</param>
        /// <param name="role">  .</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> IsInRoleAsync(TKey userId, string role)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await userRoleStore.IsInRoleAsync(user, role).WithCurrentCulture();
        }

        /// <summary>Returns true if the user is locked out.</summary>
        /// <param name="userId">.</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> IsLockedOutAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await store.GetLockoutEnabledAsync(user).WithCurrentCulture()
                && await store.GetLockoutEndDateAsync(user).WithCurrentCulture() >= DateTimeOffset.UtcNow;
        }

        /// <summary>Returns true if the user's phone number has been confirmed.</summary>
        /// <param name="userId">.</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> IsPhoneNumberConfirmedAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetPhoneNumberStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await store.GetPhoneNumberConfirmedAsync(user).WithCurrentCulture();
        }

        /// <summary>Notify a user with a token using a specific two-factor authentication provider's Notify method.</summary>
        /// <param name="userId">           .</param>
        /// <param name="twoFactorProvider">.</param>
        /// <param name="token">            .</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> NotifyTwoFactorTokenAsync(
            TKey userId,
            string twoFactorProvider,
            string token)
        {
            var manager = this;
            manager.ThrowIfDisposed();
            var user = await manager.FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (!manager._factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(
                    string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, twoFactorProvider));
            }
            await manager._factors[twoFactorProvider].NotifyAsync(token, manager, user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>Register a two factor authentication provider with the TwoFactorProviders mapping.</summary>
        /// <param name="twoFactorProvider">.</param>
        /// <param name="provider">         .</param>
        public virtual void RegisterTwoFactorProvider(
            string twoFactorProvider,
            IUserTokenProvider<TUser, TKey> provider)
        {
            ThrowIfDisposed();
            if (twoFactorProvider == null)
            {
                throw new ArgumentNullException(nameof(twoFactorProvider));
            }
            TwoFactorProviders[twoFactorProvider] = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>Remove a user claim.</summary>
        /// <param name="userId">.</param>
        /// <param name="claim"> .</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> RemoveClaimAsync(TKey userId, Claim claim)
        {
            ThrowIfDisposed();
            var claimStore = GetClaimStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await claimStore.RemoveClaimAsync(user, claim).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Remove a user from a role.</summary>
        /// <param name="userId">.</param>
        /// <param name="role">  .</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> RemoveFromRoleAsync(TKey userId, string role)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (!await userRoleStore.IsInRoleAsync(user, role).WithCurrentCulture())
            {
                return new IdentityResult(Resources.UserNotInRole);
            }
            await userRoleStore.RemoveFromRoleAsync(user, role).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Remove user from multiple roles.</summary>
        /// <param name="userId">user id.</param>
        /// <param name="roles"> list of role names.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> RemoveFromRolesAsync(TKey userId, params string[] roles)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            if (roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
            var strArray = roles;
            for (var index = 0; index < strArray.Length; ++index)
            {
                var roleName = strArray[index];
                if (!userRoles.Contains(roleName))
                {
                    return new IdentityResult(Resources.UserNotInRole);
                }
                await userRoleStore.RemoveFromRoleAsync(user, roleName).WithCurrentCulture();
            }
            strArray = null;
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Remove a user login.</summary>
        /// <param name="userId">.</param>
        /// <param name="login"> .</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> RemoveLoginAsync(TKey userId, UserLoginInfo login)
        {
            ThrowIfDisposed();
            var loginStore = GetLoginStore();
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var cultureAwaiter = loginStore.RemoveLoginAsync(user, login).WithCurrentCulture();
            await cultureAwaiter;
            cultureAwaiter = UpdateSecurityStampInternal(user).WithCurrentCulture();
            await cultureAwaiter;
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Remove a user's password.</summary>
        /// <param name="userId">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> RemovePasswordAsync(TKey userId)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await passwordStore.SetPasswordHashAsync(user, null).WithCurrentCulture();
            await UpdateSecurityStampInternal(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Resets the access failed count for the user to 0.</summary>
        /// <param name="userId">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> ResetAccessFailedCountAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (await GetAccessFailedCountAsync(user.Id).WithCurrentCulture() == 0)
            {
                return IdentityResult.Success;
            }
            await store.ResetAccessFailedCountAsync(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Reset a user's password using a reset password token.</summary>
        /// <param name="userId">     .</param>
        /// <param name="token">      .</param>
        /// <param name="newPassword">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> ResetPasswordAsync(TKey userId, string token, string newPassword)
        {
            ThrowIfDisposed();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (!await VerifyUserTokenAsync(userId, "ResetPassword", token).WithCurrentCulture())
            {
                return IdentityResult.Failed(Resources.InvalidToken);
            }
            var identityResult = await UpdatePassword(GetPasswordStore(), user, newPassword).WithCurrentCulture();
            return !identityResult.Succeeded ? identityResult : await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Send an email to the user.</summary>
        /// <param name="userId"> .</param>
        /// <param name="subject">.</param>
        /// <param name="body">   .</param>
        /// <returns>A Task.</returns>
        public virtual async Task SendEmailAsync(TKey userId, string subject, string body)
        {
            ThrowIfDisposed();
            if (EmailService == null)
            {
                return;
            }
            var identityMessage1 = new IdentityMessage();
            var identityMessage2 = identityMessage1;
            identityMessage2.Destination = await GetEmailAsync(userId).WithCurrentCulture();
            identityMessage1.Subject = subject;
            identityMessage1.Body = body;
            var message = identityMessage1;
            identityMessage2 = null;
            identityMessage1 = null;
            await EmailService.SendAsync(message).WithCurrentCulture();
        }

        /// <summary>Send a user a sms message.</summary>
        /// <param name="userId"> .</param>
        /// <param name="message">.</param>
        /// <returns>A Task.</returns>
        public virtual async Task SendSmsAsync(TKey userId, string message)
        {
            ThrowIfDisposed();
            if (SmsService == null)
            {
                return;
            }
            var identityMessage1 = new IdentityMessage();
            var identityMessage2 = identityMessage1;
            identityMessage2.Destination = await GetPhoneNumberAsync(userId).WithCurrentCulture();
            identityMessage1.Body = message;
            var message1 = identityMessage1;
            identityMessage2 = null;
            identityMessage1 = null;
            await SmsService.SendAsync(message1).WithCurrentCulture();
        }

        /// <summary>Set a user's email.</summary>
        /// <param name="userId">.</param>
        /// <param name="email"> .</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> SetEmailAsync(TKey userId, string email)
        {
            ThrowIfDisposed();
            var store = GetEmailStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var cultureAwaiter = store.SetEmailAsync(user, email).WithCurrentCulture();
            await cultureAwaiter;
            cultureAwaiter = store.SetEmailConfirmedAsync(user, false).WithCurrentCulture();
            await cultureAwaiter;
            cultureAwaiter = UpdateSecurityStampInternal(user).WithCurrentCulture();
            await cultureAwaiter;
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Sets whether lockout is enabled for this user.</summary>
        /// <param name="userId"> .</param>
        /// <param name="enabled">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> SetLockoutEnabledAsync(TKey userId, bool enabled)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await store.SetLockoutEnabledAsync(user, enabled).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Sets the when a user lockout ends.</summary>
        /// <param name="userId">    .</param>
        /// <param name="lockoutEnd">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> SetLockoutEndDateAsync(TKey userId, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (!await store.GetLockoutEnabledAsync(user).WithCurrentCulture())
            {
                return IdentityResult.Failed(Resources.LockoutNotEnabled);
            }
            await store.SetLockoutEndDateAsync(user, lockoutEnd).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Set a user's phoneNumber.</summary>
        /// <param name="userId">     .</param>
        /// <param name="phoneNumber">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> SetPhoneNumberAsync(TKey userId, string phoneNumber)
        {
            ThrowIfDisposed();
            var store = GetPhoneNumberStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var cultureAwaiter = store.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
            await cultureAwaiter;
            cultureAwaiter = store.SetPhoneNumberConfirmedAsync(user, false).WithCurrentCulture();
            await cultureAwaiter;
            cultureAwaiter = UpdateSecurityStampInternal(user).WithCurrentCulture();
            await cultureAwaiter;
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Set whether a user has two factor authentication enabled.</summary>
        /// <param name="userId"> .</param>
        /// <param name="enabled">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> SetTwoFactorEnabledAsync(TKey userId, bool enabled)
        {
            ThrowIfDisposed();
            var store = GetUserTwoFactorStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            var cultureAwaiter = store.SetTwoFactorEnabledAsync(user, enabled).WithCurrentCulture();
            await cultureAwaiter;
            cultureAwaiter = UpdateSecurityStampInternal(user).WithCurrentCulture();
            await cultureAwaiter;
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Update a user.</summary>
        /// <param name="user">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> UpdateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var identityResult = await UserValidator.ValidateAsync(user).WithCurrentCulture();
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }
            await Store.UpdateAsync(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>Generate a new security stamp for a user, used for SignOutEverywhere functionality.</summary>
        /// <param name="userId">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> UpdateSecurityStampAsync(TKey userId)
        {
            ThrowIfDisposed();
            var securityStore = GetSecurityStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            await securityStore.SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Verify the code is valid for a specific user and for a specific phone number.</summary>
        /// <param name="userId">     .</param>
        /// <param name="token">      .</param>
        /// <param name="phoneNumber">.</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> VerifyChangePhoneNumberTokenAsync(TKey userId, string token, string phoneNumber)
        {
            ThrowIfDisposed();
            var securityToken = await CreateSecurityTokenAsync(userId).WithCurrentCulture();
            return securityToken != null
                && int.TryParse(token, out var result)
                && Rfc6238AuthenticationService.ValidateCode(securityToken, result, phoneNumber);
        }

        /// <summary>Verify a two factor token with the specified provider.</summary>
        /// <param name="userId">           .</param>
        /// <param name="twoFactorProvider">.</param>
        /// <param name="token">            .</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> VerifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token)
        {
            var manager = this;
            manager.ThrowIfDisposed();
            var user = await manager.FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            if (!manager._factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(
                    string.Format(CultureInfo.CurrentCulture, Resources.NoTwoFactorProvider, twoFactorProvider));
            }
            return await manager._factors[twoFactorProvider]
                .ValidateAsync(twoFactorProvider, token, manager, user)
                .WithCurrentCulture();
        }

        /// <summary>Verify a user token with the specified purpose.</summary>
        /// <param name="userId"> .</param>
        /// <param name="purpose">.</param>
        /// <param name="token">  .</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> VerifyUserTokenAsync(TKey userId, string purpose, string token)
        {
            var manager = this;
            manager.ThrowIfDisposed();
            if (manager.UserTokenProvider == null)
            {
                throw new NotSupportedException(Resources.NoTokenProvider);
            }
            var user = await manager.FindByIdAsync(userId).WithCurrentCulture();
            if (user == null)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.UserIdNotFound, userId));
            }
            return await manager.UserTokenProvider.ValidateAsync(purpose, token, manager, user).WithCurrentCulture();
        }

        /// <summary>Creates security token asynchronous.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The new security token asynchronous.</returns>
        internal async Task<SecurityToken> CreateSecurityTokenAsync(TKey userId)
        {
            var unicode = Encoding.Unicode;
            return new SecurityToken(unicode.GetBytes(await GetSecurityStampAsync(userId).WithCurrentCulture()));
        }

        /// <summary>Gets email store.</summary>
        /// <returns>The email store.</returns>
        internal IUserEmailStore<TUser, TKey> GetEmailStore()
        {
            return Store is IUserEmailStore<TUser, TKey> store
                ? store
                : throw new NotSupportedException(Resources.StoreNotIUserEmailStore);
        }

        /// <summary>Gets phone number store.</summary>
        /// <returns>The phone number store.</returns>
        internal IUserPhoneNumberStore<TUser, TKey> GetPhoneNumberStore()
        {
            return Store is IUserPhoneNumberStore<TUser, TKey> store
                ? store
                : throw new NotSupportedException(Resources.StoreNotIUserPhoneNumberStore);
        }

        /// <summary>Gets user lockout store.</summary>
        /// <returns>The user lockout store.</returns>
        internal IUserLockoutStore<TUser, TKey> GetUserLockoutStore()
        {
            return Store is IUserLockoutStore<TUser, TKey> store
                ? store
                : throw new NotSupportedException(Resources.StoreNotIUserLockoutStore);
        }

        /// <summary>Gets user two factor store.</summary>
        /// <returns>The user two factor store.</returns>
        internal IUserTwoFactorStore<TUser, TKey> GetUserTwoFactorStore()
        {
            return Store is IUserTwoFactorStore<TUser, TKey> store
                ? store
                : throw new NotSupportedException(Resources.StoreNotIUserTwoFactorStore);
        }

        /// <summary>Updates the security stamp internal described by user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>A Task.</returns>
#pragma warning disable IDE1006 // Naming Styles
        internal async Task UpdateSecurityStampInternal(TUser user)
#pragma warning restore IDE1006 // Naming Styles
        {
            if (!SupportsUserSecurityStamp)
            {
                return;
            }
            await GetSecurityStore().SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture();
        }

        /// <summary>When disposing, actually dipose the store.</summary>
        /// <param name="disposing">.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
            {
                return;
            }
            Store.Dispose();
            _disposed = true;
        }

        /// <summary>Updates the password.</summary>
        /// <param name="passwordStore">The password store.</param>
        /// <param name="user">         The user.</param>
        /// <param name="newPassword">  The new password.</param>
        /// <returns>A Task{IdentityResult}</returns>
#pragma warning disable IDE1006 // Naming Styles
        protected virtual async Task<IdentityResult> UpdatePassword(
            IUserPasswordStore<TUser, TKey> passwordStore,
            TUser user,
            string newPassword)
#pragma warning restore IDE1006 // Naming Styles
        {
            var identityResult = await PasswordValidator.ValidateAsync(newPassword).WithCurrentCulture();
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }
            await passwordStore.SetPasswordHashAsync(user, PasswordHasher.HashPassword(newPassword))
                .WithCurrentCulture();
            await UpdateSecurityStampInternal(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>By default, retrieves the hashed password from the user store and calls
        /// PasswordHasher.VerifyHashPassword.</summary>
        /// <param name="store">   .</param>
        /// <param name="user">    .</param>
        /// <param name="password">.</param>
        /// <returns>A Task{bool}</returns>
        protected virtual async Task<bool> VerifyPasswordAsync(
            IUserPasswordStore<TUser, TKey> store,
            TUser user,
            string password)
        {
            return (uint)PasswordHasher.VerifyHashedPassword(
                    await store.GetPasswordHashAsync(user).WithCurrentCulture(),
                    password)
                > 0U;
        }

        /// <summary>Creates a new security stamp.</summary>
        /// <returns>A string.</returns>
        private static string NewSecurityStamp()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>Gets claim store.</summary>
        /// <returns>The claim store.</returns>
        private IUserClaimStore<TUser, TKey> GetClaimStore()
        {
            return Store is IUserClaimStore<TUser, TKey> store
                ? store
                : throw new NotSupportedException(Resources.StoreNotIUserClaimStore);
        }

        /// <summary>Gets login store.</summary>
        /// <returns>The login store.</returns>
        private IUserLoginStore<TUser, TKey> GetLoginStore()
        {
            return Store is IUserLoginStore<TUser, TKey> store
                ? store
                : throw new NotSupportedException(Resources.StoreNotIUserLoginStore);
        }

        /// <summary>Gets password store.</summary>
        /// <returns>The password store.</returns>
        private IUserPasswordStore<TUser, TKey> GetPasswordStore()
        {
            return Store is IUserPasswordStore<TUser, TKey> store
                ? store
                : throw new NotSupportedException(Resources.StoreNotIUserPasswordStore);
        }

        /// <summary>Gets security store.</summary>
        /// <returns>The security store.</returns>
        private IUserSecurityStampStore<TUser, TKey> GetSecurityStore()
        {
            return Store is IUserSecurityStampStore<TUser, TKey> store
                ? store
                : throw new NotSupportedException(Resources.StoreNotIUserSecurityStampStore);
        }

        /// <summary>Gets user role store.</summary>
        /// <returns>The user role store.</returns>
        private IUserRoleStore<TUser, TKey> GetUserRoleStore()
        {
            return Store is IUserRoleStore<TUser, TKey> store
                ? store
                : throw new NotSupportedException(Resources.StoreNotIUserRoleStore);
        }

        /// <summary>Throw if disposed.</summary>
        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}
