// <copyright file="OAuthAuthorizationEndpointResponseContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication authorization endpoint response context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Messages;
    using Provider;

    /// <summary>Provides context information when processing an Authorization Response.</summary>
    /// <seealso cref="EndpointContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthAuthorizationEndpointResponseContext : EndpointContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthAuthorizationEndpointResponseContext" /> class.</summary>
        /// <param name="context">                 .</param>
        /// <param name="options">                 .</param>
        /// <param name="ticket">                  .</param>
        /// <param name="authorizeEndpointRequest">.</param>
        /// <param name="accessToken">             The access token.</param>
        /// <param name="authorizationCode">       The authorization code.</param>
        public OAuthAuthorizationEndpointResponseContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            AuthenticationTicket ticket,
            AuthorizeEndpointRequest authorizeEndpointRequest,
            string accessToken,
            string authorizationCode) : base(context, options)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            Identity = ticket.Identity;
            Properties = ticket.Properties;
            AuthorizeEndpointRequest = authorizeEndpointRequest;
            AdditionalResponseParameters = new Dictionary<string, object>(StringComparer.Ordinal);
            AccessToken = accessToken;
            AuthorizationCode = authorizationCode;
        }

        /// <summary>The serialized Access-Token. Depending on the flow, it can be null.</summary>
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

        /// <summary>The created Authorization-Code. Depending on the flow, it can be null.</summary>
        /// <value>The authorization code.</value>
        public string AuthorizationCode
        {
            get;
        }

        /// <summary>Gets information about the authorize endpoint request.</summary>
        /// <value>The authorize endpoint request.</value>
        public AuthorizeEndpointRequest AuthorizeEndpointRequest
        {
            get;
        }

        /// <summary>Gets the identity of the resource owner.</summary>
        /// <value>The identity.</value>
        public ClaimsIdentity Identity
        {
            get;
        }

        /// <summary>Dictionary containing the state of the authentication session.</summary>
        /// <value>The properties.</value>
        public AuthenticationProperties Properties
        {
            get;
        }
    }
}
