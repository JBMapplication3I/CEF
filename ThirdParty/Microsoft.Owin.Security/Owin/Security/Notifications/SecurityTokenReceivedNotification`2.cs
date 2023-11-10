// <copyright file="SecurityTokenReceivedNotification`2.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the security token received notification` 2 class</summary>
namespace Microsoft.Owin.Security.Notifications
{
    /// <summary>A security token received notification.</summary>
    /// <typeparam name="TMessage">Type of the message.</typeparam>
    /// <typeparam name="TOptions">Type of the options.</typeparam>
    /// <seealso cref="BaseNotification{TOptions}"/>
    /// <seealso cref="BaseNotification{TOptions}"/>
    public class SecurityTokenReceivedNotification<TMessage, TOptions> : BaseNotification<TOptions>
    {
        /// <summary>Initializes a new instance of the {see
        /// cref="Microsoft.Owin.Security.Notifications.SecurityTokenReceivedNotification{TMessage, TOptions}"/}
        /// class.</summary>
        /// <param name="context">The context.</param>
        /// <param name="options">Options for controlling the operation.</param>
        public SecurityTokenReceivedNotification(IOwinContext context, TOptions options) : base(context, options) { }

        /// <summary>Gets or sets a message describing the protocol.</summary>
        /// <value>A message describing the protocol.</value>
        public TMessage ProtocolMessage
        {
            get;
            set;
        }
    }
}
