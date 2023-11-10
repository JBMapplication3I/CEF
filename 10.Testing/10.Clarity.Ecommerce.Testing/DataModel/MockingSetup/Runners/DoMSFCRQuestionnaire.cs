// <copyright file="DoMSFCRQuestionnaire.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do msfcr questionnaire class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerQuestionnaireAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and setup IQueryable: Questions
            if (DoAll || DoQuestionnaire || DoQuestionTable)
            {
                RawQuestions = new()
                {
                    await CreateADummyQuestionAsync(id: 1, key: "Question1", questionTranslationKey: "Question One?").ConfigureAwait(false),
                    await CreateADummyQuestionAsync(id: 2, key: "Question2", questionTranslationKey: "Question Two?").ConfigureAwait(false),
                    await CreateADummyQuestionAsync(id: 3, key: "Question3", questionTranslationKey: "Question Three?").ConfigureAwait(false),
                };
                await InitializeMockSetQuestionsAsync(mockContext, RawQuestions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and setup IQueryable: Question Options
            if (DoAll || DoQuestionnaire || DoQuestionOptionTable)
            {
                RawQuestionOptions = new()
                {
                    await CreateADummyQuestionOptionAsync(1, key: "Option1").ConfigureAwait(false),
                    await CreateADummyQuestionOptionAsync(2, key: "Option2").ConfigureAwait(false),
                    await CreateADummyQuestionOptionAsync(3, key: "Option3").ConfigureAwait(false),
                };
                await InitializeMockSetQuestionOptionsAsync(mockContext, RawQuestionOptions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and setup IQueryable: Question Types
            if (DoAll || DoQuestionnaire || DoQuestionTypeTable)
            {
                RawQuestionTypes = new()
                {
                    await CreateADummyQuestionTypeAsync(id: 1, key: "Single Choice", name: "Single Choice", displayName: "Single Choice").ConfigureAwait(false),
                    await CreateADummyQuestionTypeAsync(id: 2, key: "Multiple Choice", name: "Multiple Choice", displayName: "Multiple Choice").ConfigureAwait(false),
                    await CreateADummyQuestionTypeAsync(id: 3, key: "Open Response", name: "Open Response", displayName: "Open Response").ConfigureAwait(false),
                };
                await InitializeMockSetQuestionTypesAsync(mockContext, RawQuestionTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and setup IQueryable: Answers
            if (DoAll || DoQuestionnaire || DoAnswerTable)
            {
                RawAnswers = new()
                {
                    await CreateADummyAnswerAsync(id: 1, key: "Answer1").ConfigureAwait(false),
                    await CreateADummyAnswerAsync(id: 2, key: "Answer2").ConfigureAwait(false),
                    await CreateADummyAnswerAsync(id: 3, key: "Answer3").ConfigureAwait(false),
                };
                await InitializeMockSetAnswersAsync(mockContext, RawAnswers).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
