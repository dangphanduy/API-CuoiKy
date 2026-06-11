using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Library
{
    public int LibraryId { get; set; }

    public int OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<SavedItem> SavedItems { get; set; } = new List<SavedItem>();
}
