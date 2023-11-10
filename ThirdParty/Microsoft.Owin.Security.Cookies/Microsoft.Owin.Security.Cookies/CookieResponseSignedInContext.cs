// <copyright file="CookieResponseSignedInContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cookie response signed in context class</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using System.Security.Claims;
    using Provider;

    /// <summary>Context object passed to the ICookieAuthenticationProvider method ResponseSignedIn.</summary>
    /// <seealso cref="BaseContext{CookieAuthenticationOptions}"/>
    /// <seealso cref="BaseContext{CookieAuthenticationOptions}"/>
    public class CookieResponseSignedInContext : BaseContext<CookieAuthenticationOptions>
    {
        /// <summary>Creates a new instance of the context object.</summary>
        /// <param name="context">           The OWIN request context.</param>
        /// <param name="options">           The middleware options.</param>
        /// <param name="authenticationType">Initializes AuthenticationType property.</param>
        /// <param name="identity">          Initializes Identity property.</param>
        /// <param name="properties">        Initializes Properties property.</param>
        public CookieResponseSignedInContext(
            IOwinContext context,
            CookieAuthenticationOptions options,
            string authenticationType,
            ClaimsIdentity identity,
            AuthenticationProperties properties) : base(context, options)
        {
            AuthenticationType = authenticationType;
            Identity = identity;
            Properties = properties;
        }

        /// <summary>The name of the AuthenticationType creating a cookie.</summary>
        /// <value>The type of the authentication.</value>
        public string AuthenticationType
        {
            get;
        }

        /// <summary>Contains the claims that were converted into the outgoing cookie.</summary>
        /// <value>The identity.</value>
        public ClaimsIdentity Identity
        {
            get;
        }

        /// <summary>Contains the extra data that was contained in the outgoing cookie.</summary>
        /// <value>The properties.</value>
        public AuthenticationProperties Properties
        {
            get;
        }
    }
}
