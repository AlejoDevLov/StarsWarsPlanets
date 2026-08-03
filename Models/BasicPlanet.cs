using System.Text.Json.Serialization;

namespace StarsWarsPlanets.Models;

internal record BasicPlanet
(
     string Name,
     string Diameter,
     string SurfaceWater,
     string Population
);
