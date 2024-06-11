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
    
    public async Task<string> GetDirAsync(string from, string to)
    {
        using (HttpClient client = new HttpClient())
        {
            var requestUrl = $"{baseUrl}?api_key={apiKey}&start={from}&end={to}";
            HttpResponseMessage response = await client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error retrieving route information");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            return jsonResponse;
        }
    }
}