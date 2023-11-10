// <copyright file="IMessageAttachmentModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMessageAttachmentModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    /// <summary>Interface for message attachment model.</summary>
    public partial interface IMessageAttachmentModel
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the created by user.</summary>
        /// <value>The identifier of the created by user.</value>
        int CreatedByUserID { get; set; }

        /// <summary>Gets or sets the created by user key.</summary>
        /// <value>The created by user key.</value>
        string? CreatedByUserKey { get; set; }

        /// <summary>Gets or sets the created by user.</summary>
        /// <value>The created by user.</value>
        IUserModel? CreatedByUser { get; set; }

        /// <summary>Gets or sets the identifier of the updated by user.</summary>
        /// <value>The identifier of the updated by user.</value>
        int? UpdatedByUserID { get; set; }

        /// <summary>Gets or sets the updated by user key.</summary>
        /// <value>The updated by user key.</value>
        string? UpdatedByUserKey { get; set; }

        /// <summary>Gets or sets the updated by user.</summary>
        /// <value>The updated by user.</value>
        IUserModel? UpdatedByUser { get; set; }
        #endregion
    }
}
