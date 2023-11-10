// <copyright file="QuestionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the QuestionModel class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class QuestionModel
    {
        #region Question Properties
        /// <inheritdoc/>
        public string? QuestionTranslationKey { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? NextQuestionID { get; set; }

        /// <inheritdoc/>
        public string? NextQuestionKey { get; set; }

        /// <inheritdoc cref="IQuestionModel.NextQuestion"/>
        public QuestionModel? NextQuestion { get; set; }

        /// <inheritdoc/>
        IQuestionModel? IQuestionModel.NextQuestion { get => NextQuestion; set => NextQuestion = (QuestionModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IQuestionModel.Options"/>
        public List<QuestionOptionModel>? Options { get; set; }

        /// <inheritdoc/>
        List<IQuestionOptionModel>? IQuestionModel.Options { get => Options?.ToList<IQuestionOptionModel>(); set => Options = value?.Cast<QuestionOptionModel>().ToList(); }
        #endregion
    }
}
