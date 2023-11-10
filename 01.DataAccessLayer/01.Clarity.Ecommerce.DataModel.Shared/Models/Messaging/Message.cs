// <copyright file="Message.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IMessage
        : IAmFilterableByNullableStore,
            IAmFilterableByNullableBrand
    {
        #region Message Properties
        /// <summary>Gets or sets the subject.</summary>
        /// <value>The subject.</value>
        string? Subject { get; set; }

        /// <summary>Gets or sets the body.</summary>
        /// <value>The body.</value>
        string? Body { get; set; }

        /// <summary>Gets or sets the context.</summary>
        /// <value>The context.</value>
        string? Context { get; set; }

        /// <summary>Gets or sets a value indicating whether this IMessage is reply all allowed.</summary>
        /// <value>True if this IMessage is reply all allowed, false if not.</value>
        bool IsReplyAllAllowed { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the conversation.</summary>
        /// <value>The identifier of the conversation.</value>
        int? ConversationID { get; set; }

        /// <summary>Gets or sets the conversation.</summary>
        /// <value>The conversation.</value>
        Conversation? Conversation { get; set; }

        /// <summary>Gets or sets the identifier of the sent by user.</summary>
        /// <value>The identifier of the sent by user.</value>
        int? SentByUserID { get; set; }

        /// <summary>Gets or sets the sent by user.</summary>
        /// <value>The sent by user.</value>
        User? SentByUser { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the message recipients.</summary>
        /// <value>The message recipients.</value>
        ICollection<MessageRecipient>? MessageRecipients { get; set; }

        /// <summary>Gets or sets the message attachments.</summary>
        /// <value>The message attachments.</value>
        ICollection<MessageAttachment>? MessageAttachments { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Messaging", "Message")]
    public class Message : Base, IMessage
    {
        private ICollection<MessageRecipient>? messageRecipients;
        private ICollection<MessageAttachment>? messageAttachments;

        public Message()
        {
            messageRecipients = new HashSet<MessageRecipient>();
            messageAttachments = new HashSet<MessageAttachment>();
        }

        #region IAmFilterableByNullableStore
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(null)]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }
        #endregion

        #region IAmFilterableByNullableBrand Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(null)]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }
        #endregion

        #region Message Properties
        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(255), DefaultValue(null)]
        public string? Subject { get; set; } = null;

        /// <inheritdoc/>
        [StringIsUnicode(true), StringLength(256), DefaultValue(null)]
        public string? Context { get; set; } = null;

        /// <inheritdoc/>
        [Required, StringIsUnicode(true), DefaultValue(null)]
        public string? Body { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool IsReplyAllAllowed { get; set; } = true;
        #endregion

        #region Related objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Conversation)), DefaultValue(null)]
        public int? ConversationID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Conversation? Conversation { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SentByUser)), DefaultValue(null)]
        public int? SentByUserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? SentByUser { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<MessageRecipient>? MessageRecipients { get => messageRecipients; set => messageRecipients = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<MessageAttachment>? MessageAttachments { get => messageAttachments; set => messageAttachments = value; }
        #endregion
    }
}
