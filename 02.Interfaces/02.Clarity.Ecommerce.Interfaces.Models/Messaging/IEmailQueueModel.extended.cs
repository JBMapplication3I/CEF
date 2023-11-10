// <copyright file="IEmailQueueModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IEmailQueueModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for email queue model.</summary>
    public partial interface IEmailQueueModel
    {
        #region Email Properties
        /// <summary>Gets or sets the addresses to.</summary>
        /// <value>The addresses to.</value>
        string? AddressesTo { get; set; }

        /// <summary>Gets or sets the addresses Cc.</summary>
        /// <value>The addresses Cc.</value>
        string? AddressesCc { get; set; }

        /// <summary>Gets or sets the addresses Bcc.</summary>
        /// <value>The addresses Bcc.</value>
        string? AddressesBcc { get; set; }

        /// <summary>Gets or sets the address from.</summary>
        /// <value>The address from.</value>
        string? AddressFrom { get; set; }

        /// <summary>Gets or sets the subject.</summary>
        /// <value>The subject.</value>
        string? Subject { get; set; }

        /// <summary>Gets or sets the body.</summary>
        /// <value>The body.</value>
        string? Body { get; set; }

        /// <summary>Gets or sets a value indicating whether this IEmailQueueModel is HTML.</summary>
        /// <value>True if this IEmailQueueModel is html, false if not.</value>
        bool IsHtml { get; set; }

        /// <summary>Gets or sets the attempts.</summary>
        /// <value>The attempts.</value>
        int Attempts { get; set; }

        /// <summary>Gets or sets a value indicating whether this IEmailQueueModel has error.</summary>
        /// <value>True if this IEmailQueueModel has error, false if not.</value>
        bool HasError { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the email template.</summary>
        /// <value>The identifier of the email template.</value>
        int? EmailTemplateID { get; set; }

        /// <summary>Gets or sets the email template key.</summary>
        /// <value>The email template key.</value>
        string? EmailTemplateKey { get; set; }

        /// <summary>Gets or sets the name of the email template.</summary>
        /// <value>The name of the email template.</value>
        string? EmailTemplateName { get; set; }

        /// <summary>Gets or sets the email template.</summary>
        /// <value>The email template.</value>
        IEmailTemplateModel? EmailTemplate { get; set; }

        /// <summary>Gets or sets the identifier of the message recipient.</summary>
        /// <value>The identifier of the message recipient.</value>
        int? MessageRecipientID { get; set; }

        /// <summary>Gets or sets the message recipient key.</summary>
        /// <value>The message recipient key.</value>
        string? MessageRecipientKey { get; set; }

        /// <summary>Gets or sets the message recipient.</summary>
        /// <value>The message recipient.</value>
        IMessageRecipientModel? MessageRecipient { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the email queue attachments.</summary>
        /// <value>The email queue attachments.</value>
        List<IEmailQueueAttachmentModel>? EmailQueueAttachments { get; set; }
        #endregion
    }
}
