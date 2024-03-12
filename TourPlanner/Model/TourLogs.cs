using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TourPlanner.Model;


public class TourLogs : INotifyPropertyChanged
{
    
    public enum DifficultyType
    {
        Easy,
        Medium,
        Hard
    }
    
    public enum RatingType
    {
        Bad,
        Good,
        VeryGood,
        Excellent
    }
    
    private string _date;
    private float _totalTime;
    private string _comment;
    private DifficultyType _difficulty;
    private float _totalDistance;
    private RatingType _rating; 
    
    public string Date
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
    
    public string Comment { 
        get => this._comment; 
        set
        {
            this._comment = value;
            this.OnPropertyChanged();
        }
    }
    
    public DifficultyType Difficulty { 
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
    
    public RatingType Rating { 
        get => this._rating; 
        set
        {
            this._rating = value;
            this.OnPropertyChanged();
        }
    }
    
    
    
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}