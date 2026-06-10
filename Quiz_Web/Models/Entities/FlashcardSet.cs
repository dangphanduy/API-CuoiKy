using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class FlashcardSet
{
    public int SetId { get; set; }

    public int OwnerId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Visibility { get; set; } = null!;

    public string? CoverUrl { get; set; }

    public string? TagsText { get; set; }

    public string? Language { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<FlashcardPracticeLog> FlashcardPracticeLogs { get; set; } = new List<FlashcardPracticeLog>();

    public virtual ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();

    public virtual User Owner { get; set; } = null!;
}
