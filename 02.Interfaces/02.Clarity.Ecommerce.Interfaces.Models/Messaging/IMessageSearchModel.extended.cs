// <copyright file="IMessageSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMessageSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for message search model.</summary>
    public partial interface IMessageSearchModel
    {
        /// <summary>Gets or sets the identifier of the sent from or to user.</summary>
        /// <value>The identifier of the sent from or to user.</value>
        int? SentFromOrToUserID { get; set; }

        /// <summary>Gets or sets the identifier of the sent from user.</summary>
        /// <value>The identifier of the sent from user.</value>
        int? SentFromUserID { get; set; }

        /// <summary>Gets or sets the identifier of the sent to user.</summary>
        /// <value>The identifier of the sent to user.</value>
        int? SentToUserID { get; set; }
    }
}
