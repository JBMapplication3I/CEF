// <copyright file="IHaveRequiresRolesBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveHaveRequiresRolesBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have HaveRequiresRoles base model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IHaveRequiresRolesBaseModel : IBaseModel
    {
        /// <summary>Gets or sets the requires roles.</summary>
        /// <value>The requires roles.</value>
        string? RequiresRoles { get; set; }

        /// <summary>Gets a list of requires roles.</summary>
        /// <value>A List of requires roles.</value>
        List<string>? RequiresRolesList { get; }

        /// <summary>Gets or sets the requires roles. This is an alternate list for additional purposes.</summary>
        /// <value>The requires roles.</value>
        string? RequiresRolesAlt { get; set; }

        /// <summary>Gets a list of requires roles. This is an alternate list for additional purposes.</summary>
        /// <value>A List of requires roles.</value>
        List<string>? RequiresRolesListAlt { get; }
    }
}
