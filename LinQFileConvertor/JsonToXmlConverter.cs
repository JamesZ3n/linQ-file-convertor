using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class JsonToXmlConverter
{
    public static void Convertor()
    {
        Console.WriteLine("Welcome to the JSON to XML converter!");
        string sourceDataDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, "sourceData");
        string targetDataDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, "targetData");

        if (!Directory.Exists(targetDataDirectory))
        {
            Directory.CreateDirectory(targetDataDirectory);
        }

        if (!Directory.Exists(sourceDataDirectory))
        {
            Console.WriteLine(sourceDataDirectory + "The 'data' directory does not exist.");
            return;
        }

        string[] jsonFiles = Directory.GetFiles(sourceDataDirectory, "*.json");

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
        string input = Console.ReadLine()?.Trim()!;

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input, please try again with an existing file.");
            return;
        }

        string fullPath = Path.Combine(sourceDataDirectory, input);

        if (!File.Exists(fullPath))
        {
            Console.WriteLine("File not found, program terminated.");
            return;
        }

        try
        {
            string jsonContent = File.ReadAllText(fullPath);

            // Étape intermédiaire : recherche spécifique
            JObject? searchData = JsonSearch.PerformSearch(jsonContent)!;

            // Étape intermédiaire : filtre spécifique
            JObject? filteredData = FilterJsonData.Filter(searchData.ToString());

            // Étape intermédiaire : groupe spécifique
            JObject? groupedData = GroupJson.PerformGroup(filteredData.ToString());

            //Afficher les résultats de la recherche
            if (groupedData != null && groupedData.HasValues)
            {
                Console.WriteLine(groupedData.ToString());
            }
            else
            {
                Console.WriteLine("No data found for json.");
                return;
            }

            
            XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(groupedData.ToString(), "Root")!;

            string xmlFileName = Path.Combine(targetDataDirectory, Path.ChangeExtension(input, ".xml"));
            xmlDoc!.Save(xmlFileName);

            Console.WriteLine("File successfully converted!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during conversion: " + ex.Message);
        }
    }
}
