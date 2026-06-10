using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Certificate
{
    public int CertId { get; set; }

    public int CourseId { get; set; }

    public int UserId { get; set; }

    public DateTime IssuedAt { get; set; }

    public string? Serial { get; set; }

    public string VerifyCode { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
