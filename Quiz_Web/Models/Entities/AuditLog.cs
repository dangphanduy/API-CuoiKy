using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class AuditLog
{
    public int AuditId { get; set; }

    public int? UserId { get; set; }

    public string Action { get; set; } = null!;

    public string EntityType { get; set; } = null!;

    public int? EntityId { get; set; }

    public string? Before { get; set; }

    public string? After { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? IpAddress { get; set; }

    public virtual User? User { get; set; }
}
