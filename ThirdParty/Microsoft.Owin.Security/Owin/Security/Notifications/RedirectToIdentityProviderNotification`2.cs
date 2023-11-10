// <copyright file="RedirectToIdentityProviderNotification`2.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the redirect to identity provider notification` 2 class</summary>
namespace Microsoft.Owin.Security.Notifications
{
    using Provider;

    /// <summary>A redirect to identity provider notification.</summary>
    /// <typeparam name="TMessage">Type of the message.</typeparam>
    /// <typeparam name="TOptions">Type of the options.</typeparam>
    /// <seealso cref="BaseContext{TOptions}"/>
    /// <seealso cref="BaseContext{TOptions}"/>
    public class RedirectToIdentityProviderNotification<TMessage, TOptions> : BaseContext<TOptions>
    {
        /// <summary>Initializes a new instance of the {see
        /// cref="Microsoft.Owin.Security.Notifications.RedirectToIdentityProviderNotification{TMessage, TOptions}"/}
        /// class.</summary>
        /// <param name="context">The context.</param>
        /// <param name="options">Options for controlling the operation.</param>
        public RedirectToIdentityProviderNotification(IOwinContext context, TOptions options)
            : base(context, options)
        {
        }

        /// <summary>Gets a value indicating whether the handled response.</summary>
        /// <value>True if handled response, false if not.</value>
        public bool HandledResponse => State == NotificationResultState.HandledResponse;

        /// <summary>Gets or sets a message describing the protocol.</summary>
        /// <value>A message describing the protocol.</value>
        public TMessage ProtocolMessage
        {
            get;
            set;
        }

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        public NotificationResultState State
        {
            get;
            set;
        }

        /// <summary>Discontinue all processing for this request and return to the client. The caller is responsible for
        /// generating the full response.</summary>
        public void HandleResponse()
        {
            State = NotificationResultState.HandledResponse;
        }
    }
}
