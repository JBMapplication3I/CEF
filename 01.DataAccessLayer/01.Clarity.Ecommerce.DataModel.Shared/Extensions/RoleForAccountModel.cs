// <copyright file="RoleForAccountModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the role for account model class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;

    /// <summary>A data Model for the role for account.</summary>
    /// <seealso cref="IRoleForAccountModel"/>
    public class RoleForAccountModel : IRoleForAccountModel
    {
        /// <summary>Gets or sets the identifier of the role.</summary>
        /// <value>The identifier of the role.</value>
        public int RoleId { get; set; }

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        public int AccountId { get; set; }

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
