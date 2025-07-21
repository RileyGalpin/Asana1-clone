using Asana.Library.Models;
using Asana.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Asana.Maui.ViewModels
{
    public class ProjectViewModel
    {
        public ProjectViewModel() {
            Model = new Project();

            DeleteCommand = new Command(async () => await DoDelete());
        }

        public ProjectViewModel(int id)
        {
            Model = ProjectServiceProxy.Current.GetById(id) ?? new Project();

            DeleteCommand = new Command(async () => await DoDelete());
        }

        public ProjectViewModel(Project? model)
        {
            Model = model ?? new Project();
            DeleteCommand = new Command(async () => await DoDelete());
        }

        public async Task DoDelete() {

           await ProjectServiceProxy.Current.DeleteProject(Model?.Id ?? 0);
        }

        public Project? Model { get ; set; }
        public ICommand? DeleteCommand { get; set; }

        public async Task AddOrUpdateProject()
        {
            try
            {
                await ProjectServiceProxy.Current.AddOrUpdate(Model);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding/updating project: {ex.Message}");
            }
        }


    }
}
