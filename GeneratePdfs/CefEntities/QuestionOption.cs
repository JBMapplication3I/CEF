using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class QuestionOption
    {
        public QuestionOption()
        {
            Answers = new HashSet<Answer>();
        }

        public int Id { get; set; }
        public string? OptionTranslationKey { get; set; }
        public bool RequiresAdditionalInformation { get; set; }
        public int QuestionId { get; set; }
        public int? FollowUpQuestionId { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Question? FollowUpQuestion { get; set; }
        public virtual Question Question { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
