using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Question
{
    public int QuestionId { get; set; }

    public int TestId { get; set; }

    public string Type { get; set; } = null!;

    public string StemText { get; set; } = null!;

    public int? StemMediaId { get; set; }

    public decimal Points { get; set; }

    public int OrderIndex { get; set; }

    public string? Metadata { get; set; }

    public virtual ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();

    public virtual ICollection<QuestionClozeBlank> QuestionClozeBlanks { get; set; } = new List<QuestionClozeBlank>();

    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();

    public virtual ICollection<QuestionRangeAnswer> QuestionRangeAnswers { get; set; } = new List<QuestionRangeAnswer>();

    public virtual File? StemMedia { get; set; }

    public virtual Test Test { get; set; } = null!;
}
