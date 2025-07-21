using Asana.Library.Models;
using Asana.Maui.ViewModels;

namespace Asana.Maui.Views;

[QueryProperty(nameof(ProjectId), "projectId")]
public partial class ProjectsPageView : ContentPage
{
    public ProjectsPageView()
    {
        InitializeComponent();

    }
    public int ProjectId { get; set; }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectViewModel)?.AddOrUpdateProject();
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        //BindingContext = new ProjectViewModel(ProjectId);
    }


protected override async void OnAppearing()
{
    base.OnAppearing();
    
    // Small delay to ensure query parameters are processed
    await Task.Delay(100);
    
    System.Diagnostics.Debug.WriteLine($"OnAppearing (after delay) - ProjectId: {ProjectId}");
    BindingContext = new ProjectViewModel(ProjectId);
}
}



