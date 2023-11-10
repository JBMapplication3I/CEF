// <copyright file="AnswerCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the AnswerWorkflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using Utilities;

    public partial class AnswerWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> SecureCreateAnswersAsync(
            List<IAnswerModel> answers,
            int userID,
            string? contextProfileName)
        {
            foreach (var answer in answers)
            {
                // Sanitize the user data
                answer.UserID = userID;
                answer.User = null;
                answer.UserKey = null;
                // Create the entity
                var result = await CreateAsync(model: answer, contextProfileName: contextProfileName).ConfigureAwait(false);
                if (result == null || !Contract.CheckValidID(result.Result))
                {
                    return CEFAR.FailingCEFAR(
                        "ERROR! Something about creating and saving an answer model failed.");
                }
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<bool>> CheckIfUserHasAnsweredBrandQuestionnaireAsync(
            int userID,
            int? brandID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Answers
                .AsNoTracking()
                .FilterByActive(true)
                .FilterAnswersByUserID(userID);
            if (Contract.CheckValidID(brandID))
            {
                query = query
                    .Where(x => x.Question != null
                        && x.Question.BrandID == brandID);
            }
            return (await query.AnyAsync().ConfigureAwait(false))
                .WrapInPassingCEFAR();
        }
    }
}
