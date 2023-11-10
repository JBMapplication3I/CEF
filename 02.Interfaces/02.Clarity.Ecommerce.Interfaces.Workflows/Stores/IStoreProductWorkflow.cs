// <copyright file="IStoreProductWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IStoreProductWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for store product workflow.</summary>
    public partial interface IStoreProductWorkflow
    {
        /// <summary>Creates a many.</summary>
        /// <param name="models">            The models.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new many.</returns>
        Task<CEFActionResponse> CreateManyAsync(List<IStoreProductModel> models, string? contextProfileName);

        /// <summary>Updates the many described by models.</summary>
        /// <param name="models">            The models.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> UpdateManyAsync(List<IStoreProductModel> models, string? contextProfileName);
    }
}
