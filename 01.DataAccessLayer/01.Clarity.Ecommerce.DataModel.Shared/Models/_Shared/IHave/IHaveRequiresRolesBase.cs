// <copyright file="IHaveRequiresRolesBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveRequiresRolesBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;

    /// <summary>Interface for have JSON attributes base.</summary>
    /// <seealso cref="IBase"/>
    public interface IHaveRequiresRolesBase : IBase
    {
        /// <summary>Gets or sets the requires roles.</summary>
        /// <value>The requires roles.</value>
        string? RequiresRoles { get; set; }

        /// <summary>Gets a list of requires roles.</summary>
        /// <value>A List of requires roles.</value>
        List<string> RequiresRolesList { get; }

        /// <summary>Gets or sets the requires roles. This is an alternate list for secondary uses.</summary>
        /// <value>The requires roles.</value>
        string? RequiresRolesAlt { get; set; }

        /// <summary>Gets a list of requires roles. This is an alternate list for secondary uses.</summary>
        /// <value>A List of requires roles.</value>
        List<string> RequiresRolesListAlt { get; }
    }
}
