// <copyright file="QuestionOption.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the QuestionOption class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IQuestionOption : IBase
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

        /// <summary>Gets or sets the question this option is for.</summary>
        /// <value>The question this option is for.</value>
        Question? Question { get; set; }

        /// <summary>Gets or sets the ID of the follow-up question for this answer.</summary>
        /// <value>The ID of the follow-up question for this answer.</value>
        int? FollowUpQuestionID { get; set; }

        /// <summary>Gets or sets the follow-up question to this answer.</summary>
        /// <value>The follow-up question to this answer.</value>
        Question? FollowUpQuestion { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets all the answers that use this option.</summary>
        /// <value>All the answers that use this option.</value>
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

    [SqlSchema("Questionnaire", "QuestionOption")]
    public class QuestionOption : Base, IQuestionOption
    {
        private ICollection<Answer>? answers;

        public QuestionOption()
        {
            answers = new HashSet<Answer>();
        }

        #region QuestionOption Properties
        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(512)]
        public string? OptionTranslationKey { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool RequiresAdditionalInformation { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Question)), DefaultValue(0)]
        public int QuestionID { get; set; }

        /// <inheritdoc/>
        [AllowLookupAssignInWithRelateWorkflowsButDontAffect, DefaultValue(null), JsonIgnore]
        public virtual Question? Question { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(FollowUpQuestion)), DefaultValue(null)]
        public int? FollowUpQuestionID { get; set; }

        /// <inheritdoc/>
        [AllowLookupAssignInWithRelateWorkflowsButDontAffect, DefaultValue(null), JsonIgnore]
        public virtual Question? FollowUpQuestion { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Answer>? Answers { get => answers; set => answers = value; }
        #endregion
    }
}
