namespace StarsWarsPlanets.ConsoleUI;

internal class UserInput
{
    public static string AskUserWhichStatisticWantsToSee(IEnumerable<string> basicPlanetProperties)
    {

        Console.WriteLine("The statistics of which property would you like to see?");
        foreach (var basicPlanetProperty in basicPlanetProperties)
        {
            Console.WriteLine(basicPlanetProperty);
        }
        Console.WriteLine();
        return Console.ReadLine() ?? "";
    }
}
