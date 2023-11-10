// <copyright file="OAuthTokenEndpointResponseContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication token endpoint response context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Messages;
    using Provider;

    /// <summary>Provides context information used at the end of a token-endpoint-request.</summary>
    /// <seealso cref="EndpointContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthTokenEndpointResponseContext : EndpointContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthTokenEndpointResponseContext" />
        /// class.</summary>
        /// <param name="context">                     .</param>
        /// <param name="options">                     .</param>
        /// <param name="ticket">                      .</param>
        /// <param name="tokenEndpointRequest">        .</param>
        /// <param name="accessToken">                 The access token.</param>
        /// <param name="additionalResponseParameters">Options for controlling the additional response.</param>
        public OAuthTokenEndpointResponseContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            AuthenticationTicket ticket,
            TokenEndpointRequest tokenEndpointRequest,
            string accessToken,
            IDictionary<string, object> additionalResponseParameters) : base(context, options)
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
            AccessToken = accessToken;
            AdditionalResponseParameters = additionalResponseParameters;
        }

        /// <summary>The issued Access-Token.</summary>
        /// <value>The access token.</value>
        public string AccessToken
        {
            get;
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
