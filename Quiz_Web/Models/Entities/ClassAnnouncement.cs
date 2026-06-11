using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class ClassAnnouncement
{
    public int AnnouncementId { get; set; }

    public int ClassId { get; set; }

    public int AuthorId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? PinUntil { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;
}
