// <copyright file="SecurityHelper.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the security helper class</summary>
namespace Microsoft.Owin.Security.Infrastructure
{
    using System;
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>Helper code used when implementing authentication middleware.</summary>
    public struct SecurityHelper
    {
        /// <summary>The context.</summary>
        private readonly IOwinContext _context;

        /// <summary>Helper code used when implementing authentication middleware.</summary>
        /// <param name="context">.</param>
        public SecurityHelper(IOwinContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>Add an additional ClaimsIdentity to the ClaimsPrincipal in the "server.User" environment key.</summary>
        /// <param name="identity">.</param>
        public void AddUserIdentity(IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var user = _context.Request.User;
            if (user != null)
            {
                if (user is ClaimsPrincipal claimsPrincipal1)
                {
                    foreach (var claimsIdentity in claimsPrincipal1.Identities)
                    {
                        if (!claimsIdentity.IsAuthenticated)
                        {
                            continue;
                        }
                        claimsPrincipal.AddIdentity(claimsIdentity);
                    }
                }
                else
                {
                    var identity1 = user.Identity;
                    if (identity1.IsAuthenticated)
                    {
                        claimsPrincipal.AddIdentity(identity1 as ClaimsIdentity ?? new ClaimsIdentity(identity1));
                    }
                }
            }
            _context.Request.User = claimsPrincipal;
        }

        /// <summary>Tests if this SecurityHelper is considered equal to another.</summary>
        /// <param name="other">The security helper to compare to this SecurityHelper.</param>
        /// <returns>True if the objects are considered equal, false if they are not.</returns>
        public bool Equals(SecurityHelper other)
        {
            return Equals(_context, other._context);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is SecurityHelper))
            {
                return false;
            }
            return Equals((SecurityHelper)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            if (_context == null)
            {
                return 0;
            }
            return _context.GetHashCode();
        }

        /// <summary>Find response challenge details for a specific authentication middleware.</summary>
        /// <param name="authenticationType">The authentication type to look for.</param>
        /// <param name="authenticationMode">The authentication mode the middleware is running under.</param>
        /// <returns>The information instructing the middleware how it should behave.</returns>
        public AuthenticationResponseChallenge LookupChallenge(
            string authenticationType,
            AuthenticationMode authenticationMode)
        {
            if (authenticationType == null)
            {
                throw new ArgumentNullException(nameof(authenticationType));
            }
            var authenticationResponseChallenge = _context.Authentication.AuthenticationResponseChallenge;
            if (authenticationResponseChallenge?.AuthenticationTypes == null
                || authenticationResponseChallenge.AuthenticationTypes.Length == 0)
            {
                if (authenticationMode != AuthenticationMode.Active)
                {
                    return null;
                }
                return authenticationResponseChallenge ?? new AuthenticationResponseChallenge(null, null);
            }
            var authenticationTypes = authenticationResponseChallenge.AuthenticationTypes;
            for (var i = 0; i < authenticationTypes.Length; i++)
            {
                if (string.Equals(authenticationTypes[i], authenticationType, StringComparison.Ordinal))
                {
                    return authenticationResponseChallenge;
                }
            }
            return null;
        }

        /// <summary>Find response sign-in details for a specific authentication middleware.</summary>
        /// <param name="authenticationType">The authentication type to look for.</param>
        /// <returns>The information instructing the middleware how it should behave.</returns>
        public AuthenticationResponseGrant LookupSignIn(string authenticationType)
        {
            AuthenticationResponseGrant authenticationResponseGrant;
            if (authenticationType == null)
            {
                throw new ArgumentNullException(nameof(authenticationType));
            }
            var authenticationResponseGrant1 = _context.Authentication.AuthenticationResponseGrant;
            if (authenticationResponseGrant1 == null)
            {
                return null;
            }
            using var enumerator = authenticationResponseGrant1.Principal.Identities.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (!string.Equals(authenticationType, current.AuthenticationType, StringComparison.Ordinal))
                {
                    continue;
                }
                authenticationResponseGrant = new AuthenticationResponseGrant(
                    current,
                    authenticationResponseGrant1.Properties ?? new AuthenticationProperties());
                return authenticationResponseGrant;
            }
            return null;
        }

        /// <summary>Find response sign-out details for a specific authentication middleware.</summary>
        /// <param name="authenticationType">The authentication type to look for.</param>
        /// <param name="authenticationMode">The authentication mode the middleware is running under.</param>
        /// <returns>The information instructing the middleware how it should behave.</returns>
        public AuthenticationResponseRevoke LookupSignOut(
            string authenticationType,
            AuthenticationMode authenticationMode)
        {
            if (authenticationType == null)
            {
                throw new ArgumentNullException(nameof(authenticationType));
            }
            var authenticationResponseRevoke = _context.Authentication.AuthenticationResponseRevoke;
            if (authenticationResponseRevoke == null)
            {
                return null;
            }
            if (authenticationResponseRevoke.AuthenticationTypes == null
                || authenticationResponseRevoke.AuthenticationTypes.Length == 0)
            {
                if (authenticationMode != AuthenticationMode.Active)
                {
                    return null;
                }
                return authenticationResponseRevoke;
            }
            for (var i = 0; i != authenticationResponseRevoke.AuthenticationTypes.Length; i++)
            {
                if (string.Equals(
                    authenticationType,
                    authenticationResponseRevoke.AuthenticationTypes[i],
                    StringComparison.Ordinal))
                {
                    return authenticationResponseRevoke;
                }
            }
            return null;
        }

        /// <summary>Equality operator.</summary>
        /// <param name="left"> The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator ==(SecurityHelper left, SecurityHelper right)
        {
            return left.Equals(right);
        }

        /// <summary>Inequality operator.</summary>
        /// <param name="left"> The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operation.</returns>
        public static bool operator !=(SecurityHelper left, SecurityHelper right)
        {
            return !left.Equals(right);
        }
    }
}
