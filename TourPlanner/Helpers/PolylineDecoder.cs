using System;
using System.Collections.Generic;

namespace TourPlanner.Helpers;

public class PolylineDecoder
{
    public static List<(double lat, double lng)> DecodePolyline(string polyline)
    {
        if (string.IsNullOrEmpty(polyline))
            throw new ArgumentException("Invalid polyline");

        var polylineChars = polyline.ToCharArray();
        var polylineCoords = new List<(double lat, double lng)>();
        int index = 0;
        int currentLat = 0;
        int currentLng = 0;

        while (index < polylineChars.Length)
        {
            // Calculate next latitude
            int sum = 0;
            int shifter = 0;
            int nextFiveBits;
            do
            {
                nextFiveBits = polylineChars[index++] - 63;
                sum |= (nextFiveBits & 31) << shifter;
                shifter += 5;
            } while (nextFiveBits >= 32 && index < polylineChars.Length);

            if (index >= polylineChars.Length) break;

            currentLat += (sum & 1) != 0 ? ~(sum >> 1) : (sum >> 1);

            // Calculate next longitude
            sum = 0;
            shifter = 0;
            do
            {
                nextFiveBits = polylineChars[index++] - 63;
                sum |= (nextFiveBits & 31) << shifter;
                shifter += 5;
            } while (nextFiveBits >= 32 && index < polylineChars.Length);

            if (index >= polylineChars.Length && nextFiveBits >= 32) break;

            currentLng += (sum & 1) != 0 ? ~(sum >> 1) : (sum >> 1);

            polylineCoords.Add((currentLat / 1E5, currentLng / 1E5));
        }

        return polylineCoords;
    }

    public static string EncodeCoordinates(List<(double lat, double lng)> coordinates)
    {
        var encodedCoords = new List<string>();
        foreach (var point in coordinates)
        {
            encodedCoords.Add($"{point.lng},{point.lat}");
        }

        return string.Join(";", encodedCoords);
    }
}