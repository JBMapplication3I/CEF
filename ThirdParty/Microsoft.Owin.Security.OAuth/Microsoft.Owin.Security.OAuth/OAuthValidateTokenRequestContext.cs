// <copyright file="OAuthValidateTokenRequestContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication validate token request context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using Messages;

    /// <summary>Provides context information used in validating an OAuth token request.</summary>
    /// <seealso cref="BaseValidatingContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthValidateTokenRequestContext : BaseValidatingContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthValidateTokenRequestContext" />
        /// class.</summary>
        /// <param name="context">      .</param>
        /// <param name="options">      .</param>
        /// <param name="tokenRequest"> .</param>
        /// <param name="clientContext">.</param>
        public OAuthValidateTokenRequestContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            TokenEndpointRequest tokenRequest,
            BaseValidatingClientContext clientContext) : base(context, options)
        {
            TokenRequest = tokenRequest;
            ClientContext = clientContext;
        }

        /// <summary>Gets information about the client.</summary>
        /// <value>The client context.</value>
        public BaseValidatingClientContext ClientContext
        {
            get;
        }

        /// <summary>Gets the token request data.</summary>
        /// <value>The token request.</value>
        public TokenEndpointRequest TokenRequest
        {
            get;
        }
    }
}
