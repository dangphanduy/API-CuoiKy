using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class UserSetting
{
    public int UserId { get; set; }

    public string? UiTheme { get; set; }

    public string? Language { get; set; }

    public string? TimeZone { get; set; }

    public bool EmailOptIn { get; set; }

    public bool PushOptIn { get; set; }

    public virtual User User { get; set; } = null!;
}
