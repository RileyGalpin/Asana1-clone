using Asana.API.Enterprise;
using Asana.Library.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ToDoApplication.Persistence
{
    public class Filebase
    {
        private string _root;
        private string _toDoRoot;
        private string _projectRoot;
        private static Filebase _instance;

        private List<ToDo> _toDoList;
        private bool _isToDoInitalized = false;
        


        public static Filebase Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = @"/Users/rileygalpin/programming/school/Asana1/database";
            _toDoRoot = $"{_root}/ToDos"; //home/temp/ToDos
            _projectRoot = $"{_root}/Projects";
        }

        public int LastKeyToDo
        {
            get
            {
                if (ToDos.Any())
                {
                    return ToDos.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

    public int LastKeyProject
        {
            get
            {
                if (Projects.Any())
                {
                    return Projects.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        public ToDo AddOrUpdate(ToDo toDo)
        {
            //set up a new Id if one doesn't already exist
            if (toDo.Id <= 0)
            {
                toDo.Id = LastKeyToDo + 1;
            }

            //go to the right place
            string path = $"{_toDoRoot}/{toDo.Id}.json"; // "\\" change here


            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(toDo));

            //return the item, which now has an id
            return toDo;
        }

        public Project AddOrUpdate(Project project)
        {
            //set up a new Id if one doesn't already exist
            if (project.Id <= 0)
            {
                project.Id = LastKeyProject + 1;
            }

            //go to the right place
            string path = $"{_projectRoot}/{project.Id}.json"; // "\\" change here


            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(project));

            //return the item, which now has an id
            return project;
        }


        public List<ToDo> ToDos
        {
            get
            {
                var root = new DirectoryInfo(_toDoRoot);
                var _toDos = new List<ToDo>();
                foreach (var patientFile in root.GetFiles())
                {
                    var toDo = JsonConvert
                        .DeserializeObject<ToDo>
                        (File.ReadAllText(patientFile.FullName));
                    if (toDo != null)
                    {
                        _toDos.Add(toDo);
                    }

                }
                return _toDos;
            }
        }

         public List<Project> Projects
        {
            get
            {
                var root = new DirectoryInfo(_projectRoot);
                var _projects = new List<Project>();
                foreach (var patientFile in root.GetFiles())
                {
                    var project = JsonConvert
                        .DeserializeObject<Project>
                        (File.ReadAllText(patientFile.FullName));
                    if (project != null)
                    {
                        _projects.Add(project);
                    }

                }
                return _projects;
            }
        }


        public bool DeleteToDo(int id)
        {

            if (id <= 0)
            {
                return false;
            }

            //go to the right place
            string path = $"{_toDoRoot}/{id}.json";


            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
                return true;
            }
            return false;
        }

         public bool DeleteProject(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            //go to the right place
            string path = $"{_projectRoot}/{id}.json";


            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
                return true;
            }
            return false;
        }

    
    
    }
    


   
}