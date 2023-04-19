using Microsoft.Extensions.Configuration;

var WriteKeys = (IConfiguration config) =>
{
    foreach (var kvp in config.AsEnumerable().Reverse())
    {
        if (!string.IsNullOrWhiteSpace(kvp.Value))
        {
            Console.WriteLine($"{kvp.Key} = {kvp.Value}");
        }
    }
};

Console.WriteLine("Building configuration from appsettings.json");
var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json");

WriteKeys(builder.Build());

Console.WriteLine("\nBuilding configuration from appsettings.json and appsettings.public.json");
builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.public.json");

WriteKeys(builder.Build());


Console.WriteLine("\nBuilding configuration from appsettings.json, appsettings.public.json, and appsettings.cosmos.json");
builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.public.json")
    .AddJsonFile("appsettings.cosmos.json");

WriteKeys(builder.Build());

