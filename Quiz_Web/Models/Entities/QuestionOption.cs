using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class QuestionOption
{
    public int OptionId { get; set; }

    public int QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    public int? OptionMediaId { get; set; }

    public bool IsCorrect { get; set; }

    public int OrderIndex { get; set; }

    public virtual File? OptionMedia { get; set; }

    public virtual Question Question { get; set; } = null!;
}
