// <copyright file="MessageReceivedNotification`2.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message received notification` 2 class</summary>
namespace Microsoft.Owin.Security.Notifications
{
    /// <summary>A message received notification.</summary>
    /// <typeparam name="TMessage">Type of the message.</typeparam>
    /// <typeparam name="TOptions">Type of the options.</typeparam>
    /// <seealso cref="BaseNotification{TOptions}"/>
    /// <seealso cref="BaseNotification{TOptions}"/>
    public class MessageReceivedNotification<TMessage, TOptions> : BaseNotification<TOptions>
    {
        /// <summary>Initializes a new instance of the {see
        /// cref="Microsoft.Owin.Security.Notifications.MessageReceivedNotification{TMessage, TOptions}"/} class.</summary>
        /// <param name="context">The context.</param>
        /// <param name="options">Options for controlling the operation.</param>
        public MessageReceivedNotification(IOwinContext context, TOptions options) : base(context, options) { }

        /// <summary>Gets or sets a message describing the protocol.</summary>
        /// <value>A message describing the protocol.</value>
        public TMessage ProtocolMessage
        {
            get;
            set;
        }
    }
}
