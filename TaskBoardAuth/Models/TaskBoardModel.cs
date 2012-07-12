using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskBoardAuth.Models;

namespace TaskBoardAuth.Models
{
    public class TaskBoardModel
    {
        public List<Task> Tasks { get; set; }
        public Project Project { get; set; }
    }
}