using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TourPlanner.Enums;

namespace TourPlanner.Models;


public class TourLogs : INotifyPropertyChanged
{
    
    
    private string? _date;
    private float _totalTime;
    private string? _comment;
    private Difficulty _difficulty;
    private float _totalDistance;
    private Rating _rating; 
    
    public string? Date
    {
        get => this._date; 
        set
        {
            this._date = value;
            this.OnPropertyChanged();
        }       
    }
    
    public float TotalTime { 
        get => this._totalTime; 
        set
        {
            this._totalTime = value;
            this.OnPropertyChanged();
        }
    }
    
    public string? Comment { 
        get => this._comment; 
        set
        {
            this._comment = value;
            this.OnPropertyChanged();
        }
    }
    
    public Difficulty Difficulty { 
        get => this._difficulty; 
        set
        {
            this._difficulty = value;
            this.OnPropertyChanged();
        }
    }
    
    public float TotalDistance { 
        get => this._totalDistance; 
        set
        {
            this._totalDistance = value;
            this.OnPropertyChanged();
        }
    }
    
    public Rating Rating { 
        get => this._rating; 
        set
        {
            this._rating = value;
            this.OnPropertyChanged();
        }
    }
    
    public void Clear()
    {
        this.Date = null;
        this.TotalTime = 0;
        this.Comment = null;
        this.Difficulty = 0;
        this.TotalDistance = 0;
        this.Rating = 0;
    }
    
    
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}