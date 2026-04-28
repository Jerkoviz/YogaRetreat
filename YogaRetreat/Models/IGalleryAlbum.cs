namespace YogaRetreat.Models;

public interface IGalleryAlbum
{
    string Slug { get; }
    string Title { get; }
    string? Description { get; }
    string CoverImageUrl { get; }
    string[] ImageUrls { get; }
    DateTime Date { get; }
    string? LinkedEventSlug { get; }
}

public record GalleryAlbum(
    string Slug,
    string Title,
    string? Description,
    string CoverImageUrl,
    string[] ImageUrls,
    DateTime Date,
    string? LinkedEventSlug
) : IGalleryAlbum;
