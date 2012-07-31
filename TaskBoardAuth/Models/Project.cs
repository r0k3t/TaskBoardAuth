using System;
using System.ComponentModel.DataAnnotations;

namespace TaskBoardAuth.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus ProjectStatusEnum
        {
            get
            {
                return (ProjectStatus) ProjectStatus;
            } 
            set
            {
                ProjectStatus = (int) value;
            }
        }

        public int ProjectStatus { get; set; }
    }
}