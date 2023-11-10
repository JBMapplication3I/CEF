// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.PasswordValidator
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>Used to validate some basic password policy like length and number of non alphanumerics.</summary>
    /// <seealso cref="IIdentityValidator{String}"/>
    /// <seealso cref="IIdentityValidator{String}"/>
    public class PasswordValidator : IIdentityValidator<string>
    {
        /// <summary>Require a digit ('0' - '9')</summary>
        /// <value>True if require digit, false if not.</value>
        public bool RequireDigit { get; set; }

        /// <summary>Minimum required length.</summary>
        /// <value>The length of the required.</value>
        public int RequiredLength { get; set; }

        /// <summary>Require a lower case letter ('a' - 'z')</summary>
        /// <value>True if require lowercase, false if not.</value>
        public bool RequireLowercase { get; set; }

        /// <summary>Require a non letter or digit character.</summary>
        /// <value>True if require non letter or digit, false if not.</value>
        public bool RequireNonLetterOrDigit { get; set; }

        /// <summary>Require an upper case letter ('A' - 'Z')</summary>
        /// <value>True if require uppercase, false if not.</value>
        public bool RequireUppercase { get; set; }

        /// <summary>Returns true if the character is a digit between '0' and '9'.</summary>
        /// <param name="c">.</param>
        /// <returns>True if digit, false if not.</returns>
        public virtual bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>Returns true if the character is upper, lower, or a digit.</summary>
        /// <param name="c">.</param>
        /// <returns>True if letter or digit, false if not.</returns>
        public virtual bool IsLetterOrDigit(char c)
        {
            return IsUpper(c) || IsLower(c) || IsDigit(c);
        }

        /// <summary>Returns true if the character is between 'a' and 'z'.</summary>
        /// <param name="c">.</param>
        /// <returns>True if lower, false if not.</returns>
        public virtual bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        /// <summary>Returns true if the character is between 'A' and 'Z'.</summary>
        /// <param name="c">.</param>
        /// <returns>True if upper, false if not.</returns>
        public virtual bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        /// <summary>Ensures that the string is of the required length and meets the configured requirements.</summary>
        /// <param name="item">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual Task<IdentityResult> ValidateAsync(string item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var stringList = new List<string>();
            if (string.IsNullOrWhiteSpace(item) || item.Length < RequiredLength)
            {
                stringList.Add(string.Format(CultureInfo.CurrentCulture, Resources.PasswordTooShort, RequiredLength));
            }
            if (RequireNonLetterOrDigit && item.All(IsLetterOrDigit))
            {
                stringList.Add(Resources.PasswordRequireNonLetterOrDigit);
            }
            if (RequireDigit && item.All(c => !IsDigit(c)))
            {
                stringList.Add(Resources.PasswordRequireDigit);
            }
            if (RequireLowercase && item.All(c => !IsLower(c)))
            {
                stringList.Add(Resources.PasswordRequireLower);
            }
            if (RequireUppercase && item.All(c => !IsUpper(c)))
            {
                stringList.Add(Resources.PasswordRequireUpper);
            }
            if (stringList.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(IdentityResult.Failed(string.Join(" ", stringList)));
        }
    }
}
