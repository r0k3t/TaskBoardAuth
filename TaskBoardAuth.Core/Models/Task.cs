using System;
using System.ComponentModel.DataAnnotations;

namespace TaskBoardAuth.Core.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public Guid OwnerId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public Project Project { get; set; }
        public int LocationTop { get; set; }
        public int LocationLeft { get; set; }
    }
}