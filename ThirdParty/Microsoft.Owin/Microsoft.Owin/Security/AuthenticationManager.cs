// <copyright file="AuthenticationManager.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication manager class</summary>
namespace Microsoft.Owin.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;

    /// <summary>Manager for authentications.</summary>
    /// <seealso cref="IAuthenticationManager"/>
    /// <seealso cref="IAuthenticationManager"/>
    internal class AuthenticationManager : IAuthenticationManager
    {
        /// <summary>The context.</summary>
        private readonly IOwinContext _context;

        /// <summary>The request.</summary>
        private readonly IOwinRequest _request;

        /// <summary>Initializes a new instance of the <see cref="AuthenticationManager" />
        /// class.</summary>
        /// <param name="context">The context.</param>
        public AuthenticationManager(IOwinContext context)
        {
            _context = context;
            _request = _context.Request;
        }

        /// <summary>Exposes the security.Challenge environment value as a strong type.</summary>
        /// <value>The authentication response challenge.</value>
        public AuthenticationResponseChallenge AuthenticationResponseChallenge
        {
            get
            {
                var challengeEntry = ChallengeEntry;
                if (challengeEntry == null)
                {
                    return null;
                }
                return new AuthenticationResponseChallenge(
                    challengeEntry.Item1,
                    new AuthenticationProperties(challengeEntry.Item2));
            }
            set
            {
                if (value == null)
                {
                    ChallengeEntry = null;
                    return;
                }
                ChallengeEntry = Tuple.Create(value.AuthenticationTypes, value.Properties.Dictionary);
            }
        }

        /// <summary>Exposes the security.SignIn environment value as a strong type.</summary>
        /// <value>The authentication response grant.</value>
        public AuthenticationResponseGrant AuthenticationResponseGrant
        {
            get
            {
                var signInEntry = SignInEntry;
                if (signInEntry == null)
                {
                    return null;
                }
                return new AuthenticationResponseGrant(
                    signInEntry.Item1 as ClaimsPrincipal ?? new ClaimsPrincipal(signInEntry.Item1),
                    new AuthenticationProperties(signInEntry.Item2));
            }
            set
            {
                if (value == null)
                {
                    SignInEntry = null;
                    return;
                }
                SignInEntry = Tuple.Create<IPrincipal, IDictionary<string, string>>(
                    value.Principal,
                    value.Properties.Dictionary);
            }
        }

        /// <summary>Exposes the security.SignOut environment value as a strong type.</summary>
        /// <value>The authentication response revoke.</value>
        public AuthenticationResponseRevoke AuthenticationResponseRevoke
        {
            get
            {
                var signOutEntry = SignOutEntry;
                if (signOutEntry == null)
                {
                    return null;
                }
                return new AuthenticationResponseRevoke(
                    signOutEntry,
                    new AuthenticationProperties(SignOutPropertiesEntry));
            }
            set
            {
                if (value == null)
                {
                    SignOutEntry = null;
                    SignOutPropertiesEntry = null;
                    return;
                }
                SignOutEntry = value.AuthenticationTypes;
                SignOutPropertiesEntry = value.Properties.Dictionary;
            }
        }

        /// <summary>Gets or sets the challenge entry.</summary>
        /// <value>The challenge entry.</value>
        public Tuple<string[], IDictionary<string, string>> ChallengeEntry
        {
            get => _context.Get<Tuple<string[], IDictionary<string, string>>>("security.Challenge");
            set => _context.Set("security.Challenge", value);
        }

        /// <summary>Gets or sets the sign in entry.</summary>
        /// <value>The sign in entry.</value>
        public Tuple<IPrincipal, IDictionary<string, string>> SignInEntry
        {
            get => _context.Get<Tuple<IPrincipal, IDictionary<string, string>>>("security.SignIn");
            set => _context.Set("security.SignIn", value);
        }

        /// <summary>Gets or sets the sign out entry.</summary>
        /// <value>The sign out entry.</value>
        public string[] SignOutEntry
        {
            get => _context.Get<string[]>("security.SignOut");
            set => _context.Set("security.SignOut", value);
        }

        /// <summary>Gets or sets the sign out properties entry.</summary>
        /// <value>The sign out properties entry.</value>
        public IDictionary<string, string> SignOutPropertiesEntry
        {
            get => _context.Get<IDictionary<string, string>>("security.SignOutProperties");
            set => _context.Set("security.SignOutProperties", value);
        }

        /// <inheritdoc/>
        public ClaimsPrincipal User
        {
            get
            {
                var user = _request.User;
                if (user == null)
                {
                    return null;
                }
                return user as ClaimsPrincipal ?? new ClaimsPrincipal(user);
            }
            set => _request.User = value;
        }

        /// <summary>An enum constant representing the internal option.</summary>
        internal
            Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task> AuthenticateDelegate
            => _context
                .Get<Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task>>("security.Authenticate");

        /// <summary>Authenticates.</summary>
        /// <param name="authenticationTypes">.</param>
        /// <param name="callback">           .</param>
        /// <param name="state">              .</param>
        /// <returns>A Task.</returns>
#pragma warning disable IDE1006 // Naming Styles
        public async Task Authenticate(
            string[] authenticationTypes,
            Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object> callback,
            object state)
#pragma warning restore IDE1006 // Naming Styles
        {
            var authenticateDelegate = AuthenticateDelegate;
            if (authenticateDelegate != null)
            {
                await authenticateDelegate(authenticationTypes, callback, state);
            }
        }

        /// <inheritdoc/>
        public async Task<AuthenticateResult> AuthenticateAsync(string authenticationType)
        {
            string[] strArrays = { authenticationType };
            return (await AuthenticateAsync(strArrays)).SingleOrDefault();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
        {
            var authenticateResults = new List<AuthenticateResult>();
            await Authenticate(authenticationTypes, AuthenticateAsyncCallback, authenticateResults);
            return authenticateResults;
        }

        /// <inheritdoc/>
        public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            _context.Response.StatusCode = 401;
            var authenticationResponseChallenge = AuthenticationResponseChallenge;
            if (authenticationResponseChallenge == null)
            {
                AuthenticationResponseChallenge = new AuthenticationResponseChallenge(authenticationTypes, properties);
                return;
            }
            var array = authenticationResponseChallenge.AuthenticationTypes.Concat(authenticationTypes).ToArray();
            if (properties != null && properties.Dictionary != authenticationResponseChallenge.Properties.Dictionary)
            {
                foreach (var dictionary in properties.Dictionary)
                {
                    authenticationResponseChallenge.Properties.Dictionary[dictionary.Key] = dictionary.Value;
                }
            }
            AuthenticationResponseChallenge = new AuthenticationResponseChallenge(
                array,
                authenticationResponseChallenge.Properties);
        }

        /// <inheritdoc/>
        public void Challenge(params string[] authenticationTypes)
        {
            Challenge(new AuthenticationProperties(), authenticationTypes);
        }

        /// <inheritdoc/>
        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
        {
            return GetAuthenticationTypes(_ => true);
        }

        /// <inheritdoc/>
        public IEnumerable<AuthenticationDescription> GetAuthenticationTypes(
            Func<AuthenticationDescription, bool> predicate)
        {
            var authenticationDescriptions = new List<AuthenticationDescription>();
            GetAuthenticationTypes(
                    rawDescription =>
                    {
                        var authenticationDescription = new AuthenticationDescription(rawDescription);
                        if (predicate(authenticationDescription))
                        {
                            authenticationDescriptions.Add(authenticationDescription);
                        }
                    })
                .Wait();
            return authenticationDescriptions;
        }

        /// <inheritdoc/>
        public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        {
            var authenticationResponseRevoke = AuthenticationResponseRevoke;
            if (authenticationResponseRevoke != null)
            {
                var array = (
                    from authType in authenticationResponseRevoke.AuthenticationTypes
                    where !identities.Any(
                        identity => identity.AuthenticationType.Equals(authType, StringComparison.Ordinal))
                    select authType).ToArray();
                if (array.Length < authenticationResponseRevoke.AuthenticationTypes.Length)
                {
                    if (array.Length != 0)
                    {
                        AuthenticationResponseRevoke = new AuthenticationResponseRevoke(array);
                    }
                    else
                    {
                        AuthenticationResponseRevoke = null;
                    }
                }
            }
            var authenticationResponseGrant = AuthenticationResponseGrant;
            if (authenticationResponseGrant == null)
            {
                AuthenticationResponseGrant = new AuthenticationResponseGrant(
                    new ClaimsPrincipal(identities),
                    properties);
                return;
            }
            var claimsIdentityArray = authenticationResponseGrant.Principal.Identities.Concat(identities).ToArray();
            if (properties != null && properties.Dictionary != authenticationResponseGrant.Properties.Dictionary)
            {
                foreach (var dictionary in properties.Dictionary)
                {
                    authenticationResponseGrant.Properties.Dictionary[dictionary.Key] = dictionary.Value;
                }
            }
            AuthenticationResponseGrant = new AuthenticationResponseGrant(
                new ClaimsPrincipal(claimsIdentityArray),
                authenticationResponseGrant.Properties);
        }

        /// <inheritdoc/>
        public void SignIn(params ClaimsIdentity[] identities)
        {
            SignIn(new AuthenticationProperties(), identities);
        }

        /// <inheritdoc/>
        public void SignOut(AuthenticationProperties properties, string[] authenticationTypes)
        {
            var authenticationResponseGrant = AuthenticationResponseGrant;
            if (authenticationResponseGrant != null)
            {
                var array = (
                    from identity in authenticationResponseGrant.Principal.Identities
                    where !authenticationTypes.Contains(identity.AuthenticationType, StringComparer.Ordinal)
                    select identity).ToArray();
                if (array.Length < authenticationResponseGrant.Principal.Identities.Count())
                {
                    if (array.Length != 0)
                    {
                        AuthenticationResponseGrant = new AuthenticationResponseGrant(
                            new ClaimsPrincipal(array),
                            authenticationResponseGrant.Properties);
                    }
                    else
                    {
                        AuthenticationResponseGrant = null;
                    }
                }
            }
            var authenticationResponseRevoke = AuthenticationResponseRevoke;
            if (authenticationResponseRevoke == null)
            {
                AuthenticationResponseRevoke = new AuthenticationResponseRevoke(authenticationTypes, properties);
                return;
            }
            if (properties != null && properties.Dictionary != authenticationResponseRevoke.Properties.Dictionary)
            {
                foreach (var dictionary in properties.Dictionary)
                {
                    authenticationResponseRevoke.Properties.Dictionary[dictionary.Key] = dictionary.Value;
                }
            }
            var strArrays = authenticationResponseRevoke.AuthenticationTypes.Concat(authenticationTypes).ToArray();
            AuthenticationResponseRevoke = new AuthenticationResponseRevoke(
                strArrays,
                authenticationResponseRevoke.Properties);
        }

        /// <inheritdoc/>
        public void SignOut(string[] authenticationTypes)
        {
            SignOut(new AuthenticationProperties(), authenticationTypes);
        }

        /// <summary>Callback, called when the authenticate asynchronous.</summary>
        /// <param name="identity">   The identity.</param>
        /// <param name="properties"> The properties.</param>
        /// <param name="description">The description.</param>
        /// <param name="state">      The state.</param>
        private static void AuthenticateAsyncCallback(
            IIdentity identity,
            IDictionary<string, string> properties,
            IDictionary<string, object> description,
            object state)
        {
            ((List<AuthenticateResult>)state).Add(
                new AuthenticateResult(
                    identity,
                    new AuthenticationProperties(properties),
                    new AuthenticationDescription(description)));
        }

        /// <summary>Gets authentication types.</summary>
        /// <param name="callback">The callback.</param>
        /// <returns>The authentication types.</returns>
        private Task GetAuthenticationTypes(Action<IDictionary<string, object>> callback)
        {
            return Authenticate(null, (_, __, description, ___) => callback(description), null);
        }
    }
}
