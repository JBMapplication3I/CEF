// <copyright file="IAnswerModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the IAnswerModel class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IAnswerModel
    {
        #region Answer Properties
        /// <summary>Gets or sets the additional information supplied with this answer.</summary>
        /// <value>The additional information supplied with this answer.</value>
        string? AdditionalInformation { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the ID of the user who answered the question.</summary>
        /// <value>The ID of the user who answered the question.</value>
        int UserID { get; set; }

        /// <summary>Gets or sets the custom key of the user who answered the question.</summary>
        /// <value>The custom key of the user who answered the question.</value>
        string? UserKey { get; set; }

        /// <summary>Gets or sets the user who answered the question.</summary>
        /// <value>The user who answered the question.</value>
        IUserModel? User { get; set; }

        /// <summary>Gets or sets the ID of the question the user answered.</summary>
        /// <value>The ID of the question the user answered.</value>
        int QuestionID { get; set; }

        /// <summary>Gets or sets the custom key of the question the user answered.</summary>
        /// <value>The custom key of the question the user answered.</value>
        string? QuestionKey { get; set; }

        /// <summary>Gets or sets the question the user answered.</summary>
        /// <value>The question the user answered.</value>
        IQuestionModel? Question { get; set; }

        /// <summary>Gets or sets the ID of the option the user chose.</summary>
        /// <value>The ID of the option the user chose.</value>
        int OptionID { get; set; }

        /// <summary>Gets or sets the custom key of the option the user chose.</summary>
        /// <value>The custom key of the option the user chose.</value>
        string? OptionKey { get; set; }

        /// <summary>Gets or sets the option the user chose.</summary>
        /// <value>The option the user chose.</value>
        IQuestionOptionModel? Option { get; set; }
        #endregion
    }
}
