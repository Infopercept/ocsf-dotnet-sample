// See https://aka.ms/new-console-template for more information
using OcsfDemo.Schema;

Console.WriteLine("Hello, World!");

var obj = new OcsfRoot
{
    ActivityId = 3001,
    ActivityName = "Test Activity",
};

Console.WriteLine(System.Text.Json.JsonSerializer.Serialize<OcsfRoot>(obj, new System.Text.Json.JsonSerializerOptions
{
    WriteIndented = true

}));


Console.ReadLine();