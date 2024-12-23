using CSLama.Models;
using System.Net.Http.Json;

namespace CSLama.Services;

public class OllamaApiService
{
    private readonly HttpClient _httpClient;

    public OllamaApiService(string baseUrl = "http://localhost:11434/api/")
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl),
            Timeout = TimeSpan.FromHours(3) // Set timeout to 3 hours
        };
    }

    public async Task<OllamaGenerateResponse> GenerateCompletionAsync(string model, string prompt, bool stream)
    {
        var payload = new
        {
            model,
            prompt,
            stream
        };

        var response = await _httpClient.PostAsJsonAsync("generate", payload);
        response.EnsureSuccessStatusCode();

        // Deserialize to OllamaGenerateResponse
        var generateResponse = await response.Content.ReadFromJsonAsync<OllamaGenerateResponse>();
        if (generateResponse == null)
        {
            throw new Exception("Failed to parse the API response.");
        }

        return generateResponse;
    }

    public async Task<OllamaTagResponse> ListModelsAsync()
    {
        var response = await _httpClient.GetAsync("tags");
        response.EnsureSuccessStatusCode();

        var modelsResponse = await response.Content.ReadFromJsonAsync<OllamaTagResponse>();
        if (modelsResponse == null)
        {
            throw new Exception("Failed to parse the API response.");
        }

        return modelsResponse;
    }

    public async Task<string> PullModelAsync(string model)
    {
        var payload = new
        {
            model
        };
        var response = await _httpClient.PostAsJsonAsync("pull", payload);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
