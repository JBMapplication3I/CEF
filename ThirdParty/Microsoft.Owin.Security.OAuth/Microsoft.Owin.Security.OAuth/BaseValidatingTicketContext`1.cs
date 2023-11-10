// <copyright file="BaseValidatingTicketContext`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base validating ticket context` 1 class</summary>
namespace Microsoft.Owin.Security.OAuth
{
    using System.Security.Claims;

    /// <summary>Base class used for certain event contexts.</summary>
    /// <typeparam name="TOptions">Type of the options.</typeparam>
    /// <seealso cref="BaseValidatingContext{TOptions}"/>
    public abstract class BaseValidatingTicketContext<TOptions> : BaseValidatingContext<TOptions>
    {
        /// <summary>Contains the identity and properties for the application to authenticate. If the Validated method is
        /// invoked with an AuthenticationTicket or ClaimsIdentity argument, that new value is assigned to this property
        /// in addition to changing IsValidated to true.</summary>
        /// <value>The ticket.</value>
        public AuthenticationTicket Ticket
        {
            get;
            private set;
        }

        /// <summary>Initializes base class used for certain event contexts.</summary>
        /// <param name="context">The context.</param>
        /// <param name="options">Options for controlling the operation.</param>
        /// <param name="ticket"> The ticket.</param>
        protected BaseValidatingTicketContext(IOwinContext context, TOptions options, AuthenticationTicket ticket) : base(context, options)
        {
            Ticket = ticket;
        }

        /// <summary>Replaces the ticket information on this context and marks it as as validated by the application.
        /// IsValidated becomes true and HasError becomes false as a result of calling.</summary>
        /// <param name="ticket">Assigned to the Ticket property.</param>
        /// <returns>True if the validation has taken effect.</returns>
        public bool Validated(AuthenticationTicket ticket)
        {
            Ticket = ticket;
            return Validated();
        }

        /// <summary>Alters the ticket information on this context and marks it as as validated by the application.
        /// IsValidated becomes true and HasError becomes false as a result of calling.</summary>
        /// <param name="identity">Assigned to the Ticket.Identity property.</param>
        /// <returns>True if the validation has taken effect.</returns>
        public bool Validated(ClaimsIdentity identity)
        {
            return Validated(new AuthenticationTicket(identity, (Ticket != null ? Ticket.Properties : new AuthenticationProperties())));
        }
    }
}