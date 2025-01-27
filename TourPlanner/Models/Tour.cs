using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Windows.Media;
using TourPlanner.Enums;
using System.Text.RegularExpressions;
namespace TourPlanner.Models;

public class Tour : INotifyPropertyChanged
{
    private ObservableCollection<TourLogs> _logs = new ObservableCollection<TourLogs>();
    
    private string _name;
    private string _description;
    private string _startLocation;
    private string _endLocation;
    private TransportType _type;
    private float _distance;
    private float _time;
    private string _imagePath;
    
    public int Id { get; set; }
    
    public ObservableCollection<TourLogs> Logs
    {
        get => this._logs;
        set
        {
            this._logs = value;
            this.OnPropertyChanged();
        }
    }
    

    public string Name
    {
        get=> this._name; 
        set
        {
            this._name = value;
            this.OnPropertyChanged();
        }       
    }
    
    public string Description { 
        get => this._description; 
        set
        {
            this._description = value;
            this.OnPropertyChanged();
        }
    }
    
    public string StartLocation { 
        get => this._startLocation; 
        set
        {
            this._startLocation = value;
            this.OnPropertyChanged();
        }
    }
    
    public string EndLocation { 
        get => this._endLocation; 
        set
        {
            this._endLocation = value;
            this.OnPropertyChanged();
        }
    }
    
    public TransportType Type { 
        get => this._type; 
        set
        {
            this._type = value;
            this.OnPropertyChanged();
        }
    }
    
    public float Distance { 
        get => this._distance; 
        set
        {
            this._distance = value;
            this.OnPropertyChanged();
        }
    }
    
    public float Time { 
        get => this._time; 
        set
        {
            this._time = value;
            this.OnPropertyChanged();
        }
    }
    
    public string ImagePath { 
        get => this._imagePath; 
        set
        {
            this._imagePath = value;
            this.OnPropertyChanged();
        }
    }
    
    public void AddDemoLogs()
    {
        //add random number (1-5) of logs to the tour
        int random = new Random().Next(1, 5);
        
        for (int i = 0; i < random; i++)
        {
            Logs.Add(GetDemoLog());
        }
        
    }

    public TourLogs GetDemoLog()
    {
        
        TourLogs log = new TourLogs();
        
        log.Date = DateTime.Now.ToString("dd.MM.yyyy");
        log.TotalTime = 5;
        // log.TotalTime = new Random().Next(1, 5);
        log.Comment = "Demo Comment";
        log.Difficulty = (Difficulty)1;
        log.TotalDistance = 3;
        log.Rating = (Rating)2;
        // log.Difficulty = (Difficulty)new Random().Next(0, 3);
        // log.TotalDistance = new Random().Next(1, 5);
        // log.Rating = (Rating)new Random().Next(0, 3);
        
        return log;
     
    }
    
    public void Clear ()
    {
        this.Name = "";
        this.Description = "";
        this.StartLocation = "";
        this.EndLocation = "";
        this.Type = TransportType.Car;
        this.Distance = 0;
        this.Time = 0;
        this.ImagePath = "";
    }
    
    public bool HasValidInput()
    {
        return IsValidName(this.Name);
        
    }
    
    private bool IsValidName(string name)
    {
        //if the name is null or empty or does not only contain letters and numbers return false
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }
        else
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            if (!regex.IsMatch(name))
            {
                return false;
            }
        }
        return true;
    }
    
   
    
    
    
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}