using System.Collections.Generic;
using TaskBoardAuth.Core.Models;

namespace TaskBoardAuth.Core.Interfaces
{
    public interface ITaskBoardRepository
    {
        List<Project> GetProjects();
        TaskBoardModel GetTaskBoardModel(int projectId); 
        Task SaveNewTask(Task task);
        Project SaveProject(Project project);
        OperationStatus CloseProject(int projectId);
        OperationStatus UpdateStatus(Task task);
    }
}