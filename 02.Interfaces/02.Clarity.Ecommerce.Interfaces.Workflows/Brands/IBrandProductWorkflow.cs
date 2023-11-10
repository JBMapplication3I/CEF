// <copyright file="IBrandProductWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IBrandProductWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for brand product workflow.</summary>
    public partial interface IBrandProductWorkflow
    {
        /// <summary>Creates a many.</summary>
        /// <param name="models">            The models.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new many.</returns>
        Task<CEFActionResponse> CreateManyAsync(List<IBrandProductModel> models, string? contextProfileName);

        /// <summary>Updates the many described by models.</summary>
        /// <param name="models">            The models.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> UpdateManyAsync(List<IBrandProductModel> models, string? contextProfileName);
    }
}
