
using StarsWarsPlanets.Models;
using System.Text.Json;

namespace StarsWarsPlanets.Services;

internal class HttpService
{
    public static async Task<List<Planet>> ReadAPI(string uri, string endpoint)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(uri);
        HttpResponseMessage response = await client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        string? json = await response.Content.ReadAsStringAsync();
        if (json is null) return [];
        return JsonSerializer.Deserialize<List<Planet>>(json)!;
    }
}
