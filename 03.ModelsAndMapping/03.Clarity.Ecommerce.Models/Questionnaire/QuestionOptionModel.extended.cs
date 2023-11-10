// <copyright file="QuestionOptionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the QuestionOptionModel class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    public partial class QuestionOptionModel
    {
        #region QuestionOption Properties
        /// <inheritdoc/>
        public string? OptionTranslationKey { get; set; }

        /// <inheritdoc/>
        public bool RequiresAdditionalInformation { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int QuestionID { get; set; }

        /// <inheritdoc/>
        public string? QuestionKey { get; set; }

        /// <inheritdoc cref="IQuestionOptionModel.Question"/>
        public QuestionModel? Question { get; set; }

        /// <inheritdoc/>
        IQuestionModel? IQuestionOptionModel.Question { get => Question; set => Question = (QuestionModel?)value; }

        /// <inheritdoc/>
        public int? FollowUpQuestionID { get; set; }

        /// <inheritdoc/>
        public string? FollowUpQuestionKey { get; set; }

        /// <inheritdoc cref="IQuestionOptionModel.FollowUpQuestion"/>
        public QuestionModel? FollowUpQuestion { get; set; }

        /// <inheritdoc/>
        IQuestionModel? IQuestionOptionModel.FollowUpQuestion { get => FollowUpQuestion; set => FollowUpQuestion = (QuestionModel?)value; }
        #endregion
    }
}
