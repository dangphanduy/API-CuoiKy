using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class QuestionClozeBlank
{
    public int BlankId { get; set; }

    public int QuestionId { get; set; }

    public int BlankIndex { get; set; }

    public string CorrectText { get; set; } = null!;

    public string? AcceptRegex { get; set; }

    public bool CaseSensitive { get; set; }

    public virtual Question Question { get; set; } = null!;
}
