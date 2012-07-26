using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskBoardAuth.Models;
using TaskBoardAuth.Services;

namespace TaskBoardAuth.Controllers
{
    public class TaskBoardController : Controller
    {
        private readonly ITaskBoardService service;
        private readonly IStaticMembershipService staticMembershipService;
        private readonly IProfileFactoryService profileFactoryService;

        public TaskBoardController(ITaskBoardService service, IStaticMembershipService staticMembershipService, IProfileFactoryService profileFactoryService)
        {
            this.service = service;
            this.staticMembershipService = staticMembershipService;
            this.profileFactoryService = profileFactoryService;
        }

        [Authorize]
        public ViewResult Index()
        {
            return View(service.GetProjects());
        }
        
        [HttpGet]
        public ViewResult TaskBoard(int projectId)
        {
            var userName = staticMembershipService.GetUser().UserName;
            var taskBoardModel = service.GetTaskBoardModel(projectId);
            taskBoardModel.Name = profileFactoryService.GetPropertyValue(userName, "FirstName");
            return View(taskBoardModel);
        }

        [HttpGet]
        public ViewResult Edit(int projectId)
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateTask(Task task)
        {
            task.OwnerId = (Guid)staticMembershipService.GetUser().ProviderUserKey;
            task = service.SaveNewTask(task);
            
            return Json(task);
        }

        [HttpPost]
        [Authorize]
        public JsonResult CreatProject(Project project)
        {
            project.ProjectStatus = ProjectStatus.Open;
            project.OwnerId = (Guid)staticMembershipService.GetUser().ProviderUserKey;
            service.SaveProject(project);

            return Json(project);
        }

    }
}
