using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Class
{
    public int ClassId { get; set; }

    public int TeacherId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public string? Term { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ClassAnnouncement> ClassAnnouncements { get; set; } = new List<ClassAnnouncement>();

    public virtual ICollection<ClassAssignment> ClassAssignments { get; set; } = new List<ClassAssignment>();

    public virtual ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();

    public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    public virtual User Teacher { get; set; } = null!;
}
