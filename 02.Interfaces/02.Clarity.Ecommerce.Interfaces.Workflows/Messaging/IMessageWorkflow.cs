// <copyright file="IMessageWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMessageWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    public partial interface IMessageWorkflow
    {
        /// <summary>Posts a message.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{int}.</returns>
        Task<CEFActionResponse<int>> PostMessageAsync(IMessageModel model, string? contextProfileName);
    }
}
