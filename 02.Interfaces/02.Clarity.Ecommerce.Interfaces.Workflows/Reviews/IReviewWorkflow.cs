// <copyright file="IReviewWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2014-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IReviewWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Ecommerce.Models;

    /// <summary>Interface for review workflow.</summary>
    public partial interface IReviewWorkflow
    {
        /// <summary>Approve the review.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        public Task<CEFActionResponse> ApproveAsync(int id, int userID, string? contextProfileName);

        /// <summary>Unapprove the review.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        public Task<CEFActionResponse> UnapproveAsync(int id, int userID, string? contextProfileName);
    }
}
