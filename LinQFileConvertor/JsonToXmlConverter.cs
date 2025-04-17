using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

class JsonToXmlConverter
{
    public static void Main()
    {
        string dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data");

        if (!Directory.Exists(dataDirectory))
        {
            Console.WriteLine("The 'data' directory does not exist.");
            return;
        }

        string[] jsonFiles = Directory.GetFiles(dataDirectory, "*.json");

        if (jsonFiles.Length == 0)
        {
            Console.WriteLine("No JSON files found in the 'data' directory.");
            return;
        }

        Console.WriteLine("Here are all the JSON files you can convert to XML:");
        foreach (var file in jsonFiles)
        {
            Console.WriteLine("- " + Path.GetFileName(file));
        }

        Console.WriteLine("\nEnter the name of the JSON file to convert:");
        string input = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input, please try again with an existing file.");
            return;
        }

        string fullPath = Path.Combine(dataDirectory, input);

        if (!File.Exists(fullPath))
        {
            Console.WriteLine("File not found, program terminated.");
            return;
        }

        try
        {
            string jsonContent = File.ReadAllText(fullPath);
            XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(jsonContent, "Root");

            string xmlFileName = Path.Combine(dataDirectory, Path.ChangeExtension(input, ".xml"));
            xmlDoc.Save(xmlFileName);

            Console.WriteLine("File successfully converted!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during conversion: " + ex.Message);
        }
    }
}
