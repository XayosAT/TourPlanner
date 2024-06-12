using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Linq;

namespace TourPlanner.RESTServices;

public class RouteService
{
    private static readonly string apiKey = "5b3ce3597851110001cf624814f69a36c67c4473a3ae8cdd7b8e79e1";
    private static readonly string baseUrl = "https://api.openrouteservice.org/v2/directions/driving-car";
    
    public async Task<string> GetDirAsync(string from, string to)
    {
        using (HttpClient client = new HttpClient())
        {
            var fromCoords = await GetCoordinatesAsync(from);
            var toCoords = await GetCoordinatesAsync(to);
            var requestUrl =
                $"{baseUrl}?api_key={apiKey}&start={fromCoords.Item2},{fromCoords.Item1}&end={toCoords.Item2},{toCoords.Item1}";
            HttpResponseMessage response = await client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error retrieving route information");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            return jsonResponse;
        }
    }
    
    private async Task<(string,string)> GetCoordinatesAsync(string address)
    {
        using (HttpClient client = new HttpClient())
        {
            var requestUrl = $"https://api.openrouteservice.org/geocode/search?api_key={apiKey}&text={address}";
            HttpResponseMessage response = await client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error retrieving coordinates");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(jsonResponse);
            var features = json["features"];
            var coordinates = features[0]["geometry"]["coordinates"];

            double lng = coordinates[0].Value<double>();
            double lat = coordinates[1].Value<double>();
            
            string formattedLat = lat.ToString(CultureInfo.InvariantCulture);
            string formattedLng = lng.ToString(CultureInfo.InvariantCulture);

            

            return (formattedLat, formattedLng);
            
        }
    }
}