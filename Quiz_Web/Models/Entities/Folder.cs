using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Folder
{
    public int FolderId { get; set; }

    public int LibraryId { get; set; }

    public int? ParentFolderId { get; set; }

    public string Name { get; set; } = null!;

    public int OrderIndex { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Folder> InverseParentFolder { get; set; } = new List<Folder>();

    public virtual Library Library { get; set; } = null!;

    public virtual Folder? ParentFolder { get; set; }

    public virtual ICollection<SavedItem> SavedItems { get; set; } = new List<SavedItem>();
}
