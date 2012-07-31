using System;
using System.Collections.Generic;
using System.Linq;
using TaskBoardAuth.Models;

namespace TaskBoardAuth.Services
{
    public class TaskBoardService : ITaskBoardService
    {
        private readonly TaskManagerContext context;

        public TaskBoardService(TaskManagerContext context)
        {
            this.context = context;
        }

        #region ITaskBoardService Members

        public List<Project> GetProjects()
        {
            return context.Projects.Where(x => x.ProjectStatus == (int)ProjectStatus.Open).ToList();
        }

        public TaskBoardModel GetTaskBoardModel(int projectId)
        {
            List<Task> tasks = context.Tasks.Where(x => x.ProjectId == projectId).ToList();
            Project project = context.Projects.Single(x => x.ProjectId == projectId);
            return new TaskBoardModel
                       {
                           Project = project,
                           Tasks = tasks
                       };
        }

        public Project SaveProject(Project project)
        {
            context.Projects.Add(project);
            context.SaveChanges();
            return project;
        }

        public OperationStatus CloseProject(int projectId)
        {
            var status = new OperationStatus {Success = true};
            try
            {
                context.Projects.Single(x => x.ProjectId == projectId).ProjectStatus = (int)ProjectStatus.Closed;
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                status.Success = false;
                status.ErrorMessege = ex.ToString();
            }
            return status;
        }

        public Task SaveNewTask(Task task)
        {
            context.Tasks.Add(task);
            context.SaveChanges();
            return task;
        }

        #endregion
    }
}