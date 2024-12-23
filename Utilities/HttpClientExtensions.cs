using System.Net.Http.Json;

namespace CSLama.Utilities;

public static class HttpClientExtensions
{
    public static async Task<T?> PostAsJsonAsync<T>(this HttpClient httpClient, string url, object payload)
    {
        var response = await httpClient.PostAsJsonAsync(url, payload);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public static async Task<T?> GetFromJsonAsync<T>(this HttpClient httpClient, string url)
    {
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }
}
