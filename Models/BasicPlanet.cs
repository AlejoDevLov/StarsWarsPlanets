
namespace StarsWarsPlanets.Models;

internal record BasicPlanet
{
    public string Name { get; }
     public string Diameter { get; }
    public string SurfaceWater { get; }
    public string Population { get; }
    
    public BasicPlanet(string name, string diameter, string surfaceWater, string population)
    {
        Name = name;
        Diameter = diameter;
        SurfaceWater = surfaceWater;
        Population = population;
    }

    public static explicit operator BasicPlanet(Planet planet)
    {
        return new BasicPlanet(planet.Name, planet.Diameter, planet.SurfaceWater, planet.Population);
    }
}
