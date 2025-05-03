using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

class FilterJsonData
{
    public static JObject Filter(string jsonContent)
    {
        try
        {
            JObject jsonData = JObject.Parse(jsonContent);

            Console.WriteLine("Do you want to perform a specific filter? (yes/no)");
            string searchResponse = Console.ReadLine()?.Trim().ToLower()!;

            if (searchResponse == "yes")
            {
                // Convert the object to a dictionary of houses (key-value pairs)
                var houses = jsonData.Properties()
                    .ToDictionary(p => p.Name, p => (JObject)p.Value);

                if (!houses.Any())
                {
                    Console.WriteLine("No houses found in the JSON file.");
                    return new JObject(); // Return an empty JSON object
                }

                Console.WriteLine("\nAvailable fields in the JSON data:");
                var sampleItem = houses.First().Value;
                foreach (var property in sampleItem.Properties())
                {
                    Console.WriteLine("- " + property.Name);
                }

                Dictionary<string, JObject> filteredHouses = houses;

                while (true)
                {
                    Console.WriteLine("\nChoose a field to filter by (or press Enter to finish):");
                    string filterField = Console.ReadLine()?.Trim()!;
                    if (string.IsNullOrWhiteSpace(filterField))
                    {
                        break;
                    }

                    Console.WriteLine($"Enter a value for '{filterField}':");
                    string filterValue = Console.ReadLine()?.Trim()!;

                    filteredHouses = filteredHouses
                        .Where(h => h.Value[filterField]?.ToString().Equals(filterValue, StringComparison.OrdinalIgnoreCase) == true)
                        .ToDictionary(h => h.Key, h => h.Value);

                    if (!filteredHouses.Any())
                    {
                        Console.WriteLine("No houses match the filter criteria.");
                        return new JObject(); // Return an empty JSON object if no matches
                    }

                    Console.WriteLine($"\nFiltered houses count: {filteredHouses.Count}");
                }

                // Return the filtered houses as a JSON object
                return new JObject
                {
                    ["FilteredHouses"] = JObject.FromObject(filteredHouses)
                };
            }

            // If no specific filter is applied, return the original JSON
            return jsonData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return new JObject(); // Return an empty JSON object in case of an error
        }
    }
}