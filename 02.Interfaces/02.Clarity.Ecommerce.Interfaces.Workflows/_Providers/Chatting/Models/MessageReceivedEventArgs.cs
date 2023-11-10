// <copyright file="MessageReceivedEventArgs.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message received event arguments class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Chatting
{
    using System;
    using Ecommerce.Models;
    using Models;

    /// <summary>Additional information for message received events.</summary>
    /// <seealso cref="EventArgs"/>
    public class MessageReceivedEventArgs : EventArgs
    {
        /// <summary>Gets or sets the response.</summary>
        /// <value>The response.</value>
        public CEFActionResponse? Response { get; set; }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        public IMessageModel? Message { get; set; }
    }
}
