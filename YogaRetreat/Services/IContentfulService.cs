using YogaRetreat.Models;

namespace YogaRetreat.Services;

public interface IContentfulService
{
    Task<IReadOnlyList<IRetreatEvent>> GetFeaturedEventsAsync();
    Task<IReadOnlyList<IRetreatEvent>> GetAllEventsAsync();
    Task<IRetreatEvent?> GetEventBySlugAsync(string slug);
    Task<IReadOnlyList<IRetreatEvent>> GetUpcomingEventsAsync(int max = 3);
    Task<IReadOnlyList<IGalleryAlbum>> GetGalleryAlbumsAsync();
    Task<IGalleryAlbum?> GetGalleryAlbumBySlugAsync(string slug);
    Task<IReadOnlyList<ITestimonial>> GetTestimonialsAsync();
    Task<ISiteConfig?> GetSiteConfigAsync();
}
