// <copyright file="ChattingProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the chatting provider base class</summary>
namespace Clarity.Ecommerce.Providers.Chatting
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Providers.Chatting;
    using Models;

    /// <summary>A chatting provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IChattingProviderBase"/>
    public abstract class ChattingProviderBase : ProviderBase, IChattingProviderBase
    {
        /// <summary>Gets the message received.</summary>
        /// <value>The message received.</value>
        public event EventHandler<MessageReceivedEventArgs>? MessageReceived;

        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Chatting;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <summary>Posts a message to the chat provider.</summary>
        /// <param name="fromUserAccount">Identifier for from user (the third party username).</param>
        /// <param name="toUserAccount">  Identifier for to user account (the third party username).</param>
        /// <param name="message">       The message.</param>
        /// <param name="additionalData">A variable-length parameters list containing additional data.</param>
        /// <returns>A CEFActionResponse.</returns>
        public abstract CEFActionResponse PostMessage(
            string fromUserAccount,
            string toUserAccount,
            string message,
            params object[] additionalData);

        /// <summary>Posts a message to the chat provider.</summary>
        /// <param name="fromUserAccount">Identifier for from user (the third party username).</param>
        /// <param name="toUserAccount">  Identifier for to user account (the third party username).</param>
        /// <param name="message">       The message.</param>
        /// <param name="additionalData">A variable-length parameters list containing additional data.</param>
        /// <returns>A CEFActionResponse.</returns>
        public abstract Task<CEFActionResponse> PostMessageAsync(
            string? fromUserAccount,
            string? toUserAccount,
            string? message,
            params object?[]? additionalData);

        /// <summary>Raises the message received event.</summary>
        /// <param name="e">Event information to send to registered event handlers.</param>
        protected void OnMessageReceived(MessageReceivedEventArgs e)
        {
            var handler = MessageReceived;
            handler?.Invoke(this, e);
        }
    }
}
