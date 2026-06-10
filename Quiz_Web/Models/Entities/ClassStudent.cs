using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class ClassStudent
{
    public int ClassId { get; set; }

    public int StudentId { get; set; }

    public DateTime JoinedAt { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
