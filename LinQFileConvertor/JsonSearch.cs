using System;
using System.Linq;
using Newtonsoft.Json.Linq;

class JsonSearch
{
    public static JObject? PerformSearch(string jsonContent)
    {
        try
        {
            JObject jsonObject = JObject.Parse(jsonContent);

            Console.WriteLine("Do you want to perform a specific search? (yes/no)");
            string searchResponse = Console.ReadLine()?.Trim().ToLower()!;

            if (searchResponse == "yes")
            {
                Console.WriteLine("Available fields in the JSON file:");
                foreach (var property in jsonObject.Properties())
                {
                    Console.WriteLine("- " + property.Name);
                }

                Console.WriteLine("\nEnter a keyword to search for fields containing it:");
                string keyword = Console.ReadLine()?.Trim()!;

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    // Filter fields containing the keyword
                    var filteredFields = jsonObject.Properties()
                        .Where(p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        .ToDictionary(p => p.Name, p => p.Value);

                    if (filteredFields.Count > 0)
                    {
                        Console.WriteLine("Fields matching your search:");
                        foreach (var field in filteredFields.Keys)
                        {
                            Console.WriteLine("- " + field);
                        }

                        // Return only the filtered fields
                        return JObject.FromObject(filteredFields);
                    }
                    else
                    {
                        Console.WriteLine("No fields match your search. Returning the full JSON content.");
                    }
                }
                else
                {
                    Console.WriteLine("No keyword entered. Returning the full JSON content.");
                }
            }

            return jsonObject;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred during the search: " + ex.Message);
            return null;
        }
    }
}