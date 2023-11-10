// <copyright file="IConversationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IConversationModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for conversation model.</summary>
    public partial interface IConversationModel
    {
        #region Conversation Properties
        /// <summary>Gets or sets the has ended.</summary>
        /// <value>The has ended.</value>
        bool? HasEnded { get; set; }

        /// <summary>Gets or sets the copy user when ended.</summary>
        /// <value>The copy user when ended.</value>
        bool? CopyUserWhenEnded { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the messages.</summary>
        /// <value>The messages.</value>
        List<IMessageModel>? Messages { get; set; }

        /// <summary>Gets or sets the conversation users.</summary>
        /// <value>The conversation users.</value>
        List<IConversationUserModel>? ConversationUsers { get; set; }
        #endregion

        #region Convenience Properties
        /// <summary>Gets or sets the number of messages.</summary>
        /// <value>The number of messages.</value>
        int? MessagesCount { get; set; }

        /// <summary>Gets or sets the number of conversation users.</summary>
        /// <value>The number of conversation users.</value>
        int? ConversationUsersCount { get; set; }

        /// <summary>Gets or sets a list of names of the conversation users.</summary>
        /// <value>A list of names of the conversation users.</value>
        List<string>? ConversationUserUserNames { get; set; }
        #endregion
    }
}
