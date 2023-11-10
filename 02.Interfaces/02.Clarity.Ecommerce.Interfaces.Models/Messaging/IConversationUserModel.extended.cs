// <copyright file="IConversationUserModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IConversationUserModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for conversation user model.</summary>
    public partial interface IConversationUserModel
    {
        /// <summary>Gets or sets the is typing.</summary>
        /// <value>The is typing.</value>
        bool? IsTyping { get; set; }

        /// <summary>Gets or sets the Date/Time of the last heartbeat.</summary>
        /// <value>The last heartbeat.</value>
        DateTime? LastHeartbeat { get; set; }
    }
}
