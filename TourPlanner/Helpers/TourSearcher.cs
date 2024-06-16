using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using log4net;
using TourPlanner.Models;

namespace TourPlanner.Helpers;

public class TourSearcher
{
    private static readonly ILog log = LogManager.GetLogger(typeof(TourSearcher));

    public ObservableCollection<Tour> Search(string searchTerm, ObservableCollection<Tour> tours)
    {
        try
        {
            log.Info("Searching tours with search term: " + searchTerm);
            
            var results = tours
                .Where(t => (t.Name?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (t.Description?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (t.StartLocation?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (t.EndLocation?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (t.Description?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (t.Distance.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (t.Logs?.Any(log => (log.Comment?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                                (log.TotalDistance.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                                (log.TotalTime.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                                (log.Rating.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                                (log.Date?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false)) ?? false))
                .ToList();

            log.Info("Search completed successfully. Found " + results.Count + " results");
            return new ObservableCollection<Tour>(results);
        }
        catch (Exception ex)
        {
            log.Error("Failed to search tours");
            return null;
        }
    }
}
