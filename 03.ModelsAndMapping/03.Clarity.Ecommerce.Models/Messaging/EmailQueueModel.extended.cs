// <copyright file="EmailQueueModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email queue model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the email queue.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IEmailQueueModel"/>
    public partial class EmailQueueModel
    {
        #region EmailQueue Properties
        /// <inheritdoc/>
        public string? AddressesTo { get; set; }

        /// <inheritdoc/>
        public string? AddressesCc { get; set; }

        /// <inheritdoc/>
        public string? AddressesBcc { get; set; }

        /// <inheritdoc/>
        public string? AddressFrom { get; set; }

        /// <inheritdoc/>
        public string? Subject { get; set; }

        /// <inheritdoc/>
        public string? Body { get; set; }

        /// <inheritdoc/>
        public bool IsHtml { get; set; }

        /// <inheritdoc/>
        public int Attempts { get; set; }

        /// <inheritdoc/>
        public bool HasError { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? EmailTemplateID { get; set; }

        /// <inheritdoc/>
        public string? EmailTemplateKey { get; set; }

        /// <inheritdoc/>
        public string? EmailTemplateName { get; set; }

        /// <inheritdoc cref="IEmailQueueModel.EmailTemplate"/>
        public EmailTemplateModel? EmailTemplate { get; set; }

        /// <inheritdoc/>
        IEmailTemplateModel? IEmailQueueModel.EmailTemplate { get => EmailTemplate; set => EmailTemplate = (EmailTemplateModel?)value; }

        /// <inheritdoc/>
        public int? MessageRecipientID { get; set; }

        /// <inheritdoc/>
        public string? MessageRecipientKey { get; set; }

        /// <inheritdoc cref="IEmailQueueModel.MessageRecipient"/>
        public MessageRecipientModel? MessageRecipient { get; set; }

        /// <inheritdoc/>
        IMessageRecipientModel? IEmailQueueModel.MessageRecipient { get => MessageRecipient; set => MessageRecipient = (MessageRecipientModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IEmailQueueModel.EmailQueueAttachments"/>
        public List<EmailQueueAttachmentModel>? EmailQueueAttachments { get; set; }

        /// <inheritdoc/>
        List<IEmailQueueAttachmentModel>? IEmailQueueModel.EmailQueueAttachments { get => EmailQueueAttachments?.ToList<IEmailQueueAttachmentModel>(); set => EmailQueueAttachments = value?.Cast<EmailQueueAttachmentModel>().ToList(); }
        #endregion
    }
}
