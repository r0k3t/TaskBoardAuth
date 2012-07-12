using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskBoardAuth.Models;

namespace TaskBoardAuth.Services
{
    public interface ITaskBoardService
    {
        List<Project> GetProjects();
        TaskBoardModel GetTaskBoardModel(int projectId); 
        Task SaveNewTask(Task task);
    }
}