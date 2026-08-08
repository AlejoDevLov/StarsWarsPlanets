
using System.Text.Json;

namespace StarsWarsPlanets.Services;

internal class HttpService
{
    public static async Task<List<Planet>> ReadAPI(string uri, string endpoint)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(uri);
        try
        {
            HttpResponseMessage response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            string? json = await response.Content.ReadAsStringAsync();
            if (json is null) return [];
            return JsonSerializer.Deserialize<List<Planet>>(json)!;
        }
        catch (JsonException ex) 
        {
            Console.WriteLine($"An exception ocurred when trying to deserialize the json file. {ex.Message}");
            return [];
        }
        catch(ArgumentException ex)
        {
            Console.WriteLine($"The argument in the Deserialize method is not valid. {ex.Message}.");
            return [];
        }
    }
}
