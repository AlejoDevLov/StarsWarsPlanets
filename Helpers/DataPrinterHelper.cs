using StarsWarsPlanets.Enums;
using StarsWarsPlanets.Models;
using System.Reflection;

namespace StarsWarsPlanets.Helpers;

internal class DataPrinterHelper
{
    public static string FindValueForThePlanetProperty(BasicPlanetProperties property, BasicPlanet planet) =>
     property switch
     {
         BasicPlanetProperties.Diameter => planet.Diameter,
         BasicPlanetProperties.Surface => planet.SurfaceWater,
         BasicPlanetProperties.Population => planet.Population,
         _ => throw new InvalidFilterCriteriaException()
     };

    public static (string PlanetName, string PropertyValue) FindPlanetNameAndMaxOrMinValueByProperty(IEnumerable<BasicPlanet> planets, BasicPlanetProperties planetProperty, FilteringCriteria filter)
    {
        var filteredPlanets = planets
                .Select(p => (PlanetName: p.Name, PropertyValue: FindValueForThePlanetProperty(planetProperty, p)))
                .Where(p => long.TryParse(p.PropertyValue, out _));
        return filter == FilteringCriteria.Max ? filteredPlanets.MaxBy(p => p.PropertyValue) : filteredPlanets.MinBy(p => p.PropertyValue);
    }

    public static BasicPlanetProperties ConvertUserChoiceToPlanetProperty(string userChoice)
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
}
