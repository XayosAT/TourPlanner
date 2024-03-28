using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TourPlanner.ViewModels;

// NOT IN USE
public class RouteViewModel : INotifyPropertyChanged
{
    
    public ICommand ShowAddLogFormCommand { get; set; }
    public ICommand AddLogCommand { get; set; }
    public ICommand CancelAddLogCommand { get; set; }
    private bool _showAddLogForm;
    
    
    public RouteViewModel()
    {
        ShowAddLogFormCommand = new RelayCommand(ShowAddLogFormAction);
    }
    
    private void ShowAddLogFormAction()
    {
        this.ShowAddLogForm = true;
    }
    
    
    public bool ShowAddLogForm
    {
        get => this._showAddLogForm;
        set
        {
            this._showAddLogForm = value;
            this.OnPropertyChanged();
        }
    }
    
    
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}