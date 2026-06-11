using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class ContentTag
{
    public int ContentTagId { get; set; }

    public string ContentType { get; set; } = null!;

    public int ContentId { get; set; }

    public int TagId { get; set; }

    public virtual Tag Tag { get; set; } = null!;
}
