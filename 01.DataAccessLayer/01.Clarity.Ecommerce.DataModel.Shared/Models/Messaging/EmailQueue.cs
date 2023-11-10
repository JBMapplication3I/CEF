// <copyright file="EmailQueue.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email queue class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IEmailQueue : IHaveATypeBase<EmailType>, IHaveAStatusBase<EmailStatus>
    {
        #region EmailQueue Properties
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

        /// <summary>Gets or sets a value indicating whether this IEmailQueue is HTML.</summary>
        /// <value>True if this IEmailQueue is html, false if not.</value>
        bool IsHtml { get; set; }

        /// <summary>Gets or sets the attempts.</summary>
        /// <value>The attempts.</value>
        int Attempts { get; set; }

        /// <summary>Gets or sets a value indicating whether this IEmailQueue has error.</summary>
        /// <value>True if this IEmailQueue has error, false if not.</value>
        bool HasError { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the email template.</summary>
        /// <value>The identifier of the email template.</value>
        int? EmailTemplateID { get; set; }

        /// <summary>Gets or sets the email template.</summary>
        /// <value>The email template.</value>
        EmailTemplate? EmailTemplate { get; set; }

        /// <summary>Gets or sets the identifier of the message recipient.</summary>
        /// <value>The identifier of the message recipient.</value>
        int? MessageRecipientID { get; set; }

        /// <summary>Gets or sets the message recipient.</summary>
        /// <value>The message recipient.</value>
        MessageRecipient? MessageRecipient { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the email queue attachments.</summary>
        /// <value>The email queue attachments.</value>
        ICollection<EmailQueueAttachment>? EmailQueueAttachments { get; set; }
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

    [SqlSchema("Messaging", "EmailQueue")]
    public class EmailQueue : Base, IEmailQueue
    {
        private ICollection<EmailQueueAttachment>? emailQueueAttachments;

        public EmailQueue()
        {
            emailQueueAttachments = new HashSet<EmailQueueAttachment>();
        }

        #region EmailQueue Properties
        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(1024), DefaultValue(null)]
        public string? AddressesTo { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(1024), DefaultValue(null)]
        public string? AddressesCc { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(1024), DefaultValue(null)]
        public string? AddressesBcc { get; set; }

        /// <inheritdoc/>
        [Required, StringIsUnicode(false), StringLength(1024), DefaultValue(null)]
        public string? AddressFrom { get; set; }

        /// <inheritdoc/>
        [Required, StringIsUnicode(true), StringLength(0255), DefaultValue(null)]
        public string? Subject { get; set; }

        /// <inheritdoc/>
        [Required, StringIsUnicode(true), DefaultValue(null), DontMapOutWithListing, DontMapOutWithLite]
        public string? Body { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int Attempts { get; set; } = 0;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsHtml { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool HasError { get; set; } = false;
        #endregion

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual EmailType? Type { get; set; }
        #endregion

        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual EmailStatus? Status { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(EmailTemplate)), DefaultValue(null)]
        public int? EmailTemplateID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual EmailTemplate? EmailTemplate { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(MessageRecipient)), DefaultValue(null)]
        public int? MessageRecipientID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual MessageRecipient? MessageRecipient { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<EmailQueueAttachment>? EmailQueueAttachments { get => emailQueueAttachments; set => emailQueueAttachments = value; }
        #endregion
    }
}
