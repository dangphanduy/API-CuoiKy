using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Flashcard
{
    public int CardId { get; set; }

    public int SetId { get; set; }

    public string FrontText { get; set; } = null!;

    public string BackText { get; set; } = null!;

    public int? FrontMediaId { get; set; }

    public int? BackMediaId { get; set; }

    public string? Hint { get; set; }

    public int OrderIndex { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual File? BackMedia { get; set; }

    public virtual ICollection<FlashcardPracticeLog> FlashcardPracticeLogs { get; set; } = new List<FlashcardPracticeLog>();

    public virtual File? FrontMedia { get; set; }

    public virtual FlashcardSet Set { get; set; } = null!;
}
