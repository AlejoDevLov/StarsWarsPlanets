using StarsWarsPlanets.Models;

namespace StarsWarsPlanets.Helpers;

internal class DataPrinterHelper<T>
{
    public static BasicPlanet FindPlanetNameAndMinValueByProperty(IEnumerable<T> items, Func<BasicPlanet, string> planetProperty)
    {
        if (items is IEnumerable<BasicPlanet> planets)
        {
            var planet = planets
                    .Where(p => long.TryParse(planetProperty(p), out _))
                    .MinBy(planetProperty);

            return planet ??= new BasicPlanet("Fake planet", "0", "0", "0");
        }
        throw new NotImplementedException($"The logic for this type ({items?.ElementAt(0)?.GetType()}) has not been implemented.");
    }

    public static BasicPlanet FindPlanetNameAndMaxValueByProperty(IEnumerable<T> items, Func<BasicPlanet, string> planetProperty)
    {
        if (items is IEnumerable<BasicPlanet> planets)
        {
            var planet = planets
                    .Where(p => long.TryParse(planetProperty(p), out _))
                    .MaxBy(planetProperty);

            return planet ??= new BasicPlanet("Fake planet", "0", "0", "0");
        }
        throw new NotImplementedException($"The logic for this type ({items?.ElementAt(0)?.GetType()}) has not been implemented.");

    }
}