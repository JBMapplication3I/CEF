// <copyright file="UserTypingStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user typing status class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>A user typing status.</summary>
    public class UserTypingStatus
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        public int UserID { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string? UserName { get; set; }

        /// <summary>Gets or sets a value indicating whether the status.</summary>
        /// <value>True if status, false if not.</value>
        public bool Status { get; set; }
    }
}
