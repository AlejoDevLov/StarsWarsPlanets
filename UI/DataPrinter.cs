using StarsWarsPlanets.Models;
using System.Reflection;

namespace StarsWarsPlanets.UI;


// This class represents the object's data in a chart in the console. 
internal class DataPrinter
{
    //This field defines the width of the cell that represents each value in the console
    private readonly int _cellLength = 20;
    private readonly IEnumerable<object> Planets;
    private readonly string[] _BasicPlanetProperties = ["Diameter", "Surface", "Population"];

    internal DataPrinter(IEnumerable<object> planets)
    {
        Planets = planets;
    }

    public void PrintPlanetsUsingReflection()
    {
        if (Planets.Any())
        {
            // The code in this loop defines the headers of the chart
            PropertyInfo[] properties = Planets.ElementAt(0).GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo property in properties)
            {
                Console.Write($"{property.Name}" + " ".PadLeft(_cellLength - property.Name.Length) + "|");
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', _cellLength * properties.Length + properties.Length));

            // The code in these loops defines the values for the chart
            foreach (var obj in Planets)
            {
                PropertyInfo[] props = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
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
        foreach(var basicPlanetProperty in _BasicPlanetProperties)
        {
            Console.WriteLine(basicPlanetProperty);
        }
        Console.WriteLine();
        return Console.ReadLine() ?? "";
    }

    public void PrintStatisticsForThePropertySelectedByUser(string usersChoice)
    {
        usersChoice = usersChoice.ToLower().Trim();
        bool isAValidChoice = ValidateUsersChoice(usersChoice);

        if (!isAValidChoice)
        {
            Console.WriteLine("Invalid choice.");
            Console.WriteLine("Press any key to close");
            return;
        }

        Tuple<string, string> planetNameAndMaxValue = FindPlanetNameAndMaxValueByProperty(usersChoice, (IEnumerable<BasicPlanet>)Planets);
        Tuple<string, string> planetNameAndMinValue = FindPlanetNameAndMinValueByProperty(usersChoice, (IEnumerable<BasicPlanet>)Planets);
        
        Console.WriteLine($"The max {usersChoice} is {planetNameAndMaxValue.Item2} (Planet: {planetNameAndMaxValue.Item1})");
        Console.WriteLine($"The min {usersChoice} is {planetNameAndMinValue.Item2} (Planet: {planetNameAndMinValue.Item1 })");
        Console.WriteLine("Press any key to close");
        return;
    }

    private bool ValidateUsersChoice(string usersChoice)
    {
        return _BasicPlanetProperties.Where( property => property.ToLower().Equals(usersChoice) ).Any();
    }

    private Tuple<string, string> FindPlanetNameAndMaxValueByProperty(string property, IEnumerable<BasicPlanet> basicPlanets)
    {
#nullable disable
        // diameter
        if (property.Equals(_BasicPlanetProperties[0].ToLower()))
        {
            var planet = basicPlanets
                .Where(p => long.TryParse(p.Diameter, out _))
                .MaxBy(p => long.Parse(p.Diameter));
            return Tuple.Create(planet.Name, planet.Diameter);
        }
        // Surface
        else if (property.Equals(_BasicPlanetProperties[1].ToLower()))
        {
            var planet = basicPlanets
                .Where(p => long.TryParse(p.SurfaceWater, out _))
                .MaxBy(p => long.Parse(p.SurfaceWater));
            return Tuple.Create(planet.Name, planet.SurfaceWater);
        }
        // Population
        else
        {
            var planet = basicPlanets
                .Where(p => long.TryParse(p.Population, out _))
                .MaxBy(p => long.Parse(p.Population));
            return Tuple.Create(planet.Name, planet.Population);
        }
# nullable enable
    }

    private Tuple<string, string> FindPlanetNameAndMinValueByProperty(string property, IEnumerable<BasicPlanet> basicPlanets)
    {
#nullable disable
        // diameter
        if (property.Equals(_BasicPlanetProperties[0].ToLower()))
        {
            var planet = basicPlanets
                .Where(p => long.TryParse(p.Diameter, out _))
                .MinBy(p => long.Parse(p.Diameter));
            return Tuple.Create(planet.Name, planet.Diameter);
        }
        // Surface
        else if (property.Equals(_BasicPlanetProperties[1].ToLower()))
        {
            var planet = basicPlanets
                .Where(p => long.TryParse(p.SurfaceWater, out _))
                .MinBy(p => long.Parse(p.SurfaceWater));
            return Tuple.Create(planet.Name, planet.SurfaceWater);
        }
        // Population
        else
        {
            var planet = basicPlanets
                .Where(p => long.TryParse(p.Population, out _))
                .MinBy(p => long.Parse(p.Population));
            return Tuple.Create(planet.Name, planet.Population);
        }
# nullable enable
    }
}