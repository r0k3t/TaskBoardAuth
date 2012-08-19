using System.Collections.Generic;

namespace TaskBoardAuth.Core.Models
{
    public class TaskBoardModel
    {
        public List<Task> Tasks { get; set; }
        public Project Project { get; set; }
        public string Name { get; set; }
    }
}