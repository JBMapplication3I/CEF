// <copyright file="OAuthGrantRefreshTokenContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication grant refresh token context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    /// <summary>Provides context information used when granting an OAuth refresh token.</summary>
    /// <seealso cref="BaseValidatingTicketContext{OAuthAuthorizationServerOptions}"/>
    public class OAuthGrantRefreshTokenContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthGrantRefreshTokenContext" />
        /// class.</summary>
        /// <param name="context"> .</param>
        /// <param name="options"> .</param>
        /// <param name="ticket">  .</param>
        /// <param name="clientId">.</param>
        public OAuthGrantRefreshTokenContext(
            IOwinContext context,
            OAuthAuthorizationServerOptions options,
            AuthenticationTicket ticket,
            string clientId) : base(context, options, ticket)
        {
            ClientId = clientId;
        }

        /// <summary>The OAuth client id.</summary>
        /// <value>The identifier of the client.</value>
        public string ClientId
        {
            get;
        }
    }
}
