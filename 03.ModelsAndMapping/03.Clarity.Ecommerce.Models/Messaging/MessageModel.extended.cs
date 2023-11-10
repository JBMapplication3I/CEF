// <copyright file="MessageModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the message.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IMessageModel"/>
    public partial class MessageModel
    {
        /// <inheritdoc/>
        public string? Subject { get; set; }

        /// <inheritdoc/>
        public string? Body { get; set; }

        /// <inheritdoc/>
        public string? Context { get; set; }

        /// <inheritdoc/>
        public bool IsReplyAllAllowed { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int? ConversationID { get; set; }

        /// <inheritdoc/>
        public string? ConversationKey { get; set; }

        /// <inheritdoc cref="IMessageModel.Conversation"/>
        public ConversationModel? Conversation { get; set; }

        /// <inheritdoc/>
        IConversationModel? IMessageModel.Conversation { get => Conversation; set => Conversation = (ConversationModel?)value; }

        /// <inheritdoc/>
        public int? SentByUserID { get; set; }

        /// <inheritdoc/>
        public string? SentByUserKey { get; set; }

        /// <inheritdoc/>
        public string? SentByUserUserName { get; set; }

        /// <inheritdoc/>
        public string? SentByUserContactFirstName { get; set; }

        /// <inheritdoc/>
        public string? SentByUserContactLastName { get; set; }

        /// <inheritdoc cref="IMessageModel.SentByUser"/>
        public UserModel? SentByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? IMessageModel.SentByUser { get => SentByUser; set => SentByUser = (UserModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IMessageModel.MessageRecipients"/>
        public List<MessageRecipientModel>? MessageRecipients { get; set; }

        /// <inheritdoc/>
        List<IMessageRecipientModel>? IMessageModel.MessageRecipients { get => MessageRecipients?.ToList<IMessageRecipientModel>(); set => MessageRecipients = value?.Cast<MessageRecipientModel>().ToList(); }

        /// <inheritdoc cref="IMessageModel.MessageAttachments"/>
        public List<MessageAttachmentModel>? MessageAttachments { get; set; }

        /// <inheritdoc/>
        List<IMessageAttachmentModel>? IMessageModel.MessageAttachments { get => MessageAttachments?.ToList<IMessageAttachmentModel>(); set => MessageAttachments = value?.Cast<MessageAttachmentModel>().ToList(); }
        #endregion
    }
}
