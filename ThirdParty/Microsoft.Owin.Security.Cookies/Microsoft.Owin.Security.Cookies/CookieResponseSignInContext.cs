// <copyright file="CookieResponseSignInContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cookie response sign in context class</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using System.Security.Claims;
    using Provider;

    /// <summary>Context object passed to the ICookieAuthenticationProvider method ResponseSignIn.</summary>
    /// <seealso cref="BaseContext{CookieAuthenticationOptions}"/>
    /// <seealso cref="BaseContext{CookieAuthenticationOptions}"/>
    public class CookieResponseSignInContext : BaseContext<CookieAuthenticationOptions>
    {
        /// <summary>Creates a new instance of the context object.</summary>
        /// <param name="context">           The OWIN request context.</param>
        /// <param name="options">           The middleware options.</param>
        /// <param name="authenticationType">Initializes AuthenticationType property.</param>
        /// <param name="identity">          Initializes Identity property.</param>
        /// <param name="properties">        Initializes Extra property.</param>
        /// <param name="cookieOptions">     Initializes options for the authentication cookie.</param>
        public CookieResponseSignInContext(
            IOwinContext context,
            CookieAuthenticationOptions options,
            string authenticationType,
            ClaimsIdentity identity,
            AuthenticationProperties properties,
            CookieOptions cookieOptions) : base(context, options)
        {
            AuthenticationType = authenticationType;
            Identity = identity;
            Properties = properties;
            CookieOptions = cookieOptions;
        }

        /// <summary>The name of the AuthenticationType creating a cookie.</summary>
        /// <value>The type of the authentication.</value>
        public string AuthenticationType
        {
            get;
        }

        /// <summary>The options for creating the outgoing cookie. May be replace or altered during the ResponseSignIn
        /// call.</summary>
        /// <value>Options that control the cookie.</value>
        public CookieOptions CookieOptions
        {
            get;
            set;
        }

        /// <summary>Contains the claims about to be converted into the outgoing cookie. May be replaced or altered
        /// during the ResponseSignIn call.</summary>
        /// <value>The identity.</value>
        public ClaimsIdentity Identity
        {
            get;
            set;
        }

        /// <summary>Contains the extra data about to be contained in the outgoing cookie. May be replaced or altered
        /// during the ResponseSignIn call.</summary>
        /// <value>The properties.</value>
        public AuthenticationProperties Properties
        {
            get;
            set;
        }
    }
}
