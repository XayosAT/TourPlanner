using System;
using Newtonsoft.Json.Linq;

namespace TourPlanner.Helpers;

public static class RouteDecoder
{
    // make static method that returns a tuple of 2 doubles
    public static (double, double) GetDistanceAndDuration(string jsonResponse)
    {
        // parse the JSON response
        var json = JObject.Parse(jsonResponse);
        // get the distance and duration values
        var distance = json["features"][0]["properties"]["segments"][0]["distance"];
        var duration = json["features"][0]["properties"]["segments"][0]["duration"];
        // return the values as a tuple
        return (distance.Value<float>(), duration.Value<float>());
    }
}
    