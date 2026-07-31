using System.Reflection;

namespace StarsWarsPlanets.UI;

internal class ReflectionObjectDataPrinter : IObjectDataPrinter
{
    private readonly int _cellLength = 20;

    public void PrintPlanets(IEnumerable<object> objs)
    {
        if (objs.Any())
        {
            PropertyInfo[] properties = objs.ElementAt(0).GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo property in properties)
            {
                Console.Write($"{property.Name}" + " ".PadLeft(_cellLength - property.Name.Length) + "|");
            }

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------------");

            foreach (object obj in objs)
            {
                PropertyInfo[] props = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo property in props)
                {
                    var item = property.GetValue(obj);
                    if (item is null)
                    {
                        Console.WriteLine("Null" + " ".PadLeft(_cellLength - 4));
                    }
                    else if(item!.ToString()!.Length >= _cellLength)
                    {
                        Console.WriteLine(item!.ToString()!.Substring(0, _cellLength - 3 ) + "  |");
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

}
