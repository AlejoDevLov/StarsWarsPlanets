using StarsWarsPlanets.API;
using StarsWarsPlanets.UI;
using System.Text.Json;


var apiBaseUrl = "https://swapi.info/api/";
var apiEndpoint = "planets";

IAPIDataReader apiDataReader = new APIDataReaderHttpClient();

string json = await apiDataReader.Read(apiBaseUrl, apiEndpoint);

List<Planet>? planets = JsonSerializer.Deserialize<List<Planet>>(json);

if(planets is not null)
{
    foreach(var planet in planets)
    {
        Console.WriteLine(planet);
        ConsoleUI.PrintMessage("hello");
    }
}

Console.ReadKey();