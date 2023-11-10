// <copyright file="EmailQueueAttachmentModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email queue attachment model class</summary>
// ReSharper disable MissingXmlDoc
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the email queue attachment.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IEmailQueueAttachmentModel"/>
    public partial class EmailQueueAttachmentModel
    {
        /// <inheritdoc/>
        public int CreatedByUserID { get; set; }

        /// <inheritdoc/>
        public string? CreatedByUserKey { get; set; }

        /// <inheritdoc cref="IEmailQueueAttachmentModel.CreatedByUser"/>
        public UserModel? CreatedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? IEmailQueueAttachmentModel.CreatedByUser { get => CreatedByUser; set => CreatedByUser = (UserModel?)value; }

        /// <inheritdoc/>
        public int? UpdatedByUserID { get; set; }

        /// <inheritdoc/>
        public string? UpdatedByUserKey { get; set; }

        /// <inheritdoc cref="IEmailQueueAttachmentModel.UpdatedByUser"/>
        public UserModel? UpdatedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? IEmailQueueAttachmentModel.UpdatedByUser { get => UpdatedByUser; set => UpdatedByUser = (UserModel?)value; }
    }
}
