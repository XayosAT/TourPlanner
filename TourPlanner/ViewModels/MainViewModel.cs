using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TourPlanner.DAL;
using TourPlanner.Models;
using TourPlanner.Enums;
using TourPlanner.RESTServices;
using TourPlanner.MapServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using TourPlanner.Helpers;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context = new AppDbContext();
        private Tour _selectedTour;
        private TourLogs _selectedLog;
        private Tour _newTour = new Tour();
        private TourLogs _newLog = new TourLogs();
        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours
        {
            get => _tours;
            set
            {
                _tours = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand ShowAddTourFormCommand { get; set; }
        public ICommand ShowAddLogFormCommand { get; set; }
        public ICommand AddTourCommand { get; set; }
        public ICommand CancelAddTourCommand { get; set; }
        public ICommand CancelAddLogCommand { get; set; }
        public ICommand DeleteTourCommand { get; set; }
        public ICommand AddLogCommand { get; set; }
        public ICommand DeleteLogCommand { get; set; }

        public MainViewModel()
        {
            Tours = new ObservableCollection<Tour>();
            ShowAddTourFormCommand = new RelayCommand(ShowAddTourFormAction);
            ShowAddLogFormCommand = new RelayCommand(ShowAddLogFormAction);
            AddTourCommand = new RelayCommand(AddTourAction);
            CancelAddTourCommand = new RelayCommand(CancelAddTourAction);
            CancelAddLogCommand = new RelayCommand(CancelAddLogAction);
            DeleteTourCommand = new RelayCommand(DeleteTourAction);
            AddLogCommand = new RelayCommand(AddLogAction);
            DeleteLogCommand = new RelayCommand(DeleteLogAction);
            LoadTours();
            NewTour.ImagePath = "default.jpg";
        }

        private async void LoadTours()
        {
            // test openrouteservice
            var routeService = new RouteService();
            // var route = await routeService.GetRouteAsync("8.681495,49.41461", "8.687872,49.420318");
            var route = await routeService.GetDirAsync("16.407756633547216, 48.24348388094564", "16.409850252854763, 48.242941593703485");
            // Console.WriteLine($"Distance: {route.distance}m, Duration: {route.duration}s, Polyline: {route.polyline}");
            
            
            // File path for route.js
            string pathToRouteJS = "C:\\Users\\Philipp Wudernitz\\OneDrive\\Desktop\\SWEN_Tour_Planner\\TourPlanner\\TourPlanner\\Resources\\route.js";
            
            // add 'var directions = ' to the JSON string
            var directionsVar = "var directions = " + route;
            
            // Write JSON string to the file
            try
            {
                File.WriteAllText(pathToRouteJS, directionsVar);
                Console.WriteLine("JSON string has been saved to the .js file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            
            // Path to the HTML file
            var filePath = "C:\\Users\\Philipp Wudernitz\\OneDrive\\Desktop\\SWEN_Tour_Planner\\TourPlanner\\TourPlanner\\Resources\\map.html";
        
            // Open the HTML file in the default browser
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            
            Console.WriteLine("Loading tours");
            var tours = await _context.Tours.Include(t => t.Logs).ToListAsync();
            Tours = new ObservableCollection<Tour>(tours);
            
        }

        private void ShowAddTourFormAction()
        {
            ShowAddTourForm = true;
        }

        private void ShowAddLogFormAction()
        {
            ShowAddLogForm = true;
        }

        private async void AddTourAction()
        {
            if (!NewTour.HasValidInput()) { return; }

            await _context.Tours.AddAsync(NewTour);
            await _context.SaveChangesAsync();
            LoadTours();
            NewTour = new Tour();
            NewTour.ImagePath = "default.jpg";
            ShowAddTourForm = false;
        }

        private void CancelAddTourAction()
        {
            NewTour = new Tour();
            ShowAddTourForm = false;
        }

        private async void DeleteTourAction()
        {
            if (SelectedTour != null)
            {
                _context.Tours.Remove(SelectedTour);
                await _context.SaveChangesAsync();
                LoadTours();
            }
        }

        private async void AddLogAction()
        {
            if (SelectedTour != null)
            {
                NewLog.TourId = SelectedTour.Id;
                await _context.Logs.AddAsync(NewLog);
                await _context.SaveChangesAsync();
                LoadTours();
                NewLog = new TourLogs();
                ShowAddLogForm = false;
            }
        }

        private void CancelAddLogAction()
        {
            NewLog = new TourLogs();
            ShowAddLogForm = false;
        }

        private async void DeleteLogAction()
        {
            if (SelectedTour != null && SelectedLog != null)
            {
                _context.Logs.Remove(SelectedLog);
                await _context.SaveChangesAsync();
                LoadTours();
            }
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

        public TourLogs SelectedLog
        {
            get => _selectedLog;
            set
            {
                _selectedLog = value;
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

        public TourLogs NewLog
        {
            get => _newLog;
            set
            {
                _newLog = value;
                OnPropertyChanged();
            }
        }

        private bool _showAddTourForm;
        public bool ShowAddTourForm
        {
            get => _showAddTourForm;
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
            get => _showAddLogForm;
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
}
