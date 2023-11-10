// <copyright file="AuthenticationTokenReceiveContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication token receive context class</summary>
namespace Microsoft.Owin.Security.Infrastructure
{
    using System;
    using Provider;

    /// <summary>An authentication token receive context.</summary>
    /// <seealso cref="BaseContext"/>
    /// <seealso cref="BaseContext"/>
    public class AuthenticationTokenReceiveContext : BaseContext
    {
        /// <summary>The secure data format.</summary>
        private readonly ISecureDataFormat<AuthenticationTicket> _secureDataFormat;

        /// <summary>Initializes a new instance of the
        /// <see cref="AuthenticationTokenReceiveContext" /> class.</summary>
        /// <param name="context">         The context.</param>
        /// <param name="secureDataFormat">The secure data format.</param>
        /// <param name="token">           The token.</param>
        public AuthenticationTokenReceiveContext(
            IOwinContext context,
            ISecureDataFormat<AuthenticationTicket> secureDataFormat,
            string token) : base(context)
        {
            _secureDataFormat = secureDataFormat ?? throw new ArgumentNullException(nameof(secureDataFormat));
            Token = token ?? throw new ArgumentNullException(nameof(token));
        }

        /// <summary>Gets the ticket.</summary>
        /// <value>The ticket.</value>
        public AuthenticationTicket Ticket
        {
            get;
            protected set;
        }

        /// <summary>Gets the token.</summary>
        /// <value>The token.</value>
        public string Token
        {
            get;
            protected set;
        }

        /// <summary>Deserialize ticket.</summary>
        /// <param name="protectedData">Information describing the protected.</param>
        public void DeserializeTicket(string protectedData)
        {
            Ticket = _secureDataFormat.Unprotect(protectedData);
        }

        /// <summary>Sets a ticket.</summary>
        /// <param name="ticket">The ticket.</param>
        public void SetTicket(AuthenticationTicket ticket)
        {
            Ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));
        }
    }
}
