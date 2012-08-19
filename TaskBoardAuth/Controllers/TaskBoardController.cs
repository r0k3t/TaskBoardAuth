using System;
using System.Web.Mvc;
using TaskBoardAuth.Core.Interfaces;
using TaskBoardAuth.Core.Models;

namespace TaskBoardAuth.Controllers
{
    [Authorize]
    public class TaskBoardController : Controller
    {
        private readonly IProfileFactoryService profileFactoryService;
        private readonly ITaskBoardRepository repository;
        private readonly IStaticMembershipService staticMembershipService;

        public TaskBoardController(ITaskBoardRepository repository, IStaticMembershipService staticMembershipService,
                                   IProfileFactoryService profileFactoryService)
        {
            this.repository = repository;
            this.staticMembershipService = staticMembershipService;
            this.profileFactoryService = profileFactoryService;
        }

        public ViewResult Index()
        {
            return View(repository.GetProjects());
        }

        [HttpGet]
        public ViewResult TaskBoard(int projectId)
        {
            string userName = staticMembershipService.GetUser().UserName;
            TaskBoardModel taskBoardModel = repository.GetTaskBoardModel(projectId);
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
            task.LocationLeft = 12;
            task.LocationTop = 190;
            task.OwnerId = (Guid) staticMembershipService.GetUser().ProviderUserKey;
            task = repository.SaveNewTask(task);

            return Json(task);
        }

        [HttpPost]
        public JsonResult CreateProject(Project project)
        {
            if (string.IsNullOrEmpty(project.Name) || string.IsNullOrEmpty(project.Description))
                return Json(new Project
                                {
                                    Name = "Please supply a name for this project."
                                });
            project.ProjectStatus = (int) ProjectStatus.Open;
            project.OwnerId = (Guid) staticMembershipService.GetUser().ProviderUserKey;
            repository.SaveProject(project);

            return Json(project);
        }

        [HttpPost]
        [Authorize]
        public JsonResult CloseProject(int projectId)
        {
            OperationStatus status = repository.CloseProject(projectId);
            if (!status.Success)
                return Json(status.ErrorMessege);
            return Json("Project Id: " + projectId + " successfully closed.");
        }
    }
}