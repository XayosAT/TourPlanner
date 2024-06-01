using System;

namespace TourPlanner.MapServices;

public class MapService
{
    private static readonly string mapboxToken = "pk.eyJ1IjoieGF5b3MiLCJhIjoiY2x3dzUzcmk2MHhoMTJucjFxbno2NnlwbyJ9.NoMhAGmreezq-QYC4btHMg";

    public string GetStaticMapUrl(string polyline)
    {
        string encodedPolyline = Uri.EscapeDataString(polyline);
        string mapUrl = $"https://api.mapbox.com/styles/v1/mapbox/streets-v11/static/path-5+ff0000-0.5({encodedPolyline})/auto/600x400?access_token={mapboxToken}";
        
        return mapUrl;
    }
}