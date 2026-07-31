using StarsWarsPlanets.API;
using StarsWarsPlanets.UI;
using System.Text.Json;


var apiBaseUrl = "https://swapi.info/api/";
var apiEndpoint = "planets";

IAPIDataReader apiDataReader = new APIDataReaderHttpClient();

string json = await apiDataReader.Read(apiBaseUrl, apiEndpoint);

List<Planet>? planets = JsonSerializer.Deserialize<List<Planet>>(json);

IObjectDataPrinter objectPrinter = new ReflectionObjectDataPrinter();

if (planets is not null)
{
    objectPrinter.PrintPlanets(planets);
}

Console.ReadKey();