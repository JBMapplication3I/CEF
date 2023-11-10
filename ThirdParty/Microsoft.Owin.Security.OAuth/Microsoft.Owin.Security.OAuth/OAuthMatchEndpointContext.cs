// <copyright file="OAuthMatchEndpointContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication match endpoint context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using Provider;

    /// <summary>Provides context information used when determining the OAuth flow type based on the request.</summary>
    /// <seealso cref="EndpointContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthMatchEndpointContext : EndpointContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthMatchEndpointContext" /> class.</summary>
        /// <param name="context">.</param>
        /// <param name="options">.</param>
        public OAuthMatchEndpointContext(IOwinContext context, OAuthAuthorizationServerOptions options)
            : base(context, options)
        {
        }

        /// <summary>Gets whether or not the endpoint is an OAuth authorize endpoint.</summary>
        /// <value>True if this OAuthMatchEndpointContext is authorize endpoint, false if not.</value>
        public bool IsAuthorizeEndpoint
        {
            get;
            private set;
        }

        /// <summary>Gets whether or not the endpoint is an OAuth token endpoint.</summary>
        /// <value>True if this OAuthMatchEndpointContext is token endpoint, false if not.</value>
        public bool IsTokenEndpoint
        {
            get;
            private set;
        }

        /// <summary>Sets the endpoint type to authorize endpoint.</summary>
        public void MatchesAuthorizeEndpoint()
        {
            IsAuthorizeEndpoint = true;
            IsTokenEndpoint = false;
        }

        /// <summary>Sets the endpoint type to neither authorize nor token.</summary>
        public void MatchesNothing()
        {
            IsAuthorizeEndpoint = false;
            IsTokenEndpoint = false;
        }

        /// <summary>Sets the endpoint type to token endpoint.</summary>
        public void MatchesTokenEndpoint()
        {
            IsAuthorizeEndpoint = false;
            IsTokenEndpoint = true;
        }
    }
}
