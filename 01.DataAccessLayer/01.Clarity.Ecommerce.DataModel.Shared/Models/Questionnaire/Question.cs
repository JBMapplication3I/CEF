// <copyright file="Question.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the question class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IQuestion
        : IHaveATypeBase<QuestionType>,
            IAmFilterableByNullableBrand
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

        /// <summary>Gets or sets the next question.</summary>
        /// <value>The next question.</value>
        Question? NextQuestion { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the response options for this question.</summary>
        /// <value>The response options for this question.</value>
        ICollection<QuestionOption>? Options { get; set; }

        /// <summary>Gets or sets the question options where this question is the follow-up.</summary>
        /// <value>The question options where this question is the follow-up.</value>
        ICollection<QuestionOption>? FollowUpQuestionOptions { get; set; }

        /// <summary>Gets or sets the parent questions of this question.</summary>
        /// <value>The parent questions of this question.</value>
        ICollection<Question>? ParentQuestions { get; set; }

        /// <summary>Gets or sets all answers for this question.</summary>
        /// <value>All answers for this question.</value>
        ICollection<Answer>? Answers { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Questionnaire", "Question")]
    public class Question : Base, IQuestion
    {
        private ICollection<QuestionOption>? options;
        private ICollection<QuestionOption>? followUpQuestionOptions;
        private ICollection<Question>? parentQuestions;
        private ICollection<Answer>? answers;

        public Question()
        {
            options = new HashSet<QuestionOption>();
            followUpQuestionOptions = new HashSet<QuestionOption>();
            parentQuestions = new HashSet<Question>();
            answers = new HashSet<Answer>();
        }

        #region IHaveATypeBase<QuestionType> Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type))]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual QuestionType? Type { get; set; }
        #endregion

        #region IAmFilterableByNullableBrand Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(null)]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }
        #endregion

        #region Question Properties
        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(128)]
        public string? QuestionTranslationKey { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(NextQuestion)), DefaultValue(null)]
        public int? NextQuestionID { get; set; }

        /// <inheritdoc/>
        [AllowLookupAssignInWithRelateWorkflowsButDontAffect, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Question? NextQuestion { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual ICollection<QuestionOption>? Options { get => options; set => options = value; }

        #region Don't Map
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<QuestionOption>? FollowUpQuestionOptions { get => followUpQuestionOptions; set => followUpQuestionOptions = value; }

        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Question>? ParentQuestions { get => parentQuestions; set => parentQuestions = value; }

        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Answer>? Answers { get => answers; set => answers = value; }
        #endregion
        #endregion
    }
}
