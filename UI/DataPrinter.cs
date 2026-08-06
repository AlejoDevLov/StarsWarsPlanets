using StarsWarsPlanets.Models;
using System.Reflection;

namespace StarsWarsPlanets.UI;

enum BasicPlanetProperties
{
    Diameter,
    Surface,
    Population
}

enum FilteringCriteria { Max, Min }


// This class represents the object's data in a chart in the console. 
internal class DataPrinter
{
    //This field defines the width of the cell that represents each value in the console
    private readonly int _cellLength = 20;
    private readonly IEnumerable<BasicPlanet> Planets;

    internal DataPrinter(IEnumerable<BasicPlanet> planets)
    {
        Planets = planets;
    }

    public void PrintPlanetsUsingReflection()
    {
        if (Planets.Any())
        {
            PropertyInfo[] properties = Planets.ElementAt(0).GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            // The code in this loop defines the headers of the chart
            foreach (PropertyInfo property in properties)
            {
                Console.Write($"{property.Name}" + " ".PadLeft(_cellLength - property.Name.Length) + "|");
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', _cellLength * properties.Length + properties.Length));

            // The code in these loops defines the values for the chart
            PropertyInfo[] props = Planets.ElementAt(0).GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var obj in Planets)
            {
                foreach (PropertyInfo property in props)
                {
                    var item = property.GetValue(obj);
                    if (item is null)
                    {
                        Console.WriteLine("Null" + " ".PadLeft(_cellLength - 4));
                    }
                    else if (item.ToString()!.Length >= _cellLength)
                    {
                        Console.WriteLine(item.ToString()!.Substring(0, _cellLength - 3).Concat("  |"));
                    }
                    else
                    {
                        Console.Write($"{item + " ".PadLeft(_cellLength - item!.ToString()!.Length) + "|"}");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    public string AskUserWhichStatisticWantsToSee()
    {

        Console.WriteLine("The statistics of which property would you like to see?");
        foreach(var basicPlanetProperty in Enum.GetValues<BasicPlanetProperties>())
        {
            Console.WriteLine(basicPlanetProperty);
        }
        Console.WriteLine();
        return Console.ReadLine() ?? "";
    }

    public void PrintStatisticsForThePropertySelectedByUser(string userChoice)
    {
        try
        {
            BasicPlanetProperties planetProperty = ConvertUserChoiceToPlanetProperty(userChoice);

            var (planetNameMax, maxValue) = FindPlanetNameAndMaxOrMinValueByProperty(planetProperty, FilteringCriteria.Max);
            var (planetNameMin, minValue) = FindPlanetNameAndMaxOrMinValueByProperty(planetProperty, FilteringCriteria.Min);

            Console.WriteLine($"The max {planetProperty} is {maxValue} (Planet: {planetNameMax})");
            Console.WriteLine($"The min {planetProperty} is {minValue} (Planet: {planetNameMin})");
            Console.WriteLine("Press any key to close");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Invalid choice.");
            Console.WriteLine("Press any key to close");
        }
    }

    private BasicPlanetProperties ConvertUserChoiceToPlanetProperty(string userChoice)
    {
        userChoice = userChoice.ToLower().Trim();
        return userChoice switch
        {
            "population" => BasicPlanetProperties.Population,
            "diameter" => BasicPlanetProperties.Diameter,
            "surface" => BasicPlanetProperties.Surface,
            _ => throw new ArgumentException($"The user choice ({userChoice}) is invalid.")
        };
    } 
    private string FindValueForThePlanetProperty(BasicPlanetProperties property, BasicPlanet planet) =>
         property switch
        {
            BasicPlanetProperties.Diameter => planet.Diameter,
            BasicPlanetProperties.Surface => planet.SurfaceWater,
            BasicPlanetProperties.Population => planet.Population,
            _ => throw new InvalidFilterCriteriaException()
        }; 

    private (string PlanetName, string PropertyValue) FindPlanetNameAndMaxOrMinValueByProperty(BasicPlanetProperties planetProperty, FilteringCriteria filter)
    {
        var planets = Planets
                .Select(p => (PlanetName: p.Name, PropertyValue: FindValueForThePlanetProperty(planetProperty, p)))
                .Where(p => long.TryParse(p.PropertyValue, out _));
        return filter == FilteringCriteria.Max ? planets.MaxBy(p => p.PropertyValue) : planets.MinBy(p => p.PropertyValue);
    }
}