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
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Extensions.Configuration;
using Microsoft.Web.WebView2.Wpf;
using TourPlanner.Helpers;
using TourPlanner.Views;


namespace TourPlanner.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context = new AppDbContext();
        private RouteService _routeService = new RouteService();
        private Tour _selectedTour;
        private TourLogs _selectedLog;
        private Tour _newTour = new Tour();
        private TourLogs _newLog = new TourLogs();
        private ObservableCollection<Tour> _tours;
        private WebView2 _mapWebView;
        private Route _routeControl;
        public Route RouteControl
        {
            get => _routeControl;
            set
            {
                _routeControl = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Tour> Tours
        {
            get => _tours;
            set
            {
                _tours = value;
                OnPropertyChanged();
            }
        }
        
        public WebView2 MapWebView
        {
            get => _mapWebView;
            set
            {
                if (_mapWebView != value)
                {
                    _mapWebView = value;
                    OnPropertyChanged(nameof(MapWebView));
                }
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
        
        
        string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        private string _pathToRouteJs;
        private string _pathToMapHtml;
        private string _pathToConfigFile;

        private string _apiKey;
        private string _connectionString;

        public string PathToMapHtml
        {
            get => _pathToMapHtml;
            set
            {
                _pathToMapHtml = value;
                OnPropertyChanged();
            }
        }
        
        public MainViewModel()
        {
            _pathToRouteJs = Path.Combine(projectDirectory, "Resources", "route.js");
            _pathToMapHtml = Path.Combine(projectDirectory, "Resources", "map.html");
            _pathToConfigFile = Path.Combine(projectDirectory, "appsettings.json");
            NewTour.ImagePath = "";
            LoadConfig();
            ConfigureContext();
            ConfigureRouteService();
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
        }

        private void LoadConfig()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(_pathToConfigFile, optional: false, reloadOnChange: true)
                .Build();

            // Read the connection string
            _connectionString = configuration.GetConnectionString("DefaultConnection");

            // Read the API key
            _apiKey = configuration["ApiSettings:ApiKey"];
        }
        
        private void ConfigureContext()
        {
            _context.Configure(_connectionString);
        }

        public void ConfigureRouteService()
        {
            _routeService.Configure(_apiKey);
        }

        private async void LoadTours()
        {
            Console.WriteLine("Loading tours");
            var tours = await _context.Tours.Include(t => t.Logs).ToListAsync();
            Tours = new ObservableCollection<Tour>(tours);
            
        }
        
        public void LoadMap()
        {
            
            // add 'var directions = ' to the JSON string
            var directionsVar = "var directions = " + SelectedTour.ImagePath;
            Console.WriteLine(directionsVar);
            
            // Write JSON string to the file
            try
            {
                File.WriteAllText(_pathToRouteJs, directionsVar);
                Console.WriteLine("JSON string has been saved to the .js file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            
            try
            {
                _mapWebView.CoreWebView2.Navigate(_pathToMapHtml);
                Console.WriteLine("Navigating to HTML file: " + _pathToMapHtml);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation error: {ex.Message}");
            }

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
            var imgPath = await _routeService.GetDirAsync(NewTour.StartLocation, NewTour.EndLocation, _apiKey);
            NewTour.ImagePath = imgPath;
            SelectedTour = NewTour;
            //NewTour.ImagePath = imgPath;
            await _context.Tours.AddAsync(NewTour);
            await _context.SaveChangesAsync();
            LoadTours();
            NewTour = new Tour();
            NewTour.ImagePath = "";
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
                LoadMap();
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
