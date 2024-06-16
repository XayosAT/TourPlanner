using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TourPlanner.Models;
using System;
using log4net;

namespace TourPlanner.Helpers;

public class TourImporter
{
    private static readonly ILog log = LogManager.GetLogger(typeof(TourImporter));
    public static async Task<Tour> ImportTourAsync(string filePath)
    {
        
        try
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            // Read the JSON string from the file
            string jsonString = await File.ReadAllTextAsync(filePath);

            // Deserialize the JSON string to a Tour object
            Tour tour = JsonSerializer.Deserialize<Tour>(jsonString, options);
            return tour;
        }
        catch (IOException ex)
        {
            log.Error($"An error occurred while reading from the file: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            log.Error($"An error occurred: {ex.Message}");
            return null;
        }
    }
}