using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

class FilterJsonData
{
    public static void Filter()
    {
        Console.WriteLine("Welcome to the JSON filtering program!");

        string sourceDataDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, "sourceData");

        if (!Directory.Exists(sourceDataDirectory))
        {
            Console.WriteLine("The 'sourceData' folder does not exist.");
            return;
        }

        string[] jsonFiles = Directory.GetFiles(sourceDataDirectory, "*.json");
        if (jsonFiles.Length == 0)
        {
            Console.WriteLine("No JSON files found in the 'sourceData' folder.");
            return;
        }

        Console.WriteLine("Available JSON files:");
        foreach (var file in jsonFiles)
        {
            Console.WriteLine("- " + Path.GetFileName(file));
        }

        Console.WriteLine("\nEnter the name of the JSON file to use for filtering:");
        string input = Console.ReadLine()?.Trim()!;
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid file name.");
            return;
        }

        string fullPath = Path.Combine(sourceDataDirectory, input);
        if (!File.Exists(fullPath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            string jsonContent = File.ReadAllText(fullPath);
            JObject jsonData = JObject.Parse(jsonContent);

            // Convert the object to a list of houses
            var houses = jsonData.Properties()
                .Select(p => (JObject)p.Value)
                .ToList();

            if (!houses.Any())
            {
                Console.WriteLine("No houses found in the JSON file.");
                return;
            }

            Console.WriteLine("\nAvailable fields in the JSON data:");
            var sampleItem = houses.First();
            foreach (var property in sampleItem.Properties())
            {
                Console.WriteLine("- " + property.Name);
            }

            List<JObject> filteredHouses = houses;

            while (true)
            {
                Console.WriteLine("\nChoose a field to filter by (or press Enter to finish):");
                string filterField = Console.ReadLine()?.Trim()!;
                if (string.IsNullOrWhiteSpace(filterField))
                {
                    break; // Exit the loop if the user presses Enter
                }

                if (!sampleItem.ContainsKey(filterField))
                {
                    Console.WriteLine("Invalid field name.");
                    continue;
                }

                // Check if the field is numeric or string
                var fieldValues = filteredHouses.Select(h => h[filterField]?.ToString()).Where(v => v != null).Distinct().ToList();
                bool isNumericField = fieldValues.All(value => decimal.TryParse(value, out _));

                if (isNumericField)
                {
                    Console.WriteLine($"The field '{filterField}' is numeric. Enter the operator for filtering (<, >, <=, >=, =):");
                    string filterOperator = Console.ReadLine()?.Trim()!;
                    if (!new[] { "<", ">", "<=", ">=", "=" }.Contains(filterOperator))
                    {
                        Console.WriteLine("Invalid operator.");
                        continue;
                    }

                    Console.WriteLine($"Enter the value to filter '{filterField}' by:");
                    string filterValue = Console.ReadLine()?.Trim()!;
                    if (!decimal.TryParse(filterValue, out var filterNumericValue))
                    {
                        Console.WriteLine("Invalid numeric value.");
                        continue;
                    }

                    // Filter numeric fields
                    filteredHouses = filteredHouses
                        .Where(h =>
                        {
                            var fieldValue = h[filterField]?.ToString();
                            if (fieldValue == null || !decimal.TryParse(fieldValue, out var numericValue)) return false;

                            return filterOperator switch
                            {
                                "<" => numericValue < filterNumericValue,
                                ">" => numericValue > filterNumericValue,
                                "<=" => numericValue <= filterNumericValue,
                                ">=" => numericValue >= filterNumericValue,
                                "=" => numericValue == filterNumericValue,
                                _ => false
                            };
                        })
                        .ToList();
                }
                else
                {
                    Console.WriteLine($"The field '{filterField}' is a string. Possible values are:");
                    foreach (var value in fieldValues)
                    {
                        Console.WriteLine("- " + value);
                    }

                    Console.WriteLine($"Enter the value to filter '{filterField}' by:");
                    string filterValue = Console.ReadLine()?.Trim()!;
                    if (string.IsNullOrWhiteSpace(filterValue))
                    {
                        Console.WriteLine("Invalid filter value.");
                        continue;
                    }

                    // Filter string fields using "contains"
                    filteredHouses = filteredHouses
                        .Where(h => h[filterField]?.ToString().IndexOf(filterValue, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToList();

                    if (!filteredHouses.Any())
                    {
                        Console.WriteLine("No items match the criteria.");
                        return;
                    }

                    Console.WriteLine("\nFiltered results so far:");
                    foreach (var house in filteredHouses)
                    {
                        // Find the house number (key) by matching the JObject
                        var houseNumber = jsonData.Properties()
                            .FirstOrDefault(p => JToken.DeepEquals(p.Value, house))?.Name;

                        Console.WriteLine($"House Number: {houseNumber}");
                        Console.WriteLine(house);
                    }
                }

                if (!filteredHouses.Any())
                {
                    Console.WriteLine("No items match the criteria.");
                    return;
                }

                Console.WriteLine("\nFiltered results so far:");
                foreach (var house in filteredHouses)
                {
                    // Find the house number (key) by matching the JObject
                    var houseNumber = jsonData.Properties()
                        .FirstOrDefault(p => JToken.DeepEquals(p.Value, house))?.Name;

                    Console.WriteLine($"House Number: {houseNumber}");
                    Console.WriteLine(house);
                }
            }

            Console.WriteLine("\nFinal filtered results:");
            foreach (var house in filteredHouses)
            {
                Console.WriteLine(house);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}