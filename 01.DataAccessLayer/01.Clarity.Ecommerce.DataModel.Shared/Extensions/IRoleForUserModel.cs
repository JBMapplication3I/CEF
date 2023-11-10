// <copyright file="IRoleForUserModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRoleForUserModel interface</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>Interface for role for user model.</summary>
    public interface IRoleForUserModel
    {
        /// <summary>Gets or sets the identifier of the role.</summary>
        /// <value>The identifier of the role.</value>
        int RoleId { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int UserId { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string? Name { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime? EndDate { get; set; }
    }
}
