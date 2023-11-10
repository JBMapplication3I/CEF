// <copyright file="OAuthValidateIdentityContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication validate identity context class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    /// <summary>Contains the authentication ticket data from an OAuth bearer token.</summary>
    /// <seealso cref="BaseValidatingTicketContext{OAuthBearerAuthenticationOptions}"/>
    public class OAuthValidateIdentityContext : BaseValidatingTicketContext<OAuthBearerAuthenticationOptions>
    {
        /// <summary>Initializes a new instance of the
        /// <see cref="T:Microsoft.Owin.Security.OAuth.OAuthValidateIdentityContext" /> class.</summary>
        /// <param name="context">.</param>
        /// <param name="options">.</param>
        /// <param name="ticket"> .</param>
        public OAuthValidateIdentityContext(
            IOwinContext context,
            OAuthBearerAuthenticationOptions options,
            AuthenticationTicket ticket) : base(context, options, ticket) { }
    }
}
