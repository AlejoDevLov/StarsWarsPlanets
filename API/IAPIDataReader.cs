
namespace StarsWarsPlanets.API;

internal interface IAPIDataReader
{
    Task<string> Read(string uri, string endpoint);
}
