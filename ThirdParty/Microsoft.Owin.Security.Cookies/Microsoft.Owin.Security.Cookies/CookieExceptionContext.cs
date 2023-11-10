// <copyright file="CookieExceptionContext.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cookie exception context class</summary>
namespace Microsoft.Owin.Security.Cookies
{
    using System;
    using Provider;

    /// <summary>Context object passed to the ICookieAuthenticationProvider method Exception.</summary>
    /// <seealso cref="BaseContext{CookieAuthenticationOptions}"/>
    /// <seealso cref="BaseContext{CookieAuthenticationOptions}"/>
    public class CookieExceptionContext : BaseContext<CookieAuthenticationOptions>
    {
        /// <summary>Creates a new instance of the context object.</summary>
        /// <param name="context">  The OWIN request context.</param>
        /// <param name="options">  The middleware options.</param>
        /// <param name="location"> The location of the exception.</param>
        /// <param name="exception">The exception thrown.</param>
        /// <param name="ticket">   The current ticket, if any.</param>
        public CookieExceptionContext(
            IOwinContext context,
            CookieAuthenticationOptions options,
            ExceptionLocation location,
            Exception exception,
            AuthenticationTicket ticket) : base(context, options)
        {
            Location = location;
            Exception = exception;
            Rethrow = true;
            Ticket = ticket;
        }

        /// <summary>The code paths where exceptions may be reported.</summary>
        public enum ExceptionLocation
        {
            /// <summary>
            ///     The exception was reported in the AuthenticateAsync code path.
            /// </summary>
            AuthenticateAsync,

            /// <summary>
            ///     The exception was reported in the ApplyResponseGrant code path, during sign-in, sign-out, or refresh.
            /// </summary>
            ApplyResponseGrant,

            /// <summary>
            ///     The exception was reported in the ApplyResponseChallenge code path, during redirect generation.
            /// </summary>
            ApplyResponseChallenge,
        }

        /// <summary>The exception thrown.</summary>
        /// <value>The exception.</value>
        public Exception Exception
        {
            get;
        }

        /// <summary>The code path the exception occurred in.</summary>
        /// <value>The location.</value>
        public ExceptionLocation Location
        {
            get;
        }

        /// <summary>True if the exception should be re-thrown (default), false if it should be suppressed.</summary>
        /// <value>True if rethrow, false if not.</value>
        public bool Rethrow
        {
            get;
            set;
        }

        /// <summary>The current authentication ticket, if any. In the AuthenticateAsync code path, if the given
        /// exception is not re-thrown then this ticket will be returned to the application. The ticket may be replaced
        /// if needed.</summary>
        /// <value>The ticket.</value>
        public AuthenticationTicket Ticket
        {
            get;
            set;
        }
    }
}
