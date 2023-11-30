using System;
using System.Linq;
using System.Reflection;

public class Source
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Occupation { get; set; }
}

public class Destination
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public string Profession { get; set; }
}

public static class DynamicPropertyMapper
{
    public static void MapProperties<TSource, TDestination>(TSource source, TDestination destination)
    {
        var sourceProperties = typeof(TSource).GetProperties();
        var destinationProperties = typeof(TDestination).GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == sourceProperty.Name);

            if (destinationProperty != null)
            {
                destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var source = new Source
        {
            Name = "Krishna",
            Age = 30,
            Occupation = "Software Engineer"
        };

        var destination = new Destination();
        DynamicPropertyMapper.MapProperties(source, destination);
        Console.WriteLine("Destination:");
        Console.WriteLine($"FullName: {destination.FullName}");
        Console.WriteLine($"Age: {destination.Age}");
        Console.WriteLine($"Profession: {destination.Profession}");
    }
}
