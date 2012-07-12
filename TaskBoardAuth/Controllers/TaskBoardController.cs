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

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
                return View(service.GetProjects());
            return RedirectToAction("Login", "Account");
        }
        
        [HttpGet]
        public ViewResult TaskBoard(int projectId)
        {
            return View(service.GetTaskBoardModel(projectId));
        }

        [HttpPost]
        public JsonResult CreateTask(Task task)
        {
            //todo - make sure the task has all the information it needs and then get the it saved to the DB 
            task = service.SaveNewTask(task);
            
            return Json(task);
        }

    }
}
