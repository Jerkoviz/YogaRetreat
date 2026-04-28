namespace YogaRetreat.Models;

public interface IRetreatEvent
{
    string Slug { get; }
    string Title { get; }
    string ShortDescription { get; }
    string BodyMarkdown { get; }
    DateTime StartDate { get; }
    DateTime EndDate { get; }
    string Location { get; }
    decimal PriceEur { get; }
    int CapacityMax { get; }
    int? SpotsRemaining { get; }
    string HeroImageUrl { get; }
    string? TallyFormId { get; }
    string[] Tags { get; }
    bool IsFeatured { get; }
}

public record RetreatEvent(
    string Slug,
    string Title,
    string ShortDescription,
    string BodyMarkdown,
    DateTime StartDate,
    DateTime EndDate,
    string Location,
    decimal PriceEur,
    int CapacityMax,
    int? SpotsRemaining,
    string HeroImageUrl,
    string? TallyFormId,
    string[] Tags,
    bool IsFeatured
) : IRetreatEvent;
