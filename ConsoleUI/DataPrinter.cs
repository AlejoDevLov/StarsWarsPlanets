using StarsWarsPlanets.Enums;
using StarsWarsPlanets.Helpers;
using StarsWarsPlanets.Models;
using System.Reflection;

namespace StarsWarsPlanets.ConsoleUI;


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

    public void PrintStatisticsForThePropertySelectedByUser(string userChoice)
    {
        try
        {
            BasicPlanetProperties planetProperty = DataPrinterHelper.ConvertUserChoiceToPlanetProperty(userChoice);

            var (planetNameMax, maxValue) = DataPrinterHelper.FindPlanetNameAndMaxOrMinValueByProperty(Planets, planetProperty, FilteringCriteria.Max);
            var (planetNameMin, minValue) = DataPrinterHelper.FindPlanetNameAndMaxOrMinValueByProperty(Planets, planetProperty, FilteringCriteria.Min);

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
 
}