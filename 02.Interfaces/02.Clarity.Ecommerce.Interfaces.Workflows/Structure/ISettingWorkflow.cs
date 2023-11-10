// <copyright file="ISettingWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISettingWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for setting workflow.</summary>
    public partial interface ISettingWorkflow
    {
        /// <summary>Gets the setting by type names in this collection.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The setting by type name.</returns>
        Task<List<ISettingModel?>> GetSettingByTypeNameAsync(string name, string? contextProfileName);

        /// <summary>Gets the settings by group names in this collection.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The settings by group name.</returns>
        Task<List<ISettingModel?>> GetSettingsByGroupNameAsync(string name, string? contextProfileName);
    }
}
