using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskBoardAuth.Models;
using TaskBoardAuth.Services;

namespace TaskBoardAuth.Controllers
{
    public class TaskBoardController : Controller
    {
        private ITaskBoardService service;

        public TaskBoardController(ITaskBoardService service)
        {
            this.service = service;
        }

        [Authorize]
        public ViewResult Index()
        {
            return View(service.GetProjects());
        }
        
        [HttpGet]
        [Authorize]
        public ViewResult TaskBoard(int projectId)
        {
            var profile = UserProfile.GetUserProfile(HttpContext.User.Identity.Name);
            profile.Description = "testing";
            return View(service.GetTaskBoardModel(projectId));
        }

        [HttpPost]
        [Authorize]
        public JsonResult CreateTask(Task task)
        {
            //todo - make sure the task has all the information it needs and then get the it saved to the DB 
            task = service.SaveNewTask(task);
            
            return Json(task);
        }

    }
}
