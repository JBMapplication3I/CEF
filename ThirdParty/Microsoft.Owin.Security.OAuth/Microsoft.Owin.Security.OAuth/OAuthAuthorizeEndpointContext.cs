// <copyright file="OAuthAuthorizeEndpointContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication authorize endpoint context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using Messages;
    using Provider;

    /// <summary>An event raised after the Authorization Server has processed the request, but before it is passed on
    /// to the web application. Calling RequestCompleted will prevent the request from passing on to the web
    /// application.</summary>
    /// <seealso cref="EndpointContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthAuthorizeEndpointContext : EndpointContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Creates an instance of this context.</summary>
        /// <param name="context">         The context.</param>
        /// <param name="options">         Options for controlling the operation.</param>
        /// <param name="authorizeRequest">The authorize request.</param>
        public OAuthAuthorizeEndpointContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            AuthorizeEndpointRequest authorizeRequest) : base(context, options)
        {
            AuthorizeRequest = authorizeRequest;
        }

        /// <summary>Gets OAuth authorization request data.</summary>
        /// <value>The authorize request.</value>
        public AuthorizeEndpointRequest AuthorizeRequest
        {
            get;
        }
    }
}
