using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace TourPlanner.RESTServices;

public class RouteService
{
    private static readonly string apiKey = "5b3ce3597851110001cf624814f69a36c67c4473a3ae8cdd7b8e79e1";
    private static readonly string baseUrl = "https://api.openrouteservice.org/v2/directions/driving-car";

    public async Task<(double distance, double duration, string polyline)> GetRouteAsync(string from, string to)
    {
        using (HttpClient client = new HttpClient())
        {
            var requestUrl = $"{baseUrl}?api_key={apiKey}&start={from}&end={to}";
            HttpResponseMessage response = await client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error retrieving route information");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(jsonResponse);

            Console.WriteLine("API Response: " + jsonResponse);

            // Extracting the relevant information
            var feature = data["features"]?.FirstOrDefault();
            if (feature == null)
                throw new Exception("No routes found");

            var properties = feature["properties"];
            if (properties == null)
                throw new Exception("properties data is missing");

            var summary = properties["summary"];
            if (summary == null)
                throw new Exception("Summary data is missing");

            double distance = summary["distance"]?.Value<double>() ?? 0;
            double duration = summary["duration"]?.Value<double>() ?? 0;

            var coordinates = feature["geometry"]?["coordinates"];
            if (coordinates == null)
                throw new Exception("Coordinates data is missing");

            string polyline = coordinates.ToString();

            return (distance, duration, polyline);
        }
    }
}