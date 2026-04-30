using Blazored.LocalStorage;

namespace YogaRetreat.Services;

public class LanguageService
{
    private const string StorageKey = "lang";
    private readonly ILocalStorageService _storage;

    public string Current { get; private set; } = "sr";
    public event Action? OnChanged;

    public LanguageService(ILocalStorageService storage) => _storage = storage;

    public async Task InitAsync()
    {
        var stored = await _storage.GetItemAsync<string>(StorageKey);
        Current = stored is "sr" or "en" ? stored : "sr";
    }

    public async Task SetAsync(string lang)
    {
        if (lang == Current) return;
        Current = lang;
        await _storage.SetItemAsync(StorageKey, lang);
        OnChanged?.Invoke();
    }

    public string T(string sr, string en) => Current == "en" ? en : sr;
}
