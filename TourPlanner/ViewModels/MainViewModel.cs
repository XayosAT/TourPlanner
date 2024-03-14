using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.Models;

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
        SelectedTour = Tours[0];
    }

    private void LoadTours()
    {
        Tours.Add(new Tour
        {
            Name = "Wienerwald",
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
            Name = "Donauinsel",
            Description = "Tour 2 Description",
            StartLocation = "Start 2",
            EndLocation = "End 2",
            Type = Tour.TransportType.Bicycle,
            Distance = 200,
            Time = 120,
            ImagePath = "Path to Image 2"
        });
        Tours[0].AddDemoLogs();
        Tours[1].AddDemoLogs();
          
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
    
    private bool _addTourVisibility;
    
    public bool AddTourVisibility
    {
        get => _addTourVisibility;
        set
        {
            if (_addTourVisibility != value)
            {
                _addTourVisibility = value;
                OnPropertyChanged();
            }
        }
    }   
    
    
    

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}