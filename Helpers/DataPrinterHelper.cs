using StarsWarsPlanets.Models;

namespace StarsWarsPlanets.Helpers;

internal class DataPrinterHelper
{
    public static BasicPlanet FindPlanetNameAndMinValueByProperty(IEnumerable<BasicPlanet> planets, Func<BasicPlanet, string> planetProperty)
    {
        var planet = planets
                .Where(p => long.TryParse(planetProperty(p), out _))
                .MinBy(planetProperty);

        return planet ??= new BasicPlanet("Fake planet", "0", "0", "0");
    }

    public static BasicPlanet FindPlanetNameAndMaxValueByProperty(IEnumerable<BasicPlanet> planets, Func<BasicPlanet, string> planetProperty)
    {
        var planet = planets
                .Where(p => long.TryParse(planetProperty(p), out _))
                .MaxBy(planetProperty);

        return planet ??= new BasicPlanet("Fake planet", "0", "0", "0");
    }
}
