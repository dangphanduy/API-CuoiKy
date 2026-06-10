using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class LessonSlideFlashcard
{
    public int SlideId { get; set; }

    public string BackText { get; set; } = null!;

    public int? BackMediaId { get; set; }

    public virtual File? BackMedia { get; set; }

    public virtual LessonSlide Slide { get; set; } = null!;
}
