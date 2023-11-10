// <copyright file="RoleForUserModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the role for user model class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>A data Model for the role for user.</summary>
    /// <seealso cref="IRoleForUserModel"/>
    public class RoleForUserModel : IRoleForUserModel
    {
        /// <summary>Gets or sets the identifier of the role.</summary>
        /// <value>The identifier of the role.</value>
        public int RoleId { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        public int UserId { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        public DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        public DateTime? EndDate { get; set; }
    }
}
