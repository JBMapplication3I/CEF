// <copyright file="AuthenticationTokenCreateContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication token create context class</summary>
namespace Microsoft.Owin.Security.Infrastructure
{
    using System;
    using Provider;

    /// <summary>An authentication token create context.</summary>
    /// <seealso cref="BaseContext"/>
    /// <seealso cref="BaseContext"/>
    public class AuthenticationTokenCreateContext : BaseContext
    {
        /// <summary>The secure data format.</summary>
        private readonly ISecureDataFormat<AuthenticationTicket> _secureDataFormat;

        /// <summary>Initializes a new instance of the
        /// <see cref="AuthenticationTokenCreateContext" /> class.</summary>
        /// <param name="context">         The context.</param>
        /// <param name="secureDataFormat">The secure data format.</param>
        /// <param name="ticket">          The ticket.</param>
        public AuthenticationTokenCreateContext(
            IOwinContext context,
            ISecureDataFormat<AuthenticationTicket> secureDataFormat,
            AuthenticationTicket ticket) : base(context)
        {
            _secureDataFormat = secureDataFormat ?? throw new ArgumentNullException(nameof(secureDataFormat));
            Ticket = ticket ?? throw new ArgumentNullException(nameof(ticket));
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

        /// <summary>Serialize ticket.</summary>
        /// <returns>A string.</returns>
        public string SerializeTicket()
        {
            return _secureDataFormat.Protect(Ticket);
        }

        /// <summary>Sets a token.</summary>
        /// <param name="tokenValue">The token value.</param>
        public void SetToken(string tokenValue)
        {
            Token = tokenValue ?? throw new ArgumentNullException(nameof(tokenValue));
        }
    }
}
