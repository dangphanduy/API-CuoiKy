using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class NotificationChannel
{
    public int ChannelId { get; set; }

    public int UserId { get; set; }

    public string Channel { get; set; } = null!;

    public bool Enabled { get; set; }

    public string? AddressOrToken { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
