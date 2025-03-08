using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string Title { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty; 
        public bool IsCompleted { get; set; } = true; 
        public DateTime DueDate { get; set; } 
    }
}
