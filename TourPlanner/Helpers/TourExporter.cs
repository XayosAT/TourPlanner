using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TourPlanner.Models;
using log4net;
using System;
using System.Text.Json.Serialization;

namespace TourPlanner.Helpers;

public class TourExporter
{
    private static readonly ILog log = LogManager.GetLogger(typeof(TourExporter));
    
    public static async Task ExportTourAsync(Tour tour, string filePath)
    {
        try
        {
            // Configure JSON serializer options to handle cycles
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // For pretty printing
                ReferenceHandler = ReferenceHandler.Preserve
            };

            // Serialize the Tour object to JSON
            string jsonString = JsonSerializer.Serialize(tour, options);

            // Write the JSON string to a file
            await File.WriteAllTextAsync(filePath, jsonString);
        }
        catch (IOException ex)
        {
            // Handle file I/O errors
            log.Error($"An error occurred while writing to the file: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            log.Error($"An error occurred: {ex.Message}");
            throw;
        }
    }
}