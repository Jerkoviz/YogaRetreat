# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Run dev server (hot reload)
dotnet watch --project YogaRetreat/YogaRetreat.csproj

# Build only
dotnet build YogaRetreat/YogaRetreat.csproj

# Publish for Cloudflare Pages
dotnet publish YogaRetreat/YogaRetreat.csproj -c Release -o publish
```

VS Code: press F5 with the `blazorwasm` launch config (`.vscode/launch.json`) — starts `dotnet watch` and opens Chrome at `https://localhost:5001`.

## Architecture

Blazor WebAssembly standalone on .NET 10, no server-side rendering. All data fetched client-side.

```
YogaRetreat/
  Models/          # Interfaces (IRetreatEvent, IGalleryAlbum, ITestimonial, ISiteConfig)
                   # + record implementations (RetreatEvent, GalleryAlbum, …)
  Services/        # IContentfulService / ContentfulService — all Contentful queries + TTL cache
                   # ICacheService / CacheService — Blazored.LocalStorage wrapper
  Theme/           # YogaTheme.cs — single MudBlazor theme (greens/terracotta, Cormorant Garamond)
  Layout/          # MainLayout.razor, NavMenu.razor
  Shared/          # Reusable components: HeroSection, EventCard, CountdownTimer, GalleryGrid,
                   #   GalleryImage, TestimonialSlider, Footer
  Pages/           # Home, Events, EventDetail, Gallery, Register, NotFound
  wwwroot/
    appsettings.json          # Contentful SpaceId + ApiKey go here (not committed with real keys)
    appsettings.Development.json
    _redirects                # Cloudflare Pages SPA fallback: /*  /index.html  200
    css/app.css
    images/                   # terasa.jpg (hero), placeholder.svg
```

### Data flow

`MainLayout` fetches `ISiteConfig` once on init and passes it down via `CascadingValue<ISiteConfig?>`. Pages receive it with `[CascadeParameter]`. All Contentful calls go through `ContentfulService`, which wraps every method with LocalStorage TTL cache (15 min events, 60 min everything else).

### Contentful content types

| Content type | Key fields |
|---|---|
| `retreatEvent` | slug, title, startDate, endDate, location, priceEur, capacityMax, spotsRemaining, heroImage, isFeatured, tags, body (Markdown), tallyFormId |
| `galleryAlbum` | slug, title, coverImage, images[], date, linkedEvent |
| `testimonial` | authorName, authorPhoto, quote, rating, isActive |
| `siteConfig` | siteName, tagline, logo, heroVideo, heroFallbackImage, contactEmail, socialUrls, defaultTallyFormId |

Contentful field DTOs (e.g. `ContentfulRetreatEvent`) live at the bottom of `Services/ContentfulService.cs`. Mappers convert them to immutable records. `FixAssetUrl()` ensures protocol-relative `//images.ctfassets.net` URLs become `https:`.

### UI conventions

- All UI strings are in Serbian (no i18n library — literals in razor files).
- Site name fallback: `"Šumoterapija"`. Hero image fallback: `/images/terasa.jpg`.
- Theme colors: Primary `#6B8C6B` (sage green), Secondary `#C4785A` (terracotta).
- Pages add `padding-top:64px` to clear the fixed AppBar.
- Loading states use `MudSkeleton`; errors use `MudAlert Severity.Error`; empty states use `MudAlert Severity.Info`.
- `CountdownTimer` is `IDisposable` — uses a `System.Threading.Timer` with disposed guard.

### Deployment

Cloudflare Pages. `wwwroot/_redirects` handles SPA routing. Set `Contentful__SpaceId` and `Contentful__ApiKey` as Cloudflare Pages environment variables (they map to `appsettings.json` keys via WASM config).
