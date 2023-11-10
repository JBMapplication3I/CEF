using System;
using System.Collections.Generic;

namespace GeneratePdfs.CefEntities
{
    public partial class Answer
    {
        public int Id { get; set; }
        public string? AdditionalInformation { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int OptionId { get; set; }
        public string? CustomKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Active { get; set; }
        public long? Hash { get; set; }
        public string? JsonAttributes { get; set; }

        public virtual QuestionOption Option { get; set; } = null!;
        public virtual Question Question { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
