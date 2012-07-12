﻿using System;
using System.ComponentModel.DataAnnotations;
using TaskBoardAuth.Models;

namespace TaskBoardAuth.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        public int CreatedById { get; set; }
        public int ProjectOwnerId { get; set; }
        public int UpdatedById { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public User Owner { get; set; }
        public User CreatedBy { get; set; }
        public User UpdatedBy { get; set; }
    }
}