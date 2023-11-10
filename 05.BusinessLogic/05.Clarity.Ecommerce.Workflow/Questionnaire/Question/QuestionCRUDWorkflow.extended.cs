// <copyright file="QuestionCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the QuestionWorkflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using Mapper;

    public partial class QuestionWorkflow
    {
        static QuestionWorkflow()
        {
            // Ensure Hooks as needed
            try
            {
                ModelMapperForQuestion.CreateQuestionModelFromEntityHooksList = (entity, model, _) =>
                {
                    model.NextQuestionID = entity.NextQuestionID;
                    return model;
                };
            }
            catch
            {
                // Do Nothing
            }
        }
    }
}
