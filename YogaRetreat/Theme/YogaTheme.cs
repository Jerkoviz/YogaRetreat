using MudBlazor;

namespace YogaRetreat.Theme;

public static class YogaTheme
{
    public static MudTheme Create() => new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#6B8C6B",
            Secondary = "#C4785A",
            Background = "#F8F5F0",
            Surface = "#FFFFFF",
            AppbarBackground = "#6B8C6B",
            AppbarText = "#FFFFFF",
            DrawerBackground = "#F8F5F0",
            TextPrimary = "#2D2D2D",
            TextSecondary = "#5A5A5A",
            ActionDefault = "#6B8C6B",
            ActionDisabled = "#BDBDBD",
            ActionDisabledBackground = "#EEEEEE",
            Divider = "#E0DDD8",
            DividerLight = "#F0EDE8",
            TableLines = "#E0DDD8",
            LinesDefault = "#E0DDD8",
            LinesInputs = "#BDBDBD",
            TextDisabled = "#BDBDBD",
            Info = "#4A90D9",
            Success = "#6B8C6B",
            Warning = "#C4785A",
            Error = "#B5373A",
            Dark = "#2D2D2D"
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "1rem",
                FontWeight = "400",
                LineHeight = "1.6"
            },
            H1 = new H1Typography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "3rem",
                FontWeight = "600",
                LineHeight = "1.2"
            },
            H2 = new H2Typography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "2.25rem",
                FontWeight = "600",
                LineHeight = "1.25"
            },
            H3 = new H3Typography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "1.75rem",
                FontWeight = "600",
                LineHeight = "1.3"
            },
            H4 = new H4Typography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "1.5rem",
                FontWeight = "600",
                LineHeight = "1.35"
            },
            H5 = new H5Typography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "1.25rem",
                FontWeight = "600",
                LineHeight = "1.4"
            },
            H6 = new H6Typography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "1.1rem",
                FontWeight = "600",
                LineHeight = "1.4"
            },
            Button = new ButtonTypography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "0.95rem",
                FontWeight = "600",
                TextTransform = "none"
            },
            Subtitle1 = new Subtitle1Typography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "1.1rem",
                FontWeight = "400",
                LineHeight = "1.5"
            },
            Subtitle2 = new Subtitle2Typography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "1rem",
                FontWeight = "600",
                LineHeight = "1.5"
            },
            Body1 = new Body1Typography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "1rem",
                FontWeight = "400",
                LineHeight = "1.7"
            },
            Body2 = new Body2Typography
            {
                FontFamily = ["Cormorant Garamond", "Georgia", "serif"],
                FontSize = "0.9rem",
                FontWeight = "400",
                LineHeight = "1.6"
            }
        },
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "6px"
        }
    };
}
