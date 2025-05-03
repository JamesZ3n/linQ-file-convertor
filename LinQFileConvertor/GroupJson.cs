using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

class GroupJson
{
    public static JObject? PerformGroup(string jsonContent)
    {
        try
        {
            JObject jsonObject = JObject.Parse(jsonContent);

            Console.WriteLine("Do you want to perform a specific grouping? (yes/no)");
            string groupResponse = Console.ReadLine()?.Trim().ToLower()!;
            if (groupResponse != "yes")
            {
                return jsonObject;
            }

            Console.WriteLine("Available fields to group by:");
            var sampleHouse = jsonObject.Properties().FirstOrDefault()?.Value as JObject;
            if (sampleHouse != null)
            {
                foreach (var property in sampleHouse.Properties())
                {
                    Console.WriteLine("- " + property.Name);
                }
            }
            else
            {
                Console.WriteLine("No valid fields found for grouping.");
            }

            Console.WriteLine("\nEnter a field to group by:");
            string groupField = Console.ReadLine()?.Trim()!;
            if (string.IsNullOrWhiteSpace(groupField))
            {
                Console.WriteLine("Invalid field name. Returning the original JSON.");
                return jsonObject;
            }
            if (!jsonObject.Properties().Any(p => (p.Value as JObject)?.Properties().Any(prop => prop.Name.Equals(groupField, StringComparison.OrdinalIgnoreCase)) == true))            {
                Console.WriteLine($"Field '{groupField}' not found in the JSON. Returning the original JSON.");
                return jsonObject;
            }
            Console.WriteLine($"Grouping by '{groupField}'...");

            // Group houses by the specified field
           var groupedData = jsonObject.Properties()
            .GroupBy(
                p => ((JObject)p.Value)[groupField]?.ToString() ?? "Unknown", // Group by the field value
                p => new JProperty(p.Name, p.Value) // Preserve the house key and its object
            )
            .ToDictionary(
                g => g.Key, // Group key (e.g., "1", "2")
                g => new JObject(g) // Create a JObject for each group
            );

            // Create the final grouped JSON object
            return new JObject
            {
                [groupField] = JObject.FromObject(groupedData)
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during grouping: " + ex.Message);
            return null;
        }
    }
}