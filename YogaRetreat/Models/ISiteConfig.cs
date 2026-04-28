namespace YogaRetreat.Models;

public interface ISiteConfig
{
    string SiteName { get; }
    string Tagline { get; }
    string LogoUrl { get; }
    string? HeroVideoUrl { get; }
    string HeroFallbackImageUrl { get; }
    string ContactEmail { get; }
    string? InstagramUrl { get; }
    string? FacebookUrl { get; }
    string? YouTubeUrl { get; }
    string PrivacyPolicyBody { get; }
    string DefaultTallyFormId { get; }
}

public record SiteConfig(
    string SiteName,
    string Tagline,
    string LogoUrl,
    string? HeroVideoUrl,
    string HeroFallbackImageUrl,
    string ContactEmail,
    string? InstagramUrl,
    string? FacebookUrl,
    string? YouTubeUrl,
    string PrivacyPolicyBody,
    string DefaultTallyFormId
) : ISiteConfig;
