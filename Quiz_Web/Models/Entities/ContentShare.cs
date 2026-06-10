using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class ContentShare
{
    public int ShareId { get; set; }

    public string ContentType { get; set; } = null!;

    public int ContentId { get; set; }

    public string TargetType { get; set; } = null!;

    public int? TargetId { get; set; }

    public bool CanView { get; set; }

    public bool CanEdit { get; set; }

    public bool CanAssign { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;
}
