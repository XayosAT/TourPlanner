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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Extensions.Configuration;
using Microsoft.Web.WebView2.Wpf;
using TourPlanner.Helpers;
using TourPlanner.Views;
using log4net;
using Microsoft.Web.WebView2.Core;
using Microsoft.Win32;
using TourPlanner.Helpers;


namespace TourPlanner.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainViewModel));
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
        public ICommand GenerateReportCommand { get; set; }
        public ICommand GenerateAverageReportCommand { get; set; }
        public ICommand ExportTourCommand { get; set; }
        public ICommand ImportTourCommand { get; set; }
        
        
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
            log.Debug("Initializing MainViewModel");
            
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
            GenerateReportCommand = new RelayCommand(async () => await CreateReportAsync());
            GenerateAverageReportCommand = new RelayCommand(async () => await CreateAverageReportAsync());
            ExportTourCommand = new RelayCommand(async () => await ExportTourAsync());
            ImportTourCommand = new RelayCommand(async () => await ImportTourAsync());
            LoadTours();
            

        }

        private async Task ExportTourAsync()
        {
            if (SelectedTour != null)
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fileName = SelectedTour.Name + ".json";
                string filePath = Path.Combine(documentsPath, fileName);
                await TourExporter.ExportTourAsync(SelectedTour, filePath);
                log.Info("Tour exported successfully at " + filePath);
            }
        }
        
        private async Task ImportTourAsync()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON file (*.json)|*.json",
                InitialDirectory = documentsPath
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                Tour importedTour = await TourImporter.ImportTourAsync(filePath);
                if (importedTour != null)
                {
                    // Check if a tour with the same ID already exists
                    if (!Tours.Any(t => t.Id == importedTour.Id))
                    {
                        await _context.Tours.AddAsync(importedTour);
                        await _context.SaveChangesAsync();
                        LoadTours();
                        log.Info("Tour imported successfully from " + filePath);
                    }
                    else
                    {
                        log.Warn("A tour with the same ID already exists and will not be imported.");
                    }
                }
            }
        }


        public async Task CreateAverageReportAsync()
        {

            // Generate the PDF report
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = "AverageTourReport.pdf";
            string filePath = Path.Combine(documentsPath, fileName);

            await ReportGenerator.GenerateAveragePDFReportAsync(SelectedTour, filePath);
            log.Info("Average PDF report generated successfully at " + filePath);
            // Console.WriteLine($"PDF report generated at: {filePath}");
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
            
            log.Info("Configuration loaded");
        }
        
        private void ConfigureContext()
        {
            _context.Configure(_connectionString);
            log.Info("Configured context");
        }

        public void ConfigureRouteService()
        {
            _routeService.Configure(_apiKey);
            log.Info("Configured route service");
        }

        private async void LoadTours()
        {
            var tours = await _context.Tours.Include(t => t.Logs).ToListAsync();
            Tours = new ObservableCollection<Tour>(tours);
            log.Info("Loaded tours");
            if (Tours.Count > 0)
            {
                SelectedTour = Tours[0];
            }
        }
        
        public void LoadMap()
        {
            // add 'var directions = ' to the JSON string
            var directionsVar = "var directions = " + SelectedTour.ImagePath; 
            
            // Write JSON string to the file
            try
            {
                File.WriteAllText(_pathToRouteJs, directionsVar);
                Console.WriteLine("JSON string has been saved to the .js file successfully.");
                log.Info("JSON string has been saved to the .js file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                log.Error($"An error occurred: {ex.Message}");
            }
            
            try
            {
                _mapWebView.CoreWebView2.Navigate(_pathToMapHtml);
                log.Info("Navigating to map.html");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation error: {ex.Message}");
                log.Error($"Navigation error: {ex.Message}");
            }

        }

        private void ShowAddTourFormAction()
        {
            log.Info("Showing add tour form");
            ShowAddTourForm = true;
        }

        private void ShowAddLogFormAction()
        {
            log.Info("Showing add log form");
            ShowAddLogForm = true;
        }

        private async void AddTourAction()
        {
            if (!NewTour.HasValidInput()) { return; }
            var jsonResponse = await _routeService.GetDirAsync(NewTour.StartLocation, NewTour.EndLocation);
            var result = RouteDecoder.GetDistanceAndDuration(jsonResponse);
            // round to 2 decimal places
            NewTour.Distance = (float)Math.Round(result.Item1 / 1000, 2);
            NewTour.Time = (float)Math.Round(result.Item2 / 3600, 2);
            NewTour.ImagePath = jsonResponse;
            SelectedTour = NewTour;
            await _context.Tours.AddAsync(NewTour);
            await _context.SaveChangesAsync();
            LoadTours();
            NewTour = new Tour();
            NewTour.ImagePath = "";
            ShowAddTourForm = false;
            
            // added tour log
            log.Info($"Added new tour: {NewTour.Name}");
        }

        private void CancelAddTourAction()
        {
            NewTour = new Tour();
            ShowAddTourForm = false;
            log.Info("Cancelled adding new tour");
        }

        private async void DeleteTourAction()
        {
            if (SelectedTour != null)
            {
                _context.Tours.Remove(SelectedTour);
                await _context.SaveChangesAsync();
                log.Info($"Deleted tour: {SelectedTour.Name}");
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
                log.Info($"Added new log to tour: {SelectedTour.Name}");
                LoadTours();
                NewLog = new TourLogs();
                ShowAddLogForm = false;
            }
        }

        private void CancelAddLogAction()
        {
            NewLog = new TourLogs();
            ShowAddLogForm = false;
            log.Info("Cancelled adding new log");
        }

        private async void DeleteLogAction()
        {
            if (SelectedTour != null && SelectedLog != null)
            {
                _context.Logs.Remove(SelectedLog);
                await _context.SaveChangesAsync();
                log.Info($"Deleted log from tour: {SelectedTour.Name}");
                LoadTours();
            }
        }

        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                log.Info($"Selected tour: {value.Name}");
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
                log.Info($"Selected log: {value.Date}");
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
        
        public async Task CreateReportAsync()
        {
            // Capture the map image
            string mapImagePath = await CaptureMapAsync();

            // Generate the PDF report
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = "TourReport.pdf";
            string filePath = Path.Combine(documentsPath, fileName);

            await ReportGenerator.GeneratePDFReportAsync(SelectedTour, filePath, mapImagePath);
            log.Info("PDF report generated successfully at " + filePath);
            // Console.WriteLine($"PDF report generated at: {filePath}");
        }
        
        public async Task<string> CaptureMapAsync()
        {
            if (MapWebView?.CoreWebView2 == null)
            {
                log.Debug("MapWebView or CoreWebView2 is not initialized.");
                return null;
            }

            try
            {   
                Console.WriteLine("Capturing map...");
                string imagePath = Path.Combine(projectDirectory, "Resources", "mapCapture.png");
                // Console.WriteLine(imagePath);
                using (var fileStream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    await MapWebView.CoreWebView2.CapturePreviewAsync(CoreWebView2CapturePreviewImageFormat.Png, fileStream);
                    log.Info("Map captured successfully at " + imagePath);
                }

                return imagePath;
            }
            catch (Exception ex)
            {
                log.Debug($"Exception in CaptureMapAsync: {ex.Message}");
                return null;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
