using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class File
{
    public int FileId { get; set; }

    public int OwnerId { get; set; }

    public string FileName { get; set; } = null!;

    public string MimeType { get; set; } = null!;

    public long SizeBytes { get; set; }

    public string StoragePath { get; set; } = null!;

    public int? Width { get; set; }

    public int? Height { get; set; }

    public int? DurationSec { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Flashcard> FlashcardBackMedia { get; set; } = new List<Flashcard>();

    public virtual ICollection<Flashcard> FlashcardFrontMedia { get; set; } = new List<Flashcard>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
