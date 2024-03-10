using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using TourPlanner.Model;

namespace TourPlanner;

public class MainViewModel : INotifyPropertyChanged
{
    
    private Tour _selectedTour;
    public ObservableCollection<Tour> Tours { get; set; }

    public MainViewModel()
    {
        Tours = new ObservableCollection<Tour>();
        // Populate Tours collection here
        LoadTours();
    }

    private void LoadTours()
    {
        Tours.Add(new Tour
        {
            Name = "Tour 1",
            Description = "Tour 1 Description",
            StartLocation = "Start 1",
            EndLocation = "End 1",
            Type = Tour.TransportType.Car,
            Distance = 100,
            Time = 60,
            ImagePath = "Path to Image 1"
        });
        Tours.Add(new Tour
        {
            Name = "Tour 2",
            Description = "Tour 2 Description",
            StartLocation = "Start 2",
            EndLocation = "End 2",
            Type = Tour.TransportType.Bicycle,
            Distance = 200,
            Time = 120,
            ImagePath = "Path to Image 2"
        });
    }
    
    public Tour SelectedTour
    {
        get => _selectedTour;
        set
        {
            _selectedTour = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}