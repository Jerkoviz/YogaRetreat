using Blazored.LocalStorage;
using System.Text.Json;

namespace YogaRetreat.Services;

public class CacheService : ICacheService
{
    private const string KeyPrefix = "yogaretreat_";
    private readonly ILocalStorageService _localStorage;

    public CacheService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        try
        {
            var prefixedKey = KeyPrefix + key;
            var json = await _localStorage.GetItemAsStringAsync(prefixedKey);
            if (string.IsNullOrEmpty(json))
                return default;

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("expiry", out var expiryEl) ||
                !root.TryGetProperty("data", out var dataEl))
                return default;

            if (!DateTime.TryParse(expiryEl.GetString(), out var expiry))
                return default;

            if (DateTime.UtcNow > expiry)
            {
                await RemoveAsync(key);
                return default;
            }

            var dataJson = dataEl.GetRawText();
            return JsonSerializer.Deserialize<T>(dataJson);
        }
        catch
        {
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan ttl)
    {
        try
        {
            var prefixedKey = KeyPrefix + key;
            var expiry = DateTime.UtcNow.Add(ttl).ToString("O");
            var dataJson = JsonSerializer.Serialize(value);
            var envelope = $"{{\"expiry\":\"{expiry}\",\"data\":{dataJson}}}";
            await _localStorage.SetItemAsStringAsync(prefixedKey, envelope);
        }
        catch
        {
            // Silently fail — cache is best-effort
        }
    }

    public async Task RemoveAsync(string key)
    {
        try
        {
            await _localStorage.RemoveItemAsync(KeyPrefix + key);
        }
        catch
        {
            // Silently fail
        }
    }

    public async Task ClearAllAsync()
    {
        try
        {
            var keys = await _localStorage.KeysAsync();
            var yogaKeys = keys.Where(k => k.StartsWith(KeyPrefix)).ToList();
            foreach (var k in yogaKeys)
                await _localStorage.RemoveItemAsync(k);
        }
        catch
        {
            // Silently fail
        }
    }
}
