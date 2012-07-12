using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskBoardAuth.Controllers;
using TaskBoardAuth.Services;

namespace TaskBoardAuth
{
    public class TaskBoardControllerFactory : DefaultControllerFactory 
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if(controllerType == typeof(TaskBoardController))
                return new TaskBoardController(new TaskBoardService());
            return base.GetControllerInstance(requestContext, controllerType);

        }
    }
}