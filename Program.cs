using StarsWarsPlanets.API;
using StarsWarsPlanets.Models;
using StarsWarsPlanets.UI;
using System.Text.Json;


var apiBaseUrl = "https://swapi.info/api/";
var apiEndpoint = "planets";

IAPIDataReader apiDataReader = new APIDataReaderHttpClient();

string json = await apiDataReader.Read(apiBaseUrl, apiEndpoint);

List<Planet>? planets = JsonSerializer.Deserialize<List<Planet>>(json);

var basicPlanetList = CreateBasicPlanets(planets);

var dataPrinter = new DataPrinter(basicPlanetList);

if (basicPlanetList is not null)
{
    dataPrinter.PrintPlanetsUsingReflection();
}

string usersChoice = dataPrinter.AskUserWhichStatisticWantsToSee();
dataPrinter.PrintStatisticsForThePropertySelectedByUser(usersChoice);


static List<BasicPlanet> CreateBasicPlanets(IEnumerable<Planet> planets)
{
    var basicPlanets = new List<BasicPlanet>();
    foreach (var planet in planets)
    {
        basicPlanets.Add(new BasicPlanet(planet.Name, planet.Diameter, planet.SurfaceWater, planet.Population));
    }
    return basicPlanets;
}

Console.ReadKey();