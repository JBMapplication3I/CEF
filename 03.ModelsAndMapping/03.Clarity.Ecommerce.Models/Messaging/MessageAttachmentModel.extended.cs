// <copyright file="MessageAttachmentModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message attachment model class</summary>
// ReSharper disable MissingXmlDoc
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the message attachment.</summary>
    public partial class MessageAttachmentModel
    {
        /// <inheritdoc/>
        public int CreatedByUserID { get; set; }

        /// <inheritdoc/>
        public string? CreatedByUserKey { get; set; }

        /// <inheritdoc cref="IMessageAttachmentModel.CreatedByUser"/>
        public UserModel? CreatedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? IMessageAttachmentModel.CreatedByUser { get => CreatedByUser; set => CreatedByUser = (UserModel?)value; }

        /// <inheritdoc/>
        public int? UpdatedByUserID { get; set; }

        /// <inheritdoc/>
        public string? UpdatedByUserKey { get; set; }

        /// <inheritdoc cref="IMessageAttachmentModel.UpdatedByUser"/>
        public UserModel? UpdatedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? IMessageAttachmentModel.UpdatedByUser { get => UpdatedByUser; set => UpdatedByUser = (UserModel?)value; }
    }
}
