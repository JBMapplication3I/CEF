// <copyright file="AuthenticationFailedNotification`2.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication failed notification` 2 class</summary>
namespace Microsoft.Owin.Security.Notifications
{
    using System;

    /// <summary>An authentication failed notification.</summary>
    /// <typeparam name="TMessage">Type of the message.</typeparam>
    /// <typeparam name="TOptions">Type of the options.</typeparam>
    /// <seealso cref="BaseNotification{TOptions}"/>
    /// <seealso cref="BaseNotification{TOptions}"/>
    public class AuthenticationFailedNotification<TMessage, TOptions> : BaseNotification<TOptions>
    {
        /// <summary>Initializes a new instance of the {see
        /// cref="Microsoft.Owin.Security.Notifications.AuthenticationFailedNotification{TMessage, TOptions}"/}
        /// class.</summary>
        /// <param name="context">The context.</param>
        /// <param name="options">Options for controlling the operation.</param>
        public AuthenticationFailedNotification(IOwinContext context, TOptions options) : base(context, options) { }

        /// <summary>Gets or sets details of the exception.</summary>
        /// <value>The exception.</value>
        public Exception Exception
        {
            get;
            set;
        }

        /// <summary>Gets or sets a message describing the protocol.</summary>
        /// <value>A message describing the protocol.</value>
        public TMessage ProtocolMessage
        {
            get;
            set;
        }
    }
}
