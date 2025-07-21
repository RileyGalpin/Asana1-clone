using Api.ToDoApplication.Persistence;
using Asana.API.Database;
using Asana.Library.Models;

namespace Asana.API.Enterprise
{
    public class ProjectEC
    {
        public ProjectEC()
        {
            
        }

        public IEnumerable<Project>? Get(bool Expand = false)
        {
            //return Filebase.Current.Projects.Take(100);
            return Filebase.Current.Projects.Take(100);
        }

        public Project? GetById(int id)
        {
            return Filebase.Current.Projects.FirstOrDefault(p => p.Id == id);
        }

        public Project? AddOrUpdate(Project? project)
        {
            if(project == null)
            {
                return project;
            }

            Filebase.Current.AddOrUpdate(project);
            return project;
        }

        public Project? Delete(int id)
        {
            var projectToDelete = GetById(id);
            if (projectToDelete != null)
            {
                Filebase.Current.DeleteProject(projectToDelete.Id);
            }
            return projectToDelete;
        }
    }
}
