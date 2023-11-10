// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IdentityExtensions
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Globalization;
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>Extensions making it easier to get the user name/user id claims off of an identity.</summary>
    public static class IdentityExtensions
    {
        /// <summary>Return the claim value for the first claim with the specified type if it exists, null otherwise.</summary>
        /// <param name="identity"> .</param>
        /// <param name="claimType">.</param>
        /// <returns>The found value.</returns>
        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            return identity.FindFirst(claimType)?.Value;
        }

        /// <summary>Return the user id using the UserIdClaimType.</summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="identity">.</param>
        /// <returns>The user identifier.</returns>
        public static T GetUserId<T>(this IIdentity identity)
            where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            if (identity is ClaimsIdentity identity1)
            {
                var firstValue = identity1.FindFirstValue(
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (firstValue != null)
                {
                    return (T)Convert.ChangeType(firstValue, typeof(T), CultureInfo.InvariantCulture);
                }
            }
            return default;
        }

        /// <summary>Return the user id using the UserIdClaimType.</summary>
        /// <param name="identity">.</param>
        /// <returns>The user identifier.</returns>
        public static string GetUserId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            return identity is ClaimsIdentity identity1
                ? identity1.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                : null;
        }

        /// <summary>Return the user name using the UserNameClaimType.</summary>
        /// <param name="identity">.</param>
        /// <returns>The user name.</returns>
        public static string GetUserName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            return identity is ClaimsIdentity identity1
                ? identity1.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                : null;
        }
    }
}
