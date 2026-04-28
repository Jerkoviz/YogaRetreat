using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using YogaRetreat.Models;

namespace YogaRetreat.Services;

public class ContentfulService : IContentfulService
{
    private readonly IContentfulClient _client;
    private readonly ICacheService _cache;

    private static readonly TimeSpan EventsTtl = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan GalleryTtl = TimeSpan.FromMinutes(60);
    private static readonly TimeSpan TestimonialsTtl = TimeSpan.FromMinutes(60);
    private static readonly TimeSpan SiteConfigTtl = TimeSpan.FromMinutes(60);

    public ContentfulService(IContentfulClient client, ICacheService cache)
    {
        _client = client;
        _cache = cache;
    }

    // ─── Events ──────────────────────────────────────────────────────────────

    public async Task<IReadOnlyList<IRetreatEvent>> GetFeaturedEventsAsync()
    {
        const string cacheKey = "events_featured";
        var cached = await _cache.GetAsync<List<RetreatEvent>>(cacheKey);
        if (cached is not null)
            return cached;

        try
        {
            var qb = new QueryBuilder<ContentfulRetreatEvent>()
                .ContentTypeIs("retreatEvent")
                .FieldEquals("fields.isFeatured", "true")
                .OrderBy("fields.startDate");

            var entries = await _client.GetEntries(qb, CancellationToken.None);
            var result = entries.Select(MapEvent).Where(e => e is not null).Cast<RetreatEvent>().ToList();
            await _cache.SetAsync(cacheKey, result, EventsTtl);
            return result;
        }
        catch
        {
            return Array.Empty<IRetreatEvent>();
        }
    }

    public async Task<IReadOnlyList<IRetreatEvent>> GetAllEventsAsync()
    {
        const string cacheKey = "events_all";
        var cached = await _cache.GetAsync<List<RetreatEvent>>(cacheKey);
        if (cached is not null)
            return cached;

        try
        {
            var qb = new QueryBuilder<ContentfulRetreatEvent>()
                .ContentTypeIs("retreatEvent")
                .OrderBy("fields.startDate");

            var entries = await _client.GetEntries(qb, CancellationToken.None);
            var result = entries.Select(MapEvent).Where(e => e is not null).Cast<RetreatEvent>().ToList();
            await _cache.SetAsync(cacheKey, result, EventsTtl);
            return result;
        }
        catch
        {
            return Array.Empty<IRetreatEvent>();
        }
    }

    public async Task<IRetreatEvent?> GetEventBySlugAsync(string slug)
    {
        var cacheKey = $"event_{slug}";
        var cached = await _cache.GetAsync<RetreatEvent>(cacheKey);
        if (cached is not null)
            return cached;

        try
        {
            var qb = new QueryBuilder<ContentfulRetreatEvent>()
                .ContentTypeIs("retreatEvent")
                .FieldEquals("fields.slug", slug);

            var entries = await _client.GetEntries(qb, CancellationToken.None);
            var entry = entries.FirstOrDefault();
            if (entry is null)
                return null;

            var result = MapEvent(entry);
            if (result is not null)
                await _cache.SetAsync(cacheKey, result, EventsTtl);
            return result;
        }
        catch
        {
            return null;
        }
    }

    public async Task<IReadOnlyList<IRetreatEvent>> GetUpcomingEventsAsync(int max = 3)
    {
        var cacheKey = $"events_upcoming_{max}";
        var cached = await _cache.GetAsync<List<RetreatEvent>>(cacheKey);
        if (cached is not null)
            return cached;

        try
        {
            var now = DateTime.UtcNow.ToString("O");
            var qb = new QueryBuilder<ContentfulRetreatEvent>()
                .ContentTypeIs("retreatEvent")
                .FieldGreaterThan("fields.startDate", now)
                .OrderBy("fields.startDate")
                .Limit(max);

            var entries = await _client.GetEntries(qb, CancellationToken.None);
            var result = entries.Select(MapEvent).Where(e => e is not null).Cast<RetreatEvent>().ToList();
            await _cache.SetAsync(cacheKey, result, EventsTtl);
            return result;
        }
        catch
        {
            return Array.Empty<IRetreatEvent>();
        }
    }

    // ─── Gallery ─────────────────────────────────────────────────────────────

