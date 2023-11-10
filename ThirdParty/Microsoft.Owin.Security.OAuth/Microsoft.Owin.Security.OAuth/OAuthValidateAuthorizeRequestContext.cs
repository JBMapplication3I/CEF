// <copyright file="OAuthValidateAuthorizeRequestContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication validate authorize request context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using Messages;

    /// <summary>Provides context information used in validating an OAuth authorization request.</summary>
    /// <seealso cref="BaseValidatingContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthValidateAuthorizeRequestContext : BaseValidatingContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthValidateAuthorizeRequestContext" /> class.</summary>
        /// <param name="context">         .</param>
        /// <param name="options">         .</param>
        /// <param name="authorizeRequest">.</param>
        /// <param name="clientContext">   .</param>
        public OAuthValidateAuthorizeRequestContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            AuthorizeEndpointRequest authorizeRequest,
            OAuthValidateClientRedirectUriContext clientContext) : base(context, options)
        {
            AuthorizeRequest = authorizeRequest;
            ClientContext = clientContext;
        }

        /// <summary>Gets OAuth authorization request data.</summary>
        /// <value>The authorize request.</value>
        public AuthorizeEndpointRequest AuthorizeRequest
        {
            get;
        }

        /// <summary>Gets data about the OAuth client.</summary>
        /// <value>The client context.</value>
        public OAuthValidateClientRedirectUriContext ClientContext
        {
            get;
        }
    }
}
