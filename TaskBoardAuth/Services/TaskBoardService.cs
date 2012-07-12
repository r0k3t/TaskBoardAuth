using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskBoardAuth.Models;
using TaskBoardAuth.Models;

namespace TaskBoardAuth.Services
{
    public class TaskBoardService: ITaskBoardService
    {
        public List<Project> GetProjects()
        {
            var context = new TaskManagerContext();
            return context.Projects.ToList();
        }

        public TaskBoardModel GetTaskBoardModel(int projectId)
        {
            var context = new TaskManagerContext();
            var tasks = context.Tasks.Where(x => x.ProjectId == projectId).ToList();
            var project = context.Projects.Single(x => x.ProjectId == projectId);
            return new TaskBoardModel
                       {
                           Project = project,
                           Tasks = tasks
                       };
        }

        //public List<Task> GetTasks(int projectId)
        //{
        //    var context = new TaskManagerContext();
        //    return context.Tasks.Where(x => x.ProjectId == projectId).ToList();
        //}

        public Task SaveNewTask(Task task)
        {
            var context = new TaskManagerContext();
            task.CreatedById = 1;
            task.UpdatedById = 1;
            task.TaskOwnerId = 1;
            context.Tasks.Add(task);
            context.SaveChanges();
            return task;
        }
    }
}