// <copyright file="Answer.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Answer class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAnswer : IBase
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

        /// <summary>Gets or sets the user who answered the question.</summary>
        /// <value>The user who answered the question.</value>
        User? User { get; set; }

        /// <summary>Gets or sets the ID of the question the user answered.</summary>
        /// <value>The ID of the question the user answered.</value>
        int QuestionID { get; set; }

        /// <summary>Gets or sets the question the user answered.</summary>
        /// <value>The question the user answered.</value>
        Question? Question { get; set; }

        /// <summary>Gets or sets the ID of the option the user chose.</summary>
        /// <value>The ID of the option the user chose.</value>
        int OptionID { get; set; }

        /// <summary>Gets or sets the option the user chose.</summary>
        /// <value>The option the user chose.</value>
        QuestionOption? Option { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Questionnaire", "Answer")]
    public class Answer : Base, IAnswer
    {
        #region Answer Properties
        /// <inheritdoc/>
        [StringIsUnicode(true), DefaultValue(null)]
        public string? AdditionalInformation { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(0)]
        public int UserID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Question))]
        public int QuestionID { get; set; }

        /// <inheritdoc/>
        [AllowLookupAssignInWithRelateWorkflowsButDontAffect, DefaultValue(null), JsonIgnore]
        public virtual Question? Question { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Option))]
        public int OptionID { get; set; }

        /// <inheritdoc/>
        [AllowLookupAssignInWithRelateWorkflowsButDontAffect, DefaultValue(null), JsonIgnore]
        public virtual QuestionOption? Option { get; set; }
        #endregion
    }
}
