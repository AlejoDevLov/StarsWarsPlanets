
namespace StarsWarsPlanets.API;

internal class APIDataReaderHttpClient : IAPIDataReader
{
    public async Task<string> Read(string uri, string endpoint)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(uri);
        HttpResponseMessage response = await client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
