using Asana.Maui.ViewModels;

namespace Asana.Maui.Views;

public partial class ProjectsView : ContentPage
{
    public ProjectsView()
    {
        InitializeComponent();
        BindingContext = new ProjectsPageViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ProjectsPageView");
    }

    private void EditClicked(object sender, EventArgs e)
{
    var projectId = (BindingContext as ProjectsPageViewModel)?.SelectedProjectId ?? 0;
    
    // Debug: Check what ID we're trying to navigate with
    System.Diagnostics.Debug.WriteLine($"Navigating with ProjectId: {projectId}");
    
    if (projectId > 0)
    {
        // Make sure the parameter name matches exactly
        Shell.Current.GoToAsync($"//ProjectsPageView?projectId={projectId}");
    }
    else
    {
        DisplayAlert("Error", "No project selected", "OK");
    }
}
    // private void EditClicked(object sender, EventArgs e)
    // {
    //     var selectedId = (BindingContext as ProjectsPageViewModel)?.SelectedProjectId ?? 0;
    //     Shell.Current.GoToAsync($"//ProjectsPageView?projectId={selectedId}");
    // }

    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as ProjectsPageViewModel)?.DeleteProject();
    }
    
}