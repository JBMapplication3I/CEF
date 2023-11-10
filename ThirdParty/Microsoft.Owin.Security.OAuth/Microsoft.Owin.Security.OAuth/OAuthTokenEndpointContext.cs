// <copyright file="OAuthTokenEndpointContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication token endpoint context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Messages;
    using Provider;

    /// <summary>Provides context information used when processing an OAuth token request.</summary>
    /// <seealso cref="EndpointContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthTokenEndpointContext : EndpointContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthTokenEndpointContext" /> class.</summary>
        /// <param name="context">             .</param>
        /// <param name="options">             .</param>
        /// <param name="ticket">              .</param>
        /// <param name="tokenEndpointRequest">.</param>
        public OAuthTokenEndpointContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            AuthenticationTicket ticket,
            TokenEndpointRequest tokenEndpointRequest) : base(context, options)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            Identity = ticket.Identity;
            Properties = ticket.Properties;
            TokenEndpointRequest = tokenEndpointRequest;
            AdditionalResponseParameters = new Dictionary<string, object>(StringComparer.Ordinal);
            TokenIssued = Identity != null;
        }

        /// <summary>Enables additional values to be appended to the token response.</summary>
        /// <value>Options that control the additional response.</value>
        public IDictionary<string, object> AdditionalResponseParameters
        {
            get;
        }

        /// <summary>Gets the identity of the resource owner.</summary>
        /// <value>The identity.</value>
        public ClaimsIdentity Identity
        {
            get;
            private set;
        }

        /// <summary>Dictionary containing the state of the authentication session.</summary>
        /// <value>The properties.</value>
        public AuthenticationProperties Properties
        {
            get;
            private set;
        }

        /// <summary>Gets or sets information about the token endpoint request.</summary>
        /// <value>The token endpoint request.</value>
        public TokenEndpointRequest TokenEndpointRequest
        {
            get;
            set;
        }

        /// <summary>Gets whether or not the token should be issued.</summary>
        /// <value>True if token issued, false if not.</value>
        public bool TokenIssued
        {
            get;
            private set;
        }

        /// <summary>Issues the token.</summary>
        /// <param name="identity">  .</param>
        /// <param name="properties">.</param>
        public void Issue(ClaimsIdentity identity, AuthenticationProperties properties)
        {
            Identity = identity;
            Properties = properties;
            TokenIssued = true;
        }
    }
}
