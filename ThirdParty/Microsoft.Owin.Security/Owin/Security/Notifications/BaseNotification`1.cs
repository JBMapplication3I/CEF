// <copyright file="BaseNotification`1.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the base notification` 1 class</summary>
namespace Microsoft.Owin.Security.Notifications
{
    using Provider;

    /// <summary>A base notification.</summary>
    /// <typeparam name="TOptions">Type of the options.</typeparam>
    /// <seealso cref="BaseContext{TOptions}"/>
    /// <seealso cref="BaseContext{TOptions}"/>
    public class BaseNotification<TOptions> : BaseContext<TOptions>
    {
        /// <summary>Initializes a new instance of the {see
        /// cref="Microsoft.Owin.Security.Notifications.BaseNotification{TOptions}"/} class.</summary>
        /// <param name="context">The context.</param>
        /// <param name="options">Options for controlling the operation.</param>
        protected BaseNotification(IOwinContext context, TOptions options) : base(context, options) { }

        /// <summary>Gets a value indicating whether the handled response.</summary>
        /// <value>True if handled response, false if not.</value>
        public bool HandledResponse => State == NotificationResultState.HandledResponse;

        /// <summary>Gets a value indicating whether the skipped.</summary>
        /// <value>True if skipped, false if not.</value>
        public bool Skipped => State == NotificationResultState.Skipped;

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

        /// <summary>Discontinue processing the request in the current middleware and pass control to the next one.</summary>
        public void SkipToNextMiddleware()
        {
            State = NotificationResultState.Skipped;
        }
    }
}
