using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

class JsonToXmlConverter
{
<<<<<<< HEAD
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
=======
    public static void Main()
    {
        string dataDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data");

        if (!Directory.Exists(dataDirectory))
>>>>>>> d66e093a1785acd2f3da32dd9dfbdcd193235002
        {
            Console.WriteLine("The 'data' directory does not exist.");
            return;
        }

<<<<<<< HEAD
        string[] jsonFiles = Directory.GetFiles(sourceDataDirectory, "*.json");
=======
        string[] jsonFiles = Directory.GetFiles(dataDirectory, "*.json");
>>>>>>> d66e093a1785acd2f3da32dd9dfbdcd193235002

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
<<<<<<< HEAD
        string input = Console.ReadLine()?.Trim()!;
=======
        string input = Console.ReadLine()?.Trim();
>>>>>>> d66e093a1785acd2f3da32dd9dfbdcd193235002

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input, please try again with an existing file.");
            return;
        }

<<<<<<< HEAD
        string fullPath = Path.Combine(sourceDataDirectory, input);
=======
        string fullPath = Path.Combine(dataDirectory, input);
>>>>>>> d66e093a1785acd2f3da32dd9dfbdcd193235002

        if (!File.Exists(fullPath))
        {
            Console.WriteLine("File not found, program terminated.");
            return;
        }

        try
        {
            string jsonContent = File.ReadAllText(fullPath);
<<<<<<< HEAD
            XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(jsonContent, "Root")!;

            string xmlFileName = Path.Combine(targetDataDirectory, Path.ChangeExtension(input, ".xml"));
            xmlDoc!.Save(xmlFileName);
=======
            XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(jsonContent, "Root");

            string xmlFileName = Path.Combine(dataDirectory, Path.ChangeExtension(input, ".xml"));
            xmlDoc.Save(xmlFileName);
>>>>>>> d66e093a1785acd2f3da32dd9dfbdcd193235002

            Console.WriteLine("File successfully converted!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during conversion: " + ex.Message);
        }
    }
}