    public async Task<IReadOnlyList<IGalleryAlbum>> GetGalleryAlbumsAsync()
    {
        const string cacheKey = "gallery_albums";
        var cached = await _cache.GetAsync<List<GalleryAlbum>>(cacheKey);
        if (cached is not null)
            return cached;

        try
        {
            var qb = new QueryBuilder<ContentfulGalleryAlbum>()
                .ContentTypeIs("galleryAlbum")
                .OrderBy("-fields.date");

            var entries = await _client.GetEntries(qb, CancellationToken.None);
            var result = entries.Select(MapAlbum).Where(e => e is not null).Cast<GalleryAlbum>().ToList();
            await _cache.SetAsync(cacheKey, result, GalleryTtl);
            return result;
        }
        catch
        {
            return Array.Empty<IGalleryAlbum>();
        }
    }

    public async Task<IGalleryAlbum?> GetGalleryAlbumBySlugAsync(string slug)
    {
        var cacheKey = $"album_{slug}";
        var cached = await _cache.GetAsync<GalleryAlbum>(cacheKey);
        if (cached is not null)
            return cached;

        try
        {
            var qb = new QueryBuilder<ContentfulGalleryAlbum>()
                .ContentTypeIs("galleryAlbum")
                .FieldEquals("fields.slug", slug);

            var entries = await _client.GetEntries(qb, CancellationToken.None);
            var entry = entries.FirstOrDefault();
            if (entry is null)
                return null;

            var result = MapAlbum(entry);
            if (result is not null)
                await _cache.SetAsync(cacheKey, result, GalleryTtl);
            return result;
        }
        catch
        {
            return null;
        }
    }

    // ─── Testimonials ─────────────────────────────────────────────────────────

    public async Task<IReadOnlyList<ITestimonial>> GetTestimonialsAsync()
    {
        const string cacheKey = "testimonials";
        var cached = await _cache.GetAsync<List<Testimonial>>(cacheKey);
        if (cached is not null)
            return cached;

        try
        {
            var qb = new QueryBuilder<ContentfulTestimonial>()
                .ContentTypeIs("testimonial")
                .FieldEquals("fields.isActive", "true")
                .OrderBy("-fields.date");

            var entries = await _client.GetEntries(qb, CancellationToken.None);
            var result = entries.Select(MapTestimonial).Where(e => e is not null).Cast<Testimonial>().ToList();
            await _cache.SetAsync(cacheKey, result, TestimonialsTtl);
            return result;
        }
        catch
        {
            return Array.Empty<ITestimonial>();
        }
    }

    // ─── SiteConfig ───────────────────────────────────────────────────────────

    public async Task<ISiteConfig?> GetSiteConfigAsync()
    {
        const string cacheKey = "siteconfig";
        var cached = await _cache.GetAsync<SiteConfig>(cacheKey);
        if (cached is not null)
            return cached;

        try
        {
            var qb = new QueryBuilder<ContentfulSiteConfig>()
                .ContentTypeIs("siteConfig")
                .Limit(1);

            var entries = await _client.GetEntries(qb, CancellationToken.None);
            var entry = entries.FirstOrDefault();
            if (entry is null)
                return null;

            var result = MapSiteConfig(entry);
            if (result is not null)
                await _cache.SetAsync(cacheKey, result, SiteConfigTtl);
            return result;
        }
        catch
        {
            return null;
        }
    }

    // ─── Mappers ─────────────────────────────────────────────────────────────

    private static string FixAssetUrl(string? url)
    {
        if (string.IsNullOrEmpty(url))
            return string.Empty;
        return url.StartsWith("//") ? "https:" + url : url;
    }

    private static RetreatEvent? MapEvent(ContentfulRetreatEvent entry)
    {
        if (entry is null) return null;
        return new RetreatEvent(
            Slug: entry.Slug ?? string.Empty,
            Title: entry.Title ?? string.Empty,
            ShortDescription: entry.ShortDescription ?? string.Empty,
            BodyMarkdown: entry.Body ?? string.Empty,
            StartDate: entry.StartDate ?? DateTime.MinValue,
            EndDate: entry.EndDate ?? DateTime.MinValue,
            Location: entry.Location ?? string.Empty,
            PriceEur: entry.PriceEur,
            CapacityMax: entry.CapacityMax,
            SpotsRemaining: entry.SpotsRemaining,
            HeroImageUrl: FixAssetUrl(entry.HeroImage?.File?.Url),
            TallyFormId: entry.TallyFormId,
            Tags: entry.Tags ?? [],
            IsFeatured: entry.IsFeatured
        );
    }

