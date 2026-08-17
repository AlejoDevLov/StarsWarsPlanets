using StarsWarsPlanets.Helpers;
using StarsWarsPlanets.Models;
using System.Reflection;

namespace StarsWarsPlanets.ConsoleUI;


// This class represents the object's data in a chart in the console. 
internal class DataPrinter<T>
{
    //This field defines the width of the cell that represents each value in the console
    private const int _columnWidth = 20;
    private readonly IEnumerable<T> Items;

    internal DataPrinter(IEnumerable<T> items)
    {
        Items = items;
    }

    public void PrintPlanetsUsingReflection()
    {
        if (Items.Any())
        {
            PropertyInfo[] properties = Items.ElementAt(0)!.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            // The code in this loop defines the headers of the chart
            foreach (PropertyInfo property in properties)
            {
                Console.Write($"{property.Name}" + " ".PadLeft(_columnWidth - property.Name.Length) + "|");
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', _columnWidth * properties.Length + properties.Length));

            // The code in these loops defines the values for the chart
            PropertyInfo[] props = Items.ElementAt(0)!.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var obj in Items)
            {
                foreach (PropertyInfo property in props)
                {
                    var item = property.GetValue(obj);
                    if (item is null)
                    {
                        Console.WriteLine("Null" + " ".PadLeft(_columnWidth - 4));
                    }
                    else if (item.ToString()!.Length >= _columnWidth)
                    {
                        Console.WriteLine(item.ToString()!.Substring(0, _columnWidth - 3).Concat("  |"));
                    }
                    else
                    {
                        Console.Write($"{item + " ".PadLeft(_columnWidth - item!.ToString()!.Length) + "|"}");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    public void PrintStatisticsForThePropertySelectedByUser(string userChoice, Func<BasicPlanet, string> planetProperty)
    {
        try
        {
            var planetMax = DataPrinterHelper<BasicPlanet>.FindPlanetNameAndMaxValueByProperty((IEnumerable<BasicPlanet>)Items, planetProperty);
            var planetMin = DataPrinterHelper<BasicPlanet>.FindPlanetNameAndMinValueByProperty((IEnumerable<BasicPlanet>)Items, planetProperty);

            Console.WriteLine($"The max {userChoice} is {planetProperty(planetMax)} (Planet: {planetMax.Name})");
            Console.WriteLine($"The min {userChoice} is {planetProperty(planetMin)} (Planet: {planetMin.Name})");
            Console.WriteLine("Press any key to close");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Invalid choice.");
            Console.WriteLine("Press any key to close");
        }
    }
 
}