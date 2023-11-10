// <copyright file="IQuestionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the IQuestionModel class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    public partial interface IQuestionModel
    {
        #region Question Properties
        /// <summary>Gets or sets the translation key of the question.</summary>
        /// <value>The translation key of the question.</value>
        string? QuestionTranslationKey { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the ID of the next question.</summary>
        /// <value>The ID of the next question.</value>
        int? NextQuestionID { get; set; }

        /// <summary>Gets or sets the custom key of the next question.</summary>
        /// <value>The custom key of the next question.</value>
        string? NextQuestionKey { get; set; }

        /// <summary>Gets or sets the next question.</summary>
        /// <value>The next question.</value>
        IQuestionModel? NextQuestion { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the response options for this question.</summary>
        /// <value>The response options for this question.</value>
        List<IQuestionOptionModel>? Options { get; set; }
        #endregion
    }
}
