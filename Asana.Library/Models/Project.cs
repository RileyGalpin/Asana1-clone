using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asana.Library.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    private double? _percentCompleted;
         public double? PercentCompleted 
    { 
        get 
        {
                if (_percentCompleted == null && ToDoList != null)
                {
                    // Calculate it on-demand
                    var totalTodos = ToDoList.Count(t => t != null);
                    var completedTodos = ToDoList.Count(t => t != null && t.IsCompleted == true);

                    if (totalTodos > 0)
                    {
                        _percentCompleted = Math.Round((double)completedTodos / totalTodos, 2);
                    }
                    else
                    {
                        _percentCompleted = 0;
                    }
                }
                else
                {
                    _percentCompleted = 0;
            }
            return _percentCompleted;
        }
        set => _percentCompleted = value;
    }
        public List<ToDo>? ToDoList { get; set; }
        
        public override string ToString()
        {
            return $"[{Id}] {Name} - {Description} : {PercentCompleted}";
        }
    }
}
