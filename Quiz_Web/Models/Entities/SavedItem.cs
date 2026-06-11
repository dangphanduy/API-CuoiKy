using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class SavedItem
{
    public int SavedItemId { get; set; }

    public int LibraryId { get; set; }

    public int? FolderId { get; set; }

    public string ContentType { get; set; } = null!;

    public int ContentId { get; set; }

    public string? Note { get; set; }

    public DateTime AddedAt { get; set; }

    public virtual Folder? Folder { get; set; }

    public virtual Library Library { get; set; } = null!;
}
