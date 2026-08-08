
using StarsWarsPlanets.Enums;

namespace StarsWarsPlanets.ConsoleUI;

internal class UserInput
{
    public static string AskUserWhichStatisticWantsToSee()
    {

        Console.WriteLine("The statistics of which property would you like to see?");
        foreach (var basicPlanetProperty in Enum.GetValues<BasicPlanetProperties>())
        {
            Console.WriteLine(basicPlanetProperty);
        }
        Console.WriteLine();
        return Console.ReadLine() ?? "";
    }
}
