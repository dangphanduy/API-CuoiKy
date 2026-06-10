using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class CoursePurchase
{
    public int PurchaseId { get; set; }

    public int CourseId { get; set; }

    public int BuyerId { get; set; }

    public decimal PricePaid { get; set; }

    public string Currency { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime PurchasedAt { get; set; }

    public virtual User Buyer { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
