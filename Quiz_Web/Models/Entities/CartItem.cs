using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public int CartId { get; set; }

    public int CourseId { get; set; }

    public DateTime AddedAt { get; set; }

    public virtual ShoppingCart Cart { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
