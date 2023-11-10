// <copyright file="IFranchiseProductWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IFranchiseProductWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for franchise product workflow.</summary>
    public partial interface IFranchiseProductWorkflow
    {
        /// <summary>Creates a many.</summary>
        /// <param name="models">            The models.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new many.</returns>
        Task<CEFActionResponse> CreateManyAsync(List<IFranchiseProductModel> models, string? contextProfileName);

        /// <summary>Updates the many described by models.</summary>
        /// <param name="models">            The models.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> UpdateManyAsync(List<IFranchiseProductModel> models, string? contextProfileName);
    }
}
