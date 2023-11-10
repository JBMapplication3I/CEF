// <copyright file="IChattingProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IChattingProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Chatting
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Models;

    /// <summary>Interface for chatting provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface IChattingProviderBase : IProviderBase
    {
        /// <summary>Occurs when Message Received.</summary>
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>Posts a message.</summary>
        /// <param name="fromUserAccount">from user account.</param>
        /// <param name="toUserAccount">  to user account.</param>
        /// <param name="message">        The message.</param>
        /// <param name="additionalData"> A variable-length parameters list containing additional data.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> PostMessageAsync(
            string? fromUserAccount,
            string? toUserAccount,
            string? message,
            params object?[]? additionalData);
    }
}
