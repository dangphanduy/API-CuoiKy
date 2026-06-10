using System;
using System.Collections.Generic;

namespace Quiz_Web.Models.Entities;

public partial class Course
{
    public int CourseId { get; set; }

    public int OwnerId { get; set; }

    public int? CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Summary { get; set; }

    public string? CoverUrl { get; set; }

    public decimal Price { get; set; }

    public bool IsPublished { get; set; }

    public decimal AverageRating { get; set; }

    public int TotalReviews { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual CourseCategory? Category { get; set; }

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<CourseChapter> CourseChapters { get; set; } = new List<CourseChapter>();

    public virtual ICollection<CourseProgress> CourseProgresses { get; set; } = new List<CourseProgress>();

    public virtual ICollection<CoursePurchase> CoursePurchases { get; set; } = new List<CoursePurchase>();

    public virtual ICollection<CourseReview> CourseReviews { get; set; } = new List<CourseReview>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User Owner { get; set; } = null!;
}
