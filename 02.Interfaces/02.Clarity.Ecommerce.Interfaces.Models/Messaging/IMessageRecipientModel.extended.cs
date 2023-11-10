// <copyright file="IMessageRecipientModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMessageRecipientModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for message recipient model.</summary>
    public partial interface IMessageRecipientModel
    {
        /// <summary>Gets or sets a value indicating whether this IMessageRecipientModel is read.</summary>
        /// <value>True if this IMessageRecipientModel is read, false if not.</value>
        bool IsRead { get; set; }

        /// <summary>Gets or sets the Date/Time of the read at.</summary>
        /// <value>The read at.</value>
        DateTime? ReadAt { get; set; }

        /// <summary>Gets or sets a value indicating whether this IMessageRecipientModel is archived.</summary>
        /// <value>True if this IMessageRecipientModel is archived, false if not.</value>
        bool IsArchived { get; set; }

        /// <summary>Gets or sets the Date/Time of the archived at.</summary>
        /// <value>The archived at.</value>
        DateTime? ArchivedAt { get; set; }

        /// <summary>Gets or sets a value indicating whether this IMessageRecipientModel has sent an email.</summary>
        /// <value>True if this IMessageRecipientModel has sent an email, false if not.</value>
        bool HasSentAnEmail { get; set; }

        /// <summary>Gets or sets the Date/Time of the email sent at.</summary>
        /// <value>The email sent at.</value>
        DateTime? EmailSentAt { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the group.</summary>
        /// <value>The identifier of the group.</value>
        int? GroupID { get; set; }

        /// <summary>Gets or sets the group key.</summary>
        /// <value>The group key.</value>
        string? GroupKey { get; set; }

        /// <summary>Gets or sets the name of the group.</summary>
        /// <value>The name of the group.</value>
        string? GroupName { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        IGroupModel? Group { get; set; }
        #endregion
    }
}
