// <copyright file="IMessageModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMessageModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for message model.</summary>
    public partial interface IMessageModel
    {
        /// <summary>Gets or sets the subject.</summary>
        /// <value>The subject.</value>
        string? Subject { get; set; }

        /// <summary>Gets or sets the body.</summary>
        /// <value>The body.</value>
        string? Body { get; set; }

        /// <summary>Gets or sets the context.</summary>
        /// <value>The context.</value>
        string? Context { get; set; }

        /// <summary>Gets or sets a value indicating whether this IMessageModel is reply all allowed.</summary>
        /// <value>True if this IMessageModel is reply all allowed, false if not.</value>
        bool IsReplyAllAllowed { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the conversation.</summary>
        /// <value>The identifier of the conversation.</value>
        int? ConversationID { get; set; }

        /// <summary>Gets or sets the conversation key.</summary>
        /// <value>The conversation key.</value>
        string? ConversationKey { get; set; }

        /// <summary>Gets or sets the conversation.</summary>
        /// <value>The conversation.</value>
        IConversationModel? Conversation { get; set; }

        /// <summary>Gets or sets the identifier of the sent by user.</summary>
        /// <value>The identifier of the sent by user.</value>
        int? SentByUserID { get; set; }

        /// <summary>Gets or sets the sent by user.</summary>
        /// <value>The sent by user.</value>
        IUserModel? SentByUser { get; set; }

        /// <summary>Gets or sets the sent by user key.</summary>
        /// <value>The sent by user key.</value>
        string? SentByUserKey { get; set; }

        /// <summary>Gets or sets the name of the sent by user.</summary>
        /// <value>The name of the sent by user.</value>
        string? SentByUserUserName { get; set; }

        /// <summary>Gets or sets the name of the sent by user contact first.</summary>
        /// <value>The name of the sent by user contact first.</value>
        string? SentByUserContactFirstName { get; set; }

        /// <summary>Gets or sets the name of the sent by user contact last.</summary>
        /// <value>The name of the sent by user contact last.</value>
        string? SentByUserContactLastName { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the message recipients.</summary>
        /// <value>The message recipients.</value>
        List<IMessageRecipientModel>? MessageRecipients { get; set; }

        /// <summary>Gets or sets the message attachments.</summary>
        /// <value>The message attachments.</value>
        List<IMessageAttachmentModel>? MessageAttachments { get; set; }
        #endregion
    }
}
