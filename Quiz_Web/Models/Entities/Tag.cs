using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Tag
{
    public int TagId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public virtual ICollection<ContentTag> ContentTags { get; set; } = new List<ContentTag>();
}
