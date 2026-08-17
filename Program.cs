using StarsWarsPlanets.ConsoleUI;
using StarsWarsPlanets.Exceptions;
using StarsWarsPlanets.Models;
using StarsWarsPlanets.Services;


var apiBaseUrl = "https://swapi.info/api/";
var apiEndpoint = "planets";

try
{
    List<Planet> planets = await HttpService.ReadAPI(apiBaseUrl, apiEndpoint);
    if(planets.Count != 0 && planets is not null)
    {
        IEnumerable<BasicPlanet> basicPlanetList = planets.Select( p => (BasicPlanet)p );

        var dataPrinter = new DataPrinter<BasicPlanet>(basicPlanetList);

        if (basicPlanetList is not null)
        {
            dataPrinter.PrintPlanetsUsingReflection();
        }

        var propertyValuesByPlanetProperty = new Dictionary<string, Func<BasicPlanet, string>>
        {
            ["population"] = planet => planet.Population,
            ["surface"] = planet => planet.SurfaceWater,
            ["diameter"] = planet => planet.Diameter
        };

        string userChoice = UserInput.AskUserWhichStatisticWantsToSee(propertyValuesByPlanetProperty.Keys);
        userChoice = userChoice.ToLower().Trim();


        if(!propertyValuesByPlanetProperty.ContainsKey(userChoice))
            throw new InvalidUserChoiceException($"The user choice ({userChoice}) is invalid.");

        dataPrinter.PrintStatisticsForThePropertySelectedByUser(userChoice, propertyValuesByPlanetProperty[userChoice]);
    
    }
}
catch(InvalidUserChoiceException ex)
{
    Console.WriteLine(ex.Message);
}
catch (Exception ex)
{
    Console.WriteLine("An unexpected error ocurred." +
        "Error Message: " + ex.Message + 
        "Stack Trace: " + ex.StackTrace);
}


Console.ReadKey();