// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.UserValidator`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Mail;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>Validates users before they are saved.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    /// <seealso cref="Microsoft.AspNet.Identity.IIdentityValidator{TUser}"/>
    /// <seealso cref="IIdentityValidator{TUser}"/>
    public class UserValidator<TUser, TKey> : IIdentityValidator<TUser>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>Constructor.</summary>
        /// <param name="manager">.</param>
        public UserValidator(UserManager<TUser, TKey> manager)
        {
            AllowOnlyAlphanumericUserNames = true;
            Manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Only allow [A-Za-z0-9@_] in UserNames.</summary>
        /// <value>True if allow only alphanumeric user names, false if not.</value>
        public bool AllowOnlyAlphanumericUserNames { get; set; }

        /// <summary>If set, enforces that emails are non empty, valid, and unique.</summary>
        /// <value>True if require unique email, false if not.</value>
        public bool RequireUniqueEmail { get; set; }

        /// <summary>Gets the manager.</summary>
        /// <value>The manager.</value>
        private UserManager<TUser, TKey> Manager { get; }

        /// <summary>Validates a user before saving.</summary>
        /// <param name="item">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual async Task<IdentityResult> ValidateAsync(TUser item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var errors = new List<string>();
            await ValidateUserName(item, errors).WithCurrentCulture();
            if (RequireUniqueEmail)
            {
                await ValidateEmailAsync(item, errors).WithCurrentCulture();
            }
            return errors.Count <= 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }

        /// <summary>Validates the email asynchronous.</summary>
        /// <param name="user">  The user.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>A Task.</returns>
        private async Task ValidateEmailAsync(TUser user, List<string> errors)
        {
            var email = await Manager.GetEmailStore().GetEmailAsync(user).WithCurrentCulture();
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, "Email"));
            }
            else
            {
                try
                {
                    var mailAddress = new MailAddress(email);
                }
                catch (FormatException)
                {
                    errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.InvalidEmail, email));
                    return;
                }
                var user1 = await Manager.FindByEmailAsync(email).WithCurrentCulture();
                if (user1 == null || EqualityComparer<TKey>.Default.Equals(user1.Id, user.Id))
                {
                    return;
                }
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.DuplicateEmail, email));
            }
        }

        /// <summary>Validates the user name.</summary>
        /// <param name="user">  The user.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>A Task.</returns>
#pragma warning disable IDE1006 // Naming Styles
        private async Task ValidateUserName(TUser user, List<string> errors)
#pragma warning restore IDE1006 // Naming Styles
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.PropertyTooShort, "Name"));
            }
            else if (AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, "^[A-Za-z0-9@_\\.]+$"))
            {
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.InvalidUserName, user.UserName));
            }
            else
            {
                var user1 = await Manager.FindByNameAsync(user.UserName).WithCurrentCulture();
                if (user1 == null || EqualityComparer<TKey>.Default.Equals(user1.Id, user.Id))
                {
                    return;
                }
                errors.Add(string.Format(CultureInfo.CurrentCulture, Resources.DuplicateName, user.UserName));
            }
        }
    }
}
