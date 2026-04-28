namespace YogaRetreat.Models;

public interface ITestimonial
{
    string AuthorName { get; }
    string? AuthorPhotoUrl { get; }
    string Quote { get; }
    int Rating { get; }
    string? RetreatTitle { get; }
    DateTime Date { get; }
    bool IsActive { get; }
}

public record Testimonial(
    string AuthorName,
    string? AuthorPhotoUrl,
    string Quote,
    int Rating,
    string? RetreatTitle,
    DateTime Date,
    bool IsActive
) : ITestimonial;
