using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            InverseNextQuestion = new HashSet<Question>();
            QuestionOptionFollowUpQuestions = new HashSet<QuestionOption>();
            QuestionOptionQuestions = new HashSet<QuestionOption>();
        }

        public int Id { get; set; }
        public int TypeId { get; set; }
        public int? BrandId { get; set; }
        public string? QuestionTranslationKey { get; set; }
        public int? NextQuestionId { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Question? NextQuestion { get; set; }
        public virtual QuestionType Type { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Question> InverseNextQuestion { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptionFollowUpQuestions { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptionQuestions { get; set; }
    }
}
