using Asana.Library.Models;
using Asana.Maui.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Services
{
    public class ProjectServiceProxy
    {
        private List<Project> projects;
        public List<Project> Projects
        {
            get
            {
                return projects;
            }
        }
        private ProjectServiceProxy()
        {
       
            var projectData = new WebRequestHandler().Get("/Project/Expand").Result;
            projects = JsonConvert.DeserializeObject<List<Project>>(projectData) ?? new List<Project>();
        }
        private static object _lock = new object();
        private static ProjectServiceProxy? instance;
        public static ProjectServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ProjectServiceProxy();
                    }
                }

                return instance;
            }
        }

        public async Task<Project?> AddOrUpdate(Project? project)
        {
            if (project == null)
            {
                return project;
            }
            var isNewProject = project.Id == 0;
            var projectData = await new WebRequestHandler().Post("/Project", project);
            var newProject = JsonConvert.DeserializeObject<Project>(projectData);

            if (newProject != null)
            {
                if (!isNewProject)
                {
                    var existingProject = projects.FirstOrDefault(p => p.Id == newProject.Id);
                    if (existingProject != null)
                    {
                        var index = projects.IndexOf(existingProject);
                        projects.RemoveAt(index);
                        projects.Insert(index, newProject);
                    }

                }
                else
                {
                    projects.Add(newProject);
                }

            }

            return project;
        }

        public void DisplayProjects()
        {
            Projects.ForEach(Console.WriteLine);
        }

        public Project? GetById(int id)
        {
            return Projects.FirstOrDefault(p => p.Id == id);
        }

        public async Task DeleteProject(int id)
        {
            if (id == 0)
            {
                return;
            }
            var projectData = await new WebRequestHandler().Delete($"/Project/{id}");
            var projectToDelete =  JsonConvert.DeserializeObject<Project>(projectData);
            if (projectToDelete != null)
            {
                var localProject = projects.FirstOrDefault(p => p.Id == projectToDelete.Id);
                if (localProject != null)
                {
                    projects.Remove(localProject);
                }
            }

        }


    }
}
