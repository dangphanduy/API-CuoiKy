using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class LessonSlide
{
    public int SlideId { get; set; }

    public int LessonId { get; set; }

    public int OrderIndex { get; set; }

    public string SlideType { get; set; } = null!;

    public string StemText { get; set; } = null!;

    public int? StemMediaId { get; set; }

    public decimal Points { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual LessonSlideFlashcard? LessonSlideFlashcard { get; set; }

    public virtual ICollection<LessonSlideOption> LessonSlideOptions { get; set; } = new List<LessonSlideOption>();

    public virtual ICollection<LessonSlideShortText> LessonSlideShortTexts { get; set; } = new List<LessonSlideShortText>();

    public virtual File? StemMedia { get; set; }
}
