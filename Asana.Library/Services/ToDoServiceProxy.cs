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
    public class ToDoServiceProxy
    {
        private List<ToDo> _toDoList;
        public List<ToDo> ToDos { 
            get
            {
                return _toDoList.ToList();
            }

            private set {
                if (value != _toDoList)
                {
                    _toDoList = value;
                }
            }
        }

       private ToDoServiceProxy()
{
    try
    {
        var todoData = Task.Run(() => new WebRequestHandler().Get("/ToDo")).Result;
        _toDoList = JsonConvert.DeserializeObject<List<ToDo>>(todoData) ?? new List<ToDo>();
    }
    catch (Exception ex)
    {
        _toDoList = new List<ToDo>();
    }
}
         private static object _lock = new object();
        private static ToDoServiceProxy? instance;

        public static ToDoServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ToDoServiceProxy();
                    }
                }

                return instance;
            }
        }
        public async Task<ToDo?> AddOrUpdateAsync(ToDo? toDo)
        {
            if(toDo == null)
            {
                return toDo;
            }
            var isNewToDo = toDo.Id == 0;
           var todoData = await new WebRequestHandler().Post("/ToDo", toDo);
            var newToDo = JsonConvert.DeserializeObject<ToDo>(todoData);

            if (newToDo != null)
            {
                if(!isNewToDo)
                {
                    var existingToDo = _toDoList.FirstOrDefault(t => t.Id == newToDo.Id);
                    if(existingToDo != null)
                    {
                        var index = _toDoList.IndexOf(existingToDo);
                        _toDoList.RemoveAt(index);
                        _toDoList.Insert(index, newToDo);
                    }

                } else
                {
                    _toDoList.Add(newToDo);
                }

            }

            return newToDo;
        }

        public void DisplayToDos(bool isShowCompleted = false)
        {
            if (isShowCompleted)
            {
                ToDos.ForEach(Console.WriteLine);
            }
            else
            {
                ToDos.Where(t => (t != null) && !(t?.IsCompleted ?? false))
                                .ToList()
                                .ForEach(Console.WriteLine);
            }
        }

        public ToDo? GetById(int id)
        {
            return ToDos.FirstOrDefault(t => t.Id == id);
        }

        public async Task DeleteToDo(int id)
        {
            if (id == 0)
            {
                return;
            }
            var todoData = await new WebRequestHandler().Delete($"/ToDo/{id}");
             Console.WriteLine($"DEBUG: todoData = '{todoData}' (Length: {todoData?.Length ?? 0})");
            var toDoToDelete = JsonConvert.DeserializeObject<ToDo>(todoData);
            if(toDoToDelete != null)
            {
                var localToDo = _toDoList.FirstOrDefault(t => t.Id == toDoToDelete.Id);
                if(localToDo != null)
                {
                    _toDoList.Remove(localToDo);
                }
            }

        }

    }
}
