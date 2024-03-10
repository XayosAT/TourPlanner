using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TourPlanner.Model;

public class Tour : INotifyPropertyChanged
{
    public enum TransportType
    {
        Car,
        Bicycle,
        Foot
    }
    
    private string _name;
    private string _description;
    private string _startLocation;
    private string _endLocation;
    private TransportType _type;
    private float _distance;
    private float _time;
    private string _imagePath;
    
    

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
    
    
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}