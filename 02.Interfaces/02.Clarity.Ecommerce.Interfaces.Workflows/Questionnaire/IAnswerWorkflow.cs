// <copyright file="IAnswerWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAnswerWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    public partial interface IAnswerWorkflow
    {
        /// <summary>Securely creates the answers, updating the user ID on each before saving it.</summary>
        /// <param name="answers">           The user's answers.</param>
        /// <param name="userID">            The ID of the user who answered the questions.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>A CEFActionResponse indicating the result of the operation.</returns>
        Task<CEFActionResponse> SecureCreateAnswersAsync(
            List<IAnswerModel> answers,
            int userID,
            string? contextProfileName);

        /// <summary>Checks if the given user has answered the questionnaire for the given brand. If the brand is not
        /// provided, it will return true if they've answered any questionnaire.</summary>
        /// <param name="userID">            The user to check for answers.</param>
        /// <param name="brandID">           The brand to filter by (optional).</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>A CEFActionResponse{bool}.</returns>
        Task<CEFActionResponse<bool>> CheckIfUserHasAnsweredBrandQuestionnaireAsync(
            int userID,
            int? brandID,
            string? contextProfileName);
    }
}
