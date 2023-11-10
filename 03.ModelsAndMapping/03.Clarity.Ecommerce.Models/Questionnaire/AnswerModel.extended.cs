// <copyright file="AnswerModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the AnswerModel class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    public partial class AnswerModel
    {
        #region Answer Properties
        /// <inheritdoc/>
        public string? AdditionalInformation { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int UserID { get; set; }

        /// <inheritdoc/>
        public string? UserKey { get; set; }

        /// <inheritdoc cref="IAnswerModel.User"/>
        public UserModel? User { get; set; }

        /// <inheritdoc/>
        IUserModel? IAnswerModel.User { get => User; set => User = (UserModel?)value; }

        /// <inheritdoc/>
        public int QuestionID { get; set; }

        /// <inheritdoc/>
        public string? QuestionKey { get; set; }

        /// <inheritdoc cref="IAnswerModel.Question"/>
        public QuestionModel? Question { get; set; }

        /// <inheritdoc/>
        IQuestionModel? IAnswerModel.Question { get => Question; set => Question = (QuestionModel?)value; }

        /// <inheritdoc/>
        public int OptionID { get; set; }

        /// <inheritdoc/>
        public string? OptionKey { get; set; }

        /// <inheritdoc cref="IAnswerModel.Option"/>
        public QuestionOptionModel? Option { get; set; }

        /// <inheritdoc/>
        IQuestionOptionModel? IAnswerModel.Option { get => Option; set => Option = (QuestionOptionModel?)value; }
        #endregion
    }
}
