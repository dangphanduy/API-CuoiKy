using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Reminder
{
    public int ReminderId { get; set; }

    public int UserId { get; set; }

    public string RelatedType { get; set; } = null!;

    public int RelatedId { get; set; }

    public DateTime TriggerAt { get; set; }

    public DateTime? SentAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
