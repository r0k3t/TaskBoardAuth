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
        private TaskManagerContext context;
        public TaskBoardService(TaskManagerContext context)
        {
            this.context = context;
        }
        public List<Project> GetProjects()
        {
            return context.Projects.ToList();
        }

        public TaskBoardModel GetTaskBoardModel(int projectId)
        {
            var tasks = context.Tasks.Where(x => x.ProjectId == projectId).ToList();
            var project = context.Projects.Single(x => x.ProjectId == projectId);
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

        public Task SaveNewTask(Task task)
        {
            context.Tasks.Add(task);
            context.SaveChanges();
            return task;
        }
    }
}