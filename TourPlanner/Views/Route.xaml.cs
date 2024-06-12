using System.Windows.Controls;
using TourPlanner.ViewModels;

namespace TourPlanner.Views;

public partial class Route : UserControl
{
    public Route(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.MapWebView = MapWebView;
        
        InitializeWebViewAsync(viewModel);
    }
    
    private async void InitializeWebViewAsync(MainViewModel viewModel)
    {
        await MapWebView.EnsureCoreWebView2Async();
        viewModel.LoadMap();
    }
}