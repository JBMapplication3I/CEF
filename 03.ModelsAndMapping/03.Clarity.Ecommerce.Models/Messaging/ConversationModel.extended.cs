// <copyright file="ConversationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the conversation model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the conversation.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IConversationModel"/>
    public partial class ConversationModel
    {
        #region Conversation Properties
        /// <inheritdoc/>
        public bool? HasEnded { get; set; }

        /// <inheritdoc/>
        public bool? CopyUserWhenEnded { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IConversationModel.Messages"/>
        public List<MessageModel>? Messages { get; set; }

        /// <inheritdoc/>
        List<IMessageModel>? IConversationModel.Messages { get => Messages?.ToList<IMessageModel>(); set => Messages = value?.Cast<MessageModel>().ToList(); }

        /// <inheritdoc cref="IConversationModel.ConversationUsers"/>
        public List<ConversationUserModel>? ConversationUsers { get; set; }

        /// <inheritdoc/>
        List<IConversationUserModel>? IConversationModel.ConversationUsers { get => ConversationUsers?.ToList<IConversationUserModel>(); set => ConversationUsers = value?.Cast<ConversationUserModel>().ToList(); }
        #endregion

        #region Convenience Properties
        /// <inheritdoc/>
        public int? MessagesCount { get; set; }

        /// <inheritdoc/>
        public int? ConversationUsersCount { get; set; }

        /// <inheritdoc/>
        public List<string>? ConversationUserUserNames { get; set; }
        #endregion
    }
}
