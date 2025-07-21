using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Asana.Maui.ViewModels
{
    public class ProjectsPageViewModel : INotifyPropertyChanged
    {
 private ProjectServiceProxy _projectSvc;

        public ProjectsPageViewModel()
        {
            _projectSvc = ProjectServiceProxy.Current;
        }

        public ProjectViewModel SelectedProject { get; set; }

        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                var projectList 
                    = ProjectServiceProxy.Current
                    .Projects.Select(p => new ProjectViewModel(p));
                return new ObservableCollection<ProjectViewModel>(projectList);
            }
        }

        public int SelectedProjectId => SelectedProject?.Model?.Id ?? 0;



        public async Task DeleteProject()
        {
            if (SelectedProject == null)
            {
                return;
            }

            await ProjectServiceProxy.Current.DeleteProject(SelectedProject?.Model?.Id ?? 0);
            NotifyPropertyChanged(nameof(Projects));
        }

        public void RefreshPage()
        {
            NotifyPropertyChanged(nameof(Projects));
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // public double PercentCompleted()
        // {
        //     return ProjectServiceProxy.Current.ProjectPercentCompleted();
        // }


    }
}
