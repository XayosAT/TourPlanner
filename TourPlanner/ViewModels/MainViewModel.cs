using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.Models;

namespace TourPlanner.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    
    private Tour _selectedTour;
    private Tour _newTour = new Tour();
    private TourLogs _newLog = new TourLogs();
    public ObservableCollection<Tour> Tours { get; set; }
    public ICommand ShowAddTourFormCommand { get; set; }
    public ICommand ShowAddLogFormCommand { get; set; }
    public ICommand AddTourCommand { get; set; }
    public ICommand CancelAddTourCommand { get; set; }
    public ICommand CancelAddLogCommand { get; set; }
    public ICommand DeleteTourCommand { get; set; }
    public ICommand AddLogCommand { get; set; }
    

    public MainViewModel()
    {
        Tours = new ObservableCollection<Tour>();
        // Populate Tours collection here
        LoadTours();
        SelectedTour = Tours[0];
        ShowAddTourFormCommand = new RelayCommand(ShowAddTourFormAction);
        ShowAddLogFormCommand = new RelayCommand(ShowAddLogFormAction);
        AddTourCommand = new RelayCommand(AddTourAction);
        CancelAddTourCommand = new RelayCommand(CancelAddTourAction);
        CancelAddLogCommand = new RelayCommand(CancelAddLogAction);
        DeleteTourCommand = new RelayCommand(DeleteTourAction);
        AddLogCommand = new RelayCommand(AddLogAction);
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
    private void ShowAddTourFormAction()
    {
        ShowAddTourForm = true;
    }
    
    private void ShowAddLogFormAction()
    {
        ShowAddLogForm = true;
    }

    private void AddTourAction()
    {
        
        Tours.Add(NewTour);
        NewTour.Clear();
        // Reset form visibility
        ShowAddTourForm = false;
    }

    private void CancelAddTourAction()
    {
        // Reset form visibility
        ShowAddTourForm = false;
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
    
    private void DeleteTourAction()
    {
        Tours.Remove(SelectedTour);
    }
    
    private void AddLogAction()
    {
        //print log to console
        Console.WriteLine("Date: " + NewLog.Date);
        Console.WriteLine("TotalTime: " + NewLog.TotalTime);
        Console.WriteLine("Distance: " +  NewLog.TotalDistance);
        
        SelectedTour.Logs.Add(NewLog);
        NewLog.Clear();
        // Reset form visibility
        ShowAddLogForm = false;
    }
    
    private void CancelAddLogAction()
    {
        // Reset form visibility
        ShowAddLogForm = false;
    }
    
    public TourLogs NewLog
    {
        get => _newLog;
        set
        {
            _newLog = value;
            OnPropertyChanged();
        }
    }
    
    public Tour NewTour
    {
        get => _newTour;
        set
        {
            _newTour = value;
            OnPropertyChanged();
        }
    }
    
    
    private bool _showAddTourForm;
    public bool ShowAddTourForm
    {
        get { return _showAddTourForm; }
        set
        {
            if (_showAddTourForm != value)
            {
                _showAddTourForm = value;
                OnPropertyChanged(nameof(ShowAddTourForm));
            }
        }
    }
    
    private bool _showAddLogForm;
    
    public bool ShowAddLogForm
    {
        get { return _showAddLogForm; }
        set
        {
            if (_showAddLogForm != value)
            {
                _showAddLogForm = value;
                OnPropertyChanged(nameof(ShowAddLogForm));
            }
        }
    }
    
    

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}