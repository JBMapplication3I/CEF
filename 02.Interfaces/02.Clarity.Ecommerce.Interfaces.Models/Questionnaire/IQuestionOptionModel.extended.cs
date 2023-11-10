// <copyright file="IQuestionOptionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the IQuestionOptionModel class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IQuestionOptionModel
    {
        #region QuestionOption Properties
        /// <summary>Gets or sets the translation key of the question option.</summary>
        /// <value>The translation key of the question option.</value>
        string? OptionTranslationKey { get; set; }

        /// <summary>Gets or sets a value indicating whether this option requires additional information.</summary>
        /// <value>A value indicating whether this option requires additional information.</value>
        bool RequiresAdditionalInformation { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the ID of the question this option is for.</summary>
        /// <value>The ID of the question this option is for.</value>
        int QuestionID { get; set; }

        /// <summary>Gets or sets the custom key of the question this option is for.</summary>
        /// <value>The custom key of the question this option is for.</value>
        string? QuestionKey { get; set; }

        /// <summary>Gets or sets the question this option is for.</summary>
        /// <value>The question this option is for.</value>
        IQuestionModel? Question { get; set; }

        /// <summary>Gets or sets the ID of the follow-up question for this answer.</summary>
        /// <value>The ID of the follow-up question for this answer.</value>
        int? FollowUpQuestionID { get; set; }

        /// <summary>Gets or sets the custom key of the follow-up question for this answer.</summary>
        /// <value>The custom key of the follow-up question for this answer.</value>
        string? FollowUpQuestionKey { get; set; }

        /// <summary>Gets or sets the follow-up question to this answer.</summary>
        /// <value>The follow-up question to this answer.</value>
        IQuestionModel? FollowUpQuestion { get; set; }
        #endregion
    }
}
