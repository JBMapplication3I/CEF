// <copyright file="ReturnEndpointContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the return endpoint context class</summary>
namespace Microsoft.Owin.Security.Provider
{
    using System.Security.Claims;

    /// <summary>A return endpoint context.</summary>
    /// <seealso cref="EndpointContext"/>
    /// <seealso cref="EndpointContext"/>
    public abstract class ReturnEndpointContext : EndpointContext
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="ReturnEndpointContext" /> class.</summary>
        /// <param name="context">The context.</param>
        /// <param name="ticket"> The ticket.</param>
        protected ReturnEndpointContext(IOwinContext context, AuthenticationTicket ticket) : base(context)
        {
            if (ticket != null)
            {
                Identity = ticket.Identity;
                Properties = ticket.Properties;
            }
        }

        /// <summary>Gets or sets the identity.</summary>
        /// <value>The identity.</value>
        public ClaimsIdentity Identity
        {
            get;
            set;
        }

        /// <summary>Gets or sets the properties.</summary>
        /// <value>The properties.</value>
        public AuthenticationProperties Properties
        {
            get;
            set;
        }

        /// <summary>Gets or sets URI of the redirect.</summary>
        /// <value>The redirect URI.</value>
        public string RedirectUri
        {
            get;
            set;
        }

        /// <summary>Gets or sets the type of the sign in as authentication.</summary>
        /// <value>The type of the sign in as authentication.</value>
        public string SignInAsAuthenticationType
        {
            get;
            set;
        }
    }
}
