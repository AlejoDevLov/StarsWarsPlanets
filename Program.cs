using StarsWarsPlanets.ConsoleUI;
using StarsWarsPlanets.Services;


var apiBaseUrl = "https://swapi.info/api/";
var apiEndpoint = "planets";

try
{
    List<Planet> planets = await HttpService.ReadAPI(apiBaseUrl, apiEndpoint);
    if(planets.Any() && planets is not null)
    {
        var basicPlanetList = ParsePlanetsService.GenerateBasicPlanets(planets);
        var dataPrinter = new DataPrinter(basicPlanetList);

        if (basicPlanetList is not null)
        {
            dataPrinter.PrintPlanetsUsingReflection();
        }

        string usersChoice = UserInput.AskUserWhichStatisticWantsToSee();
        dataPrinter.PrintStatisticsForThePropertySelectedByUser(usersChoice);
    
    }
} 
catch (Exception ex)
{
    Console.WriteLine("An unexpected error ocurred. " + ex.Message + " " + ex.StackTrace);
}


Console.ReadKey();