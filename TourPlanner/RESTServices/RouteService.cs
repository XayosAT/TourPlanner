using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Linq;
using log4net;

namespace TourPlanner.RESTServices;

public class RouteService
{
    private static readonly ILog log = LogManager.GetLogger(typeof(RouteService));
    private static readonly string _baseUrl = "https://api.openrouteservice.org/v2/directions/driving-car";
    private static string _apiKey;

    public void Configure(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<string> GetDirAsync(string from, string to)
    {
        using (HttpClient client = new HttpClient())
        {
            var fromCoords = await GetCoordinatesAsync(from);
            var toCoords = await GetCoordinatesAsync(to);
            var requestUrl =
                $"{_baseUrl}?api_key={_apiKey}&start={fromCoords.Item2},{fromCoords.Item1}&end={toCoords.Item2},{toCoords.Item1}";
            HttpResponseMessage response = await client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                log.Error("Error retrieving route information");
                throw new Exception("Error retrieving route information");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            log.Info("Route information retrieved successfully");
            return jsonResponse;
        }
    }
    
    private async Task<(string,string)> GetCoordinatesAsync(string address)
    {
        using (HttpClient client = new HttpClient())
        {
            var requestUrl = $"https://api.openrouteservice.org/geocode/search?api_key={_apiKey}&text={address}";
            HttpResponseMessage response = await client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                log.Error("Error retrieving coordinates");
                throw new Exception("Error retrieving coordinates");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(jsonResponse);
            var features = json["features"];
            var coordinates = features[0]["geometry"]["coordinates"];

            double lng = coordinates[0].Value<double>();
            double lat = coordinates[1].Value<double>();
            
            string formattedLat = lat.ToString(CultureInfo.InvariantCulture);
            string formattedLng = lng.ToString(CultureInfo.InvariantCulture);

            
            log.Info("Coordinates retrieved successfully");
            return (formattedLat, formattedLng);
            
        }
    }
}