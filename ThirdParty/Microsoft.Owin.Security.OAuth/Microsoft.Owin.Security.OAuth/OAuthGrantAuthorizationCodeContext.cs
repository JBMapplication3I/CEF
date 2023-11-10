// <copyright file="OAuthGrantAuthorizationCodeContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication grant authorization code context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    /// <summary>Provides context information when handling an OAuth authorization code grant.</summary>
    /// <seealso cref="BaseValidatingTicketContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthGrantAuthorizationCodeContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthGrantAuthorizationCodeContext" />
        /// class.</summary>
        /// <param name="context">.</param>
        /// <param name="options">.</param>
        /// <param name="ticket"> .</param>
        public OAuthGrantAuthorizationCodeContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            AuthenticationTicket ticket) : base(context, options, ticket) { }
    }
}
