// <copyright file="MessageRecipientModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the message recipient model class</summary>
// ReSharper disable MissingXmlDoc
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the message recipient.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IMessageRecipientModel"/>
    public partial class MessageRecipientModel
    {
        /// <inheritdoc/>
        public bool IsRead { get; set; }

        /// <inheritdoc/>
        public DateTime? ReadAt { get; set; }

        /// <inheritdoc/>
        public bool IsArchived { get; set; }

        /// <inheritdoc/>
        public DateTime? ArchivedAt { get; set; }

        /// <inheritdoc/>
        public bool HasSentAnEmail { get; set; }

        /// <inheritdoc/>
        public DateTime? EmailSentAt { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int? GroupID { get; set; }

        /// <inheritdoc/>
        public string? GroupKey { get; set; }

        /// <inheritdoc/>
        public string? GroupName { get; set; }

        /// <inheritdoc cref="IMessageRecipientModel.Group"/>
        public GroupModel? Group { get; set; }

        /// <inheritdoc/>
        IGroupModel? IMessageRecipientModel.Group { get => Group; set => Group = (GroupModel?)value; }
        #endregion
    }
}
