using YogaRetreat.Models;

namespace YogaRetreat.Services;

public class StaticDataService : IContentfulService
{
    private static readonly ISiteConfig Config = new SiteConfig(
        SiteName: "Yoga Retreat",
        Tagline: "Find your balance",
        LogoUrl: "",
        HeroVideoUrl: null,
        HeroFallbackImageUrl: "",
        ContactEmail: "info@example.com",
        InstagramUrl: "https://www.instagram.com/shumozov",
        FacebookUrl: null,
        YouTubeUrl: null,
        PrivacyPolicyBody: "",
        DefaultTallyFormId: ""
    );

    public Task<ISiteConfig?> GetSiteConfigAsync() =>
        Task.FromResult<ISiteConfig?>(Config);

    public Task<IReadOnlyList<IRetreatEvent>> GetFeaturedEventsAsync() =>
        Task.FromResult<IReadOnlyList<IRetreatEvent>>(Array.Empty<IRetreatEvent>());

    public Task<IReadOnlyList<IRetreatEvent>> GetAllEventsAsync() =>
        Task.FromResult<IReadOnlyList<IRetreatEvent>>(Array.Empty<IRetreatEvent>());

    public Task<IReadOnlyList<IRetreatEvent>> GetUpcomingEventsAsync(int max = 3) =>
        Task.FromResult<IReadOnlyList<IRetreatEvent>>(Array.Empty<IRetreatEvent>());

    public Task<IRetreatEvent?> GetEventBySlugAsync(string slug) =>
        Task.FromResult<IRetreatEvent?>(null);

    public Task<IReadOnlyList<IGalleryAlbum>> GetGalleryAlbumsAsync() =>
        Task.FromResult<IReadOnlyList<IGalleryAlbum>>(Array.Empty<IGalleryAlbum>());

    public Task<IGalleryAlbum?> GetGalleryAlbumBySlugAsync(string slug) =>
        Task.FromResult<IGalleryAlbum?>(null);

    public Task<IReadOnlyList<ITestimonial>> GetTestimonialsAsync() =>
        Task.FromResult<IReadOnlyList<ITestimonial>>(Array.Empty<ITestimonial>());
}
