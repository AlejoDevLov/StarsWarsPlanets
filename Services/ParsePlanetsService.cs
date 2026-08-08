
using StarsWarsPlanets.Models;

namespace StarsWarsPlanets.Services;

internal class ParsePlanetsService
{
    public static List<BasicPlanet> GenerateBasicPlanets(IEnumerable<Planet> planets)
    {
        var basicPlanets = new List<BasicPlanet>();
        foreach (var planet in planets)
        {
            basicPlanets.Add(new BasicPlanet(planet.Name, planet.Diameter, planet.SurfaceWater, planet.Population));
        }
        return basicPlanets;
    }
}
