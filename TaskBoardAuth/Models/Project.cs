using System;
using System.ComponentModel.DataAnnotations;
using TaskBoardAuth.Models;

namespace TaskBoardAuth.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus ProjectStatus { get; set; }

    }
}