    private static GalleryAlbum? MapAlbum(ContentfulGalleryAlbum entry)
    {
        if (entry is null) return null;
        var imageUrls = entry.Images?
            .Select(a => FixAssetUrl(a?.File?.Url))
            .Where(u => !string.IsNullOrEmpty(u))
            .ToArray() ?? [];
        return new GalleryAlbum(
            Slug: entry.Slug ?? string.Empty,
            Title: entry.Title ?? string.Empty,
            Description: entry.Description,
            CoverImageUrl: FixAssetUrl(entry.CoverImage?.File?.Url),
            ImageUrls: imageUrls!,
            Date: entry.Date ?? DateTime.MinValue,
            LinkedEventSlug: entry.LinkedEvent?.Slug
        );
    }

    private static Testimonial? MapTestimonial(ContentfulTestimonial entry)
    {
        if (entry is null) return null;
        return new Testimonial(
            AuthorName: entry.AuthorName ?? string.Empty,
            AuthorPhotoUrl: FixAssetUrl(entry.AuthorPhoto?.File?.Url).NullIfEmpty(),
            Quote: entry.Quote ?? string.Empty,
            Rating: entry.Rating,
            RetreatTitle: entry.RetreatTitle,
            Date: entry.Date ?? DateTime.MinValue,
            IsActive: entry.IsActive
        );
    }

    private static SiteConfig? MapSiteConfig(ContentfulSiteConfig entry)
    {
        if (entry is null) return null;
        return new SiteConfig(
            SiteName: entry.SiteName ?? string.Empty,
            Tagline: entry.Tagline ?? string.Empty,
            LogoUrl: FixAssetUrl(entry.Logo?.File?.Url),
            HeroVideoUrl: FixAssetUrl(entry.HeroVideo?.File?.Url).NullIfEmpty(),
            HeroFallbackImageUrl: FixAssetUrl(entry.HeroFallbackImage?.File?.Url),
            ContactEmail: entry.ContactEmail ?? string.Empty,
            InstagramUrl: entry.InstagramUrl,
            FacebookUrl: entry.FacebookUrl,
            YouTubeUrl: entry.YouTubeUrl,
            PrivacyPolicyBody: entry.PrivacyPolicy ?? string.Empty,
            DefaultTallyFormId: entry.DefaultTallyFormId ?? string.Empty
        );
    }
}

// ─── Contentful field DTOs ────────────────────────────────────────────────────

public class ContentfulRetreatEvent
{
    public string? Slug { get; set; }
    public string? Title { get; set; }
    public string? ShortDescription { get; set; }
    public string? Body { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Location { get; set; }
    public decimal PriceEur { get; set; }
    public int CapacityMax { get; set; }
    public int? SpotsRemaining { get; set; }
    public Asset? HeroImage { get; set; }
    public string? TallyFormId { get; set; }
    public string[]? Tags { get; set; }
    public bool IsFeatured { get; set; }
}

public class ContentfulGalleryAlbum
{
    public string? Slug { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Asset? CoverImage { get; set; }
    public List<Asset>? Images { get; set; }
    public DateTime? Date { get; set; }
    public ContentfulLinkedEvent? LinkedEvent { get; set; }
}

public class ContentfulLinkedEvent
{
    public string? Slug { get; set; }
}

public class ContentfulTestimonial
{
    public string? AuthorName { get; set; }
    public Asset? AuthorPhoto { get; set; }
    public string? Quote { get; set; }
    public int Rating { get; set; }
    public string? RetreatTitle { get; set; }
    public DateTime? Date { get; set; }
    public bool IsActive { get; set; }
}

public class ContentfulSiteConfig
{
    public string? SiteName { get; set; }
    public string? Tagline { get; set; }
    public Asset? Logo { get; set; }
    public Asset? HeroVideo { get; set; }
    public Asset? HeroFallbackImage { get; set; }
    public string? ContactEmail { get; set; }
    public string? InstagramUrl { get; set; }
    public string? FacebookUrl { get; set; }
    public string? YouTubeUrl { get; set; }
    public string? PrivacyPolicy { get; set; }
    public string? DefaultTallyFormId { get; set; }
}

internal static class StringExtensions
{
    public static string? NullIfEmpty(this string? s) =>
        string.IsNullOrEmpty(s) ? null : s;
}
