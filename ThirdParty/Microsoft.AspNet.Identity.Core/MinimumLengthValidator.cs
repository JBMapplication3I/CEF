// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.MinimumLengthValidator
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System.Globalization;
    using System.Threading.Tasks;

    /// <summary>Used to validate that passwords are a minimum length.</summary>
    /// <seealso cref="IIdentityValidator{String}"/>
    /// <seealso cref="IIdentityValidator{String}"/>
    public class MinimumLengthValidator : IIdentityValidator<string>
    {
        /// <summary>Constructor.</summary>
        /// <param name="requiredLength">.</param>
        public MinimumLengthValidator(int requiredLength)
        {
            RequiredLength = requiredLength;
        }

        /// <summary>Minimum required length for the password.</summary>
        /// <value>The length of the required.</value>
        public int RequiredLength { get; set; }

        /// <summary>Ensures that the password is of the required length.</summary>
        /// <param name="item">.</param>
        /// <returns>A Task{IdentityResult}</returns>
        public virtual Task<IdentityResult> ValidateAsync(string item)
        {
            if (!string.IsNullOrWhiteSpace(item) && item.Length >= RequiredLength)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            return Task.FromResult(
                IdentityResult.Failed(
                    string.Format(CultureInfo.CurrentCulture, Resources.PasswordTooShort, RequiredLength)));
        }
    }
}
