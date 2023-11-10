// <copyright file="IGeneralAttributeWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IGeneralAttributeWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public partial interface IGeneralAttributeWorkflow
    {
        /// <summary>Gets all attributes.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>All attributes.</returns>
        Task<List<IGeneralAttributeModel>> GetAllAsync(string? contextProfileName);
    }
}
