// <copyright file="IAccountContactWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAccountContactWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Ecommerce.Models;

    public partial interface IAccountContactWorkflow
    {
        /// <summary>Mark account contact as neither billing nor shipping.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> MarkAccountContactAsNeitherBillingNorShippingAsync(int id, string? contextProfileName);

        /// <summary>Mark account contact as primary shipping.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> MarkAccountContactAsPrimaryShippingAsync(int id, string? contextProfileName);

        /// <summary>Mark account contact as default billing.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> MarkAccountContactAsDefaultBillingAsync(int id, string? contextProfileName);
    }
}
